namespace Starnight.Internal.Gateway.Deserialization;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text.Json;

using Starnight.Internal.Gateway.Events.Inbound;
using Starnight.Internal.Gateway.Events.Inbound.Dispatch;
using Starnight.Internal.Gateway.Payloads.Inbound.Dispatch;

/// <summary>
/// Contains static deserialization information about dispatch events.
/// </summary>
internal unsafe static class DispatchEvents
{
	// delegate type holding dynamic methods for deserializing the inner payload object
	private delegate Object DeserializePayloadDelegate(JsonElement element, JsonSerializerOptions options);

	// delegate type holding dynamic methods for deserializing the entire event
	private delegate IDiscordGatewayEvent DeserializeEventDelegate(JsonElement element);

	// intermediary delegate type for dynamic methods
	private delegate IDiscordGatewayEvent IntermediaryCreatorDelegate(Int32 sequence, String name, Object payload, DiscordGatewayOpcode opcode);

	private readonly static List<DeserializePayloadDelegate> __payload_delegates;
	private readonly static List<DeserializeEventDelegate> __event_delegates;
	private readonly static List<DynamicMethod> __event_creators;
			
	private readonly static Dictionary<Type, Type> __payload_types;
	private readonly static Dictionary<Type, IntPtr> __payload_delegate_pointers;

	private readonly static MethodInfo __json_deserializer_method;

	static DispatchEvents()
	{
		__payload_delegates = new();
		__event_delegates = new();
		__payload_types = new();
		__payload_delegate_pointers = new();
		__event_creators = new();
		__json_deserializer_method = typeof(JsonSerializer)
			.GetMethods(BindingFlags.Static | BindingFlags.Public)
			.Where(xm => xm.Name == nameof(JsonSerializer.Deserialize))
			.Select(xm => new
			{
				Method = xm,
				Parameters = xm.GetParameters(),
				GenericArguments = xm.GetGenericArguments()
			})
			.Where(xm => xm.GenericArguments.Length == 1 &&
						 xm.Parameters.Length == 2 &&
						 xm.Parameters.First().ParameterType == typeof(JsonElement) &&
						 xm.Parameters.Last().ParameterType == typeof(JsonSerializerOptions))
			.Select(xm => xm.Method)
			.FirstOrDefault()
			?? throw new MissingMethodException($"The method {nameof(JsonSerializer)}#{nameof(JsonSerializer.Deserialize)} went missing.");
	}

