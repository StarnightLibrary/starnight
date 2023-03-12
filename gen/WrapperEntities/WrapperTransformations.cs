namespace Starnight.SourceGenerators.WrapperEntities;

using System;

internal record WrapperTransformations
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

	public void MoveNext() => this.index++;

	public void Reset() => this.index = 0;

	public WrapperTransformationType Next
		=> this[this.index + 1];

	public WrapperTransformationType DictionaryNext
		=> this[this.index + 2];
}
