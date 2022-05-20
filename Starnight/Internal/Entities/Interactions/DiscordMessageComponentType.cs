namespace Starnight.Internal.Entities.Interactions;

/// <summary>
/// Represents the different message component types.
/// </summary>
public enum DiscordMessageComponentType
{
	/// <summary>
	/// A container for more components.
	/// </summary>
	ActionRow = 1,

	/// <summary>
	/// A button.
	/// </summary>
	Button,

	/// <summary>
	/// A dropdown menu for picking from predefined choices.
	/// </summary>
	SelectMenu,

	/// <summary>
	/// A text field in a modal.
	/// </summary>
	TextInput
}
