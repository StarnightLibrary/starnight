namespace Starnight.SourceGenerators.Shims;

using System;

internal class ShimGeneratorHelper
{
	public const String Attribute = """
		// auto-generated code
		namespace Starnight.SourceGenerators.Shims;

		[global::System.AttributeUsage(global::System.AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
		[global::System.CodeDom.Compiler.GeneratedCode("starnight-shim-generator", "0.1.0")]
		internal sealed class ShimAttribute<TInterface> : global::System.Attribute
		{ }
		""";
}
