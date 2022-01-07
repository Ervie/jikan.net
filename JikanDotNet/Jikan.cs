using JikanDotNet.Config;
using JikanDotNet.Consts;
using JikanDotNet.Exceptions;
using JikanDotNet.Extensions;
using JikanDotNet.Helpers;
using JikanDotNet.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace JikanDotNet
{
	/// <summary>
	/// Implementation of Jikan wrapper for .Net platform.
	/// </summary>
	public class Jikan : IJikan
	{
		#region Fields

		/// <summary>
		/// Http client class to call REST request and receive REST response.
		/// </summary>
		private readonly HttpClient _httpClient;

		/// <summary>
		/// Client configuration.
		/// </summary>
		private readonly JikanClientConfiguration _jikanConfiguration;

		#endregion Fields

		#region Constructors

		/// <summary>
		/// Constructor.
		/// </summary>
		public Jikan() : this(new JikanClientConfiguration()) { }

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="jikanClientConfiguration">Options.</param>
		public Jikan(JikanClientConfiguration jikanClientConfiguration)
		{
			_jikanConfiguration = jikanClientConfiguration;
			_httpClient = string.IsNullOrWhiteSpace(_jikanConfiguration.Endpoint) ?
				HttpProvider.GetHttpClient() :
				HttpProvider.GetHttpClient(new Uri(_jikanConfiguration.Endpoint));
		}

		#endregion Constructors

		#region Private Methods

		/// <summary>
		/// Vasic method for handling requests and responses from endpoint.
		/// </summary>
		/// <typeparam name="T">Class type received from GET requests.</typeparam>
		/// <param name="args">Arguments building endpoint.</param>
		/// <returns>Requested object if successfull, null otherwise.</returns>
		private async Task<T> ExecuteGetRequestAsync<T>(string[] args) where T : class
		{
			T returnedObject = null;
			var requestUrl = string.Join("/", args);
			try
			{
				using HttpResponseMessage response = await _httpClient.GetAsync(requestUrl);
				if (response.IsSuccessStatusCode)
				{
					string json = await response.Content.ReadAsStringAsync();

					returnedObject = JsonSerializer.Deserialize<T>(json);
				}
				else if (!_jikanConfiguration.SuppressException)
				{
					string json = await response.Content.ReadAsStringAsync();
					var errorData = JsonSerializer.Deserialize<JikanApiError>(json);
					throw new JikanRequestException(string.Format(ErrorMessagesConst.FailedRequest, response.StatusCode, response.Content), errorData);
				}
			}
			catch (JsonException ex)
			{
				if (!_jikanConfiguration.SuppressException)
				{
					throw new JikanRequestException(ErrorMessagesConst.SerializationFailed + Environment.NewLine + "Inner exception message: " + ex.Message, ex);
				}
			}
			return returnedObject;
		}

		#endregion Private Methods

		#region Public Methods

		#region Anime methods

		#region GetAnimeAsync

		/// <summary>
		/// Return anime with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <returns>Anime with given MAL id.</returns>
		public async Task<BaseJikanResponse<Anime>> GetAnimeAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Anime, id.ToString() };
			return await ExecuteGetRequestAsync<BaseJikanResponse<Anime>>(endpointParts);
		}

		#endregion GetAnimeAsync

		#region GetAnimeCharactersAsync

		/// <summary>
		/// Return collections of characters of anime with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <returns>Collections of characters of anime with given MAL id.</returns>
		public async Task<BaseJikanResponse<ICollection<AnimeCharacter>>> GetAnimeCharactersAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Anime, id.ToString(), AnimeExtension.Characters.GetDescription() };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<AnimeCharacter>>>(endpointParts);
		}

		#endregion GetAnimeCharactersAsync

		#region GetAnimeStaffAsync

		/// <summary>
		/// Return collections of characters of anime with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <returns>Collections of characters of anime with given MAL id.</returns>
		public async Task<BaseJikanResponse<ICollection<AnimeStaffPosition>>> GetAnimeStaffAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Anime, id.ToString(), AnimeExtension.Staff.GetDescription() };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<AnimeStaffPosition>>>(endpointParts);
		}

		#endregion GetAnimeStaffAsync

		#region GetAnimeEpisodes

		/// <summary>
		/// Return list of episodes for anime with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <param name="page">Index of page folding 100 records of top ranging (e.g. 1 will return first 100 records, 2 will return record from 101 to 200 etc.)</param>
		/// <returns>List of episodes with details.</returns>
		public async Task<PaginatedJikanResponse<ICollection<AnimeEpisode>>> GetAnimeEpisodesAsync(long id, int page)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			Guard.IsGreaterThanZero(page, nameof(page));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Anime, id.ToString(), AnimeExtension.Episodes.GetDescription(), "?page=" , page.ToString() };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<AnimeEpisode>>>(endpointParts);
		}

		/// <summary>
		/// Return list of episodes for anime with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <returns>List of episodes with details.</returns>
		public async Task<PaginatedJikanResponse<ICollection<AnimeEpisode>>> GetAnimeEpisodesAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Anime, id.ToString(), AnimeExtension.Episodes.GetDescription() };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<AnimeEpisode>>>(endpointParts);
		}

		#endregion GetAnimeEpisodes

		#region GetAnimeEpisode

		/// <summary>
		/// Returns details about specific episode.
		/// </summary>
		/// <param name="animeId">MAL id of anime.</param>
		/// <param name="episodeId">Id of episode.</param>
		/// <returns>Details about specific episode.</returns>
		public async Task<BaseJikanResponse<AnimeEpisode>> GetAnimeEpisodeAsync(long animeId, int episodeId)
		{
			Guard.IsGreaterThanZero(animeId, nameof(animeId));
			Guard.IsGreaterThanZero(episodeId, nameof(episodeId));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Anime, animeId.ToString(), AnimeExtension.Episodes.GetDescription(), episodeId.ToString() };
			return await ExecuteGetRequestAsync<BaseJikanResponse<AnimeEpisode>>(endpointParts);
		}

		#endregion GetAnimeEpisode

		#region GetAnimeGenre

		/// <summary>
		/// Return information about anime genre.
		/// </summary>
		/// <param name="genreId">Id of the searched genre.</param>
		/// <returns>Information about anime genre</returns>
		public async Task<AnimeGenre> GetAnimeGenre(long genreId)
		{
			Guard.IsGreaterThanZero(genreId, nameof(genreId));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Genre, JikanEndPointCategoryConsts.Anime, genreId.ToString() };
			return await ExecuteGetRequestAsync<AnimeGenre>(endpointParts);
		}

		/// <summary>
		/// Return information about anime genre.
		/// </summary>
		/// <param name="animeGenre">Searched genre.</param>
		/// <returns>Information about anime genre</returns>
		public async Task<AnimeGenre> GetAnimeGenre(AnimeGenreSearch animeGenre)
		{
			Guard.IsValidEnum(animeGenre, nameof(animeGenre));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Genre, JikanEndPointCategoryConsts.Anime, animeGenre.GetDescription() };
			return await ExecuteGetRequestAsync<AnimeGenre>(endpointParts);
		}

		/// <summary>
		/// Return information about anime genre.
		/// </summary>
		/// <param name="genreId">Id of the searched genre.</param>
		/// <param name="page">Indexx of page folding 100 records of top ranging (e.g. 1 will return first 100 records, 2 will return record from 101 to 200 etc.)</param>
		/// <returns>Information about anime genre</returns>
		public async Task<AnimeGenre> GetAnimeGenre(long genreId, int page)
		{
			Guard.IsGreaterThanZero(genreId, nameof(genreId));
			Guard.IsGreaterThanZero(page, nameof(page));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Genre, JikanEndPointCategoryConsts.Anime, genreId.ToString(), page.ToString() };
			return await ExecuteGetRequestAsync<AnimeGenre>(endpointParts);
		}

		/// <summary>
		/// Return information about anime genre.
		/// </summary>
		/// <param name="animeGenre">Searched genre.</param>
		/// <param name="page">Index of page folding 100 records of top ranging (e.g. 1 will return first 100 records, 2 will return record from 101 to 200 etc.)</param>
		/// <returns>Information about anime genre</returns>
		public async Task<AnimeGenre> GetAnimeGenre(AnimeGenreSearch animeGenre, int page)
		{
			Guard.IsGreaterThanZero(page, nameof(page));
			Guard.IsValidEnum(animeGenre, nameof(animeGenre));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Genre, JikanEndPointCategoryConsts.Anime, animeGenre.GetDescription(), page.ToString() };
			return await ExecuteGetRequestAsync<AnimeGenre>(endpointParts);
		}

		#endregion GetAnimeGenre

		#region GetAnimePictures

		/// <summary>
		/// Return collections of links to pictures related to anime with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <returns>Collections of links to pictures related to anime with given MAL id.</returns>
		public async Task<AnimePictures> GetAnimePictures(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Anime, id.ToString(), AnimeExtension.Pictures.GetDescription() };
			return await ExecuteGetRequestAsync<AnimePictures>(endpointParts);
		}

		#endregion GetAnimePictures

		#region GetAnimeNews

		/// <summary>
		/// Return collections of news related to anime with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <returns>Collections of news related to anime with given MAL id.</returns>
		public async Task<AnimeNews> GetAnimeNews(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Anime, id.ToString(), AnimeExtension.News.GetDescription() };
			return await ExecuteGetRequestAsync<AnimeNews>(endpointParts);
		}

		#endregion GetAnimeNews

		#region GetAnimeVideos

		/// <summary>
		/// Return collections of videos related to anime with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <returns>Collections of videos related to anime with given MAL id.</returns>
		public async Task<AnimeVideos> GetAnimeVideos(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Anime, id.ToString(), AnimeExtension.Videos.GetDescription() };
			return await ExecuteGetRequestAsync<AnimeVideos>(endpointParts);
		}

		#endregion GetAnimeVideos

		#region GetAnimeStatistics

		/// <summary>
		/// Return statistics related to anime with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <returns>Statistics related to anime with given MAL id.</returns>
		public async Task<AnimeStats> GetAnimeStatistics(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Anime, id.ToString(), AnimeExtension.Stats.GetDescription() };
			return await ExecuteGetRequestAsync<AnimeStats>(endpointParts);
		}

		#endregion GetAnimeStatistics

		#region GetAnimeForumTopics

		/// <summary>
		/// Return collections of forum topics related to anime with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <returns>Collections of forum topics related to anime with given MAL id.</returns>
		public async Task<ForumTopics> GetAnimeForumTopics(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Anime, id.ToString(), AnimeExtension.Forum.GetDescription() };
			return await ExecuteGetRequestAsync<ForumTopics>(endpointParts);
		}

		#endregion GetAnimeForumTopics

		#region GetAnimeMoreInfo

		/// <summary>
		/// Return additional information related to anime with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <returns>Additional information related to anime with given MAL id.</returns>
		public async Task<MoreInfo> GetAnimeMoreInfo(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Anime, id.ToString(), AnimeExtension.MoreInfo.GetDescription() };
			return await ExecuteGetRequestAsync<MoreInfo>(endpointParts);
		}

		#endregion GetAnimeMoreInfo

		#region GetAnimeRecommendations

		/// <summary>
		/// Returns collection of anime recommendation.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <returns>Collection of anime recomendation.</returns>
		public async Task<Recommendations> GetAnimeRecommendations(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Anime, id.ToString(), AnimeExtension.Recommendations.GetDescription() };
			return await ExecuteGetRequestAsync<Recommendations>(endpointParts);
		}

		#endregion GetAnimeRecommendations

		#region GetAnimeReviews

		/// <summary>
		/// Returns collection of anime reviews.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <returns>Collection of anime reviews.</returns>
		public async Task<AnimeReviews> GetAnimeReviews(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Anime, id.ToString(), AnimeExtension.Reviews.GetDescription() };
			return await ExecuteGetRequestAsync<AnimeReviews>(endpointParts);
		}

		/// <summary>
		/// Returns collection of anime reviews.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <param name="page">Index of page folding 20 records (e.g. 1 will return first 20 records, 2 will return record from 21 to 40 etc.)</param>
		/// <returns>Collection of anime reviews.</returns>
		public async Task<AnimeReviews> GetAnimeReviews(long id, int page)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			Guard.IsGreaterThanZero(page, nameof(page));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Anime, id.ToString(), AnimeExtension.Reviews.GetDescription(), page.ToString() };
			return await ExecuteGetRequestAsync<AnimeReviews>(endpointParts);
		}

		#endregion GetAnimeReviews

		#region GetAnimeUserUpdates

		/// <summary>
		/// Returns collection of anime user updates.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <returns>Collection of anime user updates.</returns>
		public async Task<AnimeUserUpdates> GetAnimeUserUpdates(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Anime, id.ToString(), AnimeExtension.UserUpdates.GetDescription() };
			return await ExecuteGetRequestAsync<AnimeUserUpdates>(endpointParts);
		}

		/// <summary>
		/// Returns collection of anime user updates.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <param name="page">Index of page folding 75 records (e.g. 1 will return first 75 records, 2 will return record from 76 to 150 etc.)</param>
		/// <returns>Collection of anime user updates.</returns>
		public async Task<AnimeUserUpdates> GetAnimeUserUpdates(long id, int page)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			Guard.IsGreaterThanZero(page, nameof(page));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Anime, id.ToString(), AnimeExtension.UserUpdates.GetDescription(), page.ToString() };
			return await ExecuteGetRequestAsync<AnimeUserUpdates>(endpointParts);
		}

		#endregion GetAnimeUserUpdates

		#endregion Anime methods

		#region Character methods

		#region GetCharacter

		/// <summary>
		/// Return character with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of character.</param>
		/// <returns>Character with given MAL id.</returns>
		public async Task<Character> GetCharacter(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Character, id.ToString() };
			return await ExecuteGetRequestAsync<Character>(endpointParts);
		}

		#endregion GetCharacter

		#region GetCharacterPictures

		/// <summary>
		/// Return collections of links to pictures related to character with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of character.</param>
		/// <returns>Collections of links to pictures related to character with given MAL id.</returns>
		public async Task<CharacterPictures> GetCharacterPictures(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Character, id.ToString(), CharacterExtension.Pictures.GetDescription() };
			return await ExecuteGetRequestAsync<CharacterPictures>(endpointParts);
		}

		#endregion GetCharacterPictures

		#endregion Character methods

		#region Manga methods

		#region GetManga

		/// <summary>
		/// Return manga with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of manga.</param>
		/// <returns>Manga with given MAL id.</returns>
		public async Task<Manga> GetManga(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Manga, id.ToString() };
			return await ExecuteGetRequestAsync<Manga>(endpointParts);
		}

		#endregion GetManga

		#region GetMangaPictures

		/// <summary>
		/// Return collections of links to pictures related to manga with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of manga.</param>
		/// <returns>Collections of links to pictures related to manga with given MAL id.</returns>
		public async Task<MangaPictures> GetMangaPictures(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Manga, id.ToString(), MangaExtension.Pictures.GetDescription() };
			return await ExecuteGetRequestAsync<MangaPictures>(endpointParts);
		}

		#endregion GetMangaPictures

		#region GetMangaCharacters

		/// <summary>
		/// Return collections of characters appearing in manga with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of manga.</param>
		/// <returns>Collections of characters appearing in manga with given MAL id.</returns>
		public async Task<MangaCharacters> GetMangaCharacters(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Manga, id.ToString(), MangaExtension.Characters.GetDescription() };
			return await ExecuteGetRequestAsync<MangaCharacters>(endpointParts);
		}

		#endregion GetMangaCharacters

		#region GetMangaGenre

		/// <summary>
		/// Return information about manga genre.
		/// </summary>
		/// <param name="genreId">Id of the searched genre.</param>
		/// <returns>Information about manga genre</returns>
		public async Task<MangaGenre> GetMangaGenre(long genreId)
		{
			Guard.IsGreaterThanZero(genreId, nameof(genreId));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Genre, JikanEndPointCategoryConsts.Manga, genreId.ToString() };
			return await ExecuteGetRequestAsync<MangaGenre>(endpointParts);
		}

		/// <summary>
		/// Return information about manga genre.
		/// </summary>
		/// <param name="mangaGenre">Searched genre.</param>
		/// <returns>Information about manga genre</returns>
		public async Task<MangaGenre> GetMangaGenre(MangaGenreSearch mangaGenre)
		{
			Guard.IsValidEnum(mangaGenre, nameof(mangaGenre));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Genre, JikanEndPointCategoryConsts.Manga, mangaGenre.GetDescription() };
			return await ExecuteGetRequestAsync<MangaGenre>(endpointParts);
		}

		/// <summary>
		/// Return information about manga genre.
		/// </summary>
		/// <param name="genreId">Id of the searched genre.</param>
		/// <param name="page">Index of page folding 100 records of top ranging (e.g. 1 will return first 100 records, 2 will return record from 101 to 200 etc.)</param>
		/// <returns>Information about manga genre</returns>
		public async Task<MangaGenre> GetMangaGenre(long genreId, int page)
		{
			Guard.IsGreaterThanZero(genreId, nameof(genreId));
			Guard.IsGreaterThanZero(page, nameof(page));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Genre, JikanEndPointCategoryConsts.Manga, genreId.ToString(), page.ToString() };
			return await ExecuteGetRequestAsync<MangaGenre>(endpointParts);
		}

		/// <summary>
		/// Return information about manga genre.
		/// </summary>
		/// <param name="mangaGenre">Searched genre.</param>
		/// <param name="page">Index of page folding 100 records of top ranging (e.g. 1 will return first 100 records, 2 will return record from 101 to 200 etc.)</param>
		/// <returns>Information about manga genre</returns>
		public async Task<MangaGenre> GetMangaGenre(MangaGenreSearch mangaGenre, int page)
		{
			Guard.IsGreaterThanZero(page, nameof(page));
			Guard.IsValidEnum(mangaGenre, nameof(mangaGenre));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Genre, JikanEndPointCategoryConsts.Manga, mangaGenre.GetDescription(), page.ToString() };
			return await ExecuteGetRequestAsync<MangaGenre>(endpointParts);
		}

		#endregion GetMangaGenre

		#region GetMangaNews

		/// <summary>
		/// Return collections of news related to manga with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of manga.</param>
		/// <returns>Collections of news related to manga with given MAL id.</returns>
		public async Task<MangaNews> GetMangaNews(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Manga, id.ToString(), MangaExtension.News.GetDescription() };
			return await ExecuteGetRequestAsync<MangaNews>(endpointParts);
		}

		#endregion GetMangaNews

		#region GetMangaStatistics

		/// <summary>
		/// Return statistics related to manga with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of manga.</param>
		/// <returns>Statistics related to manga with given MAL id.</returns>
		public async Task<MangaStats> GetMangaStatistics(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Manga, id.ToString(), MangaExtension.Stats.GetDescription() };
			return await ExecuteGetRequestAsync<MangaStats>(endpointParts);
		}

		#endregion GetMangaStatistics

		#region GetMangaForumTopics

		/// <summary>
		/// Return collections of forum topics related to manga with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of manga.</param>
		/// <returns>Collections of forum topics related to manga with given MAL id.</returns>
		public async Task<ForumTopics> GetMangaForumTopics(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Manga, id.ToString(), MangaExtension.Forum.GetDescription() };
			return await ExecuteGetRequestAsync<ForumTopics>(endpointParts);
		}

		#endregion GetMangaForumTopics

		#region GetMangaMoreInfo

		/// <summary>
		/// Return additional information related to manga with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of manga.</param>
		/// <returns>Additional information related to manga with given MAL id.</returns>
		public async Task<MoreInfo> GetMangaMoreInfo(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Manga, id.ToString(), AnimeExtension.MoreInfo.GetDescription() };
			return await ExecuteGetRequestAsync<MoreInfo>(endpointParts);
		}

		#endregion GetMangaMoreInfo

		#region GetMangaRecommendations

		/// <summary>
		/// Returns collection of manga recommendation.
		/// </summary>
		/// <param name="id">MAL id of manga.</param>
		/// <returns>Collection of manga recomendation.</returns>
		public async Task<Recommendations> GetMangaRecommendations(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Manga, id.ToString(), MangaExtension.Recommendations.GetDescription() };
			return await ExecuteGetRequestAsync<Recommendations>(endpointParts);
		}

		#endregion GetMangaRecommendations

		#region GetMangaReviews

		/// <summary>
		/// Returns collection of manga reviews.
		/// </summary>
		/// <param name="id">MAL id of manga.</param>
		/// <returns>Collection of manga reviews.</returns>
		public async Task<MangaReviews> GetMangaReviews(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Manga, id.ToString(), MangaExtension.Reviews.GetDescription() };
			return await ExecuteGetRequestAsync<MangaReviews>(endpointParts);
		}

		/// <summary>
		/// Returns collection of manga reviews.
		/// </summary>
		/// <param name="id">MAL id of manga.</param>
		/// <param name="page">Index of page folding 20 records of top ranging (e.g. 1 will return first 20 records, 2 will return record from 21 to 40 etc.)</param>
		/// <returns>Collection of manga reviews.</returns>
		public async Task<MangaReviews> GetMangaReviews(long id, int page)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			Guard.IsGreaterThanZero(page, nameof(page));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Manga, id.ToString(), MangaExtension.Reviews.GetDescription(), page.ToString() };
			return await ExecuteGetRequestAsync<MangaReviews>(endpointParts);
		}

		#endregion GetMangaReviews

		#region GetMangaUserUpdates

		/// <summary>
		/// Returns collection of manga user updates.
		/// </summary>
		/// <param name="id">MAL id of manga.</param>
		/// <returns>Collection of manga user updates.</returns>
		public async Task<MangaUserUpdates> GetMangaUserUpdates(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Manga, id.ToString(), MangaExtension.UserUpdates.GetDescription() };
			return await ExecuteGetRequestAsync<MangaUserUpdates>(endpointParts);
		}

		/// <summary>
		/// Returns collection of manga user updates.
		/// </summary>
		/// <param name="id">MAL id of manga.</param>
		/// <param name="page">Index of page folding 75 records of top ranging (e.g. 1 will return first 75 records, 2 will return record from 76 to 150 etc.)</param>
		/// <returns>Collection of manga user updates.</returns>
		public async Task<MangaUserUpdates> GetMangaUserUpdates(long id, int page)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			Guard.IsGreaterThanZero(page, nameof(page));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Manga, id.ToString(), MangaExtension.UserUpdates.GetDescription(), page.ToString() };
			return await ExecuteGetRequestAsync<MangaUserUpdates>(endpointParts);
		}

		#endregion GetMangaUserUpdates

		#endregion Manga methods

		#region Person methods

		#region GetPerson

		/// <summary>
		/// Return person with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of person.</param>
		/// <returns>Person with given MAL id.</returns>
		public async Task<Person> GetPerson(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Person, id.ToString() };
			return await ExecuteGetRequestAsync<Person>(endpointParts);
		}

		#endregion GetPerson

		#region GetPersonPictures

		/// <summary>
		/// Return collections of links to pictures related to person with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of person.</param>
		/// <returns>Collections of links to pictures related to person with given MAL id.</returns>
		public async Task<PersonPictures> GetPersonPictures(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Person, id.ToString(), PersonExtension.Pictures.GetDescription() };
			return await ExecuteGetRequestAsync<PersonPictures>(endpointParts);
		}

		#endregion GetPersonPictures

		#endregion Person methods

		#region Schedule methods

		#region GetSchedule

		/// <summary>
		/// Return current season schedule.
		/// </summary>
		/// <returns>Current season schedule.</returns>
		public async Task<Schedule> GetSchedule()
		{
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Schedule };
			return await ExecuteGetRequestAsync<Schedule>(endpointParts);
		}

		/// <summary>
		/// Return current season schedule.
		/// </summary>
		/// <param name="scheduledDay">Scheduled day to filter by.</param>
		/// <returns>Current season schedule.</returns>
		public async Task<Schedule> GetSchedule(ScheduledDay scheduledDay)
		{
			Guard.IsValidEnum(scheduledDay, nameof(scheduledDay));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Schedule, scheduledDay.GetDescription() };
			return await ExecuteGetRequestAsync<Schedule>(endpointParts);
		}

		#endregion GetSchedule

		#endregion Schedule methods

		#region Season methods

		#region GetSeason

		/// <summary>
		/// Return current season preview.
		/// </summary>
		/// <returns>Current season preview.</returns>
		public async Task<AnimeSeason> GetSeason()
		{
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Season };
			return await ExecuteGetRequestAsync<AnimeSeason>(endpointParts);
		}

		/// <summary>
		/// Return season preview.
		/// </summary>
		/// <param name="year">Year of selected season.</param>
		/// <param name="season">Selected season.</param>
		/// <returns>Season preview.</returns>
		public async Task<AnimeSeason> GetSeason(int year, Season season)
		{
			Guard.IsValid(year => year >= 1000 && year < 10000, year, nameof(year));
			Guard.IsValidEnum(season, nameof(season));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Season, year.ToString(), season.GetDescription() };
			return await ExecuteGetRequestAsync<AnimeSeason>(endpointParts);
		}

		#endregion GetSeason

		#region GetSeasonArchive

		/// <summary>
		/// Return list of availaible season to query with <see cref="GetSeason(int, Season)"/>
		/// </summary>
		/// <returns></returns>
		public async Task<SeasonArchives> GetSeasonArchive()
		{
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Season, SeasonExtension.Archive.GetDescription() };
			return await ExecuteGetRequestAsync<SeasonArchives>(endpointParts);
		}

		#endregion GetSeasonArchive

		#region GetSeasonLater

		/// <summary>
		/// Return season preview for anime with undefined airing season (marked as "Later" on MAL).
		/// </summary>
		/// <returns>Season preview for anime with undefined airing date.</returns>
		public async Task<AnimeSeason> GetSeasonLater()
		{
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Season, SeasonExtension.Later.GetDescription() };
			return await ExecuteGetRequestAsync<AnimeSeason>(endpointParts);
		}

		#endregion GetSeasonLater

		#endregion Season methods

		#region Top methods

		#region GetAnimeTop

		/// <summary>
		/// Return list of top anime.
		/// </summary>
		/// <returns>List of top anime.</returns>
		public async Task<AnimeTop> GetAnimeTop()
		{
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.TopList, JikanEndPointCategoryConsts.Anime };
			return await ExecuteGetRequestAsync<AnimeTop>(endpointParts);
		}

		/// <summary>
		/// Return list of top anime.
		/// </summary>
		/// <param name="page">Indexx of page folding 50 records of top ranging (e.g. 1 will return first 50 records, 2 will return record from 51 to 100 etc.)</param>
		/// <returns>List of top anime.</returns>
		public async Task<AnimeTop> GetAnimeTop(int page)
		{
			Guard.IsGreaterThanZero(page, nameof(page));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.TopList, JikanEndPointCategoryConsts.Anime, page.ToString() };
			return await ExecuteGetRequestAsync<AnimeTop>(endpointParts);
		}

		/// <summary>
		/// Return list of top anime.
		/// </summary>
		/// <param name="extension">Extension for specific type of ranking.</param>
		/// <returns>List of top anime.</returns>
		public async Task<AnimeTop> GetAnimeTop(TopAnimeExtension extension)
		{
			Guard.IsValidEnum(extension, nameof(extension));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.TopList, JikanEndPointCategoryConsts.Anime, "1", extension.GetDescription() };
			return await ExecuteGetRequestAsync<AnimeTop>(endpointParts);
		}

		/// <summary>
		/// Return list of top anime.
		/// </summary>
		/// <param name="page">Indexx of page folding 50 records of top ranging (e.g. 1 will return first 50 records, 2 will return record from 51 to 100 etc.)</param>
		/// <param name="extension">Extension for specific type of ranking.</param>
		/// <returns>List of top anime.</returns>
		public async Task<AnimeTop> GetAnimeTop(int page, TopAnimeExtension extension = TopAnimeExtension.None)
		{
			Guard.IsGreaterThanZero(page, nameof(page));
			Guard.IsValidEnum(extension, nameof(extension));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.TopList, JikanEndPointCategoryConsts.Anime, page.ToString(), extension.GetDescription() };
			return await ExecuteGetRequestAsync<AnimeTop>(endpointParts);
		}

		#endregion GetAnimeTop

		#region GetMangaTop

		/// <summary>
		/// Return list of top manga.
		/// </summary>
		/// <returns>List of top manga.</returns>
		public async Task<MangaTop> GetMangaTop()
		{
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.TopList, JikanEndPointCategoryConsts.Manga };
			return await ExecuteGetRequestAsync<MangaTop>(endpointParts);
		}

		/// <summary>
		/// Return list of top manga.
		/// </summary>
		/// <param name="page">Indexx of page folding 50 records of top ranging (e.g. 1 will return first 50 records, 2 will return record from 51 to 100 etc.)</param>
		/// <returns>List of top manga.</returns>
		public async Task<MangaTop> GetMangaTop(int page)
		{
			Guard.IsGreaterThanZero(page, nameof(page));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.TopList, JikanEndPointCategoryConsts.Manga, page.ToString() };
			return await ExecuteGetRequestAsync<MangaTop>(endpointParts);
		}

		/// <summary>
		/// Return list of top manga.
		/// </summary>
		/// <param name="extension">Extension for specific type of ranking.</param>
		/// <returns>List of top manga.</returns>
		public async Task<MangaTop> GetMangaTop(TopMangaExtension extension)
		{
			Guard.IsValidEnum(extension, nameof(extension));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.TopList, JikanEndPointCategoryConsts.Manga, "1", extension.GetDescription() };
			return await ExecuteGetRequestAsync<MangaTop>(endpointParts);
		}

		/// <summary>
		/// Return list of top manga.
		/// </summary>
		/// <param name="page">Indexx of page folding 50 records of top ranging (e.g. 1 will return first 50 records, 2 will return record from 51 to 100 etc.)</param>
		/// <param name="extension">Extension for specific type of ranking.</param>
		/// <returns>List of top manga.</returns>
		public async Task<MangaTop> GetMangaTop(int page, TopMangaExtension extension = TopMangaExtension.None)
		{
			Guard.IsGreaterThanZero(page, nameof(page));
			Guard.IsValidEnum(extension, nameof(extension));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.TopList, JikanEndPointCategoryConsts.Manga, page.ToString(), extension.GetDescription() };
			return await ExecuteGetRequestAsync<MangaTop>(endpointParts);
		}

		#endregion GetMangaTop

		#region GetPeopleTop

		/// <summary>
		/// Return list of most popular people.
		/// </summary>
		/// <returns>List of most popular people.</returns>
		public async Task<PeopleTop> GetPeopleTop()
		{
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.TopList, JikanEndPointCategoryConsts.People };
			return await ExecuteGetRequestAsync<PeopleTop>(endpointParts);
		}

		/// <summary>
		/// Return list of most popular people.
		/// </summary>
		/// <param name="page">Indexx of page folding 50 records of top ranging (e.g. 1 will return first 50 records, 2 will return record from 51 to 100 etc.)</param>
		/// <returns>List of most popular people.</returns>
		public async Task<PeopleTop> GetPeopleTop(int page)
		{
			Guard.IsGreaterThanZero(page, nameof(page));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.TopList, JikanEndPointCategoryConsts.People, page.ToString() };
			return await ExecuteGetRequestAsync<PeopleTop>(endpointParts);
		}

		#endregion GetPeopleTop

		#region GetCharactersTop

		/// <summary>
		/// Return list of most popular characters.
		/// </summary>
		/// <returns>List of most popular characters.</returns>
		public async Task<CharactersTop> GetCharactersTop()
		{
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.TopList, JikanEndPointCategoryConsts.Characters };
			return await ExecuteGetRequestAsync<CharactersTop>(endpointParts);
		}

		/// <summary>
		/// Return list of most popular characters.
		/// </summary>
		/// <param name="page">Index of page folding 50 records of top ranging (e.g. 1 will return first 50 records, 2 will return record from 51 to 100 etc.)</param>
		/// <returns>List of most popular characters.</returns>
		public async Task<CharactersTop> GetCharactersTop(int page)
		{
			Guard.IsGreaterThanZero(page, nameof(page));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.TopList, JikanEndPointCategoryConsts.Characters, page.ToString() };
			return await ExecuteGetRequestAsync<CharactersTop>(endpointParts);
		}

		#endregion GetCharactersTop

		#endregion Top methods

		#region Producer methods

		#region GetProducer

		/// <summary>
		/// Returns information about producer with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of the producer.</param>
		/// <returns>Information about producer with given MAL id. </returns>
		public async Task<Producer> GetProducer(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Producer, id.ToString() };
			return await ExecuteGetRequestAsync<Producer>(endpointParts);
		}

		/// <summary>
		/// Returns information about producer with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of the producer.</param>
		/// <param name="page">Index of page folding 100 records of top ranging (e.g. 1 will return first 100 records, 2 will return record from 101 to 200 etc.)</param>
		/// <returns>Information about producer with given MAL id. </returns>
		public async Task<Producer> GetProducer(long id, int page)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			Guard.IsGreaterThanZero(page, nameof(page));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Producer, id.ToString(), page.ToString() };
			return await ExecuteGetRequestAsync<Producer>(endpointParts);
		}

		#endregion GetProducer

		#endregion Producer methods

		#region Magazine methods

		#region GetMagazine

		/// <summary>
		/// Returns information about magazine with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of the magazine.</param>
		/// <returns>Information about magazine with given MAL id. </returns>
		public async Task<Magazine> GetMagazine(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Magazine, id.ToString() };
			return await ExecuteGetRequestAsync<Magazine>(endpointParts);
		}

		/// <summary>
		/// Returns information about magazine with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of the magazine.</param>
		/// <param name="page">Index of page folding 100 records of top ranging (e.g. 1 will return first 100 records, 2 will return record from 101 to 200 etc.)</param>
		/// <returns>Information about magazine with given MAL id. </returns>
		public async Task<Magazine> GetMagazine(long id, int page)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			Guard.IsGreaterThanZero(page, nameof(page));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Magazine, id.ToString(), page.ToString() };
			return await ExecuteGetRequestAsync<Magazine>(endpointParts);
		}

		#endregion GetMagazine

		#endregion Magazine methods

		#region User methods

		#region GetUserProfile

		/// <summary>
		/// Returns information about user's profile with given username.
		/// </summary>
		/// <param name="username">Username.</param>
		/// <returns>Information about user's profile with given username.</returns>
		public async Task<UserProfile> GetUserProfile(string username)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.User, username, UserExtension.Profile.GetDescription() };
			return await ExecuteGetRequestAsync<UserProfile>(endpointParts);
		}

		#endregion GetUserProfile

		#region GetUserHistory

		/// <summary>
		/// Returns information about user's history with given username.
		/// </summary>
		/// <param name="username">Username.</param>
		/// <returns>Information about user's profile with given username.</returns>
		public async Task<UserHistory> GetUserHistory(string username)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.User, username, UserExtension.History.GetDescription() };
			return await ExecuteGetRequestAsync<UserHistory>(endpointParts);
		}

		/// <summary>
		/// Returns information about user's history with given username.
		/// </summary>
		/// <param name="username">Username.</param>
		/// <param name="historyExtension">Option to filter history.</param>
		/// <returns>Information about user's profile with given username.</returns>
		public async Task<UserHistory> GetUserHistory(string username, UserHistoryExtension historyExtension)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			Guard.IsValidEnum(historyExtension, nameof(historyExtension));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.User, username, UserExtension.History.GetDescription(), historyExtension.GetDescription() };
			return await ExecuteGetRequestAsync<UserHistory>(endpointParts);
		}

		#endregion GetUserHistory

		#region GetUserAnimeList

		/// <summary>
		/// Returns entries on user's anime list.
		/// </summary>
		/// <param name="username">Username.</param>
		/// <returns>Entries on user's anime list.</returns>
		public async Task<UserAnimeList> GetUserAnimeList(string username)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.User, username, UserExtension.AnimeList.GetDescription() };
			return await ExecuteGetRequestAsync<UserAnimeList>(endpointParts);
		}

		/// <summary>
		/// Returns entries on user's anime list.
		/// </summary>
		/// <param name="username">Username.</param>
		/// <param name="page">Index of page folding 300 records of top ranging (e.g. 1 will return first 300 records, 2 will return record from 301 to 600 etc.)</param>
		/// <returns>Entries on user's anime list.</returns>
		public async Task<UserAnimeList> GetUserAnimeList(string username, int page)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			Guard.IsGreaterThanZero(page, nameof(page));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.User, username, UserExtension.AnimeList.GetDescription(), UserAnimeListExtension.All.GetDescription(), page.ToString() };
			return await ExecuteGetRequestAsync<UserAnimeList>(endpointParts);
		}

		/// <summary>
		/// Returns entries on user's anime list.
		/// </summary>
		/// <param name="username">Username.</param>
		/// <param name="filter">Option to filter list.</param>
		/// <returns>Entries on user's anime list.</returns>
		public async Task<UserAnimeList> GetUserAnimeList(string username, UserAnimeListExtension filter)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			Guard.IsValidEnum(filter, nameof(filter));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.User, username, UserExtension.AnimeList.GetDescription(), filter.GetDescription() };
			return await ExecuteGetRequestAsync<UserAnimeList>(endpointParts);
		}

		/// <summary>
		/// Returns entries on user's anime list.
		/// </summary>
		/// <param name="username">Username.</param>
		/// <param name="filter">Option to filter list.</param>
		/// <param name="page">Index of page folding 300 records of top ranging (e.g. 1 will return first 300 records, 2 will return record from 301 to 600 etc.)</param>
		/// <returns>Entries on user's anime list.</returns>
		public async Task<UserAnimeList> GetUserAnimeList(string username, UserAnimeListExtension filter, int page)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			Guard.IsGreaterThanZero(page, nameof(page));
			Guard.IsValidEnum(filter, nameof(filter));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.User, username, UserExtension.AnimeList.GetDescription(), filter.GetDescription(), page.ToString() };
			return await ExecuteGetRequestAsync<UserAnimeList>(endpointParts);
		}

		/// <summary>
		/// Returns entries on user's anime list.
		/// </summary>
		/// <param name="username">Username.</param>
		/// <param name="searchConfig">Config to modify request input parameters.</param>
		/// <returns>Entries on user's anime list.</returns>
		public async Task<UserAnimeList> GetUserAnimeList(string username, UserListAnimeSearchConfig searchConfig)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			var query = string.Concat(UserExtension.AnimeList.GetDescription(), searchConfig.ConfigToString());
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.User, username, query };
			return await ExecuteGetRequestAsync<UserAnimeList>(endpointParts);
		}

		#endregion GetUserAnimeList

		#region GetUserMangaList

		/// <summary>
		/// Returns entries on user's manga list.
		/// </summary>
		/// <param name="username">Username.</param>
		/// <returns>Entries on user's manga list.</returns>
		public async Task<UserMangaList> GetUserMangaList(string username)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.User, username, UserExtension.MangaList.GetDescription() };
			return await ExecuteGetRequestAsync<UserMangaList>(endpointParts);
		}

		/// <summary>
		/// Returns entries on user's manga list.
		/// </summary>
		/// <param name="username">Username.</param>
		/// <param name="page">Index of page folding 300 records of top ranging (e.g. 1 will return first 300 records, 2 will return record from 301 to 600 etc.)</param>
		/// <returns>Entries on user's manga list.</returns>
		public async Task<UserMangaList> GetUserMangaList(string username, int page)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			Guard.IsGreaterThanZero(page, nameof(page));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.User, username, UserExtension.MangaList.GetDescription(), UserMangaListExtension.All.GetDescription(), page.ToString() };
			return await ExecuteGetRequestAsync<UserMangaList>(endpointParts);
		}

		/// <summary>
		/// Returns entries on user's manga list.
		/// </summary>
		/// <param name="username">Username.</param>
		/// <param name="filter">Option to filter list.</param>
		/// <returns>Entries on user's manga list.</returns>
		public async Task<UserMangaList> GetUserMangaList(string username, UserMangaListExtension filter)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			Guard.IsValidEnum(filter, nameof(filter));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.User, username, UserExtension.MangaList.GetDescription(), filter.GetDescription() };
			return await ExecuteGetRequestAsync<UserMangaList>(endpointParts);
		}

		/// <summary>
		/// Returns entries on user's manga list.
		/// </summary>
		/// <param name="username">Username.</param>
		/// <param name="filter">Option to filter list.</param>
		/// <param name="page">Index of page folding 300 records of top ranging (e.g. 1 will return first 300 records, 2 will return record from 301 to 600 etc.)</param>
		/// <returns>Entries on user's manga list.</returns>
		public async Task<UserMangaList> GetUserMangaList(string username, UserMangaListExtension filter, int page)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			Guard.IsGreaterThanZero(page, nameof(page));
			Guard.IsValidEnum(filter, nameof(filter));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.User, username, UserExtension.MangaList.GetDescription(), filter.GetDescription(), page.ToString() };
			return await ExecuteGetRequestAsync<UserMangaList>(endpointParts);
		}

		/// <summary>
		/// Returns entries on user's manga list.
		/// </summary>
		/// <param name="username">Username.</param>
		/// <param name="searchConfig">Config to modify request input parameters.</param>
		/// <returns>Entries on user's manga list.</returns>
		public async Task<UserMangaList> GetUserMangaList(string username, UserListMangaSearchConfig searchConfig)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			var query = string.Concat(UserExtension.MangaList.GetDescription(), searchConfig.ConfigToString());
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.User, username, query };
			return await ExecuteGetRequestAsync<UserMangaList>(endpointParts);
		}

		#endregion GetUserMangaList

		#region GetUserFriend

		/// <summary>
		/// Returns information about user's friends with given username.
		/// </summary>
		/// <param name="username">Username.</param>
		/// <returns>Information about user's friends with given username.</returns>
		public async Task<UserFriends> GetUserFriends(string username)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.User, username, UserExtension.Friends.GetDescription() };
			return await ExecuteGetRequestAsync<UserFriends>(endpointParts);
		}

		/// <summary>
		/// Returns information about user's friends with given username.
		/// </summary>
		/// <param name="username">Username.</param>
		/// <param name="page">Index of the page.</param>
		/// <returns>Information about user's friends with given username.</returns>
		public async Task<UserFriends> GetUserFriends(string username, int page)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			Guard.IsGreaterThanZero(page, nameof(page));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.User, username, UserExtension.Friends.GetDescription(), page.ToString() };
			return await ExecuteGetRequestAsync<UserFriends>(endpointParts);
		}

		#endregion GetUserFriend

		#endregion User methods

		#region Club Methods

		#region GetClub

		/// <summary>
		/// Return club's profile information.
		/// </summary>
		/// <param name="id">MAL id of the club.</param>
		/// <returns>Club's profile information.</returns>
		public async Task<Club> GetClub(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Club, id.ToString() };
			return await ExecuteGetRequestAsync<Club>(endpointParts);
		}

		#endregion GetClub

		#region GetClubMembers

		/// <summary>
		/// Return club's member list.
		/// </summary>
		/// <param name="id">MAL id of the club.</param>
		/// <returns>Club's member list.</returns>
		public async Task<ClubMembers> GetClubMembers(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Club, id.ToString(), ClubExtensions.Members.GetDescription() };
			return await ExecuteGetRequestAsync<ClubMembers>(endpointParts);
		}

		/// <summary>
		/// Return club's member list.
		/// </summary>
		/// <param name="id">MAL id of the club.</param>
		/// <param name="page">Index of page folding 36 records of top ranging (e.g. 1 will return first 36 records, 2 will return record from 37 to 72 etc.)</param>
		/// <returns>Club's member list.</returns>
		public async Task<ClubMembers> GetClubMembers(long id, int page)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			Guard.IsGreaterThanZero(page, nameof(page));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Club, id.ToString(), ClubExtensions.Members.GetDescription(), page.ToString() };
			return await ExecuteGetRequestAsync<ClubMembers>(endpointParts);
		}

		#endregion GetClubMembers

		#endregion Club Methods

		#region Search methods

		#region SearchAnime

		/// <summary>
		/// Return list of results related to search.
		/// </summary>
		/// <param name="query">Search query.</param>
		/// <returns>List of result related to search query.</returns>
		public async Task<AnimeSearchResult> SearchAnime(string query)
		{
			Guard.IsLongerThan2Characters(query, nameof(query));
			query = string.Concat(JikanEndPointCategoryConsts.Anime, "?q=", query.Replace(' ', '+'));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Search, query };
			return await ExecuteGetRequestAsync<AnimeSearchResult>(endpointParts);
		}

		/// <summary>
		/// Return list of results related to search.
		/// </summary>
		/// <param name="query">Search query.</param>
		/// <param name="page">Indexx of page folding 50 records of top ranging (e.g. 1 will return first 50 records, 2 will return record from 51 to 100 etc.)</param>
		/// <returns>List of result related to search query.</returns>
		public async Task<AnimeSearchResult> SearchAnime(string query, int page)
		{
			Guard.IsLongerThan2Characters(query, nameof(query));
			Guard.IsGreaterThanZero(page, nameof(page));
			query = string.Concat(JikanEndPointCategoryConsts.Anime, "/", page.ToString(), "?q=", query.Replace(' ', '+'));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Search, query };
			return await ExecuteGetRequestAsync<AnimeSearchResult>(endpointParts);
		}

		/// <summary>
		/// Return list of results related to search.
		/// </summary>
		/// <param name="query">Search query.</param>
		/// <param name="searchConfig">Additional configuration for advanced search.</param>
		/// <returns>List of result related to search query.</returns>
		public async Task<AnimeSearchResult> SearchAnime(string query, AnimeSearchConfig searchConfig)
		{
			Guard.IsLongerThan2Characters(query, nameof(query));
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			query = string.Concat(JikanEndPointCategoryConsts.Anime, "?q=", query.Replace(' ', '+'), "&", searchConfig.ConfigToString());
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Search, query };
			return await ExecuteGetRequestAsync<AnimeSearchResult>(endpointParts);
		}

		/// <summary>
		/// Returns list of results related to search.
		/// </summary>
		/// <param name="searchConfig">Additional configuration for advanced search.</param>
		/// <returns>List of result related to search query.</returns>
		public async Task<AnimeSearchResult> SearchAnime(AnimeSearchConfig searchConfig)
		{
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			var query = string.Concat(JikanEndPointCategoryConsts.Anime, "?", searchConfig.ConfigToString());
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Search, query };
			return await ExecuteGetRequestAsync<AnimeSearchResult>(endpointParts);
		}

		/// <summary>
		/// Returns list of results related to search.
		/// </summary>
		/// <param name="page">Index of page folding 50 records of top ranging (e.g. 1 will return first 50 records, 2 will return record from 51 to 100 etc.)</param>
		/// <param name="searchConfig">Additional configuration for advanced search.</param>
		/// <returns>List of result related to search query.</returns>
		public async Task<AnimeSearchResult> SearchAnime(AnimeSearchConfig searchConfig, int page)
		{
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			Guard.IsGreaterThanZero(page, nameof(page));
			var query = string.Concat(JikanEndPointCategoryConsts.Anime, "/", page.ToString(), "?", searchConfig.ConfigToString());
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Search, query };
			return await ExecuteGetRequestAsync<AnimeSearchResult>(endpointParts);
		}

		/// <summary>
		/// Return list of results related to search.
		/// </summary>
		/// <param name="query">Search query.</param>
		/// <param name="page">Indexx of page folding 50 records of top ranging (e.g. 1 will return first 50 records, 2 will return record from 51 to 100 etc.)</param>
		/// <param name="searchConfig">Additional configuration for advanced search.</param>
		/// <returns>List of result related to search query.</returns>
		public async Task<AnimeSearchResult> SearchAnime(string query, int page, AnimeSearchConfig searchConfig)
		{
			Guard.IsLongerThan2Characters(query, nameof(query));
			Guard.IsGreaterThanZero(page, nameof(page));
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			query = string.Concat(JikanEndPointCategoryConsts.Anime, "/", page.ToString(), "?q=", query.Replace(' ', '+'), "&", searchConfig.ConfigToString());
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Search, query };
			return await ExecuteGetRequestAsync<AnimeSearchResult>(endpointParts);
		}

		#endregion SearchAnime

		#region SearchManga

		/// <summary>
		/// Return list of results related to search.
		/// </summary>
		/// <param name="query">Search query.</param>
		/// <returns>List of result related to search query.</returns>
		public async Task<MangaSearchResult> SearchManga(string query)
		{
			Guard.IsLongerThan2Characters(query, nameof(query));
			query = string.Concat(JikanEndPointCategoryConsts.Manga, "?q=", query.Replace(' ', '+'));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Search, query };
			return await ExecuteGetRequestAsync<MangaSearchResult>(endpointParts);
		}

		/// <summary>
		/// Return list of results related to search.
		/// </summary>
		/// <param name="query">Search query.</param>
		/// <param name="page">Indexx of page folding 50 records of top ranging (e.g. 1 will return first 50 records, 2 will return record from 51 to 100 etc.)</param>
		/// <returns>List of result related to search query.</returns>
		public async Task<MangaSearchResult> SearchManga(string query, int page)
		{
			Guard.IsLongerThan2Characters(query, nameof(query));
			Guard.IsGreaterThanZero(page, nameof(page));
			query = string.Concat(JikanEndPointCategoryConsts.Manga, "/", page.ToString(), "?q=", query.Replace(' ', '+'));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Search, query };
			return await ExecuteGetRequestAsync<MangaSearchResult>(endpointParts);
		}

		/// <summary>
		/// Return list of results related to search.
		/// </summary>
		/// <param name="query">Search query.</param>
		/// <param name="searchConfig">Additional configuration for advanced search.</param>
		/// <returns>List of result related to search query.</returns>
		public async Task<MangaSearchResult> SearchManga(string query, MangaSearchConfig searchConfig)
		{
			Guard.IsLongerThan2Characters(query, nameof(query));
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			query = string.Concat(JikanEndPointCategoryConsts.Manga, "?q=", query.Replace(' ', '+'), "&", searchConfig.ConfigToString());
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Search, query };
			return await ExecuteGetRequestAsync<MangaSearchResult>(endpointParts);
		}

		/// <summary>
		/// Returns list of results related to search.
		/// </summary>
		/// <param name="searchConfig">Additional configuration for advanced search.</param>
		/// <returns>List of result related to search query.</returns>
		public async Task<MangaSearchResult> SearchManga(MangaSearchConfig searchConfig)
		{
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			var query = string.Concat(JikanEndPointCategoryConsts.Manga, "?", searchConfig.ConfigToString());
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Search, query };
			return await ExecuteGetRequestAsync<MangaSearchResult>(endpointParts);
		}

		/// <summary>
		/// Returns list of results related to search.
		/// </summary>
		/// <param name="page">Index of page folding 50 records of top ranging (e.g. 1 will return first 50 records, 2 will return record from 51 to 100 etc.)</param>
		/// <param name="searchConfig">Additional configuration for advanced search.</param>
		/// <returns>List of result related to search query.</returns>
		public async Task<MangaSearchResult> SearchManga(MangaSearchConfig searchConfig, int page)
		{
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			Guard.IsGreaterThanZero(page, nameof(page));
			var query = string.Concat(JikanEndPointCategoryConsts.Manga, "/", page.ToString(), "?", searchConfig.ConfigToString());
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Search, query };
			return await ExecuteGetRequestAsync<MangaSearchResult>(endpointParts);
		}

		/// <summary>
		/// Return list of results related to search.
		/// </summary>
		/// <param name="query">Search query.</param>
		/// <param name="page">Indexx of page folding 50 records of top ranging (e.g. 1 will return first 50 records, 2 will return record from 51 to 100 etc.)</param>
		/// <param name="searchConfig">Additional configuration for advanced search.</param>
		/// <returns>List of result related to search query.</returns>
		public async Task<MangaSearchResult> SearchManga(string query, int page, MangaSearchConfig searchConfig)
		{
			Guard.IsLongerThan2Characters(query, nameof(query));
			Guard.IsGreaterThanZero(page, nameof(page));
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			query = string.Concat(JikanEndPointCategoryConsts.Manga, "/", page.ToString(), "?q=", query.Replace(' ', '+'), "&", searchConfig.ConfigToString());
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Search, query };
			return await ExecuteGetRequestAsync<MangaSearchResult>(endpointParts);
		}

		#endregion SearchManga

		#region SearchPerson

		/// <summary>
		/// Return list of results related to search.
		/// </summary>
		/// <param name="query">Search query.</param>
		/// <returns>List of result related to search query.</returns>
		public async Task<PersonSearchResult> SearchPerson(string query)
		{
			Guard.IsLongerThan2Characters(query, nameof(query));
			query = string.Concat(JikanEndPointCategoryConsts.Person, "?q=", query.Replace(' ', '+'));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Search, query };
			return await ExecuteGetRequestAsync<PersonSearchResult>(endpointParts);
		}

		/// <summary>
		/// Return list of results related to search.
		/// </summary>
		/// <param name="query">Search query.</param>
		/// <param name="page">Indexx of page folding 50 records of top ranging (e.g. 1 will return first 50 records, 2 will return record from 51 to 100 etc.)</param>
		/// <returns>List of result related to search query.</returns>
		public async Task<PersonSearchResult> SearchPerson(string query, int page)
		{
			Guard.IsLongerThan2Characters(query, nameof(query));
			Guard.IsGreaterThanZero(page, nameof(page));
			query = string.Concat(JikanEndPointCategoryConsts.Person, "/", page.ToString(), "?q=", query.Replace(' ', '+'));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Search, query };
			return await ExecuteGetRequestAsync<PersonSearchResult>(endpointParts);
		}

		#endregion SearchPerson

		#region SearchCharacter

		/// <summary>
		/// Return list of results related to search.
		/// </summary>
		/// <param name="query">Search query.</param>
		/// <returns>List of result related to search query.</returns>
		public async Task<CharacterSearchResult> SearchCharacter(string query)
		{
			Guard.IsLongerThan2Characters(query, nameof(query));
			query = string.Concat(JikanEndPointCategoryConsts.Character, "?q=", query.Replace(' ', '+'));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Search, query };
			return await ExecuteGetRequestAsync<CharacterSearchResult>(endpointParts);
		}

		/// <summary>
		/// Return list of results related to search.
		/// </summary>
		/// <param name="query">Search query.</param>
		/// <param name="page">Indexx of page folding 50 records of top ranging (e.g. 1 will return first 50 records, 2 will return record from 51 to 100 etc.)</param>
		/// <returns>List of result related to search query.</returns>
		public async Task<CharacterSearchResult> SearchCharacter(string query, int page)
		{
			Guard.IsLongerThan2Characters(query, nameof(query));
			Guard.IsGreaterThanZero(page, nameof(page));
			query = string.Concat(JikanEndPointCategoryConsts.Character, "/", page.ToString(), "?q=", query.Replace(' ', '+'));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Search, query };
			return await ExecuteGetRequestAsync<CharacterSearchResult>(endpointParts);
		}

		#endregion SearchCharacter

		#endregion Search methods

		#region Metadata methods

		#region GetStatusMetadata

		/// <summary>
		/// Return Jikan REST API metadata - status.
		/// </summary>
		/// <returns>Jikan REST API metadata - status.</returns>
		public async Task<StatusMetadata> GetStatusMetadata()
		{
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Meta, "status" };
			return await ExecuteGetRequestAsync<StatusMetadata>(endpointParts);
		}

		#endregion GetStatusMetadata

		#endregion Metadata methods

		#endregion Public Methods
	}
}