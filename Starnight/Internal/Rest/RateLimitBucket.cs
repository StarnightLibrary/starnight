namespace Starnight.Internal.Rest;

using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using Starnight.Internal.Rest.Exceptions;

/// <summary>
/// Represents a REST ratelimit bucket.
/// </summary>
public record RatelimitBucket
{
	private const String __unlimited_ratelimit_hash = "unlimited";
	private const Int32 __default_maximum = 1;
	private const Int32 __default_remaining = 0;
	private readonly static DateTimeOffset __unix_epoch = new(1970, 0, 0, 0, 0, 0, new());

	public event Action<RatelimitBucket, HttpResponseMessage> SharedRatelimitHit = null!;
	public event Action<RatelimitBucket, HttpResponseMessage, String> RatelimitHit = null!;
	public event Action<RatelimitBucket, Int32, Int32> UpperLimitChanged = null!;

	/// <summary>
	/// Stores the values for all major parameters. May be <c>null</c>.
	/// </summary>
	public Int64[] RouteParameters { get; set; } = null!;

	/// <summary>
	/// Stores this request's bucketing path.
	/// </summary>
	public String? Path { get; init; } = null;

	public Int32 Maximum
	{
		get => this.__maximum;
		init => this.__maximum = value;
	}

#pragma warning disable CS0420

	private void setTotalLimit(Int32 value)
		=> Volatile.Write(ref this.__maximum, value);

	private volatile Int32 __maximum = __default_maximum;

	/// <summary>
	/// Keeps track of how many requests remain for this bucket.
	/// </summary>
	public Int32 Remaining
	{
		get => Volatile.Read(ref this.__remaining);
		set => Volatile.Write(ref this.__remaining, value);
	}

	private volatile Int32 __remaining = __default_remaining;

	/// <summary>
	/// Gets the current hashed ID for this bucket as returned by Discord.
	/// </summary>
	public String BucketId
		=> Volatile.Read(ref this.__ratelimit_hash);

	/// <summary>
	/// Keeps track of the current hashed ID for this bucket as returned by Discord.
	/// </summary>
	public String RatelimitHash
	{
		get => Volatile.Read(ref this.__ratelimit_hash);
		set
		{
			if(value == this.BucketId)
			{
				return;
			}

			this.__past_route_hashes.Add(this.BucketId);
			Volatile.Write(ref this.__ratelimit_hash, value);
		}
	}

	private volatile String __ratelimit_hash = __unlimited_ratelimit_hash;

	// stores all previous route hashes for this bucket, just to ensure discord isn't playing a trick on us.
	private readonly ConcurrentBag<String> __past_route_hashes = new();

	/// <summary>
	/// Keeps track of whether the ratelimit for the current bucket is determined.
	/// </summary>
	public Boolean IsRatelimitDetermined
	{
		get => Volatile.Read(ref this.__ratelimit_determined);
		set => Volatile.Write(ref this.__ratelimit_determined, value);
	}

	private volatile Boolean __ratelimit_determined = false;

	/// <summary>
	/// Keeps track of whether the last response in this bucket was a 429 Too Many Requests response.
	/// </summary>
	public Boolean WasLastResponse429
	{
		get => Volatile.Read(ref this.__last_response_429);
		set => Volatile.Write(ref this.__last_response_429, value);
	}

	private volatile Boolean __last_response_429 = false;


	/// <summary>
	/// Keeps track of the timestamp at which this rate limit bucket resets.
	/// </summary>
	public DateTimeOffset ResetTime
	{
		get
		{
			lock(this.__lock)
			{
				return this.__reset_time;
			}
		}

		set
		{
			lock(this.__lock)
			{
				this.__reset_time = value;
			}
		}
	}

	private DateTimeOffset __reset_time = DateTimeOffset.MinValue;
	private readonly Object __lock = new();
#pragma warning restore CS0420

