namespace Starnight.Internal.Rest.Payloads.Guilds;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a payload to POST /guilds/:guild_id/roles.
/// </summary>
public record CreateRoleRequestPayload
{
	/// <summary>
	/// The name of the to-be-created role.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("name")]
	public String? Name { get; init; }

	/// <summary>
	/// Permissions for this role. Defaults to the @everyone permissions.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("permissions")]
	public String? Permissions { get; init; }

	/// <summary>
	/// RGB color value for this role.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("color")]
	public Int32? Color { get; init; }

	/// <summary>
	/// Whether the role should be hoisted in the sidebar. Defaults to <see langword="false"/>.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("hoist")]
	public Boolean? Hoist { get; init; }

	/// <summary>
	/// The role's icon image, if possible.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("icon")]
	public String? Icon { get; init; }

	/// <summary>
	/// The role's unicode emote as role icon, if possible.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("unicode_emoji")]
	public String? UnicodeEmote { get; init; }

	/// <summary>
	/// Whether the role should be mentionable by everyone.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	[JsonPropertyName("mentionable")]
	public Boolean? Mentionable { get; init; }
}
