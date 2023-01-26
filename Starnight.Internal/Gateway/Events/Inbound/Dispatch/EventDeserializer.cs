// auto-generated code
namespace Starnight.Internal.Gateway.Events.Inbound.Dispatch;

using System;
using System.CodeDom.Compiler;
using System.Text.Json;

using Starnight.Internal;
using Starnight.Internal.Entities.Channels;
using Starnight.Internal.Entities.Channels.Threads;
using Starnight.Internal.Entities.Guilds;
using Starnight.Internal.Entities.Guilds.Audit;
using Starnight.Internal.Entities.Guilds.AutoModeration;
using Starnight.Internal.Entities.Interactions;
using Starnight.Internal.Entities.Interactions.ApplicationCommands;
using Starnight.Internal.Entities.Messages;
using Starnight.Internal.Entities.Users;
using Starnight.Internal.Entities.Voice;
using Starnight.Internal.Gateway.Payloads.Inbound;
using Starnight.Internal.Gateway.Payloads.Inbound.Dispatch;

[GeneratedCode("starnight-internal-events-generator", "v0.3.0")]
internal static class EventDeserializer
{
	// methods here are separated to reduce IL size in the main method
	private static IDiscordGatewayEvent deserializeDiscordConnectedEvent
	(
		JsonElement element,
		String name
	)
	{
		return new DiscordConnectedEvent
		{
			Opcode = (DiscordGatewayOpcode)element.GetProperty("op").GetInt32(),
			EventName = name,
			Sequence = element.GetProperty("s").GetInt32(),
			Data = JsonSerializer.Deserialize<ConnectedPayload>
			(
				element.GetProperty("d"),
				StarnightInternalConstants.DefaultSerializerOptions
			)!
		};
	}

	private static IDiscordGatewayEvent deserializeDiscordApplicationCommandPermissionsUpdatedEvent
	(
		JsonElement element,
		String name
	)
	{
		return new DiscordApplicationCommandPermissionsUpdatedEvent
		{
			Opcode = (DiscordGatewayOpcode)element.GetProperty("op").GetInt32(),
			EventName = name,
			Sequence = element.GetProperty("s").GetInt32(),
			Data = JsonSerializer.Deserialize<DiscordApplicationCommandPermissions>
			(
				element.GetProperty("d"),
				StarnightInternalConstants.DefaultSerializerOptions
			)!
		};
	}

	private static IDiscordGatewayEvent deserializeDiscordAutoModerationRuleCreatedEvent
	(
		JsonElement element,
		String name
	)
	{
		return new DiscordAutoModerationRuleCreatedEvent
		{
			Opcode = (DiscordGatewayOpcode)element.GetProperty("op").GetInt32(),
			EventName = name,
			Sequence = element.GetProperty("s").GetInt32(),
			Data = JsonSerializer.Deserialize<DiscordAutoModerationRule>
			(
				element.GetProperty("d"),
				StarnightInternalConstants.DefaultSerializerOptions
			)!
		};
	}

	private static IDiscordGatewayEvent deserializeDiscordAutoModerationRuleUpdatedEvent
	(
		JsonElement element,
		String name
	)
	{
		return new DiscordAutoModerationRuleUpdatedEvent
		{
			Opcode = (DiscordGatewayOpcode)element.GetProperty("op").GetInt32(),
			EventName = name,
			Sequence = element.GetProperty("s").GetInt32(),
			Data = JsonSerializer.Deserialize<DiscordAutoModerationRule>
			(
				element.GetProperty("d"),
				StarnightInternalConstants.DefaultSerializerOptions
			)!
		};
	}

	private static IDiscordGatewayEvent deserializeDiscordAutoModerationRuleDeletedEvent
	(
		JsonElement element,
		String name
	)
	{
		return new DiscordAutoModerationRuleDeletedEvent
		{
			Opcode = (DiscordGatewayOpcode)element.GetProperty("op").GetInt32(),
			EventName = name,
			Sequence = element.GetProperty("s").GetInt32(),
			Data = JsonSerializer.Deserialize<DiscordAutoModerationRule>
			(
				element.GetProperty("d"),
				StarnightInternalConstants.DefaultSerializerOptions
			)!
		};
	}

	private static IDiscordGatewayEvent deserializeDiscordAutoModerationActionExecutedEvent
	(
		JsonElement element,
		String name
	)
	{
		return new DiscordAutoModerationActionExecutedEvent
		{
			Opcode = (DiscordGatewayOpcode)element.GetProperty("op").GetInt32(),
			EventName = name,
			Sequence = element.GetProperty("s").GetInt32(),
			Data = JsonSerializer.Deserialize<AutoModerationActionExecutedPayload>
			(
				element.GetProperty("d"),
				StarnightInternalConstants.DefaultSerializerOptions
			)!
		};
	}

	private static IDiscordGatewayEvent deserializeDiscordChannelCreatedEvent
	(
		JsonElement element,
		String name
	)
	{
		return new DiscordChannelCreatedEvent
		{
			Opcode = (DiscordGatewayOpcode)element.GetProperty("op").GetInt32(),
			EventName = name,
			Sequence = element.GetProperty("s").GetInt32(),
			Data = JsonSerializer.Deserialize<DiscordChannel>
			(
				element.GetProperty("d"),
				StarnightInternalConstants.DefaultSerializerOptions
			)!
		};
	}

	private static IDiscordGatewayEvent deserializeDiscordChannelUpdatedEvent
	(
		JsonElement element,
		String name
	)
	{
		return new DiscordChannelUpdatedEvent
		{
			Opcode = (DiscordGatewayOpcode)element.GetProperty("op").GetInt32(),
			EventName = name,
			Sequence = element.GetProperty("s").GetInt32(),
			Data = JsonSerializer.Deserialize<DiscordChannel>
			(
				element.GetProperty("d"),
				StarnightInternalConstants.DefaultSerializerOptions
			)!
		};
	}

