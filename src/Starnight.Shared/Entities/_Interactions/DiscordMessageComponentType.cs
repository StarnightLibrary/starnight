namespace Starnight.Entities;

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
	/// A dropdown menu for picking from predefined string choices.
	/// </summary>
	TextSelect,

	/// <summary>
	/// A text field in a modal.
	/// </summary>
	TextInput,

	/// <summary>
	/// A dropdown menu for picking from user choices.
	/// </summary>
	UserSelect,

	/// <summary>
	/// A dropdown menu for picking from role choices.
	/// </summary>
	RoleSelect,

	/// <summary>
	/// A dropdown menu for picking from user or role choices.
	/// </summary>
	MentionableSelect,

	/// <summary>
	/// A dropdown menu for picking from channel choices.
	/// </summary>
	ChannelSelect
}
