namespace Starnight.Internal.Rest.Payloads.ApplicationCommands;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using Starnight.Entities;
using Starnight.Internal.Entities.Interactions.ApplicationCommands;
using Starnight.Internal.Entities.Interactions.Components;
using Starnight.Internal.Entities.Messages;
using Starnight.Internal.Entities.Messages.Embeds;

/// <summary>
/// Represents the response body of a <see cref="CreateInteractionCallbackRequestPayload"/>
/// </summary>
public sealed record DiscordInteractionCallbackData
{
	/// <summary>
	/// Whether this response is a TTS message.
	/// </summary>
	[JsonPropertyName("tts")]
	public Optional<Boolean> TTS { get; init; }

	/// <summary>
	/// Response message content.
	/// </summary>
	[JsonPropertyName("content")]
	public Optional<String> Content { get; init; }

	/// <summary>
	/// Up to 10 embeds to be attached to this message.
	/// </summary>
	[JsonPropertyName("embeds")]
	public Optional<IEnumerable<DiscordEmbed>> Embeds { get; init; }

	/// <summary>
	/// Controls what mentions should be parsed as such.
	/// </summary>
	[JsonPropertyName("allowed_mentions")]
	public Optional<DiscordAllowedMentions> AllowedMentions { get; init; }

	/// <summary>
	/// Message flags for this interaction response.
	/// </summary>
	[JsonPropertyName("flags")]
	public Optional<DiscordMessageFlags> Flags { get; init; }

	/// <summary>
	/// Message components to be attached to this interaction response message.
	/// </summary>
	[JsonPropertyName("components")]
	public Optional<IEnumerable<AbstractDiscordMessageComponent>> Components { get; init; }

	/// <summary>
	/// Attachments to be... attached... to this interaction response message.
	/// </summary>
	[JsonPropertyName("attachments")]
	public Optional<IEnumerable<DiscordMessageAttachment>> Attachments { get; init; }

	/// <summary>
	/// Up to 25 autocomplete choices. Mutually exclusive with all other properties.
	/// </summary>
	[JsonPropertyName("choices")]
	public Optional<IEnumerable<DiscordApplicationCommandOptionChoice>> Choices { get; init; }
}
