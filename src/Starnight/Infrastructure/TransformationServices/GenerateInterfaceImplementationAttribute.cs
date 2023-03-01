namespace Starnight.Infrastructure.TransformationServices;

using System;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
internal class GenerateInterfaceImplementationAttribute : Attribute
{
	public Boolean GenerateInterfaceImplementation { get; set; }

	public GenerateInterfaceImplementationAttribute
	(
		Boolean generateImplementation
	)
		=> this.GenerateInterfaceImplementation = generateImplementation;
}
