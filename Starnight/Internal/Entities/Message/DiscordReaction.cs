namespace Starnight.Internal.Entities.Message;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Guild;

/// <summary>
/// Represents a discord message reaction.
/// </summary>
public class DiscordReaction
{
	/// <summary>
	/// Gets however often this emote has been used to react.
	/// </summary>
	[JsonPropertyName("count")]
	public Int32 Count { get; init; }

	/// <summary>
	/// Whether the current user reacted using this emote.
	/// </summary>
	[JsonPropertyName("me")]
	public Boolean CurrentUserReacted { get; init; }

	/// <summary>
	/// More emote information
	/// </summary>
	[JsonPropertyName("emoji")]
	public DiscordEmote Emote { get; init; } = default!;
}
