namespace Starnight.Internal.Rest;

using System;

internal record struct RestClientQueueItem
{
	public IRestRequest Request { get; init; }
	public Guid RequestGuid { get; init; }
}
