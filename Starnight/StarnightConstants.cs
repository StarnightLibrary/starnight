namespace Starnight;

using System;

/// <summary>
/// Represents constants shared by the Starnight library not directly related to the discord API.
/// </summary>
public static class StarnightConstants
{
	public static String Version => "0.0.1-dev";
	public static String UserAgentHeader => $"Starnight Library v{Version}";
}
