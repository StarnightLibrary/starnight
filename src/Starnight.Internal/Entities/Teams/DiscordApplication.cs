namespace Starnight.Internal.Entities.Teams;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using Starnight.Entities;
using Starnight.Internal.Entities.Users;

/// <summary>
/// Represents a discord application.
/// </summary>
public sealed record DiscordApplication : DiscordSnowflakeObject
{
	/// <summary>
	/// The name of this application.
	/// </summary>
	[JsonPropertyName("name")]
	public Optional<String> Name { get; init; }

	/// <summary>
	/// The icon hash for this application.
	/// </summary>
	[JsonPropertyName("icon")]
	public String? Icon { get; init; }

	/// <summary>
	/// The description for this application.
	/// </summary>
	[JsonPropertyName("description")]
	public Optional<String> Description { get; init; }

	/// <summary>
	/// An array of RPC origin URLs, if RPC is enabled.
	/// </summary>
	[JsonPropertyName("rpc_origins")]
	public Optional<IEnumerable<String>> RPCOrigins { get; init; }

	/// <summary>
	/// Whether the application's bot is public.
	/// </summary>
	[JsonPropertyName("bot_public")]
	public Optional<Boolean> HasPublicBot { get; init; }

	/// <summary>
	/// Whether the application's bot requires the full oauth2 code grant.
	/// </summary>
	[JsonPropertyName("bot_require_code_grant")]
	public Optional<Boolean> BotRequiresCodeGrant { get; init; }

	/// <summary>
	/// The URL pointing to this application's Terms of Service.
	/// </summary>
	[JsonPropertyName("terms_of_service_url")]
	public Optional<String> TosUrl { get; init; }

	/// <summary>
	/// The URL pointing to this application's Privacy Policy.
	/// </summary>
	[JsonPropertyName("privacy_policy_url")]
	public Optional<String> PrivacyPolicyUrl { get; init; }

	/// <summary>
	/// The owner of this application.
	/// </summary>
	[JsonPropertyName("owner")]
	public Optional<DiscordUser> Owner { get; init; }

	/// <summary>
	/// The hex-encoded verification key for interactions and the GameSDK.
	/// </summary>
	[JsonPropertyName("verify_key")]
	public Optional<String> VerificationKey { get; init; }

	/// <summary>
	/// Team object associated with this application.
	/// </summary>
	[JsonPropertyName("team")]
	public DiscordTeam? Team { get; init; }

	/// <summary>
	/// Home guild of this application.
	/// </summary>
	[JsonPropertyName("guild_id")]
	public Optional<Int64> GuildId { get; init; }

	/// <summary>
	/// Primary SKU identifier of this application.
	/// </summary>
	[JsonPropertyName("primary_sku_id")]
	public Optional<Int64> PrimarySkuId { get; init; }

	/// <summary>
	/// URL slug for this application.
	/// </summary>
	[JsonPropertyName("slug")]
	public Optional<String> Slug { get; init; }

	/// <summary>
	/// Default RPC invite cover image hash for this application.
	/// </summary>
	[JsonPropertyName("cover_image")]
	public Optional<String> CoverImage { get; init; }

	/// <summary>
	/// The public flags for this application.
	/// </summary>
	[JsonPropertyName("flags")]
	public Optional<DiscordApplicationFlags> Flags { get; init; }

	/// <summary>
	/// Up to five tags describing the content and functionality of this application.
	/// </summary>
	[JsonPropertyName("tags")]
	public Optional<IEnumerable<String>> Tags { get; init; }

	/// <summary>
	/// Installation parameters for this application.
	/// </summary>
	[JsonPropertyName("install_params")]
	public Optional<DiscordApplicationInstallParameters> InstallParameters { get; init; }

	/// <summary>
	/// The application's default custom authorization link, if enabled.
	/// </summary>
	[JsonPropertyName("custom_install_url")]
	public Optional<String> CustomInstallationUrl { get; init; }
}
