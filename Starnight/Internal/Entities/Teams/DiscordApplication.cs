namespace Starnight.Internal.Entities.Teams;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Users;

/// <summary>
/// Represents a discord application.
/// </summary>
public record DiscordApplication : DiscordSnowflakeObject
{
	/// <summary>
	/// The name of this application.
	/// </summary>
	[JsonPropertyName("name")]
	public String Name { get; init; } = default!;

	/// <summary>
	/// The icon hash for this application.
	/// </summary>
	[JsonPropertyName("icon")]
	public String? Icon { get; init; }

	/// <summary>
	/// The description for this application.
	/// </summary>
	[JsonPropertyName("description")]
	public String Description { get; init; } = default!;

	/// <summary>
	/// An array of RPC origin URLs, if RPC is enabled.
	/// </summary>
	[JsonPropertyName("rpc_origins")]
	public String[]? RCPOrigins { get; init; }

	/// <summary>
	/// Whether the application's bot is public.
	/// </summary>
	[JsonPropertyName("bot_public")]
	public Boolean PublicBot { get; init; }

	/// <summary>
	/// Whether the application's bot requires the full oauth2 code grant.
	/// </summary>
	[JsonPropertyName("bot_require_code_grant")]
	public Boolean BotRequiresCodeGrant { get; init; }

	/// <summary>
	/// The URL pointing to this application's Terms of Service.
	/// </summary>
	[JsonPropertyName("terms_of_service_url")]
	public String? TosUrl { get; init; }

	/// <summary>
	/// The URL pointing to this application's Privacy Policy.
	/// </summary>
	[JsonPropertyName("privacy_policy_url")]
	public String? PrivacyPolicyUrl { get; init; }

	/// <summary>
	/// The owner of this application.
	/// </summary>
	[JsonPropertyName("owner")]
	public DiscordUser? Owner { get; init; }

	/// <summary>
	/// The summary field for this application.
	/// </summary>
	[JsonPropertyName("summary")]
	public String Summary { get; init; } = default!;

	/// <summary>
	/// The hex-encoded verification key for interactions and the GameSDK.
	/// </summary>
	[JsonPropertyName("verify_key")]
	public String VerificationKey { get; init; } = default!;

	/// <summary>
	/// Team object associated with this application.
	/// </summary>
	[JsonPropertyName("team")]
	public DiscordTeam? Team { get; init; } = default!;

	/// <summary>
	/// Home guild of this application.
	/// </summary>
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("guild_id")]
	public Int64? GuildId { get; init; }

	/// <summary>
	/// Primary SKU identifier of this application.
	/// </summary>
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("primary_sku_id")]
	public Int64? PrimarySkuId { get; init; }

	/// <summary>
	/// URL slug for this application.
	/// </summary>
	[JsonPropertyName("slug")]
	public String? Slug { get; init; }

	/// <summary>
	/// Default RPC invite cover image hash for this application.
	/// </summary>
	[JsonPropertyName("cover_image")]
	public String? CoverImage { get; init; }

	/// <summary>
	/// The public flags for this application.
	/// </summary>
	[JsonPropertyName("flags")]
	public DiscordApplicationFlags? Flags { get; init; }
}
