namespace Starnight.Internal.Entities;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Extensions;

/// <summary>
/// Base class for all Snowflake objects. Used chiefly for REST purposes and basic abstraction. Do not use directly.
/// One ID can only refer to one specific object, Discord ensures that.
/// </summary>
public record DiscordSnowflakeObject : IConvertible, IEquatable<DiscordSnowflakeObject>
{
	/// <summary>
	/// Snowflake Identifier of this object.
	/// </summary>
	[JsonPropertyName("id")]
	public required Int64 Id { get; init; }

	/// <summary>
	/// Creation date of this object.
	/// </summary>
	[JsonIgnore]
	public DateTimeOffset Creation => this.Id.GetSnowflakeTime();

	/// <summary>
	/// Gets whether this Snowflake equals another.
	/// </summary>
	/// <returns><see langword="false"/> if one is null or the objects do not match</returns>
	public virtual Boolean Equals(DiscordSnowflakeObject? other)
		=> (this is null || other is not null) && this.Id == other!.Id;

	public TypeCode GetTypeCode() => TypeCode.Int64;

	/// <summary>
	/// Always throws InvalidCastException.
	/// </summary>
	public Boolean ToBoolean(IFormatProvider? provider) => throw new InvalidCastException();

	/// <summary>
	/// Always throws InvalidCastException.
	/// </summary>
	public Byte ToByte(IFormatProvider? provider) => throw new InvalidCastException();

	/// <summary>
	/// Always throws InvalidCastException.
	/// </summary>
	public Char ToChar(IFormatProvider? provider) => throw new InvalidCastException();

	public DateTime ToDateTime(IFormatProvider? provider) => Convert.ToDateTime(this.Creation, provider);

	public Decimal ToDecimal(IFormatProvider? provider) => this.Id;

	public Double ToDouble(IFormatProvider? provider) => this.Id;

	/// <summary>
	/// Always throws InvalidCastException.
	/// </summary>
	public Int16 ToInt16(IFormatProvider? provider) => throw new InvalidCastException();

	/// <summary>
	/// Always throws InvalidCastException.
	/// </summary>
	public Int32 ToInt32(IFormatProvider? provider) => throw new InvalidCastException();

	public Int64 ToInt64(IFormatProvider? provider) => this.Id;

	/// <summary>
	/// Always throws InvalidCastException.
	/// </summary>
	public SByte ToSByte(IFormatProvider? provider) => throw new InvalidCastException();

	public Single ToSingle(IFormatProvider? provider) => this.Id;

	public String ToString(IFormatProvider? provider) => this.Id.ToString();

	/// <summary>
	/// Always throws InvalidCastException.
	/// </summary>
	public Object ToType(Type conversionType, IFormatProvider? provider) => throw new InvalidCastException();

	/// <summary>
	/// Always throws InvalidCastException.
	/// </summary>
	public UInt16 ToUInt16(IFormatProvider? provider) => throw new InvalidCastException();

	/// <summary>
	/// Always throws InvalidCastException.
	/// </summary>
	public UInt32 ToUInt32(IFormatProvider? provider) => throw new InvalidCastException();

	public UInt64 ToUInt64(IFormatProvider? provider) => Convert.ToUInt64(this.Id, provider);

	/// <summary>
	/// Returns the hash code of the snowflake Id
	/// </summary>
	public override Int32 GetHashCode()
		=> this.Id.GetHashCode();
}
