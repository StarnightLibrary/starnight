namespace Starnight.Internal.Entities.Interactions;

using NetEscapades.EnumGenerators;

/// <summary>
/// Represents a list of locales, currently in use by application command localization.
/// </summary>
/// <remarks>
/// Since hyphens are not allowed in enum identifiers, they are replaced by underscores. If their string representation is required,
/// use <c>(item).<see cref="DiscordLocaleExtensions.ToStringFast(DiscordLocale)"/>.Replace("_", "-");</c>.
/// <see cref="DiscordLocaleExtensions.ToStringFast(DiscordLocale)"/> is an extension method generated on top of <see cref="DiscordLocale"/>,
/// by virtue of source generation.
/// </remarks>
[EnumExtensions]
public enum DiscordLocale
{
	da,
	de,
	en_GB,
	en_US,
	es_ES,
	fr,
	hr,
	it,
	lt,
	hu,
	nl,
	no,
	pl,
	pt_BR,
	ro,
	fi,
	sv_SE,
	vi,
	tr,
	cs,
	el,
	bg,
	ru,
	uk,
	hi,
	th,
	zh_CN,
	ja,
	zh_TW,
	ko
}
