namespace Starnight.Internal.Converters;

using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization.Metadata;

using OptionalDelegate = System.Func
<
	System.Object,
	System.Object?,
	System.Boolean
>;

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
					if(property.PropertyType.IsConstructedGenericType &&
						property.PropertyType.GetGenericTypeDefinition() == typeof(Optional<>))
					{
						property.ShouldSerialize = (OptionalDelegate?)typeof(OptionalTypeInfoResolver)
							.GetMethod
							(
								nameof(shouldIgnoreOptional),
								BindingFlags.NonPublic | BindingFlags.Static
							)!
							.MakeGenericMethod
							(
								property.PropertyType.GetGenericArguments().First()!
							)!
							.CreateDelegate(typeof(OptionalDelegate));
					}
				}
			}
		}
	};

	private static Boolean shouldIgnoreOptional<T>(Object _, Object? value) => Unsafe.Unbox<Optional<T>>(value!).HasValue;
}