	public static Dictionary<String, IntPtr> Events = new()
	{
		["READY"] = CreateDeserializationInfo<DiscordConnectedEvent>(),
		["APPLICATION_COMMAND_PERMISSIONS_UPDATE"] = CreateDeserializationInfo<DiscordApplicationCommandPermissionsUpdatedEvent>(),
		["AUTO_MODERATION_RULE_CREATE"] = CreateDeserializationInfo<DiscordAutoModerationRuleCreatedEvent>(),
		["AUTO_MODERATION_RULE_UPDATE"] = CreateDeserializationInfo<DiscordAutoModerationRuleUpdatedEvent>(),
		["AUTO_MODERATION_RULE_DELETE"] = CreateDeserializationInfo<DiscordAutoModerationRuleDeletedEvent>(),
		["AUTO_MODERATION_ACTION_EXECUTION"] = CreateDeserializationInfo<DiscordAutoModerationActionExecutedEvent>(),
		["CHANNEL_CREATE"] = CreateDeserializationInfo<DiscordChannelCreatedEvent>(),
		["CHANNEL_UPDATE"] = CreateDeserializationInfo<DiscordChannelUpdatedEvent>(),
		["CHANNEL_DELETE"] = CreateDeserializationInfo<DiscordChannelDeletedEvent>(),
		["CHANNEL_PINS_UPDATE"] = CreateDeserializationInfo<DiscordChannelPinsUpdatedEvent>(),
		["THREAD_CREATE"] = CreateDeserializationInfo<DiscordThreadCreatedEvent>(),
		["THREAD_UPDATE"] = CreateDeserializationInfo<DiscordThreadUpdatedEvent>(),
		["THREAD_DELETE"] = CreateDeserializationInfo<DiscordThreadDeletedEvent>(),
		["THREAD_LIST_SYNC"] = CreateDeserializationInfo<DiscordThreadListSyncEvent>(),
		["THREAD_MEMBER_UPDATE"] = CreateDeserializationInfo<DiscordThreadMemberUpdatedEvent>(),
		["THREAD_MEMBERS_UPDATE"] = CreateDeserializationInfo<DiscordThreadMembersUpdatedEvent>(),
		["GUILD_CREATE"] = CreateDeserializationInfo<DiscordGuildCreatedEvent>(),
		["GUILD_UPDATE"] = CreateDeserializationInfo<DiscordGuildUpdatedEvent>(),
		["GUILD_DELETE"] = CreateDeserializationInfo<DiscordGuildDeletedEvent>(),
		["GUILD_BAN_ADD"] = CreateDeserializationInfo<DiscordGuildBanAddedEvent>(),
		["GUILD_BAN_REMOVE"] = CreateDeserializationInfo<DiscordGuildBanRemovedEvent>(),
		["GUILD_EMOJIS_UPDATE"] = CreateDeserializationInfo<DiscordGuildEmojisUpdatedEvent>(),
		["GUILD_STICKERS_UPDATE"] = CreateDeserializationInfo<DiscordGuildStickersUpdatedEvent>(),
		["GUILD_INTEGRATIONS_UPDATE"] = CreateDeserializationInfo<DiscordGuildIntegrationsUpdatedEvent>(),
		["GUILD_MEMBER_ADD"] = CreateDeserializationInfo<DiscordGuildMemberAddedEvent>(),
		["GUILD_MEMBER_REMOVE"] = CreateDeserializationInfo<DiscordGuildMemberRemovedEvent>(),
		["GUILD_MEMBER_UPDATE"] = CreateDeserializationInfo<DiscordGuildMemberUpdatedEvent>(),
		["GUILD_MEMBERS_CHUNK"] = CreateDeserializationInfo<DiscordGuildMembersChunkEvent>(),
		["GUILD_ROLE_CREATE"] = CreateDeserializationInfo<DiscordGuildRoleCreatedEvent>(),
		["GUILD_ROLE_UPDATE"] = CreateDeserializationInfo<DiscordGuildRoleUpdatedEvent>(),
		["GUILD_ROLE_DELETE"] = CreateDeserializationInfo<DiscordGuildRoleDeletedEvent>(),
		["GUILD_SCHEDULED_EVENT_CREATE"] = CreateDeserializationInfo<DiscordScheduledEventCreatedEvent>(),
		["GUILD_SCHEDULED_EVENT_UPDATE"] = CreateDeserializationInfo<DiscordScheduledEventUpdatedEvent>(),
		["GUILD_SCHEDULED_EVENT_DELETE"] = CreateDeserializationInfo<DiscordScheduledEventDeletedEvent>(),
		["GUILD_SCHEDULED_EVENT_USER_ADD"] = CreateDeserializationInfo<DiscordScheduledEventUserAddedEvent>(),
		["GUILD_SCHEDULED_EVENT_USER_REMOVE"] = CreateDeserializationInfo<DiscordScheduledEventUserRemovedEvent>(),
		["INTEGRATION_CREATE"] = CreateDeserializationInfo<DiscordIntegrationCreatedEvent>(),
		["INTEGRATION_UPDATE"] = CreateDeserializationInfo<DiscordIntegrationUpdatedEvent>(),
		["INTEGRATION_DELETE"] = CreateDeserializationInfo<DiscordIntegrationDeletedEvent>(),
		["INTERACTION_CREATE"] = CreateDeserializationInfo<DiscordInteractionCreatedEvent>(),
		["INVITE_CREATE"] = CreateDeserializationInfo<DiscordInviteCreatedEvent>(),
		["INVITE_DELETE"] = CreateDeserializationInfo<DiscordInviteDeletedEvent>(),
		["MESSAGE_CREATE"] = CreateDeserializationInfo<DiscordMessageCreatedEvent>(),
		["MESSAGE_UPDATE"] = CreateDeserializationInfo<DiscordMessageUpdatedEvent>(),
		["MESSAGE_DELETE"] = CreateDeserializationInfo<DiscordMessageDeletedEvent>(),
		["MESSAGE_DELETE_BULK"] = CreateDeserializationInfo<DiscordMessagesBulkDeletedEvent>(),
		["MESSAGE_REACTION_ADD"] = CreateDeserializationInfo<DiscordMessageReactionAddedEvent>(),
		["MESSAGE_REACTION_REMOVE"] = CreateDeserializationInfo<DiscordMessageReactionRemovedEvent>(),
		["MESSAGE_REACTION_REMOVE_ALL"] = CreateDeserializationInfo<DiscordAllMessageReactionsRemovedEvent>(),
		["MESSAGE_REACTION_REMOVE_EMOJI"] = CreateDeserializationInfo<DiscordEmojiMessageReactionsRemovedEvent>(),
		["PRESENCE_UPDATE"] = CreateDeserializationInfo<DiscordPresenceUpdatedEvent>(),
		["STAGE_INSTANCE_CREATE"] = CreateDeserializationInfo<DiscordStageInstanceCreatedEvent>(),
		["STAGE_INSTANCE_DELETE"] = CreateDeserializationInfo<DiscordStageInstanceDeletedEvent>(),
		["STAGE_INSTANCE_UPDATE"] = CreateDeserializationInfo<DiscordStageInstanceUpdatedEvent>(),
		["TYPING_START"] = CreateDeserializationInfo<DiscordTypingStartedEvent>(),
		["USER_UPDATE"] = CreateDeserializationInfo<DiscordUserUpdatedEvent>(),
		["VOICE_STATE_UPDATE"] = CreateDeserializationInfo<DiscordVoiceStateUpdatedEvent>(),
		["VOICE_SERVER_UPDATE"] = CreateDeserializationInfo<DiscordVoiceServerUpdatedEvent>(),
		["WEBHOOKS_UPDATE"] = CreateDeserializationInfo<DiscordWebhooksUpdatedEvent>()
	};

