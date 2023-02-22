namespace Starnight.Entities;

/// <summary>
/// Represents the different possible verification levels for a guild.
/// </summary>
public enum DiscordGuildVerificationLevel
{
	/// <summary>
	/// Unrestricted.
	/// </summary>
	None,

	/// <summary>
	/// Must have a verified email address.
	/// </summary>
	Low,

	/// <summary>
	/// Must be registered on Discord for over 5 minutes.
	/// </summary>
	Medium,

	/// <summary>
	/// Must be a member of the guild for over 10 minutes.
	/// </summary>
	High,

	/// <summary>
	/// Must have a verified phone number.
	/// </summary>
	VeryHigh
}
