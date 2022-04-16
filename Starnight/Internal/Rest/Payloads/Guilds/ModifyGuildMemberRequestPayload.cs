namespace Starnight.Internal.Rest.Payloads.Guilds;

using System.Text.Json.Serialization;
using System;

using Starnight.Converters;
using System.Collections.Generic;

/// <summary>
/// Represents a payload to PATCH /guilds/:guild_id/members/:user_id
/// </summary>
public record ModifyGuildMemberRequestPayload
{
	/// <summary>
	/// The nickname to force the user to assume.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("nick")]
	[JsonConverter(typeof(OptionalParameterJsonConverterFactory))]
	public OptionalParameter<String>? Nickname { get; init; }

	/// <summary>
	/// An array of role IDs to assign.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("roles")]
	public IEnumerable<Int64>? Roles { get; init; }

	/// <summary>
	/// Whether to mute the user.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("mute")]
	public Boolean? Mute { get; init; }

	/// <summary>
	/// Whether to deafen the user.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("deaf")]
	public Boolean? Deafen { get; init; }

	/// <summary>
	/// The voice channel ID to move the user into.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("channel_id")]
	public Int64? ChannelId { get; init; }

	/// <summary>
	/// The timestamp at which the user's timeout is supposed to expire. Set to null to remove the timeout.
	/// Must be no more than 28 days in the future.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("communication_disabled_until")]
	[JsonConverter(typeof(OptionalParameterJsonConverterFactory))]
	public OptionalParameter<DateTimeOffset>? CommunicationDisabledUntil { get; init; }
}
