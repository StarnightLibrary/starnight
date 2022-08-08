namespace Starnight.Internal.Entities.Messages;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a message attachment.
/// </summary>
public sealed record DiscordMessageAttachment : DiscordSnowflakeObject
{
	/// <summary>
	/// Filename to be displayed in the client.
	/// </summary>
	[JsonPropertyName("filename")]
	public required String Filename { get; init; }

	/// <summary>
	/// Description of this file to be displayed in the client.
	/// </summary>
	[JsonPropertyName("description")]
	public Optional<String> Description { get; init; }

	/// <summary>
	/// The attachment's media type. 
	/// </summary>
	[JsonPropertyName("content_type")]
	public Optional<String> ContentType { get; init; }

	/// <summary>
	/// The attachments file size in bytes.
	/// </summary>
	[JsonPropertyName("size")]
	public Optional<Int32> Size { get; init; }

	/// <summary>
	/// Source URL of this file.
	/// </summary>
	[JsonPropertyName("url")]
	public required String Url { get; init; }

	/// <summary>
	/// Proxied URL of this file.
	/// </summary>
	[JsonPropertyName("proxy_url")]
	public Optional<String> ProxiedUrl { get; init; }

	/// <summary>
	/// Image height, if this attachment is an image.
	/// </summary>
	[JsonPropertyName("height")]
	public Optional<Int32?> Height { get; init; }

	/// <summary>
	/// Image width, if this attachment is an image.
	/// </summary>
	[JsonPropertyName("width")]
	public Optional<Int32?> Width { get; init; }

	/// <summary>
	/// Whether this attachment is ephemeral (only true if the message is ephemeral too).
	/// </summary>
	[JsonPropertyName("ephemeral")]
	public Optional<Boolean> Ephemeral { get; init; }
}
