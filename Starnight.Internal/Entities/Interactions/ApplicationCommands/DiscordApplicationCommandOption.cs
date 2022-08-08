namespace Starnight.Internal.Entities.Interactions.ApplicationCommands;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Channels;
using Starnight.Internal.Entities.Interactions;

/// <summary>
/// Represents a slash command parameter.
/// </summary>
public sealed record DiscordApplicationCommandOption
{
	/// <summary>
	/// The parameter type.
	/// </summary>
	[JsonPropertyName("type")]
	public required DiscordApplicationCommandOptionType Type { get; init; }

	/// <summary>
	/// The parameter name.
	/// </summary>
	[JsonPropertyName("name")]
	public required String Name { get; init; }

	/// <summary>
	/// Localized name of this application command option.
	/// </summary>
	[JsonPropertyName("name_localized")]
	public Optional<String> NameLocalized { get; init; }

	/// <summary>
	/// Localization dictionary for the <see cref="Name"/> field.
	/// </summary>
	/// <remarks>
	/// Also refer to the documentation of <seealso cref="DiscordLocale"/>.
	/// </remarks>
	[JsonPropertyName("name_localizations")]
	public Optional<IDictionary<String, String>?> NameLocalizations { get; init; }

	/// <summary>
	/// The parameter description.
	/// </summary>
	[JsonPropertyName("description")]
	public required String Description { get; init; }

	/// <summary>
	/// Localized description of this application command option.
	/// </summary>
	[JsonPropertyName("description_localized")]
	public Optional<String> DescriptionLocalized { get; init; }

	/// <summary>
	/// Localization dictionary for the <see cref="Description"/> field.
	/// </summary>
	/// <remarks>
	/// Also refer to the documentation of <seealso cref="DiscordLocale"/>.
	/// </remarks>
	[JsonPropertyName("description_localizations")]
	public Optional<IDictionary<String, String>?> DescriptionLocalizations { get; init; }

	/// <summary>
	/// Whether this parameter must be passed to the slash command.
	/// </summary>
	[JsonPropertyName("required")]
	public Optional<Boolean> Required { get; init; }

	/// <summary>
	/// All valid choices for this parameter. Not listed choices are not valid inputs.
	/// </summary>
	[JsonPropertyName("choices")]
	public Optional<IEnumerable<DiscordApplicationCommandOptionChoice>> Choices { get; init; }

	/// <summary>
	/// If this is a subcommand/subcommand group; these options are valid for the next level.
	/// </summary>
	[JsonPropertyName("options")]
	public Optional<IEnumerable<DiscordApplicationCommandOption>> Suboptions { get; init; }

	/// <summary>
	/// If the option is a channel, the channels suggested will be restricted to these types.
	/// </summary>
	[JsonPropertyName("channel_types")]
	public Optional<IEnumerable<DiscordChannelType>> ChannelTypes { get; init; }

	/// <summary>
	/// The minimum value for this parameter. 
	/// </summary>
	/// <remarks>
	/// This must be Int32 for integer options and Double for decimal options.
	/// </remarks>
	[JsonPropertyName("min_value")]
	public Optional<Object> MinValue { get; init; }

	/// <summary>
	/// The maximum value for this parameter.
	/// </summary>
	/// <remarks>
	/// This must be Int32 for integer options and Double for decimal options.
	/// </remarks>
	[JsonPropertyName("max_value")]
	public Optional<Object> MaxValue { get; init; }

	/// <summary>
	/// For string options, the minimum allowed length (0-6000).
	/// </summary>
	[JsonPropertyName("min_length")]
	public Optional<Int32> MinLength { get; init; }

	/// <summary>
	/// For string options, the maximum allowed length (1-6000).
	/// </summary>
	[JsonPropertyName("max_length")]
	public Optional<Int32> MaxLength { get; init; }

	/// <summary>
	/// Whether this application command option should use autocomplete.
	/// </summary>
	[JsonPropertyName("autocomplete")]
	public Optional<Boolean> Autocomplete { get; init; }
}
