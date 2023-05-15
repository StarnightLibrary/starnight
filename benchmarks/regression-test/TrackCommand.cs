namespace Starnight.Benchmarks.RegressionTests;

using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

using Spectre.Console.Cli;

public sealed class TrackCommand : Command<TrackCommand.Settings>
{
	public sealed class Settings : CommandSettings
	{
		[Description("Specifies whether the full diffs should be listed, as opposed to just the overview.")]
		[CommandOption("-d|--diffs")]
		public Boolean ListFullDiffs { get; init; }
	}

	public override Int32 Execute
	(
		[NotNull]
		CommandContext context,

		[NotNull]
		Settings settings
	)
	{
		return 0;
	}
}
