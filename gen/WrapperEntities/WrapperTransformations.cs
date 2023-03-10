namespace Starnight.SourceGenerators.WrapperEntities;

using System;
using System.Collections;
using System.Collections.Generic;

internal record struct WrapperTransformations : IEnumerator<WrapperTransformationType>
{
	private readonly WrapperTransformationType[] transformations = new WrapperTransformationType[8];
	private Int32 index = 0;

	public WrapperTransformations()
	{
	}

	public WrapperTransformationType this[Int32 index]
	{
		get => this.transformations[index];
		set => this.transformations[index] = value;
	}

	public WrapperTransformationType Current
	{
		get => this[this.index];
		set => this[this.index] = value;
	}

	Object IEnumerator.Current => this.Current;

	public void Dispose() { }
	public Boolean MoveNext()
	{
		this.index++;

		return this.index <= 7;
	}
	public void Reset() => this.index = 0;
}
