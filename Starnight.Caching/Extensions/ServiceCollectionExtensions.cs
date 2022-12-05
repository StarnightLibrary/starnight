namespace Starnight.Caching.Extensions;

using Microsoft.Extensions.DependencyInjection;

using Starnight.Caching.Responders;
using Starnight.Caching.Services;
using Starnight.Caching.Shims;
using Starnight.Internal.Gateway.Responders;
using Starnight.Internal.Rest.Resources;
using Starnight.Internal.Utils;

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

		_ = services.AddResponder
		(
			typeof(PreChannelResponder),
			ResponderPhase.PreEvent
		);

		return services;
	}
}
