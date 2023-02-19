namespace Starnight;

using System;

/// <summary>
/// Represents a discord snowflake; the type discord uses for IDs first and foremost.
/// </summary>
public readonly partial record struct Snowflake :
	IComparable<Snowflake>
{ 
	/// <summary>
	/// The discord epoch; the start of 2015. All snowflakes are based upon this time.
	/// </summary>
	public static readonly DateTimeOffset DiscordEpoch = new(2015, 1, 1, 0, 0, 0, TimeSpan.Zero);

	/// <summary>
	/// The snowflake's underlying value.
	/// </summary>
	public Int64 Value { get; }

	/// <summary>
	/// The time when this snowflake was created.
	/// </summary>
	public DateTimeOffset Timestamp => DiscordEpoch.AddMilliseconds
	(
		this.Value >> 22
	);

	/// <summary>
	/// The internal worker's ID that was used to generate the snowflake.
	/// </summary>
	public Byte InternalWorkerId => (Byte)((this.Value & 0x3E0000) >> 17);

	/// <summary>
	/// The internal process' ID that was used to generate the snowflake.
	/// </summary>
	public Byte InternalProcessId => (Byte)((this.Value & 0x1F000) >> 12);

	/// <summary>
	/// The internal worker-specific and process-specific increment.
	/// </summary>
	public UInt16 InternalIncrement => (UInt16)(this.Value & 0xFFF);

	/// <summary>
	/// Creates a new snowflake from a given integer.
	/// </summary>
	/// <param name="value">The numerical representation to translate from.</param>
	public Snowflake(Int64 value)
		=> this.Value = value;

	/// <summary>
	/// Creates a fake snowflake from scratch. If no parameters are provided, returns a newly generated snowflake.
	/// </summary>
	/// <remarks>
	/// If a value larger than allowed is supplied for the three numerical parameters, it will be cut off at
	/// the maximum allowed value.
	/// </remarks>
	/// <param name="timestamp">
	/// The date when the snowflake was created. If null, this defaults to the current time.
	/// </param>
	/// <param name="workerId">
	/// A 5 bit worker id that was used to create the snowflake. If null, generates a random number between 0 and 31.
	/// </param>
	/// <param name="processId">
	/// A 5 bit process id that was used to create the snowflake. If null, generates a random number between 0 and 31.
	/// </param>
	/// <param name="increment">
	/// A 12 bit integer which represents the number of previously generated snowflakes in the given context.
	/// If null, generates a random number between 0 and 4,095.
	/// </param>
	public Snowflake
	(
		DateTimeOffset? timestamp = null,
		Byte? workerId = null,
		Byte? processId = null,
		UInt16? increment = null
	)
	{
		timestamp ??= DateTimeOffset.Now;
		workerId ??= (Byte)Random.Shared.Next(0, 32);
		processId ??= (Byte)Random.Shared.Next(0, 32);
		increment ??= (UInt16)Random.Shared.Next(0, 4095);

		this.Value = ((UInt32)timestamp.Value.Subtract(DiscordEpoch).TotalMilliseconds << 22)
			| ((Int64)workerId.Value << 17)
			| ((Int64)processId.Value << 12)
			| increment.Value;
	}

	public Int32 CompareTo
	(
		Snowflake other
	)
		=> this.Value.CompareTo(other.Value);

	public static Boolean operator <(Snowflake left, Snowflake right) => left.Value < right.Value;
	public static Boolean operator <=(Snowflake left, Snowflake right) => left.Value <= right.Value;
	public static Boolean operator >(Snowflake left, Snowflake right) => left.Value > right.Value;
	public static Boolean operator >=(Snowflake left, Snowflake right) => left.Value >= right.Value;
	public static implicit operator Int64(Snowflake snowflake) => snowflake.Value;
	public static implicit operator Snowflake(Int64 value) => value;
}
