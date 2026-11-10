using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tenrai.Consts;

namespace Tenrai;

public partial interface ITenrai
{
	/// <summary>
	/// Returns anime with given MAL id.
	/// </summary>
	/// <param name="id">MAL id of anime.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Anime with given MAL id.</returns>
	Task<BaseTenraiResponse<Anime>> GetAnimeAsync(long id, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns collections of characters of anime with given MAL id.
	/// </summary>
	/// <param name="id">MAL id of anime.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Collection of characters of anime with given MAL id.</returns>
	Task<BaseTenraiResponse<ICollection<AnimeCharacter>>> GetAnimeCharactersAsync(long id, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns collections of staff of anime with given MAL id.
	/// </summary>
	/// <param name="id">MAL id of anime.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Collection of staff of anime with given MAL id.</returns>
	Task<BaseTenraiResponse<ICollection<AnimeStaffPosition>>> GetAnimeStaffAsync(long id, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns list of episodes for anime with given MAL id.
	/// </summary>
	/// <param name="id">MAL id of anime.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>List of episodes with details.</returns>
	Task<PaginatedTenraiResponse<ICollection<AnimeEpisode>>> GetAnimeEpisodesAsync(long id, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns list of episodes for anime with given MAL id.
	/// </summary>
	/// <param name="id">MAL id of anime.</param>
	/// <param name="page">Index of page folding 100 records of top ranging (e.g. 1 will return first 100 records, 2 will return record from 101 to 200 etc.)</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>List of episodes with details.</returns>
	Task<PaginatedTenraiResponse<ICollection<AnimeEpisode>>> GetAnimeEpisodesAsync(long id, int page, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns details about specific episode.
	/// </summary>
	/// <param name="animeId">MAL id of anime.</param>
	/// <param name="episodeId">Id of episode.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Details about specific episode.</returns>
	Task<BaseTenraiResponse<AnimeEpisode>> GetAnimeEpisodeAsync(long animeId, int episodeId, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns collections of news related to anime with given MAL id.
	/// </summary>
	/// <param name="id">MAL id of anime.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Collections of news related to anime with given MAL id.</returns>
	Task<PaginatedTenraiResponse<ICollection<News>>> GetAnimeNewsAsync(long id, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns collections of news related to anime with given MAL id.
	/// </summary>
	/// <param name="id">MAL id of anime.</param>
	/// <param name="page">Index of page folding 100 records of top ranging (e.g. 1 will return first 100 records, 2 will return record from 101 to 200 etc.)</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Collections of news related to anime with given MAL id.</returns>
	Task<PaginatedTenraiResponse<ICollection<News>>> GetAnimeNewsAsync(long id, int page, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns news related to anime with given MAL id, filtered and sorted.
	/// </summary>
	/// <param name="id">MAL id of anime.</param>
	/// <param name="filterConfig">Author, publish dates, sort order, paging and sfw filtering.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Page of news related to the anime.</returns>
	Task<PaginatedTenraiResponse<ICollection<News>>> GetAnimeNewsAsync(long id, NewsFilterConfig filterConfig, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns the first page of articles about anime with given MAL id.
	/// </summary>
	/// <param name="id">MAL id of anime.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Page of articles about the anime.</returns>
	Task<PaginatedTenraiResponse<ICollection<Article>>> GetAnimeArticlesAsync(long id, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns articles about anime with given MAL id, filtered and sorted.
	/// </summary>
	/// <param name="id">MAL id of anime.</param>
	/// <param name="filterConfig">Author, publish dates, sort order, paging and sfw filtering.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Page of articles about the anime.</returns>
	Task<PaginatedTenraiResponse<ICollection<Article>>> GetAnimeArticlesAsync(long id, ArticleFilterConfig filterConfig, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns the first page of episode discussion threads for anime with given MAL id. Other forum topics are not available.
	/// </summary>
	/// <param name="id">MAL id of anime.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Page of episode discussion threads, 30 per page.</returns>
	Task<PaginatedTenraiResponse<ICollection<ForumTopic>>> GetAnimeForumTopicsAsync(long id, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns a page of episode discussion threads for anime with given MAL id, sorted as requested.
	/// </summary>
	/// <param name="id">MAL id of anime.</param>
	/// <param name="searchConfig">Page and sort order.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Page of episode discussion threads, 30 per page.</returns>
	Task<PaginatedTenraiResponse<ICollection<ForumTopic>>> GetAnimeForumTopicsAsync(long id, AnimeForumSearchConfig searchConfig, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns collections of videos related to anime with given MAL id.
	/// </summary>
	/// <param name="id">MAL id of anime.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Collections of videos related to anime with given MAL id.</returns>
	Task<BaseTenraiResponse<AnimeVideos>> GetAnimeVideosAsync(long id, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns collections of video episodes related to anime with given MAL id.
	/// </summary>
	/// <param name="id">MAL id of anime.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Collections of video episodes related to anime with given MAL id.</returns>
	Task<PaginatedTenraiResponse<ICollection<EpisodeVideo>>> GetAnimeVideosEpisodesAsync(long id, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns collections of video episodes related to anime with given MAL id.
	/// </summary>
	/// <param name="id">MAL id of anime.</param>
	/// <param name="page">Index of page folding 100 records of top ranging (e.g. 1 will return first 100 records, 2 will return record from 101 to 200 etc.)</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Collections of video episodes related to anime with given MAL id.</returns>
	Task<PaginatedTenraiResponse<ICollection<EpisodeVideo>>> GetAnimeVideosEpisodesAsync(long id, int page, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns collections of links to pictures related to anime with given MAL id.
	/// </summary>
	/// <param name="id">MAL id of anime.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Collections of links to pictures related to anime with given MAL id.</returns>
	Task<BaseTenraiResponse<ICollection<ImagesSet>>> GetAnimePicturesAsync(long id, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns statistics related to anime with given MAL id.
	/// </summary>
	/// <param name="id">MAL id of anime.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Statistics related to anime with given MAL id.</returns>
	Task<BaseTenraiResponse<AnimeStatistics>> GetAnimeStatisticsAsync(long id, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns additional information related to anime with given MAL id.
	/// </summary>
	/// <param name="id">MAL id of anime.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Additional information related to anime with given MAL id.</returns>
	Task<BaseTenraiResponse<MoreInfo>> GetAnimeMoreInfoAsync(long id, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns collection of anime recommendation.
	/// </summary>
	/// <param name="id">MAL id of anime.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Collection of anime recommendation.</returns>
	Task<BaseTenraiResponse<ICollection<Recommendation>>> GetAnimeRecommendationsAsync(long id, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns collection of anime recommendation.
	/// </summary>
	/// <param name="id">MAL id of anime.</param>
	/// <param name="searchConfig">Username and safe-for-work filters.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Collection of anime recommendation.</returns>
	Task<BaseTenraiResponse<ICollection<Recommendation>>> GetAnimeRecommendationsAsync(long id, RecommendationsConfig searchConfig, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns collection of anime user updates.
	/// </summary>
	/// <param name="id">MAL id of anime.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Collection of anime user updates.</returns>
	[Obsolete(ErrorMessagesConst.UnsupportedUserUpdates)]
	Task<PaginatedTenraiResponse<ICollection<AnimeUserUpdate>>> GetAnimeUserUpdatesAsync(long id, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns collection of anime user updates.
	/// </summary>
	/// <param name="id">MAL id of anime.</param>
	/// <param name="page">Index of page folding 75 records of top ranging (e.g. 1 will return first 75 records, 2 will return record from 76 to 150 etc.)</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Collection of anime user updates.</returns>
	[Obsolete(ErrorMessagesConst.UnsupportedUserUpdates)]
	Task<PaginatedTenraiResponse<ICollection<AnimeUserUpdate>>> GetAnimeUserUpdatesAsync(long id, int page, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns collection of anime reviews.
	/// </summary>
	/// <param name="id">MAL id of anime.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Collection of anime reviews.</returns>
	Task<PaginatedTenraiResponse<ICollection<Review>>> GetAnimeReviewsAsync(long id, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns collection of anime reviews, filtered and sorted using the supplied configuration.
	/// </summary>
	/// <param name="id">MAL id of anime.</param>
	/// <param name="searchConfig">Query configuration (page, sort order, preliminary/spoiler filters, sentiment).</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Collection of anime reviews.</returns>
	Task<PaginatedTenraiResponse<ICollection<Review>>> GetAnimeReviewsAsync(long id, ReviewsSearchConfig searchConfig, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns collection of anime related entries.
	/// </summary>
	/// <param name="id">MAL id of anime.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Collection of anime related entries.</returns>
	Task<PaginatedTenraiResponse<ICollection<RelatedEntry>>> GetAnimeRelationsAsync(long id, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns collection of anime related entries.
	/// </summary>
	/// <param name="id">MAL id of anime.</param>
	/// <param name="searchConfig">Safe-for-work filtering for this call.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Collection of anime related entries.</returns>
	Task<PaginatedTenraiResponse<ICollection<RelatedEntry>>> GetAnimeRelationsAsync(long id, SfwConfig searchConfig, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns collection of anime openings and endings.
	/// </summary>
	/// <param name="id">MAL id of anime.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Collection of anime openings and endings.</returns>
	Task<BaseTenraiResponse<AnimeThemes>> GetAnimeThemesAsync(long id, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns collection of external services links related to anime.
	/// </summary>
	/// <param name="id">MAL id of anime.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Collection of external services links related to anime.</returns>
	Task<BaseTenraiResponse<ICollection<ExternalLink>>> GetAnimeExternalLinksAsync(long id, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns collection of external streaming services links related to anime.
	/// </summary>
	/// <param name="id">MAL id of anime.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Collection of external services links related to anime.</returns>
	Task<BaseTenraiResponse<ICollection<ExternalLink>>> GetAnimeStreamingLinksAsync(long id, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns anime with additional data.
	/// </summary>
	/// <param name="id">MAL id of anime.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Anime with additional data.</returns>
	Task<FullTenraiResponse<AnimeFull>> GetAnimeFullDataAsync(long id, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns anime with additional data, plus up to 24 more anime in the same request.
	/// </summary>
	/// <param name="id">MAL id of anime.</param>
	/// <param name="config">Extra ids, archive mode, and sfw filtering of relations.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Anime with additional data, the extra anime in <c>Included</c>, and ids not found in <c>MissingIds</c>.</returns>
	Task<FullTenraiResponse<AnimeFull>> GetAnimeFullDataAsync(long id, FullDataConfig config, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns the complete collection of anime MAL IDs available in the catalogue.
	/// </summary>
	/// <remarks>Requires a Tenrai Server Key (set <see cref="Config.TenraiClientConfiguration.ServerKey"/>); otherwise the API responds with 401.</remarks>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Collection of anime MAL IDs.</returns>
	Task<IdsTenraiResponse> GetAnimeIdsAsync(CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns the MAL IDs of the anime matching the filters.
	/// </summary>
	/// <remarks>Requires a Tenrai Server Key (set <see cref="Config.TenraiClientConfiguration.ServerKey"/>); otherwise the API responds with 401.</remarks>
	/// <param name="searchConfig">Types, ratings, status, updated date and safe-for-work filters.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Collection of anime MAL IDs.</returns>
	Task<IdsTenraiResponse> GetAnimeIdsAsync(AnimeIdsConfig searchConfig, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns the MAL ID, title and type of every anime in the catalogue.
	/// </summary>
	/// <remarks>Requires a Tenrai Server Key (set <see cref="Config.TenraiClientConfiguration.ServerKey"/>); otherwise the API responds with 401.</remarks>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Collection of anime ids with their title and type.</returns>
	Task<IdEntriesTenraiResponse> GetAnimeIdEntriesAsync(CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns the MAL ID, title and type of the anime matching the filters.
	/// </summary>
	/// <remarks>Requires a Tenrai Server Key (set <see cref="Config.TenraiClientConfiguration.ServerKey"/>); otherwise the API responds with 401.</remarks>
	/// <param name="searchConfig">Types, ratings, status, updated date and safe-for-work filters.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Collection of anime ids with their title and type.</returns>
	Task<IdEntriesTenraiResponse> GetAnimeIdEntriesAsync(AnimeIdsConfig searchConfig, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns anime still awaiting approval on MyAnimeList.
	/// </summary>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Paginated collection of unapproved anime.</returns>
	Task<PaginatedTenraiResponse<ICollection<Anime>>> GetUnapprovedAnimeAsync(CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns anime awaiting approval on MyAnimeList, searched, sorted and paged.
	/// </summary>
	/// <param name="searchConfig">Search, sort and paging.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Paginated collection of unapproved anime.</returns>
	Task<PaginatedTenraiResponse<ICollection<Anime>>> GetUnapprovedAnimeAsync(CatalogueSearchConfig searchConfig, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns anime submissions MyAnimeList denied.
	/// </summary>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Paginated collection of denied anime.</returns>
	Task<PaginatedTenraiResponse<ICollection<Anime>>> GetDeniedAnimeAsync(CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns anime submissions MyAnimeList denied, searched, sorted and paged.
	/// </summary>
	/// <param name="searchConfig">Search, sort and paging.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Paginated collection of denied anime.</returns>
	Task<PaginatedTenraiResponse<ICollection<Anime>>> GetDeniedAnimeAsync(CatalogueSearchConfig searchConfig, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns anime deleted from MyAnimeList.
	/// </summary>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Paginated collection of deleted anime.</returns>
	Task<PaginatedTenraiResponse<ICollection<Anime>>> GetDeletedAnimeAsync(CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns anime deleted from MyAnimeList, searched, sorted and paged.
	/// </summary>
	/// <param name="searchConfig">Search, sort and paging.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Paginated collection of deleted anime.</returns>
	Task<PaginatedTenraiResponse<ICollection<Anime>>> GetDeletedAnimeAsync(DeletedSearchConfig searchConfig, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns the day-by-day score, rank, popularity, members and favorites of an anime, newest first.
	/// </summary>
	/// <param name="id">MAL id of anime.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Paginated collection of history days.</returns>
	Task<PaginatedTenraiResponse<ICollection<StatisticsHistoryEntry<AnimeStatistics>>>> GetAnimeStatisticsHistoryAsync(long id, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns the day-by-day statistics of an anime for a date range.
	/// </summary>
	/// <param name="id">MAL id of anime.</param>
	/// <param name="config">Date range, order and paging.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Paginated collection of history days.</returns>
	Task<PaginatedTenraiResponse<ICollection<StatisticsHistoryEntry<AnimeStatistics>>>> GetAnimeStatisticsHistoryAsync(long id, StatisticsHistoryConfig config, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns list of results related to search.
	/// </summary>
	/// <param name="query">Search query.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>List of result related to search query.</returns>
	Task<PaginatedTenraiResponse<ICollection<Anime>>> SearchAnimeAsync(string query, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns list of results related to search.
	/// </summary>
	/// <param name="searchConfig">Additional configuration for advanced search.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>List of result related to search query.</returns>
	Task<PaginatedTenraiResponse<ICollection<Anime>>> SearchAnimeAsync(AnimeSearchConfig searchConfig, CancellationToken cancellationToken = default);
}
