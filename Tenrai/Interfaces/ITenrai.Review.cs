using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Tenrai;

public partial interface ITenrai
{
	/// <summary>
	/// Gets collection of recently added anime reviews.
	/// </summary>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Collection of recently added anime reviews</returns>
	Task<PaginatedTenraiResponse<ICollection<Review>>> GetRecentAnimeReviewsAsync(CancellationToken cancellationToken = default);

	/// <summary>
	/// Gets collection of recently added anime reviews, filtered and sorted using the supplied configuration.
	/// </summary>
	/// <param name="searchConfig">Query configuration (page, limit, sort order, preliminary/spoiler filters, sentiment).</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Collection of recently added anime reviews</returns>
	Task<PaginatedTenraiResponse<ICollection<Review>>> GetRecentAnimeReviewsAsync(RecentReviewsConfig searchConfig, CancellationToken cancellationToken = default);

	/// <summary>
	/// Gets collection of recently added manga reviews.
	/// </summary>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Collection of recently added manga reviews</returns>
	Task<PaginatedTenraiResponse<ICollection<Review>>> GetRecentMangaReviewsAsync(CancellationToken cancellationToken = default);

	/// <summary>
	/// Gets collection of recently added manga reviews, filtered and sorted using the supplied configuration.
	/// </summary>
	/// <param name="searchConfig">Query configuration (page, limit, sort order, preliminary/spoiler filters, sentiment).</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Collection of recently added manga reviews.</returns>
	Task<PaginatedTenraiResponse<ICollection<Review>>> GetRecentMangaReviewsAsync(RecentReviewsConfig searchConfig, CancellationToken cancellationToken = default);
}
