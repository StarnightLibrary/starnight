namespace Starnight.Internal.Rest.Payloads.ApplicationCommands;

using Starnight.Internal.Entities.Interactions;
using Starnight.Internal.Entities.Messages.Embeds;
using Starnight.Internal.Entities.Messages;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System;

public record EditOriginalResponseRequestPayload
{
	/// <summary>
	/// Response message content.
	/// </summary>
	[JsonPropertyName("content")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	public String? Content { get; init; }

	/// <summary>
	/// Up to 10 embeds to be attached to this message.
	/// </summary>
	[JsonPropertyName("embeds")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	public IEnumerable<DiscordEmbed>? Embeds { get; init; }

	/// <summary>
	/// Controls what mentions should be parsed as such.
	/// </summary>
	[JsonPropertyName("allowed_mentions")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	public DiscordAllowedMentions? AllowedMentions { get; init; }

	/// <summary>
	/// Message flags for this interaction response.
	/// </summary>
	[JsonPropertyName("flags")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	public DiscordMessageFlags? Flags { get; init; }

	/// <summary>
	/// Message components to be attached to this interaction response message.
	/// </summary>
	[JsonPropertyName("components")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	public IEnumerable<DiscordMessageComponent>? Components { get; init; }

	/// <summary>
	/// Attachments to be... attached... to this interaction response message.
	/// </summary>
	[JsonPropertyName("attachments")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	public IEnumerable<DiscordMessageAttachment>? Attachments { get; init; }

	/// <summary>
	/// Files to be uploaded with this message.
	/// </summary>
	[JsonIgnore]
	public IEnumerable<DiscordAttachmentFile>? Files { get; init; }
}
