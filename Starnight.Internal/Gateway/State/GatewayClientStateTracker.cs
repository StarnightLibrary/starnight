namespace Starnight.Internal.Gateway.State;

/// <summary>
/// Tracks the state of the gateway.
/// </summary>
public record struct GatewayClientStateTracker
{
	public DiscordGatewayClientState State { get; internal set; }
}
