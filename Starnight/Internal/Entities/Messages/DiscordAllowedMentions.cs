namespace Starnight.Internal.Entities.Messages;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

/// <summary>
/// Represents information about what mentions in a message should be resolved to mentions.
/// </summary>
public record DiscordAllowedMentions
{
	/// <summary>
	/// An array of allowed mention types to parse from the context.
	/// Can contain <c>roles</c>, <c>users</c> and <c>everyone</c>.
	/// </summary>
	[JsonPropertyName("parse")]
	public IEnumerable<String>? Parse { get; init; }

	/// <summary>
	/// An array of up to 100 snowflake IDs of roles to parse from the context.
	/// </summary>
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("roles")]
	public IEnumerable<Int64>? Roles { get; init; }

	/// <summary>
	/// An array of up to 100 snowflake IDs of users to parse from the context.
	/// </summary>
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("users")]
	public IEnumerable<Int64>? Users { get; init; }

	/// <summary>
	/// Whether the user this message replies to should be mentioned.
	/// </summary>
	[JsonPropertyName("replied_user")]
	public Boolean RepliedUser { get; init; }
}
