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

		/// <inheritdoc />
		public async Task<BaseJikanResponse<Anime>> GetAnimeAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Anime, id.ToString() };
			return await ExecuteGetRequestAsync<BaseJikanResponse<Anime>>(endpointParts);
		}

		#endregion GetAnimeAsync

		#region GetAnimeCharactersAsync

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<AnimeCharacter>>> GetAnimeCharactersAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Anime, id.ToString(), AnimeExtension.Characters.GetDescription() };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<AnimeCharacter>>>(endpointParts);
		}

		#endregion GetAnimeCharactersAsync

		#region GetAnimeStaffAsync

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<AnimeStaffPosition>>> GetAnimeStaffAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Anime, id.ToString(), AnimeExtension.Staff.GetDescription() };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<AnimeStaffPosition>>>(endpointParts);
		}

		#endregion GetAnimeStaffAsync

		#region GetAnimeEpisodesAsync

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<AnimeEpisode>>> GetAnimeEpisodesAsync(long id, int page)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			Guard.IsGreaterThanZero(page, nameof(page));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Anime, id.ToString(), AnimeExtension.Episodes.GetDescription() + $"?page={page}" };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<AnimeEpisode>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<AnimeEpisode>>> GetAnimeEpisodesAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Anime, id.ToString(), AnimeExtension.Episodes.GetDescription() };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<AnimeEpisode>>>(endpointParts);
		}

		#endregion GetAnimeEpisodesAsync

		#region GetAnimeEpisodeAsync

		/// <inheritdoc />
		public async Task<BaseJikanResponse<AnimeEpisode>> GetAnimeEpisodeAsync(long animeId, int episodeId)
		{
			Guard.IsGreaterThanZero(animeId, nameof(animeId));
			Guard.IsGreaterThanZero(episodeId, nameof(episodeId));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Anime, animeId.ToString(), AnimeExtension.Episodes.GetDescription(), episodeId.ToString() };
			return await ExecuteGetRequestAsync<BaseJikanResponse<AnimeEpisode>>(endpointParts);
		}

		#endregion GetAnimeEpisodeAsync

		#region GetAnimeNewsAsync

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<News>>> GetAnimeNewsAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Anime, id.ToString(), AnimeExtension.News.GetDescription() };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<News>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<News>>> GetAnimeNewsAsync(long id, int page)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			Guard.IsGreaterThanZero(page, nameof(page));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Anime, id.ToString(), AnimeExtension.News.GetDescription() + $"?page={page}" };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<News>>>(endpointParts);
		}

		#endregion GetAnimeNewsAsync

		#region GetAnimeForumTopicsAsync

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<ForumTopic>>> GetAnimeForumTopicsAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Anime, id.ToString(), AnimeExtension.Forum.GetDescription() };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<ForumTopic>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<ForumTopic>>> GetAnimeForumTopicsAsync(long id, ForumTopicType type = ForumTopicType.All)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			Guard.IsValidEnum(type, nameof(type));

			var queryParams = $"?topic={type.GetDescription()}";
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Anime, id.ToString(), AnimeExtension.Forum.GetDescription() + queryParams };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<ForumTopic>>>(endpointParts);
		}

		#endregion GetAnimeForumTopicsAsync

		#region GetAnimeVideosAsync

		/// <inheritdoc />
		public async Task<BaseJikanResponse<AnimeVideos>> GetAnimeVideosAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Anime, id.ToString(), AnimeExtension.Videos.GetDescription() };
			return await ExecuteGetRequestAsync<BaseJikanResponse<AnimeVideos>>(endpointParts);
		}

		#endregion GetAnimeVideosAsync

		#region GetAnimeGenre

		/// <inheritdoc />
		public async Task<AnimeGenre> GetAnimeGenre(long genreId)
		{
			Guard.IsGreaterThanZero(genreId, nameof(genreId));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Genre, JikanEndPointCategoryConsts.Anime, genreId.ToString() };
			return await ExecuteGetRequestAsync<AnimeGenre>(endpointParts);
		}

		/// <inheritdoc />
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

		#region GetAnimePicturesAsync

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<ImagesSet>>> GetAnimePicturesAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Anime, id.ToString(), AnimeExtension.Pictures.GetDescription() };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<ImagesSet>>>(endpointParts);
		}

		#endregion GetAnimePicturesAsync

		#region GetAnimeStatisticsAsync

		/// <inheritdoc />
		public async Task<BaseJikanResponse<AnimeStatistics>> GetAnimeStatisticsAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Anime, id.ToString(), AnimeExtension.Statistics.GetDescription() };
			return await ExecuteGetRequestAsync<BaseJikanResponse<AnimeStatistics>>(endpointParts);
		}

		#endregion GetAnimeStatisticsAsync

		#region GetAnimeMoreInfoAsync

		/// <inheritdoc />
		public async Task<BaseJikanResponse<MoreInfo>> GetAnimeMoreInfoAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Anime, id.ToString(), AnimeExtension.MoreInfo.GetDescription() };
			return await ExecuteGetRequestAsync<BaseJikanResponse<MoreInfo>>(endpointParts);
		}

		#endregion GetAnimeMoreInfoAsync

		#region GetAnimeRecommendationsAsync

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<Recommendation>>> GetAnimeRecommendationsAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Anime, id.ToString(), AnimeExtension.Recommendations.GetDescription() };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<Recommendation>>>(endpointParts);
		}

		#endregion GetAnimeRecommendationsAsync

		#region GetAnimeUserUpdatesAsync

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<AnimeUserUpdate>>> GetAnimeUserUpdatesAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Anime, id.ToString(), AnimeExtension.UserUpdates.GetDescription() };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<AnimeUserUpdate>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<AnimeUserUpdate>>> GetAnimeUserUpdatesAsync(long id, int page)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			Guard.IsGreaterThanZero(page, nameof(page));

			var queryParams = $"?page={page}";
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Anime, id.ToString(), AnimeExtension.UserUpdates.GetDescription() + queryParams };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<AnimeUserUpdate>>>(endpointParts);
		}

		#endregion GetAnimeUserUpdatesAsync

		#region GetAnimeReviewsAsync

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<AnimeReview>>> GetAnimeReviewsAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Anime, id.ToString(), AnimeExtension.Reviews.GetDescription() };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<AnimeReview>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<AnimeReview>>> GetAnimeReviewsAsync(long id, int page)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			Guard.IsGreaterThanZero(page, nameof(page));

			var queryParams = $"?page={page}";
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Anime, id.ToString(), AnimeExtension.Reviews.GetDescription() + queryParams };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<AnimeReview>>>(endpointParts);
		}

		#endregion GetAnimeReviewsAsync

		#region GetAnimeRelationsAsync

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<RelatedEntry>>> GetAnimeRelationsAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Anime, id.ToString(), AnimeExtension.Relations.GetDescription() };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<RelatedEntry>>>(endpointParts);
		}

		#endregion GetAnimeRelationsAsync

		#region GetAnimeThemesAsync

		/// <summary>
		/// Returns collection of anime openings and endings.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <returns>Collection of anime openings and endings.</returns>
		public async Task<BaseJikanResponse<AnimeThemes>> GetAnimeThemesAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Anime, id.ToString(), AnimeExtension.Themes.GetDescription() };
			return await ExecuteGetRequestAsync<BaseJikanResponse<AnimeThemes>>(endpointParts);
		}

		#endregion GetAnimeThemesAsync

		#endregion Anime methods

		#region Character methods

		#region GetCharacter

		/// <inheritdoc />
		public async Task<Character> GetCharacter(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Character, id.ToString() };
			return await ExecuteGetRequestAsync<Character>(endpointParts);
		}

		#endregion GetCharacter

		#region GetCharacterPictures

		/// <inheritdoc />
		public async Task<CharacterPictures> GetCharacterPictures(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Character, id.ToString(), CharacterExtension.Pictures.GetDescription() };
			return await ExecuteGetRequestAsync<CharacterPictures>(endpointParts);
		}

		#endregion GetCharacterPictures

		#endregion Character methods

		#region Manga methods

		#region GetMangaAsync

		/// <inheritdoc />
		public async Task<BaseJikanResponse<Manga>> GetMangaAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Manga, id.ToString() };
			return await ExecuteGetRequestAsync<BaseJikanResponse<Manga>>(endpointParts);
		}

		#endregion GetMangaAsync

		#region GetMangaCharactersAsync

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<MangaCharacter>>> GetMangaCharactersAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Manga, id.ToString(), MangaExtension.Characters.GetDescription() };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<MangaCharacter>>>(endpointParts);
		}

		#endregion GetMangaCharactersAsync

		#region GetMangaNewsAsync

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<News>>> GetMangaNewsAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Manga, id.ToString(), MangaExtension.News.GetDescription() };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<News>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<News>>> GetMangaNewsAsync(long id, int page)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			Guard.IsGreaterThanZero(page, nameof(page));

			var queryParams = $"?page={page}";
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Manga, id.ToString(), MangaExtension.News.GetDescription() + queryParams };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<News>>>(endpointParts);
		}

		#endregion GetMangaNewsAsync

		#region GetMangaForumTopicsAsync

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<ForumTopic>>> GetMangaForumTopicsAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Manga, id.ToString(), MangaExtension.Forum.GetDescription() };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<ForumTopic>>>(endpointParts);
		}

		#endregion GetMangaForumTopicsAsync

		#region GetMangaPicturesAsync

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<ImagesSet>>> GetMangaPicturesAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Manga, id.ToString(), MangaExtension.Pictures.GetDescription() };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<ImagesSet>>>(endpointParts);
		}

		#endregion GetMangaPicturesAsync

		#region GetMangaGenre

		/// <inheritdoc />
		public async Task<MangaGenre> GetMangaGenre(long genreId)
		{
			Guard.IsGreaterThanZero(genreId, nameof(genreId));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Genre, JikanEndPointCategoryConsts.Manga, genreId.ToString() };
			return await ExecuteGetRequestAsync<MangaGenre>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<MangaGenre> GetMangaGenre(MangaGenreSearch mangaGenre)
		{
			Guard.IsValidEnum(mangaGenre, nameof(mangaGenre));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Genre, JikanEndPointCategoryConsts.Manga, mangaGenre.GetDescription() };
			return await ExecuteGetRequestAsync<MangaGenre>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<MangaGenre> GetMangaGenre(long genreId, int page)
		{
			Guard.IsGreaterThanZero(genreId, nameof(genreId));
			Guard.IsGreaterThanZero(page, nameof(page));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Genre, JikanEndPointCategoryConsts.Manga, genreId.ToString(), page.ToString() };
			return await ExecuteGetRequestAsync<MangaGenre>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<MangaGenre> GetMangaGenre(MangaGenreSearch mangaGenre, int page)
		{
			Guard.IsGreaterThanZero(page, nameof(page));
			Guard.IsValidEnum(mangaGenre, nameof(mangaGenre));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Genre, JikanEndPointCategoryConsts.Manga, mangaGenre.GetDescription(), page.ToString() };
			return await ExecuteGetRequestAsync<MangaGenre>(endpointParts);
		}

		#endregion GetMangaGenre

		#region GetMangaStatisticsAsync

		/// <inheritdoc />
		public async Task<BaseJikanResponse<MangaStatistics>> GetMangaStatisticsAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Manga, id.ToString(), MangaExtension.Statistics.GetDescription() };
			return await ExecuteGetRequestAsync<BaseJikanResponse<MangaStatistics>>(endpointParts);
		}

		#endregion GetMangaStatisticsAsync

		#region GetMangaMoreInfoAsync

		/// <inheritdoc />
		public async Task<BaseJikanResponse<MoreInfo>> GetMangaMoreInfoAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Manga, id.ToString(), AnimeExtension.MoreInfo.GetDescription() };
			return await ExecuteGetRequestAsync<BaseJikanResponse<MoreInfo>>(endpointParts);
		}

		#endregion GetMangaMoreInfoAsync

		#region GetMangaUserUpdatesAsync

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<MangaUserUpdate>>> GetMangaUserUpdatesAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Manga, id.ToString(), MangaExtension.UserUpdates.GetDescription() };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<MangaUserUpdate>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<MangaUserUpdate>>> GetMangaUserUpdatesAsync(long id, int page)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			Guard.IsGreaterThanZero(page, nameof(page));

			var queryParams = $"?page={page}";
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Manga, id.ToString(), MangaExtension.UserUpdates.GetDescription() + queryParams };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<MangaUserUpdate>>>(endpointParts);
		}

		#endregion GetMangaUserUpdatesAsync

		#region GetMangaRecommendationsAsync

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<Recommendation>>> GetMangaRecommendationsAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Manga, id.ToString(), MangaExtension.Recommendations.GetDescription() };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<Recommendation>>>(endpointParts);
		}

		#endregion GetMangaRecommendationsAsync

		#region GetMangaReviews

		/// <inheritdoc />
		public async Task<MangaReviews> GetMangaReviews(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Manga, id.ToString(), MangaExtension.Reviews.GetDescription() };
			return await ExecuteGetRequestAsync<MangaReviews>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<MangaReviews> GetMangaReviews(long id, int page)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			Guard.IsGreaterThanZero(page, nameof(page));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Manga, id.ToString(), MangaExtension.Reviews.GetDescription(), page.ToString() };
			return await ExecuteGetRequestAsync<MangaReviews>(endpointParts);
		}

		#endregion GetMangaReviews

		#endregion Manga methods

		#region Person methods

		#region GetPerson

		/// <inheritdoc />
		public async Task<Person> GetPerson(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Person, id.ToString() };
			return await ExecuteGetRequestAsync<Person>(endpointParts);
		}

		#endregion GetPerson

		#region GetPersonPictures

		/// <inheritdoc />
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

		/// <inheritdoc />
		public async Task<Schedule> GetSchedule()
		{
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Schedule };
			return await ExecuteGetRequestAsync<Schedule>(endpointParts);
		}

		/// <inheritdoc />
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

		/// <inheritdoc />
		public async Task<AnimeSeason> GetSeason()
		{
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Season };
			return await ExecuteGetRequestAsync<AnimeSeason>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<AnimeSeason> GetSeason(int year, Season season)
		{
			Guard.IsValid(year => year >= 1000 && year < 10000, year, nameof(year));
			Guard.IsValidEnum(season, nameof(season));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Season, year.ToString(), season.GetDescription() };
			return await ExecuteGetRequestAsync<AnimeSeason>(endpointParts);
		}

		#endregion GetSeason

		#region GetSeasonArchive

		/// <inheritdoc />
		public async Task<SeasonArchives> GetSeasonArchive()
		{
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Season, SeasonExtension.Archive.GetDescription() };
			return await ExecuteGetRequestAsync<SeasonArchives>(endpointParts);
		}

		#endregion GetSeasonArchive

		#region GetSeasonLater

		/// <inheritdoc />
		public async Task<AnimeSeason> GetSeasonLater()
		{
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Season, SeasonExtension.Later.GetDescription() };
			return await ExecuteGetRequestAsync<AnimeSeason>(endpointParts);
		}

		#endregion GetSeasonLater

		#endregion Season methods

		#region Top methods

		#region GetAnimeTop

		/// <inheritdoc />
		public async Task<AnimeTop> GetAnimeTop()
		{
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.TopList, JikanEndPointCategoryConsts.Anime };
			return await ExecuteGetRequestAsync<AnimeTop>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<AnimeTop> GetAnimeTop(int page)
		{
			Guard.IsGreaterThanZero(page, nameof(page));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.TopList, JikanEndPointCategoryConsts.Anime, page.ToString() };
			return await ExecuteGetRequestAsync<AnimeTop>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<AnimeTop> GetAnimeTop(TopAnimeExtension extension)
		{
			Guard.IsValidEnum(extension, nameof(extension));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.TopList, JikanEndPointCategoryConsts.Anime, "1", extension.GetDescription() };
			return await ExecuteGetRequestAsync<AnimeTop>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<AnimeTop> GetAnimeTop(int page, TopAnimeExtension extension = TopAnimeExtension.None)
		{
			Guard.IsGreaterThanZero(page, nameof(page));
			Guard.IsValidEnum(extension, nameof(extension));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.TopList, JikanEndPointCategoryConsts.Anime, page.ToString(), extension.GetDescription() };
			return await ExecuteGetRequestAsync<AnimeTop>(endpointParts);
		}

		#endregion GetAnimeTop

		#region GetMangaTop

		/// <inheritdoc />
		public async Task<MangaTop> GetMangaTop()
		{
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.TopList, JikanEndPointCategoryConsts.Manga };
			return await ExecuteGetRequestAsync<MangaTop>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<MangaTop> GetMangaTop(int page)
		{
			Guard.IsGreaterThanZero(page, nameof(page));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.TopList, JikanEndPointCategoryConsts.Manga, page.ToString() };
			return await ExecuteGetRequestAsync<MangaTop>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<MangaTop> GetMangaTop(TopMangaExtension extension)
		{
			Guard.IsValidEnum(extension, nameof(extension));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.TopList, JikanEndPointCategoryConsts.Manga, "1", extension.GetDescription() };
			return await ExecuteGetRequestAsync<MangaTop>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<MangaTop> GetMangaTop(int page, TopMangaExtension extension = TopMangaExtension.None)
		{
			Guard.IsGreaterThanZero(page, nameof(page));
			Guard.IsValidEnum(extension, nameof(extension));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.TopList, JikanEndPointCategoryConsts.Manga, page.ToString(), extension.GetDescription() };
			return await ExecuteGetRequestAsync<MangaTop>(endpointParts);
		}

		#endregion GetMangaTop

		#region GetPeopleTop

		/// <inheritdoc />
		public async Task<PeopleTop> GetPeopleTop()
		{
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.TopList, JikanEndPointCategoryConsts.People };
			return await ExecuteGetRequestAsync<PeopleTop>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<PeopleTop> GetPeopleTop(int page)
		{
			Guard.IsGreaterThanZero(page, nameof(page));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.TopList, JikanEndPointCategoryConsts.People, page.ToString() };
			return await ExecuteGetRequestAsync<PeopleTop>(endpointParts);
		}

		#endregion GetPeopleTop

		#region GetCharactersTop

		/// <inheritdoc />
		public async Task<CharactersTop> GetCharactersTop()
		{
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.TopList, JikanEndPointCategoryConsts.Characters };
			return await ExecuteGetRequestAsync<CharactersTop>(endpointParts);
		}

		/// <inheritdoc />
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

		/// <inheritdoc />
		public async Task<Producer> GetProducer(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Producer, id.ToString() };
			return await ExecuteGetRequestAsync<Producer>(endpointParts);
		}

		/// <inheritdoc />
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

		/// <inheritdoc />
		public async Task<Magazine> GetMagazine(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Magazine, id.ToString() };
			return await ExecuteGetRequestAsync<Magazine>(endpointParts);
		}

		/// <inheritdoc />
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

		/// <inheritdoc />
		public async Task<UserProfile> GetUserProfile(string username)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.User, username, UserExtension.Profile.GetDescription() };
			return await ExecuteGetRequestAsync<UserProfile>(endpointParts);
		}

		#endregion GetUserProfile

		#region GetUserHistory

		/// <inheritdoc />
		public async Task<UserHistory> GetUserHistory(string username)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.User, username, UserExtension.History.GetDescription() };
			return await ExecuteGetRequestAsync<UserHistory>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<UserHistory> GetUserHistory(string username, UserHistoryExtension historyExtension)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			Guard.IsValidEnum(historyExtension, nameof(historyExtension));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.User, username, UserExtension.History.GetDescription(), historyExtension.GetDescription() };
			return await ExecuteGetRequestAsync<UserHistory>(endpointParts);
		}

		#endregion GetUserHistory

		#region GetUserAnimeList

		/// <inheritdoc />
		public async Task<UserAnimeList> GetUserAnimeList(string username)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.User, username, UserExtension.AnimeList.GetDescription() };
			return await ExecuteGetRequestAsync<UserAnimeList>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<UserAnimeList> GetUserAnimeList(string username, int page)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			Guard.IsGreaterThanZero(page, nameof(page));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.User, username, UserExtension.AnimeList.GetDescription(), UserAnimeListExtension.All.GetDescription(), page.ToString() };
			return await ExecuteGetRequestAsync<UserAnimeList>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<UserAnimeList> GetUserAnimeList(string username, UserAnimeListExtension filter)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			Guard.IsValidEnum(filter, nameof(filter));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.User, username, UserExtension.AnimeList.GetDescription(), filter.GetDescription() };
			return await ExecuteGetRequestAsync<UserAnimeList>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<UserAnimeList> GetUserAnimeList(string username, UserAnimeListExtension filter, int page)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			Guard.IsGreaterThanZero(page, nameof(page));
			Guard.IsValidEnum(filter, nameof(filter));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.User, username, UserExtension.AnimeList.GetDescription(), filter.GetDescription(), page.ToString() };
			return await ExecuteGetRequestAsync<UserAnimeList>(endpointParts);
		}

		/// <inheritdoc />
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

		/// <inheritdoc />
		public async Task<UserMangaList> GetUserMangaList(string username)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.User, username, UserExtension.MangaList.GetDescription() };
			return await ExecuteGetRequestAsync<UserMangaList>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<UserMangaList> GetUserMangaList(string username, int page)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			Guard.IsGreaterThanZero(page, nameof(page));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.User, username, UserExtension.MangaList.GetDescription(), UserMangaListExtension.All.GetDescription(), page.ToString() };
			return await ExecuteGetRequestAsync<UserMangaList>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<UserMangaList> GetUserMangaList(string username, UserMangaListExtension filter)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			Guard.IsValidEnum(filter, nameof(filter));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.User, username, UserExtension.MangaList.GetDescription(), filter.GetDescription() };
			return await ExecuteGetRequestAsync<UserMangaList>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<UserMangaList> GetUserMangaList(string username, UserMangaListExtension filter, int page)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			Guard.IsGreaterThanZero(page, nameof(page));
			Guard.IsValidEnum(filter, nameof(filter));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.User, username, UserExtension.MangaList.GetDescription(), filter.GetDescription(), page.ToString() };
			return await ExecuteGetRequestAsync<UserMangaList>(endpointParts);
		}

		/// <inheritdoc />
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

		/// <inheritdoc />
		public async Task<UserFriends> GetUserFriends(string username)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.User, username, UserExtension.Friends.GetDescription() };
			return await ExecuteGetRequestAsync<UserFriends>(endpointParts);
		}

		/// <inheritdoc />
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

		/// <inheritdoc />
		public async Task<Club> GetClub(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Club, id.ToString() };
			return await ExecuteGetRequestAsync<Club>(endpointParts);
		}

		#endregion GetClub

		#region GetClubMembers

		/// <inheritdoc />
		public async Task<ClubMembers> GetClubMembers(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Club, id.ToString(), ClubExtensions.Members.GetDescription() };
			return await ExecuteGetRequestAsync<ClubMembers>(endpointParts);
		}

		/// <inheritdoc />
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

		/// <inheritdoc />
		public async Task<AnimeSearchResult> SearchAnime(string query)
		{
			Guard.IsLongerThan2Characters(query, nameof(query));
			query = string.Concat(JikanEndPointCategoryConsts.Anime, "?q=", query.Replace(' ', '+'));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Search, query };
			return await ExecuteGetRequestAsync<AnimeSearchResult>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<AnimeSearchResult> SearchAnime(string query, int page)
		{
			Guard.IsLongerThan2Characters(query, nameof(query));
			Guard.IsGreaterThanZero(page, nameof(page));
			query = string.Concat(JikanEndPointCategoryConsts.Anime, "/", page.ToString(), "?q=", query.Replace(' ', '+'));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Search, query };
			return await ExecuteGetRequestAsync<AnimeSearchResult>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<AnimeSearchResult> SearchAnime(string query, AnimeSearchConfig searchConfig)
		{
			Guard.IsLongerThan2Characters(query, nameof(query));
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			query = string.Concat(JikanEndPointCategoryConsts.Anime, "?q=", query.Replace(' ', '+'), "&", searchConfig.ConfigToString());
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Search, query };
			return await ExecuteGetRequestAsync<AnimeSearchResult>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<AnimeSearchResult> SearchAnime(AnimeSearchConfig searchConfig)
		{
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			var query = string.Concat(JikanEndPointCategoryConsts.Anime, "?", searchConfig.ConfigToString());
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Search, query };
			return await ExecuteGetRequestAsync<AnimeSearchResult>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<AnimeSearchResult> SearchAnime(AnimeSearchConfig searchConfig, int page)
		{
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			Guard.IsGreaterThanZero(page, nameof(page));
			var query = string.Concat(JikanEndPointCategoryConsts.Anime, "/", page.ToString(), "?", searchConfig.ConfigToString());
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Search, query };
			return await ExecuteGetRequestAsync<AnimeSearchResult>(endpointParts);
		}

		/// <inheritdoc />
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

		/// <inheritdoc />
		public async Task<MangaSearchResult> SearchManga(string query)
		{
			Guard.IsLongerThan2Characters(query, nameof(query));
			query = string.Concat(JikanEndPointCategoryConsts.Manga, "?q=", query.Replace(' ', '+'));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Search, query };
			return await ExecuteGetRequestAsync<MangaSearchResult>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<MangaSearchResult> SearchManga(string query, int page)
		{
			Guard.IsLongerThan2Characters(query, nameof(query));
			Guard.IsGreaterThanZero(page, nameof(page));
			query = string.Concat(JikanEndPointCategoryConsts.Manga, "/", page.ToString(), "?q=", query.Replace(' ', '+'));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Search, query };
			return await ExecuteGetRequestAsync<MangaSearchResult>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<MangaSearchResult> SearchManga(string query, MangaSearchConfig searchConfig)
		{
			Guard.IsLongerThan2Characters(query, nameof(query));
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			query = string.Concat(JikanEndPointCategoryConsts.Manga, "?q=", query.Replace(' ', '+'), "&", searchConfig.ConfigToString());
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Search, query };
			return await ExecuteGetRequestAsync<MangaSearchResult>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<MangaSearchResult> SearchManga(MangaSearchConfig searchConfig)
		{
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			var query = string.Concat(JikanEndPointCategoryConsts.Manga, "?", searchConfig.ConfigToString());
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Search, query };
			return await ExecuteGetRequestAsync<MangaSearchResult>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<MangaSearchResult> SearchManga(MangaSearchConfig searchConfig, int page)
		{
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			Guard.IsGreaterThanZero(page, nameof(page));
			var query = string.Concat(JikanEndPointCategoryConsts.Manga, "/", page.ToString(), "?", searchConfig.ConfigToString());
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Search, query };
			return await ExecuteGetRequestAsync<MangaSearchResult>(endpointParts);
		}

		/// <inheritdoc />
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

		/// <inheritdoc />
		public async Task<PersonSearchResult> SearchPerson(string query)
		{
			Guard.IsLongerThan2Characters(query, nameof(query));
			query = string.Concat(JikanEndPointCategoryConsts.Person, "?q=", query.Replace(' ', '+'));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Search, query };
			return await ExecuteGetRequestAsync<PersonSearchResult>(endpointParts);
		}

		/// <inheritdoc />
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

		/// <inheritdoc />
		public async Task<CharacterSearchResult> SearchCharacter(string query)
		{
			Guard.IsLongerThan2Characters(query, nameof(query));
			query = string.Concat(JikanEndPointCategoryConsts.Character, "?q=", query.Replace(' ', '+'));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Search, query };
			return await ExecuteGetRequestAsync<CharacterSearchResult>(endpointParts);
		}

		/// <inheritdoc />
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

		/// <inheritdoc />
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