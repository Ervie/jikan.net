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

		#region GetCharacterAsync

		/// <inheritdoc />
		public async Task<BaseJikanResponse<Character>> GetCharacterAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Characters, id.ToString() };
			return await ExecuteGetRequestAsync<BaseJikanResponse<Character>>(endpointParts);
		}

		#endregion GetCharacterAsync

		#region GetCharacterAnimeAsync

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<CharacterAnimeographyEntry>>> GetCharacterAnimeAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Characters, id.ToString(), CharacterExtension.Anime.GetDescription() };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<CharacterAnimeographyEntry>>>(endpointParts);
		}

		#endregion GetCharacterAnimeAsync

		#region GetCharacterMangaAsync

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<CharacterMangaographyEntry>>> GetCharacterMangaAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Characters, id.ToString(), CharacterExtension.Manga.GetDescription() };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<CharacterMangaographyEntry>>>(endpointParts);
		}

		#endregion GetCharacterMangaAsync

		#region GetCharacterVoiceActorsAsync

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<VoiceActorEntry>>> GetCharacterVoiceActorsAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Characters, id.ToString(), CharacterExtension.Voices.GetDescription() };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<VoiceActorEntry>>>(endpointParts);
		}

		#endregion GetCharacterVoiceActorsAsync

		#region GetCharacterPicturesAsync

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<ImagesSet>>> GetCharacterPicturesAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Characters, id.ToString(), CharacterExtension.Pictures.GetDescription() };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<ImagesSet>>>(endpointParts);
		}

		#endregion GetCharacterPicturesAsync

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

		#region GetMangaReviewsAsync

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<MangaReview>>> GetMangaReviewsAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Manga, id.ToString(), MangaExtension.Reviews.GetDescription() };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<MangaReview>>>(endpointParts);
		}

		#endregion GetMangaReviewsAsync

		#region GetMangaRelationsAsync

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<RelatedEntry>>> GetMangaRelationsAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Manga, id.ToString(), MangaExtension.Relations.GetDescription() };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<RelatedEntry>>>(endpointParts);
		}

		#endregion GetMangaRelationsAsync

		#endregion Manga methods

		#region Person methods

		#region GetPersonAsync

		/// <inheritdoc />
		public async Task<BaseJikanResponse<Person>> GetPersonAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.People, id.ToString() };
			return await ExecuteGetRequestAsync<BaseJikanResponse<Person>>(endpointParts);
		}

		#endregion GetPersonAsync

		#region GetPersonAnimeAsync

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<PersonAnimeographyEntry>>> GetPersonAnimeAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.People, id.ToString(), CharacterExtension.Anime.GetDescription() };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<PersonAnimeographyEntry>>>(endpointParts);
		}

		#endregion GetPersonAnimeAsync

		#region GetPersonMangaAsync

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<PersonMangaographyEntry>>> GetPersonMangaAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.People, id.ToString(), CharacterExtension.Manga.GetDescription() };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<PersonMangaographyEntry>>>(endpointParts);
		}

		#endregion GetPersonMangaAsync

		#region GetPersonVoiceActingRolesAsync

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<VoiceActingRole>>> GetPersonVoiceActingRolesAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.People, id.ToString(), CharacterExtension.Voices.GetDescription() };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<VoiceActingRole>>>(endpointParts);
		}

		#endregion GetPersonVoiceActingRolesAsync

		#region GetPersonPicturesAsync

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<ImagesSet>>> GetPersonPicturesAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.People, id.ToString(), PersonExtension.Pictures.GetDescription() };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<ImagesSet>>>(endpointParts);
		}

		#endregion GetPersonPicturesAsync

		#endregion Person methods

		#region Season methods

		#region GetSeasonAsync

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Anime>>> GetSeasonAsync(int year, Season season)
		{
			Guard.IsValid(year => year >= 1000 && year < 10000, year, nameof(year));
			Guard.IsValidEnum(season, nameof(season));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Seasons, year.ToString(), season.GetDescription() };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Anime>>>(endpointParts);
		}

		#endregion GetSeasonAsync

		#region GetSeasonArchiveAsync

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<SeasonArchive>>> GetSeasonArchiveAsync()
		{
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Seasons };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<SeasonArchive>>>(endpointParts);
		}

		#endregion GetSeasonArchiveAsync

		#region GetUpcomingSeasonAsync

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Anime>>> GetUpcomingSeasonAsync()
		{
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Seasons, SeasonExtension.Upcoming.GetDescription() };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Anime>>>(endpointParts);
		}

		#endregion GetUpcomingSeasonAsync

		#endregion Season methods

		#region Schedule methods

		#region GetScheduleAsync

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Anime>>> GetScheduleAsync()
		{
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Schedules };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Anime>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Anime>>> GetScheduleAsync(int page)
		{
			Guard.IsGreaterThanZero(page, nameof(page));
			var queryParams = $"?page={page}";
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Schedules + queryParams };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Anime>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Anime>>> GetScheduleAsync(ScheduledDay scheduledDay)
		{
			Guard.IsValidEnum(scheduledDay, nameof(scheduledDay));
			var queryParams = $"?topic={scheduledDay.GetDescription()}";
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Schedules + queryParams };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Anime>>>(endpointParts);
		}

		#endregion GetScheduleAsync

		#endregion Schedule methods

		#region Top methods

		#region GetTopAnimeAsync

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Anime>>> GetTopAnimeAsync()
		{
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.TopList, JikanEndPointCategoryConsts.Anime };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Anime>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Anime>>> GetTopAnimeAsync(int page)
		{
			Guard.IsGreaterThanZero(page, nameof(page));
			var queryParams = $"?page={page}";
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.TopList, JikanEndPointCategoryConsts.Anime + queryParams };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Anime>>>(endpointParts);
		}

		#endregion GetTopAnimeAsync

		#region GetTopMangaAsync

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Manga>>> GetTopMangaAsync()
		{
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.TopList, JikanEndPointCategoryConsts.Manga };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Manga>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Manga>>> GetTopMangaAsync(int page)
		{
			Guard.IsGreaterThanZero(page, nameof(page));
			var queryParams = $"?page={page}";
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.TopList, JikanEndPointCategoryConsts.Manga + queryParams };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Manga>>>(endpointParts);
		}

		#endregion GetTopMangaAsync

		#region GetTopPeopleAsync

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Person>>> GetTopPeopleAsync()
		{
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.TopList, JikanEndPointCategoryConsts.People };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Person>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Person>>> GetTopPeopleAsync(int page)
		{
			Guard.IsGreaterThanZero(page, nameof(page));
			var queryParams = $"?page={page}";
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.TopList, JikanEndPointCategoryConsts.People + queryParams };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Person>>>(endpointParts);
		}

		#endregion GetTopPeopleAsync

		#region GetTopCharactersAsync

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Character>>> GetTopCharactersAsync()
		{
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.TopList, JikanEndPointCategoryConsts.Characters };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Character>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Character>>> GetTopCharactersAsync(int page)
		{
			Guard.IsGreaterThanZero(page, nameof(page));
			var queryParams = $"?page={page}";
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.TopList, JikanEndPointCategoryConsts.Characters + queryParams };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Character>>>(endpointParts);
		}

		#endregion GetTopCharactersAsync

		#region GetTopReviewsAsync

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Review>>> GetTopReviewsAsync()
		{
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.TopList, JikanEndPointCategoryConsts.Reviews };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Review>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Review>>> GetTopReviewsAsync(int page)
		{
			Guard.IsGreaterThanZero(page, nameof(page));
			var queryParams = $"?page={page}";
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.TopList, JikanEndPointCategoryConsts.Reviews + queryParams };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Review>>>(endpointParts);
		}

		#endregion GetTopReviewsAsync

		#endregion Top methods

		#region Genre methods

		#region GetAnimeGenresAsync

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<Genre>>> GetAnimeGenresAsync()
		{
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Genres, JikanEndPointCategoryConsts.Anime };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<Genre>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<Genre>>> GetAnimeGenresAsync(GenresFilter filter)
		{
			Guard.IsValidEnum(filter, nameof(filter));
			var queryParams = $"?filter={filter.GetDescription()}";
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Genres, JikanEndPointCategoryConsts.Anime + queryParams };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<Genre>>>(endpointParts);
		}

		#endregion GetAnimeGenresAsync

		#region GetMangaGenresAsync

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<Genre>>> GetMangaGenresAsync()
		{
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Genres, JikanEndPointCategoryConsts.Manga };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<Genre>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<Genre>>> GetMangaGenresAsync(GenresFilter filter)
		{
			Guard.IsValidEnum(filter, nameof(filter));
			var queryParams = $"?filter={filter.GetDescription()}";
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Genres, JikanEndPointCategoryConsts.Manga + queryParams };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<Genre>>>(endpointParts);
		}

		#endregion GetMangaGenresAsync

		#endregion Genre methods

		#region Producer methods

		#region GetProducersAsync

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Producer>>> GetProducersAsync()
		{
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Producers };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Producer>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Producer>>> GetProducersAsync(int page)
		{
			Guard.IsGreaterThanZero(page, nameof(page));
			var queryParams = $"?page={page}";
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Producers + queryParams };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Producer>>>(endpointParts);
		}

		#endregion GetProducersAsync

		#endregion Producer methods

		#region Magazine methods

		#region GetMagazinesAsync

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Magazine>>> GetMagazinesAsync()
		{
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Magazines };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Magazine>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Magazine>>> GetMagazinesAsync(int page)
		{
			Guard.IsGreaterThanZero(page, nameof(page));
			var queryParams = $"?page={page}";
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Magazines + queryParams };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Magazine>>>(endpointParts);
		}

		#endregion GetMagazinesAsync

		#endregion Magazine methods

		#region Club methods

		#region GetClubAsync

		/// <inheritdoc />
		public async Task<BaseJikanResponse<Club>> GetClubAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Clubs, id.ToString() };
			return await ExecuteGetRequestAsync<BaseJikanResponse<Club>>(endpointParts);
		}

		#endregion GetClubAsync

		#region GetClubMembersAsync

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<ClubMember>>> GetClubMembersAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Clubs, id.ToString(), ClubExtensions.Members.GetDescription() };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<ClubMember>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<ClubMember>>> GetClubMembersAsync(long id, int page)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			Guard.IsGreaterThanZero(page, nameof(page));
			var queryParams = $"?page={page}";
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Clubs, id.ToString(), ClubExtensions.Members.GetDescription() + queryParams };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<ClubMember>>>(endpointParts);
		}

		#endregion GetClubMembersAsync

		#region GetClubStaffAsync

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<ClubStaff>>> GetClubStaffAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Clubs, id.ToString(), ClubExtensions.Staff.GetDescription() };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<ClubStaff>>>(endpointParts);
		}

		#endregion GetClubStaffAsync

		#region GetClubRelationsAsync

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ClubRelations>> GetClubRelationsAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Clubs, id.ToString(), ClubExtensions.Relations.GetDescription() };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ClubRelations>>(endpointParts);
		}

		#endregion GetClubRelationsAsync

		#endregion Club methods

		#region User methods

		#region GetUserProfileAsync

		/// <inheritdoc />
		public async Task<BaseJikanResponse<UserProfile>> GetUserProfileAsync(string username)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Users, username };
			return await ExecuteGetRequestAsync<BaseJikanResponse<UserProfile>>(endpointParts);
		}

		#endregion GetUserProfileAsync

		#region GetUserStatisticsAsync

		/// <inheritdoc />
		public async Task<BaseJikanResponse<UserStatistics>> GetUserStatisticsAsync(string username)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Users, username, UserExtension.Statistics.GetDescription() };
			return await ExecuteGetRequestAsync<BaseJikanResponse<UserStatistics>>(endpointParts);
		}

		#endregion GetUserStatisticsAsync

		#region GetUserFavoritesAsync

		/// <inheritdoc />
		public async Task<BaseJikanResponse<UserFavorites>> GetUserFavoritesAsync(string username)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Users, username, UserExtension.Favorites.GetDescription() };
			return await ExecuteGetRequestAsync<BaseJikanResponse<UserFavorites>>(endpointParts);
		}

		#endregion GetUserFavoritesAsync

		#region GetUserAboutAsync

		/// <inheritdoc />
		public async Task<BaseJikanResponse<UserAbout>> GetUserAboutAsync(string username)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Users, username, UserExtension.About.GetDescription() };
			return await ExecuteGetRequestAsync<BaseJikanResponse<UserAbout>>(endpointParts);
		}

		#endregion GetUserAboutAsync

		#region GetUserHistory

		/// <inheritdoc />
		public async Task<UserHistory> GetUserHistory(string username)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Users, username, UserExtension.History.GetDescription() };
			return await ExecuteGetRequestAsync<UserHistory>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<UserHistory> GetUserHistory(string username, UserHistoryExtension historyExtension)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			Guard.IsValidEnum(historyExtension, nameof(historyExtension));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Users, username, UserExtension.History.GetDescription(), historyExtension.GetDescription() };
			return await ExecuteGetRequestAsync<UserHistory>(endpointParts);
		}

		#endregion GetUserHistory

		#region GetUserAnimeList

		/// <inheritdoc />
		public async Task<UserAnimeList> GetUserAnimeList(string username)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Users, username, UserExtension.AnimeList.GetDescription() };
			return await ExecuteGetRequestAsync<UserAnimeList>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<UserAnimeList> GetUserAnimeList(string username, int page)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			Guard.IsGreaterThanZero(page, nameof(page));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Users, username, UserExtension.AnimeList.GetDescription(), UserAnimeListExtension.All.GetDescription(), page.ToString() };
			return await ExecuteGetRequestAsync<UserAnimeList>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<UserAnimeList> GetUserAnimeList(string username, UserAnimeListExtension filter)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			Guard.IsValidEnum(filter, nameof(filter));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Users, username, UserExtension.AnimeList.GetDescription(), filter.GetDescription() };
			return await ExecuteGetRequestAsync<UserAnimeList>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<UserAnimeList> GetUserAnimeList(string username, UserAnimeListExtension filter, int page)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			Guard.IsGreaterThanZero(page, nameof(page));
			Guard.IsValidEnum(filter, nameof(filter));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Users, username, UserExtension.AnimeList.GetDescription(), filter.GetDescription(), page.ToString() };
			return await ExecuteGetRequestAsync<UserAnimeList>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<UserAnimeList> GetUserAnimeList(string username, UserListAnimeSearchConfig searchConfig)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			var query = string.Concat(UserExtension.AnimeList.GetDescription(), searchConfig.ConfigToString());
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Users, username, query };
			return await ExecuteGetRequestAsync<UserAnimeList>(endpointParts);
		}

		#endregion GetUserAnimeList

		#region GetUserMangaList

		/// <inheritdoc />
		public async Task<UserMangaList> GetUserMangaList(string username)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Users, username, UserExtension.MangaList.GetDescription() };
			return await ExecuteGetRequestAsync<UserMangaList>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<UserMangaList> GetUserMangaList(string username, int page)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			Guard.IsGreaterThanZero(page, nameof(page));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Users, username, UserExtension.MangaList.GetDescription(), UserMangaListExtension.All.GetDescription(), page.ToString() };
			return await ExecuteGetRequestAsync<UserMangaList>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<UserMangaList> GetUserMangaList(string username, UserMangaListExtension filter)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			Guard.IsValidEnum(filter, nameof(filter));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Users, username, UserExtension.MangaList.GetDescription(), filter.GetDescription() };
			return await ExecuteGetRequestAsync<UserMangaList>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<UserMangaList> GetUserMangaList(string username, UserMangaListExtension filter, int page)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			Guard.IsGreaterThanZero(page, nameof(page));
			Guard.IsValidEnum(filter, nameof(filter));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Users, username, UserExtension.MangaList.GetDescription(), filter.GetDescription(), page.ToString() };
			return await ExecuteGetRequestAsync<UserMangaList>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<UserMangaList> GetUserMangaList(string username, UserListMangaSearchConfig searchConfig)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			var query = string.Concat(UserExtension.MangaList.GetDescription(), searchConfig.ConfigToString());
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Users, username, query };
			return await ExecuteGetRequestAsync<UserMangaList>(endpointParts);
		}

		#endregion GetUserMangaList

		#region GetUserFriend

		/// <inheritdoc />
		public async Task<UserFriends> GetUserFriends(string username)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Users, username, UserExtension.Friends.GetDescription() };
			return await ExecuteGetRequestAsync<UserFriends>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<UserFriends> GetUserFriends(string username, int page)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			Guard.IsGreaterThanZero(page, nameof(page));
			string[] endpointParts = new string[] { JikanEndPointCategoryConsts.Users, username, UserExtension.Friends.GetDescription(), page.ToString() };
			return await ExecuteGetRequestAsync<UserFriends>(endpointParts);
		}

		#endregion GetUserFriend

		#endregion User methods

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