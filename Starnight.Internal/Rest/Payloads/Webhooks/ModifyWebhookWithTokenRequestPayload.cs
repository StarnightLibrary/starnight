namespace Starnight.Internal.Rest.Payloads.Webhooks;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a request payload to PATCH /webhooks/:webhook_id/:webhook_token
/// </summary>
public sealed record ModifyWebhookWithTokenRequestPayload
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
}
