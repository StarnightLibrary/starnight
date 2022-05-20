namespace Starnight.Internal.Rest.Resources;

using Microsoft.Extensions.Caching.Memory;

/// <summary>
/// An abstract base class for all rest resources.
/// </summary>
public abstract class AbstractRestResource
{
	/// <summary>
	/// MemoryCache holding all ratelimit buckets currently in use.
	/// </summary>
	public IMemoryCache RatelimitBucketCache { get; internal set; }

	/// <inheritdoc/>
	public AbstractRestResource(IMemoryCache ratelimitBucketCache)
		=> this.RatelimitBucketCache = ratelimitBucketCache;
}
