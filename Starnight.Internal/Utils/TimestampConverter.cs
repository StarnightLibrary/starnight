namespace Starnight.Internal.Utils;

using System;

using Starnight.Internal.Entities;

/// <summary>
/// Extension method holder to extract data from Snowflakes.
/// </summary>
public static class SnowflakeConverter
{
	private static readonly DateTimeOffset __discordepoch = new(2015, 1, 1, 0, 0, 0, TimeSpan.Zero);

	/// <summary>
	/// Extracts the Creation timestamp from a raw snowflake ID.
	/// </summary>
	/// <param name="snowflake">A raw snowflake ID as <see cref="Int64"/>.</param>
	/// <returns>A <see cref="DateTimeOffset"/> holding the requested timestamp.</returns>
	public static DateTimeOffset GetSnowflakeTime(this Int64 snowflake)
		=> __discordepoch.AddMilliseconds(snowflake >> 22);

	/// <summary>
	/// Extracts the Creation timestamp from a wrapped snowflake.
	/// </summary>
	/// <param name="snowflake">A wrapped snowflake as <see cref="DiscordSnowflakeObject"/>.</param>
	/// <returns>A <see cref="DateTimeOffset"/> holding the requested timestamp.</returns>
	public static DateTimeOffset GetSnowflakeTime(this DiscordSnowflakeObject snowflake)
		=> __discordepoch.AddMilliseconds(snowflake.Id >> 22);

	/// <summary>
	/// Extracts the internal worker ID from a raw snowflake ID.
	/// </summary>
	/// <param name="snowflake">A raw snowflake ID as <see cref="Int64"/>.</param>
	/// <returns>A <see cref="Byte"/> holding the internal worker ID as used by Discord.</returns>
	public static Byte GetInternalWorkerId(this Int64 snowflake)
		=> (Byte)((snowflake & 0x3E0000) >> 17);

	/// <summary>
	/// Extracts the internal worker ID from a wrapped snowflake.
	/// </summary>
	/// <param name="snowflake">A raw snowflake ID as <see cref="DiscordSnowflakeObject"/>.</param>
	/// <returns>A <see cref="Byte"/> holding the internal worker ID as used by Discord.</returns>
	public static Byte GetInternalWorkerId(this DiscordSnowflakeObject snowflake)
		=> (Byte)((snowflake.Id & 0x3E0000) >> 17);

	/// <summary>
	/// Extracts the internal process ID from a raw snowflake ID.
	/// </summary>
	/// <param name="snowflake">A raw snowflake ID as <see cref="Int64"/>.</param>
	/// <returns>A <see cref="Byte"/> holding the internal process ID as used by Discord.</returns>
	public static Byte GetInternalProcessId(this Int64 snowflake)
		=> (Byte)((snowflake & 0x1F000) >> 12);

	/// <summary>
	/// Extracts the internal process ID from a wrapped snowflake.
	/// </summary>
	/// <param name="snowflake">A raw snowflake ID as <see cref="DiscordSnowflakeObject"/>.</param>
	/// <returns>A <see cref="Byte"/> holding the internal process ID as used by Discord.</returns>
	public static Byte GetInternalProcessId(this DiscordSnowflakeObject snowflake)
		=> (Byte)((snowflake.Id & 0x1F000) >> 12);

	/// <summary>
	/// Extracts the internal increment from a raw snowflake ID. This number is incremented every time a new ID
	/// is generated on the process as obtainable from <see cref="GetInternalProcessId(Int64)"/>.
	/// </summary>
	/// <param name="snowflake">A raw snowflake ID as <see cref="Int64"/>.</param>
	/// <returns>A <see cref="Byte"/> holding the internal increment as used by Discord.</returns>
	public static UInt16 GetIncrement(this Int64 snowflake)
		=> (UInt16)(snowflake & 0xFFF);

	/// <summary>
	/// Extracts the internal increment from a wrapped snowflake. This number is incremented every time a new ID
	/// is generated on the process as obtainable from <see cref="GetInternalProcessId(DiscordSnowflakeObject)"/>.
	/// </summary>
	/// <param name="snowflake">A wrapped snowflake as <see cref="DiscordSnowflakeObject"/>.</param>
	/// <returns>A <see cref="Byte"/> holding the internal increment as used by Discord.</returns>
	public static UInt16 GetIncrement(this DiscordSnowflakeObject snowflake)
		=> (UInt16)(snowflake.Id & 0xFFF);
}
