namespace Starnight.Internal.Entities.Interactions.ApplicationCommand;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a discord application option choice.
/// </summary>
public record DiscordApplicationCommandOptionChoice
{
	/// <summary>
	/// The display name of this option.
	/// </summary>
	[JsonPropertyName("name")]
	public String Name { get; init; } = default!;

	/// <summary>
	/// Localized name of this application command option choice.
	/// </summary>
	[JsonPropertyName("name_localized")]
	public String? NameLocalized { get; init; }

	/// <summary>
	/// Localization dictionary for the <see cref="Name"/> field.
	/// </summary>
	/// <remarks>
	/// Also refer to the documentation of <seealso cref="DiscordLocale"/>.
	/// </remarks>
	[JsonPropertyName("name_localizations")]
	public IDictionary<String, String>? NameLocalizations { get; init; }

	/// <summary>
	/// The value of this object, either a string, integer or floating-point number.
	/// </summary>
	[JsonPropertyName("value")]
	public Object Value { get; init; } = default!;
}
