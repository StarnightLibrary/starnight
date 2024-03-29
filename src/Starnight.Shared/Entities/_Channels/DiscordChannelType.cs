namespace Starnight.Entities;

/// <summary>
/// Describes the different variants of Discord channels
/// </summary>
public enum DiscordChannelType
{
	/// <summary>
	/// A guild text channel.
	/// </summary>
	GuildText,

	/// <summary>
	/// A direct message channel between two users.
	/// </summary>
	DirectMessage,

	/// <summary>
	/// A guild voice channel.
	/// </summary>
	GuildVoice,

	/// <summary>
	/// A direct message channel between up to 10 users.
	/// </summary>
	GroupDirectMessage,

	/// <summary>
	/// A guild category, holding up to 50 non-category channels.
	/// </summary>
	GuildCategory,

	/// <summary>
	/// A guild news channel - messages published there will be crossposted into subscribing channels in other servers.
	/// </summary>
	GuildNews,

	/// <summary>
	/// A thread channel in a <see cref="GuildNews"/> channel.
	/// </summary>
	GuildNewsThread = 10, // discord. can we count to six?

	/// <summary>
	/// A public thread channel in a <see cref="GuildText"/> channel.
	/// </summary>
	GuildPublicThread,

	/// <summary>
	/// A private thread channel in a <see cref="GuildText"/> channel.
	/// </summary>
	GuildPrivateThread,

	/// <summary>
	/// A stage voice channel.
	/// </summary>
	GuildStageVoice,

	/// <summary>
	/// The channel in a student hub containing listed servers.
	/// </summary>
	GuildDirectory,

	/// <summary>
	/// A channel that can only contain threads.
	/// </summary>
	GuildForum
}
