using System;
using System.Threading;
using System.Threading.Tasks;
using Tenrai.Consts;

namespace Tenrai;

public partial interface ITenrai
{
	/// <summary>
	/// Gets random anime.
	/// </summary>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Random anime</returns>
	Task<BaseTenraiResponse<Anime>> GetRandomAnimeAsync(CancellationToken cancellationToken = default);

	/// <summary>
	/// Gets random manga.
	/// </summary>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Random manga</returns>
	Task<BaseTenraiResponse<Manga>> GetRandomMangaAsync(CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns a random anime matching the filters.
	/// </summary>
	/// <param name="config">Types, status, ratings, source, genres, score and year filters.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>A random anime.</returns>
	Task<BaseTenraiResponse<Anime>> GetRandomAnimeAsync(RandomAnimeConfig config, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns a random manga matching the filters.
	/// </summary>
	/// <param name="config">Types, status, genres, authors, score and year filters.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>A random manga.</returns>
	Task<BaseTenraiResponse<Manga>> GetRandomMangaAsync(RandomMangaConfig config, CancellationToken cancellationToken = default);

	/// <summary>
	/// Gets random character.
	/// </summary>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Random character</returns>
	Task<BaseTenraiResponse<Character>> GetRandomCharacterAsync(CancellationToken cancellationToken = default);

	/// <summary>
	/// Gets random person.
	/// </summary>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Random person</returns>
	Task<BaseTenraiResponse<Person>> GetRandomPersonAsync(CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns a random character within a favorites range.
	/// </summary>
	/// <param name="config">Favorites range.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>A random character.</returns>
	Task<BaseTenraiResponse<Character>> GetRandomCharacterAsync(RandomFavoritesConfig config, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns a random person within a favorites range.
	/// </summary>
	/// <param name="config">Favorites range.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>A random person.</returns>
	Task<BaseTenraiResponse<Person>> GetRandomPersonAsync(RandomFavoritesConfig config, CancellationToken cancellationToken = default);

	/// <summary>
	/// Gets random user.
	/// </summary>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Random character</returns>
	[Obsolete(ErrorMessagesConst.UnsupportedUserData)]
	Task<BaseTenraiResponse<UserProfile>> GetRandomUserAsync(CancellationToken cancellationToken = default);
}
