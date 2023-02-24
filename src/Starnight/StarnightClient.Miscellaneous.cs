namespace Starnight;

using Starnight.Infrastructure.TransformationServices;

// here, we hold all those members that are too disconnected from anything else to be
// realistically grouped into logical units.
public partial class StarnightClient
{
	/// <summary>
	/// Gets the configured collection transformer for this client.
	/// </summary>
	internal ICollectionTransformerService CollectionTransformer { get; }
}
