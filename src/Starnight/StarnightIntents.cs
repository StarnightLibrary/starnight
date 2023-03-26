namespace Starnight;

using System;

/** <summary>
 * Represents a collection of flags, so-called intents, that control which events and what data
 * your bot will receive from the gateway.<br/>
 * 
 * Generally, each intent commands a group of events. If you do not request an intent, you will
 * not receive the events or data associated with it. Certain intents also control <i>where</i>
 * you will receive events from, or what data is sent with the events you receive. This behaviour
 * is fully documented on the flags in question.<br/>
 * 
 * In case this documentation is outdated or incomplete, the full documentation is available at
 * <seealso href="https://discord.com/developers/docs/topics/gateway#gateway-intents"/>
 * </summary>
 * <remarks>
 * There are certain members of this enumeration to look out for:<br/>
 * On one hand, there are Discord's privileged intents, which are intents that you need to specifically
 * enable or apply for in the developer panel, depending on the size of your bot. Those currently are:<br/>
 * 
 * - <c>MessageContent</c><br/>
 * - <c>GuildPresences</c><br/>
 * - <c>GuildMembers</c><br/><br/>
 * 
 * On the other, Starnight synthesizes some combination fields that represent not one but multiple flags.
 * See the documentation on each of them for guidance and details. These currently are:<br/>
 * 
 * - <c>AllUnprivileged</c><br/>
 * - <c>All</c><br/><br/>
 * 
 * Furthermore, some events may be sent to you regardless of intents. These are all events not mentioned
 * here, as well as <c>GuildMemberUpdated</c> for the current (bot) user.
 * </remarks> */
// Generally, all enums go in Starnight.Shared, but this specific enum is treated differently to allow us
// to provide convenience hooks for certain combinations of intents. This is also an useful capability in
// the event that updating the public event system takes time to update to new events/intents.
[Flags]
public enum StarnightIntents
{
	/** <summary>
	 * Covers most generic guild-related events:
	 * </summary>
	 * <remarks>
	 * - GuildCreated <br/>
	 * - GuildUpdated <br/>
	 * - GuildDeleted <br/>
	 * - GuildRoleCreated <br/>
	 * - GuildRoleUpdated <br/>
	 * - GuildRoleDeleted <br/>
	 * - ChannelCreated <br/>
	 * - ChannelUpdated <br/>
	 * - ChannelDeleted <br/>
	 * - ChannelPinsUpdated <br/>
	 * - ThreadCreated <br/>
	 * - ThreadUpdated <br/>
	 * - ThreadDeleted <br/>
	 * - ThreadListSync <br/>
	 * - ThreadMemberUpdated <br/>
	 * - ThreadMembersUpdated <br/>
	 * - StageInstanceCreated <br/>
	 * - StageInstanceUpdated <br/>
	 * - StageInstanceDeleted <br/> <br/>
	 * 
	 * It should be noted that ThreadMembersUpdated, with this intent, only contains updated for the current user,
	 * that is, the connected bot. To receive these updates for all thread members, additionally request the 
	 * <see cref="GuildMembers"/> gateway intent.
	 * </remarks> */
	Guilds = 1 << 0,

	/** <summary>
	 * Covers guild member events. This is a privileged intent, meaning it has to be explicitly enabled in the 
	 * developer portal as well as enabled in your gateway connection.<br/>
	 * The following events are covered by this intent:
	 * </summary>
	 * <remarks>
	 * - GuildMemberAdded <br/>
	 * - GuildMemberUpdated <br/>
	 * - GuildMemberRemoved <br/>
	 * - ThreadMembersUpdated <br/> <br/>
	 * 
	 * It should be noted that ThreadMembersUpdated may be sent to you even if the intent is not registered, 
	 * however, in these cases it will only be sent for <i>current user</i> thread member updates. With this 
	 * intent, it will be sent for all thread member updates. See also the comment on <seealso cref="Guilds"/>.
	 * </remarks> */
	GuildMembers = 1 << 1,

	/** <summary>
	 * Covers ban-related events:
	 * </summary>
	 * <remarks>
	 * - GuildBanAdded <br/>
	 * - GuildBanRemoved <br/>
	 * </remarks> */
	GuildBans = 1 << 2,

	/** <summary>
	 * Covers emoji/sticker-related events:
	 * </summary>
	 * <remarks>
	 * - GuildEmojisUpdate <br/>
	 * - GuildStickersUpdate <br/>
	 * </remarks> */
	GuildEmojisAndStickers = 1 << 3,

