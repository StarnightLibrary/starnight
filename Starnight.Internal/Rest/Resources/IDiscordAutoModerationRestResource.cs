namespace Starnight.Internal.Rest.Resources;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Starnight.Internal.Entities.Guilds.AutoModeration;
using Starnight.Internal.Rest.Payloads.AutoModeration;

/// <summary>
/// Represents a wrapper for all requests to discord's audit log rest resource.
/// </summary>
public interface IDiscordAutoModerationRestResource
{
	/// <summary>
	/// Returns a list of all auto moderation rules currently configured for the guild.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	public ValueTask<IEnumerable<DiscordAutoModerationRule>> ListAutoModerationRulesAsync
	(
		Int64 guildId
	);

	/// <summary>
	/// Returns a single auto moderation rule.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild this rule is a part of.</param>
	/// <param name="ruleId">Snowflake identifier of the rule in question.</param>
	public ValueTask<DiscordAutoModerationRule> GetAutoModerationRuleAsync
	(
		Int64 guildId,
		Int64 ruleId
	);

	/// <summary>
	/// Creates a new auto moderation rule in the given guild.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	/// <param name="payload">Request payload.</param>
	/// <param name="reason">Optional audit log reason.</param>
	/// <returns>The newly created audit log rule.</returns>
	public ValueTask<DiscordAutoModerationRule> CreateAutoModerationRuleAsync
	(
		Int64 guildId,
		CreateAutoModerationRuleRequestPayload payload,
		String? reason
	);
}
