namespace Starnight.Internal.Gateway.Services;

using System;

using Microsoft.Extensions.Options;

/// <summary>
/// Contains settings for <see cref="IOutboundGatewayService"/>.
/// </summary>
public class OutboundGatewayServiceOptions : IOptions<OutboundGatewayServiceOptions>
{
	public OutboundGatewayServiceOptions Value => this;

	/// <summary>
	/// The amount of times the outbound gateway service should retry if the ratelimit was hit.
	/// </summary>
	public Int32 RetryCount { get; set; } = Int32.MaxValue;
}
