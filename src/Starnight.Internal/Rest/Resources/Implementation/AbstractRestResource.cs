namespace Starnight.Internal.Rest.Resources.Implementation;

using Starnight.Caching.Providers.Abstractions;

/// <summary>
/// An abstract base class for all rest resources.
/// </summary>
public abstract class AbstractRestResource
{
	/// <summary>
	/// MemoryCache holding all ratelimit buckets currently in use.
	/// </summary>
	public ICacheProvider RatelimitBucketCache { get; internal set; }

	/// <inheritdoc/>
	public AbstractRestResource
	(
		ICacheProvider ratelimitBucketCache
	)
		=> this.RatelimitBucketCache = ratelimitBucketCache;
}
