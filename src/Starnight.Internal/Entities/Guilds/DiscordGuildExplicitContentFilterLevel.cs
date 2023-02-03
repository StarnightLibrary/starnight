namespace Starnight.Internal.Entities.Guilds;

/// <summary>
/// Represents the different severity levels of the explicit content filter.
/// </summary>
public enum DiscordGuildExplicitContentFilterLevel
{
	/// <summary>
	/// Nothing will be scanned.
	/// </summary>
	Disabled,

	/// <summary>
	/// Messages by members without roles will be scanned.
	/// </summary>
	MembersWithoutRoles,

	/// <summary>
	/// Everything will be scanned.
	/// </summary>
	AllMembers
}
