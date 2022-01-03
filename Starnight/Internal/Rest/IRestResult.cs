namespace Starnight.Internal.Rest;

using System.Net;

/// <summary>
/// Represents a base interface for REST results.
/// </summary>
public interface IRestResult
{
	public HttpStatusCode StatusCode { get; }
}
