namespace Starnight.Internal.Entities.Guilds;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Stickers;

/// <summary>
/// Represents guild data for a guild preview.
/// </summary>
public record DiscordGuildPreview : DiscordSnowflakeObject
{
	/// <summary>
	/// Guild name, 2 - 100 characters.
	/// </summary>
	[JsonPropertyName("name")]
	public String Name { get; init; } = default!;

	/// <summary>
	/// Icon hash for this guild.
	/// </summary>
	[JsonPropertyName("icon")]
	public String? Icon { get; init; }

	/// <summary>
	/// Splash hash for this guild.
	/// </summary>
	[JsonPropertyName("splash")]
	public String? Splash { get; init; }

	/// <summary>
	/// Discovery splash hash for this guild.
	/// </summary>
	[JsonPropertyName("discovery_splash")]
	public String? DiscoverySplash { get; init; }

	/// <summary>
	/// List of custom emotes in this guild.
	/// </summary>
	[JsonPropertyName("emojis")]
	public DiscordEmote[] Emotes { get; init; } = default!;

	/// <summary>
	/// List of guild features.
	/// </summary>
	[JsonPropertyName("features")]
	public String[] Features { get; init; } = default!;

	/// <summary>
	/// Approximate member count for this guild.
	/// </summary>
	[JsonPropertyName("approximate_member_count")]
	public Int32 ApproximateMemberCount { get; init; }

	/// <summary>
	/// Approximate online member count for this guild.
	/// </summary>
	[JsonPropertyName("approximate_presence_count")]
	public Int32 ApproximatePresenceCount { get; init; }

	/// <summary>
	/// Discovery description for this guild.
	/// </summary>
	[JsonPropertyName("description")]
	public String? Description { get; init; }

	/// <summary>
	/// Stickers for this guild.
	/// </summary>
	[JsonPropertyName("stickers")]
	public DiscordSticker[]? Stickers { get; init; }
}
