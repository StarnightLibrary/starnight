namespace Starnight.Internal.Gateway.Listeners;

/// <summary>
/// Represents the different phases event listeners run in.
/// </summary>
public enum ListenerPhase
{
	/// <summary>
	/// Listeners registered in this group will run immediately after the scope is created.
	/// </summary>
	/// <remarks>
	/// This phase is intended for globally preparing listener logic, such as opening connections for the listeners.
	/// </remarks>
	PreEvent,

	/// <summary>
	/// Listeners registered in this group will run early in the scope lifecycle.
	/// </summary>
	/// <remarks>
	/// This phase is intended for preliminary, concrete logic, such as handling caches.
	/// </remarks>
	Early,

	/// <summary>
	/// Listeners registered in this group will run midway through the scope lifecycle.
	/// </summary>
	/// <remarks>
	/// This phase is intended for the main listener logic.
	/// </remarks>
	Normal,

	/// <summary>
	/// Listeners registered in this group will run late in the scope lifecycle.
	/// </summary>
	/// <remarks>
	/// This phase is intended for cleaning up the results of previous phases.
	/// </remarks>
	Late,

	/// <summary>
	/// Listeners registered in this group will run immediately before the scope ends.
	/// </summary>
	/// <remarks>
	/// This phase is intended for globally cleaning up, such as closing opened connections.
	/// </remarks>
	PostEvent
}
