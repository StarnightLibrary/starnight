namespace Starnight.Internal.Gateway;

/// <summary>
/// Represents the 12 opcodes defined by the gateway.
/// </summary>
public enum DiscordGatewayOpcode
{
	/// <summary>
	/// Clientbound. This is used when events are dispatched from Discord's side.
	/// </summary>
	Dispatch,

	/// <summary>
	/// Client- and serverbound. This is sent periodically to keep the connection alive. 
	/// </summary>
	Heartbeat,

	/// <summary>
	/// Serverbound. This is used to start a new gateway session.
	/// </summary>
	Identify,

	/// <summary>
	/// Serverbound. This is used to update the client's presence.
	/// </summary>
	PresenceUpdate,

	/// <summary>
	/// Serverbound. This is used to update the client's voice state.
	/// </summary>
	VoiceStateUpdate,

	/// <summary>
	/// Serverbound. This is used to resume a previous session that was invalidated.
	/// </summary>
	Resume = 6,

	/// <summary>
	/// Clientbound. This indicates to the client that it should attempt to reconnect and <see cref="Resume"/> immediately.
	/// </summary>
	Reconnect,

	/// <summary>
	/// Serverbound. This is used to request information about offline guild members in large guilds.
	/// </summary>
	RequestGuildMembers,

	/// <summary>
	/// Clientbound. This indicates to the client that its session is invalid, and that it should reconnect and
	/// <see cref="Identify"/>/<see cref="Resume"/> accordingly.
	/// </summary>
	InvalidSession,

	/// <summary>
	/// Clientbound. This sends the client the heartbeat interval to use.
	/// </summary>
	Hello,

	/// <summary>
	/// Clientbound. This is sent in response to a <see cref="Heartbeat"/> indicating it has been received successfully.
	/// </summary>
	HeartbeatAck
}
