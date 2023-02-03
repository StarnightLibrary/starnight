namespace Starnight.Internal.Gateway.State;

/// <summary>
/// Represents different possible states of the gateway client.
/// </summary>
public enum DiscordGatewayClientState
{
	/// <summary>
	/// Disconnected, can connect.
	/// </summary>
	Disconnected,

	/// <summary>
	/// Connecting to Discord.
	/// </summary>
	Connecting,

	/// <summary>
	/// Identifying to the Discord gateway.
	/// </summary>
	Identifying,

	/// <summary>
	/// Connected to and able to interact with Discord.
	/// </summary>
	Connected,

	/// <summary>
	/// Disconnected, can resume.
	/// </summary>
	DisconnectedResumable,

	/// <summary>
	/// Resuming an existing session.
	/// </summary>
	Resuming,

	/// <summary>
	/// Zombied - Discord has failed to acknowledge several heartbeats.
	/// </summary>
	Zombied
}
