namespace Starnight.Extensions.Caching.Update.Internal;

using Starnight.Internal.Entities.Users;
using Starnight.SourceGenerators.Caching;

internal partial class UpdateUserWrapper
{
	[CacheUpdateMethod]
	public static partial DiscordUser UpdateDiscordUser
	(
		DiscordUser current,
		DiscordUser update
	);
}
