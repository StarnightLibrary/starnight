namespace Starnight.Internal.Entities.Voice;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a voice/rtc region.
/// </summary>
public sealed record DiscordVoiceRegion
{
	/// <summary>
	/// Unique region ID.
	/// </summary>
	[JsonPropertyName("id")]
	public required String Id { get; init; }

	/// <summary>
	/// Region name.
	/// </summary>
	[JsonPropertyName("name")]
	public required String Name { get; init; }

	/// <summary>
	/// Whether this region is optimal for the current user's client.
	/// </summary>
	[JsonPropertyName("optimal")]
	public required Boolean IsOptimal { get; init; }

	/// <summary>
	/// Whether this region is considered deprecated.
	/// </summary>
	[JsonPropertyName("deprecated")]
	public required Boolean IsDeprecated { get; init; }

	/// <summary>
	/// Whether this is a custom region, used for events etc.
	/// </summary>
	[JsonPropertyName("custom")]
	public required Boolean IsCustom { get; init; }
}
