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
}
