namespace Starnight.Internal.Gateway.Objects.Serverbound;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using Starnight.Internal;

/// <summary>
/// Represents the payload of an identify command.
/// </summary>
public sealed record DiscordIdentifyCommandObject
{
	/// <summary>
	/// Stores the bot token for this connection.
	/// </summary>
	[JsonPropertyName("token")]
	public required String Token { get; init; }

	/// <summary>
	/// Connection properties for this connection.
	/// </summary>
	[JsonPropertyName("properties")]
	public required DiscordIdentifyConnectionProperties ConnectionProperties { get; init; }

	/// <summary>
	/// Whether this connection supports compression.
	/// </summary>
	[JsonPropertyName("compress")]
	public Optional<Boolean> Compress { get; init; }

	/// <summary>
	/// Indicates a threshold from which on guilds will be considered large - no offline presences will be initially
	/// sent for large guilds, they have to be fetched separately. This may range from 50 to 250, defaulting to 50 if
	/// left unset.
	/// </summary>
	[JsonPropertyName("large_threshold")]
	public Optional<Int32> LargeGuildThreshold { get; init; }

	/// <summary>
	/// An array of two integers indicating shard data for this connection
	/// </summary>
	/// <remarks>
	/// As first integer you must pass the ID of your shard, as second integer you must pass the number of total shards.
	/// </remarks>
	[JsonPropertyName("shard")]
	public Optional<IEnumerable<Int32>> Shard { get; init; }

	/// <summary>
	/// The initial presence information when connecting to the gateway.
	/// </summary>
	[JsonPropertyName("presence")]
	public Optional<DiscordPresenceUpdateCommandObject> Presence { get; init; }

	/// <summary>
	/// The gateway intents you wish to receive.
	/// </summary>
	[JsonPropertyName("intents")]
	public required Int32 Intents { get; init; }
}
