namespace Starnight.Internal.Gateway.Payloads.Inbound.Dispatch;

using System;
using System.Text.Json.Serialization;

using Starnight.Entities;
using Starnight.Internal.Entities.Teams;
using Starnight.Internal.Entities.Users;

/// <summary>
/// Represents a payload for an InviteCreated object.
/// </summary>
public sealed record InviteCreatedPayload
{
	/// <summary>
	/// The channel this invite points to.
	/// </summary>
	[JsonPropertyName("channel_id")]
	public required Int64 ChannelId { get; init; }

	/// <summary>
	/// This invites invite code.
	/// </summary>
	[JsonPropertyName("code")]
	public required String Code { get; init; }

	/// <summary>
	/// The time at which this invite was created.
	/// </summary>
	[JsonPropertyName("created_at")]
	public required DateTimeOffset CreatedAt { get; init; }

	/// <summary>
	/// The guild this invite points to.
	/// </summary>
	[JsonPropertyName("guild_id")]
	public Optional<Int64> GuildId { get; init; }

	/// <summary>
	/// The user who created this invite.
	/// </summary>
	[JsonPropertyName("inviter")]
	public Optional<DiscordUser> Inviter { get; init; }

	/// <summary>
	/// The time this invite is valid for, in seconds.
	/// </summary>
	[JsonPropertyName("max_age")]
	public required Int32 MaxAge { get; init; }

	/// <summary>
	/// The maximum number of times this invite can be used.
	/// </summary>
	[JsonPropertyName("max_uses")]
	public required Int32 MaxUses { get; init; }

	/// <summary>
	/// The type of this voice channel, if this invite points to a voice channel.
	/// </summary>
	[JsonPropertyName("target_type")]
	public Optional<DiscordVoiceInviteTargetType> TargetType { get; init; }

	/// <summary>
	/// If this is a voice channel stream invite, the user whose stream this invite points to.
	/// </summary>
	[JsonPropertyName("target_user")]
	public Optional<DiscordUser> TargetUser { get; init; }

	/// <summary>
	/// If this is an embedded application invite, the embedded application to open.
	/// </summary>
	[JsonPropertyName("target_application")]
	public Optional<DiscordApplication> TargetApplication { get; init; }

	/// <summary>
	/// Whether the invite grants temporary membership.
	/// </summary>
	[JsonPropertyName("temporary")]
	public required Boolean Temporary { get; init; }
}
