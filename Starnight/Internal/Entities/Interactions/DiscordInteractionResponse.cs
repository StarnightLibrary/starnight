namespace Starnight.Internal.Entities.Interactions;

using System.Text.Json.Serialization;

/// <summary>
/// Represents a response to a <see cref="DiscordInteraction"/>.
/// </summary>
public record DiscordInteractionResponse
{
	/// <summary>
	/// The selected response type.
	/// </summary>
	[JsonPropertyName("type")]
	public DiscordInteractionResponseType Type { get; init; }

	/// <summary>
	/// Optional data for this response.
	/// </summary>
	[JsonPropertyName("data")]
	public DiscordInteractionResolvedData? Data { get; init; }
}
