namespace Starnight.Internal.Utils;

using Microsoft.Extensions.Logging;

public static class LoggingEvents
{
	public static readonly EventId RestClientRequestDenied = new(1101, "RestClient");
	public static readonly EventId RestClientIncoming = new(1102, "RestClient");
	public static readonly EventId RestClientOutgoing = new(1103, "RestClient");
}
