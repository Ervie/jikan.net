using Tenrai.Consts;
using Tenrai.Helpers;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Tenrai;

public partial class TenraiClient
{
	/// <inheritdoc />
	public Task<PaginatedTenraiResponse<ICollection<InterestStack>>> GetInterestStacksAsync(CancellationToken cancellationToken = default)
		=> SearchInterestStacksAsync(new InterestStackSearchConfig(), cancellationToken);

	/// <inheritdoc />
	public Task<PaginatedTenraiResponse<ICollection<InterestStack>>> GetInterestStacksAsync(int page, CancellationToken cancellationToken = default)
	{
		Guard.IsGreaterThanZero(page, nameof(page));
		return SearchInterestStacksAsync(new InterestStackSearchConfig { Page = page }, cancellationToken);
	}

	/// <inheritdoc />
	public async Task<PaginatedTenraiResponse<ICollection<InterestStack>>> SearchInterestStacksAsync(InterestStackSearchConfig searchConfig, CancellationToken cancellationToken = default)
	{
		Guard.IsNotNull(searchConfig, nameof(searchConfig));
		var endpointParts = new[] { TenraiEndpointConsts.Stacks + QueryFor(searchConfig) };
		return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<InterestStack>>>(endpointParts, cancellationToken);
	}

	/// <inheritdoc />
	public Task<BaseTenraiResponse<InterestStackDetails>> GetInterestStackAsync(long id, CancellationToken cancellationToken = default)
		=> GetInterestStackAsync(id, new SfwConfig(), cancellationToken);

	/// <inheritdoc />
	public async Task<BaseTenraiResponse<InterestStackDetails>> GetInterestStackAsync(long id, SfwConfig searchConfig, CancellationToken cancellationToken = default)
	{
		Guard.IsGreaterThanZero(id, nameof(id));
		Guard.IsNotNull(searchConfig, nameof(searchConfig));
		var endpointParts = new[] { TenraiEndpointConsts.Stacks, id.ToString() + QueryFor(searchConfig) };
		return await ExecuteGetRequestAsync<BaseTenraiResponse<InterestStackDetails>>(endpointParts, cancellationToken);
	}

	/// <inheritdoc />
	public Task<PaginatedTenraiResponse<ICollection<InterestStack>>> GetAnimeInterestStacksAsync(long id, CancellationToken cancellationToken = default)
		=> GetAnimeInterestStacksAsync(id, new InterestStackListConfig(), cancellationToken);

	/// <inheritdoc />
	public async Task<PaginatedTenraiResponse<ICollection<InterestStack>>> GetAnimeInterestStacksAsync(long id, InterestStackListConfig searchConfig, CancellationToken cancellationToken = default)
	{
		Guard.IsGreaterThanZero(id, nameof(id));
		Guard.IsNotNull(searchConfig, nameof(searchConfig));
		var endpointParts = new[] { TenraiEndpointConsts.Anime, id.ToString(), TenraiEndpointConsts.Stacks + QueryFor(searchConfig) };
		return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<InterestStack>>>(endpointParts, cancellationToken);
	}

	/// <inheritdoc />
	public Task<PaginatedTenraiResponse<ICollection<InterestStack>>> GetAnimeInterestStacksAsync(long id, int page, CancellationToken cancellationToken = default)
		=> GetAnimeInterestStacksAsync(id, new InterestStackListConfig { Page = page }, cancellationToken);

	/// <inheritdoc />
	public Task<PaginatedTenraiResponse<ICollection<InterestStack>>> GetMangaInterestStacksAsync(long id, CancellationToken cancellationToken = default)
		=> GetMangaInterestStacksAsync(id, new InterestStackListConfig(), cancellationToken);

	/// <inheritdoc />
	public async Task<PaginatedTenraiResponse<ICollection<InterestStack>>> GetMangaInterestStacksAsync(long id, InterestStackListConfig searchConfig, CancellationToken cancellationToken = default)
	{
		Guard.IsGreaterThanZero(id, nameof(id));
		Guard.IsNotNull(searchConfig, nameof(searchConfig));
		var endpointParts = new[] { TenraiEndpointConsts.Manga, id.ToString(), TenraiEndpointConsts.Stacks + QueryFor(searchConfig) };
		return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<InterestStack>>>(endpointParts, cancellationToken);
	}

	/// <inheritdoc />
	public Task<PaginatedTenraiResponse<ICollection<InterestStack>>> GetMangaInterestStacksAsync(long id, int page, CancellationToken cancellationToken = default)
		=> GetMangaInterestStacksAsync(id, new InterestStackListConfig { Page = page }, cancellationToken);
}
