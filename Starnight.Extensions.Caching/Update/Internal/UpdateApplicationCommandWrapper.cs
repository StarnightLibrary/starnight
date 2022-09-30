namespace Starnight.Extensions.Caching.Update.Internal;

using Starnight.Internal.Entities.Interactions.ApplicationCommands;
using Starnight.SourceGenerators.Caching;

internal partial class UpdateApplicationCommandWrapper
{
	[CacheUpdateMethod]
	public partial DiscordApplicationCommand UpdateDiscordApplicationCommand
	(
		DiscordApplicationCommand current,
		DiscordApplicationCommand update
	);

	[CacheUpdateMethod]
	public partial DiscordApplicationCommandOption UpdateDiscordApplicationCommandOption
	(
		DiscordApplicationCommandOption current,
		DiscordApplicationCommandOption update
	);
}
