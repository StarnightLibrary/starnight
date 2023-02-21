namespace Starnight.Internal.Entities.Interactions;

using System;
using System.Text.Json.Serialization;

using Starnight.Entities;
using Starnight.Internal.Entities.Guilds;
using Starnight.Internal.Entities.Messages;
using Starnight.Internal.Entities.Users;

/// <summary>
/// Represents an interaction object.
/// </summary>
public sealed record DiscordInteraction : DiscordSnowflakeObject
{
	/// <summary>
	/// Snowflake identifier of the application 'owning' this interaction.
	/// </summary>
	[JsonPropertyName("application_id")]
	public required Int64 ApplicationId { get; init; }

	/// <summary>
	/// Type of this interaction.
	/// </summary>
	[JsonPropertyName("type")]
	public required DiscordInteractionType Type { get; init; }

	/// <summary>
	/// The entire command data payload.
	/// </summary>
	[JsonPropertyName("data")]
	public Optional<DiscordInteractionData> Data { get; init; }

	/// <summary>
	/// Snowflake identifier of the guild this interaction was executed from.
	/// </summary>
	[JsonPropertyName("guild_id")]
	public Optional<Int64> GuildId { get; init; }

	/// <summary>
	/// Snowflake identifier of the channel this interaction was executed from.
	/// </summary>
	[JsonPropertyName("channel_id")]
	public Optional<Int64> ChannelId { get; init; }

	/// <summary>
	/// The member which executed this interaction if it took place in a guild.
	/// </summary>
	[JsonPropertyName("member")]
	public Optional<DiscordGuildMember> Member { get; init; }

	/// <summary>
	/// The user which executed this interaction if it took place in a DM.
	/// </summary>
	[JsonPropertyName("user")]
	public Optional<DiscordUser> User { get; init; }

	/// <summary>
	/// A unique token to continue this interaction flow.
	/// </summary>
	[JsonPropertyName("token")]
	public required String ContinuationToken { get; init; }

	/// <summary>
	/// Read-only, always 1.
	/// </summary>
	[JsonPropertyName("version")]
	public required Int32 Version { get; init; } = 1;

	/// <summary>
	/// The message this component interaction is attached to.
	/// </summary>
	[JsonPropertyName("message")]
	public Optional<DiscordMessage> Message { get; init; }

	/// <summary>
	/// Permissions this application has within the channel the interaction was sent from.
	/// </summary>
	[JsonPropertyName("app_permissions")]
	public Optional<DiscordPermissions> ApplicationPermissions { get; init; }

	/// <summary>
	/// The preferred locale of the executing user.
	/// </summary>
	[JsonPropertyName("locale")]
	public Optional<String> Locale { get; init; }

	/// <summary>
	/// The set locale of the guild this interaction was executed in.
	/// </summary>
	[JsonPropertyName("guild_locale")]
	public Optional<String> GuildLocale { get; init; }
}
