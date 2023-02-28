namespace Starnight.Infrastructure.TransformationServices;

using System;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
internal class RenameTransformedPropertyAttribute : Attribute
{
	public required String InternalName { get; init; }

	public required String WrapperName { get; init; }

	public RenameTransformedPropertyAttribute
	(
		String internalName,
		String wrapperName
	)
	{
		this.InternalName = internalName;
		this.WrapperName = wrapperName;
	}
}
