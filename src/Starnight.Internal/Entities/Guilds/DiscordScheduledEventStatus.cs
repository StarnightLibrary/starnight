namespace Starnight.Entities;

/// <summary>
/// Represents the status of a scheduled event.
/// </summary>
/// <remarks>
/// Once the status is set to <see cref="Completed"/> or <see cref="Cancelled"/>, the status can no longer be updated.
/// </remarks>
public enum DiscordScheduledEventStatus
{
	Scheduled = 1,
	Active,
	Completed,
	Cancelled
}
