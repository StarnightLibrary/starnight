namespace Starnight.Internal.Gateway;

using System;

using Starnight.Internal.Gateway.Events;

/// <summary>
/// Contains the results of the current gateway frame.
/// </summary>
public record struct InboundGatewayFrame
{
	/// <summary>
	/// The event being read in this gateway frame, if applicable.
	/// </summary>
	public IDiscordGatewayEvent? Event { get; set; }

	/// <summary>
	/// Specifies whether a disconnect was requested.
	/// </summary>
	public Boolean IsDisconnected { get; set; }

	/// <summary>
	/// Specifies whether the gateway socket was disposed.
	/// </summary>
	public Boolean IsDisposed { get; set; }

	/// <summary>
	/// A template response if the gateway was not connected.
	/// </summary>
	public static readonly InboundGatewayFrame NotConnected = new()
	{
		Event = null,
		IsDisconnected = true,
		IsDisposed = false
	};

	/// <summary>
	/// A template response if the socket was disposed.
	/// </summary>
	public static readonly InboundGatewayFrame Disposed = new()
	{
		Event = null,
		IsDisconnected = true,
		IsDisposed = true
	};

	/// <summary>
	/// A template response if the payload was empty.
	/// </summary>
	public static readonly InboundGatewayFrame EmptyResponse = new()
	{
		Event = null,
		IsDisconnected = false,
		IsDisposed = false
	};
}
