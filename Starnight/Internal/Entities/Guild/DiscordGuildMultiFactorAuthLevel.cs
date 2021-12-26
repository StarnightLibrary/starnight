namespace Starnight.Internal.Entities.Guild;

/// <summary>
/// Represents the different levels of guild moderation Multi-Factor Auth.
/// </summary>
public enum DiscordGuildMultiFactorAuthLevel
{
	/// <summary>
	/// No MFA requirements for moderation actions.
	/// </summary>
	None,

	/// <summary>
	/// >= 2 factor auth is required for moderation actions.
	/// </summary>
	Elevated
}
