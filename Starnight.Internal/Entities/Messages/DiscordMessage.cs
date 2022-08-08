namespace Starnight.Internal.Entities.Messages;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities;
using Starnight.Internal.Entities.Channels;
using Starnight.Internal.Entities.Guilds;
using Starnight.Internal.Entities.Interactions;
using Starnight.Internal.Entities.Interactions.Components;
using Starnight.Internal.Entities.Messages.Embeds;
using Starnight.Internal.Entities.Stickers;
using Starnight.Internal.Entities.Teams;
using Starnight.Internal.Entities.Users;

/// <summary>
/// Represents a discord channel message.
/// </summary>
public sealed record DiscordMessage : DiscordSnowflakeObject
{
	/// <summary>
	/// Snowflake identifier of the channel this message was sent in.
	/// </summary>
	[JsonPropertyName("channel_id")]
	public required Int64 ChannelId { get; init; }

	/// <summary>
	/// Author of this message. If this is sent by a webhook, this author will be invalid.
	/// </summary>
	[JsonPropertyName("author")]
	public required DiscordUser Author { get; init; }

	/// <summary>
	/// Text contents of this message.
	/// </summary>
	[JsonPropertyName("content")]
	public Optional<String> Content { get; init; }

	/// <summary>
	/// Denotes when this message was sent.
	/// </summary>
	[JsonPropertyName("timestamp")]
	public required DateTimeOffset Timestamp { get; init; }

	/// <summary>
	/// Denotes when this message was last edited.
	/// </summary>
	[JsonPropertyName("edited_timestamp")]
	public DateTimeOffset? EditTimestamp { get; init; }

	/// <summary>
	/// Whether this message was sent as TTS message.
	/// </summary>
	[JsonPropertyName("tts")]
	public required Boolean TTS { get; init; }

	/// <summary>
	/// Whether this message mentions @everyone.
	/// </summary>
	[JsonPropertyName("mention_everyone")]
	public required Boolean MentionsEveryone { get; init; }

	/// <summary>
	/// Array of the users specifically mentioned in this message.
	/// </summary>
	[JsonPropertyName("mentions")]
	public required IEnumerable<DiscordUser> MentionedUsers { get; init; } 

	/// <summary>
	/// Array of role IDs specifically mentioned in this message.
	/// </summary>
	[JsonPropertyName("mention_roles")]
	public required IEnumerable<Int64> MentionedRoles { get; init; }

	/// <summary>
	/// Array of channel mentions from this message.
	/// </summary>
	[JsonPropertyName("mention_channel")]
	public Optional<IEnumerable<DiscordChannelMention>> MentionedChannels { get; init; }

	/// <summary>
	/// Array of attachments to this message.
	/// </summary>
	[JsonPropertyName("attachments")]
	public required IEnumerable<DiscordMessageAttachment> Attachments { get; init; }

	/// <summary>
	/// Array of embeds attached to this message.
	/// </summary>
	[JsonPropertyName("embeds")]
	public required IEnumerable<DiscordEmbed> Embeds { get; init; }

	/// <summary>
	/// Array of reactions for this message.
	/// </summary>
	[JsonPropertyName("reactions")]
	public Optional<IEnumerable<DiscordMessageReaction>> Reactions { get; init; }

	[JsonPropertyName("nonce")]
	public Optional<Object> Nonce { get; init; }

	/// <summary>
	/// Whether this message is pinned.
	/// </summary>
	[JsonPropertyName("pinned")]
	public required Boolean Pinned { get; init; }

	/// <summary>
	/// Snowflake identifier of the webhook that created this message.
	/// </summary>
	[JsonPropertyName("webhook_id")]
	public Optional<Int64> WebhookId { get; init; }

	/// <summary>
	/// The message type.
	/// </summary>
	[JsonPropertyName("type")]
	public required DiscordMessageType MessageType { get; init; }

	/// <summary>
	/// Message-associated activity. Only sent with Rich Presence-related events.
	/// </summary>
	[JsonPropertyName("activity")]
	public Optional<DiscordMessageActivity> Activity { get; init; }

	/// <summary>
	/// Message-associated application. Only sent with Rich Presence-related events.
	/// </summary>
	[JsonPropertyName("application")]
	public Optional<DiscordApplication> Application { get; init; }

	/// <summary>
	/// Snowflake identifier of the associated application if this message was a response to an interaction.
	/// </summary>
	[JsonPropertyName("application_id")]
	public Optional<Int64> ApplicationId { get; init; }

	/// <summary>
	/// Any possible reference data for this message.
	/// </summary>
	[JsonPropertyName("message_reference")]
	public Optional<DiscordMessageReference> Reference { get; init; }

	/// <summary>
	/// Discord message flags, combined as a bitfield.
	/// </summary>
	[JsonPropertyName("flags")]
	public Optional<DiscordMessageFlags> Flags { get; init; }

	/// <summary>
	/// The message associated with <see cref="Reference"/>
	/// </summary>
	[JsonPropertyName("referenced_message")]
	public Optional<DiscordMessage?> ReferencedMessage { get; init; }

	/// <summary>
	/// Only sent if this message is a response to an interaction.
	/// </summary>
	[JsonPropertyName("interaction")]
	public Optional<DiscordMessageInteraction> Interaction { get; init; }

	/// <summary>
	/// The thread that was started from this message, if any.
	/// </summary>
	[JsonPropertyName("thread")]
	public Optional<DiscordChannel> Thread { get; init; }

	/// <summary>
	/// Message components associated with this message.
	/// </summary>
	[JsonPropertyName("components")]
	public Optional<IEnumerable<AbstractDiscordMessageComponent>> Components { get; init; }

	/// <summary>
	/// The stickers sent with this message.
	/// </summary>
	[JsonPropertyName("sticker_items")]
	public Optional<IEnumerable<DiscordMessageSticker>> Stickers { get; init; }

	/// <summary>
	/// A generally increasing, inconsistent integer (there may be gaps or duplicates) that represents the approximate
	/// position of the message in a thread. This can be used to estimate the rough position of the message in a thread.
	/// </summary>
	[JsonPropertyName("position")]
	public Optional<Int32> Position { get; init; }
}