	/** <summary>
	 * Covers integration-related events:
	 * </summary>
	 * <remarks>
	 * - GuildIntegrationsUpdated <br/>
	 * - IntegrationCreated <br/>
	 * - IntegrationUpdated <br/>
	 * - IntegrationDeleted <br/>
	 * </remarks> */
	GuildIntegrations = 1 << 4,

	/** <summary>
	 * Covers a single event; WebhooksUpdated.
	 * </summary> */
	GuildWebhooks = 1 << 5,

	/** <summary>
	 * Covers invite-related events, though notably not invite uses:
	 * </summary>
	 * <remarks>
	 * - InviteCreated <br/>
	 * - InviteDeleted <br/>
	 * </remarks> */
	GuildInvites = 1 << 6,

	/** <summary>
	 * Covers a single event, VoiceStateUpdated.
	 * </summary> */
	GuildVoiceStates = 1 << 7,

	/** <summary>
	 * Covers a single event, PresenceUpdated. This is a privileged intent.
	 * </summary> */
	GuildPresences = 1 << 8,

	/** <summary>
	 * Covers guild messaging-related events:
	 * </summary>
	 * <remarks>
	 * - MessageCreated <br/>
	 * - MessageUpdated <br/>
	 * - MessageDeleted <br/>
	 * - MessagesBulkDeleted <br/> <br/>
	 * Note that the first three of these events may still be sent to you if in a direct message, this intent
	 * only controls message events as dispatched from within a guild.
	 * </remarks> */
	GuildMessages = 1 << 9,

	/** <summary>
	 * Covers guild message reaction related events:
	 * </summary>
	 * <remarks>
	 * - MessageReactionAdded <br/>
	 * - MessageReactionRemoved <br/>
	 * - MessageReactionAllRemoved <br/>
	 * - MessageReactionEmojiRemoved <br/> <br/>
	 * Note that these events may still be sent to you from direct messages, this intent only controls message 
	 * reaction events as dispatched from within a guild.
	 * </remarks> */
	GuildMessageReactions = 1 << 10,

	/** <summary>
	 * Covers a single event, TypingStarted, as dispatched from within a guild. Note that you will still receive 
	 * the event from direct messages.
	 * </summary> */
	GuildMessageTyping = 1 << 11,

	/** <summary>
	 * Covers messaging-related events from direct messages:
	 * </summary>
	 * <remarks>
	 * - MessageCreated <br/>
	 * - MessageUpdated <br/>
	 * - MessageDeleted <br/>
	 * - MessagesBulkDeleted <br/> <br/>
	 * Note that the first three of these events may still be sent to you from within a guild, this intent
	 * only controls message events as dispatched from direct messages.
	 * </remarks> */
	DirectMessages = 1 << 12,

	/** <summary>
	 * Covers direct message reaction related events:
	 * </summary>
	 * <remarks>
	 * - MessageReactionAdded <br/>
	 * - MessageReactionRemoved <br/>
	 * - MessageReactionAllRemoved <br/>
	 * - MessageReactionEmojiRemoved <br/> <br/>
	 * Note that these events may still be sent to you from within a guild, this intent only controls message 
	 * reaction events as dispatched from direct messages.
	 * </remarks> */
	DirectMessageReactions = 1 << 13,

	/** <summary>
	 * Covers a single event, TypingStarted, as dispatched from within direct messages. Note that you will still 
	 * receive the event from within a guild.
	 * </summary> */
	DirectMessageTyping = 1 << 14,

	/** <summary>
	 * This intent does not directly cover any events. Instead, it influences the data sent to you in other events
	 * controlled by other intents.
	 * </summary> */
	MessageContent = 1 << 15,

	/** <summary>
	 * Covers gateway events related to scheduled events:
	 * </summary>
	 * <remarks>
	 * - GuildScheduledEventCreated <br/>
	 * - GuildScheduledEventUpdated <br/>
	 * - GuildScheduledEventDeleted <br/>
	 * - GuildScheduledEventUserAdded <br/>
	 * - GuildScheduledEventUserRemoved <br/>
	 * </remarks> */
	GuildScheduledEvents = 1 << 16,

	/** <summary>
	 * Covers gateway events related to configuring the built-in auto-moderator:
	 * </summary>
	 * <remarks>
	 * - AutoModerationRuleCreated <br/>
	 * - AutoModerationRuleUpdated <br/>
	 * - AutoModerationRuleDeleted <br/>
	 * </remarks> */
	AutoModerationConfiguration = 1 << 20, // discord is having trouble with counting again

