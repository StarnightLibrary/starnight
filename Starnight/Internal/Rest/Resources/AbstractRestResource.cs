namespace Starnight.Internal.Rest.Resources;

using Microsoft.Extensions.Caching.Memory;

public abstract class AbstractRestResource
{
	public IMemoryCache RatelimitBucketCache { get; internal set; }

	public AbstractRestResource(IMemoryCache ratelimitBucketCache)
		=> this.RatelimitBucketCache = ratelimitBucketCache;
}
