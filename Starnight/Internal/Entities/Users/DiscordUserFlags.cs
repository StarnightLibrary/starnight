namespace Starnight.Internal.Entities.Users;

/// <summary>
/// Enum holding all possible user flags. Some bitshifts are missing, those appear to be used for nitro badges etc (or just not exist at all.)
/// It is important converters for <c>user.public_flags</c> keep this in mind - bits not used here may still be used by the official client and
/// should not be treated as malformatted data.
/// </summary>
[Flags]
public enum DiscordUserFlags
{
	/// <summary>
	/// None.
	/// </summary>
	None = 0,

	/// <summary>
	/// Profile badge indicating this user is a Discord employee.
	/// </summary>
	DiscordEmployee = 1 << 0,

	/// <summary>
	/// Profile badge indicating this user owns a Partnered server.
	/// </summary>
	// discord, really? Server or Guild, decide.
	PartneredServerOwner = 1 << 1,

	/// <summary>
	/// Profile badge indicating this user has attended a real-world HypeSquad event.
	/// </summary>
	HypeSquadEvents = 1 << 2,

	/// <summary>
	/// First of two badges for bug hunters. Discord doesn't tell us any further information.
	/// </summary>
	BugHunterLevel1 = 1 << 3, // 4 and 5 went missing, cope - discord, probably

	/// <summary>
	/// Profile badge indicating this user is a member of the HypeSquad House of Bravery.
	/// </summary>
	HouseBravery = 1 << 6,

	/// <summary>
	/// Profile badge indicating this user is a member of the Hypesquad House of Brilliance.
	/// </summary>
	HouseBrilliance = 1 << 7,

	/// <summary>
	/// Profile badge indicating this user is a member of the HypeSquad House of Balance.
	/// </summary>
	HouseBalance = 1 << 8,

	/// <summary>
	/// Profile badge indicating this user has purchased Nitro before 10/10/2018.
	/// </summary>
	EarlySupporter = 1 << 9,

	/// <summary>
	/// Profile badge indicating... what, exactly? Discord doesn't tell us.
	/// </summary>
	TeamUser = 1 << 10, // 11 to 13 are bad numbers, everyone knows that

	/// <summary>
	/// Bug hunter badge two: electric boogaloo.
	/// </summary>
	BugHunterLevel2 = 1 << 14, // didnt need 15 anyways

	/// <summary>
	/// Profile badge indicating this bot is, in fact, verified. This is not rendered as a profile badge by the client, rather as the good old
	/// checkmarked bot tag.
	/// </summary>
	VerifiedBot = 1 << 16,

	/// <summary>
	/// Profile badge indicating this user has developed a bot which obtained verification before Discord stopped giving out the badge.
	/// </summary>
	EarlyVerifiedBotDeveloper = 1 << 17,

	/// <summary>
	/// Profile badge indicating this user has passed Discord's Moderator Exam.
	/// </summary>
	DiscordCertifiedModerator = 1 << 18
}
