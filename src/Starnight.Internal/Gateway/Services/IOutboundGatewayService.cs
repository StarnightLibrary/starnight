namespace Starnight.Internal.Gateway.Services;

using System;
using System.Threading;
using System.Threading.Tasks;

using Starnight.Internal.Gateway.Events;
using Starnight.Internal.Gateway.Payloads.Outbound;

/// <summary>
/// Represents an interface for outbound gateway handling. To modify outbound gateway handling logic,
/// decorate this interface or register your own implementation of this interface.
/// </summary>
public interface IOutboundGatewayService
{
	/// <summary>
	/// Sends a heartbeat to the gateway.
	/// </summary>
	/// <param name="lastSequence">The last received sequence number from the gateway.</param>
	public ValueTask SendHeartbeatAsync
	(
		Int32 lastSequence
	);

	/// <summary>
	/// Identifies with the gateway and starts the gateway send loop.
	/// </summary>
	/// <param name="payload">The inner payload used to identify.</param>
	/// <param name="ct">The token for the gateway send loop.</param>
	public ValueTask IdentifyAsync
	(
		IdentifyPayload payload,
		CancellationToken ct
	);

	/// <summary>
	/// Resumes an existing gateway session.
	/// </summary>
	/// <param name="payload">The inner payload used to resume.</param>
	public ValueTask ResumeAsync
	(
		ResumePayload payload
	);

	/// <summary>
	/// Sends an event to the discord gateway. This method does not validate event data.
	/// </summary>
	/// <param name="event">The event to be sent.</param>
	public ValueTask SendEventAsync
	(
		IDiscordGatewayEvent @event
	);
}