	private static IDiscordGatewayEvent deserializeDiscordChannelDeletedEvent
	(
		JsonElement element,
		String name
	)
	{
		return new DiscordChannelDeletedEvent
		{
			Opcode = (DiscordGatewayOpcode)element.GetProperty("op").GetInt32(),
			EventName = name,
			Sequence = element.GetProperty("s").GetInt32(),
			Data = JsonSerializer.Deserialize<DiscordChannel>
			(
				element.GetProperty("d"),
				StarnightInternalConstants.DefaultSerializerOptions
			)!
		};
	}

	private static IDiscordGatewayEvent deserializeDiscordChannelPinsUpdatedEvent
	(
		JsonElement element,
		String name
	)
	{
		return new DiscordChannelPinsUpdatedEvent
		{
			Opcode = (DiscordGatewayOpcode)element.GetProperty("op").GetInt32(),
			EventName = name,
			Sequence = element.GetProperty("s").GetInt32(),
			Data = JsonSerializer.Deserialize<ChannelPinsUpdatedPayload>
			(
				element.GetProperty("d"),
				StarnightInternalConstants.DefaultSerializerOptions
			)!
		};
	}

	private static IDiscordGatewayEvent deserializeDiscordThreadCreatedEvent
	(
		JsonElement element,
		String name
	)
	{
		return new DiscordThreadCreatedEvent
		{
			Opcode = (DiscordGatewayOpcode)element.GetProperty("op").GetInt32(),
			EventName = name,
			Sequence = element.GetProperty("s").GetInt32(),
			Data = JsonSerializer.Deserialize<DiscordChannel>
			(
				element.GetProperty("d"),
				StarnightInternalConstants.DefaultSerializerOptions
			)!
		};
	}

	private static IDiscordGatewayEvent deserializeDiscordThreadUpdatedEvent
	(
		JsonElement element,
		String name
	)
	{
		return new DiscordThreadUpdatedEvent
		{
			Opcode = (DiscordGatewayOpcode)element.GetProperty("op").GetInt32(),
			EventName = name,
			Sequence = element.GetProperty("s").GetInt32(),
			Data = JsonSerializer.Deserialize<DiscordChannel>
			(
				element.GetProperty("d"),
				StarnightInternalConstants.DefaultSerializerOptions
			)!
		};
	}

	private static IDiscordGatewayEvent deserializeDiscordThreadDeletedEvent
	(
		JsonElement element,
		String name
	)
	{
		return new DiscordThreadDeletedEvent
		{
			Opcode = (DiscordGatewayOpcode)element.GetProperty("op").GetInt32(),
			EventName = name,
			Sequence = element.GetProperty("s").GetInt32(),
			Data = JsonSerializer.Deserialize<DiscordChannel>
			(
				element.GetProperty("d"),
				StarnightInternalConstants.DefaultSerializerOptions
			)!
		};
	}

	private static IDiscordGatewayEvent deserializeDiscordThreadListSyncEvent
	(
		JsonElement element,
		String name
	)
	{
		return new DiscordThreadListSyncEvent
		{
			Opcode = (DiscordGatewayOpcode)element.GetProperty("op").GetInt32(),
			EventName = name,
			Sequence = element.GetProperty("s").GetInt32(),
			Data = JsonSerializer.Deserialize<ThreadListSyncPayload>
			(
				element.GetProperty("d"),
				StarnightInternalConstants.DefaultSerializerOptions
			)!
		};
	}

	private static IDiscordGatewayEvent deserializeDiscordThreadMemberUpdatedEvent
	(
		JsonElement element,
		String name
	)
	{
		return new DiscordThreadMemberUpdatedEvent
		{
			Opcode = (DiscordGatewayOpcode)element.GetProperty("op").GetInt32(),
			EventName = name,
			Sequence = element.GetProperty("s").GetInt32(),
			Data = JsonSerializer.Deserialize<DiscordThreadMember>
			(
				element.GetProperty("d"),
				StarnightInternalConstants.DefaultSerializerOptions
			)!
		};
	}

	private static IDiscordGatewayEvent deserializeDiscordThreadMembersUpdatedEvent
	(
		JsonElement element,
		String name
	)
	{
		return new DiscordThreadMembersUpdatedEvent
		{
			Opcode = (DiscordGatewayOpcode)element.GetProperty("op").GetInt32(),
			EventName = name,
			Sequence = element.GetProperty("s").GetInt32(),
			Data = JsonSerializer.Deserialize<ThreadMembersUpdatedPayload>
			(
				element.GetProperty("d"),
				StarnightInternalConstants.DefaultSerializerOptions
			)!
		};
	}

	private static IDiscordGatewayEvent deserializeDiscordGuildCreatedEvent
	(
		JsonElement element,
		String name
	)
	{
		return new DiscordGuildCreatedEvent
		{
			Opcode = (DiscordGatewayOpcode)element.GetProperty("op").GetInt32(),
			EventName = name,
			Sequence = element.GetProperty("s").GetInt32(),
			Data = JsonSerializer.Deserialize<DiscordGuild>
			(
				element.GetProperty("d"),
				StarnightInternalConstants.DefaultSerializerOptions
			)!
		};
	}

