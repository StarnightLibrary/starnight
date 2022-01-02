namespace Starnight.Internal.Entities.Guild.Audit;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Channel;

/// <summary>
/// Represents an audit log reflected change.
/// </summary>
public class DiscordAuditLogChange
{
	/// <summary>
	/// The new value of the object in question. The type must be inferred from the action key.
	/// </summary>
	[JsonPropertyName("new_value")]
	public Object NewValue { get; init; } = default!;

	/// <summary>
	/// The old value of the object in question. The type must be inferred from the action key.
	/// </summary>
	[JsonPropertyName("old_value")]
	public Object OldValue { get; init; } = default!;

	/// <summary>
	/// The action key for this change.
	/// </summary>
	[JsonPropertyName("key")]
	public String ActionKey { get; init; } = default!;

	public static readonly Dictionary<String, Type> ActionTypeMap = new()
	{
		["afk_channel_id"] = typeof(String),
		["afk_timeout"] = typeof(Int32),
		["allow"] = typeof(String),
		["application_id"] = typeof(String),
		["archived"] = typeof(Boolean),
		["asset"] = typeof(String),
		["auto_archive_duration"] = typeof(Int32),
		["available"] = typeof(Boolean),
		["avatar_hash"] = typeof(String),
		["banner_hash"] = typeof(String),
		["bitrate"] = typeof(Int32),
		["channel_id"] = typeof(String),
		["code"] = typeof(String),
		["color"] = typeof(Int32),
		["deaf"] = typeof(Boolean),
		["default_auto_archive_duration"] = typeof(Int32),
		["default_message_notifications"] = typeof(Int32),
		["deny"] = typeof(String),
		["description"] = typeof(String),
		["discovery_splash_hash"] = typeof(String),
		["enable_emoticons"] = typeof(Boolean),
		["entity_type"] = typeof(Int32),
		["expire_behavior"] = typeof(Int32),
		["expire_grace_period"] = typeof(Int32),
		["explicit_content_filter"] = typeof(Int32),
		["format_type"] = typeof(Int32),
		["guild_id"] = typeof(String),
		["hoist"] = typeof(Boolean),
		["icon_hash"] = typeof(String),
		["id"] = typeof(String),
		["inviter_id"] = typeof(String),
		["location"] = typeof(String),
		["locked"] = typeof(Boolean),
		["max_age"] = typeof(Int32),
		["max_uses"] = typeof(Int32),
		["mentionable"] = typeof(Boolean),
		["mfa_level"] = typeof(Int32),
		["mute"] = typeof(Boolean),
		["name"] = typeof(String),
		["nick"] = typeof(String),
		["nsfw"] = typeof(Boolean),
		["owner_id"] = typeof(String),
		["permission_overwrites"] = typeof(DiscordChannelOverwrite[]),
		["permissions"] = typeof(String),
		["position"] = typeof(Int32),
		["preferred_locale"] = typeof(String),
		["privacy_level"] = typeof(Int32),
		["prune_delete_days"] = typeof(Int32),
		["public_updates_channel_id"] = typeof(String),
		["rate_limit_per_user"] = typeof(Int32),
		["region"] = typeof(String),
		["rules_channel_id"] = typeof(String),
		["splash_hash"] = typeof(String),
		["status"] = typeof(Int32),
		["system_channel_id"] = typeof(String),
		["tags"] = typeof(String),
		["temporary"] = typeof(Boolean),
		["topic"] = typeof(String),
		["type"] = typeof(String),
		["unicode_emoji"] = typeof(String),
		["user_limit"] = typeof(Int32),
		["uses"] = typeof(Int32),
		["vanity_url_code"] = typeof(String),
		["verification_level"] = typeof(Int32),
		["widget_channel_id"] = typeof(String),
		["widget_enabled"] = typeof(Boolean),
		["$add"] = typeof(DiscordRole[]),
		["$remove"] = typeof(DiscordRole[])
	};
}
