using Tenrai.Consts;
using Tenrai.Helpers;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Tenrai;

public partial class TenraiClient
{
	/// <inheritdoc />
	public Task<PaginatedTenraiResponse<ICollection<Review>>> GetRecentAnimeReviewsAsync(CancellationToken cancellationToken = default)
		=> GetRecentAnimeReviewsAsync(new RecentReviewsConfig(), cancellationToken);

	/// <inheritdoc />
	public async Task<PaginatedTenraiResponse<ICollection<Review>>> GetRecentAnimeReviewsAsync(RecentReviewsConfig searchConfig, CancellationToken cancellationToken = default)
	{
		Guard.IsNotNull(searchConfig, nameof(searchConfig));
		var endpointParts = new[] { TenraiEndpointConsts.Reviews, TenraiEndpointConsts.Anime + QueryFor(searchConfig) };
		return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<Review>>>(endpointParts, cancellationToken);
	}

	/// <inheritdoc />
	public Task<PaginatedTenraiResponse<ICollection<Review>>> GetRecentMangaReviewsAsync(CancellationToken cancellationToken = default)
		=> GetRecentMangaReviewsAsync(new RecentReviewsConfig(), cancellationToken);

	/// <inheritdoc />
	public async Task<PaginatedTenraiResponse<ICollection<Review>>> GetRecentMangaReviewsAsync(RecentReviewsConfig searchConfig, CancellationToken cancellationToken = default)
	{
		Guard.IsNotNull(searchConfig, nameof(searchConfig));
		var endpointParts = new[] { TenraiEndpointConsts.Reviews, TenraiEndpointConsts.Manga + QueryFor(searchConfig) };
		return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<Review>>>(endpointParts, cancellationToken);
	}
}
