namespace Starnight.Internal.Entities.Users.Activities;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a clickable RPC button. Every presence may have at most two buttons.
/// </summary>
public sealed record DiscordActivityButton
{
	/// <summary>
	/// Text shown on the button, 1 - 32 characters.
	/// </summary>
	[JsonPropertyName("label")]
	public required String Label { get; init; }

	/// <summary>
	/// URL opened when clicking the button, 1 - 512 characters.
	/// </summary>
	[JsonPropertyName("url")]
	public required String Url { get; init; }
}
