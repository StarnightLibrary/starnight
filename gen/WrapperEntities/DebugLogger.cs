namespace Starnight.SourceGenerators.WrapperEntities;

using System;
using System.Text;

internal static class DebugLogger
{
	private static readonly StringBuilder log = new();

	public static String Output => log.ToString();

	public static void AppendLine(String line) => log.AppendLine(line);
}
