namespace Starnight.Internal.Entities.Interactions.Components;

using System;
using System.Text.Json.Serialization;

using Starnight.Entities;
using Starnight.Internal.Entities.Guilds;

/// <summary>
/// Represents a button component.
/// </summary>
public sealed record DiscordButtonComponent : AbstractInteractiveDiscordMessageComponent
{
	/// <summary>
	/// Holds the style of this button.
	/// </summary>
	[JsonPropertyName("style")]
	public required DiscordButtonStyle Style { get; init; }

	/// <summary>
	/// This button's label, max. 80 characters.
	/// </summary>
	[JsonPropertyName("label")]
	public Optional<String> Label { get; init; }

	/// <summary>
	/// Partial emoji object for this button.
	/// </summary>
	[JsonPropertyName("emoji")]
	public Optional<DiscordEmoji> Emoji { get; init; }

	/// <summary>
	/// An url for link-style buttons.
	/// </summary>
	[JsonPropertyName("url")]
	public Optional<String> Url { get; init; }

	/// <summary>
	/// Whether this button is disabled.
	/// </summary>
	[JsonPropertyName("disabled")]
	public Optional<Boolean> Disabled { get; init; }
}
