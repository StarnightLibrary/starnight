using System.Runtime.CompilerServices;

using Starnight.Internal.Exceptions;

[assembly:TypeForwardedTo(typeof(AbstractStarnightException))]
[assembly:TypeForwardedTo(typeof(StarnightGatewayConnectionRefusedException))]
[assembly:TypeForwardedTo(typeof(StarnightInvalidConnectionException))]
[assembly:TypeForwardedTo(typeof(StarnightInvalidOutboundEventException))]
[assembly:TypeForwardedTo(typeof(StarnightInvalidResponderException))]
[assembly:TypeForwardedTo(typeof(StarnightRequestRejectedException))]
[assembly:TypeForwardedTo(typeof(StarnightSharedRatelimitHitException))]
