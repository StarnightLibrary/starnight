namespace Starnight.Internal.Rest;

using System;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

using Polly;

using Starnight.Caching.Providers.Abstractions;

public class PollyRateLimitPolicy : AsyncPolicy<HttpResponseMessage>
{
	private readonly ConcurrentDictionary<String, String> endpointBuckets;
	private readonly RatelimitBucket globalBucket;

	private readonly static TimeSpan oneSecond = TimeSpan.FromSeconds(1);

	public PollyRateLimitPolicy()
	{
		// 50 per second is discord's defined global ratelimit
		this.globalBucket = new(50, 50, DateTimeOffset.UtcNow.AddSeconds(1), "global");
		this.endpointBuckets = new();
	}

	protected override async Task<HttpResponseMessage> ImplementationAsync
	(
		Func<Context, CancellationToken, Task<HttpResponseMessage>> action,
		Context context,
		CancellationToken cancellationToken,
		Boolean continueOnCapturedContext = true
	)
	{
		if(!context.TryGetValue("endpoint", out Object endpointRaw) || endpointRaw is not String endpoint)
		{
			throw new InvalidOperationException("No endpoint passed.");
		}

		if(!context.TryGetValue("cache", out Object cacheRaw) || cacheRaw is not ICacheProvider cache)
		{
			throw new InvalidOperationException("No cache passed.");
		}

		// fail-earlies done, policy time
		DateTimeOffset instant = DateTimeOffset.UtcNow;
		Boolean exemptFromGlobalLimit = false;
		String bucketHash;

		if(context.TryGetValue("exempt-from-global-limit", out Object exemptRaw) && exemptRaw is Boolean exempt)
		{
			exemptFromGlobalLimit = exempt;
		}

		if(!exemptFromGlobalLimit)
		{
			if(this.globalBucket.ResetTime < instant)
			{
				this.globalBucket.ResetBucket(instant + oneSecond);
			}

			if(!this.globalBucket.AllowNextRequest())
			{
				HttpResponseMessage response = new(HttpStatusCode.TooManyRequests);

				response.Headers.RetryAfter = new RetryConditionHeaderValue(this.globalBucket.ResetTime - instant);
				response.Headers.Add("X-Starnight-Internal-Response", "global");

				return response;
			}
		}

		bucketHash = this.endpointBuckets.TryGetValue(endpoint, out String? bucketHashNullable)
			? bucketHashNullable
			: endpoint;

		RatelimitBucket? bucket = await cache.GetAsync<RatelimitBucket>(bucketHash);

		if(bucket is not null)
		{
			if(!bucket?.AllowNextRequest() ?? false)
			{
				HttpResponseMessage response = new(HttpStatusCode.TooManyRequests);

				response.Headers.RetryAfter = new RetryConditionHeaderValue(bucket!.ResetTime - instant);
				response.Headers.Add("X-Starnight-Internal-Response", "bucket");

				return response;
			}
		}

		HttpResponseMessage message = await action(context, cancellationToken).ConfigureAwait(continueOnCapturedContext);

		if(!RatelimitBucket.ExtractRatelimitBucket(message.Headers, out RatelimitBucket? extractedBucket))
		{
			return message;
		}

		if(extractedBucket.Hash is null)
		{
			_ = this.endpointBuckets.TryRemove(endpoint, out _);

			// expire a second later, to pre-act a server/local time desync
			await cache.CacheAsync(endpoint, extractedBucket);

			return message;
		}

		_ = this.endpointBuckets.AddOrUpdate(endpoint, extractedBucket.Hash, (_, _) => extractedBucket.Hash);
		await cache.CacheAsync(endpoint, extractedBucket);

		if(extractedBucket.Hash != bucketHash)
		{
			_ = await cache.RemoveAsync<RatelimitBucket>(bucketHash);
		}

		return message;
	}
}
