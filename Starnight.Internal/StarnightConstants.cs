namespace Starnight.Internal;

using System;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

using Starnight.Internal.Converters;

/// <summary>
/// Represents constants shared by the Starnight library not directly related to the discord API.
/// </summary>
public static class StarnightConstants
{
	public static String Version => Assembly.GetExecutingAssembly()
		.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?
		.InformationalVersion ?? "0.1.0";

	public static String UserAgentHeader => "Starnight Library";

	public static String LibraryName => "Starnight";

	public static JsonSerializerOptions DefaultSerializerOptions { get; } = new()
	{
		NumberHandling = JsonNumberHandling.AllowReadingFromString,
		WriteIndented = false,
		TypeInfoResolver = OptionalTypeInfoResolver.Default
	};

	static StarnightConstants()
	{
		DefaultSerializerOptions.Converters.Add(new OptionalParameterJsonConverterFactory());
		DefaultSerializerOptions.Converters.Add(new GatewayEventJsonConverter());
	}
}
