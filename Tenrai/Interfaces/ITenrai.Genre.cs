using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Tenrai;

public partial interface ITenrai
{
	/// <summary>
	/// Returns list of anime genres.
	/// </summary>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>List of anime genres</returns>
	Task<BaseTenraiResponse<ICollection<Genre>>> GetAnimeGenresAsync(CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns list of anime genres.
	/// </summary>
	/// <param name="searchConfig">Genre kind and safe-for-work filters.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>List of anime genres</returns>
	Task<BaseTenraiResponse<ICollection<Genre>>> GetAnimeGenresAsync(GenresConfig searchConfig, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns list of anime genres.
	/// </summary>
	/// <param name="filter">Filter for genre types.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>List of anime genres</returns>
	Task<BaseTenraiResponse<ICollection<Genre>>> GetAnimeGenresAsync(GenresFilter filter, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns list of manga genres.
	/// </summary>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>List of manga genres</returns>
	Task<BaseTenraiResponse<ICollection<Genre>>> GetMangaGenresAsync(CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns list of manga genres.
	/// </summary>
	/// <param name="searchConfig">Genre kind and safe-for-work filters.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>List of manga genres</returns>
	Task<BaseTenraiResponse<ICollection<Genre>>> GetMangaGenresAsync(GenresConfig searchConfig, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns list of manga genres.
	/// </summary>
	/// <param name="filter">Filter for genre types.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>List of manga genres</returns>
	Task<BaseTenraiResponse<ICollection<Genre>>> GetMangaGenresAsync(GenresFilter filter, CancellationToken cancellationToken = default);
}
