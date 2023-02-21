namespace Starnight.Entities;

using Starnight.Internal.Entities.Guilds.Audit;

/// <summary>
/// Enumerates the different types of <see cref="DiscordAuditLogEntryOptionalData"/> objects.
/// </summary>
public enum DiscordAuditLogEntryOptionalDataType
{
	/// <summary>
	/// This targets a role.
	/// </summary>
	Role,

	/// <summary>
	/// This targets a member.
	/// </summary>
	Member
}
