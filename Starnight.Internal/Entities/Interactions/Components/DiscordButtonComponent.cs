namespace Starnight.Internal.Entities.Interactions.Components;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Guilds;

/// <summary>
/// Represents a button component.
/// </summary>
public record DiscordButtonComponent : AbstractInteractiveDiscordMessageComponent
{
	/// <summary>
	/// Holds the style of this button.
	/// </summary>
	[JsonPropertyName("style")]
	public DiscordButtonStyle Style { get; init; }

	/// <summary>
	/// This button's label, max. 80 characters.
	/// </summary>
	[JsonPropertyName("label")]
	public String? Label { get; init; }

	/// <summary>
	/// Partial emoji object for this button.
	/// </summary>
	[JsonPropertyName("emoji")]
	public DiscordEmoji? Emoji { get; init; }

	/// <summary>
	/// An url for link-style buttons.
	/// </summary>
	[JsonPropertyName("url")]
	public String? Url { get; init; }

	/// <summary>
	/// Whether this button is disabled.
	/// </summary>
	[JsonPropertyName("disabled")]
	public Boolean? Disabled { get; init; }
}