	private static IDiscordGatewayEvent deserializeDiscordGuildUpdatedEvent
	(
		JsonElement element,
		String name
	)
	{
		return new DiscordGuildUpdatedEvent
		{
			Opcode = (DiscordGatewayOpcode)element.GetProperty("op").GetInt32(),
			EventName = name,
			Sequence = element.GetProperty("s").GetInt32(),
			Data = JsonSerializer.Deserialize<DiscordGuild>
			(
				element.GetProperty("d"),
				StarnightInternalConstants.DefaultSerializerOptions
			)!
		};
	}

	private static IDiscordGatewayEvent deserializeDiscordGuildDeletedEvent
	(
		JsonElement element,
		String name
	)
	{
		return new DiscordGuildDeletedEvent
		{
			Opcode = (DiscordGatewayOpcode)element.GetProperty("op").GetInt32(),
			EventName = name,
			Sequence = element.GetProperty("s").GetInt32(),
			Data = JsonSerializer.Deserialize<DiscordUnavailableGuild>
			(
				element.GetProperty("d"),
				StarnightInternalConstants.DefaultSerializerOptions
			)!
		};
	}

	private static IDiscordGatewayEvent deserializeDiscordAuditLogEntryCreatedEvent
	(
		JsonElement element,
		String name
	)
	{
		return new DiscordAuditLogEntryCreatedEvent
		{
			Opcode = (DiscordGatewayOpcode)element.GetProperty("op").GetInt32(),
			EventName = name,
			Sequence = element.GetProperty("s").GetInt32(),
			Data = JsonSerializer.Deserialize<DiscordAuditLogEntry>
			(
				element.GetProperty("d"),
				StarnightInternalConstants.DefaultSerializerOptions
			)!
		};
	}

	private static IDiscordGatewayEvent deserializeDiscordGuildBanAddedEvent
	(
		JsonElement element,
		String name
	)
	{
		return new DiscordGuildBanAddedEvent
		{
			Opcode = (DiscordGatewayOpcode)element.GetProperty("op").GetInt32(),
			EventName = name,
			Sequence = element.GetProperty("s").GetInt32(),
			Data = JsonSerializer.Deserialize<GuildBanAddedPayload>
			(
				element.GetProperty("d"),
				StarnightInternalConstants.DefaultSerializerOptions
			)!
		};
	}

	private static IDiscordGatewayEvent deserializeDiscordGuildBanRemovedEvent
	(
		JsonElement element,
		String name
	)
	{
		return new DiscordGuildBanRemovedEvent
		{
			Opcode = (DiscordGatewayOpcode)element.GetProperty("op").GetInt32(),
			EventName = name,
			Sequence = element.GetProperty("s").GetInt32(),
			Data = JsonSerializer.Deserialize<GuildBanRemovedPayload>
			(
				element.GetProperty("d"),
				StarnightInternalConstants.DefaultSerializerOptions
			)!
		};
	}

	private static IDiscordGatewayEvent deserializeDiscordGuildEmojisUpdatedEvent
	(
		JsonElement element,
		String name
	)
	{
		return new DiscordGuildEmojisUpdatedEvent
		{
			Opcode = (DiscordGatewayOpcode)element.GetProperty("op").GetInt32(),
			EventName = name,
			Sequence = element.GetProperty("s").GetInt32(),
			Data = JsonSerializer.Deserialize<GuildEmojisUpdatedPayload>
			(
				element.GetProperty("d"),
				StarnightInternalConstants.DefaultSerializerOptions
			)!
		};
	}

	private static IDiscordGatewayEvent deserializeDiscordGuildStickersUpdatedEvent
	(
		JsonElement element,
		String name
	)
	{
		return new DiscordGuildStickersUpdatedEvent
		{
			Opcode = (DiscordGatewayOpcode)element.GetProperty("op").GetInt32(),
			EventName = name,
			Sequence = element.GetProperty("s").GetInt32(),
			Data = JsonSerializer.Deserialize<GuildStickersUpdatedPayload>
			(
				element.GetProperty("d"),
				StarnightInternalConstants.DefaultSerializerOptions
			)!
		};
	}

	private static IDiscordGatewayEvent deserializeDiscordGuildIntegrationsUpdatedEvent
	(
		JsonElement element,
		String name
	)
	{
		return new DiscordGuildIntegrationsUpdatedEvent
		{
			Opcode = (DiscordGatewayOpcode)element.GetProperty("op").GetInt32(),
			EventName = name,
			Sequence = element.GetProperty("s").GetInt32(),
			Data = JsonSerializer.Deserialize<GuildIntegrationsUpdatedPayload>
			(
				element.GetProperty("d"),
				StarnightInternalConstants.DefaultSerializerOptions
			)!
		};
	}

	private static IDiscordGatewayEvent deserializeDiscordGuildMemberAddedEvent
	(
		JsonElement element,
		String name
	)
	{
		return new DiscordGuildMemberAddedEvent
		{
			Opcode = (DiscordGatewayOpcode)element.GetProperty("op").GetInt32(),
			EventName = name,
			Sequence = element.GetProperty("s").GetInt32(),
			Data = JsonSerializer.Deserialize<DiscordGuildMember>
			(
				element.GetProperty("d"),
				StarnightInternalConstants.DefaultSerializerOptions
			)!
		};
	}

	private static IDiscordGatewayEvent deserializeDiscordGuildMemberRemovedEvent
	(
		JsonElement element,
		String name
	)
	{
		return new DiscordGuildMemberRemovedEvent
		{
			Opcode = (DiscordGatewayOpcode)element.GetProperty("op").GetInt32(),
			EventName = name,
			Sequence = element.GetProperty("s").GetInt32(),
			Data = JsonSerializer.Deserialize<GuildMemberRemovedPayload>
			(
				element.GetProperty("d"),
				StarnightInternalConstants.DefaultSerializerOptions
			)!
		};
	}

