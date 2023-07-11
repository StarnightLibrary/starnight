namespace Starnight.Internal.Models.ApplicationCommands;

using System;

using Starnight.Entities;

/// <summary>
/// Represents permissions for a <seealso cref="ApplicationCommand"/>. These are set based
/// on the <seealso cref="ApplicationCommand.DefaultMemberPermissions"/> by default, but
/// can be edited in the Discord client.
/// </summary>
public sealed record ApplicationCommandPermission
{
	/// <summary>
	/// The snowflake identifier of the role, user or channel this permission applies to.
	/// This can also be a
	/// <seealso href="https://discord.com/developers/docs/interactions/application-commands#application-command-permissions-object-application-command-permissions-constants">
	/// permission constant.</seealso>
	/// </summary>
	public required Int64 Id { get; init; }

	/// <summary>
	/// Specifies what kind of entity this permission targets.
	/// </summary>
	public required DiscordApplicationCommandPermissionType Type { get; init; }

	/// <summary>
	/// Indicates whether this overwrite is enabled or not.
	/// </summary>
	public required Boolean Permission { get; init; }
}
