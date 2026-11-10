using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Tenrai;

public partial interface ITenrai
{
	/// <summary>
	/// Returns the first page of public interest stacks.
	/// </summary>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>First page of public interest stacks.</returns>
	Task<PaginatedTenraiResponse<ICollection<InterestStack>>> GetInterestStacksAsync(CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns a page of public interest stacks.
	/// </summary>
	/// <param name="page">Index of the page.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Page of public interest stacks.</returns>
	Task<PaginatedTenraiResponse<ICollection<InterestStack>>> GetInterestStacksAsync(int page, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns public interest stacks matching the given search configuration.
	/// </summary>
	/// <param name="searchConfig">Additional configuration for advanced search.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Public interest stacks matching the search configuration.</returns>
	Task<PaginatedTenraiResponse<ICollection<InterestStack>>> SearchInterestStacksAsync(InterestStackSearchConfig searchConfig, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns interest stack with given MAL id, including its entries in the order the author arranged them.
	/// </summary>
	/// <param name="id">MAL id of the interest stack.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Interest stack with given MAL id.</returns>
	Task<BaseTenraiResponse<InterestStackDetails>> GetInterestStackAsync(long id, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns interest stack with given MAL id, including its entries in the order the author arranged them.
	/// </summary>
	/// <param name="id">MAL id of the interest stack.</param>
	/// <param name="searchConfig">Safe-for-work filtering for this call; it removes adult entries from the stack.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Interest stack with given MAL id.</returns>
	Task<BaseTenraiResponse<InterestStackDetails>> GetInterestStackAsync(long id, SfwConfig searchConfig, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns first page of public interest stacks containing the anime with given MAL id.
	/// </summary>
	/// <param name="id">MAL id of anime.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>First page of public interest stacks containing the anime.</returns>
	Task<PaginatedTenraiResponse<ICollection<InterestStack>>> GetAnimeInterestStacksAsync(long id, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns first page of public interest stacks containing the anime with given MAL id.
	/// </summary>
	/// <param name="id">MAL id of anime.</param>
	/// <param name="searchConfig">Search text, spoiler/challenge/official filters, sorting, paging and safe-for-work filters.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>First page of public interest stacks containing the anime.</returns>
	Task<PaginatedTenraiResponse<ICollection<InterestStack>>> GetAnimeInterestStacksAsync(long id, InterestStackListConfig searchConfig, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns a page of public interest stacks containing the anime with given MAL id.
	/// </summary>
	/// <param name="id">MAL id of anime.</param>
	/// <param name="page">Index of the page.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Page of public interest stacks containing the anime.</returns>
	Task<PaginatedTenraiResponse<ICollection<InterestStack>>> GetAnimeInterestStacksAsync(long id, int page, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns first page of public interest stacks containing the manga with given MAL id.
	/// </summary>
	/// <param name="id">MAL id of manga.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>First page of public interest stacks containing the manga.</returns>
	Task<PaginatedTenraiResponse<ICollection<InterestStack>>> GetMangaInterestStacksAsync(long id, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns first page of public interest stacks containing the manga with given MAL id.
	/// </summary>
	/// <param name="id">MAL id of manga.</param>
	/// <param name="searchConfig">Search text, spoiler/challenge/official filters, sorting, paging and safe-for-work filters.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>First page of public interest stacks containing the manga.</returns>
	Task<PaginatedTenraiResponse<ICollection<InterestStack>>> GetMangaInterestStacksAsync(long id, InterestStackListConfig searchConfig, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns a page of public interest stacks containing the manga with given MAL id.
	/// </summary>
	/// <param name="id">MAL id of manga.</param>
	/// <param name="page">Index of the page.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Page of public interest stacks containing the manga.</returns>
	Task<PaginatedTenraiResponse<ICollection<InterestStack>>> GetMangaInterestStacksAsync(long id, int page, CancellationToken cancellationToken = default);
}
