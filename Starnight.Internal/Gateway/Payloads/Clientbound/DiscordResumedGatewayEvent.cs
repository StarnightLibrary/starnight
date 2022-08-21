namespace Starnight.Internal.Gateway.Payloads.Clientbound;

using System;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

/// <summary>
/// Dispatched if resumption was successful.
/// </summary>
/// <remarks>
/// The reason <see cref="IntPtr"/> is used here is that it is always guaranteed to be a machine word,
/// which is the ideal amount to allocate since it doesn't require a <c>movzx</c>, nor multiple <c>mov</c> instructions.
/// </remarks>
[StructLayout(LayoutKind.Auto)]
public record struct DiscordResumedGatewayEvent : IDiscordGatewayDispatchPayload<IntPtr>
{
	/// <inheritdoc/>
	[JsonPropertyName("op")]
	public DiscordGatewayOpcode Opcode { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("s")]
	public Int32 Sequence { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("t")]
	public String EventName { get; init; }

	/// <summary>
	/// This will always be 0.
	/// </summary>
	[JsonPropertyName("d")]
	public IntPtr Data { get; init; }
}
