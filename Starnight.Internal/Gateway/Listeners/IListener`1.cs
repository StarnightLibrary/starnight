namespace Starnight.Internal.Gateway.Listeners;

using System.Threading.Tasks;

/// <summary>
/// Represents a base interface for all listeners.
/// </summary>
/// <typeparam name="TEvent">The event type this listener handles.</typeparam>
public interface IListener<TEvent> : IListener
	where TEvent : IDiscordGatewayEvent
{
	/// <summary>
	/// Responds to the given gateway event.
	/// </summary>
	/// <param name="event">The event in question.</param>
	public ValueTask ListenAsync(TEvent @event);
}
