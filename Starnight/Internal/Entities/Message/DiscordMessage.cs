namespace Starnight.Internal.Entities.Message;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Channels;
using Starnight.Internal.Entities.Guild;
using Starnight.Internal.Entities.Interaction;
using Starnight.Internal.Entities.Message.Embed;
using Starnight.Internal.Entities.Sticker;
using Starnight.Internal.Entities.Team;
using Starnight.Internal.Entities.User;

/// <summary>
/// Represents a discord channel message.
/// </summary>
public class DiscordMessage : DiscordSnowflakeObject
{
	/// <summary>
	/// Snowflake identifier of the channel this message was sent in.
	/// </summary>
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("channel_id")]
	public Int64 ChannelId { get; init; }

	/// <summary>
	/// Snowflake identifier of the guild this message was sent in.
	/// </summary>
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("guild_id")]
	public Int64? GuildId { get; init; }

	/// <summary>
	/// Author of this message. If this is sent by a webhook, this author will be invalid.
	/// </summary>
	[JsonPropertyName("author")]
	public DiscordUser Author { get; init; } = default!;

	/// <summary>
	/// Guild member this message is associated with.
	/// </summary>
	[JsonPropertyName("member")]
	public DiscordGuildMember? Member { get; init; }

	/// <summary>
	/// Text contents of this message.
	/// </summary>
	[JsonPropertyName("content")]
	public String Content { get; init; } = default!;

	/// <summary>
	/// Denotes when this message was sent.
	/// </summary>
	[JsonPropertyName("timestamp")]
	public DateTime Timestamp { get; init; }

	/// <summary>
	/// Denotes when this message was last edited.
	/// </summary>
	[JsonPropertyName("edited_timestamp")]
	public DateTime? EditTimestamp { get; init; }

	/// <summary>
	/// Whether this message was sent as TTS message.
	/// </summary>
	[JsonPropertyName("tts")]
	public Boolean TTS { get; init; }

	/// <summary>
	/// Whether this message mentions @everyone.
	/// </summary>
	[JsonPropertyName("mention_everyone")]
	public Boolean MentionsEveryone { get; init; }

	/// <summary>
	/// Array of the users specifically mentioned in this message.
	/// </summary>
	[JsonPropertyName("mentions")]
	public DiscordGuildMember[] MentionedUsers { get; init; } = default!;

	/// <summary>
	/// Array of role IDs specifically mentioned in this message.
	/// </summary>
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("mention_roles")]
	public Int64[] MentionedRoles { get; init; } = default!;

	/// <summary>
	/// Array of channel mentions from this message.
	/// </summary>
	[JsonPropertyName("mention_channel")]
	public DiscordChannelMention[]? MentionedChannels { get; init; }

	/// <summary>
	/// Array of attachments to this message.
	/// </summary>
	[JsonPropertyName("attachments")]
	public DiscordMessageAttachment[] Attachments { get; init; } = default!;

	/// <summary>
	/// Array of embeds attached to this message.
	/// </summary>
	[JsonPropertyName("embeds")]
	public DiscordEmbed[] Embeds { get; init; } = default!;

	/// <summary>
	/// Array of reactions for this message.
	/// </summary>
	[JsonPropertyName("reactions")]
	public DiscordReaction[] Reactions { get; init; } = default!;

	[JsonPropertyName("nonce")]
	public Object? Nonce { get; init; }

	/// <summary>
	/// Whether this message is pinned.
	/// </summary>
	[JsonPropertyName("pinned")]
	public Boolean Pinned { get; init; }

	/// <summary>
	/// Snowflake identifier of the webhook that created this message.
	/// </summary>
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("webhook_id")]
	public Int64 WebhookId { get; init; }

	/// <summary>
	/// The message type.
	/// </summary>
	[JsonPropertyName("type")]
	public DiscordMessageType MessageType { get; init; }

	/// <summary>
	/// Message-associated activity. Only sent with Rich Presence-related events.
	/// </summary>
	[JsonPropertyName("activity")]
	public DiscordMessageActivity? Activity { get; init; }

	/// <summary>
	/// Message-associated application. Only sent with Rich Presence-related events.
	/// </summary>
	[JsonPropertyName("application")]
	public DiscordApplication? Application { get; init; }

	/// <summary>
	/// Snowflake identifier of the associated application if this message was a response to an interaction.
	/// </summary>
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	[JsonPropertyName("application_id")]
	public Int64? ApplicationId { get; init; }

	/// <summary>
	/// Any possible reference data for this message.
	/// </summary>
	[JsonPropertyName("message_reference")]
	public DiscordMessageReference? Reference { get; init; }

	/// <summary>
	/// Discord message flags, combined as a bitfield.
	/// </summary>
	[JsonPropertyName("flags")]
	public DiscordMessageFlags? Flags { get; init; }

	/// <summary>
	/// The message associated with <see cref="Reference"/>
	/// </summary>
	[JsonPropertyName("referenced_message")]
	public DiscordMessage? ReferencedMessage { get; init; }

	/// <summary>
	/// Only sent if this message is a response to an interaction.
	/// </summary>
	[JsonPropertyName("interaction")]
	public DiscordMessageInteraction? Interaction { get; init; }

	/// <summary>
	/// The thread that was started from this message, if any.
	/// </summary>
	[JsonPropertyName("thread")]
	public DiscordChannel? Threads { get; init; }

	/// <summary>
	/// Message components associated with this message.
	/// </summary>
	[JsonPropertyName("components")]
	public DiscordMessageComponent[]? Components { get; init; }

	/// <summary>
	/// The stickers sent with this message.
	/// </summary>
	[JsonPropertyName("sticker_items")]
	public DiscordMessageSticker[]? Stickers { get; init; }

	/// <summary>
	/// Deprecated.
	/// </summary>
	[JsonPropertyName("stickers")]
	[Obsolete("Formerly the stickers sent with this message. Use DiscordMessage.Stickers instead.", DiagnosticId = "SE0004")]
	public DiscordSticker[]? DeprecatedStickers { get; init; }
}
