namespace Starnight.Internal.Gateway;

/// <summary>
/// Represents a barebones gateway payload. Note that implementers must set JSON attributes themselves, that is:
/// </summary>
/// <remarks>
/// - <seealso cref="Opcode"/> must be annotated as <c>[JsonPropertyName("op")]</c> <br/>
/// </remarks>
public interface IDiscordGatewayPayload
{
	/// <summary>
	/// Indicates the opcode of this gateway event or command.
	/// </summary>
	public DiscordGatewayOpcode Opcode { get; init; }
}
