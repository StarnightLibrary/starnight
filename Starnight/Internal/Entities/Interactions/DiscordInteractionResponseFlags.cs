namespace Starnight.Internal.Entities.Interactions;

using System;

/// <summary>
/// Bit-field flags for interaction response (there's only one bitfield.)
/// </summary>
[Flags]
public enum DiscordInteractionResponseFlags
{
	/// <summary>
	/// Whether this interaction response message is ephemeral.
	/// </summary>
	IsEmphemeral = 1 << 6
}
