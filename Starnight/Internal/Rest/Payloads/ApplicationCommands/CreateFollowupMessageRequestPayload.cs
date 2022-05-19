namespace Starnight.Internal.Rest.Payloads.ApplicationCommands;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Interactions;
using Starnight.Internal.Entities.Messages;
using Starnight.Internal.Entities.Messages.Embeds;

/// <summary>
/// Represents a payload to POST /webhooks/:application_id/:interaction_token
/// </summary>
public record CreateFollowupMessageRequestPayload
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
	/// A list of components to include with the message.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("components")]
	public IEnumerable<DiscordMessageComponent>? Components { get; init; }

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
	/// Message flags, combined as bitfield. Only <see cref="DiscordMessageFlags.SuppressEmbeds"/>
	/// and <see cref="DiscordMessageFlags.Ephemeral"/>can be set.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("flags")]
	public DiscordMessageFlags? Flags { get; init; }
}
