---
uid: articles-internal-getting-started
title: Getting Started
---

# Getting Started

Starnight.Internal is a complex beast, and you are expected to have competent understanding of C# and .NET if you wish to use Starnight.
Internal. No support is given to Visual Basic.NET, F# or any other .NET language - which might or might not work.

Starnight.Internal is designed to operate with dependency injection and ideally the .NET Generic Host, but it is perfectly possible to
operate without these, albeit painful - you will have to construct all objects yourself, including named loggers.

## 1. Simple Setup

Starnight.Internal provides a very simple, but not amazingly powerful way of registering everything in a single method call.

The extensions by the name of `.AddStarnightLibrary()` are provided both on `IServiceCollection` and `IHostBuilder`, and you can provide
them with whatever parameters you need - from just

~~~cs
hostBuilder.AddStarnightLibrary
(
    token: "example"
);
~~~

to

~~~cs
hostBuilder.AddStarnightLibrary
(
    token: "example",
    cacheKind: StarnightCacheKind.Memory,
    restClientOptions: new()
    {
        MedianFirstRequestRetryDelay = TimeSpan.FromSeconds(2),
        RatelimitedRetryCount = 100,
        RetryCount = 100
    }
);
~~~

Do note that this setup method does not allow you to register your own implementations or shims over default services.

## 2. Manual Setup

### 2.1. Registering into the .NET Generic Host

Starnight integrates with the generic host in that if registered to the generic host, Starnight will start when the host starts and exit
when the host exits.

> ![WARN]
> It is considered unsupported to run the application after the host has exited in this scenario. Starnight may break unexpectedly.

Starnight provides a extension method on `IHostBuilder`, `.AddStarnightGateway()`, which will appropriately register Starnight into the
generic host. This requires no additional effort, as far as the gateway is concerned.

### 2.2. Registering into an `IServiceCollection`

Starnight also provides a way to run in a service collection without the involvement of a host. To this purpose, there is an extension
method also by the name of `.AddStarnightGateway()` provided.

Should this approach be chosen, you will have to start the gateway client yourself. To this end, you will have to obtain the service later,
after all registration is performed, and call `StartAsync(CancellationToken)`; and when shutting down, you might want to call
`StopAsync(CancellationToken)` to perform a graceful shutdown. It is, however, perfectly possible to just leave the connection to its
fate when exiting the application.

### 2.3. Setting up everything else

No other component has specific handling for the generic host, and correspondingly they all only provide extension methods on the service
collection. If you opted to use the generic host, you will need to perform all of this registration in a `.ConfigureServices` lambda.

- You will have to pick a caching provider. Starnight, by default, contains a memory cache provider, which you will have to register to your service collection using the `.AddStarnightMemoryCache()` extension method.

  It is entirely possible to implement your own caching provider, and it is entirely possible for more providers to be added to Starnight in the future - in fact, if you devise your own provider, feel free to open a pull request.

- You will have to register Starnight's rest infrastructure, using the `.AddStarnightRestClient(RestClientOptions)` extension method. @Starnight.Internal.Rest.RestClientOptions contains a handful of parameters for how to deal with retries - on one hand, you generally want your requests to be executed, but on the other hand, you might not want to wait several minutes to do so; and Starnight lets you pick your own priorities and logic. More information can be found in the [rest article](./rest.md).

- You will have to tell Starnight about your bot token. To this end, @Starnight.Internal.TokenContainer exists, and typically you would assign your token more or less in the following way:

~~~cs
services.Configure<TokenContainer>
(
    xm => xm.Token = //...
);
~~~

- Finally, you need to register an underlying cache for your chosen provider, as well as a logger, into your service collection.

With all of that, you're good to go!

## 3. Using the library

Starnight exposes both main components of the Discord API to you. You will receive events sent from the gateway through
[listeners](./listeners.md) and you are able to make requests to the REST API through the [rest client](./rest.md).
Furthermore, Starnight allows you to send outbound gateway events - or gateway commands - through
@Starnight.Internal.Gateway.DiscordGatewayClient.SendOutboundEventAsync(Starnight.Internal.Gateway.Events.IDiscordGatewayEvent).
With this, you can access the entirety of Discords API and build your own bots - your creativity and Discord are your only limits.
