namespace Starnight.Internal.Entities.Messages;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

/// <summary>
/// Represents information about what mentions in a message should be resolved to mentions.
/// </summary>
public sealed record DiscordAllowedMentions
{
	/// <summary>
	/// An array of allowed mention types to parse from the context.
	/// Can contain <c>roles</c>, <c>users</c> and <c>everyone</c>.
	/// </summary>
	[JsonPropertyName("parse")]
	public Optional<IEnumerable<String>> Parse { get; init; }

	/// <summary>
	/// An array of up to 100 snowflake IDs of roles to parse from the context.
	/// </summary>
	[JsonPropertyName("roles")]
	public Optional<IEnumerable<Int64>> Roles { get; init; }

	/// <summary>
	/// An array of up to 100 snowflake IDs of users to parse from the context.
	/// </summary>
	[JsonPropertyName("users")]
	public Optional<IEnumerable<Int64>> Users { get; init; }

	/// <summary>
	/// Whether the user this message replies to should be mentioned.
	/// </summary>
	[JsonPropertyName("replied_user")]
	public Optional<Boolean> RepliedUser { get; init; }
}
