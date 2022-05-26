namespace Starnight.Internal.Rest.Payloads.GuildTemplates;

using System.Text.Json.Serialization;
using System;

/// <summary>
/// Represents a payload to PATCH /guilds/:guild_id/templates/:template_code.
/// </summary>
public record ModifyGuildTemplateRequestPayload
{
	/// <summary>
	/// The name of this template.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("name")]
	public String? Name { get; init; }

	/// <summary>
	/// Optional description for this template.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("description")]
	public String? Description { get; init; }
}
