namespace Starnight.Internal.Entities.Guilds;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Users;

/// <summary>
/// Represents a discord emoji.
/// </summary>
public record DiscordEmoji
{
	/// <summary>
	/// Snowflake Identifier of this emoji.
	/// </summary>
	[JsonPropertyName("id")]
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	public Int64? Id { get; init; } // this is nullable here, which is why we dont inherit from DiscordSnowflakeObject.

	/// <summary>
	/// Emoji name.
	/// </summary>
	[JsonPropertyName("name")]
	public String? Name { get; init; }

	/// <summary>
	/// Array of role IDs allowed to use this emoji.
	/// </summary>
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("roles")]
	public Int64[]? AllowedRoles { get; init; }

	/// <summary>
	/// The uploader of this emoji.
	/// </summary>
	[JsonPropertyName("user")]
	public DiscordUser? Creator { get; init; }

	/// <summary>
	/// Whether this emoji must be wrapped in colons.
	/// </summary>
	[JsonPropertyName("require_colons")]
	public Boolean? RequiresColons { get; init; }

	/// <summary>
	/// Whether this emoji is managed by an integration.
	/// </summary>
	[JsonPropertyName("managed")]
	public Boolean? Managed { get; init; }

	/// <summary>
	/// Whether this emoji is animated.
	/// </summary>
	[JsonPropertyName("animated")]
	public Boolean? Animated { get; init; }

	/// <summary>
	/// Whether this emoji is available. May be false due to loss of server boosts.
	/// </summary>
	[JsonPropertyName("available")]
	public Boolean? Available { get; init; }
}
