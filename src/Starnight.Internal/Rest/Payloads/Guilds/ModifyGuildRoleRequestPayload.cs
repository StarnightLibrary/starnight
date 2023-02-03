namespace Starnight.Internal.Rest.Payloads.Guilds;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a payload to PATCH /guilds/:guild_id/roles/:role_id
/// </summary>
public sealed record ModifyGuildRoleRequestPayload
{
	/// <summary>
	/// The name of the to-be-created role.
	/// </summary>
	[JsonPropertyName("name")]
	public Optional<String?> Name { get; init; }

	/// <summary>
	/// Permissions for this role. Defaults to the @everyone permissions.
	/// </summary>
	[JsonPropertyName("permissions")]
	public Optional<String?> Permissions { get; init; }

	/// <summary>
	/// RGB color value for this role.
	/// </summary>
	[JsonPropertyName("color")]
	public Optional<Int32?> Color { get; init; }

	/// <summary>
	/// Whether the role should be hoisted in the sidebar. Defaults to <see langword="false"/>.
	/// </summary>
	[JsonPropertyName("hoist")]
	public Optional<Boolean?> Hoist { get; init; }

	/// <summary>
	/// The role's icon image, if possible.
	/// </summary>
	[JsonPropertyName("icon")]
	public Optional<String?> Icon { get; init; }

	/// <summary>
	/// The role's unicode emoji as role icon, if possible.
	/// </summary>
	[JsonPropertyName("unicode_emoji")]
	public Optional<String?> UnicodeEmoji { get; init; }

	/// <summary>
	/// Whether the role should be mentionable by everyone.
	/// </summary>
	[JsonPropertyName("mentionable")]
	public Optional<Boolean?> Mentionable { get; init; }
}
