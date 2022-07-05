namespace Starnight.Internal.Rest.Payloads.Users;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a request payload to PATCH /users/@me.
/// </summary>
public record ModifyCurrentUserRequestPayload
{
	/// <summary>
	/// The new username for this user.
	/// </summary>
	/// <remarks>
	/// Changing the username may cause the user's discriminator to be randomized.
	/// </remarks>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("username")]
	public String? Username { get; init; }

	/// <summary>
	/// Image data string representing the user's avatar.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("avatar")]
	public String? Avatar { get; init; }
}
