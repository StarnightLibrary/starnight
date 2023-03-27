namespace Starnight;

using System;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Starnight.Caching.Services;
using Starnight.Infrastructure.TransformationServices;
using Starnight.Internal.Gateway;
using Starnight.Internal.Rest.Resources;

public partial class StarnightClient
{
	public StarnightClient
	(
		IServiceProvider services,
		ILogger<StarnightClient> logger,
		DiscordGatewayClient gatewayClient,
		IDiscordApplicationCommandsRestResource applicationCommandsRestResource,
		IDiscordAuditLogRestResource auditLogRestResource,
		IDiscordAutoModerationRestResource autoModerationRestResource,
		IDiscordChannelRestResource channelRestResource,
		IDiscordEmojiRestResource emojiRestResource,
		IDiscordGuildRestResource guildRestResource,
		IDiscordGuildTemplateRestResource guildTemplateRestResource,
		IDiscordInviteRestResource inviteRestResource,
		IDiscordRoleConnectionRestResource roleConnectionRestResource,
		IDiscordScheduledEventRestResource scheduledEventRestResource,
		IDiscordStageInstanceRestResource stageInstanceRestResource,
		IDiscordStickerRestResource stickerRestResource,
		IDiscordUserRestResource userRestResource,
		IDiscordVoiceRestResource voiceRestResource,
		IDiscordWebhookRestResource webhookRestResource,
		IStarnightCacheService cacheService,
		ICollectionTransformerService transformer
	)
	{
		this.Services = services;
		this.Logger = logger;
		this.GatewayClient = gatewayClient;
		this.ApplicationCommandsRestResource = applicationCommandsRestResource;
		this.AuditLogRestResource = auditLogRestResource;
		this.AutoModerationRestResource = autoModerationRestResource;
		this.ChannelRestResource = channelRestResource;
		this.EmojiRestResource = emojiRestResource;
		this.GuildRestResource = guildRestResource;
		this.GuildTemplateRestResource = guildTemplateRestResource;
		this.InviteRestResource = inviteRestResource;
		this.RoleConnectionRestResource = roleConnectionRestResource;
		this.ScheduledEventRestResource = scheduledEventRestResource;
		this.StageInstanceRestResource = stageInstanceRestResource;
		this.StickerRestResource = stickerRestResource;
		this.UserRestResource = userRestResource;
		this.VoiceRestResource = voiceRestResource;
		this.WebhookRestResource = webhookRestResource;
		this.CacheService = cacheService;
		this.CollectionTransformer = transformer;
	}

	public static StarnightClient Create
	(
		StarnightClientOptions options
	)
	{
		StarnightClientBuilder builder = new StarnightClientBuilder
		(
			options.Services ?? new ServiceCollection()
		)
		.WithToken
		(
			options.Token
		)
		.AddIntents
		(
			options.Intents
		);

		if(options.AverageFirstRequestRetryDelay is not null)
		{
			_ = builder.WithAverageFirstRequestRetryDelay
			(
				options.AverageFirstRequestRetryDelay!.Value
			);
		}

		if(options.RetryCount is not null)
		{
			_ = builder.WithRetryCount
			(
				options.RetryCount!.Value
			);
		}

		if(options.RatelimitedRetryCount is not null)
		{
			_ = builder.WithRatelimitedRetryCount
			(
				options.RatelimitedRetryCount!.Value
			);
		}

		if(options.LargeGuildThreshold is not null)
		{
			_ = builder.WithLargeGuildThreshold
			(
				options.LargeGuildThreshold!.Value
			);
		}

		if(options.ShardId is not null && options.ShardCount is not null)
		{
			_ = builder.WithShardInformation
			(
				options.ShardId!.Value,
				options.ShardCount!.Value
			);
		}

		if(options.UseDefaultLogger)
		{
			_ = builder.AddDefaultLogger(options.MinimumLogLevel);
		}

		if(options.ZombieThreshold is not null)
		{
			_ = builder.WithZombieThreshold
			(
				options.ZombieThreshold!.Value
			);
		}

		return builder.Build();
	}
}
