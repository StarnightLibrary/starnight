namespace Starnight.Internal.Gateway.Services;

using System;
using System.Threading;
using System.Threading.Tasks;

using Polly;
using Polly.RateLimit;

using Starnight.Internal.Gateway.Events.Outbound;
using Starnight.Internal.Gateway.Payloads.Outbound;

/// <summary>
/// Contains logic for sending payloads to Discord.
/// </summary>
public class OutboundGatewayService : IOutboundGatewayService
{
	private readonly TransportService transportService;

	private readonly RateLimitPolicy policy;

	public OutboundGatewayService
	(
		TransportService transportService
	)
	{
		this.transportService = transportService;

		this.policy = Policy.RateLimit(120, TimeSpan.FromMinutes(1));
	}

	/// <inheritdoc/>
	public async ValueTask IdentifyAsync(IdentifyPayload payload)
	{
		DiscordIdentifyEvent @event = new()
		{
			Data = payload
		};

		await this.SendEventAsync(@event);
	}

	/// <inheritdoc/>
	public async ValueTask ResumeAsync(ResumePayload payload)
	{
		DiscordResumeEvent @event = new()
		{
			Data = payload
		};

		await this.SendEventAsync(@event);
	}

	/// <inheritdoc/>
	public async ValueTask SendHeartbeatAsync(Int32 lastSequence)
	{
		DiscordOutboundHeartbeatEvent @event = new()
		{
			Data = lastSequence,
			Opcode = DiscordGatewayOpcode.Heartbeat
		};

		await this.SendEventAsync(@event);
	}

	/// <inheritdoc/>
	public async ValueTask SendEventAsync(IDiscordGatewayEvent @event)
	{
		await this.policy.Execute
		(
			async () => await this.transportService.WriteAsync
			(
				@event,
				CancellationToken.None
			)
		);
	}
}
