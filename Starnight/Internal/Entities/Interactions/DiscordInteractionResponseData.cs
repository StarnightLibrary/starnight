namespace Starnight.Internal.Entities.Interactions;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Interactions.ApplicationCommands;
using Starnight.Internal.Entities.Messages;
using Starnight.Internal.Entities.Messages.Embeds;

/// <summary>
/// Represents the response body of a <see cref="DiscordInteractionResponse"/>
/// </summary>
public record DiscordInteractionResponseData
{
	/// <summary>
	/// Whether this response is a TTS message.
	/// </summary>
	[JsonPropertyName("tts")]
	public Boolean? TTS { get; init; }

	/// <summary>
	/// Response message content.
	/// </summary>
	[JsonPropertyName("content")]
	public String? Content { get; init; }

	/// <summary>
	/// Up to 10 embeds to be attached to this message.
	/// </summary>
	[JsonPropertyName("embeds")]
	public IEnumerable<DiscordEmbed>? Embeds { get; init; }

	/// <summary>
	/// Controls what mentions should be parsed as such.
	/// </summary>
	[JsonPropertyName("allowed_mentions")]
	public DiscordAllowedMentions? AllowedMentions { get; init; }

	/// <summary>
	/// Message flags for this interaction response.
	/// </summary>
	[JsonPropertyName("flags")]
	public DiscordMessageFlags? Flags { get; init; }

	/// <summary>
	/// Message components to be attached to this interaction response message.
	/// </summary>
	[JsonPropertyName("components")]
	public IEnumerable<DiscordMessageComponent>? Components { get; init; }

	/// <summary>
	/// Attachments to be... attached... to this interaction response message.
	/// </summary>
	[JsonPropertyName("attachments")]
	public IEnumerable<DiscordMessageAttachment>? Attachments { get; init; }

	/// <summary>
	/// Up to 25 autocomplete choices. Mutually exclusive with all other properties.
	/// </summary>
	[JsonPropertyName("choices")]
	public IEnumerable<DiscordApplicationCommandOptionChoice>? Choices { get; init; }
}
