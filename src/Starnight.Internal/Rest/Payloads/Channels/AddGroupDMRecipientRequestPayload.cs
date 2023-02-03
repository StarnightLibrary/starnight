namespace Starnight.Internal.Rest.Payloads.Channels;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a payload to PUT /channel/:channel_id/recipients/:user_id.
/// </summary>
public sealed record AddGroupDMRecipientRequestPayload
{
	/// <summary>
	/// Access token of the user, which must have granted you the <c>gdm.join</c> oauth scope.
	/// </summary>
	[JsonPropertyName("access_token")]
	public required String AccessToken { get; init; }

	/// <summary>
	/// Nickname of the user, to be given on join.
	/// </summary>
	[JsonPropertyName("nick")]
	public required String Nickname { get; init; }
}
