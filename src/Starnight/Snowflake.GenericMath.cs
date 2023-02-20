namespace Starnight;

using System;
using System.Numerics;
using System.Globalization;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Buffers.Binary;

public readonly partial record struct Snowflake :
	IBinaryInteger<Snowflake>,
	IMinMaxValue<Snowflake>,
	IParsable<Snowflake>,
	ISpanFormattable,
	ISpanParsable<Snowflake>,
	IIncrementOperators<Snowflake>,
	IDecrementOperators<Snowflake>
{
	/// <inheritdoc cref="INumberBase{TSelf}.One"/>
	public static Snowflake One => 1;

	/// <inheritdoc cref="INumberBase{TSelf}.Zero"/>
	public static Snowflake Zero => 0;

	/// <inheritdoc cref="IMinMaxValue{TSelf}.MaxValue"/>
	public static Snowflake MaxValue => Int64.MaxValue;

	/// <inheritdoc cref="IMinMaxValue{TSelf}.MaxValue"/>
	public static Snowflake MinValue => new
	(
		DiscordEpoch,
		0,
		0,
		0
	);

	/// <inheritdoc/>
	static Int32 INumberBase<Snowflake>.Radix => 2;

	/// <inheritdoc/>
	static Snowflake IAdditiveIdentity<Snowflake, Snowflake>.AdditiveIdentity { get; } = 0;

	/// <inheritdoc/>
	static Snowflake IMultiplicativeIdentity<Snowflake, Snowflake>.MultiplicativeIdentity { get; } = 1;

	/// <inheritdoc cref="INumberBase{TSelf}.Parse(ReadOnlySpan{Char}, NumberStyles, IFormatProvider?)"/>
	public static Snowflake Parse
	(
		ReadOnlySpan<Char> s,
		NumberStyles style = NumberStyles.Integer | NumberStyles.AllowLeadingWhite,
		IFormatProvider? provider = null
	)
	{
		return Int64.Parse
		(
			s,
			style,
			provider
		);
	}

	/// <inheritdoc cref="INumberBase{TSelf}.Parse(String, NumberStyles, IFormatProvider?)"/>
	public static Snowflake Parse
	(
		String s,
		NumberStyles style = NumberStyles.Integer | NumberStyles.AllowLeadingWhite,
		IFormatProvider? provider = null
	)
	{
		return Int64.Parse
		(
			s,
			style,
			provider
		);
	}

	/// <inheritdoc cref="ISpanParsable{TSelf}.Parse(ReadOnlySpan{Char}, IFormatProvider?)"/>
	public static Snowflake Parse
	(
		ReadOnlySpan<Char> s,
		IFormatProvider? provider = null
	)
	{
		return Int64.Parse
		(
			s,
			provider
		);
	}

	/// <inheritdoc cref="IParsable{TSelf}.Parse(String, IFormatProvider?)"/>
	public static Snowflake Parse
	(
		String s,
		IFormatProvider? provider = null
	)
	{
		return Int64.Parse
		(
			s,
			provider
		);
	}

	/// <inheritdoc cref="INumberBase{TSelf}.TryParse(ReadOnlySpan{Char}, NumberStyles, IFormatProvider?, out TSelf)"/>
	public static Boolean TryParse
	(
		ReadOnlySpan<Char> s,
		NumberStyles style,
		IFormatProvider? provider,

		[MaybeNullWhen(false)]
		out Snowflake result
	)
	{
		Boolean success = Int64.TryParse
		(
			s,
			style,
			provider,
			out Int64 value
		);

		result = success ? value : default;

		return success;
	}

	/// <inheritdoc cref="INumberBase{TSelf}.TryParse(String, NumberStyles, IFormatProvider?, out TSelf)"/>
	public static Boolean TryParse
	(
		[NotNullWhen(true)]
		String? s,

		NumberStyles style,
		IFormatProvider? provider,

		[MaybeNullWhen(false)]
		out Snowflake result
	)
	{
		Boolean success = Int64.TryParse
		(
			s,
			style,
			provider,
			out Int64 value
		);

		result = success ? value : default;

		return success;
	}

	/// <inheritdoc cref="ISpanParsable{TSelf}.TryParse(ReadOnlySpan{Char}, IFormatProvider?, out TSelf)"/>
	public static Boolean TryParse
	(
		ReadOnlySpan<Char> s,
		IFormatProvider? provider,

		[MaybeNullWhen(false)]
		out Snowflake result
	)
	{
		Boolean success = Int64.TryParse
		(
			s,
			provider,
			out Int64 value
		);

		result = success ? value : default;

		return success;
	}

	/// <inheritdoc cref="IParsable{TSelf}.TryParse(String?, IFormatProvider?, out TSelf)"/>
	public static Boolean TryParse
	(
		[NotNullWhen(true)]
		String? s,

		IFormatProvider? provider,

		[MaybeNullWhen(false)]
		out Snowflake result
	)
	{
		Boolean success = Int64.TryParse
		(
			s,
			provider,
			out Int64 value
		);

		result = success ? value : default;

		return success;
	}

	/// <inheritdoc/>
	static Snowflake INumberBase<Snowflake>.Abs
	(
		Snowflake value
	)
		=> Int64.Abs(value);

	/// <inheritdoc/>
	static Boolean INumberBase<Snowflake>.IsCanonical
	(
		Snowflake value
	)
		=> true;

	/// <inheritdoc/>
	static Boolean INumberBase<Snowflake>.IsComplexNumber
	(
		Snowflake value
	)
		=> false;

	/// <inheritdoc/>
	static Boolean INumberBase<Snowflake>.IsEvenInteger
	(
		Snowflake value
	)
		=> Int64.IsEvenInteger(value);

	/// <inheritdoc/>
	static Boolean INumberBase<Snowflake>.IsFinite
	(
		Snowflake value
	)
		=> true;

	/// <inheritdoc/>
	static Boolean INumberBase<Snowflake>.IsImaginaryNumber
	(
		Snowflake value
	)
		=> false;

	/// <inheritdoc/>
	static Boolean INumberBase<Snowflake>.IsInfinity
	(
		Snowflake value
	)
		=> false;

	/// <inheritdoc/>
	static Boolean INumberBase<Snowflake>.IsInteger
	(
		Snowflake value
	)
		=> true;

	/// <inheritdoc/>
	static Boolean INumberBase<Snowflake>.IsNaN
	(
		Snowflake value
	)
		=> false;

	/// <inheritdoc/>
	static Boolean INumberBase<Snowflake>.IsNegative
	(
		Snowflake value
	)
		=> Int64.IsNegative(value);

	/// <inheritdoc/>
	static Boolean INumberBase<Snowflake>.IsNegativeInfinity
	(
		Snowflake value
	)
		=> false;

	/// <inheritdoc/>
	static Boolean INumberBase<Snowflake>.IsNormal
	(
		Snowflake value
	)
		=> false;

	/// <inheritdoc/>
	static Boolean INumberBase<Snowflake>.IsOddInteger
	(
		Snowflake value
	)
		=> Int64.IsOddInteger(value);

	/// <inheritdoc/>
	static Boolean INumberBase<Snowflake>.IsPositive
	(
		Snowflake value
	)
		=> Int64.IsPositive(value);

	/// <inheritdoc/>
	static Boolean INumberBase<Snowflake>.IsPositiveInfinity
	(
		Snowflake value
	)
		=> false;

	/// <inheritdoc/>
	static Boolean IBinaryNumber<Snowflake>.IsPow2
	(
		Snowflake value
	)
		=> Int64.IsPow2(value);

	/// <inheritdoc/>
	static Boolean INumberBase<Snowflake>.IsRealNumber
	(
		Snowflake value
	)
		=> true;

	/// <inheritdoc/>
	static Boolean INumberBase<Snowflake>.IsSubnormal
	(
		Snowflake value
	)
		=> false;

	/// <inheritdoc/>
	static Boolean INumberBase<Snowflake>.IsZero
	(
		Snowflake value
	)
		=> value == 0;

	/// <inheritdoc/>
	static Snowflake IBinaryNumber<Snowflake>.Log2
	(
		Snowflake value
	)
		=> Int64.Log2(value);

	/// <inheritdoc/>
	static Snowflake INumberBase<Snowflake>.MaxMagnitude
	(
		Snowflake x,
		Snowflake y
	)
		=> Int64.MaxMagnitude(x, y);

	/// <inheritdoc/>
	static Snowflake INumberBase<Snowflake>.MaxMagnitudeNumber
	(
		Snowflake x,
		Snowflake y
	)
		=> Int64.MaxMagnitude(x, y);

	/// <inheritdoc/>
	static Snowflake INumberBase<Snowflake>.MinMagnitude
	(
		Snowflake x,
		Snowflake y
	)
		=> Int64.MinMagnitude(x, y);

	/// <inheritdoc/>
	static Snowflake INumberBase<Snowflake>.MinMagnitudeNumber
	(
		Snowflake x,
		Snowflake y
	)
		=> Int64.MinMagnitude(x, y);

	/// <inheritdoc/>
	static Snowflake INumberBase<Snowflake>.Parse
	(
		ReadOnlySpan<Char> s,
		NumberStyles style,
		IFormatProvider? provider
	)
	{
		return Int64.Parse
		(
			s,
			style,
			provider
		);
	}

	/// <inheritdoc/>
	static Snowflake INumberBase<Snowflake>.Parse
	(
		String s,
		NumberStyles style,
		IFormatProvider? provider
	)
	{
		return Int64.Parse
		(
			s,
			style,
			provider
		);
	}

	/// <inheritdoc/>
	static Snowflake ISpanParsable<Snowflake>.Parse
	(
		ReadOnlySpan<Char> s,
		IFormatProvider? provider
	)
	{
		return Int64.Parse
		(
			s,
			provider
		);
	}

	/// <inheritdoc/>
	static Snowflake IParsable<Snowflake>.Parse
	(
		String s,
		IFormatProvider? provider
	)
	{
		return Int64.Parse
		(
			s,
			provider
		);
	}

	/// <inheritdoc/>
	static Snowflake IBinaryInteger<Snowflake>.PopCount
	(
		Snowflake value
	)
		=> Int64.PopCount(value);

	/// <inheritdoc/>
	static Snowflake IBinaryInteger<Snowflake>.TrailingZeroCount
	(
		Snowflake value
	)
		=> Int64.TrailingZeroCount(value);

	/// <inheritdoc/>
	static Boolean INumberBase<Snowflake>.TryConvertFromChecked<TOther>
	(
		TOther value,
		out Snowflake result
	)
	{
		try
		{
			result = Int64.CreateChecked
			(
				value
			);

			return true;
		}
		catch
		{
			result = default;
			return false;
		}
	}

	/// <inheritdoc/>
	static Boolean INumberBase<Snowflake>.TryConvertFromSaturating<TOther>
	(
		TOther value,
		out Snowflake result
	)
	{
		try
		{
			result = Int64.CreateSaturating
			(
				value
			);

			return true;
		}
		catch
		{
			result = default;
			return false;
		}
	}

	/// <inheritdoc/>
	static Boolean INumberBase<Snowflake>.TryConvertFromTruncating<TOther>
	(
		TOther value,
		out Snowflake result
	)
	{
		try
		{
			result = Int64.CreateTruncating
			(
				value
			);

			return true;
		}
		catch
		{
			result = default;
			return false;
		}
	}

#pragma warning disable CS8500 // we statically prove this is fine
	/// <inheritdoc/>
	static unsafe Boolean INumberBase<Snowflake>.TryConvertToChecked<TOther>
	(
		Snowflake value,

		[NotNullWhen(true)]
		out TOther result
	)
	{
		if(typeof(TOther) == typeof(Byte))
		{
			Byte actualResult = checked((Byte)value);
			result = *(TOther*)&actualResult;
			return true;
		}
		else if(typeof(TOther) == typeof(Char))
		{
			Char actualResult = checked((Char)value);
			result = *(TOther*)&actualResult;
			return true;
		}
		else if(typeof(TOther) == typeof(Decimal))
		{
			Decimal actualResult = value;
			result = *(TOther*)&actualResult;
			return true;
		}
		else if(typeof(TOther) == typeof(UInt16))
		{
			UInt16 actualResult = checked((UInt16)value);
			result = *(TOther*)&actualResult;
			return true;
		}
		else if(typeof(TOther) == typeof(UInt32))
		{
			UInt32 actualResult = checked((UInt32)value);
			result = *(TOther*)&actualResult;
			return true;
		}
		else if(typeof(TOther) == typeof(UInt64))
		{
			UInt64 actualResult = checked((UInt64)value.Value);
			result = *(TOther*)&actualResult;
			return true;
		}
		else if(typeof(TOther) == typeof(UInt128))
		{
			UInt128 actualResult = checked((UInt128)value.Value);
			result = *(TOther*)&actualResult;
			return true;
		}
		else if(typeof(TOther) == typeof(UIntPtr))
		{
			UIntPtr actualResult = checked((UIntPtr)value.Value);
			result = *(TOther*)&actualResult;
			return true;
		}
		else
		{
			result = default!;
			return false;
		}
	}

	/// <inheritdoc/>
	static unsafe Boolean INumberBase<Snowflake>.TryConvertToSaturating<TOther>
	(
		Snowflake value,

		[MaybeNullWhen(false)]
		out TOther result
	)
	{
		if(typeof(TOther) == typeof(Byte))
		{
			Byte actualResult = value >= Byte.MaxValue
				? Byte.MaxValue
				: value <= Byte.MinValue
					? Byte.MinValue
					: (Byte)value;

			result = *(TOther*)&actualResult;
			return true;
		}
		else if(typeof(TOther) == typeof(Char))
		{
			Char actualResult = value >= Char.MaxValue
				? Char.MaxValue
				: value <= Char.MinValue
					? Char.MinValue
					: (Char)value;

			result = *(TOther*)&actualResult;
			return true;
		}
		else if(typeof(TOther) == typeof(Decimal))
		{
			Decimal actualResult = value;
			result = *(TOther*)&actualResult;
			return true;
		}
		else if(typeof(TOther) == typeof(UInt16))
		{
			UInt16 actualResult = value >= UInt16.MaxValue
				? UInt16.MaxValue
				: value <= UInt16.MinValue
					? UInt16.MinValue
					: (UInt16)value;

			result = *(TOther*)&actualResult;
			return true;
		}
		else if(typeof(TOther) == typeof(UInt32))
		{
			UInt32 actualResult = value >= UInt32.MaxValue
				? UInt32.MaxValue
				: value <= UInt32.MinValue
					? UInt32.MinValue
					: (UInt32)value;

			result = *(TOther*)&actualResult;
			return true;
		}
		else if(typeof(TOther) == typeof(UInt64))
		{
			UInt64 actualResult = value <= 0 ? UInt64.MinValue : (UInt64)value.Value;
			result = *(TOther*)&actualResult;
			return true;
		}
		else if(typeof(TOther) == typeof(UInt128))
		{
			UInt128 actualResult = (value <= 0) ? UInt128.MinValue : (UInt128)value.Value;
			result = *(TOther*)&actualResult;
			return true;
		}
		else if(typeof(TOther) == typeof(UIntPtr))
		{
			UIntPtr actualResult = (value <= 0) ? 0 : (UIntPtr)value.Value;
			result = *(TOther*)&actualResult;
			return true;
		}
		else
		{
			result = default!;
			return false;
		}
	}

	/// <inheritdoc/>
	static unsafe Boolean INumberBase<Snowflake>.TryConvertToTruncating<TOther>
	(
		Snowflake value,

		[MaybeNullWhen(false)]
		out TOther result
	)
	{
		if(typeof(TOther) == typeof(Byte))
		{
			Byte actualResult = (Byte)value;
			result = *(TOther*)&actualResult;
			return true;
		}
		else if(typeof(TOther) == typeof(Char))
		{
			Char actualResult = (Char)value;
			result = *(TOther*)&actualResult;
			return true;
		}
		else if(typeof(TOther) == typeof(Decimal))
		{
			Decimal actualResult = value;
			result = *(TOther*)&actualResult;
			return true;
		}
		else if(typeof(TOther) == typeof(UInt16))
		{
			UInt16 actualResult = (UInt16)value;
			result = *(TOther*)&actualResult;
			return true;
		}
		else if(typeof(TOther) == typeof(UInt32))
		{
			UInt32 actualResult = (UInt32)value;
			result = *(TOther*)&actualResult;
			return true;
		}
		else if(typeof(TOther) == typeof(UInt64))
		{
			UInt64 actualResult = (UInt64)value.Value;
			result = *(TOther*)&actualResult;
			return true;
		}
		else if(typeof(TOther) == typeof(UInt128))
		{
			UInt128 actualResult = (UInt128)value.Value;
			result = *(TOther*)&actualResult;
			return true;
		}
		else if(typeof(TOther) == typeof(UIntPtr))
		{
			UIntPtr actualResult = (UIntPtr)value.Value;
			result = *(TOther*)&actualResult;
			return true;
		}
		else
		{
			result = default;
			return false;
		}
	}
#pragma warning restore CS8500

	/// <inheritdoc/>
	static Boolean INumberBase<Snowflake>.TryParse
	(
		ReadOnlySpan<Char> s,
		NumberStyles style,
		IFormatProvider? provider,
		out Snowflake result
	)
	{
		Boolean success = Int64.TryParse
		(
			s,
			style,
			provider,
			out Int64 value
		);

		result = success ? value : default;

		return success;
	}

	/// <inheritdoc/>
	static Boolean INumberBase<Snowflake>.TryParse
	(
		[NotNullWhen(true)]
		String? s,

		NumberStyles style,
		IFormatProvider? provider,
		out Snowflake result
	)
	{
		Boolean success = Int64.TryParse
		(
			s,
			style,
			provider,
			out Int64 value
		);

		result = success ? value : default;

		return success;
	}

	/// <inheritdoc/>
	static Boolean ISpanParsable<Snowflake>.TryParse
	(
		ReadOnlySpan<Char> s,
		IFormatProvider? provider,
		out Snowflake result
	)
	{
		Boolean success = Int64.TryParse
		(
			s,
			provider,
			out Int64 value
		);

		result = success ? value : default;

		return success;
	}

	/// <inheritdoc/>
	static Boolean IParsable<Snowflake>.TryParse
	(
		String? s,
		IFormatProvider? provider,
		out Snowflake result
	)
	{
		Boolean success = Int64.TryParse
		(
			s,
			provider,
			out Int64 value
		);

		result = success ? value : default;

		return success;
	}

	/// <inheritdoc/>
	static Boolean IBinaryInteger<Snowflake>.TryReadBigEndian
	(
		ReadOnlySpan<Byte> source,
		Boolean isUnsigned,
		out Snowflake value
	)
	{
		if(source.Length < 8)
		{
			value = default;
			return false;
		}

		Int64 result = Unsafe.ReadUnaligned<Int64>
		(
			ref MemoryMarshal.GetReference(source)
		);

		value = BitConverter.IsLittleEndian
			? BinaryPrimitives.ReverseEndianness(result)
			: result;

		return true;
	}

	/// <inheritdoc/>
	static Boolean IBinaryInteger<Snowflake>.TryReadLittleEndian
	(
		ReadOnlySpan<Byte> source,
		Boolean isUnsigned,
		out Snowflake value
	)
	{
		if(source.Length < 8)
		{
			value = default;
			return false;
		}

		Int64 result = Unsafe.ReadUnaligned<Int64>
		(
			ref MemoryMarshal.GetReference(source)
		);

		value = BitConverter.IsLittleEndian
			? result
			: BinaryPrimitives.ReverseEndianness(result);

		return true;
	}

	/// <inheritdoc cref="IComparable.CompareTo(Object?)"/>
	public Int32 CompareTo
	(
		Object? obj
	)
		=> this.Value.CompareTo(obj);

	/// <inheritdoc cref="IFormattable.ToString(String?, IFormatProvider?)"/>
	public String ToString
	(
		String? format,
		IFormatProvider? formatProvider = null
	)
		=> this.Value.ToString(format, formatProvider);

	/// <inheritdoc cref="ISpanFormattable.TryFormat(Span{Char}, out Int32, ReadOnlySpan{Char}, IFormatProvider?)"/>
	public Boolean TryFormat
	(
		Span<Char> destination,
		out Int32 charsWritten,
		ReadOnlySpan<Char> format,
		IFormatProvider? provider = null
	)
	{
		return this.Value.TryFormat
		(
			destination,
			out charsWritten,
			format,
			provider
		);
	}

	/// <inheritdoc/>
	Int32 IComparable.CompareTo
	(
		Object? obj
	)
		=> this.Value.CompareTo(obj);

	/// <inheritdoc/>
	Int32 IComparable<Snowflake>.CompareTo
	(
		Snowflake other
	)
		=> this.Value.CompareTo(other);

	/// <inheritdoc/>
	Boolean IEquatable<Snowflake>.Equals
	(
		Snowflake other
	)
		=> this.Value.Equals(other);

	/// <inheritdoc/>
	Int32 IBinaryInteger<Snowflake>.GetByteCount()
		=> 8;

	/// <inheritdoc/>
	Int32 IBinaryInteger<Snowflake>.GetShortestBitLength()
		=> 64 - BitOperations.LeadingZeroCount((UInt64)this.Value);

	/// <inheritdoc/>
	String IFormattable.ToString
	(
		String? format,
		IFormatProvider? formatProvider
	)
	{
		return this.ToString
		(
			format,
			formatProvider
		);
	}

	/// <inheritdoc/>
	Boolean ISpanFormattable.TryFormat
	(
		Span<Char> destination,
		out Int32 charsWritten,
		ReadOnlySpan<Char> format,
		IFormatProvider? provider
	)
	{
		return this.TryFormat
		(
			destination,
			out charsWritten,
			format,
			provider
		);
	}

	/// <inheritdoc/>
	Boolean IBinaryInteger<Snowflake>.TryWriteBigEndian
	(
		Span<Byte> destination,
		out Int32 bytesWritten
	)
	{
		if(destination.Length < 8)
		{
			bytesWritten = 0;
			return false;
		}

		Int64 value = BitConverter.IsLittleEndian
			? BinaryPrimitives.ReverseEndianness(this.Value)
			: this.Value;

		Unsafe.WriteUnaligned
		(
			ref MemoryMarshal.GetReference(destination),
			value
		);

		bytesWritten = 8;
		return true;
	}

	/// <inheritdoc/>
	Boolean IBinaryInteger<Snowflake>.TryWriteLittleEndian
	(
		Span<Byte> destination,
		out Int32 bytesWritten
	)
	{
		if(destination.Length < 8)
		{
			bytesWritten = 0;
			return false;
		}

		Int64 value = !BitConverter.IsLittleEndian
			? BinaryPrimitives.ReverseEndianness(this.Value)
			: this.Value;

		Unsafe.WriteUnaligned
		(
			ref MemoryMarshal.GetReference(destination),
			value
		);

		bytesWritten = 8;
		return true;
	}

	/// <inheritdoc/>
	static Snowflake IUnaryPlusOperators<Snowflake, Snowflake>.operator +
	(
		Snowflake value
	)
		=> +value.Value;

	/// <inheritdoc/>
	public static Snowflake operator +
	(
		Snowflake left,
		Snowflake right
	)
		=> left.Value + right.Value;

	/// <inheritdoc/>
	static Snowflake IAdditionOperators<Snowflake, Snowflake, Snowflake>.operator +
	(
		Snowflake left,
		Snowflake right
	)
		=> left.Value + right.Value;

	static Snowflake IUnaryNegationOperators<Snowflake, Snowflake>.operator -
	(
		Snowflake value
	)
		=> -value.Value;

	/// <inheritdoc/>
	public static Snowflake operator -
	(
		Snowflake left,
		Snowflake right
	)
		=> left.Value - right.Value;

	/// <inheritdoc/>
	static Snowflake ISubtractionOperators<Snowflake, Snowflake, Snowflake>.operator -
	(
		Snowflake left,
		Snowflake right
	)
		=> left.Value - right.Value;

	/// <inheritdoc/>
	static Snowflake IBitwiseOperators<Snowflake, Snowflake, Snowflake>.operator ~
	(
		Snowflake value
	)
		=> ~value.Value;

	/// <inheritdoc/>
	public static Snowflake operator ++
	(
		Snowflake value
	)
		=> value.Value + 1;

	/// <inheritdoc/>
	static Snowflake IIncrementOperators<Snowflake>.operator ++
	(
		Snowflake value
	)
		=> value.Value + 1;

	/// <inheritdoc/>
	public static Snowflake operator --
	(
		Snowflake value
	)
		=> value.Value - 1;

	/// <inheritdoc/>
	static Snowflake IDecrementOperators<Snowflake>.operator --
	(
		Snowflake value
	)
		=> value.Value - 1;

	/// <inheritdoc/>
	static Snowflake IMultiplyOperators<Snowflake, Snowflake, Snowflake>.operator *
	(
		Snowflake left,
		Snowflake right
	)
		=> left.Value * right.Value;

	/// <inheritdoc/>
	static Snowflake IDivisionOperators<Snowflake, Snowflake, Snowflake>.operator /
	(
		Snowflake left,
		Snowflake right
	)
		=> left.Value / right.Value;

	/// <inheritdoc/>
	static Snowflake IModulusOperators<Snowflake, Snowflake, Snowflake>.operator %
	(
		Snowflake left,
		Snowflake right
	)
		=> left.Value % right.Value;

	/// <inheritdoc/>
	static Snowflake IBitwiseOperators<Snowflake, Snowflake, Snowflake>.operator &
	(
		Snowflake left,
		Snowflake right
	)
		=> left.Value & right.Value;

	/// <inheritdoc/>
	static Snowflake IBitwiseOperators<Snowflake, Snowflake, Snowflake>.operator |
	(
		Snowflake left,
		Snowflake right
	)
		=> left.Value | right.Value;

	/// <inheritdoc/>
	static Snowflake IBitwiseOperators<Snowflake, Snowflake, Snowflake>.operator ^
	(
		Snowflake left,
		Snowflake right
	)
		=> left.Value ^ right.Value;

	/// <inheritdoc/>
	static Snowflake IShiftOperators<Snowflake, Int32, Snowflake>.operator <<
	(
		Snowflake value,
		Int32 shiftAmount
	)
		=> value.Value << shiftAmount;

	/// <inheritdoc/>
	static Snowflake IShiftOperators<Snowflake, Int32, Snowflake>.operator >>
	(
		Snowflake value,
		Int32 shiftAmount
	)
		=> value.Value >> shiftAmount;

	/// <inheritdoc/>
	static Boolean IEqualityOperators<Snowflake, Snowflake, Boolean>.operator ==
	(
		Snowflake left,
		Snowflake right
	)
		=> left.Value == right.Value;

	/// <inheritdoc/>
	static Boolean IEqualityOperators<Snowflake, Snowflake, Boolean>.operator !=
	(
		Snowflake left,
		Snowflake right
	)
		=> left.Value != right.Value;

	/// <inheritdoc/>
	static Boolean IComparisonOperators<Snowflake, Snowflake, Boolean>.operator <
	(
		Snowflake left,
		Snowflake right
	)
		=> left.Value < right.Value;

	/// <inheritdoc/>
	static Boolean IComparisonOperators<Snowflake, Snowflake, Boolean>.operator >
	(
		Snowflake left,
		Snowflake right
	)
		=> left.Value > right.Value;

	/// <inheritdoc/>
	static Boolean IComparisonOperators<Snowflake, Snowflake, Boolean>.operator <=
	(
		Snowflake left,
		Snowflake right
	)
		=> left.Value <= right.Value;

	/// <inheritdoc/>
	static Boolean IComparisonOperators<Snowflake, Snowflake, Boolean>.operator >=
	(
		Snowflake left,
		Snowflake right
	)
		=> left.Value >= right.Value;

	/// <inheritdoc/>
	static Snowflake IShiftOperators<Snowflake, Int32, Snowflake>.operator >>>
	(
		Snowflake value,
		Int32 shiftAmount
	)
		=> value.Value >>> shiftAmount;
}
