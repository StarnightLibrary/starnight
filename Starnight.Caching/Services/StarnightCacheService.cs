namespace Starnight.Caching.Services;

using System;
using System.Threading.Tasks;

using Starnight.Caching.Providers.Abstractions;
using Starnight.Internal.Entities.Channels;
using Starnight.Internal.Entities.Guilds;
using Starnight.Internal.Entities.Messages;
using Starnight.Internal.Entities.Stickers;
using Starnight.Internal.Entities.Users;
using Starnight.Internal.Entities.Voice;
using Starnight.SourceGenerators.Caching;

/// <summary>
/// The default caching service for Starnight.
/// </summary>
public partial class StarnightCacheService : IStarnightCacheService
{
	private readonly ICacheProvider __provider;

	public StarnightCacheService
	(
		ICacheProvider cacheProvider
	)
		=> this.__provider = cacheProvider;

	/// <inheritdoc/>
	public async ValueTask CacheObjectAsync<TItem>
	(
		String cacheKey,
		TItem @object
	)
	{
		await (@object switch
		{
			DiscordChannel channel => this.cacheChannelAsync
			(
				cacheKey,
				channel
			),
			DiscordEmoji emoji => this.cacheEmojiAsync
			(
				cacheKey,
				emoji
			),
			DiscordGuild guild => this.cacheGuildAsync
			(
				cacheKey,
				guild
			),
			DiscordGuildMember member => this.cacheGuildMemberAsync
			(
				cacheKey,
				member
			),
			DiscordGuildPreview preview => this.cacheGuildPreviewAsync
			(
				cacheKey,
				preview
			),
			DiscordMessage message => this.cacheMessageAsync
			(
				cacheKey,
				message
			),
			_ => this.__provider.CacheAsync
			(
				cacheKey,
				@object
			)
		});
	}

	public async ValueTask<TItem?> EvictObjectAsync<TItem>
	(
		String cacheKey
	)
	{
		return await this.__provider.RemoveAsync<TItem>
		(
			cacheKey
		);
	}

	public async ValueTask<TItem?> RetrieveObjectAsync<TItem>
	(
		String cacheKey
	)
	{
		return await this.__provider.GetAsync<TItem>
		(
			cacheKey
		);
	}

	private async ValueTask cacheChannelAsync
	(
		String key,
		DiscordChannel channel
	)
	{
		DiscordChannel? old = await this.__provider.GetAsync<DiscordChannel>
		(
			key
		);

		if(old is null)
		{
			await this.__provider.CacheAsync
			(
				key,
				channel
			);
		}
		else
		{
			await this.__provider.CacheAsync
			(
				key,
				this.channelUpdater
				(
					old!,
					channel
				)
			);
		}

		if(channel.Recipients.IsDefined)
		{
			foreach(DiscordUser user in channel.Recipients.Value)
			{
				String userKey = KeyHelper.GetUserKey
				(
					user.Id
				);

				await this.CacheObjectAsync
				(
					userKey,
					user
				);
			}
		}
	}

	private async ValueTask cacheEmojiAsync
	(
		String key,
		DiscordEmoji emoji
	)
	{
		DiscordEmoji? old = await this.__provider.GetAsync<DiscordEmoji>
		(
			key
		);

		if(old is null)
		{
			await this.__provider.CacheAsync
			(
				key,
				emoji
			);
		}
		else
		{
			await this.__provider.CacheAsync
			(
				key,
				this.emojiUpdater
				(
					old!,
					emoji
				)
			);
		}

		if(emoji.Creator.IsDefined)
		{
			String userKey = KeyHelper.GetUserKey
			(
				emoji.Creator.Value.Id
			);

			await this.CacheObjectAsync
			(
				userKey,
				emoji
			);
		}
	}

	private async ValueTask cacheGuildAsync
	(
		String key,
		DiscordGuild guild
	)
	{
		DiscordGuild? old = await this.__provider.GetAsync<DiscordGuild>
		(
			key
		);

		if(old is null)
		{
			await this.__provider.CacheAsync
			(
				key,
				guild
			);
		}
		else
		{
			await this.__provider.CacheAsync
			(
				key,
				this.guildUpdater
				(
					old!,
					guild
				)
			);
		}

		foreach(DiscordEmoji emoji in guild.Emojis)
		{
			if(emoji.Id is null)
			{
				continue;
			}

			String emojiKey = KeyHelper.GetEmojiKey
			(
				emoji.Id.Value
			);

			await this.CacheObjectAsync
			(
				emojiKey,
				emoji
			);
		}

		if(guild.Stickers.IsDefined)
		{
			foreach(DiscordSticker sticker in guild.Stickers.Value)
			{
				String stickerKey = KeyHelper.GetStickerKey
				(
					sticker.Id
				);

				await this.CacheObjectAsync
				(
					stickerKey,
					sticker
				);
			}
		}

		if(guild.Members.IsDefined)
		{
			foreach(DiscordGuildMember member in guild.Members.Value)
			{
				if(member.User.IsDefined)
				{
					String memberKey = KeyHelper.GetGuildMemberKey
					(
						guild.Id,
						member.User.Value.Id
					);

					await this.CacheObjectAsync
					(
						memberKey,
						member
					);
				}
			}		
		}

		if(guild.VoiceStates.IsDefined)
		{
			foreach(DiscordVoiceState voiceState in guild.VoiceStates.Value)
			{
				if(voiceState.Member.IsDefined && voiceState.Member.Value.User.IsDefined)
				{
					String memberKey = KeyHelper.GetGuildMemberKey
					(
						guild.Id,
						voiceState.Member.Value.User.Value.Id
					);

					await this.CacheObjectAsync
					(
						memberKey,
						voiceState.Member.Value
					);
				}
			}
		}

		if(guild.Channels.IsDefined)
		{
			foreach(DiscordChannel channel in guild.Channels.Value)
			{
				String channelKey = KeyHelper.GetChannelKey
				(
					channel.Id
				);

				await this.CacheObjectAsync
				(
					channelKey,
					channel
				);
			}
		}

		if(guild.Threads.IsDefined)
		{
			foreach(DiscordChannel channel in guild.Threads.Value)
			{
				String channelKey = KeyHelper.GetChannelKey
				(
					channel.Id
				);

				await this.CacheObjectAsync
				(
					channelKey,
					channel
				);
			}
		}
	}

