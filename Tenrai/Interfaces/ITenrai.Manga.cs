using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tenrai.Consts;

namespace Tenrai;

public partial interface ITenrai
{
	/// <summary>
	/// Returns manga with given MAL id.
	/// </summary>
	/// <param name="id">MAL id of manga.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Manga with given MAL id.</returns>
	Task<BaseTenraiResponse<Manga>> GetMangaAsync(long id, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns collections of characters appearing in manga with given MAL id.
	/// </summary>
	/// <param name="id">MAL id of manga.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Collections of characters appearing in manga with given MAL id.</returns>
	Task<BaseTenraiResponse<ICollection<MangaCharacter>>> GetMangaCharactersAsync(long id, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns collections of news related to manga with given MAL id.
	/// </summary>
	/// <param name="id">MAL id of manga.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Collections of news related to manga with given MAL id.</returns>
	Task<PaginatedTenraiResponse<ICollection<News>>> GetMangaNewsAsync(long id, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns collections of news related to manga with given MAL id.
	/// </summary>
	/// <param name="id">MAL id of manga.</param>
	/// <param name="page">Index of page folding 100 records of top ranging (e.g. 1 will return first 100 records, 2 will return record from 101 to 200 etc.)</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Collections of news related to manga with given MAL id.</returns>
	Task<PaginatedTenraiResponse<ICollection<News>>> GetMangaNewsAsync(long id, int page, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns news related to manga with given MAL id, filtered and sorted.
	/// </summary>
	/// <param name="id">MAL id of manga.</param>
	/// <param name="filterConfig">Author, publish dates, sort order, paging and sfw filtering.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Page of news related to the manga.</returns>
	Task<PaginatedTenraiResponse<ICollection<News>>> GetMangaNewsAsync(long id, NewsFilterConfig filterConfig, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns the first page of articles about manga with given MAL id.
	/// </summary>
	/// <param name="id">MAL id of manga.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Page of articles about the manga.</returns>
	Task<PaginatedTenraiResponse<ICollection<Article>>> GetMangaArticlesAsync(long id, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns articles about manga with given MAL id, filtered and sorted.
	/// </summary>
	/// <param name="id">MAL id of manga.</param>
	/// <param name="filterConfig">Author, publish dates, sort order, paging and sfw filtering.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Page of articles about the manga.</returns>
	Task<PaginatedTenraiResponse<ICollection<Article>>> GetMangaArticlesAsync(long id, ArticleFilterConfig filterConfig, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns the first page of chapter discussion threads for manga with given MAL id. Other forum topics are not available.
	/// </summary>
	/// <param name="id">MAL id of manga.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Page of chapter discussion threads, 30 per page.</returns>
	Task<PaginatedTenraiResponse<ICollection<ForumTopic>>> GetMangaForumTopicsAsync(long id, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns a page of chapter discussion threads for manga with given MAL id, sorted as requested.
	/// </summary>
	/// <param name="id">MAL id of manga.</param>
	/// <param name="searchConfig">Page and sort order.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Page of chapter discussion threads, 30 per page.</returns>
	Task<PaginatedTenraiResponse<ICollection<ForumTopic>>> GetMangaForumTopicsAsync(long id, MangaForumSearchConfig searchConfig, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns collections of links to pictures related to manga with given MAL id.
	/// </summary>
	/// <param name="id">MAL id of manga.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Collections of links to pictures related to manga with given MAL id.</returns>
	Task<BaseTenraiResponse<ICollection<ImagesSet>>> GetMangaPicturesAsync(long id, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns statistics related to manga with given MAL id.
	/// </summary>
	/// <param name="id">MAL id of manga.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Statistics related to manga with given MAL id.</returns>
	Task<BaseTenraiResponse<MangaStatistics>> GetMangaStatisticsAsync(long id, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns additional information related to manga with given MAL id.
	/// </summary>
	/// <param name="id">MAL id of manga.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Collections of forum topics related to manga with given MAL id.</returns>
	Task<BaseTenraiResponse<MoreInfo>> GetMangaMoreInfoAsync(long id, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns collection of manga user updates.
	/// </summary>
	/// <param name="id">MAL id of manga.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Collection of manga user updates.</returns>
	[Obsolete(ErrorMessagesConst.UnsupportedUserUpdates)]
	Task<PaginatedTenraiResponse<ICollection<MangaUserUpdate>>> GetMangaUserUpdatesAsync(long id, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns collection of manga user updates.
	/// </summary>
	/// <param name="id">MAL id of manga.</param>
	/// <param name="page">Index of page folding 75 records of top ranging (e.g. 1 will return first 75 records, 2 will return record from 76 to 150 etc.)</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Collection of manga user updates.</returns>
	[Obsolete(ErrorMessagesConst.UnsupportedUserUpdates)]
	Task<PaginatedTenraiResponse<ICollection<MangaUserUpdate>>> GetMangaUserUpdatesAsync(long id, int page, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns collection of manga recommendation.
	/// </summary>
	/// <param name="id">MAL id of manga.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Collection of manga recomendation.</returns>
	Task<BaseTenraiResponse<ICollection<Recommendation>>> GetMangaRecommendationsAsync(long id, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns collection of manga recommendation.
	/// </summary>
	/// <param name="id">MAL id of manga.</param>
	/// <param name="searchConfig">Username and safe-for-work filters.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Collection of manga recomendation.</returns>
	Task<BaseTenraiResponse<ICollection<Recommendation>>> GetMangaRecommendationsAsync(long id, RecommendationsConfig searchConfig, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns collection of manga reviews.
	/// </summary>
	/// <param name="id">MAL id of manga.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Collection of manga reviews.</returns>
	Task<PaginatedTenraiResponse<ICollection<Review>>> GetMangaReviewsAsync(long id, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns collection of manga reviews, filtered and sorted using the supplied configuration.
	/// </summary>
	/// <param name="id">MAL id of manga.</param>
	/// <param name="searchConfig">Query configuration (page, sort order, preliminary/spoiler filters, sentiment).</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Collection of manga reviews.</returns>
	Task<PaginatedTenraiResponse<ICollection<Review>>> GetMangaReviewsAsync(long id, ReviewsSearchConfig searchConfig, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns collection of manga related entries.
	/// </summary>
	/// <param name="id">MAL id of manga.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Collection of manga related entries.</returns>
	Task<PaginatedTenraiResponse<ICollection<RelatedEntry>>> GetMangaRelationsAsync(long id, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns collection of manga related entries.
	/// </summary>
	/// <param name="id">MAL id of manga.</param>
	/// <param name="searchConfig">Safe-for-work filtering for this call.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Collection of manga related entries.</returns>
	Task<PaginatedTenraiResponse<ICollection<RelatedEntry>>> GetMangaRelationsAsync(long id, SfwConfig searchConfig, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns collection of external services links related to manga.
	/// </summary>
	/// <param name="id">MAL id of manga.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Collection of external services links related to anime.</returns>
	Task<BaseTenraiResponse<ICollection<ExternalLink>>> GetMangaExternalLinksAsync(long id, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns manga with additional data.
	/// </summary>
	/// <param name="id">MAL id of manga.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Manga with additional data.</returns>
	Task<FullTenraiResponse<MangaFull>> GetMangaFullDataAsync(long id, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns manga with additional data, plus up to 24 more manga in the same request.
	/// </summary>
	/// <param name="id">MAL id of manga.</param>
	/// <param name="config">Extra ids, archive mode, and sfw filtering of relations.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Manga with additional data, the extra manga in <c>Included</c>, and ids not found in <c>MissingIds</c>.</returns>
	Task<FullTenraiResponse<MangaFull>> GetMangaFullDataAsync(long id, FullDataConfig config, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns the complete collection of manga MAL IDs available in the catalogue.
	/// </summary>
	/// <remarks>Requires a Tenrai Server Key (set <see cref="Config.TenraiClientConfiguration.ServerKey"/>); otherwise the API responds with 401.</remarks>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Collection of manga MAL IDs.</returns>
	Task<IdsTenraiResponse> GetMangaIdsAsync(CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns the MAL IDs of the manga matching the filters.
	/// </summary>
	/// <remarks>Requires a Tenrai Server Key (set <see cref="Config.TenraiClientConfiguration.ServerKey"/>); otherwise the API responds with 401.</remarks>
	/// <param name="searchConfig">Types, status, updated date and safe-for-work filters.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Collection of manga MAL IDs.</returns>
	Task<IdsTenraiResponse> GetMangaIdsAsync(MangaIdsConfig searchConfig, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns the MAL ID, title and type of every manga in the catalogue.
	/// </summary>
	/// <remarks>Requires a Tenrai Server Key (set <see cref="Config.TenraiClientConfiguration.ServerKey"/>); otherwise the API responds with 401.</remarks>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Collection of manga ids with their title and type.</returns>
	Task<IdEntriesTenraiResponse> GetMangaIdEntriesAsync(CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns the MAL ID, title and type of the manga matching the filters.
	/// </summary>
	/// <remarks>Requires a Tenrai Server Key (set <see cref="Config.TenraiClientConfiguration.ServerKey"/>); otherwise the API responds with 401.</remarks>
	/// <param name="searchConfig">Types, status, updated date and safe-for-work filters.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Collection of manga ids with their title and type.</returns>
	Task<IdEntriesTenraiResponse> GetMangaIdEntriesAsync(MangaIdsConfig searchConfig, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns manga still awaiting approval on MyAnimeList.
	/// </summary>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Paginated collection of unapproved manga.</returns>
	Task<PaginatedTenraiResponse<ICollection<Manga>>> GetUnapprovedMangaAsync(CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns manga awaiting approval on MyAnimeList, searched, sorted and paged.
	/// </summary>
	/// <param name="searchConfig">Search, sort and paging.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Paginated collection of unapproved manga.</returns>
	Task<PaginatedTenraiResponse<ICollection<Manga>>> GetUnapprovedMangaAsync(CatalogueSearchConfig searchConfig, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns manga submissions MyAnimeList denied.
	/// </summary>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Paginated collection of denied manga.</returns>
	Task<PaginatedTenraiResponse<ICollection<Manga>>> GetDeniedMangaAsync(CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns manga submissions MyAnimeList denied, searched, sorted and paged.
	/// </summary>
	/// <param name="searchConfig">Search, sort and paging.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Paginated collection of denied manga.</returns>
	Task<PaginatedTenraiResponse<ICollection<Manga>>> GetDeniedMangaAsync(CatalogueSearchConfig searchConfig, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns manga deleted from MyAnimeList.
	/// </summary>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Paginated collection of deleted manga.</returns>
	Task<PaginatedTenraiResponse<ICollection<Manga>>> GetDeletedMangaAsync(CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns manga deleted from MyAnimeList, searched, sorted and paged.
	/// </summary>
	/// <param name="searchConfig">Search, sort and paging.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Paginated collection of deleted manga.</returns>
	Task<PaginatedTenraiResponse<ICollection<Manga>>> GetDeletedMangaAsync(DeletedSearchConfig searchConfig, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns manga that have an anime adaptation.
	/// </summary>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Paginated collection of adapted manga.</returns>
	Task<PaginatedTenraiResponse<ICollection<Manga>>> GetAdaptedMangaAsync(CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns manga that have an anime adaptation, filtered, sorted and paged.
	/// </summary>
	/// <param name="searchConfig">Filters, sort and paging.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Paginated collection of adapted manga.</returns>
	Task<PaginatedTenraiResponse<ICollection<Manga>>> GetAdaptedMangaAsync(AdaptedMangaSearchConfig searchConfig, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns the day-by-day score, rank, popularity, members and favorites of a manga, newest first.
	/// </summary>
	/// <param name="id">MAL id of manga.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Paginated collection of history days.</returns>
	Task<PaginatedTenraiResponse<ICollection<StatisticsHistoryEntry<MangaStatistics>>>> GetMangaStatisticsHistoryAsync(long id, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns the day-by-day statistics of a manga for a date range.
	/// </summary>
	/// <param name="id">MAL id of manga.</param>
	/// <param name="config">Date range, order and paging.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Paginated collection of history days.</returns>
	Task<PaginatedTenraiResponse<ICollection<StatisticsHistoryEntry<MangaStatistics>>>> GetMangaStatisticsHistoryAsync(long id, StatisticsHistoryConfig config, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns list of results related to search.
	/// </summary>
	/// <param name="query">Search query.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>List of result related to search query.</returns>
	Task<PaginatedTenraiResponse<ICollection<Manga>>> SearchMangaAsync(string query, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns list of results related to search.
	/// </summary>
	/// <param name="searchConfig">Additional configuration for advanced search.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>List of result related to search query.</returns>
	Task<PaginatedTenraiResponse<ICollection<Manga>>> SearchMangaAsync(MangaSearchConfig searchConfig, CancellationToken cancellationToken = default);
}
