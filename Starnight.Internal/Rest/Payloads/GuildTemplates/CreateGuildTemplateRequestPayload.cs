namespace Starnight.Internal.Rest.Payloads.GuildTemplates;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a payload to POST /guilds/:guild_id/templates
/// </summary>
public record CreateGuildTemplateRequestPayload
{
	/// <summary>
	/// The name of this template.
	/// </summary>
	[JsonPropertyName("name")]
	public String Name { get; init; } = null!;

	/// <summary>
	/// Optional description for this template.
	/// </summary>
	[JsonPropertyName("description")]
	public String? Description { get; init; }
}
