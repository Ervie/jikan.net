using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Tenrai;

public partial interface ITenrai
{
	/// <summary>
	/// Returns information about producers.
	/// </summary>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Basic Information about producers.</returns>
	Task<PaginatedTenraiResponse<ICollection<Producer>>> GetProducersAsync(CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns information about producers.
	/// </summary>
	/// <param name="page">Index of page folding 100 records of top ranging (e.g. 1 will return first 100 records, 2 will return record from 101 to 200 etc.)</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Basic Information about producers.</returns>
	Task<PaginatedTenraiResponse<ICollection<Producer>>> GetProducersAsync(int page, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns producers, studios and licensors, searched, filtered and sorted.
	/// </summary>
	/// <param name="searchConfig">Text, role, worked-with, genres, year filters, sorting and paging.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Paginated collection of producers.</returns>
	Task<PaginatedTenraiResponse<ICollection<Producer>>> SearchProducersAsync(ProducerSearchConfig searchConfig, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns information about producer.
	/// </summary>
	/// <param name="id">MAL id of the producer.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Basic Information about producer.</returns>
	Task<BaseTenraiResponse<Producer>> GetProducerAsync(long id, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns external links related to producer
	/// </summary>
	/// <param name="id">MAL id of the producer.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>External links related to producer</returns>
	Task<BaseTenraiResponse<ICollection<ExternalLink>>> GetProducerExternalLinksAsync(long id, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns full information about producer.
	/// </summary>
	/// <param name="id">MAL id of the producer.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Full Information about producer.</returns>
	Task<BaseTenraiResponse<ProducerFull>> GetProducerFullDataAsync(long id, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns the complete collection of producer MAL IDs available in the catalogue.
	/// </summary>
	/// <remarks>Requires a Tenrai Server Key (set <see cref="Config.TenraiClientConfiguration.ServerKey"/>); otherwise the API responds with 401.</remarks>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Collection of producer MAL IDs.</returns>
	Task<IdsTenraiResponse> GetProducerIdsAsync(CancellationToken cancellationToken = default);
}
