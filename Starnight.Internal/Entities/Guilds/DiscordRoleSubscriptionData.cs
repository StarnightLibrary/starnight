namespace Starnight.Internal.Entities.Guilds;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents metadata for a role subscription message.
/// </summary>
public sealed record DiscordRoleSubscriptionData
{
	/// <summary>
	/// The ID of the SKU and listing the user is subscribed to.
	/// </summary>
	[JsonPropertyName("role_subscription_listing_id")]
	public required Int64 RoleSubscriptionListingId { get; init; }

	/// <summary>
	/// The name of the tier the user is subscribed to.
	/// </summary>
	[JsonPropertyName("tier_name")]
	public required String Tier { get; init; }

	/// <summary>
	/// The cumulative number of months the user was subscribed for.
	/// </summary>
	[JsonPropertyName("total_months_subscribed")]
	public required Int32 TotalMonthsSubscribed { get; init; }

	/// <summary>
	/// Whether this notification is for a renewal rather than a new purchase.
	/// </summary>
	[JsonPropertyName("is_renewal")]
	public required Boolean IsRenewal { get; init; }
}
