namespace Starnight.Infrastructure.TransformationServices;

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;

using Starnight.Entities;

/// <summary>
/// Provides the default implementation for <see cref="ICollectionTransformerService"/>
/// </summary>
public class CollectionTransformerService : ICollectionTransformerService
{
	private readonly StarnightClient client;

	public CollectionTransformerService
	(
		StarnightClient client
	)
		=> this.client = client;

	/// <inheritdoc/>
	public IDictionary<TWrapperKey, TWrapperValue> TransformDictionary
	<
		TInternalKey,
		TInternalValue,
		TWrapperKey,
		TWrapperValue
	>
	(
		IDictionary<TInternalKey, TInternalValue> input
	)
		where TInternalValue : class
		where TWrapperValue : IStarnightEntity<TWrapperValue, TInternalValue>
		where TWrapperKey : notnull
	{
		// validate key types
		// divergence is only allowed for Int64-Snowflake, in all other cases the keys must be identical
		if(typeof(TInternalKey) != typeof(Int64) && typeof(TWrapperKey) != typeof(TInternalKey))
		{
			throw new InvalidOperationException
			(
				$"Cannot transform dictionary key type {typeof(TInternalKey)} to {typeof(TWrapperKey)}."
			);
		}

		if(typeof(TWrapperKey) == typeof(Snowflake))
		{
			Dictionary<Snowflake, TWrapperValue> result = new();

#pragma warning disable CS8605, CS8600 // we did type checks to prove this is okay
			foreach(KeyValuePair<TInternalKey, TInternalValue> pair in input)
			{
				result.Add
				(
					(Int64)(Object)pair.Key,
					TWrapperValue.FromInternalEntity
					(
						this.client,
						pair.Value
					)
				);
			}
#pragma warning restore CS8605, CS8600

			return (IDictionary<TWrapperKey, TWrapperValue>)result;
		}
		else
		{
			Dictionary<TWrapperKey, TWrapperValue> result = new();

#pragma warning disable CS8605, CS8600, CS8604 // we did type checks to prove this is okay
			foreach(KeyValuePair<TInternalKey, TInternalValue> pair in input)
			{
				result.Add
				(
					(TWrapperKey)(Object)pair.Key,
					TWrapperValue.FromInternalEntity
					(
						this.client,
						pair.Value
					)
				);
			}
#pragma warning restore CS8605, CS8600, CS8604

			return result;
		}
	}

	/// <inheritdoc/>
	public IImmutableDictionary<TWrapperKey, TWrapperValue> TransformImmutableDictionary
	<
		TInternalKey,
		TInternalValue,
		TWrapperKey,
		TWrapperValue
	>
	(
		IDictionary<TInternalKey, TInternalValue> input
	)
		where TInternalValue : class
		where TWrapperValue : IStarnightEntity<TWrapperValue, TInternalValue>
		where TWrapperKey : notnull
	{
		// validate key types
		// divergence is only allowed for Int64-Snowflake, in all other cases the keys must be identical
		if(typeof(TInternalKey) != typeof(Int64) && typeof(TWrapperKey) != typeof(TInternalKey))
		{
			throw new InvalidOperationException
			(
				$"Cannot transform dictionary key type {typeof(TInternalKey)} to {typeof(TWrapperKey)}."
			);
		}

		if(typeof(TWrapperKey) == typeof(Snowflake))
		{
			Dictionary<Snowflake, TWrapperValue> result = new();

#pragma warning disable CS8605, CS8600 // we did type checks to prove this is okay
			foreach(KeyValuePair<TInternalKey, TInternalValue> pair in input)
			{
				result.Add
				(
					(Int64)(Object)pair.Key,
					TWrapperValue.FromInternalEntity
					(
						this.client,
						pair.Value
					)
				);
			}
#pragma warning restore CS8605, CS8600

			return Unsafe.As<IImmutableDictionary<TWrapperKey, TWrapperValue>>(result.ToImmutableDictionary());
		}
		else
		{
			Dictionary<TWrapperKey, TWrapperValue> result = new();

#pragma warning disable CS8605, CS8600, CS8604 // we did type checks to prove this is okay
			foreach(KeyValuePair<TInternalKey, TInternalValue> pair in input)
			{
				result.Add
				(
					(TWrapperKey)(Object)pair.Key,
					TWrapperValue.FromInternalEntity
					(
						this.client,
						pair.Value
					)
				);
			}
#pragma warning restore CS8605, CS8600, CS8604

			return result.ToImmutableDictionary();
		}
	}

	/// <inheritdoc/>
	public IImmutableList<TWrapper> TransformImmutableList<TInternal, TWrapper>
	(
		IEnumerable<TInternal> input
	)
		where TInternal : class
		where TWrapper : IStarnightEntity<TWrapper, TInternal>
	{
		return input
			.Select
			(
				xm => TWrapper.FromInternalEntity
				(
					this.client,
					xm
				)
			)
			.ToImmutableList();
	}

	/// <inheritdoc/>
	public IReadOnlyList<TWrapper> TransformReadOnlyList<TInternal, TWrapper>
	(
		IEnumerable<TInternal> input
	)
		where TInternal : class
		where TWrapper : IStarnightEntity<TWrapper, TInternal>
	{
		return input
			.Select
			(
				xm => TWrapper.FromInternalEntity
				(
					this.client,
					xm
				)
			)
			.ToList();
	}
}
