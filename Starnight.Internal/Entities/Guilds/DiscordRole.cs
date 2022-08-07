namespace Starnight.Internal.Entities.Guilds;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a discord role.
/// </summary>
public sealed record DiscordRole : DiscordSnowflakeObject
{
	/// <summary>
	/// Name of this role.
	/// </summary>
	[JsonPropertyName("name")]
	public required String Name { get; init; }

	/// <summary>
	/// Integer representation of this role's colour code.
	/// </summary>
	[JsonPropertyName("color")]
	public required Int32 Color { get; init; }

	/// <summary>
	/// Whether this role is hoisted on the member list.
	/// </summary>
	[JsonPropertyName("hoist")]
	public required Boolean Hoisted { get; init; }

	/// <summary>
	/// Role icon hash.
	/// </summary>
	[JsonPropertyName("icon")]
	public Optional<String?> Icon { get; init; }

	/// <summary>
	/// Unicode emoji serving as role icon.
	/// </summary>
	[JsonPropertyName("unicode_emoji")]
	public Optional<String?> UnicodeEmoji { get; init; }

	/// <summary>
	/// Position of this role in the role list.
	/// </summary>
	[JsonPropertyName("position")]
	public required Int32 Position { get; init; }

	/// <summary>
	/// Guild-wide permissions for this role, excluding channel overrides.
	/// </summary>
	[JsonPropertyName("permissions")]
	public required DiscordPermissions Permissions { get; init; }

	/// <summary>
	/// Whether this role is managed by an integration.
	/// </summary>
	[JsonPropertyName("managed")]
	public required Boolean Managed { get; init; }

	/// <summary>
	/// Whether this role is mentionable.
	/// </summary>
	[JsonPropertyName("mentionable")]
	public required Boolean Mentionable { get; init; }

	/// <summary>
	/// The role tags for this role.
	/// </summary>
	[JsonPropertyName("tags")]
	public Optional<DiscordRoleTags> Tags { get; init; }
}
