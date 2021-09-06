namespace Starnight.Internal.Entities.Guild;

using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Users;

/// <summary>
/// Represents a guild integration application.
/// </summary>
public class DiscordGuildIntegrationApplication : DiscordSnowflake
{
	/// <summary>
	/// Name of the application.
	/// </summary>
	[JsonPropertyName("name")]
	public String Name { get; init; } = default!;

	/// <summary>
	/// Icon hash of the application.
	/// </summary>
	[JsonPropertyName("icon")]
	public String? Icon { get; init; }

	/// <summary>
	/// Application description.
	/// </summary>
	[JsonPropertyName("description")]
	public String Description { get; init; } = default!;

	/// <summary>
	/// Application summary.
	/// </summary>
	[JsonPropertyName("summary")]
	public String Summary { get; init; } = default!;

	/// <summary>
	/// Bot account associated with this application.
	/// </summary>
	[JsonPropertyName("bot")]
	public DiscordUser? Bot { get; init; }
}
