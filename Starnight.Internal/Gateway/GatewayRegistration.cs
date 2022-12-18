namespace Starnight.Internal.Gateway;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Starnight.Internal.Gateway.Listeners;
using Starnight.Internal.Gateway.Services;

/// <summary>
/// Contains an extension method for registering the Starnight gateway client.
/// </summary>
public static class GatewayRegistration
{
	/// <summary>
	/// Registers the Starnight gateway client.
	/// </summary>
	/// <param name="services">The service collection to register this client into.</param>
	/// <returns>The service collection, for chaining.</returns>
	public static IServiceCollection AddStarnightGateway(this IServiceCollection services)
	{
		_ = services.AddSingleton<DiscordGatewayRestResource>();

		_ = services.AddSingleton<TransportService>()
			.AddSingleton<IOutboundGatewayService, OutboundGatewayService>()
			.AddSingleton<IInboundGatewayService, InboundGatewayService>()
			.AddSingleton<DiscordGatewayClient>();

		return services;
	}

	/// <summary>
	/// Registers the Starnight gateway client.
	/// </summary>
	/// <param name="host">The host builder to register this client into.</param>
	/// <returns>The host builder, for chaining.</returns>
	public static IHostBuilder AddStarnightGateway(this IHostBuilder host)
	{
		return host.ConfigureServices
		(
			(context, services) =>
			{
				_ = services.AddSingleton<DiscordGatewayRestResource>();

				_ = services.AddSingleton
				(
					new ListenerCollection()
				);

				_ = services.AddSingleton<TransportService>()
					.AddSingleton<IOutboundGatewayService, OutboundGatewayService>()
					.AddSingleton<IInboundGatewayService, InboundGatewayService>();

				_ = services.AddHostedService<DiscordGatewayClient>();
			}
		);
	}
}
