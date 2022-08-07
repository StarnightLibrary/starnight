namespace Starnight.Internal.Entities.Guilds;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Users;

/// <summary>
/// Represents a guild integration application.
/// </summary>
public sealed record DiscordGuildIntegrationApplication : DiscordSnowflakeObject
{
	/// <summary>
	/// Name of the application.
	/// </summary>
	[JsonPropertyName("name")]
	public required String Name { get; init; }

	/// <summary>
	/// Icon hash of the application.
	/// </summary>
	[JsonPropertyName("icon")]
	public String? Icon { get; init; }

	/// <summary>
	/// Application description.
	/// </summary>
	[JsonPropertyName("description")]
	public required String Description { get; init; }

	/// <summary>
	/// Bot account associated with this application.
	/// </summary>
	[JsonPropertyName("bot")]
	public Optional<DiscordUser> Bot { get; init; }
}
