using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Tenrai;

public partial interface ITenrai
{
	/// <summary>
	/// Returns season preview.
	/// </summary>
	/// <param name="year">Year of selected season.</param>
	/// <param name="season">Selected season.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Season preview.</returns>
	Task<PaginatedTenraiResponse<ICollection<Anime>>> GetSeasonAsync(int year, Season season, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns season preview.
	/// </summary>
	/// <param name="year">Year of selected season.</param>
	/// <param name="season">Selected season.</param>
	/// <param name="page">Index of page folding 25 records of top ranging (e.g. 1 will return first 25 records, 2 will return record from 26 to 50 etc.)</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Season preview.</returns>
	Task<PaginatedTenraiResponse<ICollection<Anime>>> GetSeasonAsync(int year, Season season, int page, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns anime from the given season, filtered and sorted.
	/// </summary>
	/// <param name="year">Year of selected season.</param>
	/// <param name="season">Selected season.</param>
	/// <param name="searchConfig">Types, status, ratings, genres, ranges, sorting and paging.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Season preview for anime in the given season.</returns>
	Task<PaginatedTenraiResponse<ICollection<Anime>>> GetSeasonAsync(int year, Season season, SeasonSearchConfig searchConfig, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns list of available season to query with <see cref="GetSeasonAsync(int, Season, CancellationToken)"/>
	/// </summary>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns></returns>
	Task<PaginatedTenraiResponse<ICollection<SeasonArchive>>> GetSeasonArchiveAsync(CancellationToken cancellationToken = default);

	/// <summary>
	/// Return season preview for anime in current airing season.
	/// </summary>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Season preview for anime in current airing season.</returns>
	Task<PaginatedTenraiResponse<ICollection<Anime>>> GetCurrentSeasonAsync(CancellationToken cancellationToken = default);

	/// <summary>
	/// Return season preview for anime in current airing season.
	/// </summary>
	/// <param name="page">Index of page folding 25 records of top ranging (e.g. 1 will return first 25 records, 2 will return record from 26 to 50 etc.)</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Season preview for anime in current airing season.</returns>
	Task<PaginatedTenraiResponse<ICollection<Anime>>> GetCurrentSeasonAsync(int page, CancellationToken cancellationToken = default);

	/// <summary>
	/// Return season preview for anime in current airing season, filtered and sorted.
	/// </summary>
	/// <param name="searchConfig">Types, status, ratings, genres, ranges, sorting and paging.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Season preview for anime in current airing season.</returns>
	Task<PaginatedTenraiResponse<ICollection<Anime>>> GetCurrentSeasonAsync(SeasonSearchConfig searchConfig, CancellationToken cancellationToken = default);

	/// <summary>
	/// Return season preview for anime with undefined airing season (marked as "Later" on MAL).
	/// </summary>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Season preview for anime with undefined airing date.</returns>
	Task<PaginatedTenraiResponse<ICollection<Anime>>> GetUpcomingSeasonAsync(CancellationToken cancellationToken = default);

	/// <summary>
	/// Return season preview for anime with undefined airing season (marked as "Later" on MAL).
	/// </summary>
	/// <param name="page">Index of page folding 25 records of top ranging (e.g. 1 will return first 25 records, 2 will return record from 26 to 50 etc.)</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Season preview for anime with undefined airing date.</returns>
	Task<PaginatedTenraiResponse<ICollection<Anime>>> GetUpcomingSeasonAsync(int page, CancellationToken cancellationToken = default);

	/// <summary>
	/// Return season preview for anime with undefined airing season, filtered and sorted. The status filter does not apply here.
	/// </summary>
	/// <param name="searchConfig">Types, ratings, genres, ranges, sorting and paging.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Season preview for anime with undefined airing date.</returns>
	Task<PaginatedTenraiResponse<ICollection<Anime>>> GetUpcomingSeasonAsync(SeasonSearchConfig searchConfig, CancellationToken cancellationToken = default);

	/// <summary>
	/// Return season preview for the season before the current one.
	/// </summary>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Season preview for anime in the previous season.</returns>
	Task<PaginatedTenraiResponse<ICollection<Anime>>> GetPreviousSeasonAsync(CancellationToken cancellationToken = default);

	/// <summary>
	/// Return season preview for the season before the current one.
	/// </summary>
	/// <param name="page">Index of page folding 25 records (e.g. 1 will return first 25 records, 2 will return record from 26 to 50 etc.)</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Season preview for anime in the previous season.</returns>
	Task<PaginatedTenraiResponse<ICollection<Anime>>> GetPreviousSeasonAsync(int page, CancellationToken cancellationToken = default);

	/// <summary>
	/// Return season preview for the season before the current one, filtered and sorted.
	/// </summary>
	/// <param name="searchConfig">Types, status, ratings, genres, ranges, sorting and paging.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Season preview for anime in the previous season.</returns>
	Task<PaginatedTenraiResponse<ICollection<Anime>>> GetPreviousSeasonAsync(SeasonSearchConfig searchConfig, CancellationToken cancellationToken = default);
}
