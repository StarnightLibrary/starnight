namespace Starnight.Internal.Rest.Resources;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Starnight.Internal.Entities.Channels;
using Starnight.Internal.Rest.Payloads.Webhooks;

/// <summary>
/// Represents a wrapper for all requests to discord's webhook rest resource.
/// </summary>
public interface IDiscordWebhookRestResource
{
	/// <summary>
	/// Creates a new webhook in the specified channel.
	/// </summary>
	/// <param name="channelId">Snowflake identifier of the channel in question.</param>
	/// <param name="payload">Request payload.</param>
	/// <param name="reason">Optional audit log reason.</param>
	/// <returns>The newly created webhook object.</returns>
	public ValueTask<DiscordWebhook> CreateWebhookAsync
	(
		Int64 channelId,
		CreateWebhookRequestPayload payload,
		String? reason
	);

	/// <summary>
	/// Returns a list of channel webhook objects.
	/// </summary>
	/// <param name="channelId">Snowflake identifier of the channel in question.</param>
	public ValueTask<IEnumerable<DiscordWebhook>> GetChannelWebhooksAsync
	(
		Int64 channelId
	);
}
