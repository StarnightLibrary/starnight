namespace Starnight.Internal.Gateway.Responders;

using System.Threading.Tasks;

/// <summary>
/// Represents a base interface for all responders.
/// </summary>
/// <typeparam name="TEvent">The event type this responder handles.</typeparam>
public interface IResponder<TEvent>
	where TEvent : IDiscordGatewayEvent
{
	/// <summary>
	/// Responds to the given gateway event.
	/// </summary>
	/// <param name="event">The event in question.</param>
	public ValueTask RespondAsync(TEvent @event);
}
