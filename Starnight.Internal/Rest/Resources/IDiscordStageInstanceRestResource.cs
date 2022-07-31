namespace Starnight.Internal.Rest.Resources;

using System;
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
	/// <returns>The newly created stage instance.</returns>
	public ValueTask<DiscordStageInstance> CreateStageInstanceAsync
	(
		CreateStageInstanceRequestPayload payload,
		String? reason
	);

	/// <summary>
	/// Returns the stage instance associated with the stage channel, if one exists.
	/// </summary>
	/// <param name="channelId">Snowflake identifier of the associated stage channel.</param>
	public ValueTask<DiscordStageInstance?> GetStageInstanceAsync
	(
		Int64 channelId
	);

	/// <summary>
	/// Modifies the given stage instance.
	/// </summary>
	/// <param name="channelId">Snowflake identifier of the parent channel.</param>
	/// <param name="payload">Request payload.</param>
	/// <param name="reason">Optional audit log reason.</param>
	/// <returns>The newly modified stage instance.</returns>
	public ValueTask<DiscordStageInstance> ModifyStageInstanceAsync
	(
		Int64 channelId,
		ModifyStageInstanceRequestPayload payload,
		String? reason
	);

	/// <summary>
	/// Deletes the given stage instance.
	/// </summary>
	/// <param name="channelId">Snowflake identifier of its parent channel.</param>
	/// <param name="reason">Optional audit log reason.</param>
	/// <returns>Whether the request was successful.</returns>
	public ValueTask<Boolean> DeleteStageInstanceAsync
	(
		Int64 channelId,
		String? reason
	);
}
