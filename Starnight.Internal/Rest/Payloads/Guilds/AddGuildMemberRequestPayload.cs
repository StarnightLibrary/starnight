namespace Starnight.Internal.Rest.Payloads.Guilds;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

/// <summary>
/// Represents the request payload for PUT /guilds/:guild_id/members/:user_id
/// </summary>
/// <remarks>
/// <see cref="AccessToken"/> is required, all other members are optional. See the Discord documentation to see which permissions are needed.
/// </remarks>
public record AddGuildMemberRequestPayload
{
	/// <summary>
	/// An OAuth2 access token granted with the <c>guilds.join</c> scope.
	/// </summary>
	[JsonPropertyName("access_token")]
	public String AccessToken { get; init; } = default!;

	/// <summary>
	/// The nickname to initialize the user with.
	/// </summary>
	[JsonPropertyName("nick")]
	public String? Nickname { get; init; }

	/// <summary>
	/// An array of role IDs to assign immediately upon join.
	/// </summary>
	[JsonPropertyName("roles")]
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
	public IEnumerable<Int64>? Roles { get; init; }

	/// <summary>
	/// Whether to immediately mute the user upon join.
	/// </summary>
	[JsonPropertyName("mute")]
	public Boolean Mute { get; init; }

	/// <summary>
	/// Whether to immediately deafen the user upon join.
	/// </summary>
	[JsonPropertyName("deaf")]
	public Boolean Deafen { get; init; }	
}
