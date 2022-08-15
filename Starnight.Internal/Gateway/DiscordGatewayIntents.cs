namespace Starnight.Internal.Gateway;

using System;

/** <summary>
* Represents a collection of gateway flags. Each of these intents represents a collection of events.
* If you do not request a specific intent, no events of that collection will be sent to you.<br/>
* The full list of events associated with each intent can be found at
* <seealso href="https://discord.com/developers/docs/topics/gateway#gateway-intents"/> and is documented
* for each enum member.
* </summary>
* <remarks>
* <see cref="DiscordGatewayIntents"/>, by design, does not provide an "all intents" or an 
* "all unprivileged intents" flag. This is, in part, to encourage more conscious design of gateway interaction 
* and to lessen unnecessary load, as well as Starnight.Internal being a direct API wrapper rather than
* a convenience library. <br/><br/>
* 
* The <see cref="MessageContent"/> intent does not actually cover any events - it controls what data is sent
* along with events that contain message content fields, that is: <br/>
* - <c>content</c> <br/>
* - <c>attachments</c> <br/>
* - <c>embeds</c> <br/>
* - <c>components</c> <br/> <br/>
* 
* Additionally, it is noteworthy that <see cref="GuildPresences"/>, <see cref="GuildMembers"/> and 
* <see cref="MessageContent"/> are privileged intents: that is, they must be manually enabled from the developer 
* panel before they can be requested to connect to the gateway, and if your bot is in over 100 guilds, they must
* be requested from and approved by Discord. <br/> <br/>
* 
* Furthermore, some events may be sent to you regardless of intents. These are all events not mentioned here,
* as well as GuildMemberUpdated, for the current user only. <br/><br/>
* 
* Lastly, the ThreadMembersUpdated, GuildCreated and RequestGuildMembers events/commands are uniquely affected
* by intents and may return varying data based on selected intents. These quirks are documented above their
* respective intent flag, as well as at the intent.
* </remarks> */
[Flags]
public enum DiscordGatewayIntents
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
	* It should be noted that ThreadMembersUpdated, with this intent, only contains updated for the current user, that is,
	* the connected bot. To receive these updates for all thread members, additionally request the <see cref="GuildMembers"/>
	* gateway intent.
	* </remarks> */
	Guilds = 1 << 0,

	/** <summary>
	* Covers guild member events. This is a privileged intent, meaning it has to be explicitly enabled in the developer
	* portal as well as enabled in your gateway connection.<br/>
	* The following events are covered by this intent:
	* </summary>
	* <remarks>
	* - GuildMemberAdded <br/>
	* - GuildMemberUpdated <br/>
	* - GuildMemberRemoved <br/>
	* - ThreadMembersUpdated <br/> <br/>
	* 
	* It should be noted that ThreadMembersUpdated may be sent to you even if the intent is not registered, however,
	* in these cases it will only be sent for <i>current user</i> thread member updates. With this intent, it will be sent
	* for all thread member updates. See also the comment on <seealso cref="Guilds"/>.
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
	* Note that these events may still be sent to you from direct messages, this intent only controls message reaction
	* events as dispatched from within a guild.
	* </remarks> */
	GuildMessageReactions = 1 << 10,

	/** <summary>
	* Covers a single event, TypingStarted, as dispatched from within a guild. Note that you will still receive the
	* event from direct messages.
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
	* Note that these events may still be sent to you from within a guild, this intent only controls message reaction
	* events as dispatched from direct messages.
	* </remarks> */
	DirectMessageReactions = 1 << 13,

	/** <summary>
	* Covers a single event, TypingStarted, as dispatched from within direct messages. Note that you will still receive the
	* event from within a guild.
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
	AutoModerationExecution = 1 << 21
}
