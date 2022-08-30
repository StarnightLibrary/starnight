namespace Starnight.Internal.Gateway.Payloads.Clientbound.Dispatch;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Guilds.AutoModeration;

/// <summary>
/// Represents the inner payload of an AutoModerationActionExecuted event.
/// </summary>
public sealed record AutoModerationActionExecutedPayload
{
	/// <summary>
	/// The ID of the guild in which the action was executed.
	/// </summary>
	[JsonPropertyName("guild_id")]
	public required Int64 GuildId { get; init; }

	/// <summary>
	/// The executed action.
	/// </summary>
	[JsonPropertyName("action")]
	public required DiscordAutoModerationAction Action { get; init; }

	/// <summary>
	/// The ID of the rule this action belongs to.
	/// </summary>
	[JsonPropertyName("rule_id")]
	public required Int64 RuleId { get; init; }

	/// <summary>
	/// The trigger type of the rule which was triggered.
	/// </summary>
	[JsonPropertyName("rule_trigger_type")]
	public required DiscordAutoModerationTriggerType TriggerType { get; init; }

	/// <summary>
	/// The ID of the user which caused the rule to trigger.
	/// </summary>
	[JsonPropertyName("user_id")]
	public required Int64 UserId { get; init; }

	/// <summary>
	/// The ID of the channel in which the auto-moderated content was posted.
	/// </summary>
	[JsonPropertyName("channel_id")]
	public Optional<Int64> ChannelId { get; init; }

	/// <summary>
	/// The ID of the message which was auto-moderated. This will not exist if the message was blocked.
	/// </summary>
	[JsonPropertyName("message_id")]
	public Optional<Int64> MessageId { get; init; }

	/// <summary>
	/// The ID of any auto-moderation alert message, if one was sent.
	/// </summary>
	[JsonPropertyName("alert_system_message_id")]
	public Optional<Int64> AlertMessageId { get; init; }

	/// <summary>
	/// The content of the message which got auto-moderated.
	/// </summary>
	[JsonPropertyName("content")]
	public Optional<String> Content { get; init; }

	/// <summary>
	/// The word or phrase configured in the rule which triggered the rule.
	/// </summary>
	[JsonPropertyName("matched_keyword")]
	public String? MatchedKeyword { get; init; }

	/// <summary>
	/// The substring in <see cref="Content"/> which triggered the rule.
	/// </summary>
	[JsonPropertyName("matched_content")]
	public Optional<String?> MatchedContent { get; init; }
}
