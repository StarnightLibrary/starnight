namespace Starnight.Entities;

/// <summary>
/// Represents the different interaction types.
/// </summary>
public enum DiscordInteractionType
{
	Ping = 1,
	ApplicationCommand,
	MessageComponent,
	ApplicationCommandAutocomplete,
	ModalSubmitted
}
