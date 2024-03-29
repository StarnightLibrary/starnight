namespace Starnight.Internal.Models.MessageComponents;

using System;
using System.Collections.Generic;

using Starnight.Entities;

/// <summary>
/// Represents a channel select menu, with the selectable options provided by the discord.
/// </summary>
public sealed record ChannelSelectComponent : AbstractInteractiveComponent
{
	public ChannelSelectComponent() => this.Type = DiscordMessageComponentType.ChannelSelect;

	/// <summary>
	/// The identifier of this select menu, passed to the developer on INTERACTION_CREATE.
	/// </summary>
	public required String CustomId { get; init; }

	/// <summary>
	/// The channel types to pre-filter the channel list by when providing options.
	/// </summary>
	public Optional<IReadOnlyList<DiscordChannelType>> ChannelTypes { get; init; }

	/// <summary>
	/// Placeholder text if no option is selected.
	/// </summary>
	public Optional<String> Placeholder { get; init; }

	/// <summary>
	/// The minimum number of items that must be chosen, between 0 and 25.
	/// </summary>
	public Optional<Int32> MinValues { get; init; }

	/// <summary>
	/// The maximum number of items that may be chosen, between 1 and 25.
	/// </summary>
	public Optional<Int32> MaxValues { get; init; }

	/// <summary>
	/// Indicates whether interacting with this select menu is disabled.
	/// </summary>
	public Optional<Boolean> Disabled { get; init; }
}
