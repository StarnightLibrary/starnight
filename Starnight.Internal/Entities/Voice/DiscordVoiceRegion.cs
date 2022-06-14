namespace Starnight.Internal.Entities.Voice;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a voice/rtc region.
/// </summary>
public record DiscordVoiceRegion
{
	/// <summary>
	/// Unique region ID.
	/// </summary>
	[JsonPropertyName("id")]
	public String Id { get; init; } = null!;

	/// <summary>
	/// Region name.
	/// </summary>
	[JsonPropertyName("name")]
	public String Name { get; init; } = null!;

	/// <summary>
	/// Whether this region is optimal for the current user's client.
	/// </summary>
	[JsonPropertyName("optimal")]
	public Boolean IsOptimal { get; init; }

	/// <summary>
	/// Whether this region is considered deprecated.
	/// </summary>
	[JsonPropertyName("deprecated")]
	public Boolean IsDeprecated { get; init; }

	/// <summary>
	/// Whether this is a custom region, used for events etc.
	/// </summary>
	[JsonPropertyName("custom")]
	public Boolean IsCustom { get; init; }
}
