namespace Starnight.Internal.Gateway.Services;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Polly;
using Polly.RateLimit;

using Starnight.Internal.Gateway.Events;
using Starnight.Internal.Gateway.Events.Outbound;
using Starnight.Internal.Gateway.Payloads.Outbound;

/// <summary>
/// Contains logic for sending payloads to Discord.
/// </summary>
public class OutboundGatewayService : IOutboundGatewayService
{
	private readonly TransportService transportService;

	private readonly RateLimitPolicy policy;

	private readonly PriorityQueue<IDiscordGatewayEvent, Int32> payloadQueue;

	public OutboundGatewayService
	(
		TransportService transportService
	)
	{
		this.transportService = transportService;

		this.policy = Policy.RateLimit(120, TimeSpan.FromMinutes(1));

		this.payloadQueue = new();
	}

	/// <inheritdoc/>
	public ValueTask IdentifyAsync
	(
		IdentifyPayload payload,
		CancellationToken ct
	)
	{
		DiscordIdentifyEvent @event = new()
		{
			Data = payload
		};

		_ = Task.Factory.StartNew(async () => await this.sendAsync(ct));

		this.payloadQueue.Enqueue(@event, 0);

		return ValueTask.CompletedTask;
	}

	/// <inheritdoc/>
	public ValueTask ResumeAsync(ResumePayload payload)
	{
		DiscordResumeEvent @event = new()
		{
			Data = payload
		};

		this.payloadQueue.Enqueue(@event, 0);

		return ValueTask.CompletedTask;
	}

	/// <inheritdoc/>
	public ValueTask SendHeartbeatAsync(Int32 lastSequence)
	{
		DiscordOutboundHeartbeatEvent @event = new()
		{
			Data = lastSequence,
			Opcode = DiscordGatewayOpcode.Heartbeat
		};

		this.payloadQueue.Enqueue(@event, 0);

		return ValueTask.CompletedTask;
	}

	/// <inheritdoc/>
	public ValueTask SendEventAsync(IDiscordGatewayEvent @event)
	{
		this.payloadQueue.Enqueue(@event, 1);

		return ValueTask.CompletedTask;		
	}

	private async ValueTask sendAsync
	(
		CancellationToken ct
	)
	{
		PeriodicTimer timer = new(TimeSpan.FromSeconds(0.5));

		while(await timer.WaitForNextTickAsync(ct))
		{
			if(this.payloadQueue.Count == 0)
			{
				continue;
			}

			IDiscordGatewayEvent @event = this.payloadQueue.Dequeue();

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
}
