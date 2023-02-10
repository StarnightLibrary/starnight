namespace Starnight.Entities;

/// <summary>
/// An interface for all starnight entities, providing methods to convert to and from internal entities.
/// </summary>
/// <typeparam name="TSelf">The implementing type.</typeparam>
/// <typeparam name="TInternal">The wrapped internal entity.</typeparam>
public interface IStarnightEntity<TSelf, TInternal> : IStarnightEntity
	where TSelf : IStarnightEntity<TSelf, TInternal>
	where TInternal : class
{
	/// <summary>
	/// Extracts the underlying internal entity from this wrapper entity.
	/// </summary>
	public abstract static explicit operator TInternal
	(
		TSelf entity
	);

	/// <summary>
	/// Extracts the underlying internal entity from this wrapper entity.
	/// </summary>
	public abstract TInternal ToInternalEntity();

	/// <summary>
	/// Constructs a wrapper entity from an internal entity using the specified client instance.
	/// </summary>
	public abstract static TSelf FromInternalEntity
	(
		StarnightClient client,
		TInternal entity
	);
}
