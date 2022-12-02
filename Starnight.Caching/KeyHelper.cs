namespace Starnight.Caching;

using System;

public static class KeyHelper
{
	public static String GetMessageKey
	(
		Int64 messageId
	)
		=> $"StarnightLibraryCache.Message.{messageId}";

	public static String GetOriginalInteractionResponseKey
	(
		String interactionToken
	)
		=> $"StarnightLibraryCache.OriginalResponse.{interactionToken}";

	public static String GetInviteKey
	(
		String inviteCode
	)
		=> $"StarnightLibraryCache.Invite.{inviteCode}";

	public static String GetChannelKey
	(
		Int64 channelId
	)
		=> $"StarnightLibraryCache.Channel.{channelId}";

	public static String GetUserKey
	(
		Int64 userId
	)
		=> $"StarnightLibraryCache.User.{userId}";

	public static String GetStickerKey
	(
		Int64 stickerId
	)
		=> $"StarnightLibraryCache.Sticker.{stickerId}";

	public static String GetGuildMemberKey
	(
		Int64 guildId,
		Int64 userId
	)
		=> $"StarnightLibraryCache.Guild.{guildId}.Member.{userId}";

	public static String GetStageInstanceKey
	(
		Int64 stageId
	)
		=> $"StarnightLibraryCache.StageInstance.{stageId}";

	public static String GetScheduledEventKey
	(
		Int64 eventId
	)
		=> $"StarnightLibraryCache.ScheduledEvent.{eventId}";

	public static String GetGuildTemplateKey
	(
		Int64 templateId
	)
		=> $"StarnightLibraryCache.Template.{templateId}";

	public static String GetGuildPreviewKey
	(
		Int64 previewId
	)
		=> $"StarnightLibraryCache.GuildPreview.{previewId}";

	public static String GetEmojiKey
	(
		Int64 emojiId
	)
		=> $"StarnightLibraryCache.Emoji.{emojiId}";

	public static String GetThreadMemberKey
	(
		Int64 threadId,
		Int64 userId
	)
		=> $"StarnightLibraryCache.Channel.{threadId}.ThreadMember.{userId}";

	public static String GetRoleKey
	(
		Int64 roleId
	)
		=> $"StarnightLibraryCache.Role.{roleId}";
}
