namespace Starnight.Caching.Listeners;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Starnight.Caching.Services;
using Starnight.Internal.Entities.Channels;
using Starnight.Internal.Entities.Channels.Threads;
using Starnight.Internal.Entities.Guilds;
using Starnight.Internal.Entities.Interactions;
using Starnight.Internal.Entities.Messages;
using Starnight.Internal.Entities.Stickers;
using Starnight.Internal.Entities.Users;
using Starnight.Internal.Gateway.Events.Inbound.Dispatch;
using Starnight.Internal.Gateway.Listeners;

/// <summary>
/// Represents a listener handling incoming create/update events.
/// </summary>
internal class PreEventListener :
	IListener<DiscordChannelCreatedEvent>,
	IListener<DiscordChannelUpdatedEvent>,
	IListener<DiscordThreadCreatedEvent>,
	IListener<DiscordThreadUpdatedEvent>,
	IListener<DiscordThreadListSyncEvent>,
	IListener<DiscordThreadMemberUpdatedEvent>,
	IListener<DiscordThreadMembersUpdatedEvent>,
	IListener<DiscordGuildCreatedEvent>,
	IListener<DiscordGuildUpdatedEvent>,
	IListener<DiscordGuildEmojisUpdatedEvent>,
	IListener<DiscordGuildStickersUpdatedEvent>,
	IListener<DiscordGuildMemberAddedEvent>,
	IListener<DiscordGuildMemberUpdatedEvent>,
	IListener<DiscordGuildMembersChunkEvent>,
	IListener<DiscordGuildRoleCreatedEvent>,
	IListener<DiscordGuildRoleUpdatedEvent>,
	IListener<DiscordScheduledEventCreatedEvent>,
	IListener<DiscordScheduledEventUpdatedEvent>,
	IListener<DiscordInteractionCreatedEvent>,
	IListener<DiscordMessageCreatedEvent>,
	IListener<DiscordMessageUpdatedEvent>,
	IListener<DiscordPresenceUpdatedEvent>
{
	private readonly IStarnightCacheService cache;

	public PreEventListener
	(
		IStarnightCacheService cache
	)
		=> this.cache = cache;

	public async ValueTask ListenAsync
	(
		DiscordChannelCreatedEvent @event
	)
	{
		await this.cache.CacheObjectAsync
		(
			KeyHelper.GetChannelKey
			(
				@event.Data.Id
			),
			@event.Data
		);
	}

	public async ValueTask ListenAsync
	(
		DiscordChannelUpdatedEvent @event
	)
	{
		await this.cache.CacheObjectAsync
		(
			KeyHelper.GetChannelKey
			(
				@event.Data.Id
			),
			@event.Data
		);
	}

	public async ValueTask ListenAsync
	(
		DiscordThreadCreatedEvent @event
	)
	{
		await this.cache.CacheObjectAsync
		(
			KeyHelper.GetChannelKey
			(
				@event.Data.Id
			),
			@event.Data
		);
	}

	public async ValueTask ListenAsync
	(
		DiscordThreadUpdatedEvent @event
	)
	{
		await this.cache.CacheObjectAsync
		(
			KeyHelper.GetChannelKey
			(
				@event.Data.Id
			),
			@event.Data
		);
	}

	public async ValueTask ListenAsync
	(
		DiscordThreadListSyncEvent @event
	)
	{
		foreach(DiscordChannel thread in @event.Data.Threads)
		{
			await this.cache.CacheObjectAsync
			(
				KeyHelper.GetChannelKey
				(
					thread.Id
				),
				thread
			);
		}

		foreach(DiscordThreadMember threadMember in @event.Data.ThreadMemberObjects)
		{
			await this.cache.CacheObjectAsync
			(
				KeyHelper.GetThreadMemberKey
				(
					threadMember.ThreadId,
					threadMember.UserId
				),
				threadMember
			);
		}
	}

	public async ValueTask ListenAsync
	(
		DiscordThreadMemberUpdatedEvent @event
	)
	{
		await this.cache.CacheObjectAsync
		(
			KeyHelper.GetThreadMemberKey
			(
				@event.Data.ThreadId,
				@event.Data.UserId
			),
			@event.Data
		);
	}

	public async ValueTask ListenAsync
	(
		DiscordThreadMembersUpdatedEvent @event
	)
	{
		// only deal with added members here, we deal with removed members later
		if(!@event.Data.AddedMembers.HasValue)
		{
			return;
		}

		foreach(DiscordThreadMember member in @event.Data.AddedMembers.Value)
		{
			await this.cache.CacheObjectAsync
			(
				KeyHelper.GetThreadMemberKey
				(
					@event.Data.ThreadId,
					member.UserId
				),
				member
			);
		}
	}

	public async ValueTask ListenAsync
	(
		DiscordGuildCreatedEvent @event
	)
	{
		await this.cache.CacheObjectAsync
		(
			KeyHelper.GetGuildKey
			(
				@event.Data.Id
			),
			@event.Data
		);
	}

	public async ValueTask ListenAsync
	(
		DiscordGuildUpdatedEvent @event
	)
	{
		await this.cache.CacheObjectAsync
		(
			KeyHelper.GetGuildKey
			(
				@event.Data.Id
			),
			@event.Data
		);
	}

	public async ValueTask ListenAsync
	(
		DiscordGuildEmojisUpdatedEvent @event
	)
	{
		foreach(DiscordEmoji emoji in @event.Data.Emojis)
		{
			await this.cache.CacheObjectAsync
			(
				KeyHelper.GetEmojiKey
				(
					emoji.Id!.Value
				),
				emoji
			);
		}
	}

	public async ValueTask ListenAsync
	(
		DiscordGuildStickersUpdatedEvent @event
	)
	{
		foreach(DiscordSticker sticker in @event.Data.Stickers)
		{
			await this.cache.CacheObjectAsync
			(
				KeyHelper.GetStickerKey
				(
					sticker.Id
				),
				sticker
			);
		}
	}

	public async ValueTask ListenAsync
	(
		DiscordGuildMemberAddedEvent @event
	)
	{
		if(!@event.Data.User.HasValue)
		{
			return;
		}

		await this.cache.CacheObjectAsync
		(
			KeyHelper.GetGuildMemberKey
			(
				@event.Data.GuildId.Value,
				@event.Data.User.Value.Id
			),
			@event.Data
		);
	}

	public async ValueTask ListenAsync
	(
		DiscordGuildMemberUpdatedEvent @event
	)
	{
		if(!@event.Data.User.HasValue)
		{
			return;
		}

		await this.cache.CacheObjectAsync
		(
			KeyHelper.GetGuildMemberKey
			(
				@event.Data.GuildId.Value,
				@event.Data.User.Value.Id
			),
			@event.Data
		);
	}

	// we remove the not found members from cache in the post event listener
	public async ValueTask ListenAsync
	(
		DiscordGuildMembersChunkEvent @event
	)
	{
		await Parallel.ForEachAsync
		(
			@event.Data.Members,
			async (member, _) =>
			{
				if(!member.User.HasValue)
				{
					return;
				}

				await this.cache.CacheObjectAsync
				(
					KeyHelper.GetGuildMemberKey
					(
						@event.Data.GuildId,
						member.User.Value.Id
					),
					member
				);
			}
		);
	}

	public async ValueTask ListenAsync
	(
		DiscordGuildRoleCreatedEvent @event
	)
	{
		await this.cache.CacheObjectAsync
		(
			KeyHelper.GetRoleKey
			(
				@event.Data.Role.Id
			),
			@event.Data.Role
		);
	}

	public async ValueTask ListenAsync
	(
		DiscordGuildRoleUpdatedEvent @event
	)
	{
		await this.cache.CacheObjectAsync
		(
			KeyHelper.GetRoleKey
			(
				@event.Data.Role.Id
			),
			@event.Data.Role
		);
	}

	public async ValueTask ListenAsync
	(
		DiscordScheduledEventCreatedEvent @event
	)
	{
		await this.cache.CacheObjectAsync
		(
			KeyHelper.GetScheduledEventKey
			(
				@event.Data.Id
			),
			@event.Data
		);
	}

	public async ValueTask ListenAsync
	(
		DiscordScheduledEventUpdatedEvent @event
	)
	{
		await this.cache.CacheObjectAsync
		(
			KeyHelper.GetScheduledEventKey
			(
				@event.Data.Id
			),
			@event.Data
		);
	}

	public async ValueTask ListenAsync
	(
		DiscordInteractionCreatedEvent @event
	)
	{
		// make this more readable since we'll be using this a lot
		DiscordInteraction interaction = @event.Data;

		if(interaction.Member.Resolve(out DiscordGuildMember? member))
		{
			await this.cache.CacheObjectAsync
			(
				KeyHelper.GetGuildMemberKey
				(
					member.GuildId.Value,
					member.User.Value.Id
				),
				member
			);
		}

		if(interaction.User.Resolve(out DiscordUser? user))
		{
			await this.cache.CacheObjectAsync
			(
				KeyHelper.GetUserKey
				(
					user.Id
				),
				user
			);
		}

		if(interaction.Message.Resolve(out DiscordMessage? message))
		{
			await this.cache.CacheObjectAsync
			(
				KeyHelper.GetMessageKey
				(
					message.Id
				),
				message
			);
		}

		if
		(
			!interaction.Data.Resolve(out DiscordInteractionData? data)
			|| !data.ResolvedData.Resolve(out DiscordInteractionResolvedData? resolved)
		)
		{
			return;
		}

		if(resolved.ResolvedUsers.Resolve(out IDictionary<Int64, DiscordUser>? resolvedUsers))
		{
			await Parallel.ForEachAsync
			(
				resolvedUsers,
				async (pair, _) =>
					await this.cache.CacheObjectAsync
					(
						KeyHelper.GetUserKey
						(
							pair.Key
						),
						pair.Value
					)
			);
		}

		if(resolved.ResolvedMessages.Resolve(out IDictionary<Int64, DiscordMessage>? resolvedMessages))
		{
			await Parallel.ForEachAsync
			(
				resolvedMessages,
				async (pair, _) =>
					await this.cache.CacheObjectAsync
					(
						KeyHelper.GetMessageKey
						(
							pair.Key
						),
						pair.Value
					)
			);
		}

		if(resolved.ResolvedChannels.Resolve(out IDictionary<Int64, DiscordChannel>? resolvedChannels))
		{
			await Parallel.ForEachAsync
			(
				resolvedChannels,
				async (pair, _) =>
					await this.cache.CacheObjectAsync
					(
						KeyHelper.GetChannelKey
						(
							pair.Key
						),
						pair.Value
					)
			);
		}

		if(resolved.ResolvedGuildMembers.Resolve(out IDictionary<Int64, DiscordGuildMember>? resolvedMembers))
		{
			await Parallel.ForEachAsync
			(
				resolvedMembers,
				async (pair, _) =>
					await this.cache.CacheObjectAsync
					(
						KeyHelper.GetGuildMemberKey
						(
							interaction.GuildId,
							pair.Key
						),
						pair.Value
					)
			);
		}

		if(resolved.ResolvedRoles.Resolve(out IDictionary<Int64, DiscordRole>? resolvedRoles))
		{
			await Parallel.ForEachAsync
			(
				resolvedRoles,
				async (pair, _) =>
					await this.cache.CacheObjectAsync
					(
						KeyHelper.GetRoleKey
						(
							pair.Key
						),
						pair.Value
					)
			);
		}
	}

	public async ValueTask ListenAsync
	(
		DiscordMessageCreatedEvent @event
	)
	{
		await this.cache.CacheObjectAsync
		(
			KeyHelper.GetMessageKey
			(
				@event.Data.Id
			),
			@event.Data
		);
	}

	public async ValueTask ListenAsync
	(
		DiscordMessageUpdatedEvent @event
	)
	{
		await this.cache.CacheObjectAsync
		(
			KeyHelper.GetMessageKey
			(
				@event.Data.Id
			),
			@event.Data
		);
	}

	public async ValueTask ListenAsync
	(
		DiscordPresenceUpdatedEvent @event
	)
	{
		await this.cache.CacheObjectAsync
		(
			KeyHelper.GetUserKey
			(
				@event.Data.User.Id
			),
			@event.Data.User
		);
	}
}
