namespace Starnight.Internal.Gateway.Payloads.Inbound.Dispatch;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Guilds;
using Starnight.Internal.Entities.Users;
using Starnight.Internal.Gateway.Events.Outbound;

/// <summary>
/// Represents the payload of the GuildMembersChunk event.
/// </summary>
public sealed record GuildMembersChunkPayload
{
	/// <summary>
	/// Snowflake ID of the guild in question.
	/// </summary>
	[JsonPropertyName("guild_id")]
	public required Int64 GuildId { get; init; }

	/// <summary>
	/// This chunk of guild members.
	/// </summary>
	[JsonPropertyName("members")]
	public required IEnumerable<DiscordGuildMember> Members { get; init; }

	/// <summary>
	/// The index of this chunk in the response.
	/// </summary>
	[JsonPropertyName("chunk_index")]
	public required Int32 ChunkIndex { get; init; }

	/// <summary>
	/// The total number of expected chunks for this response.
	/// </summary>
	[JsonPropertyName("chunk_count")]
	public required Int32 ChunkCount { get; init; }

	/// <summary>
	/// Invalid/not found IDs passed to <see cref="DiscordRequestGuildMembersEvent"/> will be returned here.
	/// </summary>
	[JsonPropertyName("not_found")]
	public Optional<IEnumerable<Int64>> NotFound { get; init; }

	/// <summary>
	/// Presences of the returned members.
	/// </summary>
	[JsonPropertyName("presences")]
	public Optional<IEnumerable<DiscordPresence>> Presences { get; init; }

	/// <summary>
	/// The nonce passed to <see cref="DiscordRequestGuildMembersEvent"/>, used for identifying responses.
	/// </summary>
	[JsonPropertyName("nonce")]
	public Optional<String> Nonce { get; init; }
}
