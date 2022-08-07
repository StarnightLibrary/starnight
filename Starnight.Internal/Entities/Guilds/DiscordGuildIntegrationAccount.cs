namespace Starnight.Internal.Entities.Guilds;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a Discord Guild Integration Account object.
/// </summary>
public sealed record DiscordGuildIntegrationAccount
{
	/// <summary>
	/// ID of the integration account.
	/// </summary>
	[JsonPropertyName("id")]
	public required String Id { get; init; }

	/// <summary>
	/// Name of the integration account.
	/// </summary>
	[JsonPropertyName("name")]
	public required String Name { get; init; }
}
