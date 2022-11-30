namespace Starnight.Internal.Entities.Channels;

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
	/// Granted permissions, by overwrite.
	/// </summary>
	[JsonPropertyName("allow")]
	public required DiscordPermissions Allow { get; init; }

	/// <summary>
	/// Denied permissions, by overwrite.
	/// </summary>
	[JsonPropertyName("deny")]
	public required DiscordPermissions Deny { get; init; }
}
