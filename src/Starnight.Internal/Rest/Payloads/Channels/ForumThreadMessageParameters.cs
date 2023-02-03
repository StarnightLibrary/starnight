namespace Starnight.Internal.Rest.Payloads.Channels;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Interactions.Components;
using Starnight.Internal.Entities.Messages;
using Starnight.Internal.Entities.Messages.Embeds;

/// <summary>
/// Represents a parameter object to <seealso cref="StartThreadInForumChannelRequestPayload"/>.
/// </summary>
public sealed record ForumThreadMessageParameters
{
	/// <summary>
	/// Represents the message content.
	/// </summary>
	[JsonPropertyName("content")]
	public Optional<String> Content { get; init; }

	/// <summary>
	/// Defines whether this message is a text-to-speech message.
	/// </summary>
	[JsonPropertyName("tts")]
	public Optional<Boolean> TTS { get; init; }

	/// <summary>
	/// Up to 10 embeds to be attached to this message.
	/// </summary>
	[JsonPropertyName("embeds")]
	public Optional<IEnumerable<DiscordEmbed>> Embeds { get; init; }

	/// <summary>
	/// Specifies which mentions should be resolved.
	/// </summary>
	[JsonPropertyName("allowed_mentions")]
	public Optional<DiscordAllowedMentions> AllowedMentions { get; init; }

	/// <summary>
	/// A list of components to include with the message.
	/// </summary>
	[JsonPropertyName("components")]
	public Optional<IEnumerable<AbstractDiscordMessageComponent>> Components { get; init; }

	/// <summary>
	/// Up to 3 snowflake identifiers of stickers to be attached to this message.
	/// </summary>
	[JsonPropertyName("sticker_ids")]
	public Optional<IEnumerable<Int64>> StickerIds { get; init; }

	/// <summary>
	/// Attachment metadata for this message.
	/// </summary>
	[JsonPropertyName("attachments")]
	public Optional<IEnumerable<DiscordMessageAttachment>> Attachments { get; init; }

	/// <summary>
	/// Message flags, combined as bitfield. Only <see cref="DiscordMessageFlags.SuppressEmbeds"/> can be set.
	/// </summary>
	[JsonPropertyName("flags")]
	public Optional<DiscordMessageFlags> Flags { get; init; }
}
