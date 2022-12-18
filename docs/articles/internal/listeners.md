---
uid: articles-internal-Listeners
title: Listeners
---

# Listeners

Listeners are a more powerful implementation of event handlers. A listener is a type implementing <xref href="Starnight.Internal.Gateway.Listeners.IListener`1">, the generic type being the @Starnight.Internal.Gateway.Events.IDiscordGatewayEvent the listener handles. Listeners are registered using the `IServiceCollection.AddListener` extension method, where they are given a [service lifetime](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.servicelifetime) and a <xref href="Starnight.Internal.Gateway.Listeners.ListenerPhase?text=phase">.

Registered into the containing dependency injection container, the listener will obey its service lifetime and be able to request services from DI - Starnight only controls execution of the listener logic itself.

A single type can implement a single listener:

~~~cs
public class ExampleListener : IListener<DiscordGuildCreatedEvent>
{
    public ValueTask ListenAsync(DiscordGuildCreatedEvent @event);
}
~~~

... or it can implement multiple Listeners:

~~~cs
public class ExampleListener :
    IListener<DiscordGuildCreatedEvent>,
    IListener<DiscordGuildDeletedEvent>
{
    public ValueTask ListenAsync(DiscordGuildCreatedEvent @event);
    public ValueTask ListenAsync(DiscordGuildDeletedEvent @event);
}
~~~

The only limit placed on the amount of interface implementation is the amount of valid event types. It is worth noting, however, that a single listener type can only have a single service lifetime and listener phase associated with it - it is impossible to register different lifetimes and phases for different implementations within the same type.

## Valid Events

In principle, listeners can implement any event within the `Starnight.Internal.Gateway.Events.Inbound` namespace. This includes Dispatch events (located in their own sub-namespace) as well as control events.

A listener method will only be called if the dispatched event matches the method perfectly, with two exceptions:

1. @Starnight.Internal.Gateway.Events.Inbound.DiscordUnknownInboundEvent: Listeners handling this event will be called whenever Starnight failed to deserialize to any known event.
2. @Starnight.Internal.Gateway.IDiscordGatewayEvent: Listeners handling this type will be called *every time any event is fired.*

## Calling Order and Phases

Starnight makes a strong guarantee about listener *phase* order - that is, Starnight guarantees no listener of a subsequent phase will execute before all listeners of the previous phase have finished.

Starnight does *not* make guarantees about listener execution order *within* a group. In practice, execution order will match registration order, but Starnight reserves the right to break this behaviour and relying on execution order within a group is undefined behaviour.

If you rely on two listeners to be executed in a certain order, you must use phases - or alternatively refactor your logic to not depend on listener order.
