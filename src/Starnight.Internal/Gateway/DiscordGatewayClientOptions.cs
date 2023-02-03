namespace Starnight.Internal.Gateway;

using System;

using Microsoft.Extensions.Options;

using Starnight.Internal.Entities.Users;

/// <summary>
/// Represents settings for <see cref="DiscordGatewayClient"/>
/// </summary>
public class DiscordGatewayClientOptions : IOptions<DiscordGatewayClientOptions>
{
	public DiscordGatewayClientOptions Value => this;

	/// <summary>
	/// The intents to pass along with this connection.
	/// </summary>
	public DiscordGatewayIntents Intents { get; set; }

	/// <summary>
	/// Optional presence object.
	/// </summary>
	public DiscordPresence? Presence { get; set; }

	/// <summary>
	/// Specifies a threshold where the gateway will stop sending offline members in the guild member list.
	/// </summary>
	public Int32 LargeGuildThreshold { get; set; } = 50;

	/// <summary>
	/// Specifies information about the current shard, if applicable.
	/// </summary>
	public Int32[]? ShardInformation { get; set; }

	/// <summary>
	/// Specifies a threshold after which the gateway connection is to be considered zombied.
	/// </summary>
	public Int32 ZombieThreshold { get; set; } = 6;
}
