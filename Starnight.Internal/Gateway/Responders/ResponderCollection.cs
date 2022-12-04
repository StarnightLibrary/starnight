namespace Starnight.Internal.Gateway.Responders;

using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Extensions.Options;

using Starnight.Exceptions;

/// <summary>
/// Represents a collection of responders.
/// </summary>
public class ResponderCollection : IOptions<ResponderCollection>
{
	private readonly Dictionary<Type, List<Type>> preEventResponders;
	private readonly Dictionary<Type, List<Type>> earlyResponders;
	private readonly Dictionary<Type, List<Type>> responders;
	private readonly Dictionary<Type, List<Type>> lateResponders;
	private readonly Dictionary<Type, List<Type>> postEventResponders;

	public ResponderCollection Value => this;

	public ResponderCollection()
	{
		this.preEventResponders = new();
		this.earlyResponders = new();
		this.responders = new();
		this.lateResponders = new();
		this.postEventResponders = new();
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
			Dictionary<Type, List<Type>> dictionary = phase switch
			{
				ResponderPhase.PreEvent => this.preEventResponders,
				ResponderPhase.Early => this.earlyResponders,
				ResponderPhase.Normal => this.responders,
				ResponderPhase.Late => this.lateResponders,
				ResponderPhase.PostEvent => this.postEventResponders,
				_ => throw new InvalidOperationException("Invalid enum type.")
			};

			if(!dictionary.TryGetValue(responderInterface, out List<Type>? value))
			{
				value = new();
				dictionary.Add
				(
					responderInterface,
					value
				);
			}

			if(value.Contains(responder))
			{
				continue;
			}

			value.Add
			(
				responder
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
		Dictionary<Type, List<Type>> dictionary = phase switch
		{
			ResponderPhase.PreEvent => this.preEventResponders,
			ResponderPhase.Early => this.earlyResponders,
			ResponderPhase.Normal => this.responders,
			ResponderPhase.Late => this.lateResponders,
			ResponderPhase.PostEvent => this.postEventResponders,
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
