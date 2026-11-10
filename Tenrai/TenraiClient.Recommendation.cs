using Tenrai.Consts;
using Tenrai.Helpers;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Tenrai;

public partial class TenraiClient
{
	/// <inheritdoc />
	public Task<PaginatedTenraiResponse<ICollection<UserRecommendation>>> GetRecentAnimeRecommendationsAsync(CancellationToken cancellationToken = default)
		=> GetRecentAnimeRecommendationsAsync(new RecentRecommendationsConfig(), cancellationToken);

	/// <inheritdoc />
	public async Task<PaginatedTenraiResponse<ICollection<UserRecommendation>>> GetRecentAnimeRecommendationsAsync(RecentRecommendationsConfig searchConfig, CancellationToken cancellationToken = default)
	{
		Guard.IsNotNull(searchConfig, nameof(searchConfig));
		var endpointParts = new[] { TenraiEndpointConsts.Recommendations, TenraiEndpointConsts.Anime + QueryFor(searchConfig) };
		return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<UserRecommendation>>>(endpointParts, cancellationToken);
	}

	/// <inheritdoc />
	public Task<PaginatedTenraiResponse<ICollection<UserRecommendation>>> GetRecentAnimeRecommendationsAsync(int page, CancellationToken cancellationToken = default)
		=> GetRecentAnimeRecommendationsAsync(new RecentRecommendationsConfig { Page = page }, cancellationToken);

	/// <inheritdoc />
	public Task<PaginatedTenraiResponse<ICollection<UserRecommendation>>> GetRecentMangaRecommendationsAsync(CancellationToken cancellationToken = default)
		=> GetRecentMangaRecommendationsAsync(new RecentRecommendationsConfig(), cancellationToken);

	/// <inheritdoc />
	public async Task<PaginatedTenraiResponse<ICollection<UserRecommendation>>> GetRecentMangaRecommendationsAsync(RecentRecommendationsConfig searchConfig, CancellationToken cancellationToken = default)
	{
		Guard.IsNotNull(searchConfig, nameof(searchConfig));
		var endpointParts = new[] { TenraiEndpointConsts.Recommendations, TenraiEndpointConsts.Manga + QueryFor(searchConfig) };
		return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<UserRecommendation>>>(endpointParts, cancellationToken);
	}

	/// <inheritdoc />
	public Task<PaginatedTenraiResponse<ICollection<UserRecommendation>>> GetRecentMangaRecommendationsAsync(int page, CancellationToken cancellationToken = default)
		=> GetRecentMangaRecommendationsAsync(new RecentRecommendationsConfig { Page = page }, cancellationToken);
}
