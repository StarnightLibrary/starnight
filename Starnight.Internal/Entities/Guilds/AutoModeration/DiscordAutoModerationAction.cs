namespace Starnight.Internal.Entities.Guilds.AutoModeration;

using System.Text.Json.Serialization;

/// <summary>
/// Represents an action which is executed whenever a rule is triggered.
/// </summary>
public record DiscordAutoModerationAction
{
	/// <summary>
	/// The type of action.
	/// </summary>
	[JsonPropertyName("type")]
	public DiscordAutoModerationActionType Type { get; init; }

	/// <summary>
	/// Additional metadata for execution of this action.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("metadata")]
	public DiscordAutoModerationActionMetadata? Metadata { get; init; }
}
