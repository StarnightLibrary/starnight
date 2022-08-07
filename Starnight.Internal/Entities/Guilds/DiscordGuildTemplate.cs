namespace Starnight.Internal.Entities.Guilds;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Users;

/// <summary>
/// Represents a discord guild template.
/// </summary>
public sealed record DiscordGuildTemplate
{
	/// <summary>
	/// Template code, in the same form as invite codes.
	/// </summary>
	[JsonPropertyName("code")]
	public required String Code { get; init; }

	/// <summary>
	/// The template name.
	/// </summary>
	[JsonPropertyName("name")]
	public required String Name { get; init; }

	/// <summary>
	/// The template description.
	/// </summary>
	[JsonPropertyName("description")]
	public String? Description { get; init; }

	/// <summary>
	/// The number of times this template has been used.
	/// </summary>
	[JsonPropertyName("usage_count")]
	public required Int32 UseCount { get; init; }

	/// <summary>
	/// Snowflake identifier of the user who created this template.
	/// </summary>
	[JsonPropertyName("creator_id")]
	public required Int64 CreatorId { get; init; }

	/// <summary>
	/// User object for the user who created this template.
	/// </summary>
	[JsonPropertyName("creator")]
	public required DiscordUser Creator { get; init; }

	/// <summary>
	/// Stores when this template was created.
	/// </summary>
	[JsonPropertyName("created_at")]
	public required DateTimeOffset CreatedAt { get; init; }

	/// <summary>
	/// Stores when this template was last updated.
	/// </summary>
	[JsonPropertyName("updated_at")]
	public required DateTimeOffset UpdatedAt { get; init; }

	/// <summary>
	/// Snowflake identifier of the guild this template is based on.
	/// </summary>
	[JsonPropertyName("source_guild_id")]
	public required Int64 SourceGuildId { get; init; }

	/// <summary>
	/// The guild snapshot this template represents.
	/// </summary>
	[JsonPropertyName("serialized_source_guild")]
	public required DiscordGuild SourceGuild { get; init; }

	/// <summary>
	/// Whether this template has unsynced changes.
	/// </summary>
	[JsonPropertyName("is_dirty")]
	public Boolean? Dirty { get; init; }
}
