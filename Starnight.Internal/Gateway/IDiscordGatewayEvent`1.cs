namespace Starnight.Internal.Gateway;
/// <summary>
/// Represents a payload for any opcode. Note that implementers must set JSON attributes themselves, that is:
/// </summary>
/// <remarks>
/// <inheritdoc path="//remarks"/>
/// - <seealso cref="Data"/> must be annotated as <c>[JsonPropertyName("d")]</c> <br/>
/// </remarks>
public interface IDiscordGatewayEvent<TData> : IDiscordGatewayEvent
{
	/// <summary>
	/// The payload data for this command/event.
	/// </summary>
	public TData Data { get; init; }
}
