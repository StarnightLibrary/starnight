namespace Starnight.Internal.Gateway;

using System;
using System.Text.Json;

using Starnight.Internal.Gateway.Events.Inbound;
using Starnight.Internal.Gateway.Events.Inbound.Dispatch;
using Starnight.SourceGenerators.GatewayEvents;

/// <summary>
/// Contains static deserialization information about dispatch events.
/// </summary>
#region serialization attributes
[GatewayEvent(EventName = Ready, EventType = typeof(DiscordConnectedEvent))]
[GatewayEvent(EventName = ApplicationCommandPermissionsUpdate, EventType = typeof(DiscordApplicationCommandPermissionsUpdatedEvent))]
[GatewayEvent(EventName = AutoModerationRuleCreate, EventType = typeof(DiscordAutoModerationRuleCreatedEvent))]
[GatewayEvent(EventName = AutoModerationRuleUpdate, EventType = typeof(DiscordAutoModerationRuleUpdatedEvent))]
[GatewayEvent(EventName = AutoModerationRuleDelete, EventType = typeof(DiscordAutoModerationRuleDeletedEvent))]
[GatewayEvent(EventName = AutoModerationActionExecution, EventType = typeof(DiscordAutoModerationActionExecutedEvent))]
[GatewayEvent(EventName = ChannelCreate, EventType = typeof(DiscordChannelCreatedEvent))]
[GatewayEvent(EventName = ChannelUpdate, EventType = typeof(DiscordChannelUpdatedEvent))]
[GatewayEvent(EventName = ChannelDelete, EventType = typeof(DiscordChannelDeletedEvent))]
[GatewayEvent(EventName = ChannelPinsUpdate, EventType = typeof(DiscordChannelPinsUpdatedEvent))]
[GatewayEvent(EventName = ThreadCreate, EventType = typeof(DiscordThreadCreatedEvent))]
[GatewayEvent(EventName = ThreadUpdate, EventType = typeof(DiscordThreadUpdatedEvent))]
[GatewayEvent(EventName = ThreadDelete, EventType = typeof(DiscordThreadDeletedEvent))]
[GatewayEvent(EventName = ThreadListSync, EventType = typeof(DiscordThreadListSyncEvent))]
[GatewayEvent(EventName = ThreadMemberUpdate, EventType = typeof(DiscordThreadMemberUpdatedEvent))]
[GatewayEvent(EventName = ThreadMembersUpdate, EventType = typeof(DiscordThreadMembersUpdatedEvent))]
[GatewayEvent(EventName = GuildCreate, EventType = typeof(DiscordGuildCreatedEvent))]
[GatewayEvent(EventName = GuildUpdate, EventType = typeof(DiscordGuildUpdatedEvent))]
[GatewayEvent(EventName = GuildDelete, EventType = typeof(DiscordGuildDeletedEvent))]
[GatewayEvent(EventName = GuildAuditLogEntryCreate, EventType = typeof(DiscordAuditLogEntryCreatedEvent))]
[GatewayEvent(EventName = GuildBanAdd, EventType = typeof(DiscordGuildBanAddedEvent))]
[GatewayEvent(EventName = GuildBanRemove, EventType = typeof(DiscordGuildBanRemovedEvent))]
[GatewayEvent(EventName = GuildEmojisUpdate, EventType = typeof(DiscordGuildEmojisUpdatedEvent))]
[GatewayEvent(EventName = GuildStickersUpdate, EventType = typeof(DiscordGuildStickersUpdatedEvent))]
[GatewayEvent(EventName = GuildIntegrationsUpdate, EventType = typeof(DiscordGuildIntegrationsUpdatedEvent))]
[GatewayEvent(EventName = GuildMemberAdd, EventType = typeof(DiscordGuildMemberAddedEvent))]
[GatewayEvent(EventName = GuildMemberRemove, EventType = typeof(DiscordGuildMemberRemovedEvent))]
[GatewayEvent(EventName = GuildMemberUpdate, EventType = typeof(DiscordGuildMemberUpdatedEvent))]
[GatewayEvent(EventName = GuildMembersChunk, EventType = typeof(DiscordGuildMembersChunkEvent))]
[GatewayEvent(EventName = GuildRoleCreate, EventType = typeof(DiscordGuildRoleCreatedEvent))]
[GatewayEvent(EventName = GuildRoleUpdate, EventType = typeof(DiscordGuildRoleUpdatedEvent))]
[GatewayEvent(EventName = GuildRoleDelete, EventType = typeof(DiscordGuildRoleDeletedEvent))]
[GatewayEvent(EventName = GuildScheduledEventCreate, EventType = typeof(DiscordScheduledEventCreatedEvent))]
[GatewayEvent(EventName = GuildScheduledEventUpdate, EventType = typeof(DiscordScheduledEventUpdatedEvent))]
[GatewayEvent(EventName = GuildScheduledEventDelete, EventType = typeof(DiscordScheduledEventDeletedEvent))]
[GatewayEvent(EventName = GuildScheduledEventUserAdd, EventType = typeof(DiscordScheduledEventUserAddedEvent))]
[GatewayEvent(EventName = GuildScheduledEventUserRemove, EventType = typeof(DiscordScheduledEventUserRemovedEvent))]
[GatewayEvent(EventName = IntegrationCreate, EventType = typeof(DiscordIntegrationCreatedEvent))]
[GatewayEvent(EventName = IntegrationUpdate, EventType = typeof(DiscordIntegrationUpdatedEvent))]
[GatewayEvent(EventName = IntegrationDelete, EventType = typeof(DiscordIntegrationDeletedEvent))]
[GatewayEvent(EventName = InteractionCreate, EventType = typeof(DiscordInteractionCreatedEvent))]
[GatewayEvent(EventName = InviteCreate, EventType = typeof(DiscordInviteCreatedEvent))]
[GatewayEvent(EventName = InviteDelete, EventType = typeof(DiscordInviteDeletedEvent))]
[GatewayEvent(EventName = MessageCreate, EventType = typeof(DiscordMessageCreatedEvent))]
[GatewayEvent(EventName = MessageUpdate, EventType = typeof(DiscordMessageUpdatedEvent))]
[GatewayEvent(EventName = MessageDelete, EventType = typeof(DiscordMessageDeletedEvent))]
[GatewayEvent(EventName = MessageDeleteBulk, EventType = typeof(DiscordMessagesBulkDeletedEvent))]
[GatewayEvent(EventName = MessageReactionAdd, EventType = typeof(DiscordMessageReactionAddedEvent))]
[GatewayEvent(EventName = MessageReactionRemove, EventType = typeof(DiscordMessageReactionRemovedEvent))]
[GatewayEvent(EventName = MessageReactionRemoveAll, EventType = typeof(DiscordAllMessageReactionsRemovedEvent))]
[GatewayEvent(EventName = MessageReactionRemoveEmoji, EventType = typeof(DiscordEmojiMessageReactionsRemovedEvent))]
[GatewayEvent(EventName = PresenceUpdate, EventType = typeof(DiscordPresenceUpdatedEvent))]
[GatewayEvent(EventName = StageInstanceCreate, EventType = typeof(DiscordStageInstanceCreatedEvent))]
[GatewayEvent(EventName = StageInstanceDelete, EventType = typeof(DiscordStageInstanceDeletedEvent))]
[GatewayEvent(EventName = StageInstanceUpdate, EventType = typeof(DiscordStageInstanceUpdatedEvent))]
[GatewayEvent(EventName = TypingStart, EventType = typeof(DiscordTypingStartedEvent))]
[GatewayEvent(EventName = UserUpdate, EventType = typeof(DiscordUserUpdatedEvent))]
[GatewayEvent(EventName = VoiceStateUpdate, EventType = typeof(DiscordVoiceStateUpdatedEvent))]
[GatewayEvent(EventName = VoiceServerUpdate, EventType = typeof(DiscordVoiceServerUpdatedEvent))]
[GatewayEvent(EventName = WebhooksUpdate, EventType = typeof(DiscordWebhooksUpdatedEvent))]
#endregion
internal static partial class DispatchEvents
{
	#region Events
	public const String Ready = "READY";
	public const String ApplicationCommandPermissionsUpdate = "APPLICATION_COMMAND_PERMISSIONS_UPDATE";
	public const String AutoModerationRuleCreate = "AUTO_MODERATION_RULE_CREATE";
	public const String AutoModerationRuleUpdate = "AUTO_MODERATION_RULE_UPDATE";
	public const String AutoModerationRuleDelete = "AUTO_MODERATION_RULE_DELETE";
	public const String AutoModerationActionExecution = "AUTO_MODERATION_ACTION_EXECUTION";
	public const String ChannelCreate = "CHANNEL_CREATE";
	public const String ChannelUpdate = "CHANNEL_UPDATE";
	public const String ChannelDelete = "CHANNEL_DELETE";
	public const String ChannelPinsUpdate = "CHANNEL_PINS_UPDATE";
	public const String ThreadCreate = "THREAD_CREATE";
	public const String ThreadUpdate = "THREAD_UPDATE";
	public const String ThreadDelete = "THREAD_DELETE";
	public const String ThreadListSync = "THREAD_LIST_SYNC";
	public const String ThreadMemberUpdate = "THREAD_MEMBER_UPDATE";
	public const String ThreadMembersUpdate = "THREAD_MEMBERS_UPDATE";
	public const String GuildCreate = "GUILD_CREATE";
	public const String GuildUpdate = "GUILD_UPDATE";
	public const String GuildDelete = "GUILD_DELETE";
	public const String GuildAuditLogEntryCreate = "GUILD_AUDIT_LOG_ENTRY_CREATE";
	public const String GuildBanAdd = "GUILD_BAN_ADD";
	public const String GuildBanRemove = "GUILD_BAN_REMOVE";
	public const String GuildEmojisUpdate = "GUILD_EMOJIS_UPDATE";
	public const String GuildStickersUpdate = "GUILD_STICKERS_UPDATE";
	public const String GuildIntegrationsUpdate = "GUILD_INTEGRATIONS_UPDATE";
	public const String GuildMemberAdd = "GUILD_MEMBER_ADD";
	public const String GuildMemberRemove = "GUILD_MEMBER_REMOVE";
	public const String GuildMemberUpdate = "GUILD_MEMBER_UPDATE";
	public const String GuildMembersChunk = "GUILD_MEMBERS_CHUNK";
	public const String GuildRoleCreate = "GUILD_ROLE_CREATE";
	public const String GuildRoleUpdate = "GUILD_ROLE_UPDATE";
	public const String GuildRoleDelete = "GUILD_ROLE_DELETE";
	public const String GuildScheduledEventCreate = "GUILD_SCHEDULED_EVENT_CREATE";
	public const String GuildScheduledEventUpdate = "GUILD_SCHEDULED_EVENT_UPDATE";
	public const String GuildScheduledEventDelete = "GUILD_SCHEDULED_EVENT_DELETE";
	public const String GuildScheduledEventUserAdd = "GUILD_SCHEDULED_EVENT_USER_ADD";
	public const String GuildScheduledEventUserRemove = "GUILD_SCHEDULED_EVENT_USER_REMOVE";
	public const String IntegrationCreate = "INTEGRATION_CREATE";
	public const String IntegrationUpdate = "INTEGRATION_UPDATE";
	public const String IntegrationDelete = "INTEGRATION_DELETE";
	public const String InteractionCreate = "INTERACTION_CREATE";
	public const String InviteCreate = "INVITE_CREATE";
	public const String InviteDelete = "INVITE_DELETE";
	public const String MessageCreate = "MESSAGE_CREATE";
	public const String MessageUpdate = "MESSAGE_UPDATE";
	public const String MessageDelete = "MESSAGE_DELETE";
	public const String MessageDeleteBulk = "MESSAGE_DELETE_BULK";
	public const String MessageReactionAdd = "MESSAGE_REACTION_ADD";
	public const String MessageReactionRemove = "MESSAGE_REACTION_REMOVE";
	public const String MessageReactionRemoveAll = "MESSAGE_REACTION_REMOVE_ALL";
	public const String MessageReactionRemoveEmoji = "MESSAGE_REACTION_REMOVE_EMOJI";
	public const String PresenceUpdate = "PRESENCE_UPDATE";
	public const String StageInstanceCreate = "STAGE_INSTANCE_CREATE";
	public const String StageInstanceDelete = "STAGE_INSTANCE_DELETE";
	public const String StageInstanceUpdate = "STAGE_INSTANCE_UPDATE";
	public const String TypingStart = "TYPING_START";
	public const String UserUpdate = "USER_UPDATE";
	public const String VoiceStateUpdate = "VOICE_STATE_UPDATE";
	public const String VoiceServerUpdate = "VOICE_SERVER_UPDATE";
	public const String WebhooksUpdate = "WEBHOOKS_UPDATE";
	#endregion

