namespace Starnight.Internal.Gateway.Events.Inbound.Dispatch;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Interactions.ApplicationCommands;
using Starnight.Internal.Gateway.Events;

/// <summary>
/// Fired when an application command's permissions are updated.
/// </summary>
public sealed record DiscordApplicationCommandPermissionsUpdatedEvent
	: IDiscordGatewayDispatchEvent<DiscordApplicationCommandPermissions>
{
	/// <inheritdoc/>
	[JsonPropertyName("s")]
	public required Int32 Sequence { get; set; }

	/// <inheritdoc/>
	[JsonPropertyName("t")]
	public required String EventName { get; set; }

	/// <inheritdoc/>
	[JsonPropertyName("d")]
	public required DiscordApplicationCommandPermissions Data { get; set; }

	/// <inheritdoc/>
	[JsonPropertyName("op")]
	public required DiscordGatewayOpcode Opcode { get; set; }
}
