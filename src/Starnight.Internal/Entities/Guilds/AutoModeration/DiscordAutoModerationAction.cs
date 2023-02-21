namespace Starnight.Internal.Entities.Guilds.AutoModeration;

using System.Text.Json.Serialization;

using Starnight.Entities;

/// <summary>
/// Represents an action which is executed whenever a rule is triggered.
/// </summary>
public sealed record DiscordAutoModerationAction
{
	/// <summary>
	/// The type of action.
	/// </summary>
	[JsonPropertyName("type")]
	public required DiscordAutoModerationActionType Type { get; init; }

	/// <summary>
	/// Additional metadata for execution of this action.
	/// </summary>
	[JsonPropertyName("metadata")]
	public Optional<DiscordAutoModerationActionMetadata> Metadata { get; init; }
}
