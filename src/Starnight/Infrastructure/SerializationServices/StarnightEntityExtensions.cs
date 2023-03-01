namespace Starnight.Infrastructure.SerializationServices;

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.Json;

using Starnight.Entities;
using Starnight.Internal;

/// <summary>
/// Contains extension methods for serializing and deserializing Starnight objects to JSON strings.
/// </summary>
public static class StarnightEntityExtensions
{
	// the type-to-delegate mapping we use to cache delegates in Deserialize<TWrapper>(StarnightClient, String)
	private readonly static Dictionary<Type, Delegate> reflectiveDeserializeMethodMap = new();

	// the type-to-delegate mapping we use to cache delegates in Serialize<TWrapper>(TWrapper)
	private readonly static Dictionary<Type, MethodInfo> reflectiveSerializeMethodMap = new();

	/// <summary>
	/// Serializes a wrapper object to a JSON string.
	/// </summary>
	public static String Serialize<TWrapper, TInternal>
	(
		this IStarnightEntity<TWrapper, TInternal> entity
	)
		where TWrapper : IStarnightEntity<TWrapper, TInternal>
		where TInternal : class
	{
		return JsonSerializer.Serialize
		(
			entity.ToInternalEntity(),
			StarnightInternalConstants.DefaultSerializerOptions
		);
	}

	/// <summary>
	/// Serializes a wrapper object to a JSON string.
	/// </summary>
	/// <exception cref="InvalidOperationException">
	/// Thrown if the entity type was not actually a Starnight entity and did not provide a way to export
	/// itself as an internal entity.
	/// </exception>
	public static String Serialize<TWrapper>
	(
		this TWrapper entity
	)
		where TWrapper : IStarnightEntity
	{
		// get the parameterized entity interface
		Type interfaceType = typeof(TWrapper)
			.GetInterface
			(
				"Starnight.Entities.IStarnightEntity`2"
			)
			?? throw new InvalidOperationException
			(
				$"The specified type {typeof(TWrapper).FullName} was not a valid wrapper type."
			);

		// get the internal type
		Type internalType = interfaceType.GetGenericArguments()[1];

		// try getting the delegate from cache
		if(!reflectiveSerializeMethodMap.TryGetValue(typeof(TWrapper), out MethodInfo? method))
		{
			// it wasn't cached, so create it
			// get the FromInternalEntity method
			method = typeof(TWrapper)
				.GetMethod
				(
					"ToInternalEntity",
					BindingFlags.Public | BindingFlags.Instance
				)
				?? throw new InvalidOperationException
				(
					"The specified wrapper type did not provide a conversion method to an internal entity."
				);

			// cache the delegate
			reflectiveSerializeMethodMap.Add
			(
				typeof(TWrapper),
				method
			);
		}

		// get the internal entity and serialize it
		Object internalEntity = method.Invoke
		(
			entity,
			null
		)!;

		return JsonSerializer.Serialize
		(
			internalEntity,
			internalType,
			StarnightInternalConstants.DefaultSerializerOptions
		);
	}

	/// <summary>
	/// Deserializes a Starnight entity object from a string.
	/// </summary>
	/// <typeparam name="TWrapper">The entity type to deserialize to.</typeparam>
	/// <param name="client">The starnight client this entity should be constructed around.</param>
	/// <param name="text">The JSON string to deserialize.</param>
	/// <exception cref="InvalidOperationException">
	/// Thrown if the entity type was not actually a Starnight entity and did not provide a way to construct
	/// itself from an internal entity.
	/// </exception>
	/// <exception cref="ArgumentException">
	/// Thrown if the specified JSON string was invalid or didn't match the object.
	/// </exception>
	public static TWrapper Deserialize<TWrapper>
	(
		this StarnightClient client,
		String text
	)
	{
		// get the entity interface
		Type interfaceType = typeof(TWrapper)
			.GetInterface
			(
				"Starnight.Entities.IStarnightEntity`2"
			)
			?? throw new InvalidOperationException
			(
				$"The specified type {typeof(TWrapper).FullName} was not a valid wrapper type."
			);

		// get the internal type
		Type internalType = interfaceType.GetGenericArguments()[1];

		// deserialize the object
		Object internalObject = JsonSerializer.Deserialize
		(
			text,
			internalType,
			StarnightInternalConstants.DefaultSerializerOptions
		)
		?? throw new ArgumentException
		(
			$"The passed text could not be deserialized to the specific type.",
			nameof(text)
		);

		// try getting the delegate from cache
		if(!reflectiveDeserializeMethodMap.TryGetValue(typeof(TWrapper), out Delegate? method))
		{
			// it wasn't cached, so create it
			// get the FromInternalEntity method
			method = typeof(TWrapper)
				.GetMethod
				(
					"FromInternalEntity",
					BindingFlags.Public | BindingFlags.Static
				)
				?.CreateDelegate
				(
					typeof(Func<,,>)
						.MakeGenericType
						(
							typeof(StarnightClient),
							internalType,
							typeof(TWrapper)
						)
				)
				?? throw new InvalidOperationException
				(
					"The specified wrapper type did not provide a construction method from an internal entity."
				);

			// cache the delegate
			reflectiveDeserializeMethodMap.Add
			(
				typeof(TWrapper),
				method
			);
		}

		// invoke and return
		return (TWrapper)method.DynamicInvoke
		(
			null,
			new Object[]
			{
				client,
				internalObject
			}
		)!;
	}

	/// <summary>
	/// Deserializes a wrapper entity from a JSON string.
	/// </summary>
	/// <param name="client">The StarnightClient to construct this wrapper entity around.</param>
	/// <param name="text">The JSON string to deserialize from.</param>
	/// <exception cref="ArgumentException">
	/// Thrown if the specified JSON string was invalid or didn't match the object.
	/// </exception>
	public static TWrapper Deserialize<TWrapper, TInternal>
	(
		this StarnightClient client,
		String text
	)
		where TWrapper : IStarnightEntity<TWrapper, TInternal>
		where TInternal : class
	{
		TInternal entity = JsonSerializer.Deserialize<TInternal>
		(
			text,
			StarnightInternalConstants.DefaultSerializerOptions
		)
		?? throw new ArgumentException
		(
			$"The passed text could not be deserialized to the internal type.",
			nameof(text)
		);

		return TWrapper.FromInternalEntity
		(
			client,
			entity
		);
	}
}
