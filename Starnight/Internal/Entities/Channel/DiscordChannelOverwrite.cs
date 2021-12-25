namespace Starnight.Internal.Entities.Channel;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a discord channel permission overwrite.
/// </summary>
public class DiscordChannelOverwrite : DiscordSnowflakeObject
{
	/// <summary>
	/// Overwrite type - either role or member, see <see cref="DiscordChannelOverwriteType"/>
	/// </summary>
	[JsonPropertyName("type")]
	public Int32 Type { get; init; }

	/// <summary>
	/// Granted permissions, by overwrite, see <see cref="DiscordPermission"/>
	/// </summary>
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("allow")]
	public Int64 Allow { get; init; }

	/// <summary>
	/// Denied permissions, by overwrite, see <see cref="DiscordPermission"/>
	/// </summary>
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("allow")]
	public Int64 Deny { get; init; }
}
