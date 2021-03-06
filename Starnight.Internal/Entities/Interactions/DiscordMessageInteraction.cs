namespace Starnight.Internal.Entities.Interactions;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Guilds;
using Starnight.Internal.Entities.Users;

/// <summary>
/// Represents a discord message interaction.
/// </summary>
public record DiscordMessageInteraction : DiscordSnowflakeObject
{
	/// <summary>
	/// Interaction type associated with this object.
	/// </summary>
	[JsonPropertyName("type")]
	public DiscordInteractionType InteractionType { get; init; }

	/// <summary>
	/// Name of the invoked application command.
	/// </summary>
	[JsonPropertyName("name")]
	public String Name { get; init; } = default!;

	/// <summary>
	/// The user who invoked this interaction.
	/// </summary>
	[JsonPropertyName("user")]
	public DiscordUser User { get; init; } = default!;

	/// <summary>
	/// The guild member who invoked this interaction
	/// </summary>
	[JsonPropertyName("member")]
	public DiscordGuildMember? Member { get; init; }
}
