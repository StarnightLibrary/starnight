namespace Starnight.Internal.Gateway.Payloads.Inbound;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Guilds;
using Starnight.Internal.Entities.Teams;
using Starnight.Internal.Entities.Users;

/// <summary>
/// Represents the payload for a READY event. In Starnight, we call them Connected, as the connection
/// is in fact not quite ready when this event is fired.
/// </summary>
public sealed record ConnectedPayload
{
	/// <summary>
	/// Gateway version.
	/// </summary>
	[JsonPropertyName("v")]
	public required Int32 Version { get; init; }

	/// <summary>
	/// Information about the current user.
	/// </summary>
	[JsonPropertyName("user")]
	public required DiscordUser User { get; init; }

	/// <summary>
	/// The guilds the user is in. These will slowly be replaced with full guild objects in later events.
	/// </summary>
	[JsonPropertyName("guilds")]
	public required IEnumerable<DiscordUnavailableGuild> Guilds { get; init; }

	/// <summary>
	/// The session ID to be used for resuming connections.
	/// </summary>
	[JsonPropertyName("session_id")]
	public required String SessionId { get; init; }

	/// <summary>
	/// The gateway URL to be used for resuming connections.
	/// </summary>
	[JsonPropertyName("resume_gateway_url")]
	public required String ResumeGatewayUrl { get; init; }

	/// <summary>
	/// The shard information associated with this session.
	/// </summary>
	[JsonPropertyName("shard")]
	public Optional<IEnumerable<Int32>> Shard { get; init; }

	/// <summary>
	/// Partial application object.
	/// </summary>
	[JsonPropertyName("application")]
	public required DiscordApplication Application { get; init; }
}
