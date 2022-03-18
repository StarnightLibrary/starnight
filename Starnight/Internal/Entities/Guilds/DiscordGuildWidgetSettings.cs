namespace Starnight.Internal.Entities.Guilds;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents settings for the <see cref="DiscordGuildWidget"/> object.
/// </summary>
public record DiscordGuildWidgetSettings
{
	/// <summary>
	/// Whether this widget should be enabled.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("enabled")]
	public Boolean? IsEnabled { get; init; }

	/// <summary>
	/// The snowflake identifier of the channel this widget binds to.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("channel_id")]
	public Int64? ChannelId { get; init; }
}
