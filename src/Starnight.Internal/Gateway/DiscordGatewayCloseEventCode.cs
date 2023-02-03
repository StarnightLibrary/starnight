namespace Starnight.Internal.Gateway;

/// <summary>
/// Represents gateway connection close codes. Each member documents whether a reconnect should be attempted upon
/// receiving this close code.
/// </summary>
public enum DiscordGatewayCloseEventCode
{
	/// <summary>
	/// Unknown Error. The client should attempt to reconnect.
	/// </summary>
	UnknownError = 4000,

	/// <summary>
	/// An invalid opcode or an invalid payload for a valid opcode was sent. The client should attempt to reconnect.
	/// </summary>
	UnknownOpcode,

	/// <summary>
	/// An invalid payload was sent. The client should attempt to reconnect.
	/// </summary>
	DecodeError,

	/// <summary>
	/// A payload was sent prior to <see cref="DiscordGatewayOpcode.Identify"/>. The client should attempt to reconnect.
	/// </summary>
	NotAuthenticated,

	/// <summary>
	/// The authentication token sent with <see cref="DiscordGatewayOpcode.Identify"/> was incorrect. The client should
	/// not attempt to reconnect.
	/// </summary>
	AuthenticationFailed,

	/// <summary>
	/// The client identified itself multiple times The client should attempt to reconnect.
	/// </summary>
	AlreadyAuthenticated,

	/// <summary>
	/// The sequence number sent when resuming was invalid. The client should attempt to reconnect, but not resume.
	/// </summary>
	InvalidSequence = 4007,

	/// <summary>
	/// A ratelimit was hit. The client should attempt to reconnect.
	/// </summary>
	Ratelimited,

	/// <summary>
	/// The current session timed out. The client should attempt to reconnect, but not resume.
	/// </summary>
	SessionTimeout,

	/// <summary>
	/// An invalid shard was sent while identifying. The client should not attempt to reconnect.
	/// </summary>
	InvalidShard,

	/// <summary>
	/// The current session would have handled too many guilds - you are required to shard in order to connect.
	/// Consequently, the client should not attempt to reconnect.
	/// </summary>
	ShardingRequired,

	/// <summary>
	/// An invalid gateway API version was sent. The client should not attempt to reconnect.
	/// </summary>
	InvalidAPIVersion,

	/// <summary>
	/// The intents passed were invalid. The client should not attempt to reconnect.
	/// </summary>
	InvalidIntents,

	/// <summary>
	/// The intents passed contained an intent you are not allowed to use. The client should not attempt to reconnect.
	/// </summary>
	DisallowedIntents
}
