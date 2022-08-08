namespace Starnight.Internal.Entities.Users;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a Discord user account, without additional information such as guilds.
/// </summary>
public sealed record DiscordUser : DiscordSnowflakeObject
{
	/// <summary>
	/// Discord username of this account. Usernames cannot contain any Discord control characters or be "everyone", "here" or "discordtag".
	/// </summary>
	[JsonPropertyName("username")]
	public required String Username { get; init; }

	/// <summary>
	/// 4-digit int discriminator of this account, from 0001 to 9999. For some reason this is sent as string.
	/// </summary>
	[JsonPropertyName("discriminator")]
	public required String Discriminator { get; init; }

	/// <summary>
	/// The avatar hash code of this account.
	/// </summary>
	[JsonPropertyName("avatar")]
	public String? AvatarHash { get; init; }

	/// <summary>
	/// Bot status of this account - true if this user belongs to an OAuth2 application.
	/// </summary>
	[JsonPropertyName("bot")]
	public Optional<Boolean> Bot { get; init; }

	/// <summary>
	/// Gets whether this account is part of the official Discord Message System.
	/// </summary>
	[JsonPropertyName("system")]
	public Optional<Boolean> System { get; init; } 

	/// <summary>
	/// Gets whether this account has Multi-Factor Authentication enabled.
	/// </summary>
	[JsonPropertyName("mfa_enabled")]
	public Optional<Boolean> MfaEnabled { get; init; } 

	/// <summary>
	/// Gets the reference to this accounts' banner, null if the user has no banner.
	/// </summary>
	[JsonPropertyName("banner")]
	public Optional<String?> Banner { get; init; }

	/// <summary>
	/// Gets the integer representation of this users baner colour, null if the user has a custom banner.
	/// </summary>
	[JsonPropertyName("accent_color")]
	public Optional<Int32?> AccentColor { get; init; } 

	/// <summary>
	/// Gets the user's chosen locale option.
	/// </summary>
	[JsonPropertyName("locale")]
	public Optional<String> Locale { get; init; }

	/// <summary>
	/// Gets the flags on this user account.
	/// </summary>
	[JsonPropertyName("flags")]
	public Optional<DiscordUserFlags> Flags { get; init; }

	/// <summary>
	/// Gets the publicly visible flags on this user account. <see langword="null"/> if there are no flags.
	/// </summary>
	[JsonPropertyName("public_flags")]
	public Optional<DiscordUserFlags> PublicFlags { get; init; }

	/// <summary>
	/// Gets the Nitro subscription type on this user account.
	/// </summary>
	[JsonPropertyName("premium_type")]
	public DiscordPremiumType? PremiumType { get; init; }

	/// <summary>
	/// The email address tied to this user account. Requires the <c>email</c> OAuth2 scope.
	/// </summary>
	[JsonPropertyName("email")]
	public Optional<String?> Email { get; init; }

	/// <summary>
	/// Gets whether the <see cref="Email"/> on this account has been verified. Requires the <c>email</c> OAuth2 scope.
	/// </summary>
	[JsonPropertyName("verified")]
	public Optional<Boolean> VerifiedEmail { get; init; }
}
