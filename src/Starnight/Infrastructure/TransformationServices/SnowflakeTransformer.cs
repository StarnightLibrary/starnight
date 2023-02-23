namespace Starnight.Infrastructure.TransformationServices;

using System;
using System.Threading.Tasks;

/// <summary>
/// Represents a transformer between the wrapper <see cref="Snowflake"/> and the internally-used <see cref="Int64"/>.
/// </summary>
public class SnowflakeTransformer :
	ITransformerService<Int64, Snowflake>,
	ITransformerService<Snowflake, Int64>
{
	/// <inheritdoc/>
	public ValueTask<Snowflake> TransformAsync
	(
		Int64 value
	)
		=> ValueTask.FromResult((Snowflake)value);

	/// <inheritdoc/>
	public ValueTask<Int64> TransformAsync
	(
		Snowflake value
	)
		=> ValueTask.FromResult(value.Value);
}
