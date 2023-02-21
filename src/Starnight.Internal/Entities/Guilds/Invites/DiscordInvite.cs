namespace Starnight.Internal.Entities.Guilds.Invites;

using System;
using System.Text.Json.Serialization;

using Starnight.Entities;
using Starnight.Internal.Entities.Channels;
using Starnight.Internal.Entities.Guilds;
using Starnight.Internal.Entities.Teams;
using Starnight.Internal.Entities.Users;

/// <summary>
/// Represents an invite to a guild, application or stage channel.
/// </summary>
public sealed record DiscordInvite
{
	/// <summary>
	/// The invite code, as in <c>discord.gg/invcode</c>
	/// </summary>
	[JsonPropertyName("code")]
	public required String Code { get; init; }

	/// <summary>
	/// The guild targeted by this invite.
	/// </summary>
	[JsonPropertyName("guild")]
	public Optional<DiscordGuild> Guild { get; init; }

	/// <summary>
	/// The channel targeted by this invite.
	/// </summary>
	[JsonPropertyName("channel")]
	public DiscordChannel? Channel { get; init; }

	/// <summary>
	/// The user who created this invite.
	/// </summary>
	[JsonPropertyName("inviter")]
	public Optional<DiscordUser> Creator { get; init; }

	/// <summary>
	/// The target type of this voice channel invite.
	/// </summary>
	[JsonPropertyName("target_type")]
	public Optional<DiscordVoiceInviteTargetType> TargetType { get; init; }

	/// <summary>
	/// The streaming user targeted by this invite.
	/// </summary>
	[JsonPropertyName("target_user")]
	public Optional<DiscordUser> TargetUser { get; init; }

	/// <summary>
	/// The embedded application targeted by this invite.
	/// </summary>
	[JsonPropertyName("target_application")]
	public Optional<DiscordApplication> TargetApplication { get; init; }

	/// <summary>
	/// Approximate count of guild members in this guild.
	/// </summary>
	[JsonPropertyName("approximate_member_count")]
	public Optional<Int32> ApproximateMemberCount { get; init; }

	/// <summary>
	/// Approximate count of online members in this guild.
	/// </summary>
	[JsonPropertyName("approximate_presence_count")]
	public Optional<Int32> ApproximatePresenceCount { get; init; }

	/// <summary>
	/// Describes when this invite expires.
	/// </summary>
	[JsonPropertyName("expires_at")]
	public Optional<DateTimeOffset> ExpiresAt { get; init; }

	/// <summary>
	/// The scheduled event targeted by this scheduled event invite.
	/// </summary>
	[JsonPropertyName("scheduled_event")]
	public Optional<DiscordScheduledEvent> ScheduledEvent { get; init; }

	// METADATA - not always sent, but always part of this object if sent. //

	/// <summary>
	/// Stores how many times this invite has been used.
	/// </summary>
	[JsonPropertyName("uses")]
	public Optional<Int32> Uses { get; init; }

	/// <summary>
	/// Stores how many times this invite can be used.
	/// </summary>
	[JsonPropertyName("max_uses")]
	public Optional<Int32> MaxUses { get; init; }

	/// <summary>
	/// Stores after how many seconds this invite expires.
	/// </summary>
	[JsonPropertyName("max_age")]
	public Optional<Int32> MaxAge { get; init; }

	/// <summary>
	/// Stores whether this invite grants temporary membership.
	/// </summary>
	[JsonPropertyName("temporary")]
	public Optional<Boolean> Temporary { get; init; }

	/// <summary>
	/// Stores when this invite was created.
	/// </summary>
	[JsonPropertyName("created_at")]
	public Optional<DateTimeOffset> CreatedAt { get; init; }
}