	private static IDiscordGatewayEvent deserializeDiscordGuildMemberUpdatedEvent
	(
		JsonElement element,
		String name
	)
	{
		return new DiscordGuildMemberUpdatedEvent
		{
			Opcode = (DiscordGatewayOpcode)element.GetProperty("op").GetInt32(),
			EventName = name,
			Sequence = element.GetProperty("s").GetInt32(),
			Data = JsonSerializer.Deserialize<DiscordGuildMember>
			(
				element.GetProperty("d"),
				StarnightInternalConstants.DefaultSerializerOptions
			)!
		};
	}

	private static IDiscordGatewayEvent deserializeDiscordGuildMembersChunkEvent
	(
		JsonElement element,
		String name
	)
	{
		return new DiscordGuildMembersChunkEvent
		{
			Opcode = (DiscordGatewayOpcode)element.GetProperty("op").GetInt32(),
			EventName = name,
			Sequence = element.GetProperty("s").GetInt32(),
			Data = JsonSerializer.Deserialize<GuildMembersChunkPayload>
			(
				element.GetProperty("d"),
				StarnightInternalConstants.DefaultSerializerOptions
			)!
		};
	}

	private static IDiscordGatewayEvent deserializeDiscordGuildRoleCreatedEvent
	(
		JsonElement element,
		String name
	)
	{
		return new DiscordGuildRoleCreatedEvent
		{
			Opcode = (DiscordGatewayOpcode)element.GetProperty("op").GetInt32(),
			EventName = name,
			Sequence = element.GetProperty("s").GetInt32(),
			Data = JsonSerializer.Deserialize<GuildRoleCreatedPayload>
			(
				element.GetProperty("d"),
				StarnightInternalConstants.DefaultSerializerOptions
			)!
		};
	}

	private static IDiscordGatewayEvent deserializeDiscordGuildRoleUpdatedEvent
	(
		JsonElement element,
		String name
	)
	{
		return new DiscordGuildRoleUpdatedEvent
		{
			Opcode = (DiscordGatewayOpcode)element.GetProperty("op").GetInt32(),
			EventName = name,
			Sequence = element.GetProperty("s").GetInt32(),
			Data = JsonSerializer.Deserialize<GuildRoleUpdatedPayload>
			(
				element.GetProperty("d"),
				StarnightInternalConstants.DefaultSerializerOptions
			)!
		};
	}

	private static IDiscordGatewayEvent deserializeDiscordGuildRoleDeletedEvent
	(
		JsonElement element,
		String name
	)
	{
		return new DiscordGuildRoleDeletedEvent
		{
			Opcode = (DiscordGatewayOpcode)element.GetProperty("op").GetInt32(),
			EventName = name,
			Sequence = element.GetProperty("s").GetInt32(),
			Data = JsonSerializer.Deserialize<GuildRoleDeletedPayload>
			(
				element.GetProperty("d"),
				StarnightInternalConstants.DefaultSerializerOptions
			)!
		};
	}

	private static IDiscordGatewayEvent deserializeDiscordScheduledEventCreatedEvent
	(
		JsonElement element,
		String name
	)
	{
		return new DiscordScheduledEventCreatedEvent
		{
			Opcode = (DiscordGatewayOpcode)element.GetProperty("op").GetInt32(),
			EventName = name,
			Sequence = element.GetProperty("s").GetInt32(),
			Data = JsonSerializer.Deserialize<DiscordScheduledEvent>
			(
				element.GetProperty("d"),
				StarnightInternalConstants.DefaultSerializerOptions
			)!
		};
	}

	private static IDiscordGatewayEvent deserializeDiscordScheduledEventUpdatedEvent
	(
		JsonElement element,
		String name
	)
	{
		return new DiscordScheduledEventUpdatedEvent
		{
			Opcode = (DiscordGatewayOpcode)element.GetProperty("op").GetInt32(),
			EventName = name,
			Sequence = element.GetProperty("s").GetInt32(),
			Data = JsonSerializer.Deserialize<DiscordScheduledEvent>
			(
				element.GetProperty("d"),
				StarnightInternalConstants.DefaultSerializerOptions
			)!
		};
	}

	private static IDiscordGatewayEvent deserializeDiscordScheduledEventDeletedEvent
	(
		JsonElement element,
		String name
	)
	{
		return new DiscordScheduledEventDeletedEvent
		{
			Opcode = (DiscordGatewayOpcode)element.GetProperty("op").GetInt32(),
			EventName = name,
			Sequence = element.GetProperty("s").GetInt32(),
			Data = JsonSerializer.Deserialize<DiscordScheduledEvent>
			(
				element.GetProperty("d"),
				StarnightInternalConstants.DefaultSerializerOptions
			)!
		};
	}

	private static IDiscordGatewayEvent deserializeDiscordScheduledEventUserAddedEvent
	(
		JsonElement element,
		String name
	)
	{
		return new DiscordScheduledEventUserAddedEvent
		{
			Opcode = (DiscordGatewayOpcode)element.GetProperty("op").GetInt32(),
			EventName = name,
			Sequence = element.GetProperty("s").GetInt32(),
			Data = JsonSerializer.Deserialize<ScheduledEventUserAddedPayload>
			(
				element.GetProperty("d"),
				StarnightInternalConstants.DefaultSerializerOptions
			)!
		};
	}

	private static IDiscordGatewayEvent deserializeDiscordScheduledEventUserRemovedEvent
	(
		JsonElement element,
		String name
	)
	{
		return new DiscordScheduledEventUserRemovedEvent
		{
			Opcode = (DiscordGatewayOpcode)element.GetProperty("op").GetInt32(),
			EventName = name,
			Sequence = element.GetProperty("s").GetInt32(),
			Data = JsonSerializer.Deserialize<ScheduledEventUserRemovedPayload>
			(
				element.GetProperty("d"),
				StarnightInternalConstants.DefaultSerializerOptions
			)!
		};
	}

