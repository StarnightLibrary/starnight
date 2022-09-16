namespace Starnight.Internal.Gateway;

using Microsoft.Extensions.DependencyInjection;

using Starnight.Internal.Gateway.Services;

public static class GatewayRegistration
{
	public static IServiceCollection RegisterStarnightGateway(this IServiceCollection services)
	{
		_ = services.AddSingleton<DiscordGatewayRestResource>();

		_ = services.AddSingleton<TransportService>()
			.AddSingleton<IOutboundGatewayService, OutboundGatewayService>();

		return services;
	}
}
