namespace Starnight.Infrastructure.Logging;

using System;

using Microsoft.Extensions.Logging;

public sealed class StarnightLogger : ILogger
{
	private readonly String name;
	private readonly LogLevel minimumLogLevel;
	private readonly Object @lock = new();

	public StarnightLogger
	(
		String name,
		LogLevel minimumLogLevel
	)
	{
		this.name = name;
		this.minimumLogLevel = minimumLogLevel;
	}

	public IDisposable? BeginScope<TState>(TState state) where TState : notnull => default;

	public Boolean IsEnabled(LogLevel logLevel)
		=> logLevel >= this.minimumLogLevel && logLevel != LogLevel.None;

	public void Log<TState>
	(
		LogLevel logLevel,
		EventId eventId,
		TState state,
		Exception? exception,
		Func<TState, Exception?, String> formatter
	)
	{
		if(!this.IsEnabled(logLevel))
		{
			return;
		}

		lock(this.@lock)
		{
			if(logLevel == LogLevel.Trace)
			{
				Console.ForegroundColor = ConsoleColor.Gray;
			}

			Console.Write($"[{DateTimeOffset.UtcNow:yyyy-MM-dd HH:mm:ss}] [{this.name}]");

			Console.ForegroundColor = logLevel switch
			{
				LogLevel.Trace => ConsoleColor.Gray,
				LogLevel.Debug => ConsoleColor.Green,
				LogLevel.Information => ConsoleColor.Magenta,
				LogLevel.Warning => ConsoleColor.Yellow,
				LogLevel.Error => ConsoleColor.Red,
				LogLevel.Critical => ConsoleColor.DarkRed,
				_ => throw new ArgumentException("Invalid log level specified.", nameof(logLevel))
			};

			Console.WriteLine
			(
				logLevel switch
				{
					LogLevel.Trace => "[Trace] ",
					LogLevel.Debug => "[Debug] ",
					LogLevel.Information => "[Info]  ",
					LogLevel.Warning => "[Warn]  ",
					LogLevel.Error => "[Error] ",
					LogLevel.Critical => "[Crit]  ",
					_ => "This code path is unreachable."
				}
			);

			Console.ResetColor();

			Console.WriteLine(formatter(state, exception));

			if(exception != null)
			{
				Console.WriteLine($"{exception} : {exception.Message}\n{exception.StackTrace}");
			}
		}
	}
}
