namespace Starnight.Internal.Gateway.Events;

using System;

/// <summary>
/// Represents a payload for opcode 0: dispatch. Note that implementers must set JSON attributes themselves, that is:
/// </summary>
/// <remarks>
/// <inheritdoc path="//remarks" cref="IDiscordGatewayEvent"/>
/// - <seealso cref="IDiscordGatewayEvent{TData}.Data"/> must be annotated as <c>[JsonPropertyName("d")]</c> <br/>
/// - <seealso cref="Sequence"/> must be annotated as <c>[JsonPropertyName("s")]</c> <br/>
/// - <seealso cref="EventName"/> must be annotated as <c>[JsonPropertyName("t")]</c> <br/>
/// </remarks>
/// <typeparam name="TData">The type of the </typeparam>
public interface IDiscordGatewayDispatchEvent<TData> : IDiscordGatewayEvent<TData>
{
	/// <summary>
	/// The sequence number of this event, used for reconnecting.
	/// </summary>
	public Int32 Sequence { get; set; }

	/// <summary>
	/// The name of this event, used to distinguish events.
	/// </summary>
	public String EventName { get; set; }
}
