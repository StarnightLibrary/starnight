namespace Starnight.Internal;

using System;

/// <summary>
/// Holds a bunch of constants for the library.
/// </summary>
public static class DiscordApiConstants
{
	#region endpoints
	public const String ApiVersion = "10";
	public const String BaseUri = "https://discord.com/api/v" + ApiVersion;

	public const String Ack = "ack";
	public const String Active = "active";
	public const String Applications = "applications";
	public const String Archived = "archived";
	public const String Assets = "assets";
	public const String AuditLogs = "audit-logs";
	public const String Auth = "auth";
	public const String AutoModeration = "auto-moderation";
	public const String Avatars = "avatars";
	public const String Bans = "bans";
	public const String Bot = "bot";
	public const String BulkDelete = "bulk-delete";
	public const String Callback = "callback";
	public const String Channels = "channels";
	public const String Commands = "commands";
	public const String Connections = "connections";
	public const String Crosspost = "crosspost";
	public const String Emojis = "emojis";
	public const String ScheduledEvents = "scheduled-events";
	public const String Followers = "followers";
	public const String Gateway = "gateway";
	public const String Github = "github";
	public const String Guilds = "guilds";
	public const String Icons = "icons";
	public const String Integrations = "integrations";
	public const String Interactions = "interactions";
	public const String Invites = "invites";
	public const String Login = "login";
	public const String Me = "@me";
	public const String Member = "member";
	public const String Members = "members";
	public const String MemberVerification = "member-verification";
	public const String Messages = "messages";
	public const String Metadata = "metadata";
	public const String MFA = "mfa";
	public const String OAuth2 = "oauth2";
	public const String Onboarding = "onboarding";
	public const String Original = "original";
	public const String Permissions = "permissions";
	public const String Pins = "pins";
	public const String Preview = "preview";
	public const String Private = "private";
	public const String Prune = "prune";
	public const String Public = "public";
	public const String Reactions = "reactions";
	public const String Recipients = "recipients";
	public const String Regions = "regions";
	public const String RoleConnections = "role-connections";
	public const String Roles = "roles";
	public const String Rules = "rules";
	public const String Search = "search";
	public const String Slack = "slack";
	public const String StageInstances = "stage-instances";
	public const String StickerPacks = "sticker-packs";
	public const String Stickers = "stickers";
	public const String Sync = "sync";
	public const String Templates = "templates";
	public const String Threads = "threads";
	public const String ThreadMembers = "thread-members";
	public const String Typing = "typing";
	public const String Users = "users";
	public const String VanityUrl = "vanity-url";
	public const String Voice = "voice";
	public const String VoiceStates = "voice-states";
	public const String Webhooks = "webhooks";
	public const String WelcomeScreen = "welcome-screen";
	public const String Widget = "widget";
	public const String WidgetJson = "widget.json";
	public const String WidgetPng = "widget.png";
	#endregion

	#region route parameters
	public const String AppId = ":app_id";
	public const String AutoModerationRuleId = ":auto_moderation_rule_id";
	public const String ChannelId = ":channel_id";
	public const String CommandId = ":command_id";
	public const String Emoji = ":emoji";
	public const String EmojiId = ":emoji_id";
	public const String GuildId = ":guild_id";
	public const String IntegrationId = ":integration_id";
	public const String InteractionId = ":interaction_id";
	public const String InteractionToken = ":interaction_token";
	public const String InviteCode = ":invite_code";
	public const String MessageId = ":message_id";
	public const String OverwriteId = ":overwrite_id";
	public const String RoleId = ":role_id";
	public const String ScheduledEventId = ":scheduled_event_id";
	public const String StickerId = ":sticker_id";
	public const String TemplateCode = ":template_code";
	public const String UserId = ":user_id";
	public const String WebhookToken = ":webhook_token";
	#endregion
}
