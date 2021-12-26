namespace Starnight.Internal.Entities.Message;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a message attachment.
/// </summary>
public class DiscordMessageAttachment : DiscordSnowflakeObject
{
	/// <summary>
	/// Filename to be displayed in the client.
	/// </summary>
	[JsonPropertyName("filename")]
	public String Filename { get; init; } = default!;

	/// <summary>
	/// Description of this file to be displayed in the client.
	/// </summary>
	[JsonPropertyName("description")]
	public String? Description { get; init; }

	/// <summary>
	/// The attachment's media type. 
	/// </summary>
	[JsonPropertyName("content_type")]
	public String? ContentType { get; init; }

	/// <summary>
	/// The attachments file size in bytes.
	/// </summary>
	[JsonPropertyName("size")]
	public Int32 Size { get; init; }

	/// <summary>
	/// Source URL of this file.
	/// </summary>
	[JsonPropertyName("url")]
	public String Url { get; init; } = default!;

	/// <summary>
	/// Proxied URL of this file.
	/// </summary>
	[JsonPropertyName("proxy_url")]
	public String ProxiedUrl { get; init; } = default!;

	/// <summary>
	/// Image height, if this attachment is an image.
	/// </summary>
	[JsonPropertyName("height")]
	public Int32? Height { get; init; }

	/// <summary>
	/// Image width, if this attachment is an image.
	/// </summary>
	[JsonPropertyName("width")]
	public Int32? Width { get; init; }

	/// <summary>
	/// Whether this attachment is ephemeral (only true if the message is ephemeral too).
	/// </summary>
	[JsonPropertyName("ephemeral")]
	public Boolean? Ephemeral { get; init; }
}
