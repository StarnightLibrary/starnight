namespace Starnight.Internal.Entities.Guilds;

/// <summary>
/// Holds the possible integration expiration behaviour values.
/// </summary>
public enum DiscordGuildIntegrationExpireBehaviour
{
	/// <summary>
	/// Once the integration expires, the role(s) will be removed.
	/// </summary>
	RemoveRole,

	/// <summary>
	/// Once the integration expires, the user will be kicked.
	/// </summary>
	Kick
}
