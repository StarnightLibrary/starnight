namespace Starnight.Internal.Rest;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;

/// <summary>
/// Represents a REST ratelimit bucket.
/// </summary>
public record RatelimitBucket
{
	private const String __unlimited_ratelimit_identifier = "unlimited";
	private const Int32 __default_limit = 1;
	private const Int32 __default_remaining = 0;
	private readonly static DateTimeOffset __discord_epoch = new(2015, 1, 1, 0, 0, 0, TimeSpan.Zero);

	private readonly Object __lock = new();

	public Int32 Limit { get; internal set; } = __default_limit;

#pragma warning disable CS0420 
	public Int32 Remaining
	{
		get => Volatile.Read(ref this.__remaining);
		set => Volatile.Write(ref this.__remaining, value);
	}
#pragma warning restore CS0420

	private volatile Int32 __remaining = __default_remaining;

	public DateTimeOffset ResetTime { get; internal set; } = __discord_epoch;

	public String Hash { get; internal set; } = __unlimited_ratelimit_identifier;

	#region Constructing

	public RatelimitBucket(Int32 limit, Int32 remaining, DateTimeOffset resetTime, String? hash)
	{
		this.Limit = limit;
		this.Remaining = remaining;
		this.ResetTime = resetTime;
		this.Hash = hash ?? __unlimited_ratelimit_identifier;
	}

	public static Boolean ExtractRatelimitBucket(HttpResponseHeaders headers,

		[NotNullWhen(true)]
		out RatelimitBucket? bucket)
	{
		bucket = null!;

		try
		{
			if(!headers.TryGetValues("X-RateLimit-Limit", out IEnumerable<String>? limitRaw)
				|| !headers.TryGetValues("X-RateLimit-Remaining", out IEnumerable<String>? remainingRaw)
				|| !headers.TryGetValues("X-RateLimit-Reset", out IEnumerable<String>? ratelimitResetRaw))
			{
				return false;
			}

			if(!Int32.TryParse(limitRaw.SingleOrDefault(), out Int32 limit)
				|| !Int32.TryParse(remainingRaw.SingleOrDefault(), out Int32 remaining)
				|| !Double.TryParse(ratelimitResetRaw.SingleOrDefault(), out Double ratelimitReset))
			{
				return false;
			}

			String? hash = headers.GetValues("X-RateLimit-Name").SingleOrDefault();
			DateTimeOffset resetTime = DateTimeOffset.UnixEpoch + TimeSpan.FromSeconds(ratelimitReset);

			bucket = new(limit, remaining, resetTime, hash);
			return true;
		}
		catch
		{
			return false;
		}
	}

	#endregion

	public void ResetBucket(DateTimeOffset nextResetTime)
	{
		if(nextResetTime < this.ResetTime)
		{
			throw new ArgumentOutOfRangeException(nameof(nextResetTime),
				"The next ratelimit bucket expiration cannot be in the past.");
		}

		lock(this.__lock)
		{
			this.Remaining = this.Limit;
			this.ResetTime = nextResetTime;
		}
	}

	public Boolean AllowNextRequest()
	{
		if(this.Remaining <= 0)
		{
			return this.ResetTime < DateTimeOffset.UtcNow;
		}

		this.Remaining--;
		return true;
	}
}
