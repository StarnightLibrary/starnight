namespace Starnight.Internal.Entities.Guilds;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a discord role.
/// </summary>
public record DiscordRole : DiscordSnowflakeObject
{
	/// <summary>
	/// Name of this role.
	/// </summary>
	[JsonPropertyName("name")]
	public String Name { get; init; } = default!;

	/// <summary>
	/// Integer representation of this role's colour code.
	/// </summary>
	[JsonPropertyName("color")]
	public Int32 Color { get; init; }

	/// <summary>
	/// Whether this role is hoisted on the member list.
	/// </summary>
	[JsonPropertyName("hoist")]
	public Boolean Hoisted { get; init; }

	/// <summary>
	/// Role icon hash.
	/// </summary>
	[JsonPropertyName("icon")]
	public String? Icon { get; init; }

	/// <summary>
	/// Unicode emote serving as role icon.
	/// </summary>
	[JsonPropertyName("unicode_emote")]
	public String? UnicodeEmote { get; init; }

	/// <summary>
	/// Position of this role in the role list.
	/// </summary>
	[JsonPropertyName("position")]
	public Int32 Position { get; init; }

	/// <summary>
	/// Guild-wide permissions for this role, excluding channel overrides.
	/// </summary>
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("permissions")]
	public Int64 Permissions { get; init; }

	/// <summary>
	/// Whether this role is managed by an integration.
	/// </summary>
	[JsonPropertyName("managed")]
	public Boolean Managed { get; init; }

	/// <summary>
	/// Whether this role is mentionable.
	/// </summary>
	[JsonPropertyName("mentionable")]
	public Boolean Mentionable { get; init; }

	/// <summary>
	/// The role tags for this role.
	/// </summary>
	[JsonPropertyName("tags")]
	public DiscordRoleTags? Tags { get; init; }
}
