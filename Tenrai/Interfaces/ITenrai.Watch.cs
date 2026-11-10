using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tenrai.Consts;

namespace Tenrai;

public partial interface ITenrai
{
	/// <summary>
	/// Return collection of recently released episodes details.
	/// </summary>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Collection of recently released episodes details..</returns>
	[Obsolete(ErrorMessagesConst.UnsupportedWatch)]
	Task<PaginatedTenraiResponse<ICollection<WatchEpisode>>> GetWatchRecentEpisodesAsync(CancellationToken cancellationToken = default);

	/// <summary>
	/// Return collection of popular episodes details.
	/// </summary>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Collection of popular episodes details.</returns>
	[Obsolete(ErrorMessagesConst.UnsupportedWatch)]
	Task<PaginatedTenraiResponse<ICollection<WatchEpisode>>> GetWatchPopularEpisodesAsync(CancellationToken cancellationToken = default);

	/// <summary>
	/// Return collection of recently released promos details.
	/// </summary>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Collection of recently released promos details.</returns>
	[Obsolete(ErrorMessagesConst.UnsupportedWatch)]
	Task<PaginatedTenraiResponse<ICollection<WatchPromoVideo>>> GetWatchRecentPromosAsync(CancellationToken cancellationToken = default);

	/// <summary>
	/// Return collection of recently released promos details.
	/// </summary>
	/// <param name="page">Index of page folding 100 records of top ranging (e.g. 1 will return first 100 records, 2 will return record from 101 to 200 etc.)</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Collection of recently released promos details.</returns>
	[Obsolete(ErrorMessagesConst.UnsupportedWatch)]
	Task<PaginatedTenraiResponse<ICollection<WatchPromoVideo>>> GetWatchRecentPromosAsync(int page, CancellationToken cancellationToken = default);

	/// <summary>
	/// Return collection of popular promos details.
	/// </summary>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Collection of popular promos details.</returns>
	[Obsolete(ErrorMessagesConst.UnsupportedWatch)]
	Task<PaginatedTenraiResponse<ICollection<WatchPromoVideo>>> GetWatchPopularPromosAsync(CancellationToken cancellationToken = default);
}
