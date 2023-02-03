namespace Starnight.Internal.Entities.Users;

/// <summary>
/// Holds the visibility status of Discord User connections.
/// </summary>
public enum DiscordUserConnectionVisibility
{
	/// <summary>
	/// Invisible to everyone save the applicable user themselves.
	/// </summary>
	None,

	/// <summary>
	/// Visible to everyone.
	/// </summary>
	Everyone
}