	private static IDiscordGatewayEvent deserializeDiscordIntegrationCreatedEvent
	(
		JsonElement element,
		String name
	)
	{
		return new DiscordIntegrationCreatedEvent
		{
			Opcode = (DiscordGatewayOpcode)element.GetProperty("op").GetInt32(),
			EventName = name,
			Sequence = element.GetProperty("s").GetInt32(),
			Data = JsonSerializer.Deserialize<DiscordGuildIntegration>
			(
				element.GetProperty("d"),
				StarnightInternalConstants.DefaultSerializerOptions
			)!
		};
	}

	private static IDiscordGatewayEvent deserializeDiscordIntegrationUpdatedEvent
	(
		JsonElement element,
		String name
	)
	{
		return new DiscordIntegrationUpdatedEvent
		{
			Opcode = (DiscordGatewayOpcode)element.GetProperty("op").GetInt32(),
			EventName = name,
			Sequence = element.GetProperty("s").GetInt32(),
			Data = JsonSerializer.Deserialize<DiscordGuildIntegration>
			(
				element.GetProperty("d"),
				StarnightInternalConstants.DefaultSerializerOptions
			)!
		};
	}

	private static IDiscordGatewayEvent deserializeDiscordIntegrationDeletedEvent
	(
		JsonElement element,
		String name
	)
	{
		return new DiscordIntegrationDeletedEvent
		{
			Opcode = (DiscordGatewayOpcode)element.GetProperty("op").GetInt32(),
			EventName = name,
			Sequence = element.GetProperty("s").GetInt32(),
			Data = JsonSerializer.Deserialize<IntegrationDeletedPayload>
			(
				element.GetProperty("d"),
				StarnightInternalConstants.DefaultSerializerOptions
			)!
		};
	}

	private static IDiscordGatewayEvent deserializeDiscordInteractionCreatedEvent
	(
		JsonElement element,
		String name
	)
	{
		return new DiscordInteractionCreatedEvent
		{
			Opcode = (DiscordGatewayOpcode)element.GetProperty("op").GetInt32(),
			EventName = name,
			Sequence = element.GetProperty("s").GetInt32(),
			Data = JsonSerializer.Deserialize<DiscordInteraction>
			(
				element.GetProperty("d"),
				StarnightInternalConstants.DefaultSerializerOptions
			)!
		};
	}

	private static IDiscordGatewayEvent deserializeDiscordInviteCreatedEvent
	(
		JsonElement element,
		String name
	)
	{
		return new DiscordInviteCreatedEvent
		{
			Opcode = (DiscordGatewayOpcode)element.GetProperty("op").GetInt32(),
			EventName = name,
			Sequence = element.GetProperty("s").GetInt32(),
			Data = JsonSerializer.Deserialize<InviteCreatedPayload>
			(
				element.GetProperty("d"),
				StarnightInternalConstants.DefaultSerializerOptions
			)!
		};
	}

	private static IDiscordGatewayEvent deserializeDiscordInviteDeletedEvent
	(
		JsonElement element,
		String name
	)
	{
		return new DiscordInviteDeletedEvent
		{
			Opcode = (DiscordGatewayOpcode)element.GetProperty("op").GetInt32(),
			EventName = name,
			Sequence = element.GetProperty("s").GetInt32(),
			Data = JsonSerializer.Deserialize<InviteDeletedPayload>
			(
				element.GetProperty("d"),
				StarnightInternalConstants.DefaultSerializerOptions
			)!
		};
	}

	private static IDiscordGatewayEvent deserializeDiscordMessageCreatedEvent
	(
		JsonElement element,
		String name
	)
	{
		return new DiscordMessageCreatedEvent
		{
			Opcode = (DiscordGatewayOpcode)element.GetProperty("op").GetInt32(),
			EventName = name,
			Sequence = element.GetProperty("s").GetInt32(),
			Data = JsonSerializer.Deserialize<DiscordMessage>
			(
				element.GetProperty("d"),
				StarnightInternalConstants.DefaultSerializerOptions
			)!
		};
	}

	private static IDiscordGatewayEvent deserializeDiscordMessageUpdatedEvent
	(
		JsonElement element,
		String name
	)
	{
		return new DiscordMessageUpdatedEvent
		{
			Opcode = (DiscordGatewayOpcode)element.GetProperty("op").GetInt32(),
			EventName = name,
			Sequence = element.GetProperty("s").GetInt32(),
			Data = JsonSerializer.Deserialize<DiscordMessage>
			(
				element.GetProperty("d"),
				StarnightInternalConstants.DefaultSerializerOptions
			)!
		};
	}

	private static IDiscordGatewayEvent deserializeDiscordMessageDeletedEvent
	(
		JsonElement element,
		String name
	)
	{
		return new DiscordMessageDeletedEvent
		{
			Opcode = (DiscordGatewayOpcode)element.GetProperty("op").GetInt32(),
			EventName = name,
			Sequence = element.GetProperty("s").GetInt32(),
			Data = JsonSerializer.Deserialize<MessageDeletedPayload>
			(
				element.GetProperty("d"),
				StarnightInternalConstants.DefaultSerializerOptions
			)!
		};
	}

