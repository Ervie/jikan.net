using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Tenrai;

public partial interface ITenrai
{
	/// <summary>
	/// Returns current season schedule.
	/// </summary>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Current season schedule.</returns>
	Task<PaginatedTenraiResponse<ICollection<Anime>>> GetScheduleAsync(CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns current season schedule.
	/// </summary>
	/// <param name="page">Index of page folding 25 records of top ranging (e.g. 1 will return first 25 records, 2 will return record from 26 to 50 etc.)</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Current season schedule.</returns>
	Task<PaginatedTenraiResponse<ICollection<Anime>>> GetScheduleAsync(int page, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns current season schedule.
	/// </summary>
	/// <param name="scheduledDay">Scheduled day to filter by.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Current season schedule.</returns>
	Task<PaginatedTenraiResponse<ICollection<Anime>>> GetScheduleAsync(ScheduledDay scheduledDay, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns the broadcast schedule, filtered by day, type, rating and genres.
	/// </summary>
	/// <param name="searchConfig">Day, types, ratings, genres and paging.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Current season schedule.</returns>
	Task<PaginatedTenraiResponse<ICollection<Anime>>> GetScheduleAsync(ScheduleSearchConfig searchConfig, CancellationToken cancellationToken = default);
}
