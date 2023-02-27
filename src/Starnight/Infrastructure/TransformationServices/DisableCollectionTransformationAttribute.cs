namespace Starnight.Infrastructure.TransformationServices;

using System;
using System.Diagnostics.CodeAnalysis;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
internal class DisableCollectionTransformationAttribute : Attribute
{
	public required String CollectionName { get; init; }

	[SetsRequiredMembers]
	public DisableCollectionTransformationAttribute
	(
		String collectionName
	)
		=> this.CollectionName = collectionName;
}
