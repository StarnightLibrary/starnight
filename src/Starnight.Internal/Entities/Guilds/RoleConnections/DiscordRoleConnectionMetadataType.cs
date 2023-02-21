namespace Starnight.Entities;

/// <summary>
/// Represents the different ways role connection data can be handled.
/// </summary>
public enum DiscordRoleConnectionMetadataType
{
	/// <summary>
	/// The integer-type metadata value is less than or equal to the guild's configured integer-type value.
	/// </summary>
	IntegerLessThanOrEqual = 1,

	/// <summary>
	/// The integer-type metadata value is greater than or equal to the guild's configured integer-type value.
	/// </summary>
	IntegerGreaterThanOrEqual,

	/// <summary>
	/// The integer-type metadata value is equal to the guild's configured integer-type value.
	/// </summary>
	IntegerEqual,

	/// <summary>
	/// The integer-type metadata value is not equal to the guild's configured integer-type value.
	/// </summary>
	IntegerNotEqual,

	/// <summary>
	/// The ISO8601 date/time typed metadata value is less than or equal to the guild's configured value,
	/// expressed in days before the current date as integer.
	/// </summary>
	DateTimeLessThanOrEqual,

	/// <summary>
	/// The ISO8601 date/time typed metadata value is greater than or equal to the guild's configured value,
	/// expressed in days before the current date as integer.
	/// </summary>
	DateTimeGreaterThanOrEqual,

	/// <summary>
	/// The boolean metadata value is true.
	/// </summary>
	BooleanEqual,

	/// <summary>
	/// The boolean metadata value is false.
	/// </summary>
	BooleanNotEqual
}