	private async ValueTask cacheGuildMemberAsync
	(
		String key,
		DiscordGuildMember member
	)
	{
		DiscordGuildMember? old = await this.__provider.GetAsync<DiscordGuildMember>
		(
			key
		);

		if(old is null)
		{
			await this.__provider.CacheAsync
			(
				key,
				member
			);
		}
		else
		{
			await this.__provider.CacheAsync
			(
				key,
				this.guildMemberUpdater
				(
					old!,
					member
				)
			);
		}

		if(member.User.IsDefined)
		{
			String userKey = KeyHelper.GetUserKey
			(
				member.User.Value.Id
			);

			await this.CacheObjectAsync
			(
				userKey,
				member.User.Value
			);
		}
	}

	private async ValueTask cacheGuildPreviewAsync
	(
		String key,
		DiscordGuildPreview preview
	)
	{
		DiscordGuildPreview? old = await this.__provider.GetAsync<DiscordGuildPreview>
		(
			key
		);

		if(old is null)
		{
			await this.__provider.CacheAsync
			(
				key,
				preview
			);
		}
		else
		{
			await this.__provider.CacheAsync
			(
				key,
				this.guildPreviewUpdater
				(
					old!,
					preview
				)
			);
		}

		foreach(DiscordEmoji emoji in preview.Emojis)
		{
			if(emoji.Id is null)
			{
				continue;
			}

			String emojiKey = KeyHelper.GetEmojiKey
			(
				emoji.Id.Value
			);

			await this.CacheObjectAsync
			(
				emojiKey,
				emoji
			);
		}

		foreach(DiscordSticker sticker in preview.Stickers)
		{
			String stickerKey = KeyHelper.GetStickerKey
			(
				sticker.Id
			);

			await this.CacheObjectAsync
			(
				stickerKey,
				sticker
			);
		}
	}

	private async ValueTask cacheMessageAsync
	(
		String key,
		DiscordMessage message
	)
	{
		DiscordMessage? old = await this.__provider.GetAsync<DiscordMessage>
		(
			key
		);

		if(old is null)
		{
			await this.__provider.CacheAsync
			(
				key,
				message
			);
		}
		else
		{
			await this.__provider.CacheAsync
			(
				key,
				this.messageUpdater
				(
					old!,
					message
				)
			);
		}

		String authorKey = KeyHelper.GetUserKey
		(
			message.Author.Id
		);

		await this.CacheObjectAsync
		(
			authorKey,
			message.Author
		);

		if(!message.ReferencedMessage.IsDefined)
		{
			return;
		}

		String referenceKey = KeyHelper.GetMessageKey
		(
			message.ReferencedMessage.Value!.Id
		);

		await this.CacheObjectAsync
		(
			referenceKey,
			message.ReferencedMessage.Value!
		);
	}

	// --- source generated item updaters --- //

	[GenerateCacheUpdater]
	private partial DiscordChannel channelUpdater
	(
		DiscordChannel first,
		DiscordChannel second
	);

	[GenerateCacheUpdater]
	private partial DiscordEmoji emojiUpdater
	(
		DiscordEmoji first,
		DiscordEmoji second
	);

	[GenerateCacheUpdater]
	private partial DiscordGuild guildUpdater
	(
		DiscordGuild first,
		DiscordGuild second
	);

	[GenerateCacheUpdater]
	private partial DiscordGuildMember guildMemberUpdater
	(
		DiscordGuildMember first,
		DiscordGuildMember second
	);

	[GenerateCacheUpdater]
	private partial DiscordGuildPreview guildPreviewUpdater
	(
		DiscordGuildPreview first,
		DiscordGuildPreview second
	);

	[GenerateCacheUpdater]
	private partial DiscordMessage messageUpdater
	(
		DiscordMessage first,
		DiscordMessage second
	);
}
