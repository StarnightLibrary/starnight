namespace Starnight;

using System;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

/// <summary>
/// Stores options to be passed to the <see cref="StarnightClient"/>.
/// </summary>
public record StarnightClientOptions
{
	public IServiceCollection? Services { get; init; }

	public ILogger? Logger { get; init; }

	public String Token { get; init; } = null!;
}
