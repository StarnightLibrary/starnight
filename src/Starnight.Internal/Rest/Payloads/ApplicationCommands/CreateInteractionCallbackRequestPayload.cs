namespace Starnight.Internal.Rest.Payloads.ApplicationCommands;

using System.Collections.Generic;
using System.Text.Json.Serialization;

using Starnight.Entities;
using Starnight.Internal.Entities.Interactions;

/// <summary>
/// Represents a response to a <see cref="DiscordInteraction"/>.
/// </summary>
public sealed record CreateInteractionCallbackRequestPayload
{
	/// <summary>
	/// The selected response type.
	/// </summary>
	[JsonPropertyName("type")]
	public required DiscordInteractionCallbackType Type { get; init; }

	/// <summary>
	/// Optional data for this response.
	/// </summary>
	[JsonPropertyName("data")]
	public Optional<DiscordInteractionCallbackData> Data { get; init; }


	/// <summary>
	/// Files to be uploaded with this message.
	/// </summary>
	[JsonIgnore]
	public IEnumerable<DiscordAttachmentFile>? Files { get; init; }
}
