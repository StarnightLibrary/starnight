---
uid: articles-internal-main
title: Starnight.Internal
---

# Starnight.Internal

If you've made your way down here, welcome in the depths of hell. If you've just stumbled upon this, you are advised to *leave. Now.*

Starnight.Internal is the package that directly wraps the Discord API, minus a handful of sanity changes exactly as outlined in the documentation. There is no handholding here and very little convenience features - you are exposed to the dangerous radiation of the Discord API. Hazmat suits are recommended.

There are very little reasons to use this, other than speed and potentially needing access to information not present in the main abstraction package.

If you do decide to dwell in this realm, to withstand the terrors of Tartarus ever-present, you have come to the right place.

On a preamble: It is very strongly discouraged to use Starnight.Internal without Starnight.Caching, which provides caching integration for data requested and obtained from the Discord API and gateway.

## Validity and Accuracy

Starnight.Internal does not validate or sanitize any information. Requests are taken at face value and passed to the Discord API directly, with the exception of rate-limiting to ensure Discords ratelimits are never hit during correct operation.

> [!CAUTION]
> This also means that 4xx error codes caused by user error will not be prevented by the library and will be counted towards your error threshold. Make sure to validate and sanitize data as you see fit before making API requests.

On the other hand, Starnight.Internal returns exactly what Discord returns without post-processing, which may be considered beneficial. This also means that features are available sooner, breaking changes may occur more frequently and that the library may see more of Discord testing-in-production.

## Usage

Starnight.Internal contains @Starnight.Internal.Gateway.DiscordGatewayClient to deal with events, as well as @Starnight.Internal.Rest.RestClient to send requests to the API. Furthermore, there are multiple classes for dealing with rest requests, ensuring correct payloads etc, known as *rest resources*.

Starnight.Internal operates upon dependency injection and has working integration for the .NET generic host. More information can be found at the [getting started article](./getting-started.md) - for this overview, let it suffice to say that it is very difficult, albeit not impossible, to operate Starnight.Internal without dependency injection. Extension methods are provided for registering the rest and gateway clients, optionally enabling generic host integration for the gateway.

## Concepts

### Listeners

Listeners are types implementing <xref href="Starnight.Internal.Gateway.Listeners.IListener`1"> and registered using the `AddListener` extension method. One listener type can implement however many listener interfaces it likes.

Whenever the @Starnight.Internal.Gateway.DiscordGatewayClient receives an event, a new service scope is created. Then, listeners are invoked within that scope by the order of <xref href="Starnight.Internal.Gateway.Listeners.ListenerPhase?text=phases">, where the specific order of listeners is undefined behaviour. More information can be found at the dedicated [article](./listeners.md).

> [!INFO]
> A listener type can only be registered to phases as a whole. If you need to fine-grain control of listener phases, you will need to register multiple different listener types.

### Rest Resources and Payloads

Rest resources are provided twofold: as interfaces, in `Starnight.Internal.Rest.Resources` and as concrete implementations, in `Starnight.Internal.Rest.Resources.Implementation`. Interfaces are provided for decoration, and if you use Starnight.Internal it is recommended to rely on the interfaces rather than the concrete implementations, however, the implementations are provided for use should the need arise.

> [!INFO]
> You can provide your own decorations to the interface types should you need to add functionality without changing the request itself.

Payload types are unvalidated types that contain the payloads, both request and response payloads, to any given request. It is highly advised to read the Discord documentation in addition to any documentation on the payload types, because Discord treats some payloads rather strangely.

More information can be found at the dedicated [article](./rest.md)
