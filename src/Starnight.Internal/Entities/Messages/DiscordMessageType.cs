namespace Starnight.Entities;

/// <summary>
/// Represents the type of a message.
/// </summary>
/// <remarks>
/// Documentation for the different fields is missing: where the names are not self-explanatory you are on your own.
/// Feel free to PR documentation. Additionally, instead of attempting to use more sane names than discord uses,
/// these names are only changed inasmuch from the discord docs as to conform with the C# naming rules.
/// </remarks>
public enum DiscordMessageType
{
	Default,
	RecipientAdd,
	RecipientRemove,
	Call,
	ChannelNameChange,
	ChannelIconChange,
	ChannelPinnedMessage,
	GuildMemberJoin,
	GuildBoost,
	GuildBoostTier1,
	GuildBoostTier2,
	GuildBoostTier3,
	ChannelFollowAdd,
	GuildDiscoveryDisqualified = 14,
	GuildDiscoveryRequalified,
	GuildDiscoveryGracePeriodInitialWarning,
	GuildDiscoveryGracePeriodFinalWarning,
	ThreadCreated,
	Reply,
	ChatInputCommand,
	ThreadStarterMessage,
	GuildInviteReminder,
	ContextMenuCommand,
	AutoModerationAction,
	RoleSubscriptionPurchase,
	InteractionPremiumUpsell,
	GuildApplicationPremiumSubscription = 32
}
