namespace Starnight.Internal.Rest.Payloads.Channel;

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
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("max_age")]
	public Int32? MaxAge { get; init; }

	/// <summary>
	/// Specifies the maximum amount of uses for this invite. Setting it to 0 means the invite can be used infinitely.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("max_uses")]
	public Int32? MaxUses { get; init; }

	/// <summary>
	/// Specifies whether this invite only grants temporary membership.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("temporary")]
	public Boolean? IsTemporary { get; init; }

	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("unique")]
	public Boolean? IsUnique { get; init; }

	/// <summary>
	/// Specifies the target type of this voice channel invite.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("target_type")]
	public DiscordVoiceInviteTargetType? VoiceTargetType { get; init; }

	/// <summary>
	/// Snowflake identifier of the invite's target user if <see cref="VoiceTargetType"/> is
	/// <see cref="DiscordVoiceInviteTargetType.Stream"/>.
	/// </summary>
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("target_user_id")]
	public Int64? TargetUserId { get; init; }

	/// <summary>
	/// Snowflake identifier of the invite's target embedded application if <see cref="VoiceTargetType"/> is
	/// <see cref="DiscordVoiceInviteTargetType.EmbeddedApplication"/>.
	/// </summary>
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("target_application_id")]
	public Int64? TargetApplicationId { get; init; }
}
