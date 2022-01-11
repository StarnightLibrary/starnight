namespace Starnight.Internal.Rest;

/// <summary>
/// Enumerats all major route parameters currently in use by Discord.
/// </summary>
public enum MajorRouteParameter
{
	/// <summary>
	/// This request is bucketed by channel ID.
	/// </summary>
	ChannelId,

	/// <summary>
	/// This request is bucketed by guild ID.
	/// </summary>
	GuildId,

	/// <summary>
	/// This request is bucketed by webhook ID + webhook token.
	/// </summary>
	WebhookData,

	/// <summary>
	/// This request has no known associated bucket.
	/// </summary>
	Unknown,

	/// <summary>
	/// This request is not bucketed by any major parameter.
	/// </summary>
	Unbucketed
}
