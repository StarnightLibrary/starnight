namespace Starnight.Caching.Extensions;

using Microsoft.Extensions.DependencyInjection;

using Starnight.Caching.Listeners;
using Starnight.Caching.Services;
using Starnight.Caching.Shims;
using Starnight.Internal.Extensions;
using Starnight.Internal.Gateway.Listeners;
using Starnight.Internal.Rest.Resources;

/// <summary>
/// Contains an extension method to register caching
/// </summary>
public static class ServiceCollectionExtensions
{
	/// <summary>
	/// Registers caching shims over the current rest resources.
	/// </summary>
	/// <param name="services">The service collection to register into.</param>
	/// <returns>The service collection for chaining.</returns>
	public static IServiceCollection AddRestCaching
	(
		this IServiceCollection services
	)
	{
		_ = services.AddSingleton<IStarnightCacheService, StarnightCacheService>();

		_ = services.Decorate<IDiscordApplicationCommandsRestResource, CachingApplicationCommandsRestResource>()
			.Decorate<IDiscordChannelRestResource, CachingChannelRestResource>()
			.Decorate<IDiscordEmojiRestResource, CachingEmojiRestResource>()
			.Decorate<IDiscordGuildRestResource, CachingGuildRestResource>();

		_ = services.AddListener
		(
			typeof(PreEventListener),
			ListenerPhase.PreEvent
		)
		.AddListener
		(
			typeof(PostEventListener),
			ListenerPhase.PostEvent
		);

		return services;
	}
}
