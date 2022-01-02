namespace Starnight.Internal.Entities.Guild.Invite;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Channels;
using Starnight.Internal.Entities.Team;
using Starnight.Internal.Entities.User;

/// <summary>
/// Represents an invite to a guild, application or stage channel.
/// </summary>
public class DiscordInvite
{
	/// <summary>
	/// The invite code, as in <c>discord.gg/invcode</c>
	/// </summary>
	[JsonPropertyName("code")]
	public String Code { get; init; } = default!;

	/// <summary>
	/// The guild targeted by this invite.
	/// </summary>
	[JsonPropertyName("guild")]
	public DiscordGuild? Guild { get; init; }

	/// <summary>
	/// The channel targeted by this invite.
	/// </summary>
	[JsonPropertyName("channel")]
	public DiscordChannel? Channel { get; init; }

	/// <summary>
	/// The user who created this invite.
	/// </summary>
	[JsonPropertyName("inviter")]
	public DiscordUser? Creator { get; init; }

	/// <summary>
	/// The target type of this voice channel invite.
	/// </summary>
	[JsonPropertyName("target_type")]
	public DiscordVoiceInviteTargetType? TargetType { get; init; }

	/// <summary>
	/// The streaming user targeted by this invite.
	/// </summary>
	[JsonPropertyName("target_user")]
	public DiscordUser? TargetUser { get; init; }

	/// <summary>
	/// The embedded application targeted by this invite.
	/// </summary>
	[JsonPropertyName("target_application")]
	public DiscordApplication? TargetApplication { get; init; }

	/// <summary>
	/// Approximate count of guild members in this guild.
	/// </summary>
	[JsonPropertyName("approximate_member_count")]
	public Int32? ApproximateMemberCount { get; init; }

	/// <summary>
	/// Approximate count of online members in this guild.
	/// </summary>
	[JsonPropertyName("approximate_presence_count")]
	public Int32? ApproximatePresenceCount { get; init; }

	/// <summary>
	/// Describes when this invite expires.
	/// </summary>
	[JsonPropertyName("expires_at")]
	public DateTime? ExpiresAt { get; init; }

	/// <summary>
	/// The stage instance targeted by this stage invite.
	/// </summary>
	[JsonPropertyName("stage_instance")]
	public DiscordInviteStageInstance? StageInstance { get; init; }

	/// <summary>
	/// The scheduled event targeted by this scheduled event invite.
	/// </summary>
	[JsonPropertyName("scheduled_event")]
	public DiscordScheduledEvent? ScheduledEvent { get; init; }

	// METADATA - not always sent, but always part of this object if sent. //

	/// <summary>
	/// Stores how many times this invite has been used.
	/// </summary>
	[JsonPropertyName("uses")]
	public Int32? Uses { get; init; }

	/// <summary>
	/// Stores how many times this invite can be used.
	/// </summary>
	[JsonPropertyName("max_uses")]
	public Int32? MaxUses { get; init; }

	/// <summary>
	/// Stores after how many seconds this invite expires.
	/// </summary>
	[JsonPropertyName("max_age")]
	public Int32? MaxAge { get; init; }

	/// <summary>
	/// Stores whether this invite grants temporary membership.
	/// </summary>
	[JsonPropertyName("temporary")]
	public Boolean? Temporary { get; init; }

	/// <summary>
	/// Stores when this invite was created.
	/// </summary>
	[JsonPropertyName("created_at")]
	public DateTime? CreatedAt { get; init; }
}
