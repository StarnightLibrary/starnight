namespace Starnight.Internal.Entities.Guild;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.User;

/// <summary>
/// Represents a discord guild template.
/// </summary>
public class DiscordGuildTemplate
{
	/// <summary>
	/// Template code, in the same form as invite codes.
	/// </summary>
	[JsonPropertyName("code")]
	public String Code { get; init; } = default!;

	/// <summary>
	/// The template name.
	/// </summary>
	[JsonPropertyName("name")]
	public String Name { get; init; } = default!;

	/// <summary>
	/// The template description.
	/// </summary>
	[JsonPropertyName("description")]
	public String? Description { get; init; }

	/// <summary>
	/// The number of times this template has been used.
	/// </summary>
	[JsonPropertyName("usage_count")]
	public Int32 UseCount { get; init; }

	/// <summary>
	/// Snowflake identifier of the user who created this template.
	/// </summary>
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("creator_id")]
	public Int64 CreatorId { get; init; }

	/// <summary>
	/// User object for the user who created this template.
	/// </summary>
	[JsonPropertyName("creator")]
	public DiscordUser Creator { get; init; } = default!;

	/// <summary>
	/// Stores when this template was created.
	/// </summary>
	[JsonPropertyName("created_at")]
	public DateTime CreatedAt { get; init; }

	/// <summary>
	/// Stores when this template was last updated.
	/// </summary>
	[JsonPropertyName("updated_at")]
	public DateTime UpdatedAt { get; init; }

	/// <summary>
	/// Snowflake identifier of the guild this template is based on.
	/// </summary>
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("source_guild_id")]
	public Int64 SourceGuildId { get; init; }

	/// <summary>
	/// The guild snapshot this template represents.
	/// </summary>
	[JsonPropertyName("serialized_source_guild")]
	public DiscordGuild SourceGuild { get; init; } = default!;

	/// <summary>
	/// Whether this template has unsynced changes.
	/// </summary>
	[JsonPropertyName("is_dirty")]
	public Boolean? Dirty { get; init; }
}
