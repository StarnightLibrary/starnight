namespace Starnight.Internal.Gateway.Payloads;

/// <summary>
/// Represents a payload for opcode 0: dispatch. Note that implementers must set JSON attributes themselves, that is:
/// </summary>
/// <remarks>
/// <inheritdoc path="//remarks"/>
/// - <seealso cref="Data"/> must be annotated as <c>[JsonPropertyName("d")]</c> <br/>
/// </remarks>
public interface IDiscordGatewayPayload<TData> : IDiscordGatewayPayload
{
	/// <summary>
	/// The payload data for this command/event.
	/// </summary>
	public TData Data { get; init; }
}
