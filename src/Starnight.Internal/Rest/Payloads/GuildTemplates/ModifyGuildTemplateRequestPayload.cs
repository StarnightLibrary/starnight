namespace Starnight.Internal.Rest.Payloads.GuildTemplates;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a payload to PATCH /guilds/:guild_id/templates/:template_code.
/// </summary>
public sealed record ModifyGuildTemplateRequestPayload
{
	/// <summary>
	/// The name of this template.
	/// </summary>
	[JsonPropertyName("name")]
	public Optional<String> Name { get; init; }

	/// <summary>
	/// Optional description for this template.
	/// </summary>
	[JsonPropertyName("description")]
	public Optional<String?> Description { get; init; }
}
