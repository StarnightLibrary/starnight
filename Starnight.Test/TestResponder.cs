namespace Starnight.Test;

using System;
using System.Threading.Tasks;

using Starnight.Internal.Gateway.Events.Inbound;
using Starnight.Internal.Gateway.Responders;

internal class TestResponder : IResponder<DiscordConnectedEvent>
{
	public ValueTask RespondAsync(DiscordConnectedEvent @event)
	{
		Console.WriteLine("received!");

		return ValueTask.CompletedTask;
	}
}
