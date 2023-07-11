namespace Starnight.Internal.Models.MessageComponents;

using System;

using Starnight.Internal.Models.Emojis;

/// <summary>
/// Represents a single option in a <seealso cref="TextSelectComponent"/>.
/// </summary>
public sealed record TextSelectOption
{
	/// <summary>
	/// The user-facing name of this option.
	/// </summary>
	public required String Label { get; init; }

	/// <summary>
	/// The developer-defined value of this option.
	/// </summary>
	public required String Value { get; init; }

	/// <summary>
	/// A potential additional description of this option.
	/// </summary>
	public Optional<String> Description { get; init; }

	/// <summary>
	/// An emoji rendered along with this option.
	/// </summary>
	public Optional<PartialEmoji> Emoji { get; init; }

	/// <summary>
	/// Indicates whether this option will be selected by default.
	/// </summary>
	public Optional<Boolean> Default { get; init; }
}
