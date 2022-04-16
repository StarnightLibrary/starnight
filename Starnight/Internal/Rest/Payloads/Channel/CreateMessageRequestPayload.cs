namespace Starnight.Internal.Rest.Payloads.Channel;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Interactions;
using Starnight.Internal.Entities.Messages;
using Starnight.Internal.Entities.Messages.Embeds;

/// <summary>
/// Represents a request payload to POST /channels/:channel_id/messages.
/// </summary>
public record CreateMessageRequestPayload
{
	/// <summary>
	/// Represents the message content.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("content")]
	public String? Content { get; init; }

	/// <summary>
	/// Defines whether this message is a text-to-speech message.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("tts")]
	public Boolean? TTS { get; init; }

	/// <summary>
	/// Up to 10 embeds to be attached to this message.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("embeds")]
	public IEnumerable<DiscordEmbed>? Embeds { get; init; }

	/// <summary>
	/// Specifies which mentions should be resolved.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("allowed_mentions")]
	public DiscordAllowedMentions? AllowedMentions { get; init; }

	/// <summary>
	/// A reference to the message this message shall reply to.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("message_reference")]
	public DiscordMessageReference? MessageReference { get; init; }

	/// <summary>
	/// A list of components to include with the message.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("components")]
	public IEnumerable<DiscordMessageComponent>? Components { get; init; }

	/// <summary>
	/// Up to 3 snowflake identifiers of stickers to be attached to this message.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("sticker_ids")]
	public IEnumerable<Int64>? StickerIds { get; init; }

	/// <summary>
	/// Files to be attached to this message.
	/// </summary>
	[JsonIgnore]
	public IEnumerable<DiscordAttachmentFile>? Files { get; init; }

	/// <summary>
	/// Attachment metadata for this message.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("attachments")]
	public IEnumerable<DiscordMessageAttachment>? Attachments { get; init; }

	/// <summary>
	/// Message flags, combined as bitfield. Only <see cref="DiscordMessageFlags.SuppressEmbeds"/> can be set.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("flags")]
	public DiscordMessageFlags? Flags { get; init; }
}
