namespace Starnight.Internal.Models.MessageComponents;

using System;

using Starnight.Entities;
using Starnight.Internal.Models.Emojis;

/// <summary>
/// Represents a message button. An <seealso cref="ActionRowComponent"/> can hold up to five buttons.
/// </summary>
public sealed record ButtonComponent : AbstractInteractiveComponent
{
	public ButtonComponent() => this.Type = DiscordMessageComponentType.Button;

	/// <summary>
	/// Specifies the visual style of this button.
	/// </summary>
	public required DiscordButtonStyle Style { get; init; }

	/// <summary>
	/// The text that will appear on the button.
	/// </summary>
	public Optional<String> Label { get; init; }

	/// <summary>
	/// The emoji which will be displayed on the button.
	/// </summary>
	public Optional<PartialEmoji> Emoji { get; init; }

	/// <summary>
	/// The developer-defined ID for this button, which will be sent back if this button is
	/// interacted with.
	/// </summary>
	public Optional<String> CustomId { get; init; }

	/// <summary>
	/// If this button is of <seealso cref="DiscordButtonStyle.Link"/>, the URL it will point to.
	/// </summary>
	public Optional<String> Url { get; init; }

	/// <summary>
	/// Indicates whether this button is disabled.
	/// </summary>
	public Optional<Boolean> Disabled { get; init; }
}
