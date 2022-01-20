namespace Starnight.Internal.Rest;

using System;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Starnight.Internal.Utils;

/// <summary>
/// Represents a rest client for the discord API.
/// </summary>
public sealed class RestClient : IDisposable
{
	private static readonly Regex __route_regex;
	private static HttpClient __http_client;
	private readonly ILogger? __logger;
	private readonly ConcurrentDictionary<String, RatelimitBucket> __ratelimit_buckets;
	private DateTimeOffset __continue_at;

	public event Action<RatelimitBucket, HttpResponseMessage> SharedRatelimitHit = null!;
	public event Action<RatelimitBucket, HttpResponseMessage, String> RatelimitHit = null!;
	public event Action<Guid> RequestDenied = null!;

	public event Action<Guid, HttpResponseMessage> RequestSucceeded = null!;
	private readonly ConcurrentQueue<RestClientQueueItem> __request_queue;
	private readonly CancellationTokenSource[] __worker_cancellation_tokens;

	static RestClient()
	{
		__route_regex = new(@":([a-z_]+)");

		HttpClientHandler handler = new()
		{
			UseCookies = false,
			AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip
		};

		__http_client = new(handler)
		{
			BaseAddress = new Uri(DiscordApiConstants.BaseUri),
			Timeout = TimeSpan.FromSeconds(15)
		};

		__http_client.DefaultRequestHeaders.Add("UserAgent", StarnightConstants.UserAgentHeader);
	}

	public static void SetClientProxy(IWebProxy proxy)
	{
		HttpClientHandler handler = new()
		{
			UseCookies = false,
			AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip,
			UseProxy = true,
			Proxy = proxy
		};

		TimeSpan timeout = __http_client.Timeout;

		__http_client = new(handler)
		{
			BaseAddress = new Uri(DiscordApiConstants.BaseUri),
			Timeout = timeout
		};
	}

	public static void SetTimeout(TimeSpan timeout)
		=> __http_client.Timeout = timeout;

	public RestClient(ILogger logger, Boolean enableQueue, Int32 workerThreadCount = 5)
	{
		this.__logger = logger;
		this.__ratelimit_buckets = new();

		if(enableQueue)
		{
			this.__request_queue = new();
			this.__worker_cancellation_tokens = new CancellationTokenSource[workerThreadCount];

			foreach(CancellationTokenSource cts in this.__worker_cancellation_tokens)
			{
				_ = Task.Run(() => this.workQueue(cts.Token));
			}
		}
		else
		{
			this.__request_queue = null!;
			this.__worker_cancellation_tokens = null!;
		}
	}

	// the GUID is intended for internal callback verification.
	public async Task<HttpResponseMessage> MakeRequestAsync(IRestRequest request, Guid guid)
	{
		if(this.__continue_at > DateTimeOffset.UtcNow) // validate global ratelimits
		{
			RequestDenied(guid);
			return null!;
		}

		if(!__route_regex.IsMatch(request.Route))
		{
			this.__logger?.LogError(LoggingEvents.RestClientRequestDenied,
				"Invalid request route. Please contact the library developers.");
			RequestDenied(guid);
			return null!;
		}

		HttpRequestMessage message = request.Build();

		// --- unreadable ternary ahead --- //

		RatelimitBucket v = !this.__ratelimit_buckets.ContainsKey(request.Route)
			? await this.createAndRegisterBucket(request.Route) // if this bucket is not registered yet: create one
			: this.__ratelimit_buckets[request.Route]; // we're good.

		// --- unreadable ternary over --- //

		if(!v.AllowRequest())
		{
#if DEBUG
			this.__logger?.LogError(LoggingEvents.RestClientRequestDenied,
				"Request {guid} to {route} was denied by the pre-emptive ratelimiter", guid, request.Route);
#else
			this.__logger?.LogWarning(LoggingEvents.RestClientRequestDenied, "The request was denied.");
#endif
			RequestDenied(guid);
		}

		HttpResponseMessage response = await __http_client.SendAsync(message);

		v.ProcessResponse(response);

		this.__ratelimit_buckets[request.Route] = v;

		return response;
	}

	public Boolean AllowRequest(String route)
		=> this.__ratelimit_buckets[route]?.AllowRequest() ?? false;

	public void EnqueueRequest(IRestRequest request, Guid guid)
	{
		this.__request_queue.Enqueue(new()
		{
			Request = request,
			RequestGuid = guid
		});
	}

	private Task<RatelimitBucket> createAndRegisterBucket(String route)
	{
		RatelimitBucket v = this.__ratelimit_buckets.AddOrUpdate(route,
			xm => new RatelimitBucket
			{
				Path = route,
				IsRatelimitDetermined = false
			},
			(x, y) => this.__ratelimit_buckets[route]);

		v.RatelimitHit += this.ratelimitHitHandler;
		v.SharedRatelimitHit += this.sharedRatelimitHitHandler;

		return Task.FromResult(v);
	}

	private void sharedRatelimitHitHandler(RatelimitBucket arg1, HttpResponseMessage arg2)
		=> this.SharedRatelimitHit(arg1, arg2);

	private void ratelimitHitHandler(RatelimitBucket arg1, HttpResponseMessage arg2, String arg3)
	{
		if(arg3 == "shared")
		{
			this.RatelimitHit(arg1, arg2, arg3);
			return;
		}
		
		this.__continue_at = arg1.ResetTime; 
	}

	private async void workQueue(CancellationToken ct)
	{
		while(!ct.IsCancellationRequested)
		{
			if(!this.__request_queue.TryDequeue(out RestClientQueueItem currentWorkItem))
			{
				continue;
			}

			if(!this.AllowRequest(currentWorkItem.Request.Route))
			{
				this.__request_queue.Enqueue(currentWorkItem);
				this.__logger?.LogDebug(LoggingEvents.RestClientQueueRequestPreemptivelyDenied,
					"The request was pre-emptively denied by the ratelimiter, re-queuing request...");
				continue;
			}

			HttpResponseMessage response = await this.MakeRequestAsync(currentWorkItem.Request, currentWorkItem.RequestGuid);
			this.RequestSucceeded(currentWorkItem.RequestGuid, response);
		}
	}

	public void Dispose()
	{
		foreach(CancellationTokenSource cts in this.__worker_cancellation_tokens)
		{
			cts.Cancel();
		}

		this.__request_queue.Clear();
	}
}
