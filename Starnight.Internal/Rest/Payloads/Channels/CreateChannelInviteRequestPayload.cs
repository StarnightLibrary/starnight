namespace Starnight.Internal.Rest.Payloads.Channels;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Guilds.Invites;

/// <summary>
/// Represents a payload to POST /channels/:channel_id/invites
/// </summary>
public record CreateChannelInviteRequestPayload
{
	/// <summary>
	/// Specifies the expiry time for this invite. Setting it to 0 means the invite never expires.
	/// </summary>
	[JsonPropertyName("max_age")]
	public Optional<Int32> MaxAge { get; init; }

	/// <summary>
	/// Specifies the maximum amount of uses for this invite. Setting it to 0 means the invite can be used infinitely.
	/// </summary>
	[JsonPropertyName("max_uses")]
	public Optional<Int32> MaxUses { get; init; }

	/// <summary>
	/// Specifies whether this invite only grants temporary membership.
	/// </summary>
	[JsonPropertyName("temporary")]
	public Optional<Boolean> IsTemporary { get; init; }

	/// <summary>
	/// Specifies whether this invite is unique.
	/// </summary>
	[JsonPropertyName("unique")]
	public Optional<Boolean> IsUnique { get; init; }

	/// <summary>
	/// Specifies the target type of this voice channel invite.
	/// </summary>
	[JsonPropertyName("target_type")]
	public Optional<DiscordVoiceInviteTargetType> VoiceTargetType { get; init; }

	/// <summary>
	/// Snowflake identifier of the invite's target user if <see cref="VoiceTargetType"/> is
	/// <see cref="DiscordVoiceInviteTargetType.Stream"/>.
	/// </summary>
	[JsonPropertyName("target_user_id")]
	public Optional<Int64> TargetUserId { get; init; }

	/// <summary>
	/// Snowflake identifier of the invite's target embedded application if <see cref="VoiceTargetType"/> is
	/// <see cref="DiscordVoiceInviteTargetType.EmbeddedApplication"/>.
	/// </summary>
	[JsonPropertyName("target_application_id")]
	public Optional<Int64> TargetApplicationId { get; init; }
}
