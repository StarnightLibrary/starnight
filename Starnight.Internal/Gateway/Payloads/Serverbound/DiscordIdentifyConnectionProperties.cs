namespace Starnight.Internal.Gateway.Payloads.Serverbound;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents the inner connection properties object of a <see cref="IdentifyPayload"/>.
/// </summary>
public sealed record DiscordIdentifyConnectionProperties
{
	/// <summary>
	/// The operating system the client runs on.
	/// </summary>
	[JsonPropertyName("os")]
	public required String OS { get; init; }

	/// <summary>
	/// The library name the client uses.
	/// </summary>
	[JsonPropertyName("browser")]
	public required String Browser { get; init; }

	/// <summary>
	/// Once again, the library name the client uses.
	/// </summary>
	[JsonPropertyName("device")]
	public required String Device { get; init; }
}
