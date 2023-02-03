namespace Starnight.Internal.Gateway.State;

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Contains state transition methods for <see cref="GatewayClientStateTracker"/>.
/// </summary>
public static class GatewayClientStateTrackerExtensions
{
	/// <summary>
	/// Transitions the state tracker, indicating the gateway client is connecting.
	/// </summary>
	public static void SetConnecting
	(
		ref this GatewayClientStateTracker tracker
	)
	{
		if
		(
			tracker.State is not DiscordGatewayClientState.Disconnected
				or DiscordGatewayClientState.Zombied
		)
		{
			ThrowInvalidPrecedingState
			(
				tracker.State,
				DiscordGatewayClientState.Connecting
			);
		}

		tracker.State = DiscordGatewayClientState.Connecting;
	}

	/// <summary>
	/// Transitions the state tracker, indicating the gateway client is identifying.
	/// </summary>
	public static void SetIdentifying
	(
		ref this GatewayClientStateTracker tracker
	)
	{
		if(tracker.State is not DiscordGatewayClientState.Connecting)
		{
			ThrowInvalidPrecedingState
			(
				tracker.State,
				DiscordGatewayClientState.Identifying
			);
		}

		tracker.State = DiscordGatewayClientState.Identifying;
	}

	public static void SetConnected
	(
		ref this GatewayClientStateTracker tracker
	)
	{
		if
		(
			tracker.State is not DiscordGatewayClientState.Identifying
				or DiscordGatewayClientState.Resuming
		)
		{
			ThrowInvalidPrecedingState
			(
				tracker.State,
				DiscordGatewayClientState.Connected
			);
		}

		tracker.State = DiscordGatewayClientState.Connected;
	}

	public static void SetDisconnected
	(
		ref this GatewayClientStateTracker tracker
	)
		=> tracker.State = DiscordGatewayClientState.Disconnected;

	public static void SetDisconnectedResumable
	(
		ref this GatewayClientStateTracker tracker
	)
	{
		if(tracker.State is not DiscordGatewayClientState.Connected)
		{
			ThrowInvalidPrecedingState
			(
				tracker.State,
				DiscordGatewayClientState.DisconnectedResumable
			);
		}

		tracker.State = DiscordGatewayClientState.DisconnectedResumable;
	}

	public static void SetResuming
	(
		ref this GatewayClientStateTracker tracker
	)
	{
		if(tracker.State is not DiscordGatewayClientState.DisconnectedResumable)
		{
			ThrowInvalidPrecedingState
			(
				tracker.State,
				DiscordGatewayClientState.Resuming
			);
		}

		tracker.State = DiscordGatewayClientState.Resuming;
	}

	public static void SetZombied
	(
		ref this GatewayClientStateTracker tracker
	)
	{
		if(tracker.State is not DiscordGatewayClientState.Connected)
		{
			ThrowInvalidPrecedingState
			(
				tracker.State,
				DiscordGatewayClientState.Zombied
			);
		}

		tracker.State = DiscordGatewayClientState.Zombied;
	}

	// more to encapsulate the throw than to optimize, but if we do it, we might as well do it properly
	[DoesNotReturn]
	[StackTraceHidden]
	internal static void ThrowInvalidPrecedingState
	(
		DiscordGatewayClientState previous,
		DiscordGatewayClientState @new
	)
	{
		throw new InvalidOperationException
		(
			$"Tried to change tracked state from {previous} to {@new} - this is not a valid transition!"
		);
	}
}
