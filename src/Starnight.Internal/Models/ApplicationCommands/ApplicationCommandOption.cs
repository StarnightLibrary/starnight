namespace Starnight.Internal.Models.ApplicationCommands;

using System;
using System.Collections.Generic;

using Starnight.Entities;

/// <summary>
/// Represents a single option, subcommand or subcommand group for an <see cref="ApplicationCommand"/>.
/// </summary>
public sealed record ApplicationCommandOption
{
	/// <summary>
	/// The type of this option.
	/// </summary>
	public required DiscordApplicationCommandOptionType Type { get; init; }

	/// <summary>
	/// The name of this option, 1-32 characters.
	/// </summary>
	public required String Name { get; init; }

	/// <summary>
	/// A dictionary of locale-mapped localized names for <seealso cref="Name"/>.
	/// </summary>
	public Optional<IReadOnlyDictionary<String, String>?> NameLocalizations { get; init; }

	/// <summary>
	/// The description of this option, 1-100 characters.
	/// </summary>
	public required String Description { get; init; }

	/// <summary>
	/// A dictionary of locale-mapped localized descriptions for <seealso cref="Description"/>.
	/// </summary>
	public Optional<IReadOnlyDictionary<String, String>?> DescriptionLocalizations { get; init; }

	/// <summary>
	/// Indicates whether this option is required and has to be passed.
	/// </summary>
	public Optional<Boolean> Required { get; init; }

	/// <summary>
	/// Up to 25 choices for the end user to pick from; only applicable if <seealso cref="Type"/> is
	/// either <seealso cref="DiscordApplicationCommandOptionType.String"/>,
	/// <seealso cref="DiscordApplicationCommandOptionType.Integer"/> or
	/// <seealso cref="DiscordApplicationCommandOptionType.Decimal"/>.
	/// </summary>
	public Optional<IReadOnlyList<ApplicationCommandOptionChoice>> Choices { get; init; }

	/// <summary>
	/// If this option is of <seealso cref="DiscordApplicationCommandOptionType.SubCommand"/> or
	/// <seealso cref="DiscordApplicationCommandOptionType.SubCommandGroup"/>, these nested options
	/// will be the parameters to the subcommand (group).
	/// </summary>
	public Optional<IReadOnlyList<ApplicationCommandOption>> Options { get; init; }

	/// <summary>
	/// If this option is of <seealso cref="DiscordApplicationCommandOptionType.Channel"/>, it will
	/// be restricted to these types.
	/// </summary>
	public Optional<IReadOnlyList<DiscordChannelType>> ChannelTypes { get; init; }

	/// <summary>
	/// If this option is of <seealso cref="DiscordApplicationCommandOptionType.Integer"/> or
	/// <seealso cref="DiscordApplicationCommandOptionType.Decimal"/>, the minimum value permitted.
	/// </summary>
	public Optional<Double> MinValue { get; init; }

	/// <summary>
	/// If this option is of <seealso cref="DiscordApplicationCommandOptionType.Integer"/> or
	/// <seealso cref="DiscordApplicationCommandOptionType.Decimal"/>, the maximum value permitted.
	/// </summary>
	public Optional<Double> MaxValue { get; init; }

	/// <summary>
	/// If this option is of <seealso cref="DiscordApplicationCommandOptionType.String"/>, the
	/// minimum length permitted, between 0 and 6000.
	/// </summary>
	public Optional<Int32> MinLength { get; init; }

	/// <summary>
	/// If this option is of <seealso cref="DiscordApplicationCommandOptionType.String"/>, the
	/// maximum length permitted, between 1 and 6000.
	/// </summary>
	public Optional<Int32> MaxLength { get; init; }

	/// <summary>
	/// If this option is of <seealso cref="DiscordApplicationCommandOptionType.String"/>,
	/// <seealso cref="DiscordApplicationCommandOptionType.Integer"/> or
	/// <seealso cref="DiscordApplicationCommandOptionType.Decimal"/>, indicates whether autocomplete
	/// interactions are enabled for this option.
	/// </summary>
	/// <remarks>
	/// This may not be <see langword="true"/> if <seealso cref="Choices"/> is populated.
	/// </remarks>
	public Optional<Boolean> Autocomplete { get; init; }
}
