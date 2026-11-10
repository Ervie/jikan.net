using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Tenrai;

public partial interface ITenrai
{
	/// <summary>
	/// Returns list of top anime.
	/// </summary>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>List of top anime.</returns>
	Task<PaginatedTenraiResponse<ICollection<Anime>>> GetTopAnimeAsync(CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns list of top anime.
	/// </summary>
	/// <param name="page">Index of page folding 25 records of top ranging (e.g. 1 will return first 25 records, 2 will return record from 26 to 50 etc.)</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>List of top anime.</returns>
	Task<PaginatedTenraiResponse<ICollection<Anime>>> GetTopAnimeAsync(int page, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns list of top anime.
	/// </summary>
	/// <param name="filter">Filter determining result of request (e.g. TopAnimeFilter.Airing will return top airing anime.)</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>List of top anime.</returns>
	Task<PaginatedTenraiResponse<ICollection<Anime>>> GetTopAnimeAsync(TopAnimeFilter filter, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns list of top anime.
	/// </summary>
	/// <param name="filter">Filter determining result of request (e.g. TopAnimeFilter.Airing will return top airing anime.)</param>
	/// <param name="page">Index of page folding 25 records of top ranging (e.g. 1 will return first 25 records, 2 will return record from 26 to 50 etc.)</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>List of top anime.</returns>
	Task<PaginatedTenraiResponse<ICollection<Anime>>> GetTopAnimeAsync(TopAnimeFilter filter, int page, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns list of top anime.
	/// </summary>
	/// <param name="searchConfig">Class containing properties representing query filters</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>List of top anime.</returns>
	Task<PaginatedTenraiResponse<ICollection<Anime>>> GetTopAnimeAsync(AnimeTopSearchConfig searchConfig, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns list of top manga.
	/// </summary>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>List of top manga.</returns>
	Task<PaginatedTenraiResponse<ICollection<Manga>>> GetTopMangaAsync(CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns list of top manga.
	/// </summary>
	/// <param name="page">Index of page folding 25 records of top ranging (e.g. 1 will return first 25 records, 2 will return record from 26 to 50 etc.)</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>List of top manga.</returns>
	Task<PaginatedTenraiResponse<ICollection<Manga>>> GetTopMangaAsync(int page, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns list of top manga.
	/// </summary>
	/// <param name="searchConfig">Class containing properties representing query filters</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>List of top manga.</returns>
	Task<PaginatedTenraiResponse<ICollection<Manga>>> GetTopMangaAsync(MangaTopSearchConfig searchConfig, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns list of most popular people.
	/// </summary>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>List of most popular people.</returns>
	Task<PaginatedTenraiResponse<ICollection<Person>>> GetTopPeopleAsync(CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns list of most popular people.
	/// </summary>
	/// <param name="searchConfig">Page and page size.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>List of most popular people.</returns>
	Task<PaginatedTenraiResponse<ICollection<Person>>> GetTopPeopleAsync(PagingConfig searchConfig, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns list of most popular people.
	/// </summary>
	/// <param name="page">Index of page folding 25 records of top ranging (e.g. 1 will return first 25 records, 2 will return record from 26 to 50 etc.)</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>List of most popular people.</returns>
	Task<PaginatedTenraiResponse<ICollection<Person>>> GetTopPeopleAsync(int page, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns list of most popular characters.
	/// </summary>
	/// <returns>List of most popular characters.</returns>
	Task<PaginatedTenraiResponse<ICollection<Character>>> GetTopCharactersAsync(CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns list of most popular characters.
	/// </summary>
	/// <param name="searchConfig">Page and page size.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>List of most popular characters.</returns>
	Task<PaginatedTenraiResponse<ICollection<Character>>> GetTopCharactersAsync(PagingConfig searchConfig, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns list of most popular characters.
	/// </summary>
	/// <param name="page">Index of page folding 25 records of top ranging (e.g. 1 will return first 25 records, 2 will return record from 26 to 50 etc.)</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>List of most popular characters.</returns>
	Task<PaginatedTenraiResponse<ICollection<Character>>> GetTopCharactersAsync(int page, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns list of most popular reviews.
	/// </summary>
	/// <returns>List of most popular reviews.</returns>
	Task<PaginatedTenraiResponse<ICollection<Review>>> GetTopReviewsAsync(CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns list of most popular reviews, filtered using the supplied configuration.
	/// </summary>
	/// <param name="searchConfig">Query configuration (page, limit, target type, preliminary/spoiler filters).</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>List of most popular reviews.</returns>
	Task<PaginatedTenraiResponse<ICollection<Review>>> GetTopReviewsAsync(TopReviewsConfig searchConfig, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns the anime recommendations with the most votes.
	/// </summary>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Paginated collection of recommendations, most votes first.</returns>
	Task<PaginatedTenraiResponse<ICollection<TopRecommendation>>> GetTopAnimeRecommendationsAsync(CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns the anime recommendations with the most votes, paged.
	/// </summary>
	/// <param name="searchConfig">Paging and sfw filtering.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Paginated collection of recommendations, most votes first.</returns>
	Task<PaginatedTenraiResponse<ICollection<TopRecommendation>>> GetTopAnimeRecommendationsAsync(TopRecommendationsConfig searchConfig, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns the manga recommendations with the most votes.
	/// </summary>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Paginated collection of recommendations, most votes first.</returns>
	Task<PaginatedTenraiResponse<ICollection<TopRecommendation>>> GetTopMangaRecommendationsAsync(CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns the manga recommendations with the most votes, paged.
	/// </summary>
	/// <param name="searchConfig">Paging and sfw filtering.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Paginated collection of recommendations, most votes first.</returns>
	Task<PaginatedTenraiResponse<ICollection<TopRecommendation>>> GetTopMangaRecommendationsAsync(TopRecommendationsConfig searchConfig, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns the top anime episodes in the API's ranking, which weighs score by votes.
	/// </summary>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Paginated collection of episodes with the anime each belongs to.</returns>
	Task<PaginatedTenraiResponse<ICollection<TopEpisode>>> GetTopEpisodesAsync(CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns the top anime episodes by score, votes or comments.
	/// </summary>
	/// <param name="searchConfig">Sorting and paging.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Paginated collection of episodes with the anime each belongs to.</returns>
	Task<PaginatedTenraiResponse<ICollection<TopEpisode>>> GetTopEpisodesAsync(TopEpisodesConfig searchConfig, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns the most viewed anime promos and music videos on YouTube.
	/// </summary>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Paginated collection of videos with the anime each belongs to.</returns>
	Task<PaginatedTenraiResponse<ICollection<TopVideo>>> GetTopVideosAsync(CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns anime promos and music videos, filtered and sorted.
	/// </summary>
	/// <param name="searchConfig">Type, airing state, year range, sorting and paging.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Paginated collection of videos with the anime each belongs to.</returns>
	Task<PaginatedTenraiResponse<ICollection<TopVideo>>> GetTopVideosAsync(TopVideosConfig searchConfig, CancellationToken cancellationToken = default);
}
