# Starnight.Extensions.Caching shims

This directory contains shims over the basic rest implementation in Starnight.Internal in order to cache received data and corroborate received objects with cached data.

These shims do not aim to return cached data instead of making a request. They also do not aim to cache data which cannot be reliably evicted from cache when invalid, regardless of eviction times. If there is no DELETE request or event and may become invalid over time, it must not be cached.

Since this excludes a lot of methods from caching and leaves them to simply redirect to the original implementation, shims are to be implemented in two files: `Caching<Name>RestResource.Shim.cs` and `Caching<Name>RestResource.Redirects.cs`. The name of the class is to be `Caching<Name>RestResource`. All methods adding their own logic are to be implemented in `.Shim.cs`, all methods redirecting to the original implementation are to be implemented in `.Redirects.cs`.

The location of a method is not permanent. If Discord adds or removes endpoints or events, methods may be moved.