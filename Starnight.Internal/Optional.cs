namespace Starnight;

using System;
using System.Collections.Generic;

/// <summary>
/// Represents an optional value - that is, a value that may either be present or not be present.
/// <see langword="null"/> is a valid presence.
/// </summary>
/// <typeparam name="T">Any parameter type.</typeparam>
public struct Optional<T> : IOptional
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
		get => this.HasValue ? this.value : throw new InvalidOperationException("This Optional instance has no value.");
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
	public Boolean IsDefined => this.HasValue && this.value is not null;

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

	public static Boolean operator ==(Optional<T> right, Optional<T> left)
		=> right.HasValue == left.HasValue && EqualityComparer<T>.Default.Equals(right.Value, left.Value);

	public static Boolean operator !=(Optional<T> right, Optional<T> left)
		=> !(right == left);

	public override Boolean Equals(Object? obj)
		=> obj is Optional<T> optional && optional.HasValue == this.HasValue && EqualityComparer<T>.Default.Equals(this.Value, optional.Value);

	public override Int32 GetHashCode()
		=> this.Value?.GetHashCode() ?? 0;

	public override String ToString()
		=> this.HasValue ? this.Value?.ToString() ?? "null" : "Optional/no value";
}
