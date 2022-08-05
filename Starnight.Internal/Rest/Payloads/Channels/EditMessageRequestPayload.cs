namespace Starnight.Internal.Rest.Payloads.Channels;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Interactions.Components;
using Starnight.Internal.Entities.Messages;
using Starnight.Internal.Entities.Messages.Embeds;

/// <summary>
/// Represents a payload to PATCH /channels/:channel_id/messages/:message_id
/// </summary>
public sealed record EditMessageRequestPayload
{
	/// <summary>
	/// New string content of the message, up to 2000 characters.
	/// </summary>
	[JsonPropertyName("content")]
	public Optional<String?> Content { get; init; }

	/// <summary>
	/// Up to 10 embeds for this message.
	/// </summary>
	[JsonPropertyName("embeds")]
	public Optional<IEnumerable<DiscordEmbed>?> Embeds { get; init; }

	/// <summary>
	/// New flags for this message. Only <see cref="DiscordMessageFlags.SuppressEmbeds"/> can currently be set or unset.
	/// </summary>
	[JsonPropertyName("flags")]
	public Optional<DiscordMessageFlags?> Flags { get; init; }

	/// <summary>
	/// Authoritative allowed mentions object for this message. Passing <see langword="null"/> <b>resets</b> the object to default.
	/// </summary>
	[JsonPropertyName("allowed_mentions")]
	public Optional<DiscordAllowedMentions?> AllowedMentions { get; init; }

	/// <summary>
	/// New components for this message.
	/// </summary>
	[JsonPropertyName("components")]
	public Optional<IEnumerable<AbstractDiscordMessageComponent>?> Components { get; init; }

	/// <summary>
	/// Attached files to this message. This must include old attachments to be retained and new attachments, if passed.
	/// </summary>
	[JsonIgnore]
	public IEnumerable<DiscordAttachmentFile>? Files { get; init; }

	/// <summary>
	/// Attachments to this message.
	/// </summary>
	[JsonPropertyName("content")]
	public Optional<IEnumerable<DiscordMessageAttachment>?> Attachments { get; init; }
}
