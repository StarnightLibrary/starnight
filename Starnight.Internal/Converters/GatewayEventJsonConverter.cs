namespace Starnight.Internal.Converters;

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

using Starnight.Internal.Gateway;
using Starnight.Internal.Gateway.Events.Inbound;
using Starnight.Internal.Gateway.Events.Inbound.Dispatch;
using Starnight.Internal.Gateway.Events.Outbound;

public class GatewayEventJsonConverter : JsonConverter<IDiscordGatewayEvent>
{
	public override IDiscordGatewayEvent? Read
	(
		ref Utf8JsonReader reader,
		Type typeToConvert,
		JsonSerializerOptions options
	)
	{
		JsonDocument document = JsonDocument.ParseValue(ref reader);
		JsonElement gatewayEvent = document.RootElement;

		DiscordGatewayOpcode opcode = (DiscordGatewayOpcode)gatewayEvent.GetProperty("op").GetInt32();

		return opcode switch
		{
			DiscordGatewayOpcode.Dispatch => EventDeserializer.DeserializeEvent
			(
				gatewayEvent,
				gatewayEvent.GetProperty("t").GetString()!
			),
			DiscordGatewayOpcode.Reconnect => JsonSerializer.Deserialize<DiscordReconnectEvent>
			(
				gatewayEvent,
				options
			),
			DiscordGatewayOpcode.InvalidSession => JsonSerializer.Deserialize<DiscordInvalidSessionEvent>
			(
				gatewayEvent,
				options
			),
			DiscordGatewayOpcode.Hello => JsonSerializer.Deserialize<DiscordHelloEvent>
			(
				gatewayEvent,
				options
			),
			DiscordGatewayOpcode.HeartbeatAck => JsonSerializer.Deserialize<DiscordInboundHeartbeatEvent>
			(
				gatewayEvent,
				options
			),
			_ => new DiscordUnknownInboundEvent()
			{
				Event = gatewayEvent,
				Opcode = opcode
			},
		};
	}

	public override void Write
	(
		Utf8JsonWriter writer,
		IDiscordGatewayEvent value,
		JsonSerializerOptions options
	)
	{
		switch(value.Opcode)
		{
			case DiscordGatewayOpcode.Identify:

				JsonSerializer.Serialize
				(
					writer,
					(DiscordIdentifyEvent)value,
					options
				);

				break;

			case DiscordGatewayOpcode.PresenceUpdate:

				JsonSerializer.Serialize
				(
					writer,
					(DiscordUpdatePresenceEvent)value,
					options
				);

				break;

			case DiscordGatewayOpcode.VoiceStateUpdate:

				JsonSerializer.Serialize
				(
					writer,
					(DiscordUpdateVoiceStateEvent)value,
					options
				);

				break;

			case DiscordGatewayOpcode.Resume:

				JsonSerializer.Serialize
				(
					writer,
					(DiscordResumeEvent)value,
					options
				);

				break;

			case DiscordGatewayOpcode.RequestGuildMembers:

				JsonSerializer.Serialize
				(
					writer,
					(DiscordRequestGuildMembersEvent)value,
					options
				);

				break;

			default:

				// serialize whatever the user gave to us, under the assumption that it's valid.
				JsonSerializer.Serialize
				(
					writer,
					(Object)value,
					options
				);

				break;
		}
	}
}
