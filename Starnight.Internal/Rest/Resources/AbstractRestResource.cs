namespace Starnight.Internal.Rest.Resources;

using Starnight.Caching.Abstractions;

/// <summary>
/// An abstract base class for all rest resources.
/// </summary>
public abstract class AbstractRestResource
{
	/// <summary>
	/// MemoryCache holding all ratelimit buckets currently in use.
	/// </summary>
	public ICacheService RatelimitBucketCache { get; internal set; }

	/// <inheritdoc/>
	public AbstractRestResource
	(
		ICacheService ratelimitBucketCache
	)
		=> this.RatelimitBucketCache = ratelimitBucketCache;
}