	private static IDiscordGatewayEvent deserializeDiscordMessagesBulkDeletedEvent
	(
		JsonElement element,
		String name
	)
	{
		return new DiscordMessagesBulkDeletedEvent
		{
			Opcode = (DiscordGatewayOpcode)element.GetProperty("op").GetInt32(),
			EventName = name,
			Sequence = element.GetProperty("s").GetInt32(),
			Data = JsonSerializer.Deserialize<MessagesBulkDeletedPayload>
			(
				element.GetProperty("d"),
				StarnightInternalConstants.DefaultSerializerOptions
			)!
		};
	}

	private static IDiscordGatewayEvent deserializeDiscordMessageReactionAddedEvent
	(
		JsonElement element,
		String name
	)
	{
		return new DiscordMessageReactionAddedEvent
		{
			Opcode = (DiscordGatewayOpcode)element.GetProperty("op").GetInt32(),
			EventName = name,
			Sequence = element.GetProperty("s").GetInt32(),
			Data = JsonSerializer.Deserialize<MessageReactionAddedPayload>
			(
				element.GetProperty("d"),
				StarnightInternalConstants.DefaultSerializerOptions
			)!
		};
	}

	private static IDiscordGatewayEvent deserializeDiscordMessageReactionRemovedEvent
	(
		JsonElement element,
		String name
	)
	{
		return new DiscordMessageReactionRemovedEvent
		{
			Opcode = (DiscordGatewayOpcode)element.GetProperty("op").GetInt32(),
			EventName = name,
			Sequence = element.GetProperty("s").GetInt32(),
			Data = JsonSerializer.Deserialize<MessageReactionRemovedPayload>
			(
				element.GetProperty("d"),
				StarnightInternalConstants.DefaultSerializerOptions
			)!
		};
	}

	private static IDiscordGatewayEvent deserializeDiscordAllMessageReactionsRemovedEvent
	(
		JsonElement element,
		String name
	)
	{
		return new DiscordAllMessageReactionsRemovedEvent
		{
			Opcode = (DiscordGatewayOpcode)element.GetProperty("op").GetInt32(),
			EventName = name,
			Sequence = element.GetProperty("s").GetInt32(),
			Data = JsonSerializer.Deserialize<AllMessageReactionsRemovedPayload>
			(
				element.GetProperty("d"),
				StarnightInternalConstants.DefaultSerializerOptions
			)!
		};
	}

	private static IDiscordGatewayEvent deserializeDiscordEmojiMessageReactionsRemovedEvent
	(
		JsonElement element,
		String name
	)
	{
		return new DiscordEmojiMessageReactionsRemovedEvent
		{
			Opcode = (DiscordGatewayOpcode)element.GetProperty("op").GetInt32(),
			EventName = name,
			Sequence = element.GetProperty("s").GetInt32(),
			Data = JsonSerializer.Deserialize<EmojiMessageReactionsRemovedPayload>
			(
				element.GetProperty("d"),
				StarnightInternalConstants.DefaultSerializerOptions
			)!
		};
	}

	private static IDiscordGatewayEvent deserializeDiscordPresenceUpdatedEvent
	(
		JsonElement element,
		String name
	)
	{
		return new DiscordPresenceUpdatedEvent
		{
			Opcode = (DiscordGatewayOpcode)element.GetProperty("op").GetInt32(),
			EventName = name,
			Sequence = element.GetProperty("s").GetInt32(),
			Data = JsonSerializer.Deserialize<PresenceUpdatedPayload>
			(
				element.GetProperty("d"),
				StarnightInternalConstants.DefaultSerializerOptions
			)!
		};
	}

	private static IDiscordGatewayEvent deserializeDiscordStageInstanceCreatedEvent
	(
		JsonElement element,
		String name
	)
	{
		return new DiscordStageInstanceCreatedEvent
		{
			Opcode = (DiscordGatewayOpcode)element.GetProperty("op").GetInt32(),
			EventName = name,
			Sequence = element.GetProperty("s").GetInt32(),
			Data = JsonSerializer.Deserialize<DiscordStageInstance>
			(
				element.GetProperty("d"),
				StarnightInternalConstants.DefaultSerializerOptions
			)!
		};
	}

	private static IDiscordGatewayEvent deserializeDiscordStageInstanceUpdatedEvent
	(
		JsonElement element,
		String name
	)
	{
		return new DiscordStageInstanceUpdatedEvent
		{
			Opcode = (DiscordGatewayOpcode)element.GetProperty("op").GetInt32(),
			EventName = name,
			Sequence = element.GetProperty("s").GetInt32(),
			Data = JsonSerializer.Deserialize<DiscordStageInstance>
			(
				element.GetProperty("d"),
				StarnightInternalConstants.DefaultSerializerOptions
			)!
		};
	}

	private static IDiscordGatewayEvent deserializeDiscordStageInstanceDeletedEvent
	(
		JsonElement element,
		String name
	)
	{
		return new DiscordStageInstanceDeletedEvent
		{
			Opcode = (DiscordGatewayOpcode)element.GetProperty("op").GetInt32(),
			EventName = name,
			Sequence = element.GetProperty("s").GetInt32(),
			Data = JsonSerializer.Deserialize<DiscordStageInstance>
			(
				element.GetProperty("d"),
				StarnightInternalConstants.DefaultSerializerOptions
			)!
		};
	}

	private static IDiscordGatewayEvent deserializeDiscordTypingStartedEvent
	(
		JsonElement element,
		String name
	)
	{
		return new DiscordTypingStartedEvent
		{
			Opcode = (DiscordGatewayOpcode)element.GetProperty("op").GetInt32(),
			EventName = name,
			Sequence = element.GetProperty("s").GetInt32(),
			Data = JsonSerializer.Deserialize<TypingStartedPayload>
			(
				element.GetProperty("d"),
				StarnightInternalConstants.DefaultSerializerOptions
			)!
		};
	}

