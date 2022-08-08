namespace Starnight.Internal.Rest.Resources;

using System.Collections.Generic;
using System.Threading.Tasks;

using Starnight.Internal.Entities.Voice;

/// <summary>
/// Represents a wrapper for all requests to discord's voice rest resource.
/// </summary>
public interface IDiscordVoiceRestResource
{
	/// <summary>
	/// Returns an array of voice region objects.
	/// </summary>
	public ValueTask<IEnumerable<DiscordVoiceRegion>> ListVoiceRegionsAsync();
}
