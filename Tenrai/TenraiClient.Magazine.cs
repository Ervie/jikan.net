using Tenrai.Consts;
using Tenrai.Helpers;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Tenrai;

public partial class TenraiClient
{
	/// <inheritdoc />
	public Task<PaginatedTenraiResponse<ICollection<Magazine>>> GetMagazinesAsync(CancellationToken cancellationToken = default)
		=> SearchMagazinesAsync(new MagazineSearchConfig(), cancellationToken);

	/// <inheritdoc />
	public Task<PaginatedTenraiResponse<ICollection<Magazine>>> GetMagazinesAsync(int page, CancellationToken cancellationToken = default)
		=> SearchMagazinesAsync(new MagazineSearchConfig { Page = page }, cancellationToken);

	/// <inheritdoc />
	public async Task<PaginatedTenraiResponse<ICollection<Magazine>>> SearchMagazinesAsync(MagazineSearchConfig searchConfig, CancellationToken cancellationToken = default)
	{
		Guard.IsNotNull(searchConfig, nameof(searchConfig));
		var endpointParts = new[] { TenraiEndpointConsts.Magazines + QueryFor(searchConfig) };
		return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<Magazine>>>(endpointParts, cancellationToken);
	}
}
