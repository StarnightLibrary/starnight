---
uid: articles-internal-responders
title: Responders
---

# Responders

Responders are a more powerful implementation of event handlers. A responder is a type implementing <xref href="Starnight.Internal.Gateway.Responders.IResponder`1">, the generic type being the @Starnight.Internal.Gateway.Events.IDiscordGatewayEvent the responder handles. Responders are registered using the `IServiceCollection.AddResponder` extension method, where they are given a [service lifetime](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.servicelifetime) and a <xref href="Starnight.Internal.Gateway.Responders.ResponderPhase?text=phase">.

Registered into the containing dependency injection container, the responder will obey its service lifetime and be able to request services from DI - Starnight only controls execution of the responder logic itself.

A single type can implement a single responder:

~~~cs
public class ExampleResponder : IResponder<DiscordGuildCreatedEvent>
{
    public ValueTask RespondAsync(DiscordGuildCreatedEvent @event);
}
~~~

... or it can implement multiple responders:

~~~cs
public class ExampleResponder :
    IResponder<DiscordGuildCreatedEvent>,
    IResponder<DiscordGuildDeletedEvent>
{
    public ValueTask RespondAsync(DiscordGuildCreatedEvent @event);
    public ValueTask RespondAsync(DiscordGuildDeletedEvent @event);
}
~~~

The only limit placed on the amount of interface implementation is the amount of valid event types. It is worth noting, however, that a single responder type can only have a single service lifetime and responder phase associated with it - it is impossible to register different lifetimes and phases for different implementations within the same type.

## Valid Events

In principle, responders can implement any event within the `Starnight.Internal.Gateway.Events.Inbound` namespace. This includes Dispatch events (located in their own sub-namespace) as well as control events.

A responder method will only be called if the dispatched event matches the method perfectly, with two exceptions:

1.  @Starnight.Internal.Gateway.Events.Inbound.DiscordUnknownInboundEvent: Responders handling this event will be called whenever Starnight failed to deserialize to any known event.
2. @Starnight.Internal.Gateway.IDiscordGatewayEvent: Responders handling this type will be called *every time any event is fired.* 

## Calling Order and Phases

Starnight makes a strong guarantee about responder *phase* order - that is, Starnight guarantees no responder of a subsequent phase will execute before all responders of the previous phase have finished.

Starnight does *not* make guarantees about responder execution order *within* a group. In practice, execution order will match registration order, but Starnight reserves the right to break this behaviour and relying on execution order within a group is undefined behaviour.

If you rely on two responders to be executed in a certain order, you must use phases - or alternatively refactor your logic to not depend on responder order.
