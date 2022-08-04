namespace Starnight.Internal.Rest.Payloads.ApplicationCommands;

using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Interactions;

/// <summary>
/// Represents a response to a <see cref="DiscordInteraction"/>.
/// </summary>
public record CreateInteractionCallbackRequestPayload
{
	/// <summary>
	/// The selected response type.
	/// </summary>
	[JsonPropertyName("type")]
	public DiscordInteractionCallbackType Type { get; init; }

	/// <summary>
	/// Optional data for this response.
	/// </summary>
	[JsonPropertyName("data")]
	public Optional<DiscordInteractionCallbackData> Data { get; init; }
}
