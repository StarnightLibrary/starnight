namespace Starnight.Internal.Entities.Guilds;

using System;
using System.Text.Json.Serialization;

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
	/// <remarks>
	/// If <see cref="Optional{T}.HasValue"/> is <see langword="false"/>, the value represented by this field
	/// is false; if <see cref="Optional{T}.HasValue"/> is <see langword="true"/>,
	/// <see cref="Optional{T}.Value"/> will always be <see langword="null"/> indicating this field is true.
	/// </remarks>
	[JsonPropertyName("premium_subscriber")]
	public Optional<Object?> PremiumRole { get; init; }

	/// <summary>
	/// The ID of this role#s subscription SKU and listing.
	/// </summary>
	[JsonPropertyName("subscription_listing_id")]
	public Optional<Int64> SubscriptionListingId { get; init; }

	/// <summary>
	/// Whether this role is available for purchase.
	/// </summary>
	/// <remarks>
	/// If <see cref="Optional{T}.HasValue"/> is <see langword="false"/>, the value represented by this field
	/// is false; if <see cref="Optional{T}.HasValue"/> is <see langword="true"/>,
	/// <see cref="Optional{T}.Value"/> will always be <see langword="null"/> indicating this field is true.
	/// </remarks>
	[JsonPropertyName("available_for_purchase")]
	public Optional<Object?> AvailableForPurchase { get; init; }

	/// <summary>
	/// Whether this role is a guild's linked role.
	/// </summary>
	/// <remarks>
	/// If <see cref="Optional{T}.HasValue"/> is <see langword="false"/>, the value represented by this field
	/// is false; if <see cref="Optional{T}.HasValue"/> is <see langword="true"/>,
	/// <see cref="Optional{T}.Value"/> will always be <see langword="null"/> indicating this field is true.
	/// </remarks>
	[JsonPropertyName("guild_connections")]
	public Optional<Object?> GuildConnections { get; init; }
}
