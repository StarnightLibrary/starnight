namespace Starnight;

using System;

using Microsoft.Extensions.Logging;

using Starnight.Caching.Services;
using Starnight.Internal.Gateway;
using Starnight.Internal.Rest.Resources;

/// <summary>
/// The central, main client for any and all interactions with Discord.
/// </summary>
public partial class StarnightClient
{
	public IServiceProvider Services { get; internal set; }

	internal ILogger<StarnightClient> Logger { get; }
	internal DiscordGatewayClient GatewayClient { get; }
	internal IDiscordApplicationCommandsRestResource ApplicationCommandsRestResource { get; }
	internal IDiscordAuditLogRestResource AuditLogRestResource { get; }
	internal IDiscordAutoModerationRestResource AutoModerationRestResource { get; }
	internal IDiscordChannelRestResource ChannelRestResource { get; }
	internal IDiscordEmojiRestResource EmojiRestResource { get; }
	internal IDiscordGuildRestResource GuildRestResource { get; }
	internal IDiscordGuildTemplateRestResource GuildTemplateRestResource { get; }
	internal IDiscordInviteRestResource InviteRestResource { get; }
	internal IDiscordRoleConnectionRestResource RoleConnectionRestResource { get; }
	internal IDiscordScheduledEventRestResource ScheduledEventRestResource { get; }
	internal IDiscordStageInstanceRestResource StageInstanceRestResource { get; }
	internal IDiscordStickerRestResource StickerRestResource { get; }
	internal IDiscordUserRestResource UserRestResource { get; }
	internal IDiscordVoiceRestResource VoiceRestResource { get; }
	internal IDiscordWebhookRestResource WebhookRestResource { get; }

	internal IStarnightCacheService CacheService { get; }
}
