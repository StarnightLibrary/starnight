namespace Starnight.Internal.Rest.Payloads.Webhooks;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a payload to POST /channels/:channel_id/webhooks
/// </summary>
public sealed record CreateWebhookRequestPayload
{
	/// <summary>
	/// The name of the webhook.
	/// </summary>
	/// <remarks>
	/// This follows several requirements:<br/><br/>
	/// <list type="bullet">
	///		<item>A webhook name can contain up to 80 characters, unlike usernames/nicknames which are limited to 32.</item>
	///		<item>A webhook name is subject to all other requirements usernames and nicknames are subject to.</item>
	///		<item>Webhook names cannot contain the substring <i>clyde</i>, case-insensitively.</item>
	/// </list>
	/// If the name does not fit all three requirements, it will be rejected and an error will be returned.
	/// </remarks>
	[JsonPropertyName("name")]
	public required String Name { get; init; }

	/// <summary>
	/// Image for the default webhook avatar.
	/// </summary>
	public Optional<String?> Avatar { get; init; }
}
