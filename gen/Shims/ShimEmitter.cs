namespace Starnight.SourceGenerators.Shims;

using System;
using System.Linq;
using System.Text;

using Microsoft.CodeAnalysis;

internal static class ShimEmitter
{
	internal static String Emit
	(
		INamedTypeSymbol shimSymbol
	)
	{
		StringBuilder builder = new();

		_ = builder.Append($$"""
// auto-generated code

#nullable enable

namespace {{shimSymbol.ContainingNamespace.GetFullNamespace()}};

partial class {{shimSymbol.Name}}
{

""");

		foreach
		(
			IMethodSymbol? method in shimSymbol.GetMembers()
				.Where
				(
					m => m is IMethodSymbol
					{
						IsPartialDefinition: true
					}
				)
				.Cast<IMethodSymbol>()
		)
		{
			if(method is null)
			{
				break;
			}

			_ = builder.Append($$"""
	/// <inheritdoc/>
	public partial {{method.ReturnType.ToDisplayString()}} {{method.Name}}
	(

""");

			for(Int32 i = 0; i < method.Parameters.Length; i++)
			{
				_ = builder.Append($$"""
		{{(method.Parameters[i].Type as INamedTypeSymbol)!.ToDisplayString()}} {{method.Parameters[i].Name}}
""");

				_ = i != (method.Parameters.Length - 1)
					? builder.Append(",\n")
					: builder.Append("\n");
			}

			_ = builder.Append($$"""
	)
	{
		return this.underlying.{{method.Name}}
		(

""");
			for(Int32 i = 0; i < method.Parameters.Length; i++)
			{
				_ = builder.Append($$"""
			{{method.Parameters[i].Name}}
""");

				_ = i != (method.Parameters.Length - 1)
					? builder.Append(",\n")
					: builder.Append("\n");
			}

			_ = builder.Append("""
		);
	}


""");
		}

		_ = builder.Append("""
}
""");

		return builder.ToString();
	}
}
