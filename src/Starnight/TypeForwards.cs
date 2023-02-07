using System.Runtime.CompilerServices;

using Starnight;
using Starnight.Exceptions;

[assembly: TypeForwardedTo(typeof(AbstractStarnightException))]
[assembly: TypeForwardedTo(typeof(StarnightGatewayConnectionRefusedException))]
[assembly: TypeForwardedTo(typeof(StarnightInvalidConnectionException))]
[assembly: TypeForwardedTo(typeof(StarnightInvalidOutboundEventException))]
[assembly: TypeForwardedTo(typeof(StarnightInvalidListenerException))]
[assembly: TypeForwardedTo(typeof(StarnightRequestRejectedException))]
[assembly: TypeForwardedTo(typeof(StarnightSharedRatelimitHitException))]

[assembly: TypeForwardedTo(typeof(Optional<>))]
