namespace Starnight.Extensions.Caching;

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

	public static String GetPermissionOverwriteKey
	(
		Int64 channelId,
		Int64 overwriteId
	)
		=> $"StarnightLibraryCache.Channel.{channelId}.Overwrite.{overwriteId}";

	public static String GetPermissionOverwriteListKey
	(
		Int64 channelId
	)
		=> $"StarnightLibraryCache.Channel.{channelId}.Overwrites";
}
