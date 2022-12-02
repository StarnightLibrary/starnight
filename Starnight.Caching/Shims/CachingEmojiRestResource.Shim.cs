namespace Starnight.Caching.Shims;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Starnight.Internal.Entities.Guilds;
using Starnight.Internal.Rest.Payloads.Emojis;
using Starnight.Internal.Rest.Resources;

/// <summary>
/// Represents a shim over the present emoji rest resource, updating and corroborating from
/// cache where possible.
/// </summary>
public partial class CachingEmojiRestResource : IDiscordEmojiRestResource
{
	public ValueTask<DiscordEmoji> CreateGuildEmojiAsync(Int64 guildId, CreateGuildEmojiRequestPayload payload, String? reason = null, CancellationToken ct = default) => throw new NotImplementedException();
	public ValueTask<Boolean> DeleteGuildEmojiAsync(Int64 guildId, Int64 emojiId, String? reason = null, CancellationToken ct = default) => throw new NotImplementedException();
	public ValueTask<DiscordEmoji> GetGuildEmojiAsync(Int64 guildId, Int64 emojiId, CancellationToken ct = default) => throw new NotImplementedException();
	public ValueTask<IEnumerable<DiscordEmoji>> ListGuildEmojisAsync(Int64 guildId, CancellationToken ct = default) => throw new NotImplementedException();
	public ValueTask<DiscordEmoji> ModifyGuildEmojiAsync(Int64 guildId, Int64 emojiId, ModifyGuildEmojiRequestPayload payload, String? reason = null, CancellationToken ct = default) => throw new NotImplementedException();
}