	private static IDiscordGatewayEvent deserializeDiscordUserUpdatedEvent
	(
		JsonElement element,
		String name
	)
	{
		return new DiscordUserUpdatedEvent
		{
			Opcode = (DiscordGatewayOpcode)element.GetProperty("op").GetInt32(),
			EventName = name,
			Sequence = element.GetProperty("s").GetInt32(),
			Data = JsonSerializer.Deserialize<DiscordUser>
			(
				element.GetProperty("d"),
				StarnightInternalConstants.DefaultSerializerOptions
			)!
		};
	}

	private static IDiscordGatewayEvent deserializeDiscordVoiceStateUpdatedEvent
	(
		JsonElement element,
		String name
	)
	{
		return new DiscordVoiceStateUpdatedEvent
		{
			Opcode = (DiscordGatewayOpcode)element.GetProperty("op").GetInt32(),
			EventName = name,
			Sequence = element.GetProperty("s").GetInt32(),
			Data = JsonSerializer.Deserialize<DiscordVoiceState>
			(
				element.GetProperty("d"),
				StarnightInternalConstants.DefaultSerializerOptions
			)!
		};
	}

	private static IDiscordGatewayEvent deserializeDiscordVoiceServerUpdatedEvent
	(
		JsonElement element,
		String name
	)
	{
		return new DiscordVoiceServerUpdatedEvent
		{
			Opcode = (DiscordGatewayOpcode)element.GetProperty("op").GetInt32(),
			EventName = name,
			Sequence = element.GetProperty("s").GetInt32(),
			Data = JsonSerializer.Deserialize<VoiceServerUpdatedPayload>
			(
				element.GetProperty("d"),
				StarnightInternalConstants.DefaultSerializerOptions
			)!
		};
	}

	private static IDiscordGatewayEvent deserializeDiscordWebhooksUpdatedEvent
	(
		JsonElement element,
		String name
	)
	{
		return new DiscordWebhooksUpdatedEvent
		{
			Opcode = (DiscordGatewayOpcode)element.GetProperty("op").GetInt32(),
			EventName = name,
			Sequence = element.GetProperty("s").GetInt32(),
			Data = JsonSerializer.Deserialize<WebhooksUpdatedPayload>
			(
				element.GetProperty("d"),
				StarnightInternalConstants.DefaultSerializerOptions
			)!
		};
	}

