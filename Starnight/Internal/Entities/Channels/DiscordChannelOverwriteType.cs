namespace Starnight.Internal.Entities.Channels;

/// <summary>
/// Represents the two different channel overwrite types.
/// </summary>
public enum DiscordChannelOverwriteType
{
	/// <summary>
	/// Indicates this overwrite targets a role.
	/// </summary>
	Role,

	/// <summary>
	/// Indicates this overwrite targets a member.
	/// </summary>
	Member
}
