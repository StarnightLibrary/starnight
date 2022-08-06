namespace Starnight.Internal.Rest.Payloads.GuildTemplates;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a payload to POST /guilds/templates/:template_code
/// </summary>
public sealed record CreateGuildFromTemplateRequestPayload
{
	/// <summary>
	/// The name of your guild.
	/// </summary>
	[JsonPropertyName("name")]
	public required String Name { get; init; }

	/// <summary>
	/// The 128x128 icon for this guild.
	/// </summary>
	[JsonPropertyName("icon")]
	public Optional<String> Icon { get; init; }
}
