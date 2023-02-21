namespace Starnight.Entities;

using System;

/// <summary>
/// Represents flags for an application.
/// </summary>
/// <remarks>
/// Documentation for the different fields is missing: where the names are not self explanatory you are on your own.
/// Feel free to PR documentation. Additionally, instead of attempting to use more sane names than discord uses,
/// these names are only changed inasmuch from the discord docs as to conform with the C# naming rules.
/// </remarks>
[Flags]
public enum DiscordApplicationFlags
{
	GatewayPresence = 1 << 12,
	GatewayPresenceLimited = 1 << 13,
	GatewayGuildMembers = 1 << 14,
	GatewayGuildMembersLimited = 1 << 15,
	VerificationPendingGuildLimit = 1 << 16,
	Embedded = 1 << 17,
	GatewayMessageContent = 1 << 18,
	GatewayMessageContentLimited = 1 << 19
}
