namespace Starnight.Internal.Entities.Guilds.AutoModeration;

/// <summary>
/// Represents the various types of contents which can trigger rules.
/// </summary>
public enum DiscordAutoModerationTriggerType
{
	/// <summary>
	/// Checks if the content contains words from a user-defined list of keywords.
	/// </summary>
	/// <remarks>
	/// There can be up to 3 <see cref="Keyword"/> rules per guild.
	/// </remarks>
	Keyword = 1,

	/// <summary>
	/// Checks if the content contains any harmful links.
	/// </summary>
	/// <remarks>
	/// There can be up to 1 <see cref="HarmfulLink"/> rule per guild.
	/// </remarks>
	HarmfulLink,

	/// <summary>
	/// Checks if the content is generic spam.
	/// </summary>
	/// <remarks>
	/// There can be up to 1 <see cref="Spam"/> rule per guild.
	/// </remarks>
	Spam,

	/// <summary>
	/// Checks if the content contains words from internal, pre-defined lists of keywords.
	/// </summary>
	/// <remarks>
	/// There can be up to 1 <see cref="KeywordPreset"/> rule per guild.
	/// </remarks>
	KeywordPreset
}
