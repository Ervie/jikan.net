using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Tenrai;

public partial interface ITenrai
{
	/// <summary>
	/// Returns person with given MAL id.
	/// </summary>
	/// <param name="id">MAL id of person.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Person with given MAL id.</returns>
	Task<BaseTenraiResponse<Person>> GetPersonAsync(long id, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns animeography of person with given MAL id.
	/// </summary>
	/// <param name="id">MAL id of person.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Collection of anime the person collaborated on.</returns>
	Task<BaseTenraiResponse<ICollection<PersonAnimeographyEntry>>> GetPersonAnimeAsync(long id, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns animeography of person with given MAL id.
	/// </summary>
	/// <param name="id">MAL id of person.</param>
	/// <param name="searchConfig">Safe-for-work filtering for this call.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Collection of anime the person collaborated on.</returns>
	Task<BaseTenraiResponse<ICollection<PersonAnimeographyEntry>>> GetPersonAnimeAsync(long id, SfwConfig searchConfig, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns mangaography of person with given MAL id.
	/// </summary>
	/// <param name="id">MAL id of person.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Collection of manga the person worked on.</returns>
	Task<BaseTenraiResponse<ICollection<PersonMangaographyEntry>>> GetPersonMangaAsync(long id, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns mangaography of person with given MAL id.
	/// </summary>
	/// <param name="id">MAL id of person.</param>
	/// <param name="searchConfig">Safe-for-work filtering for this call.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Collection of manga the person worked on.</returns>
	Task<BaseTenraiResponse<ICollection<PersonMangaographyEntry>>> GetPersonMangaAsync(long id, SfwConfig searchConfig, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns voice acting roles of person with given MAL id.
	/// </summary>
	/// <param name="id">MAL id of person.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Collection of voice acting roles of the person.</returns>
	Task<BaseTenraiResponse<ICollection<VoiceActingRole>>> GetPersonVoiceActingRolesAsync(long id, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns voice acting roles of person with given MAL id.
	/// </summary>
	/// <param name="id">MAL id of person.</param>
	/// <param name="searchConfig">Safe-for-work filtering for this call.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Collection of voice acting roles of the person.</returns>
	Task<BaseTenraiResponse<ICollection<VoiceActingRole>>> GetPersonVoiceActingRolesAsync(long id, SfwConfig searchConfig, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns collections of links to pictures related to person with given MAL id.
	/// </summary>
	/// <param name="id">MAL id of person.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Collections of links to pictures related to person with given MAL id.</returns>
	Task<BaseTenraiResponse<ICollection<ImagesSet>>> GetPersonPicturesAsync(long id, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns the first page of articles about person with given MAL id.
	/// </summary>
	/// <param name="id">MAL id of person.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Page of articles about the person.</returns>
	Task<PaginatedTenraiResponse<ICollection<Article>>> GetPersonArticlesAsync(long id, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns articles about person with given MAL id, filtered and sorted.
	/// </summary>
	/// <param name="id">MAL id of person.</param>
	/// <param name="filterConfig">Author, publish dates, sort order, paging and sfw filtering.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Page of articles about the person.</returns>
	Task<PaginatedTenraiResponse<ICollection<Article>>> GetPersonArticlesAsync(long id, ArticleFilterConfig filterConfig, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns person with additional data.
	/// </summary>
	/// <param name="id">MAL id of person.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Person with additional data.</returns>
	Task<BaseTenraiResponse<PersonFull>> GetPersonFullDataAsync(long id, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns person with additional data.
	/// </summary>
	/// <param name="id">MAL id of person.</param>
	/// <param name="searchConfig">Safe-for-work filtering for this call.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Person with additional data.</returns>
	Task<BaseTenraiResponse<PersonFull>> GetPersonFullDataAsync(long id, SfwConfig searchConfig, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns the complete collection of person MAL IDs available in the catalogue.
	/// </summary>
	/// <remarks>Requires a Tenrai Server Key (set <see cref="Config.TenraiClientConfiguration.ServerKey"/>); otherwise the API responds with 401.</remarks>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Collection of person MAL IDs.</returns>
	Task<IdsTenraiResponse> GetPersonIdsAsync(CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns list of results related to search.
	/// </summary>
	/// <param name="query">Search query.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>List of result related to search query.</returns>
	Task<PaginatedTenraiResponse<ICollection<Person>>> SearchPersonAsync(string query, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns list of results related to search.
	/// </summary>
	/// <param name="searchConfig">Additional configuration for advanced search.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>List of result related to search query.</returns>
	Task<PaginatedTenraiResponse<ICollection<Person>>> SearchPersonAsync(PersonSearchConfig searchConfig, CancellationToken cancellationToken = default);
}
