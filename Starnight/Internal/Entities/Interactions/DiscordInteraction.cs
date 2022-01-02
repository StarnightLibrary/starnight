namespace Starnight.Internal.Entities.Interactions;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Guilds;
using Starnight.Internal.Entities.Messages;
using Starnight.Internal.Entities.Users;

/// <summary>
/// Represents an interaction object.
/// </summary>
public record DiscordInteraction : DiscordSnowflakeObject
{
	/// <summary>
	/// Snowflake identifier of the application 'owning' this interaction.
	/// </summary>
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("application_id")]
	public Int64 ApplicationId { get; init; }

	/// <summary>
	/// Type of this interaction.
	/// </summary>
	[JsonPropertyName("type")]
	public DiscordInteractionType Type { get; init; }

	/// <summary>
	/// The entire command data payload.
	/// </summary>
	[JsonPropertyName("data")]
	public DiscordInteractionData? Data { get; init; }

	/// <summary>
	/// Snowflake identifier of the guild this interaction was executed from.
	/// </summary>
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("guild_id")]
	public Int64? GuildId { get; init; }

	/// <summary>
	/// Snowflake identifier of the channel this interaction was executed from.
	/// </summary>
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("channel_id")]
	public Int64? ChannelId { get; init; }

	/// <summary>
	/// The member which executed this interaction if it took place in a guild.
	/// </summary>
	[JsonPropertyName("member")]
	public DiscordGuildMember? Member { get; init; }

	/// <summary>
	/// The user which executed this interaction if it took place in a DM.
	/// </summary>
	[JsonPropertyName("user")]
	public DiscordUser? User { get; init; }

	/// <summary>
	/// A unique token to continue this interaction flow.
	/// </summary>
	[JsonPropertyName("token")]
	public String ContinuationToken { get; init; } = default!;

	/// <summary>
	/// Read-only, always 1.
	/// </summary>
	[JsonPropertyName("version")]
	public Int32 Version { get; init; } = 1;

	/// <summary>
	/// The message this component interaction is attached to.
	/// </summary>
	[JsonPropertyName("message")]
	public DiscordMessage? Message { get; init; }
}
