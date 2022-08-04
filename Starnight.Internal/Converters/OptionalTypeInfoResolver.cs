namespace Starnight.Internal.Converters;

using System.Linq;
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
					if(property.PropertyType.GetInterfaces().Contains(typeof(IOptional)))
					{
						property.ShouldSerialize = (_, value) => (value as IOptional)?.HasValue ?? false;
					}
				}
			}
		}
	};
}
