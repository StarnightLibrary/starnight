namespace Starnight.Internal;

using System;

/// <summary>
/// Represents an optional parameter; ie a parameter which may or may not be sent to Discord.
/// </summary>
/// <typeparam name="TParam">Any parameter type.</typeparam>
public struct OptionalParameter<TParam>
{
	/// <summary>
	/// The "real", underlying value of this instance.
	/// </summary>
	public TParam Value { get; set; } = default!;

	public Boolean HasValue { get; set; } = false;

	public static implicit operator TParam(OptionalParameter<TParam> parameter)
		=> parameter.Value;

	public static implicit operator OptionalParameter<TParam>(TParam value)
		=> new() { Value = value };

	public OptionalParameter(TParam value)
		=> this.Value = value;
}
