namespace Starnight.Internal.Rest.Payloads.GuildTemplates;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a payload to POST /guilds/templates/:template_code
/// </summary>
public record CreateGuildFromTemplateRequestPayload
{
	/// <summary>
	/// The name of your guild.
	/// </summary>
	[JsonPropertyName("name")]
	public String Name { get; init; } = null!;

	/// <summary>
	/// The 128x128 icon for this guild.
	/// </summary>
	[JsonPropertyName("icon")]
	public String? Icon { get; init; }
}
