namespace Starnight.Infrastructure.TransformationServices;

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;

using Starnight.Entities;

/// <summary>
/// Contains helpers to transform a collection of internal entities into a matching collection of
/// wrapper entities.
/// </summary>
// TODO: add frozen collection transformer once .NET 8 is targeted
public interface ICollectionTransformerService
{
	/// <summary>
	/// Transforms a read-only list of internal entities into a read-only list of wrapper entities.
	/// </summary>
	/// <typeparam name="TInternal">The internal entity being transformed.</typeparam>
	/// <typeparam name="TWrapper">The wrapper entity being transformed.</typeparam>
	public ValueTask<IReadOnlyList<TWrapper>> TransformReadOnlyListAsync
	<
		TInternal,
		TWrapper
	>
	(
		IReadOnlyList<TInternal> input
	)
		where TWrapper : IStarnightEntity<TWrapper, TInternal>
		where TInternal : class;

	/// <summary>
	/// Transforms an immutable list of internal entities into an immutable list of wrapper entities.
	/// </summary>
	/// <typeparam name="TInternal">The internal entity being transformed to.</typeparam>
	/// <typeparam name="TWrapper">The wrapper entity being transformed to.</typeparam>
	public ValueTask<IImmutableList<TWrapper>> TransformImmutableListAsync
	<
		TInternal,
		TWrapper
	>
	(
		IImmutableList<TInternal> input
	)
		where TWrapper : IStarnightEntity<TWrapper, TInternal>
		where TInternal : class;
}
