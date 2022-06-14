namespace Starnight.Internal.Gateway.Objects.User.Activity;

using System;

/// <summary>
/// Flags holding activity data.
/// </summary>
[Flags]
public enum DiscordActivityFlags
{
	Instance,
	Join,
	Spectate,
	JoinRequest,
	Sync,
	Play,
	PartyPrivacyFriends,
	PartyPrivacyVoiceChannel,
	Embedded
}
