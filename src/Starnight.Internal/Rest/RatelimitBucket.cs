namespace Starnight.Internal.Rest;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading;

/// <summary>
/// Represents a REST ratelimit bucket.
/// </summary>
public record RatelimitBucket
{
	private const String unlimitedRatelimitIdentifier = "unlimited";
	private const Int32 defaultLimit = 1;
	private const Int32 defaultRemaining = 0;
	private readonly static DateTimeOffset discordEpoch = new(2015, 1, 1, 0, 0, 0, TimeSpan.Zero);

	private readonly Object @lock = new();

	public Int32 Limit { get; internal set; } = defaultLimit;

#pragma warning disable CS0420
	public Int32 Remaining
	{
		get => Volatile.Read(ref this.remaining);
		set => Volatile.Write(ref this.remaining, value);
	}
#pragma warning restore CS0420

	private volatile Int32 remaining = defaultRemaining;

	public DateTimeOffset ResetTime { get; internal set; } = discordEpoch;

	public String Hash { get; internal set; } = unlimitedRatelimitIdentifier;

	#region Constructing

	public RatelimitBucket(Int32 limit, Int32 remaining, DateTimeOffset resetTime, String? hash)
	{
		this.Limit = limit;
		this.Remaining = remaining;
		this.ResetTime = resetTime;
		this.Hash = hash ?? unlimitedRatelimitIdentifier;
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

		lock(this.@lock)
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
