namespace Starnight.Internal.Entities.Interactions;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Channels;
using Starnight.Internal.Entities.Guilds;
using Starnight.Internal.Entities.Messages;
using Starnight.Internal.Entities.Users;

/// <summary>
/// Stores resolved data about an interaction.
/// </summary>
public record DiscordInteractionResolvedData
{
	/// <summary>
	/// Maps snowflake identifiers (in string form) to <see cref="DiscordUser"/>s.
	/// </summary>
	[JsonPropertyName("users")]
	public IDictionary<String, DiscordUser>? ResolvedUsers { get; init; }

	/// <summary>
	/// Maps snowflake identifiers (in string form) to <see cref="DiscordGuildMember"/>s.
	/// </summary>
	[JsonPropertyName("members")]
	public IDictionary<String, DiscordGuildMember>? ResolvedGuildMembers { get; init; }

	/// <summary>
	/// Maps snowflake identifiers (in string form) to <see cref="DiscordRole"/>s.
	/// </summary>
	[JsonPropertyName("roles")]
	public IDictionary<String, DiscordRole>? ResolvedRoles { get; init; }

	/// <summary>
	/// Maps snowflake identifiers (in string form) to <see cref="DiscordChannel"/>s.
	/// </summary>
	[JsonPropertyName("channels")]
	public IDictionary<String, DiscordChannel>? ResolvedChannels { get; init; }

	/// <summary>
	/// Maps snowflake identifiers (in string form) to <see cref="DiscordMessage"/>s.
	/// </summary>
	[JsonPropertyName("messages")]
	public IDictionary<String, DiscordMessage>? ResolvedMessages { get; init; }
}
