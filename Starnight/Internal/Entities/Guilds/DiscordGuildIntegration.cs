namespace Starnight.Internal.Entities.Guilds;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Users;

/// <summary>
/// Reprents a Discord Guild Integration, for instance the Nitro Booster role
/// </summary>
public record DiscordGuildIntegration : DiscordSnowflakeObject
{
	#region Always present
	/// <summary>
	/// Integration name - i.e. for bots the Application name
	/// </summary>
	[JsonPropertyName("name")]
	public String Name { get; init; } = default!;

	/// <summary>
	/// Integration type: twitch, youtube or discord
	/// </summary>
	[JsonPropertyName("type")]
	public String Type { get; init; } = default!;

	/// <summary>
	/// Gets whether this integration is enabled
	/// </summary>
	[JsonPropertyName("enabled")]
	public Boolean Enabled { get; init; } = true;

	/// <summary>
	/// The integration account
	/// </summary>
	[JsonPropertyName("account")]
	public DiscordGuildIntegrationAccount Account { get; init; } = default!;

	/// <summary>
	/// The bot or OAuth2 application for this integration.
	/// </summary>
	[JsonPropertyName("application")]
	public DiscordGuildIntegrationApplication? Application { get; init; }
	#endregion

	#region Present only for non-Bot integrations
	/// <summary>
	/// Gets whether this integration is syncing.
	/// </summary>
	[JsonPropertyName("syncing")]
	public Boolean? Synchronizing { get; init; }

	/// <summary>
	/// Snowflake identifier of the role this integration uses for "subscribers".
	/// </summary>
	[JsonPropertyName("role_id")]
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	public Int64? RoleId { get; init; }

	/// <summary>
	/// Gets whether emotes should be synced for this integration.
	/// This field is only present for twitch integrations.
	/// </summary>
	[JsonPropertyName("enable_emoticons")]
	public Boolean? EnableEmotes { get; init; }

	/// <summary>
	/// Gets the behaviour of expiring subscribers.
	/// </summary>
	[JsonPropertyName("expire_behaviour")]
	public DiscordGuildIntegrationExpireBehaviour? ExpirationBehaviour { get; init; }

	/// <summary>
	/// The grace period in days after the subscription has ended before the <see cref="ExpirationBehaviour"/> comes into effect.
	/// </summary>
	[JsonPropertyName("expire_grace_period")]
	public Int32? ExpirationGracePeriod { get; init; }

	/// <summary>
	/// This integration user.
	/// </summary>
	[JsonPropertyName("user")]
	public DiscordUser? User { get; init; }

	/// <summary>
	/// Timestamp at which this integration was last synced.
	/// </summary>
	[JsonPropertyName("synced_at")]
	public DateTimeOffset? LastSynced { get; init; }

	/// <summary>
	/// The subscriber count of this integration.
	/// </summary>
	[JsonPropertyName("subscriber_count")]
	public Int32? SubscriberCount { get; init; }

	/// <summary>
	/// Gets whether this application has been revoked.
	/// </summary>
	[JsonPropertyName("revoked")]
	public Boolean? Revoked { get; init; }
	#endregion
}
