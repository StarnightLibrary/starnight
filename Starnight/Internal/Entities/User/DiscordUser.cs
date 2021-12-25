namespace Starnight.Internal.Entities.User;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a Discord user account, without additional information such as guilds.
/// </summary>
public class DiscordUser : DiscordSnowflakeObject
{
	#region Fields
	#region OAuth Scope: identify

	/// <summary>
	/// Discord username of this account. Usernames cannot contain any Discord control characters or be "everyone", "here" or "discordtag".
	/// </summary>
	[JsonPropertyName("username")]
	public String Username { get; init; } = default!;

	/// <summary>
	/// 4-digit int discriminator of this account, from 0001 to 9999. For some reason this is sent as string.
	/// </summary>
	[JsonPropertyName("discriminator")]
	public String Discriminator { get; init; } = default!;

	/// <summary>
	/// The avatar hash code of this account.
	/// </summary>
	[JsonPropertyName("avatar")]
	public String? AvatarHash { get; init; }

	/// <summary>
	/// Bot status of this account - true if this user belongs to an OAuth2 application.
	/// </summary>
	[JsonPropertyName("bot")]
	public Boolean? Bot { get; init; } = false;

	/// <summary>
	/// Gets whether this account is part of the official Discord Message System.
	/// </summary>
	[JsonPropertyName("system")]
	public Boolean? System { get; init; } = false;

	/// <summary>
	/// Gets whether this account has Multi-Factor Authentication enabled.
	/// </summary>
	[JsonPropertyName("mfa_enabled")]
	public Boolean? MfaEnabled { get; init; } = false;

	/// <summary>
	/// Gets the reference to this accounts' banner, null if the user has no banner.
	/// </summary>
	[JsonPropertyName("banner")]
	public String? Banner { get; init; }

	/// <summary>
	/// Gets the integer representation of this users baner colour, null if the user has a custom banner.
	/// </summary>
	[JsonPropertyName("accent_color")]
	public Int32? AccentColor { get; init; } = null;

	/// <summary>
	/// Gets the user's chosen locale option.
	/// </summary>
	[JsonPropertyName("locale")]
	public String? Locale { get; init; }

	/// <summary>
	/// Gets the flags on this user account, see <see cref="DiscordUserFlags"/>. <see langword="null"/> if there are no flags.
	/// (instead of just sending zero. Discord, please...)
	/// </summary>
	[JsonPropertyName("flags")]
	public Int32? Flags { get; init; }

	/// <summary>
	/// Gets the publicly visible flags on this user account, see <see cref="DiscordUserFlags"/>. <see langword="null"/> if there are no flags.
	/// </summary>
	[JsonPropertyName("public_flags")]
	public Int32? PublicFlags { get; init; }

	/// <summary>
	/// Gets the Nitro subscription type on this user account, see <see cref="DiscordPremiumType"/>.
	/// </summary>
	[JsonPropertyName("premium_type")]
	public Byte? PremiumType { get; init; }

	#endregion

	#region OAuth scope: email

	/// <summary>
	/// The email address tied to this user account. Requires the <c>email</c> OAuth2 scope.
	/// </summary>
	[JsonPropertyName("email")]
	public String? Email { get; init; }

	/// <summary>
	/// Gets whether the <see cref="Email"/> on this account has been verified. Requires the <c>email</c> OAuth2 scope.
	/// </summary>
	[JsonPropertyName("verified")]
	public Boolean? VerifiedEmail { get; init; }

	#endregion
	#endregion
}
