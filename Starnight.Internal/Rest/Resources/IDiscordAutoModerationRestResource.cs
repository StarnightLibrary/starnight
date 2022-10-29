namespace Starnight.Internal.Rest.Resources;

using System;
using System.Collections.Generic;
using System.Threading;
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
	/// <param name="ct">Cancellation token for this request.</param>
	public ValueTask<IEnumerable<DiscordAutoModerationRule>> ListAutoModerationRulesAsync
	(
		Int64 guildId,
		CancellationToken ct  
	);

	/// <summary>
	/// Returns a single auto moderation rule.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild this rule is a part of.</param>
	/// <param name="ruleId">Snowflake identifier of the rule in question.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	public ValueTask<DiscordAutoModerationRule> GetAutoModerationRuleAsync
	(
		Int64 guildId,
		Int64 ruleId,
		CancellationToken ct  
	);

	/// <summary>
	/// Creates a new auto moderation rule in the given guild.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	/// <param name="payload">Request payload.</param>
	/// <param name="reason">Optional audit log reason.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	/// <returns>The newly created audit log rule.</returns>
	public ValueTask<DiscordAutoModerationRule> CreateAutoModerationRuleAsync
	(
		Int64 guildId,
		CreateAutoModerationRuleRequestPayload payload,
		String? reason,
		CancellationToken ct  
	);

	/// <summary>
	/// Modifies an existing auto moderation rule in the given guild.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	/// <param name="ruleId">Snowflake identifier of the rule in question.</param>
	/// <param name="payload">Request payload.</param>
	/// <param name="reason">Optional audit log reason.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	/// <returns>The newly modified auto moderation rule.</returns>
	public ValueTask<DiscordAutoModerationRule> ModifyAutoModerationRuleAsync
	(
		Int64 guildId,
		Int64 ruleId,
		ModifyAutoModerationRuleRequestPayload payload,
		String? reason,
		CancellationToken ct  
	);

	/// <summary>
	/// Deletes an auto moderation rule in the given guild.
	/// </summary>
	/// <param name="guildId">Snowflake identifier of the guild in question.</param>
	/// <param name="ruleId">Snowflake identifier of the rule in question.</param>
	/// <param name="reason">Optional audit log reason.</param>
	/// <param name="ct">Cancellation token for this request.</param>
	/// <returns>Whether deletion was successful</returns>
	public ValueTask<Boolean> DeleteAutoModerationRuleAsync
	(
		Int64 guildId,
		Int64 ruleId,
		String? reason,
		CancellationToken ct  
	);
}
