namespace Starnight.Internal.Entities.Interactions.ApplicationCommands;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a singular application command permission overwrite.
/// </summary>
public sealed record DiscordApplicationCommandPermission : DiscordSnowflakeObject
{
	/// <summary>
	/// The object type targeted by this overwrite.
	/// </summary>
	[JsonPropertyName("type")]
	public required DiscordApplicationCommandPermissionType Type { get; init; }

	/// <summary>
	/// Whether this permission allows or disallows use of the command.
	/// </summary>
	[JsonPropertyName("permission")]
	public required Boolean Permission { get; init; }
}