	/** <summary>
	 * Covers a single event, AutoModerationActionExecuted.
	 * </summary> */
	AutoModerationExecution = 1 << 21,

	// synthesized members

	/** <summary>
	 * Covers all unprivileged intents currently present, that is:<br/>
	 * - <seealso cref="Guilds"/><br/>
	 * - <seealso cref="GuildBans"/><br/>
	 * - <seealso cref="GuildEmojisAndStickers"/><br/>
	 * - <seealso cref="GuildIntegrations"/><br/>
	 * - <seealso cref="GuildWebhooks"/><br/>
	 * - <seealso cref="GuildInvites"/><br/>
	 * - <seealso cref="GuildVoiceStates"/><br/>
	 * - <seealso cref="GuildMessages"/><br/>
	 * - <seealso cref="GuildMessageReactions"/><br/>
	 * - <seealso cref="GuildMessageTyping"/><br/>
	 * - <seealso cref="DirectMessages"/><br/>
	 * - <seealso cref="DirectMessageReactions"/><br/>
	 * - <seealso cref="DirectMessageTyping"/><br/>
	 * - <seealso cref="GuildScheduledEvents"/><br/>
	 * - <seealso cref="AutoModerationConfiguration"/><br/>
	 * - <seealso cref="AutoModerationExecution"/><br/>
	 * </summary>
	 * <remarks>
	 * <b>It is <i>not</i> recommended to use this member outside of development scenarios. It offers no
	 * fine-grained control over events, and every event you receive but do not intend on handling under
	 * any circumstances is wasteful. In larger guilds or large amounts of guilds, performance may suffer.
	 * You are implored to instead explicitly specify each intent you want for your production software.</b>
	 * </remarks> */
	AllUnprivileged = Guilds
		| GuildBans
		| GuildEmojisAndStickers
		| GuildIntegrations
		| GuildWebhooks
		| GuildInvites
		| GuildVoiceStates
		| GuildMessages
		| GuildMessageReactions
		| GuildMessageTyping
		| DirectMessages
		| DirectMessageReactions
		| DirectMessageTyping
		| GuildScheduledEvents
		| AutoModerationConfiguration
		| AutoModerationExecution,

	/** <summary>
	 * Covers all intents currently present, that is:<br/>
	 * - <seealso cref="Guilds"/><br/>
	 * - <seealso cref="GuildBans"/><br/>
	 * - <seealso cref="GuildEmojisAndStickers"/><br/>
	 * - <seealso cref="GuildIntegrations"/><br/>
	 * - <seealso cref="GuildWebhooks"/><br/>
	 * - <seealso cref="GuildInvites"/><br/>
	 * - <seealso cref="GuildVoiceStates"/><br/>
	 * - <seealso cref="GuildMessages"/><br/>
	 * - <seealso cref="GuildMessageReactions"/><br/>
	 * - <seealso cref="GuildMessageTyping"/><br/>
	 * - <seealso cref="DirectMessages"/><br/>
	 * - <seealso cref="DirectMessageReactions"/><br/>
	 * - <seealso cref="DirectMessageTyping"/><br/>
	 * - <seealso cref="GuildScheduledEvents"/><br/>
	 * - <seealso cref="AutoModerationConfiguration"/><br/>
	 * - <seealso cref="AutoModerationExecution"/><br/>
	 * - <seealso cref="GuildMembers"/><br/>
	 * - <seealso cref="GuildPresences"/><br/>
	 * - <seealso cref="MessageContent"/><br/>
	 * </summary>
	 * <remarks>
	 * <b>It is <i>not</i> recommended to use this member outside of development scenarios. It offers no
	 * fine-grained control over events, and every event you receive but do not intend on handling under
	 * any circumstances is wasteful. In larger guilds or large amounts of guilds, performance may suffer.
	 * You are implored to instead explicitly specify each intent you want for your production software.
	 * Furthermore, if this member is updated to include a future privileged intent, you will need to enable
	 * it in the dashboard before your bot will be able to run.</b>
	 * </remarks> */
	All = AllUnprivileged
		| GuildMembers
		| GuildPresences
		| MessageContent,

	/** <summary>
	 * Represents no events. If this is selected you will only receive events not covered by intents.
	 * </summary> */
	None = 0
}
