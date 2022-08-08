namespace Starnight.Internal.Entities.Interactions;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Guilds;
using Starnight.Internal.Entities.Users;

/// <summary>
/// Represents a discord message interaction.
/// </summary>
public sealed record DiscordMessageInteraction : DiscordSnowflakeObject
{
	/// <summary>
	/// Interaction type associated with this object.
	/// </summary>
	[JsonPropertyName("type")]
	public required DiscordInteractionType InteractionType { get; init; }

	/// <summary>
	/// Name of the invoked application command.
	/// </summary>
	[JsonPropertyName("name")]
	public required String Name { get; init; }

	/// <summary>
	/// The user who invoked this interaction.
	/// </summary>
	[JsonPropertyName("user")]
	public required DiscordUser User { get; init; };

	/// <summary>
	/// The guild member who invoked this interaction
	/// </summary>
	[JsonPropertyName("member")]
	public Optional<DiscordGuildMember> Member { get; init; }
}
