namespace Starnight.Internal.Rest.Payloads.GuildTemplates;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a payload to POST /guilds/:guild_id/templates
/// </summary>
public sealed record CreateGuildTemplateRequestPayload
{
	/// <summary>
	/// The name of this template.
	/// </summary>
	[JsonPropertyName("name")]
	public required String Name { get; init; }

	/// <summary>
	/// Optional description for this template.
	/// </summary>
	[JsonPropertyName("description")]
	public Optional<String?> Description { get; init; }
}
