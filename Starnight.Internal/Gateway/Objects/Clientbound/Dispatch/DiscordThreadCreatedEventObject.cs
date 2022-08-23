namespace Starnight.Internal.Gateway.Objects.Clientbound.Dispatch;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Channels;

/// <summary>
/// The inner payload of a ThreadCreated event.
/// </summary>
public sealed record DiscordThreadCreatedEventObject : DiscordChannel
{
	/// <summary>
	/// Whether this thread was newly created.
	/// </summary>
	[JsonPropertyName("newly_created")]
	public Optional<Boolean> NewlyCreated { get; init; }
}
