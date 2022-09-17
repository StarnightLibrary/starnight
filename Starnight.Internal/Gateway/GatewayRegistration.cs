namespace Starnight.Internal.Gateway;

using Microsoft.Extensions.DependencyInjection;

using Starnight.Internal.Gateway.Responders;
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
	public static IServiceCollection RegisterStarnightGateway(this IServiceCollection services)
	{
		_ = services.AddSingleton<DiscordGatewayRestResource>();

		_ = services.AddSingleton<ResponderCollection>();

		_ = services.AddSingleton<TransportService>()
			.AddSingleton<IOutboundGatewayService, OutboundGatewayService>()
			.AddSingleton<IInboundGatewayService, InboundGatewayService>()
			.AddSingleton<DiscordGatewayClient>();

		return services;
	}
}
