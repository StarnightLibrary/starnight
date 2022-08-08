namespace Starnight.Internal.Entities.Teams;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

/// <summary>
/// Represents the parameters required for installing an application.
/// </summary>
public record DiscordApplicationInstallParameters
{
	/// <summary>
	/// The OAuth2 scopes to add this application to the server with.
	/// </summary>
	[JsonPropertyName("scopes")]
	public required IEnumerable<String> Scopes { get; init; }

	/// <summary>
	/// The permissions this application will request for its bot role.
	/// </summary>
	[JsonPropertyName("permissions")]
	public required DiscordPermissions Permissions { get; init; }
}
