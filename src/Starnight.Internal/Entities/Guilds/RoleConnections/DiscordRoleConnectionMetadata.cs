namespace Starnight.Internal.Entities.Guilds.RoleConnections;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

/// <summary>
/// Represents an application role connection metadata object.
/// </summary>
public record DiscordRoleConnectionMetadata
{
	/// <summary>
	/// The type of the metadata value.
	/// </summary>
	[JsonPropertyName("type")]
	public required DiscordRoleConnectionMetadataType Type { get; init; }

	/// <summary>
	/// The dictionary key for the metadata field.
	/// </summary>
	[JsonPropertyName("key")]
	public required String Key { get; init; }

	/// <summary>
	/// The name of the metadata field.
	/// </summary>
	[JsonPropertyName("name")]
	public required String Name { get; init; }

	/// <summary>
	/// Translations of the name, with the keys in available locales.
	/// </summary>
	[JsonPropertyName("name_localizations")]
	public Optional<IDictionary<String, String>> NameLocalizations { get; init; }

	/// <summary>
	/// The description of the metadata field.
	/// </summary>
	[JsonPropertyName("description")]
	public required String Description { get; init; }

	/// <summary>
	/// Translations of the description, with the keys in available locales.
	/// </summary>
	[JsonPropertyName("description_localizations")]
	public Optional<IDictionary<String, String>> DescriptionLocalizations { get; init; }
}
