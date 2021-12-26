namespace Starnight.Internal.Entities.User;

using System;
using System.Text.Json.Serialization;

using Starnight.Internal.Entities.Guild;

/// <summary>
/// Represents a Discord User connection object, as obtained from <c>GET users/@me/connections</c>.
/// Requires the <c>connections</c> OAuth2 scope.
/// </summary>
public class DiscordUserConnection
{
	/// <summary>
	/// String ID of the connection account.
	/// </summary>
	[JsonPropertyName("id")]
	public String Id { get; init; } = default!;

	/// <summary>
	/// Username of the connection account.
	/// </summary>
	[JsonPropertyName("name")]
	public String Name { get; init; } = default!;

	/// <summary>
	/// Service type: twitch, youtube, github etc.
	/// </summary>
	[JsonPropertyName("type")]
	public String Type { get; init; } = default!;

	/// <summary>
	/// Holds whether this connection is revoked.
	/// </summary>
	[JsonPropertyName("revoked")]
	public Boolean? Revoked { get; init; }

	/// <summary>
	/// Holds whether this connection is verified.
	/// </summary>
	[JsonPropertyName("verified")]
	public Boolean Verified { get; init; } = false;

	/// <summary>
	/// Holds whether friend sync is enabled for this connection.
	/// </summary>
	[JsonPropertyName("friend_sync")]
	public Boolean FriendSync { get; init; } = false;

	/// <summary>
	/// Holds whether activities related ot this connection will be shown.
	/// </summary>
	[JsonPropertyName("show_activity")]
	public Boolean ShowActivity { get; init; } = false;

	/// <summary>
	/// Visibility of this connection, see <see cref="DiscordUserConnectionVisibility"/>
	/// </summary>
	[JsonPropertyName("visibility")]
	public Int32 Visibility { get; init; } = 1;

	/// <summary>
	/// Active integrations tied to this user.
	/// </summary>
	[JsonPropertyName("integrations")]
	public DiscordGuildIntegration[]? Integrations { get; init; }
}
