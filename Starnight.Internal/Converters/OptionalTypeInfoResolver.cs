namespace Starnight.Internal.Converters;

using System.Runtime.CompilerServices;
using System.Text.Json.Serialization.Metadata;

internal class OptionalTypeInfoResolver
{
	public static IJsonTypeInfoResolver Default { get; } = new DefaultJsonTypeInfoResolver
	{
		Modifiers =
		{
			(JsonTypeInfo type) =>
			{
				foreach(JsonPropertyInfo property in type.Properties)
				{
					if(property.PropertyType.FullName == "Starnight.Optional`1")
					{
						property.ShouldSerialize = (_, value) => Unsafe.As<IOptional>(value)?.HasValue ?? false;
					}
				}
			}
		}
	};
}
