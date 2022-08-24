namespace Starnight.Internal.Gateway.Objects.Serverbound;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a payload for a <see cref="DiscordGatewayOpcode.Resume"/> command.
/// </summary>
public sealed record DiscordResumeCommandObject
{
	/// <summary>
	/// Session token.
	/// </summary>
	[JsonPropertyName("token")]
	public required String Token { get; init; }

	/// <summary>
	/// Session ID.
	/// </summary>
	[JsonPropertyName("session_id")]
	public required String SessionId { get; init; }

	/// <summary>
	/// Last sequence number received.
	/// </summary>
	[JsonPropertyName("seq")]
	public required Int32 Sequence { get; init; }
}