	public static IDiscordGatewayEvent DeserializeEvent
	(
		JsonElement element,
		String name
	)
	{
		switch(name)
		{
			case "READY":
				return deserializeDiscordConnectedEvent
				(
					element,
					name
				);

			case "APPLICATION_COMMAND_PERMISSIONS_UPDATE":
				return deserializeDiscordApplicationCommandPermissionsUpdatedEvent
				(
					element,
					name
				);

			case "AUTO_MODERATION_RULE_CREATE":
				return deserializeDiscordAutoModerationRuleCreatedEvent
				(
					element,
					name
				);

			case "AUTO_MODERATION_RULE_UPDATE":
				return deserializeDiscordAutoModerationRuleUpdatedEvent
				(
					element,
					name
				);

			case "AUTO_MODERATION_RULE_DELETE":
				return deserializeDiscordAutoModerationRuleDeletedEvent
				(
					element,
					name
				);

			case "AUTO_MODERATION_ACTION_EXECUTION":
				return deserializeDiscordAutoModerationActionExecutedEvent
				(
					element,
					name
				);

			case "CHANNEL_CREATE":
				return deserializeDiscordChannelCreatedEvent
				(
					element,
					name
				);

			case "CHANNEL_UPDATE":
				return deserializeDiscordChannelUpdatedEvent
				(
					element,
					name
				);

			case "CHANNEL_DELETE":
				return deserializeDiscordChannelDeletedEvent
				(
					element,
					name
				);

			case "CHANNEL_PINS_UPDATE":
				return deserializeDiscordChannelPinsUpdatedEvent
				(
					element,
					name
				);

			case "THREAD_CREATE":
				return deserializeDiscordThreadCreatedEvent
				(
					element,
					name
				);

			case "THREAD_UPDATE":
				return deserializeDiscordThreadUpdatedEvent
				(
					element,
					name
				);

			case "THREAD_DELETE":
				return deserializeDiscordThreadDeletedEvent
				(
					element,
					name
				);

			case "THREAD_LIST_SYNC":
				return deserializeDiscordThreadListSyncEvent
				(
					element,
					name
				);

			case "THREAD_MEMBER_UPDATE":
				return deserializeDiscordThreadMemberUpdatedEvent
				(
					element,
					name
				);

			case "THREAD_MEMBERS_UPDATE":
				return deserializeDiscordThreadMembersUpdatedEvent
				(
					element,
					name
				);

			case "GUILD_CREATE":
				return deserializeDiscordGuildCreatedEvent
				(
					element,
					name
				);

			case "GUILD_UPDATE":
				return deserializeDiscordGuildUpdatedEvent
				(
					element,
					name
				);

			case "GUILD_DELETE":
				return deserializeDiscordGuildDeletedEvent
				(
					element,
					name
				);

			case "GUILD_AUDIT_LOG_ENTRY_CREATE":
				return deserializeDiscordAuditLogEntryCreatedEvent
				(
					element,
					name
				);

			case "GUILD_BAN_ADD":
				return deserializeDiscordGuildBanAddedEvent
				(
					element,
					name
				);

			case "GUILD_BAN_REMOVE":
				return deserializeDiscordGuildBanRemovedEvent
				(
					element,
					name
				);

			case "GUILD_EMOJIS_UPDATE":
				return deserializeDiscordGuildEmojisUpdatedEvent
				(
					element,
					name
				);

			case "GUILD_STICKERS_UPDATE":
				return deserializeDiscordGuildStickersUpdatedEvent
				(
					element,
					name
				);

			case "GUILD_INTEGRATIONS_UPDATE":
				return deserializeDiscordGuildIntegrationsUpdatedEvent
				(
					element,
					name
				);

			case "GUILD_MEMBER_ADD":
				return deserializeDiscordGuildMemberAddedEvent
				(
					element,
					name
				);

			case "GUILD_MEMBER_REMOVE":
				return deserializeDiscordGuildMemberRemovedEvent
				(
					element,
					name
				);

			case "GUILD_MEMBER_UPDATE":
				return deserializeDiscordGuildMemberUpdatedEvent
				(
					element,
					name
				);

			case "GUILD_MEMBERS_CHUNK":
				return deserializeDiscordGuildMembersChunkEvent
				(
					element,
					name
				);

			case "GUILD_ROLE_CREATE":
				return deserializeDiscordGuildRoleCreatedEvent
				(
					element,
					name
				);

			case "GUILD_ROLE_UPDATE":
				return deserializeDiscordGuildRoleUpdatedEvent
				(
					element,
					name
				);

			case "GUILD_ROLE_DELETE":
				return deserializeDiscordGuildRoleDeletedEvent
				(
					element,
					name
				);

			case "GUILD_SCHEDULED_EVENT_CREATE":
				return deserializeDiscordScheduledEventCreatedEvent
				(
					element,
					name
				);

			case "GUILD_SCHEDULED_EVENT_UPDATE":
				return deserializeDiscordScheduledEventUpdatedEvent
				(
					element,
					name
				);

			case "GUILD_SCHEDULED_EVENT_DELETE":
				return deserializeDiscordScheduledEventDeletedEvent
				(
					element,
					name
				);

			case "GUILD_SCHEDULED_EVENT_USER_ADD":
				return deserializeDiscordScheduledEventUserAddedEvent
				(
					element,
					name
				);

			case "GUILD_SCHEDULED_EVENT_USER_REMOVE":
				return deserializeDiscordScheduledEventUserRemovedEvent
				(
					element,
					name
				);

			case "INTEGRATION_CREATE":
				return deserializeDiscordIntegrationCreatedEvent
				(
					element,
					name
				);

			case "INTEGRATION_UPDATE":
				return deserializeDiscordIntegrationUpdatedEvent
				(
					element,
					name
				);

			case "INTEGRATION_DELETE":
				return deserializeDiscordIntegrationDeletedEvent
				(
					element,
					name
				);

			case "INTERACTION_CREATE":
				return deserializeDiscordInteractionCreatedEvent
				(
					element,
					name
				);

			case "INVITE_CREATE":
				return deserializeDiscordInviteCreatedEvent
				(
					element,
					name
				);

			case "INVITE_DELETE":
				return deserializeDiscordInviteDeletedEvent
				(
					element,
					name
				);

			case "MESSAGE_CREATE":
				return deserializeDiscordMessageCreatedEvent
				(
					element,
					name
				);

			case "MESSAGE_UPDATE":
				return deserializeDiscordMessageUpdatedEvent
				(
					element,
					name
				);

			case "MESSAGE_DELETE":
				return deserializeDiscordMessageDeletedEvent
				(
					element,
					name
				);

			case "MESSAGE_DELETE_BULK":
				return deserializeDiscordMessagesBulkDeletedEvent
				(
					element,
					name
				);

			case "MESSAGE_REACTION_ADD":
				return deserializeDiscordMessageReactionAddedEvent
				(
					element,
					name
				);

			case "MESSAGE_REACTION_REMOVE":
				return deserializeDiscordMessageReactionRemovedEvent
				(
					element,
					name
				);

			case "MESSAGE_REACTION_REMOVE_ALL":
				return deserializeDiscordAllMessageReactionsRemovedEvent
				(
					element,
					name
				);

			case "MESSAGE_REACTION_REMOVE_EMOJI":
				return deserializeDiscordEmojiMessageReactionsRemovedEvent
				(
					element,
					name
				);

			case "PRESENCE_UPDATE":
				return deserializeDiscordPresenceUpdatedEvent
				(
					element,
					name
				);

			case "STAGE_INSTANCE_CREATE":
				return deserializeDiscordStageInstanceCreatedEvent
				(
					element,
					name
				);

			case "STAGE_INSTANCE_UPDATE":
				return deserializeDiscordStageInstanceUpdatedEvent
				(
					element,
					name
				);

			case "STAGE_INSTANCE_DELETE":
				return deserializeDiscordStageInstanceDeletedEvent
				(
					element,
					name
				);

			case "TYPING_START":
				return deserializeDiscordTypingStartedEvent
				(
					element,
					name
				);

			case "USER_UPDATE":
				return deserializeDiscordUserUpdatedEvent
				(
					element,
					name
				);

			case "VOICE_STATE_UPDATE":
				return deserializeDiscordVoiceStateUpdatedEvent
				(
					element,
					name
				);

			case "VOICE_SERVER_UPDATE":
				return deserializeDiscordVoiceServerUpdatedEvent
				(
					element,
					name
				);

			case "WEBHOOKS_UPDATE":
				return deserializeDiscordWebhooksUpdatedEvent
				(
					element,
					name
				);

			default:
				return new DiscordUnknownDispatchEvent
				{
					EventName = name,
					Sequence = element.GetProperty("s").GetInt32(),
					Opcode = (DiscordGatewayOpcode)element.GetProperty("op").GetInt32(),
					Data = element.GetProperty("d")
				};
		}
	}
}
