namespace Starnight.Internal.Entities.Guilds;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Users;

/// <summary>
/// Reprents a Discord Guild Integration, for instance the Nitro Booster role
/// </summary>
public sealed record DiscordGuildIntegration : DiscordSnowflakeObject
{
	/// <summary>
	/// Integration name - i.e. for bots the Application name
	/// </summary>
	[JsonPropertyName("name")]
	public required String Name { get; init; }

	/// <summary>
	/// Integration type: twitch, youtube or discord
	/// </summary>
	[JsonPropertyName("type")]
	public required String Type { get; init; }

	/// <summary>
	/// Gets whether this integration is enabled
	/// </summary>
	[JsonPropertyName("enabled")]
	public required Boolean Enabled { get; init; }

	/// <summary>
	/// The integration account
	/// </summary>
	[JsonPropertyName("account")]
	public required DiscordGuildIntegrationAccount Account { get; init; }

	/// <summary>
	/// The bot or OAuth2 application for this integration.
	/// </summary>
	[JsonPropertyName("application")]
	public Optional<DiscordGuildIntegrationApplication> Application { get; init; }

	/// <summary>
	/// Gets whether this integration is syncing.
	/// </summary>
	[JsonPropertyName("syncing")]
	public Optional<Boolean> Synchronizing { get; init; }

	/// <summary>
	/// Snowflake identifier of the role this integration uses for "subscribers".
	/// </summary>
	[JsonPropertyName("role_id")]
	public Optional<Int64> RoleId { get; init; }

	/// <summary>
	/// Gets whether emojis should be synced for this integration.
	/// This field is only present for twitch integrations.
	/// </summary>
	[JsonPropertyName("enable_emoticons")]
	public Optional<Boolean> EnableEmojis { get; init; }

	/// <summary>
	/// Gets the behaviour of expiring subscribers.
	/// </summary>
	[JsonPropertyName("expire_behaviour")]
	public Optional<DiscordGuildIntegrationExpireBehaviour> ExpirationBehaviour { get; init; }

	/// <summary>
	/// The grace period in days after the subscription has ended before the <see cref="ExpirationBehaviour"/> comes into effect.
	/// </summary>
	[JsonPropertyName("expire_grace_period")]
	public Optional<Int32> ExpirationGracePeriod { get; init; }

	/// <summary>
	/// This integration user.
	/// </summary>
	[JsonPropertyName("user")]
	public Optional<DiscordUser> User { get; init; }

	/// <summary>
	/// Timestamp at which this integration was last synced.
	/// </summary>
	[JsonPropertyName("synced_at")]
	public Optional<DateTimeOffset> LastSynced { get; init; }

	/// <summary>
	/// The subscriber count of this integration.
	/// </summary>
	[JsonPropertyName("subscriber_count")]
	public Optional<Int32> SubscriberCount { get; init; }

	/// <summary>
	/// Gets whether this application has been revoked.
	/// </summary>
	[JsonPropertyName("revoked")]
	public Optional<Boolean> Revoked { get; init; }

	/// <summary>
	/// The ID of the guild this integration belongs to. Only available in gateway events.
	/// </summary>
	[JsonPropertyName("guild_id")]
	public Optional<Int64> GuildId { get; init; }
}
