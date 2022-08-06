namespace Starnight.Internal.Rest.Payloads;

using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;

/// <summary>
/// Represents an attachment file stream.
/// </summary>
public record struct DiscordAttachmentFile
{
	/// <inheritdoc/>
	[SetsRequiredMembers]
	public DiscordAttachmentFile(Stream stream, String? filename, String? contentType)
	{
		this.FileStream = stream;
		this.Filename = filename ?? "file";
		this.ContentType = contentType;
	}

	/// <summary>
	/// A stream to the file.
	/// </summary>
	public required Stream FileStream { get; set; }

	/// <summary>
	/// The file name as sent to Discord.
	/// </summary>
	public required String Filename { get; set; }

	/// <summary>
	/// Semi-optional content type for Discord.
	/// </summary>
	public String? ContentType { get; set; }
}
