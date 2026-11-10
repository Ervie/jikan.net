using Tenrai.Consts;
using Tenrai.Helpers;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Tenrai;

public partial class TenraiClient
{
	/// <inheritdoc />
	public Task<PaginatedTenraiResponse<ICollection<Producer>>> GetProducersAsync(CancellationToken cancellationToken = default)
		=> SearchProducersAsync(new ProducerSearchConfig(), cancellationToken);

	/// <inheritdoc />
	public Task<PaginatedTenraiResponse<ICollection<Producer>>> GetProducersAsync(int page, CancellationToken cancellationToken = default)
	{
		Guard.IsGreaterThanZero(page, nameof(page));
		return SearchProducersAsync(new ProducerSearchConfig { Page = page }, cancellationToken);
	}

	/// <inheritdoc />
	public async Task<PaginatedTenraiResponse<ICollection<Producer>>> SearchProducersAsync(ProducerSearchConfig searchConfig, CancellationToken cancellationToken = default)
	{
		Guard.IsNotNull(searchConfig, nameof(searchConfig));
		var endpointParts = new[] { TenraiEndpointConsts.Producers + QueryFor(searchConfig) };
		return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<Producer>>>(endpointParts, cancellationToken);
	}

	/// <inheritdoc />
	public async Task<BaseTenraiResponse<Producer>> GetProducerAsync(long id, CancellationToken cancellationToken = default)
	{
		Guard.IsGreaterThanZero(id, nameof(id));
		var endpointParts = new[] { TenraiEndpointConsts.Producers, id.ToString() };
		return await ExecuteGetRequestAsync<BaseTenraiResponse<Producer>>(endpointParts, cancellationToken);
	}

	/// <inheritdoc />
	public async Task<BaseTenraiResponse<ICollection<ExternalLink>>> GetProducerExternalLinksAsync(long id, CancellationToken cancellationToken = default)
	{
		Guard.IsGreaterThanZero(id, nameof(id));
		var endpointParts = new[] { TenraiEndpointConsts.Producers, id.ToString(), TenraiEndpointConsts.External };
		return await ExecuteGetRequestAsync<BaseTenraiResponse<ICollection<ExternalLink>>>(endpointParts, cancellationToken);
	}

	/// <inheritdoc />
	public async Task<BaseTenraiResponse<ProducerFull>> GetProducerFullDataAsync(long id, CancellationToken cancellationToken = default)
	{
		Guard.IsGreaterThanZero(id, nameof(id));
		var endpointParts = new[] { TenraiEndpointConsts.Producers, id.ToString(), TenraiEndpointConsts.Full };
		return await ExecuteGetRequestAsync<BaseTenraiResponse<ProducerFull>>(endpointParts, cancellationToken);
	}

	/// <inheritdoc />
	public async Task<IdsTenraiResponse> GetProducerIdsAsync(CancellationToken cancellationToken = default)
	{
		var endpointParts = new[] { TenraiEndpointConsts.Producers, TenraiEndpointConsts.Ids };
		return await ExecuteGetRequestAsync<IdsTenraiResponse>(endpointParts, cancellationToken);
	}
}
