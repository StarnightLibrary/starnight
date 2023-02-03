namespace Starnight.Internal.Entities.Guilds;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Stickers;

/// <summary>
/// Represents guild data for a guild preview.
/// </summary>
public sealed record DiscordGuildPreview : DiscordSnowflakeObject
{
	/// <summary>
	/// Guild name, 2 - 100 characters.
	/// </summary>
	[JsonPropertyName("name")]
	public required String Name { get; init; }

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
	/// List of custom emojis in this guild.
	/// </summary>
	[JsonPropertyName("emojis")]
	public required IEnumerable<DiscordEmoji> Emojis { get; init; }

	/// <summary>
	/// List of guild features.
	/// </summary>
	[JsonPropertyName("features")]
	public required IEnumerable<String> Features { get; init; }

	/// <summary>
	/// Approximate member count for this guild.
	/// </summary>
	[JsonPropertyName("approximate_member_count")]
	public required Int32 ApproximateMemberCount { get; init; }

	/// <summary>
	/// Approximate online member count for this guild.
	/// </summary>
	[JsonPropertyName("approximate_presence_count")]
	public required Int32 ApproximatePresenceCount { get; init; }

	/// <summary>
	/// Discovery description for this guild.
	/// </summary>
	[JsonPropertyName("description")]
	public String? Description { get; init; }

	/// <summary>
	/// Stickers for this guild.
	/// </summary>
	[JsonPropertyName("stickers")]
	public required IEnumerable<DiscordSticker> Stickers { get; init; }
}
