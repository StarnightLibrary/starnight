namespace Starnight;

using System;

// internal interface aiding serialization
internal interface IOptional
{
	public Boolean HasValue { get; set; }
}
