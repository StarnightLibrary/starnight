namespace Starnight.Internal.Gateway.Responders;

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

using Starnight.Exceptions;

/// <summary>
/// Represents a collection of responders.
/// </summary>
public class ResponderCollection
{
	private readonly ConcurrentDictionary<Type, List<Type>> __pre_event_responders;
	private readonly ConcurrentDictionary<Type, List<Type>> __early_responders;
	private readonly ConcurrentDictionary<Type, List<Type>> __responders;
	private readonly ConcurrentDictionary<Type, List<Type>> __late_responders;
	private readonly ConcurrentDictionary<Type, List<Type>> __post_event_responders;

	public ResponderCollection()
	{
		this.__pre_event_responders = new();
		this.__early_responders = new();
		this.__responders = new();
		this.__late_responders = new();
		this.__post_event_responders = new();
	}

	/// <summary>
	/// Registers a new responder.
	/// </summary>
	/// <param name="responder">The type of the responder to register.</param>
	/// <param name="phase">The phase into which this responder will be registered.</param>
	/// <exception cref="StarnightInvalidResponderException">Thrown if the responder type was invalid.</exception>
	/// <exception cref="InvalidOperationException">Thrown if the phase wasn't a valid enum member.</exception>
	public void RegisterResponder(Type responder, ResponderPhase phase)
	{
		if(responder.GetInterface(nameof(IResponder)) is null)
		{
			throw new StarnightInvalidResponderException
			(
				$"The passed responder did not implement {nameof(IResponder)}.",
				responder
			);
		}

		IEnumerable<Type> responderInterfaces = responder.GetInterfaces()
			.Where(xm => xm.IsGenericType && xm.GetGenericTypeDefinition() == typeof(IResponder<>))
			.Select(xm => xm.GetGenericArguments().First());

		foreach(Type responderInterface in responderInterfaces)
		{
			ConcurrentDictionary<Type, List<Type>> dictionary = phase switch
			{
				ResponderPhase.PreEvent => this.__pre_event_responders,
				ResponderPhase.Early => this.__early_responders,
				ResponderPhase.Normal => this.__responders,
				ResponderPhase.Late => this.__late_responders,
				ResponderPhase.PostEvent => this.__post_event_responders,
				_ => throw new InvalidOperationException("Invalid enum type.")
			};

			_ = dictionary.AddOrUpdate
			(
				key: responderInterface,
				addValue: new()
				{
					responder
				},
				updateValueFactory: (_, current) =>
				{
					current.Add(responder);
					return current;
				}
			);
		}
	}

	/// <summary>
	/// Gets a list of responders for the given event.
	/// </summary>
	/// <param name="eventType">The type of the event to obtain responders for.</param>
	/// <param name="phase">The phase to obtain responders for.</param>
	/// <exception cref="InvalidOperationException">Thrown if the phase wasn't a valid enum member.</exception>
	public IEnumerable<Type> GetResponders
	(
		Type eventType,
		ResponderPhase phase
	)
	{
		ConcurrentDictionary<Type, List<Type>> dictionary = phase switch
		{
			ResponderPhase.PreEvent => this.__pre_event_responders,
			ResponderPhase.Early => this.__early_responders,
			ResponderPhase.Normal => this.__responders,
			ResponderPhase.Late => this.__late_responders,
			ResponderPhase.PostEvent => this.__post_event_responders,
			_ => throw new InvalidOperationException("Invalid enum type.")
		};

		_ = dictionary.TryGetValue(eventType, out List<Type>? directResponderTypes);

		_ = dictionary.TryGetValue(typeof(IDiscordGatewayEvent), out List<Type>? genericResponderTypes);

		if(directResponderTypes is null && genericResponderTypes is null)
		{
			return new List<Type>();
		}
		else if(directResponderTypes is null)
		{
			// flow analysis doesn't catch that we have guaranteed that genericResponderTypes isn't null
			return genericResponderTypes!;
		}
		else
		{
			return genericResponderTypes is null
				? (IEnumerable<Type>)directResponderTypes
				: directResponderTypes.Concat(genericResponderTypes);
		}
	}
}
