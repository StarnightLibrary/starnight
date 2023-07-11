namespace Starnight.Benchmarks.RegressionTests;

using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

using Spectre.Console.Cli;

public sealed class CICommand : Command<CICommand.Settings>
{
	public sealed class Settings : CommandSettings
	{
		[Description("The regression threshold, in percentage points from base, at which to consider the benchmark failed.")]
		[CommandOption("-t|--threshold")]
		public Single RegressionThreshold { get; init; }

		[Description("Specifies whether to log the individual worst regressions and best improvements.")]
		[CommandOption("-d|--diffs")]
		public Boolean LogDiffs { get; init; }
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
