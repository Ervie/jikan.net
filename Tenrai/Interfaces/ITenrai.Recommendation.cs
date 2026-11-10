using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Tenrai;

public partial interface ITenrai
{
	/// <summary>
	/// Gets collection of recently added anime recommendations.
	/// </summary>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Collection of recently added recommendations.r</returns>
	Task<PaginatedTenraiResponse<ICollection<UserRecommendation>>> GetRecentAnimeRecommendationsAsync(CancellationToken cancellationToken = default);

	/// <summary>
	/// Gets collection of recently added anime recommendations.
	/// </summary>
	/// <param name="searchConfig">Page, page size, username and safe-for-work filters.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Collection of recently added recommendations.r</returns>
	Task<PaginatedTenraiResponse<ICollection<UserRecommendation>>> GetRecentAnimeRecommendationsAsync(RecentRecommendationsConfig searchConfig, CancellationToken cancellationToken = default);

	/// <summary>
	/// Gets collection of recently added anime recommendations.
	/// </summary>
	/// <param name="page">Index of page folding 100 records of top ranging (e.g. 1 will return first 100 records, 2 will return record from 101 to 200 etc.)</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Collection of recently added recommendations.r</returns>
	Task<PaginatedTenraiResponse<ICollection<UserRecommendation>>> GetRecentAnimeRecommendationsAsync(int page, CancellationToken cancellationToken = default);

	/// <summary>
	/// Gets collection of recently added manga recommendations.
	/// </summary>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Collection of recently added recommendations.r</returns>
	Task<PaginatedTenraiResponse<ICollection<UserRecommendation>>> GetRecentMangaRecommendationsAsync(CancellationToken cancellationToken = default);

	/// <summary>
	/// Gets collection of recently added manga recommendations.
	/// </summary>
	/// <param name="searchConfig">Page, page size, username and safe-for-work filters.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Collection of recently added recommendations.r</returns>
	Task<PaginatedTenraiResponse<ICollection<UserRecommendation>>> GetRecentMangaRecommendationsAsync(RecentRecommendationsConfig searchConfig, CancellationToken cancellationToken = default);

	/// <summary>
	/// Gets collection of recently added manga recommendations.
	/// </summary>
	/// <param name="page">Index of page folding 100 records of top ranging (e.g. 1 will return first 100 records, 2 will return record from 101 to 200 etc.)</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Collection of recently added recommendations.r</returns>
	Task<PaginatedTenraiResponse<ICollection<UserRecommendation>>> GetRecentMangaRecommendationsAsync(int page, CancellationToken cancellationToken = default);
}
