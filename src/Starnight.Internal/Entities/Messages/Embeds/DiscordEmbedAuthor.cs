namespace Starnight.Internal.Entities.Messages.Embeds;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a discord embed author.
/// </summary>
public sealed record DiscordEmbedAuthor
{
	/// <summary>
	/// Author name.
	/// </summary>
	[JsonPropertyName("name")]
	public required String Name { get; init; }

	/// <summary>
	/// Author url.
	/// </summary>
	[JsonPropertyName("url")]
	public Optional<String> Url { get; init; }

	/// <summary>
	/// Author icon url. This only supports <c>http://</c>, <c>https://</c> and <c>attachment://</c>
	/// </summary>
	[JsonPropertyName("icon_url")]
	public Optional<String> IconUrl { get; init; }

	/// <summary>
	/// Proxied author icon url.
	/// </summary>
	[JsonPropertyName("proxy_icon_url")]
	public Optional<String> ProxiedIconUrl { get; init; }
}
