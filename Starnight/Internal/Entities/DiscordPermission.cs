namespace Starnight.Internal.Entities;

/// <summary>
/// Represents discord permissions - role permission, channel overwrites.
/// </summary>
[Flags]
public enum DiscordPermission : Int64
{
	CreateInstantInvite = 1 << 0,
	KickMembers = 1 << 1,
	BanMembers = 1 << 2,
	Administrator = 1 << 3,
	ManageChannels = 1 << 4,
	ManageGuild = 1 << 5,
	AddReactions = 1 << 6,
	ViewAuditLog = 1 << 7,
	PrioritySpeaker = 1 << 8,
	Stream = 1 << 9,
	ViewChannel = 1 << 10,
	SendMessages = 1 << 11,
	SendTTSMessages = 1 << 12,
	ManageMessages = 1 << 13,
	EmbedLinks = 1 << 14,
	AttachFiles = 1 << 15,
	ReadMessageHistory = 1 << 16,
	MentionEveryone = 1 << 17,
	UseExternalEmotes = 1 << 18,
	ViewGuildInsights = 1 << 19,
	Connect = 1 << 20,
	Speak = 1 << 21,
	MuteMembers = 1 << 22,
	DeafenMembers = 1 << 23,
	MoveMembers = 1 << 24,
	UseVoiceActivity = 1 << 25,
	ChangeNickname = 1 << 26,
	ManageNicknames = 1 << 27,
	ManageRoles = 1 << 28,
	ManageWebhooks = 1 << 29,
	ManageEmotesStickers = 1 << 30,
	UseApplicationCommands = 1 << 31,
	RequestToSpeak = (Int64)1 << 32,
	ManageThreads = (Int64)1 << 34, // where 33, discord
	UsePublicThreads = (Int64)1 << 35,
	UsePrivateThreads = (Int64)1 << 36,
	UseExternalStickers = (Int64)1 << 37
}
