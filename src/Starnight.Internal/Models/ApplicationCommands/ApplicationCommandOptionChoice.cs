namespace Starnight.Internal.Models.ApplicationCommands;

using System;
using System.Collections.Generic;

/// <summary>
/// Represents a potential chocie for a <seealso cref="ApplicationCommandOption"/>.
/// If any choices are specified for an option, they are the only valid values to choose.
/// </summary>
public sealed record ApplicationCommandOptionChoice
{
	/// <summary>
	/// The name of this choice, 1-32 characters.
	/// </summary>
	public required String Name { get; init; }

	/// <summary>
	/// A dictionary of locale-mapped localized names for <seealso cref="Name"/>.
	/// </summary>
	public Optional<IReadOnlyDictionary<String, String>?> NameLocalizations { get; init; }

	/// <summary>
	/// The value of this choice. This must be either an integer, a floating-point number or a
	/// string depending on its parent option. If this is a string, it can be up to 100 characters
	/// in length.
	/// </summary>
	public required Object Value { get; init; }
}
