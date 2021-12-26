namespace Starnight.Internal.Entities.Team;

/// <summary>
/// Represents the different membership states for each <see cref="DiscordTeamMember"/>.
/// </summary>
public enum DiscordTeamMembershipState
{
	/// <summary>
	/// Invited, pending acceptance.
	/// </summary>
	Invited = 1,

	/// <summary>
	/// Accepted.
	/// </summary>
	Accepted
}
