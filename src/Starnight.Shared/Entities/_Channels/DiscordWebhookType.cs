namespace Starnight.Entities;

/// <summary>
/// Represents the different types for webhooks.
/// </summary>
public enum DiscordWebhookType
{
	/// <summary>
	/// Represents a standard incoming webhook.
	/// </summary>
	Incoming = 1,

	/// <summary>
	/// Represents a channel following webhook.
	/// </summary>
	Follower,

	/// <summary>
	/// Represents an interaction webhook.
	/// </summary>
	Application
}
