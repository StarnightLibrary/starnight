namespace Starnight.Internal.Rest.Resources;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Starnight.Internal.Entities.Channels;
using Starnight.Internal.Entities.Messages;
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

	/// <summary>
	/// Returns a list of guild webhook objects.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	public ValueTask<IEnumerable<DiscordWebhook>> GetGuildWebhooksAsync
	(
		Int64 guildId
	);

	/// <summary>
	/// Returns the specified webhook object.
	/// </summary>
	/// <param name="webhookId">Snowflake identifier of the webhook in question.</param>
	public ValueTask<DiscordWebhook> GetWebhookAsync
	(
		Int64 webhookId
	);

	/// <summary>
	/// Returns the specified webhook object.
	/// </summary>
	/// <remarks>
	/// This endpoint does not require authentication and is not counted to your global ratelimits.
	/// </remarks>
	/// <param name="webhookId">Snowflake identifier of the webhook in question.</param>
	/// <param name="webhookToken">Access token to this webhook.</param>
	public ValueTask<DiscordWebhook> GetWebhookWithTokenAsync
	(
		Int64 webhookId,
		String webhookToken
	);

	/// <summary>
	/// Modifies the given webhook.
	/// </summary>
	/// <param name="webhookId">Snowflake identifier of the webhook to edit.</param>
	/// <param name="payload">Request payload.</param>
	/// <param name="reason">Optional audit log reason</param>
	/// <returns>The updated webhook object.</returns>
	public ValueTask<DiscordWebhook> ModifyWebhookAsync
	(
		Int64 webhookId,
		ModifyWebhookRequestPayload payload,
		String? reason
	);

	/// <summary>
	/// Modifies the given webhook.
	/// </summary>
	/// <remarks>
	/// This endpoint does not require authentication and is not counted to your global ratelimits.
	/// </remarks>
	/// <param name="webhookId">Snowflake identifier of the webhook to edit.</param>
	/// <param name="webhookToken">Webhook token of the webhook to edit.</param>
	/// <param name="payload">Request payload.</param>
	/// <param name="reason">Optional audit log reason.</param>
	/// <returns>The updated webhook object.</returns>
	public ValueTask<DiscordWebhook> ModifyWebhookWithTokenAsync
	(
		Int64 webhookId,
		String webhookToken,
		ModifyWebhookWithTokenRequestPayload payload,
		String? reason
	);

	/// <summary>
	/// Deletes the given webhook.
	/// </summary>
	/// <param name="webhookId">Snowflake identifier of the webhook to delete.</param>
	/// <param name="reason">Optional audit log reason.</param>
	/// <returns>Whether the deletion was successful.</returns>
	public ValueTask<Boolean> DeleteWebhookAsync
	(
		Int64 webhookId,
		String? reason
	);

	/// <summary>
	/// Deletes the given webhook.
	/// </summary>
	/// <remarks>
	/// This endpoint does not require authentication and is not counted to your global ratelimits.
	/// </remarks>
	/// <param name="webhookId">Snowflake identifier of the webhook to delete.</param>
	/// <param name="webhookToken">Webhook token of the webhook to delete.</param>
	/// <param name="reason">Optional audit log reason.</param>
	/// <returns>Whether the deletion was successful.</returns>
	public ValueTask<Boolean> DeleteWebhookWithTokenAsync
	(
		Int64 webhookId,
		String webhookToken,
		String? reason
	);

	/// <summary>
	/// Executes the given webhook.
	/// </summary>
	/// <param name="webhookId">Snowflake identifier of the webhook to delete.</param>
	/// <param name="webhookToken">Webhook token of the webhook to delete.</param>
	/// <param name="wait">
	///		Specifies whether to wait for server confirmation. If this is set to true, a <see cref="DiscordMessage"/>
	///		object will be returned, if not, <see langword="null"/> will be returned on success instead. Defaults to
	///		<see langword="false"/>
	///	</param>
	/// <param name="threadId">
	///		Specifies a thread to send the message to rather than directly to the parent channel. If the thread is
	///		archived, this will automatically unarchive it. Only threads with the same parent channel as the webhook
	///		can be passed.
	/// </param>
	/// <param name="payload">Request payload.</param>
	/// <returns>
	/// If <paramref name="wait"/> was set to <see langword="true"/>, a <see cref="DiscordMessage"/> object.<br/>
	/// If <paramref name="wait"/> was set to <see langword="false"/>, <see langword="null"/>.
	/// </returns>
	public ValueTask<DiscordMessage?> ExecuteWebhookAsync
	(
		Int64 webhookId,
		String webhookToken,
		Boolean? wait,
		Int64? threadId,
		ExecuteWebhookRequestPayload payload
	);

	/// <summary>
	/// Returns a previously-sent webhook message from the same token.
	/// </summary>
	/// <param name="webhookId">Snowflake identifier of your webhook.</param>
	/// <param name="webhookToken">Webhook token for your webhook. This must match the token of the original author.</param>
	/// <param name="messageId">Snowflake identifier of the message in question.</param>
	/// <param name="threadId">
	///		Specifies the thread to search in rather than the parent channel. Only threads with the same parent channel
	///		as the webhook can be passed.
	/// </param>
	public ValueTask<DiscordMessage> GetWebhookMessageAsync
	(
		Int64 webhookId,
		String webhookToken,
		Int64 messageId,
		Int64? threadId
	);
}
