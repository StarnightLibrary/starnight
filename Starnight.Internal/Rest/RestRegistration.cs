namespace Starnight.Internal.Rest;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

using Polly;
using Polly.Contrib.WaitAndRetry;

using Starnight.Internal;
using Starnight.Internal.Exceptions;
using Starnight.Internal.Rest.Resources;

/// <summary>
/// Contains an extension method on IServiceCollection to register Starnight's rest client
/// </summary>
public static class RestRegistration
{
	private static readonly TimeSpan __one_second = TimeSpan.FromSeconds(1);

	/// <summary>
	/// Registers the Starnight rest client into the given service collection.
	/// </summary>
	public static IServiceCollection AddStarnightRestClient(this IServiceCollection collection, RestClientOptions options)
	{
		PollyRateLimitPolicy ratelimiter = new();
		IEnumerable<TimeSpan> retryDelay = Backoff.DecorrelatedJitterBackoffV2(
			options.MedianFirstRequestRetryDelay, options.RetryCount);

		_ = collection
			.AddHttpClient<RestClient>()
			.ConfigureHttpClient((client) =>
			{
				client.BaseAddress = new(DiscordApiConstants.BaseUri);
				client.DefaultRequestHeaders.UserAgent.Add(new(StarnightConstants.UserAgentHeader, StarnightConstants.Version));
				client.DefaultRequestHeaders.Authorization = new("Bot", options.Token);
			})
			.AddTransientHttpErrorPolicy(policy => policy.WaitAndRetryAsync(retryDelay).WrapAsync(ratelimiter))
			.AddPolicyHandler
			(
				Policy.HandleResult<HttpResponseMessage>(result => result.StatusCode == HttpStatusCode.TooManyRequests)
					.WaitAndRetryAsync(options.RatelimitedRetryCount,
					(_, response, _) =>
					{
						HttpResponseMessage message = response.Result;

						return message.Headers.GetValues("X-RateLimit-Scope").SingleOrDefault() == "shared"
							? throw new StarnightSharedRatelimitHitException("Shared ratelimit hit, not retrying request.",
								"Polly request retry policy")
							: message == default
								? __one_second
								: message.Headers.RetryAfter is null || message.Headers.RetryAfter.Delta is null
									? __one_second
									: message.Headers.RetryAfter.Delta.Value;
					},
					(_, _, _, _) => Task.CompletedTask)
			);

		_ = collection
			.AddSingleton<DiscordChannelRestResource>()
			.AddSingleton<DiscordGuildRestResource>()
			.AddSingleton<DiscordApplicationCommandsRestResource>()
			.AddSingleton<DiscordAuditLogRestResource>()
			.AddSingleton<DiscordEmojiRestResource>()
			.AddSingleton<DiscordScheduledEventRestResource>()
			.AddSingleton<DiscordGuildTemplateRestResource>()
			.AddSingleton<DiscordInviteRestResource>()
			.AddSingleton<DiscordStageInstanceRestResource>()
			.AddSingleton<DiscordStickerRestResource>()
			.AddSingleton<DiscordUserRestResource>();

		return collection;
	}
}
