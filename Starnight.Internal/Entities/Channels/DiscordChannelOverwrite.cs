namespace Starnight.Internal.Entities.Channels;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a discord channel permission overwrite.
/// </summary>
public sealed record DiscordChannelOverwrite : DiscordSnowflakeObject
{
	/// <summary>
	/// Overwrite type - either role or member.
	/// </summary>
	[JsonPropertyName("type")]
	public required DiscordChannelOverwriteType Type { get; init; }

	/// <summary>
	/// Granted permissions, by overwrite, see <see cref="DiscordPermissions"/>
	/// </summary>
	[JsonPropertyName("allow")]
	public required Int64 Allow { get; init; }

	/// <summary>
	/// Denied permissions, by overwrite, see <see cref="DiscordPermissions"/>
	/// </summary>
	[JsonPropertyName("deny")]
	public required Int64 Deny { get; init; }
}
