namespace Starnight.Internal.Entities.Channels;

/// <summary>
/// Represents flag added to a <see cref="DiscordChannel"/>.
/// </summary>
public enum DiscordChannelFlags
{
	/// <summary>
	/// Indicates that this thread channel is pinned to the top of its parent forum channel.
	/// </summary>
	Pinned = 1 << 1
}
