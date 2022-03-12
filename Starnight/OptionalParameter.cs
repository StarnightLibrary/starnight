namespace Starnight;

using System;

/// <summary>
/// Represents an optional parameter; ie a parameter which may or may not be sent to Discord.
/// Normally this problem is handled using <c>[JsonIgnore(Condition = JsonIgnoreCondition.IgnoreWhenNull)]</c>,
/// but this is unfortunately not always possible.
/// </summary>
/// <typeparam name="TParam">Any nullable parameter type.</typeparam>
public struct OptionalParameter<TParam>
{
	/// <summary>
	/// The "real", underlying value of this instance.
	/// </summary>
	public TParam? Value { get; set; }

	public static implicit operator TParam?(OptionalParameter<TParam> parameter)
		=> parameter.Value;

	public static implicit operator OptionalParameter<TParam>(TParam? value)
		=> new() { Value = value, TreatAsNull = false };

	/// <summary>
	/// Whether this should be sent to Discord: if this is true, we send a <c>null</c> value to Discord, if not, we ignore a null value.
	/// </summary>
	public Boolean TreatAsNull { get; set; }

	public OptionalParameter(TParam? value, Boolean treatAsNull = false)
	{
		this.Value = value;
		this.TreatAsNull = treatAsNull;
	}
}
