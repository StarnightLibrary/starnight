namespace Starnight.Internal.Gateway.Listeners;

using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Extensions.Options;

using Starnight.Internal.Exceptions;
using Starnight.Internal.Gateway.Events;

/// <summary>
/// Represents a collection of listeners.
/// </summary>
public class ListenerCollection : IOptions<ListenerCollection>
{
	private readonly Dictionary<Type, List<Type>> preEventListeners;
	private readonly Dictionary<Type, List<Type>> earlyListeners;
	private readonly Dictionary<Type, List<Type>> listeners;
	private readonly Dictionary<Type, List<Type>> lateListeners;
	private readonly Dictionary<Type, List<Type>> postEventListeners;

	public ListenerCollection Value => this;

	public ListenerCollection()
	{
		this.preEventListeners = new();
		this.earlyListeners = new();
		this.listeners = new();
		this.lateListeners = new();
		this.postEventListeners = new();
	}

	/// <summary>
	/// Registers a new listener.
	/// </summary>
	/// <param name="listener">The type of the listener to register.</param>
	/// <param name="phase">The phase into which this listener will be registered.</param>
	/// <exception cref="StarnightInvalidListenerException">Thrown if the listener type was invalid.</exception>
	/// <exception cref="InvalidOperationException">Thrown if the phase wasn't a valid enum member.</exception>
	public void RegisterListener(Type listener, ListenerPhase phase)
	{
		if(listener.GetInterface(nameof(IListener)) is null)
		{
			throw new StarnightInvalidListenerException
			(
				$"The passed listener did not implement {nameof(IListener)}.",
				listener
			);
		}

		IEnumerable<Type> listenerInterfaces = listener.GetInterfaces()
			.Where(xm => xm.IsGenericType && xm.GetGenericTypeDefinition() == typeof(IListener<>))
			.Select(xm => xm.GetGenericArguments().First());

		foreach(Type listenerInterface in listenerInterfaces)
		{
			Dictionary<Type, List<Type>> dictionary = phase switch
			{
				ListenerPhase.PreEvent => this.preEventListeners,
				ListenerPhase.Early => this.earlyListeners,
				ListenerPhase.Normal => this.listeners,
				ListenerPhase.Late => this.lateListeners,
				ListenerPhase.PostEvent => this.postEventListeners,
				_ => throw new InvalidOperationException("Invalid enum type.")
			};

			if(!dictionary.TryGetValue(listenerInterface, out List<Type>? value))
			{
				value = new();
				dictionary.Add
				(
					listenerInterface,
					value
				);
			}

			if(value.Contains(listener))
			{
				continue;
			}

			value.Add
			(
				listener
			);
		}
	}

	/// <summary>
	/// Gets a list of listeners for the given event.
	/// </summary>
	/// <param name="eventType">The type of the event to obtain listeners for.</param>
	/// <param name="phase">The phase to obtain listeners for.</param>
	/// <exception cref="InvalidOperationException">Thrown if the phase wasn't a valid enum member.</exception>
	public IEnumerable<Type> GetListeners
	(
		Type eventType,
		ListenerPhase phase
	)
	{
		Dictionary<Type, List<Type>> dictionary = phase switch
		{
			ListenerPhase.PreEvent => this.preEventListeners,
			ListenerPhase.Early => this.earlyListeners,
			ListenerPhase.Normal => this.listeners,
			ListenerPhase.Late => this.lateListeners,
			ListenerPhase.PostEvent => this.postEventListeners,
			_ => throw new InvalidOperationException("Invalid enum type.")
		};

		_ = dictionary.TryGetValue(eventType, out List<Type>? directListenerTypes);

		_ = dictionary.TryGetValue(typeof(IDiscordGatewayEvent), out List<Type>? genericListenerTypes);

		if(directListenerTypes is null && genericListenerTypes is null)
		{
			return new List<Type>();
		}
		else if(directListenerTypes is null)
		{
			// flow analysis doesn't catch that we have guaranteed that genericListenerTypes isn't null
			return genericListenerTypes!;
		}
		else
		{
			return genericListenerTypes is null
				? (IEnumerable<Type>)directListenerTypes
				: directListenerTypes.Concat(genericListenerTypes);
		}
	}
}
