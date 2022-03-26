namespace Starnight.Internal.Rest.Payloads;

using System;
using System.IO;

/// <summary>
/// Represents an attachment file stream.
/// </summary>
public record struct DiscordAttachmentFile
{
	public DiscordAttachmentFile(Stream stream, String? filename, String? contentType)
	{
		this.FileStream = stream;
		this.Filename = filename ?? "file";
		this.ContentType = contentType;
	}

	/// <summary>
	/// A stream to the file.
	/// </summary>
	public Stream FileStream { get; set; }

	/// <summary>
	/// The file name as sent to Discord.
	/// </summary>
	public String Filename { get; set; }

	/// <summary>
	/// Semi-optional content type for Discord.
	/// </summary>
	public String? ContentType { get; set; }
}
