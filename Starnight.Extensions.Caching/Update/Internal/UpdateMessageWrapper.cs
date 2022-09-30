namespace Starnight.Extensions.Caching.Update.Internal;

using Starnight.Internal.Entities.Messages;
using Starnight.SourceGenerators.Caching;

internal partial class UpdateMessageWrapper
{
	[CacheUpdateMethod]
	public partial DiscordMessage UpdateDiscordMessage
	(
		DiscordMessage current,
		DiscordMessage update
	);
}
