namespace Starnight.Internal;

using System;

using Microsoft.Extensions.Options;

/// <summary>
/// Represents a container for the bot token in use.
/// </summary>
public class TokenContainer : IOptions<TokenContainer>
{
	public TokenContainer Value => this;

	public String Token { get; set; } = null!;
}
