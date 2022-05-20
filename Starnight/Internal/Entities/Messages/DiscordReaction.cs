namespace Starnight.Internal.Entities.Messages;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Guilds;

/// <summary>
/// Represents a discord message reaction.
/// </summary>
public record DiscordReaction
{
	/// <summary>
	/// Gets however often this emoji has been used to react.
	/// </summary>
	[JsonPropertyName("count")]
	public Int32 Count { get; init; }

	/// <summary>
	/// Whether the current user reacted using this emoji.
	/// </summary>
	[JsonPropertyName("me")]
	public Boolean CurrentUserReacted { get; init; }

	/// <summary>
	/// More emoji information
	/// </summary>
	[JsonPropertyName("emoji")]
	public DiscordEmoji Emoji { get; init; } = default!;
}
