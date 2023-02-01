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
public sealed record DiscordInteractionResolvedData
{
	/// <summary>
	/// Maps snowflake identifiers to <see cref="DiscordUser"/>s.
	/// </summary>
	[JsonPropertyName("users")]
	public Optional<IDictionary<Int64, DiscordUser>> ResolvedUsers { get; init; }

	/// <summary>
	/// Maps snowflake identifiers to <see cref="DiscordGuildMember"/>s.
	/// </summary>
	[JsonPropertyName("members")]
	public Optional<IDictionary<Int64, DiscordGuildMember>> ResolvedGuildMembers { get; init; }

	/// <summary>
	/// Maps snowflake identifiers to <see cref="DiscordRole"/>s.
	/// </summary>
	[JsonPropertyName("roles")]
	public Optional<IDictionary<Int64, DiscordRole>> ResolvedRoles { get; init; }

	/// <summary>
	/// Maps snowflake identifiers to <see cref="DiscordChannel"/>s.
	/// </summary>
	[JsonPropertyName("channels")]
	public Optional<IDictionary<Int64, DiscordChannel>> ResolvedChannels { get; init; }

	/// <summary>
	/// Maps snowflake identifiers to <see cref="DiscordMessage"/>s.
	/// </summary>
	[JsonPropertyName("messages")]
	public Optional<IDictionary<Int64, DiscordMessage>> ResolvedMessages { get; init; }

	/// <summary>
	/// Maps snowflake identifiers to <see cref="DiscordMessageAttachment"/>s as received with this interaction.
	/// </summary>
	[JsonPropertyName("attachments")]
	public Optional<IDictionary<Int64, DiscordMessageAttachment>> ResolvedAttachments { get; init; }
}
