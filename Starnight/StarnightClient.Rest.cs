namespace Starnight;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

using Polly;
using Polly.Contrib.WaitAndRetry;

using Starnight.Exceptions;
using Starnight.Internal;
using Starnight.Internal.Rest;
using Starnight.Internal.Rest.Resources;

public partial class StarnightClient
{
	private static readonly TimeSpan __one_second = TimeSpan.FromSeconds(1);

	private void addRestClient(StarnightClientOptions options)
	{
		PollyRateLimitPolicy ratelimiter = new();
		IEnumerable<TimeSpan> retryDelay = Backoff.DecorrelatedJitterBackoffV2(TimeSpan.FromSeconds(0.5), 10);

		_ = this.ServiceCollection
			.AddHttpClient<RestClient>()
			.ConfigureHttpClient((services, client) =>
			{
				client.BaseAddress = new(DiscordApiConstants.BaseUri);
				client.DefaultRequestHeaders.UserAgent.Add(new(StarnightConstants.UserAgentHeader, StarnightConstants.Version));
				client.DefaultRequestHeaders.Authorization = new("Bot", options.Token);
			})
			.AddTransientHttpErrorPolicy(policy => policy.WaitAndRetryAsync(retryDelay).WrapAsync(ratelimiter))
			.AddPolicyHandler
			(
				Policy.HandleResult<HttpResponseMessage>(result => result.StatusCode == HttpStatusCode.TooManyRequests)
					.WaitAndRetryAsync(1,
						(_, response, _) =>
						{
							HttpResponseMessage message = response.Result;

							return message.Headers.GetValues("X-RateLimit-Scope").SingleOrDefault() == "shared"
								? throw new StarnightSharedRatelimitHitException("Shared ratelimit hit, not retrying request.",
									"Polly request retry policy")
								: message == default
									? __one_second
									: message.Headers.RetryAfter == null || message.Headers.RetryAfter.Delta == null
										? __one_second
										: message.Headers.RetryAfter.Delta.Value;
						},
						(_, _, _, _) => Task.CompletedTask)
			);

		_ = this.ServiceCollection
			.AddSingleton<DiscordChannelRestResource>()
			.AddSingleton<DiscordGuildRestResource>();
	}
}
