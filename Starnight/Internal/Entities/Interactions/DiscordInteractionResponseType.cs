namespace Starnight.Internal.Entities.Interactions;

/// <summary>
/// Enumerates the different valid callback types.
/// </summary>
public enum DiscordInteractionResponseType
{
	/// <summary>
	/// ACKs a <c>ping</c>
	/// </summary>
	Pong = 1,

	/// <summary>
	/// Respond to the interaction with a message.
	/// </summary>
	ChannelMessageWithSource = 4,

	/// <summary>
	/// ACKs the interaction and edits in a response later; the user sees a loading state.
	/// </summary>
	DeferredChannelMessageWithSource,

	/// <summary>
	/// ACKs the interaction, suppresses the loading state and edits the original message later.
	/// This is only valid on message components.
	/// </summary>
	DeferredUpdateMessage,

	/// <summary>
	/// Edit the message this component was attached to.
	/// </summary>
	UpdateMessage,

	/// <summary>
	/// Respond to an autocomplete interaction with suggested choices.
	/// </summary>
	ApplicationCommandAutocompleteResult
}