	public RatelimitBucket Reset()
	{
		this.__maximum = __default_maximum;

		this.WasLastResponse429 = false;
		this.IsRatelimitDetermined = false;
		this.RatelimitHash = __unlimited_ratelimit_hash;
		this.Remaining = __default_remaining;

		return this;
	}

	public RatelimitBucket GetResetBucket()
	{
		RatelimitBucket newBucket = this;

		newBucket.__maximum = __default_maximum;
		newBucket.WasLastResponse429 = false;
		newBucket.IsRatelimitDetermined = false;
		newBucket.RatelimitHash = __unlimited_ratelimit_hash;
		newBucket.Remaining = __default_remaining;

		return newBucket;
	}

	public void ProcessResponse(HttpResponseMessage response)
	{
		if(response.StatusCode == HttpStatusCode.TooManyRequests)
		{
			this.WasLastResponse429 = true;

			String scope = response.Headers.GetValues("X-RateLimit-Scope").First();

			Int32 remainingIfShared = Int32.Parse(response.Headers.GetValues("X-RateLimit-Remaining").First());
			DateTimeOffset resetTime = __unix_epoch
						.AddSeconds(Double.Parse(response.Headers.GetValues("X-RateLimit-Reset").First()));
			String hash = response.Headers.GetValues("X-RateLimit-Bucket").First();

			switch(scope)
			{
				case "user":
					this.Remaining = 0;
					this.ResetTime = resetTime;
					this.RatelimitHash = hash;

					_ = Task.Run(() => this.RatelimitHit(this, response, "user"));
#if DEBUG
					throw new RatelimitExceededException("Exceeded user rate limit.", this, response);
#endif

				case "global":
					this.ResetTime = DateTimeOffset.UtcNow.AddSeconds(
						Double.Parse(response.Headers.GetValues("Retry-After").First()));

					_ = Task.Run(() => this.RatelimitHit(this, response, "global"));
#if DEBUG
					throw new GlobalRatelimitExceededException("Exceeded global rate limit.", this, response);
#endif

				case "shared":
					this.ResetTime = DateTimeOffset.UtcNow.AddSeconds(
						Double.Parse(response.Headers.GetValues("Retry-After").First()));
					this.Remaining = remainingIfShared;

					_ = Task.Run(() => this.SharedRatelimitHit(this, response));
					break;

				default:
					throw new NotImplementedException("No ratelimit of this type has been implemented.");
			}

			return;
		}

		Int32 limit = Int32.Parse(response.Headers.GetValues("X-RateLimit-Limit").First());
		Int32 remaining = Int32.Parse(response.Headers.GetValues("X-RateLimit-Remaining").First());
		Double reset = Double.Parse(response.Headers.GetValues("X-RateLimit-Reset").First());
		Double resetAfter = Double.Parse(response.Headers.GetValues("X-RateLimit-ResetAfter").First());
		String bucket = response.Headers.GetValues("X-RateLimit-Bucket").First();

		if(this.Maximum != limit)
		{
			this.UpperLimitChanged(this, this.Maximum, limit);
			this.setTotalLimit(limit);
		}

		this.Remaining = remaining;
		this.ResetTime = DateTimeOffset.UtcNow.AddSeconds(resetAfter);

		if((this.ResetTime - __unix_epoch.AddSeconds(reset)) > TimeSpan.FromSeconds(1))
		{
			throw new ArgumentException("Time discrepancy between local machine and remote server exceeds one second.");
		}

		this.RatelimitHash = bucket;
	}

	public Boolean AllowRequest()
	{
		if(this.Remaining == 0 && DateTimeOffset.UtcNow < this.ResetTime)
		{
			return false;
		}

		this.Remaining = this.Maximum;

		return true;
	}

	public void UpdateRatelimit()
	{
		if(DateTimeOffset.UtcNow > this.ResetTime)
		{
			this.Remaining = this.Maximum;
		}
	}
}
