namespace Starnight.Internal.Entities.Messages.Embeds;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents an embed footer.
/// </summary>
public record DiscordEmbedFooter
{
	/// <summary>
	/// Footer text.
	/// </summary>
	[JsonPropertyName("text")]
	public String Text { get; init; } = default!;

	/// <summary>
	/// Footer icon url. This only supports <c>http://</c>, <c>https://</c> and <c>attachment://</c>
	/// </summary>
	[JsonPropertyName("icon_url")]
	public String? IconUrl { get; init; }

	/// <summary>
	/// Proxied footer icon url.
	/// </summary>
	[JsonPropertyName("proxy_icon_url")]
	public String? ProxiedIconUrl { get; init; }
}
