namespace Starnight.Internal.Rest.Payloads.Webhooks;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Interactions.Components;
using Starnight.Internal.Entities.Messages;
using Starnight.Internal.Entities.Messages.Embeds;

/// <summary>
/// Represents a payload to POST /webhooks/:webhook_id/:webhook_token
/// </summary>
public sealed record ExecuteWebhookRequestPayload
{
	/// <summary>
	/// Message contents.
	/// </summary>
	[JsonPropertyName("content")]
	public Optional<String> Content { get; init; }

	/// <summary>
	/// Overrides the default username of the webhook.
	/// </summary>
	[JsonPropertyName("username")]
	public Optional<String> Username { get; init; }

	/// <summary>
	/// Overrides the default avatar of the webhook.
	/// </summary>
	[JsonPropertyName("avatar_url")]
	public Optional<String> AvatarUrl { get; init; }

	/// <summary>
	/// True if this is a TTS message.
	/// </summary>
	[JsonPropertyName("tts")]
	public Optional<Boolean> TTS { get; init; }

	/// <summary>
	/// Up to 10 embeds
	/// </summary>
	[JsonPropertyName("embeds")]
	public Optional<IEnumerable<DiscordEmbed>> Embeds { get; init; }

	/// <summary>
	/// Allowed mentions object for this message.
	/// </summary>
	[JsonPropertyName("allowed_mentions")]
	public Optional<DiscordAllowedMentions> AllowedMentions { get; init; }

	/// <summary>
	/// Components to include with this message.
	/// </summary>
	[JsonPropertyName("components")]
	public Optional<IEnumerable<AbstractDiscordMessageComponent>> Components { get; init; }

	/// <summary>
	/// Attachment files to include with this message.
	/// </summary>
	[JsonIgnore]
	public IEnumerable<DiscordAttachmentFile>? Files { get; init; }

	/// <summary>
	/// Attachment descriptor objects with filename and description.
	/// </summary>
	[JsonPropertyName("attachments")]
	public Optional<IEnumerable<DiscordMessageAttachment>> Attachments { get; init; }

	/// <summary>
	/// Message flags for this message. Only <see cref="DiscordMessageFlags.SuppressEmbeds"/> can be set.
	/// </summary>
	[JsonPropertyName("flags")]
	public Optional<DiscordMessageFlags> Flags { get; init; }

	/// <summary>
	/// The name of the thread to create, if the webhook channel is a forum channel.
	/// </summary>
	[JsonPropertyName("thread_name")]
	public Optional<String> ThreadName { get; init; }
}
