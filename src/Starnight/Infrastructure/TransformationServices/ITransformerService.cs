namespace Starnight.Infrastructure.TransformationServices;

using System.Threading.Tasks;

/// <summary>
/// Declares a mechanism for transforming an object. No .NET conversion, implicit or explicit, is required
/// to exist. Transformation services are permitted to take other services via DI.
/// </summary>
/// <typeparam name="TFrom">The type to transform from.</typeparam>
/// <typeparam name="TTo">The type to transform to.</typeparam>
public interface ITransformerService<TFrom, TTo>
{
	/// <summary>
	/// Transforms an object from one type to another.
	/// </summary>
	public ValueTask<TTo> TransformAsync
	(
		TFrom value
	);
}
