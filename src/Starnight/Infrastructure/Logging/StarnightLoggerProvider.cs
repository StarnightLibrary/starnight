namespace Starnight.Infrastructure.Logging;

using System;
using System.Collections.Concurrent;

using Microsoft.Extensions.Logging;

[ProviderAlias("Starnight")]
public sealed class StarnightLoggerProvider : ILoggerProvider
{
	private readonly ConcurrentDictionary<String, StarnightLogger> loggers = new(StringComparer.Ordinal);
	private readonly LogLevel minimum;

	public StarnightLoggerProvider
	(
		LogLevel minimum
	)
		=> this.minimum = minimum;

	/// <inheritdoc/>
	public ILogger CreateLogger
	(
		String categoryName
	)
	{
		if(this.loggers.TryGetValue(categoryName, out StarnightLogger? value))
		{
			return value;
		}
		else
		{
			StarnightLogger logger = new(categoryName, this.minimum);

			return this.loggers.AddOrUpdate
			(
				categoryName,
				logger,
				(_, _) => logger
			);
		}
	}

	public void Dispose() { }
}
