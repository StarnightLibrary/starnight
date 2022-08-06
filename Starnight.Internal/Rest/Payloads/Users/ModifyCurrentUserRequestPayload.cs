namespace Starnight.Internal.Rest.Payloads.Users;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a request payload to PATCH /users/@me.
/// </summary>
public sealed record ModifyCurrentUserRequestPayload
{
	/// <summary>
	/// The new username for this user.
	/// </summary>
	/// <remarks>
	/// Changing the username may cause the user's discriminator to be randomized.
	/// </remarks>
	[JsonPropertyName("username")]
	public Optional<String> Username { get; init; }

	/// <summary>
	/// Image data string representing the user's avatar.
	/// </summary>
	[JsonPropertyName("avatar")]
	public Optional<String?> Avatar { get; init; }
}
