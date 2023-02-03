namespace Starnight.Internal.Entities.Guilds;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents settings for the <see cref="DiscordGuildWidget"/> object.
/// </summary>
public sealed record DiscordGuildWidgetSettings
{
	/// <summary>
	/// Whether this widget should be enabled.
	/// </summary>
	[JsonPropertyName("enabled")]
	public required Boolean IsEnabled { get; init; }

	/// <summary>
	/// The snowflake identifier of the channel this widget binds to.
	/// </summary>
	[JsonPropertyName("channel_id")]
	public Int64? ChannelId { get; init; }
}
