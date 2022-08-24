namespace Starnight.Internal.Gateway.Payloads.Clientbound.Dispatch;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Interactions.ApplicationCommands;

/// <summary>
/// Fired when an application command's permissions are updated.
/// </summary>
public sealed record DiscordApplicationCommandPermissionsUpdatedEvent
	: IDiscordGatewayDispatchPayload<DiscordApplicationCommandPermissions>
{
	/// <inheritdoc/>
	[JsonPropertyName("s")]
	public required Int32 Sequence { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("t")]
	public required String EventName { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("d")]
	public required DiscordApplicationCommandPermissions Data { get; init; }

	/// <inheritdoc/>
	[JsonPropertyName("op")]
	public required DiscordGatewayOpcode Opcode { get; init; }
}
