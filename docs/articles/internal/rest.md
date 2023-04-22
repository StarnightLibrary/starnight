---
uid: articles-internal-rest
title: Rest
name: Rest
---

# The Starnight Rest Infrastructure

Starnight exposes and implements Discord's rest API through its "Resources", named after what Discord calls them.
Resources are exposed as interfaces in `Starnight.Internal.Rest.Resources`, and any consuming API should stick to
using these. Their implementations are given in `Starnight.Internal.Rest.Resources.Implementation` and should only
be used in exceptional cases.

The motivation behind splitting them into interfaces and implementation is to provide an extensibility point: other
code may add shims to the resources, as is done in `Starnight.Caching.Shims`; or override the resources entirely,
for whichever purpose. Since using the interfaces hides these details from the end user, libraries need to make very
clear what they do and don't do to those resources, and in what order shims and overrides are applied, so as to avoid
uncertain behaviour.

## Payloads

To keep parameter lists civilized in length and to make your payloads at least somewhat reusable, Starnight wraps
each REST request's payload into a dedicated payload object. These payloads are found in `Starnight.Internal.Rest.Payloads`,
subdivided into further namespaces by whichever resource they appear in. Occasionally, if an API endpoint doesn't
return an entity by itself, Starnight also employs response payloads which then contain the properties you are
actually looking for.

The request objects are records, which means you can employ `with` expressions and similar language features to
reuse request objects, only making minimal changes as needed.

## Shims

Shims are ultimately decorators over the REST interfaces. Starnight provides a `Decorate<TInterface, TDecorator>`
extension method on `IServiceCollection` to register your shims; and it is important to note that shims are applied
to the underlying resource *in order of application.* This also means that if at some later point you switch the
underlying resource out for whatever reason, previously registered shims will not be applied to this new implementation.

### Creating Shims

Shims have to follow certain registration rules. Firstly, shims have to implement the interface type of the resource
they are trying to shim. Secondly, shims have to take an argument of exactly that interface type as their first
constructor argument and should use this underlying instance to make all requests before or after adding their own
logic. For instance, you could create a shim to validate all arguments passed to the API like so:

~~~cs
public class ValidatingChannelRestResource : IDiscordChannelRestResource
{
    private readonly IDiscordChannelRestResource underlying;

    public ValidatingChannelRestResource
    (
        IDiscordChannelRestResource underlying
    )
        => this.underlying = underlying;

    public async ValueTask<DiscordChannel> GetChannelAsync
    (
        Int64 channelId,
        CancellationToken ct = default
    )
    {
        if(channelId == 0)
        {
            throw new ArgumentException("Cannot fetch a channel with the ID 0.");
        }

        return await this.underlying.GetChannelAsync
        (
            channelId,
            ct
        );
    }

    // and so forth.
}
~~~

It is important to note that every shim has to implement every interface method. If you do not have additional logic
to provide for some methods, defer them to the underlying resource.
