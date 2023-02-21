namespace Starnight.Internal.Entities.Interactions;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using Starnight.Entities;
using Starnight.Internal.Entities.Interactions.ApplicationCommands;
using Starnight.Internal.Entities.Interactions.Components;

/// <summary>
/// Represents additional data about a <see cref="DiscordInteraction"/>
/// </summary>
public sealed record DiscordInteractionData : DiscordSnowflakeObject
{
	/// <summary>
	/// The name of the invoked command.
	/// </summary>
	[JsonPropertyName("name")]
	public required String? Name { get; init; }

	/// <summary>
	/// The type of the invoked command.
	/// </summary>
	[JsonPropertyName("type")]
	public required DiscordInteractionType Type { get; init; }

	/// <summary>
	/// All resolved snowflake objects encountered in the interaction.
	/// </summary>
	[JsonPropertyName("resolved")]
	public Optional<DiscordInteractionResolvedData> ResolvedData { get; init; }

	/// <summary>
	/// All parameters and values from the user.
	/// </summary>
	[JsonPropertyName("options")]
	public Optional<IEnumerable<DiscordApplicationCommandDataOption>> Options { get; init; }

	/// <summary>
	/// The developer-defined custom ID for this interaction.
	/// </summary>
	[JsonPropertyName("custom_id")]
	public Optional<String> CustomIdentifier { get; init; }

	/// <summary>
	/// The type of this interaction's associated message component.
	/// </summary>
	[JsonPropertyName("component_type")]
	public Optional<DiscordMessageComponentType> ComponentType { get; init; }

	/// <summary>
	/// The select menu options selected by the user.
	/// </summary>
	[JsonPropertyName("values")]
	public Optional<IEnumerable<DiscordSelectMenuOption>> SelectedOptions { get; init; }

	/// <summary>
	/// Snowflake identifier of the user or message targeted by this right-click interaction.
	/// </summary>
	[JsonPropertyName("target_id")]
	public Optional<Int64> TargetId { get; init; }

	/// <summary>
	/// Snowflake identifier of the guild this command is registered to.
	/// </summary>
	[JsonPropertyName("guild_id")]
	public Optional<Int64> GuildId { get; init; }

	/// <summary>
	/// The values submitted by the user, if this is a modal submission interaction.
	/// </summary>
	[JsonPropertyName("components")]
	public Optional<AbstractDiscordMessageComponent> Components { get; init; }
}
