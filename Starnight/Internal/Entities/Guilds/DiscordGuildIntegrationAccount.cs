namespace Starnight.Internal.Entities.Guilds;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a Discord Guild Integration Account object.
/// </summary>
public record DiscordGuildIntegrationAccount
{
	/// <summary>
	/// ID of the integration account.
	/// </summary>
	[JsonPropertyName("id")]
	public String Id { get; init; } = default!;

	/// <summary>
	/// Name of the integration account.
	/// </summary>
	[JsonPropertyName("name")]
	public String Name { get; init; } = default!;
}
