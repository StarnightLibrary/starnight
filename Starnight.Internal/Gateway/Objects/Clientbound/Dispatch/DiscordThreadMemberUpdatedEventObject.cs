namespace Starnight.Internal.Gateway.Objects.Clientbound.Dispatch;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Channels.Threads;

/// <summary>
/// Represents a dispatch payload for ThreadMemberUpdated.
/// </summary>
public sealed record DiscordThreadMemberUpdatedEventObject : DiscordThreadMember
{
	/// <summary>
	/// The ID of the guild this event is fired from.
	/// </summary>
	[JsonPropertyName("guild_id")]
	public required Int64 GuildId { get; init; }
}