	public static IntPtr CreateDeserializationInfo<TGatewayEvent>()
		where TGatewayEvent : class, IDiscordGatewayEvent
	{
		Type payloadType;

		// establish of which type the given payload is
		if(!__payload_types.ContainsKey(typeof(TGatewayEvent)))
		{
			payloadType = typeof(TGatewayEvent).GetInterfaces()
				.Where(xm => xm.FullName == "Starnight.Internal.Gateway.IDiscordGatewayDispatchEvent`1")
				.FirstOrDefault() ?? throw new ArgumentException("The specified type is not a dispatch event.");

			__payload_types.Add(typeof(TGatewayEvent), payloadType);
		}
		else
		{
			payloadType = __payload_types[typeof(TGatewayEvent)];
		}

		// get the deserializing function pointer for the given payload
		if(!__payload_delegate_pointers.ContainsKey(payloadType))
		{
			DeserializePayloadDelegate payloadDelegate = createPayloadDeserializationDelegate(payloadType);

			// keep the delegate alive
			__payload_delegates.Add(payloadDelegate);

			// the type in operation here is delegate* managed<JsonElement, JsonSerializerOptions, Object>
			__payload_delegate_pointers.Add(payloadType, Marshal.GetFunctionPointerForDelegate(payloadDelegate));
		}

		DeserializeEventDelegate eventDelegate = createEventDeserializationDelegate(typeof(TGatewayEvent), payloadType);
		__event_delegates.Add(eventDelegate);

		return Marshal.GetFunctionPointerForDelegate(eventDelegate);
	}

	public static IDiscordGatewayEvent Dispatch(String eventName, JsonElement element)
	{
		IntPtr ptr = Events[eventName];

		delegate* managed<JsonElement, IDiscordGatewayEvent> function =
			(delegate* managed<JsonElement, IDiscordGatewayEvent>)(void*)ptr;

		return function(element);
	}

