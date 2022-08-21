namespace Starnight.Internal.Gateway.Payloads.Clientbound.Dispatch;

using System;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Interactions.ApplicationCommands;

/// <summary>
/// Fired when an application command's permissions are updated.
/// </summary>
[StructLayout(LayoutKind.Auto)]
public record struct DiscordApplicationCommandPermissionsUpdatedGatewayEvent
	: IDiscordGatewayDispatchPayload<DiscordApplicationCommandPermissions>
{
	/// <inheritdoc/>
	[JsonPropertyName("s")]
	public Int32 Sequence { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("t")]
	public String EventName { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("d")]
	public DiscordApplicationCommandPermissions Data { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("op")]
	public DiscordGatewayOpcode Opcode { get; init; }
}
