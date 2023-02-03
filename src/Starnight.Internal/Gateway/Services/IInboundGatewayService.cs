namespace Starnight.Internal.Gateway.Services;

using System;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

using Starnight.Internal.Gateway.Events;

/// <summary>
/// Represents an interface for inbouond gateway handling. To modify inbound gateway handling logic,
/// decorate this interface or register your own implementation of this interface.
/// </summary>
public interface IInboundGatewayService
{
	/// <summary>
	/// Gets the last received sequence number.
	/// </summary>
	public Int32 LastReceivedSequence { get; }

	/// <summary>
	/// Gets a channel of all control events
	/// </summary>
	public ChannelReader<IDiscordGatewayEvent> ControlEvents { get; }

	/// <summary>
	/// Starts operation of the inbound handler.
	/// </summary>
	/// <param name="ct">The cancellation token to use throughout its lifetime.</param>
	public ValueTask StartAsync(CancellationToken ct);
}