	private static DeserializePayloadDelegate createPayloadDeserializationDelegate(Type payloadType)
	{
		MethodInfo deserializer = __json_deserializer_method.MakeGenericMethod(payloadType);

		// create parameters
		ParameterExpression element = Expression.Parameter(typeof(JsonElement), "element");
		ParameterExpression options = Expression.Parameter(typeof(JsonSerializerOptions), "options");

		// call
		MethodCallExpression call = Expression.Call(deserializer, element, options);

		// compile
		return Expression.Lambda<DeserializePayloadDelegate>(call).Compile();
	}

	private static DeserializeEventDelegate createEventDeserializationDelegate(Type eventType, Type payloadType)
	{
		IntPtr payloadPtr = __payload_delegate_pointers[payloadType];

		delegate* managed<JsonElement, JsonSerializerOptions, Object> payloadFunction =
			(delegate* managed<JsonElement, JsonSerializerOptions, Object>)(void*)payloadPtr;

		DynamicMethod creator = createDynamicEventCreator(eventType, payloadType);

		__event_creators.Add(creator);

		delegate* managed<Int32, String, Object, DiscordGatewayOpcode, IDiscordGatewayEvent> creatorFunction =
			(delegate* managed<Int32, String, Object, DiscordGatewayOpcode, IDiscordGatewayEvent>)(void*)
				Marshal.GetFunctionPointerForDelegate(creator.CreateDelegate<IntermediaryCreatorDelegate>());

		return (element) =>
		{
			DiscordGatewayOpcode opcode = DiscordGatewayOpcode.Dispatch;

			String name = element.GetProperty("t").GetString()!;

			Int32 sequence = element.GetProperty("s").GetInt32()!;

			Object payload = payloadFunction(element.GetProperty("d"), StarnightConstants.DefaultSerializerOptions);

			return creatorFunction(sequence, name, payload, opcode);
		};
	}

	private static IDiscordGatewayEvent ilSample(Int32 sequence, String eventName, Object payload,
		DiscordGatewayOpcode opcode)
	{
		return new DiscordAllMessageReactionsRemovedEvent
		{
			Sequence = sequence,
			EventName = eventName,
			Data = (AllMessageReactionsRemovedPayload)payload,
			Opcode = opcode
		};
	}

	private static DynamicMethod createDynamicEventCreator(Type eventType, Type payloadType)
	{
		DynamicMethod method = new
		(
			$"create{eventType}",
			typeof(IDiscordGatewayEvent),
			new[]
			{
				typeof(Int32),
				typeof(String),
				typeof(Object),
				typeof(DiscordGatewayOpcode)
			}
		);

		ILGenerator il = method.GetILGenerator();

		MethodInfo sequenceSetter = eventType.GetProperty("EventType")!.GetSetMethod()!;
		MethodInfo nameSetter = eventType.GetProperty("EventName")!.GetSetMethod()!;
		MethodInfo payloadSetter = eventType.GetProperty("Data")!.GetSetMethod()!;
		MethodInfo opcodeSetter = eventType.GetProperty("Opcode")!.GetSetMethod()!;

		ConstructorInfo ctor = eventType.GetConstructor(Type.EmptyTypes)!;

		// construct the event object
		il.Emit(OpCodes.Newobj, ctor);
		il.Emit(OpCodes.Dup);

		// assign sequence number
		il.Emit(OpCodes.Ldarg_0);
		il.Emit(OpCodes.Call, sequenceSetter);
		il.Emit(OpCodes.Nop);
		il.Emit(OpCodes.Dup);

		// assign name
		il.Emit(OpCodes.Ldarg_1);
		il.Emit(OpCodes.Call, nameSetter);
		il.Emit(OpCodes.Nop);
		il.Emit(OpCodes.Dup);

		// cast and assign payload
		il.Emit(OpCodes.Ldarg_2);
		il.Emit(OpCodes.Castclass, payloadType);
		il.Emit(OpCodes.Call, payloadSetter);
		il.Emit(OpCodes.Nop);
		il.Emit(OpCodes.Dup);

		// assign opcode
		il.Emit(OpCodes.Ldarg_3);
		il.Emit(OpCodes.Call, opcodeSetter);
		il.Emit(OpCodes.Nop);

		// cleanup and return
		il.Emit(OpCodes.Stloc_0);
		il.Emit(OpCodes.Ldloc_0);
		il.Emit(OpCodes.Ret);

		return method;
	}
}
