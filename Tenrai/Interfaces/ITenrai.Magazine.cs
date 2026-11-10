using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Tenrai;

public partial interface ITenrai
{
	/// <summary>
	/// Returns information about magazines.
	/// </summary>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Basic Information about magazines.</returns>
	Task<PaginatedTenraiResponse<ICollection<Magazine>>> GetMagazinesAsync(CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns information about magazines.
	/// </summary>
	/// <param name="page">Index of page folding 100 records of top ranging (e.g. 1 will return first 100 records, 2 will return record from 101 to 200 etc.)</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Basic Information about magazines.</returns>
	Task<PaginatedTenraiResponse<ICollection<Magazine>>> GetMagazinesAsync(int page, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns magazines, searched and sorted.
	/// </summary>
	/// <param name="searchConfig">Text, letter, sorting and paging.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Paginated collection of magazines.</returns>
	Task<PaginatedTenraiResponse<ICollection<Magazine>>> SearchMagazinesAsync(MagazineSearchConfig searchConfig, CancellationToken cancellationToken = default);
}
