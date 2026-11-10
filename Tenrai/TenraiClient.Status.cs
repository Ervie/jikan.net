using Tenrai.Consts;
using System.Threading;
using System.Threading.Tasks;

namespace Tenrai;

public partial class TenraiClient
{
	/// <inheritdoc />
	public async Task<TenraiStatus> GetStatusAsync(CancellationToken cancellationToken = default)
	{
		// Absolute URL as a single route section, so string.Join is a no-op and BaseAddress is bypassed
		var endpointParts = new[] { TenraiEndpointConsts.StatusEndpoint };
		return await ExecuteGetRequestAsync<TenraiStatus>(endpointParts, cancellationToken, bypassLimiter: true);
	}
}
