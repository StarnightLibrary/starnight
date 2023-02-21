namespace Starnight.Entities;

/// <summary>
/// Represents the different keyword presets provided by Discord.
/// </summary>
public enum DiscordAutoModerationKeywordPresetType
{
	/// <summary>
	/// Contains words that may be considered forms of swearing or cursing.
	/// </summary>
	Profanity = 1,

	/// <summary>
	/// Contains words that refer to sexually explicit behaviour or activity.
	/// </summary>
	SexualContent,

	/// <summary>
	/// Contains personal insult and terms that may be considered hate speech.
	/// </summary>
	Slurs
}
