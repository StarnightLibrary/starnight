namespace Starnight.Test;

using System;
using System.Threading.Tasks;

using Starnight.Internal.Gateway.Events.Inbound;
using Starnight.Internal.Gateway.Events.Inbound.Dispatch;
using Starnight.Internal.Gateway.Listeners;

internal class TestListener : IListener<DiscordConnectedEvent>, IListener<DiscordGuildCreatedEvent>
{
	public ValueTask RespondAsync(DiscordConnectedEvent @event)
	{
		Console.WriteLine("received!");

		return ValueTask.CompletedTask;
	}

	public ValueTask RespondAsync(DiscordGuildCreatedEvent @event)
	{
		Console.WriteLine("received a guild!!1!");

		return ValueTask.CompletedTask;
	}
}
