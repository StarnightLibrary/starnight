namespace Starnight.Internal.Entities.Voice;

using System;

/// <summary>
/// Represents the different stage privacy levels.
/// </summary>
public enum DiscordStagePrivacyLevel
{
	/// <summary>
	/// This stage is visible publicly.
	/// </summary>
	[Obsolete("Public stages and therefore stage discovery is deprecated", DiagnosticId = "SE0006")]
	Public = 1,

	/// <summary>
	/// This stage is visible exclusively to guild members.
	/// </summary>
	GuildOnly
}
