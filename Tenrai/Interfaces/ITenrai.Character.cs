using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Tenrai;

public partial interface ITenrai
{
	/// <summary>
	/// Returns character with given MAL id.
	/// </summary>
	/// <param name="id">MAL id of character.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Character with given MAL id.</returns>
	Task<BaseTenraiResponse<Character>> GetCharacterAsync(long id, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns animeography of character with given MAL id.
	/// </summary>
	/// <param name="id">MAL id of character.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Collection of anime where character has appeared.</returns>
	Task<BaseTenraiResponse<ICollection<CharacterAnimeographyEntry>>> GetCharacterAnimeAsync(long id, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns animeography of character with given MAL id.
	/// </summary>
	/// <param name="id">MAL id of character.</param>
	/// <param name="searchConfig">Safe-for-work filtering for this call.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Collection of anime where character has appeared.</returns>
	Task<BaseTenraiResponse<ICollection<CharacterAnimeographyEntry>>> GetCharacterAnimeAsync(long id, SfwConfig searchConfig, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns mangaography of character with given MAL id.
	/// </summary>
	/// <param name="id">MAL id of character.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Collection of manga where character has appeared.</returns>
	Task<BaseTenraiResponse<ICollection<CharacterMangaographyEntry>>> GetCharacterMangaAsync(long id, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns mangaography of character with given MAL id.
	/// </summary>
	/// <param name="id">MAL id of character.</param>
	/// <param name="searchConfig">Safe-for-work filtering for this call.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Collection of manga where character has appeared.</returns>
	Task<BaseTenraiResponse<ICollection<CharacterMangaographyEntry>>> GetCharacterMangaAsync(long id, SfwConfig searchConfig, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns voice actors of character with given MAL id.
	/// </summary>
	/// <param name="id">MAL id of character.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Collection of voice actors voicing character.</returns>
	Task<BaseTenraiResponse<ICollection<VoiceActorEntry>>> GetCharacterVoiceActorsAsync(long id, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns collections of links to pictures related to character with given MAL id.
	/// </summary>
	/// <param name="id">MAL id of character.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Collections of links to pictures related to character with given MAL id.</returns>
	Task<BaseTenraiResponse<ICollection<ImagesSet>>> GetCharacterPicturesAsync(long id, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns the first page of articles about character with given MAL id.
	/// </summary>
	/// <param name="id">MAL id of character.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Page of articles about the character.</returns>
	Task<PaginatedTenraiResponse<ICollection<Article>>> GetCharacterArticlesAsync(long id, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns articles about character with given MAL id, filtered and sorted.
	/// </summary>
	/// <param name="id">MAL id of character.</param>
	/// <param name="filterConfig">Author, publish dates, sort order, paging and sfw filtering.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Page of articles about the character.</returns>
	Task<PaginatedTenraiResponse<ICollection<Article>>> GetCharacterArticlesAsync(long id, ArticleFilterConfig filterConfig, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns character with additional data.
	/// </summary>
	/// <param name="id">MAL id of character.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Character with additional data.</returns>
	Task<BaseTenraiResponse<CharacterFull>> GetCharacterFullDataAsync(long id, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns character with additional data.
	/// </summary>
	/// <param name="id">MAL id of character.</param>
	/// <param name="searchConfig">Safe-for-work filtering for this call.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Character with additional data.</returns>
	Task<BaseTenraiResponse<CharacterFull>> GetCharacterFullDataAsync(long id, SfwConfig searchConfig, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns the complete collection of character MAL IDs available in the catalogue.
	/// </summary>
	/// <remarks>Requires a Tenrai Server Key (set <see cref="Config.TenraiClientConfiguration.ServerKey"/>); otherwise the API responds with 401.</remarks>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Collection of character MAL IDs.</returns>
	Task<IdsTenraiResponse> GetCharacterIdsAsync(CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns list of results related to search.
	/// </summary>
	/// <param name="query">Search query.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>List of result related to search query.</returns>
	Task<PaginatedTenraiResponse<ICollection<Character>>> SearchCharacterAsync(string query, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns list of results related to search.
	/// </summary>
	/// <param name="searchConfig">Additional configuration for advanced search.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>List of result related to search query.</returns>
	Task<PaginatedTenraiResponse<ICollection<Character>>> SearchCharacterAsync(CharacterSearchConfig searchConfig, CancellationToken cancellationToken = default);
}
