namespace Starnight;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Represents an optional value - that is, a value that may either be present or not be present.
/// <see langword="null"/> is a valid presence.
/// </summary>
/// <typeparam name="T">Any parameter type.</typeparam>
public record struct Optional<T>
{
	/// <summary>
	/// Gets an empty <see cref="Optional{T}"/>, with no provided value and indicating that no value will be provided.
	/// </summary>
	public static Optional<T> Empty { get; } = default;

	private T value = default!;

	/// <summary>
	/// Gets or sets the underlying value of this instance.
	/// </summary>
	public T Value
	{
		readonly get
		{
			return this.HasValue
				? this.value
				: throw new InvalidOperationException("This Optional instance has no value.");
		}
		set
		{
			this.value = value;
			this.HasValue = true;
		}
	}

	/// <summary>
	/// Specifies whether this instance represents a value.
	/// </summary>
	public Boolean HasValue { get; set; } = false;

	/// <summary>
	/// Specifies whether this instance represents a value that is not null.
	/// </summary>
	public readonly Boolean IsDefined => this.HasValue && this.value is not null;

	/// <summary>
	/// Resolves the value from an optional value, if available
	/// </summary>
	/// <param name="value">The resolved value. This should only be utilized if the method returned true.</param>
	/// <returns>Whether this instance represents a non-null value.</returns>
	public readonly Boolean Resolve
	(
		[NotNullWhen(true)]
		out T? value
	)
	{
		if(this.IsDefined)
		{
			value = this.Value!;
			return true;
		}
		else
		{
			value = default;
			return false;
		}
	}

	public static implicit operator T(Optional<T> parameter)
		=> parameter.Value;

	public static implicit operator Optional<T>(T value)
		=> new() { Value = value, HasValue = true };

	public Optional(T value)
	{
		this.Value = value;
		this.HasValue = true;
	}

	public static Boolean operator ==(Optional<T> optional, T value)
		=> optional.HasValue && EqualityComparer<T>.Default.Equals(optional.Value, value);

	public static Boolean operator !=(Optional<T> optional, T value)
		=> !(optional == value);

	public readonly override Int32 GetHashCode()
		=> this.Value?.GetHashCode() ?? 0;

	public readonly override String ToString()
		=> this.HasValue ? this.Value?.ToString() ?? "null" : "Optional/no value";
}
