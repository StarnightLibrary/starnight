namespace Starnight.Internal.Rest.Resources;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Starnight.Internal.Entities.Guilds;
using Starnight.Internal.Rest.Payloads.ScheduledEvents;

/// <summary>
/// Represents a wrapper for all requests to discord's scheduled events rest resource.
/// </summary>
public interface IDiscordScheduledEventRestResource
{
	/// <summary>
	/// Returns a list of scheduled events taking place in this guild.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	/// <param name="withUserCount">Whether the answer should include user counts.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	public ValueTask<IEnumerable<DiscordScheduledEvent>> ListScheduledEventsAsync
	(
		Int64 guildId,
		Boolean? withUserCount,
		CancellationToken ct = default
	);

	/// <summary>
	/// Creates a new scheduled event in the specified guild.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	/// <param name="payload">Event creation payload.</param>
	/// <param name="reason">Optional audit log reason</param>
	/// <param name="ct">Cancellation token for this request.</param>
	/// <returns>The newly created scheduled event.</returns>
	public ValueTask<DiscordScheduledEvent> CreateScheduledEventAsync
	(
		Int64 guildId,
		CreateScheduledEventRequestPayload payload,
		String? reason,
		CancellationToken ct = default
	);

	/// <summary>
	/// Returns the requested scheduled event.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild this scheduled event takes place in.</param>
	/// <param name="eventId">Snowflake identifier of the scheduled event in qeustion.</param>
	/// <param name="withUserCount">
	/// Specifies whether the number of users subscribed to this event should be included.
	/// </param>
	/// <param name="ct">Cancellation token for this request.</param>
	public ValueTask<DiscordScheduledEvent> GetScheduledEventAsync
	(
		Int64 guildId,
		Int64 eventId,
		Boolean? withUserCount,
		CancellationToken ct = default
	);

	/// <summary>
	/// Modifies the given scheduled event.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild this event takes place in.</param>
	/// <param name="eventId">Snowflake identifier of the event to be modified.</param>
	/// <param name="payload">Request payload.</param>
	/// <param name="reason">Optional audit log reason.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	/// <returns>The newly modified scheduled event.</returns>
	public ValueTask<DiscordScheduledEvent> ModifyScheduledEventAsync
	(
		Int64 guildId,
		Int64 eventId,
		ModifyScheduledEventRequestPayload payload,
		String? reason,
		CancellationToken ct = default
	);

	/// <summary>
	/// Deletes the given scheduled event.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild this event takes place in.</param>
	/// <param name="eventId">Snowflake identifier of the event to be modified.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	/// <returns>Whether the deletion was successful.</returns>
	public ValueTask<Boolean> DeleteScheduledEventAsync
	(
		Int64 guildId,
		Int64 eventId,
		CancellationToken ct = default
	);

	/// <summary>
	/// Returns <seealso cref="DiscordScheduledEventUser"/> objects for each participant of the given scheduled event.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild this scheduled event belongs to.</param>
	/// <param name="eventId">Snowflake identifier of the scheduled event in question.</param>
	/// <param name="limit">Number of users to return.</param>
	/// <param name="withMemberObject">Specifies whether the response should include guild member data.</param>
	/// <param name="before">Only return users before the given snowflake ID, used for pagination.</param>
	/// <param name="after">Only return users after the given snowflake ID, used for pagination.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	public ValueTask<IEnumerable<DiscordScheduledEventUser>> GetScheduledEventUsersAsync
	(
		Int64 guildId,
		Int64 eventId,
		Int32? limit,
		Boolean? withMemberObject,
		Int64? before,
		Int64? after,
		CancellationToken ct = default
	);
}
