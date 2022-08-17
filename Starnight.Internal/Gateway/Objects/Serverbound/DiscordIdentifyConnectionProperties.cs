namespace Starnight.Internal.Gateway.Objects.Serverbound;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents the inner connection properties object of a <see cref="DiscordIdentifyCommandObject"/>.
/// </summary>
public record struct DiscordIdentifyConnectionProperties
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
