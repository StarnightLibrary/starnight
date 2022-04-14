namespace Starnight.Internal.Rest.Payloads.Channel;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Interactions;
using Starnight.Internal.Entities.Messages;
using Starnight.Internal.Entities.Messages.Embeds;

/// <summary>
/// Represents a payload to PATCH /channels/:channel_id/messages/:message_id
/// </summary>
public record EditMessageRequestPayload
{
	/// <summary>
	/// New string content of the message, up to 2000 characters.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("content")]
	public String? Content { get; init; }

	/// <summary>
	/// Up to 10 embeds for this message.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("embeds")]
	public DiscordEmbed[]? Embeds { get; init; }

	/// <summary>
	/// New flags for this message. Only <see cref="DiscordMessageFlags.SuppressEmbeds"/> can currently be set or unset.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("flags")]
	public DiscordMessageFlags? Flags { get; init; }

	/// <summary>
	/// Authoritative allowed mentions object for this message. Passing <see langword="null"/> <b>resets</b> the object to default.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("allowed_mentions")]
	public DiscordAllowedMentions? AllowedMentions { get; init; }

	/// <summary>
	/// New components for this message.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("components")]
	public DiscordMessageComponent[]? Components { get; init; }

	/// <summary>
	/// Attached files to this message. This must include old attachments to be retained and new attachments, if passed.
	/// </summary>
	[JsonIgnore]
	public DiscordAttachmentFile[]? Files { get; init; }

	/// <summary>
	/// Attachments to this message.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("content")]
	public DiscordMessageAttachment[]? Attachments { get; init; }
}
