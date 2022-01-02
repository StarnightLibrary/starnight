namespace Starnight.Internal.Entities.Guilds.Invites;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Guilds;

/// <summary>
/// Represents metadata required by an invite about a stage instance.
/// </summary>
public record DiscordInviteStageInstance
{
	/// <summary>
	/// Members speaking in this stage.
	/// </summary>
	[JsonPropertyName("members")]
	public DiscordGuildMember[]? Members { get; init; }

	/// <summary>
	/// The number of users in this stage.
	/// </summary>
	[JsonPropertyName("participant_count")]
	public Int32 ParticipantCount { get; init; }

	/// <summary>
	/// The number of speakers in this stage.
	/// </summary>
	[JsonPropertyName("speaker_count")]
	public Int32 SpeakerCount { get; init; }

	/// <summary>
	/// Topic of this stage, 1 - 120 characters.
	/// </summary>
	[JsonPropertyName("topic")]
	public String Topic { get; init; } = default!;
}
