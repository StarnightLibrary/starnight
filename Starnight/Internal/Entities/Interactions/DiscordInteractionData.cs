namespace Starnight.Internal.Entities.Interactions;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Interactions.ApplicationCommands;
using Starnight.Internal.Entities.Interactions.Components;

/// <summary>
/// Represents additional data about a <see cref="DiscordInteraction"/>
/// </summary>
public record DiscordInteractionData : DiscordSnowflakeObject
{
	/// <summary>
	/// The name of the invoked command.
	/// </summary>
	[JsonPropertyName("name")]
	public String? Name { get; init; } = default!;

	/// <summary>
	/// The type of the invoked command.
	/// </summary>
	[JsonPropertyName("type")]
	public DiscordInteractionType? Type { get; init; }

	/// <summary>
	/// All resolved snowflake objects encountered in the interaction.
	/// </summary>
	[JsonPropertyName("resolved")]
	public DiscordInteractionResolvedData? ResolvedData { get; init; }

	/// <summary>
	/// All parameters and values from the user.
	/// </summary>
	[JsonPropertyName("options")]
	public IEnumerable<DiscordApplicationCommandDataOption>? Options { get; init; }

	/// <summary>
	/// The developer-defined custom ID for this interaction.
	/// </summary>
	[JsonPropertyName("custom_id")]
	public String? CustomIdentifier { get; init; }

	/// <summary>
	/// The type of this interaction's associated message component.
	/// </summary>
	[JsonPropertyName("component_type")]
	public DiscordMessageComponentType? ComponentType { get; init; }

	/// <summary>
	/// The select menu options selected by the user.
	/// </summary>
	[JsonPropertyName("values")]
	public IEnumerable<DiscordSelectMenuOption>? SelectedOptions { get; init; }

	/// <summary>
	/// Snowflake identifier of the user or message targeted by this right-click interaction.
	/// </summary>
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("target_id")]
	public Int64? TargetId { get; init; }
}
