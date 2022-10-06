namespace Starnight.Extensions.Caching.Update.Internal;

using Starnight.Internal.Entities.Interactions.ApplicationCommands;
using Starnight.SourceGenerators.Caching;

internal partial class UpdateApplicationCommandWrapper
{
	[CacheUpdateMethod]
	public static partial DiscordApplicationCommand UpdateDiscordApplicationCommand
	(
		DiscordApplicationCommand current,
		DiscordApplicationCommand update
	);
}
