namespace Starnight.Internal.Rest.Payloads.Webhooks;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a request payload to PATCH /webhooks/:webhook_id
/// </summary>
public sealed record ModifyWebhookRequestPayload
{
	/// <summary>
	/// The new default name for this webhook.
	/// </summary>
	[JsonPropertyName("name")]
	public Optional<String> Name { get; init; }

	/// <summary>
	/// Image data for the new default webhook avatar.
	/// </summary>
	[JsonPropertyName("avatar")]
	public Optional<String?> Avatar { get; init; }

	/// <summary>
	/// The channel this webhook should be moved to.
	/// </summary>
	[JsonPropertyName("channel_id")]
	public Optional<Int64> ParentChannelId { get; init; }
}
