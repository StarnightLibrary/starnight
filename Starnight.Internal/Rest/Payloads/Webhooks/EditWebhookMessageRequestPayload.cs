namespace Starnight.Internal.Rest.Payloads.Webhooks;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Interactions.Components;
using Starnight.Internal.Entities.Messages;
using Starnight.Internal.Entities.Messages.Embeds;

/// <summary>
/// Represents a payload to PATCH /webhooks/:webhook_id/:webhook_token/messages/:message_id
/// </summary>
public sealed record EditWebhookMessageRequestPayload
{
	/// <summary>
	/// The message contents.
	/// </summary>
	[JsonPropertyName("content")]
	public Optional<String?> Content { get; init; }

	/// <summary>
	/// Up to 10 embeds attached to the message.
	/// </summary>
	[JsonPropertyName("embeds")]
	public Optional<IEnumerable<DiscordEmbed>?> Embeds { get; init; }

	/// <summary>
	/// Allowed mentions object for this message.
	/// </summary>
	[JsonPropertyName("allowed_mentions")]
	public Optional<DiscordAllowedMentions?> AllowedMentions { get; init; }

	/// <summary>
	/// Components attached to this message.
	/// </summary>
	[JsonPropertyName("components")]
	public Optional<IEnumerable<AbstractDiscordMessageComponent>?> Components { get; init; }

	/// <summary>
	/// Files to be sent along with the edit. Note that all used files in this message must be passed here,
	/// even if they were originally present.
	/// </summary>
	[JsonIgnore]
	public IEnumerable<DiscordAttachmentFile>? Files { get; init; }

	/// <summary>
	/// Attachment descriptor objects for this message.
	/// </summary>
	[JsonPropertyName("attachments")]
	public Optional<IEnumerable<DiscordMessageAttachment>?> Attachments { get; init; }
}
