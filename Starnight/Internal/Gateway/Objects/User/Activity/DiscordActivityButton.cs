namespace Starnight.Internal.Gateway.Objects.User.Activity;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a clickable RPC button. Every presence may have at most two buttons.
/// </summary>
public record DiscordActivityButton
{
	/// <summary>
	/// Text shown on the button, 1 - 32 characters.
	/// </summary>
	[JsonPropertyName("label")]
	public String Label { get; init; } = default!;

	/// <summary>
	/// URL opened when clicking the button, 1 - 512 characters.
	/// </summary>
	[JsonPropertyName("url")]
	public String Url { get; init; } = default!;
}