	public static IDiscordGatewayEvent Deserialize(String eventName, JsonElement element)
	{
		return eventName switch
		{
			Ready => deserializeReady(element),
			ApplicationCommandPermissionsUpdate => deserializeApplicationCommandPermissionsUpdate(element),
			AutoModerationRuleCreate => deserializeAutoModerationRuleCreate(element),
			AutoModerationRuleUpdate => deserializeAutoModerationRuleUpdate(element),
			AutoModerationRuleDelete => deserializeAutoModerationRuleDelete(element),
			AutoModerationActionExecution => deserializeAutoModerationActionExecution(element),
			ChannelCreate => deserializeChannelCreate(element),
			ChannelUpdate => deserializeChannelUpdate(element),
			ChannelDelete => deserializeChannelDelete(element),
			ChannelPinsUpdate => deserializeChannelPinsUpdate(element),
			ThreadCreate => deserializeThreadCreate(element),
			ThreadUpdate => deserializeThreadUpdate(element),
			ThreadDelete => deserializeThreadDelete(element),
			ThreadListSync => deserializeThreadListSync(element),
			ThreadMemberUpdate => deserializeThreadMemberUpdate(element),
			ThreadMembersUpdate => deserializeThreadMembersUpdate(element),
			GuildCreate => deserializeGuildCreate(element),
			GuildUpdate => deserializeGuildUpdate(element),
			GuildDelete => deserializeGuildDelete(element),
			GuildAuditLogEntryCreate => deserializeGuildAuditLogEntryCreate(element),
			GuildBanAdd => deserializeGuildBanAdd(element),
			GuildBanRemove => deserializeGuildBanRemove(element),
			GuildEmojisUpdate => deserializeGuildEmojisUpdate(element),
			GuildIntegrationsUpdate => deserializeGuildIntegrationsUpdate(element),
			GuildMemberAdd => deserializeGuildMemberAdd(element),
			GuildMemberRemove => deserializeGuildMemberRemove(element),
			GuildMemberUpdate => deserializeGuildMemberUpdate(element),
			GuildMembersChunk => deserializeGuildMembersChunk(element),
			GuildRoleCreate => deserializeGuildRoleCreate(element),
			GuildRoleUpdate => deserializeGuildRoleUpdate(element),
			GuildRoleDelete => deserializeGuildRoleDelete(element),
			GuildScheduledEventCreate => deserializeGuildScheduledEventCreate(element),
			GuildScheduledEventUpdate => deserializeGuildScheduledEventUpdate(element),
			GuildScheduledEventDelete => deserializeGuildScheduledEventDelete(element),
			GuildScheduledEventUserAdd => deserializeGuildScheduledEventUserAdd(element),
			GuildScheduledEventUserRemove => deserializeGuildScheduledEventUserRemove(element),
			IntegrationCreate => deserializeIntegrationCreate(element),
			IntegrationUpdate => deserializeIntegrationUpdate(element),
			IntegrationDelete => deserializeIntegrationDelete(element),
			InteractionCreate => deserializeInteractionCreate(element),
			InviteCreate => deserializeInviteCreate(element),
			InviteDelete => deserializeInviteDelete(element),
			MessageCreate => deserializeMessageCreate(element),
			MessageUpdate => deserializeMessageUpdate(element),
			MessageDelete => deserializeMessageDelete(element),
			MessageDeleteBulk => deserializeMessageDeleteBulk(element),
			MessageReactionAdd => deserializeMessageReactionAdd(element),
			MessageReactionRemove => deserializeMessageReactionRemove(element),
			MessageReactionRemoveAll => deserializeMessageReactionRemoveAll(element),
			MessageReactionRemoveEmoji => deserializeMessageReactionRemoveEmoji(element),
			PresenceUpdate => deserializePresenceUpdate(element),
			StageInstanceCreate => deserializeStageInstanceCreate(element),
			StageInstanceDelete => deserializeStageInstanceDelete(element),
			StageInstanceUpdate => deserializeStageInstanceUpdate(element),
			TypingStart => deserializeTypingStart(element),
			UserUpdate => deserializeUserUpdate(element),
			VoiceStateUpdate => deserializeVoiceStateUpdate(element),
			VoiceServerUpdate => deserializeVoiceServerUpdate(element),
			WebhooksUpdate => deserializeWebhooksUpdate(element),
			_ => new DiscordUnknownDispatchEvent()
			{
				EventName = eventName,
				Sequence = element.GetProperty("s").GetInt32(),
				Opcode = (DiscordGatewayOpcode)element.GetProperty("op").GetInt32(),
				Data = element.GetProperty("d")
			}
		};
	}
}
