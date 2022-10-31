namespace Starnight.Internal.Rest.Resources;

using System;
using System.Threading;
using System.Threading.Tasks;

using Starnight.Internal.Entities.Voice;

using Starnight.Internal.Rest.Payloads.StageInstances;

/// <summary>
/// Represents a wrapper for all requests to discord's stage instance rest resource.
/// </summary>
public interface IDiscordStageInstanceRestResource
{
	/// <summary>
	/// Creates a new stage instance associated to a stage channel.
	/// </summary>
	/// <param name="payload">Request payload, among others containing the channel ID to create a stage instance for.</param>
	/// <param name="reason">Optional audit log reason.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	/// <returns>The newly created stage instance.</returns>
	public ValueTask<DiscordStageInstance> CreateStageInstanceAsync
	(
		CreateStageInstanceRequestPayload payload,
		String? reason,
		CancellationToken ct = default
	);

	/// <summary>
	/// Returns the stage instance associated with the stage channel, if one exists.
	/// </summary>
	/// <param name="channelId">Snowflake identifier of the associated stage channel.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	public ValueTask<DiscordStageInstance?> GetStageInstanceAsync
	(
		Int64 channelId,
		CancellationToken ct = default
	);

	/// <summary>
	/// Modifies the given stage instance.
	/// </summary>
	/// <param name="channelId">Snowflake identifier of the parent channel.</param>
	/// <param name="payload">Request payload.</param>
	/// <param name="reason">Optional audit log reason.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	/// <returns>The newly modified stage instance.</returns>
	public ValueTask<DiscordStageInstance> ModifyStageInstanceAsync
	(
		Int64 channelId,
		ModifyStageInstanceRequestPayload payload,
		String? reason,
		CancellationToken ct = default
	);

	/// <summary>
	/// Deletes the given stage instance.
	/// </summary>
	/// <param name="channelId">Snowflake identifier of its parent channel.</param>
	/// <param name="reason">Optional audit log reason.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	/// <returns>Whether the request was successful.</returns>
	public ValueTask<Boolean> DeleteStageInstanceAsync
	(
		Int64 channelId,
		String? reason,
		CancellationToken ct = default
	);
}
