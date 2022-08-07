namespace Starnight.Internal.Entities.Guilds;

using System.Text.Json.Serialization;
using System;

/// <summary>
/// Represents possible tags of a discord role.
/// </summary>
public sealed record DiscordRoleTags
{
	/// <summary>
	/// Snowflake identifier of the bot this role belongs to.
	/// </summary>
	[JsonPropertyName("bot_id")]
	public Optional<Int64> BotId { get; init; }

	/// <summary>
	/// Snowflake identifier of the integration this role belongs to.
	/// </summary>
	[JsonPropertyName("integration_id")]
	public Optional<Int64> IntegrationId { get; init; }

	/// <summary>
	/// Whether this is the guild's premium subscriber role.
	/// </summary>
	[JsonPropertyName("premium_subscriber")]
	public Optional<Boolean> PremiumRole { get; init; }
}
