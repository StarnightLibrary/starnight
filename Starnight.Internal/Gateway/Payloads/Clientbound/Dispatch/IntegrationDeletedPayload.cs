namespace Starnight.Internal.Gateway.Payloads.Clientbound.Dispatch;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents the payload for an IntegrationDeleted event.
/// </summary>
public sealed record IntegrationDeletedPayload
{
	/// <summary>
	/// The ID of the integration.
	/// </summary>
	[JsonPropertyName("id")]
	public required Int64 IntegrationId { get; init; }

	/// <summary>
	/// The ID of the guild this integration belongs to.
	/// </summary>
	[JsonPropertyName("guild_id")]
	public required Int64 GuildId { get; init; }

	/// <summary>
	/// The ID of the application for this integration.
	/// </summary>
	[JsonPropertyName("application_id")]
	public Optional<Int64> ApplicationId { get; init; }
}
