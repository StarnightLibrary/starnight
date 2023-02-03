namespace Starnight.Internal.Entities.Users;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Guilds;

/// <summary>
/// Represents a Discord User connection object, as obtained from <c>GET users/@me/connections</c>.
/// Requires the <c>connections</c> OAuth2 scope.
/// </summary>
public sealed record DiscordUserConnection
{
	/// <summary>
	/// String ID of the connection account.
	/// </summary>
	[JsonPropertyName("id")]
	public required String Id { get; init; }

	/// <summary>
	/// Username of the connection account.
	/// </summary>
	[JsonPropertyName("name")]
	public required String Name { get; init; }

	/// <summary>
	/// Service type: twitch, youtube, github etc.
	/// </summary>
	[JsonPropertyName("type")]
	public required String Type { get; init; }

	/// <summary>
	/// Holds whether this connection is revoked.
	/// </summary>
	[JsonPropertyName("revoked")]
	public Optional<Boolean> Revoked { get; init; }

	/// <summary>
	/// Holds whether this connection is verified.
	/// </summary>
	[JsonPropertyName("verified")]
	public required Boolean Verified { get; init; }

	/// <summary>
	/// Holds whether friend sync is enabled for this connection.
	/// </summary>
	[JsonPropertyName("friend_sync")]
	public required Boolean FriendSync { get; init; }

	/// <summary>
	/// Holds whether activities related ot this connection will be shown.
	/// </summary>
	[JsonPropertyName("show_activity")]
	public required Boolean ShowActivity { get; init; }

	/// <summary>
	/// Visibility of this connection.
	/// </summary>
	[JsonPropertyName("visibility")]
	public required DiscordUserConnectionVisibility Visibility { get; init; }

	/// <summary>
	/// Active integrations tied to this user.
	/// </summary>
	[JsonPropertyName("integrations")]
	public Optional<IEnumerable<DiscordGuildIntegration>> Integrations { get; init; }
}
