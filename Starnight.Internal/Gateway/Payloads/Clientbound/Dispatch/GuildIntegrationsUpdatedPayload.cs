namespace Starnight.Internal.Gateway.Payloads.Clientbound.Dispatch;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents the payload for the GuildIntegrationsUpdated event.
/// </summary>
public sealed record GuildIntegrationsUpdatedPayload
{
	/// <summary>
	/// The ID of the guild whose integrations were updated.
	/// </summary>
	[JsonPropertyName("guild_id")]
	public required Int64 GuildId { get; init; }
}
