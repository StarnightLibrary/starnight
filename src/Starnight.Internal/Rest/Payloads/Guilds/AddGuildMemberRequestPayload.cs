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
public sealed record AddGuildMemberRequestPayload
{
	/// <summary>
	/// An OAuth2 access token granted with the <c>guilds.join</c> scope.
	/// </summary>
	[JsonPropertyName("access_token")]
	public required String AccessToken { get; init; }

	/// <summary>
	/// The nickname to initialize the user with.
	/// </summary>
	[JsonPropertyName("nick")]
	public Optional<String> Nickname { get; init; }

	/// <summary>
	/// An array of role IDs to assign immediately upon join.
	/// </summary>
	[JsonPropertyName("roles")]
	public Optional<IEnumerable<Int64>> Roles { get; init; }

	/// <summary>
	/// Whether to immediately mute the user upon join.
	/// </summary>
	[JsonPropertyName("mute")]
	public Optional<Boolean> Mute { get; init; }

	/// <summary>
	/// Whether to immediately deafen the user upon join.
	/// </summary>
	[JsonPropertyName("deaf")]
	public Optional<Boolean> Deafen { get; init; }
}
