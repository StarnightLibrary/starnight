namespace Starnight.Internal.Entities.Interaction;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Guild;

/// <summary>
/// Represents a select menu option.
/// </summary>
public class DiscordSelectMenuOption
{
	/// <summary>
	/// User-facing name of the option, max. 100 characters.
	/// </summary>
	[JsonPropertyName("label")]
	public String Label { get; init; } = default!;

	/// <summary>
	/// Dev-facing value of the option, max. 100 characters.
	/// </summary>
	[JsonPropertyName("value")]
	public String Value { get; init; } = default!;

	/// <summary>
	/// Additional description for this option, max. 100 characters
	/// </summary>
	[JsonPropertyName("description")]
	public String? Description { get; init; }

	/// <summary>
	/// Emote to be rendered for this option.
	/// </summary>
	[JsonPropertyName("emoji")]
	public DiscordEmote? Emote { get; init; }

	/// <summary>
	/// Whether this option is selected by default.
	/// </summary>
	[JsonPropertyName("default")]
	public Boolean? Default { get; init; }
}
