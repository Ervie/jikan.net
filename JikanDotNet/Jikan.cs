using JikanDotNet.Config;
using JikanDotNet.Consts;
using JikanDotNet.Exceptions;
using JikanDotNet.Extensions;
using JikanDotNet.Helpers;
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
			string[] endpointParts = new string[] { JikanEndpointConsts.Anime, id.ToString() };
			return await ExecuteGetRequestAsync<BaseJikanResponse<Anime>>(endpointParts);
		}

		#endregion GetAnimeAsync

		#region GetAnimeCharactersAsync

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<AnimeCharacter>>> GetAnimeCharactersAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndpointConsts.Anime, id.ToString(), JikanEndpointConsts.Characters };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<AnimeCharacter>>>(endpointParts);
		}

		#endregion GetAnimeCharactersAsync

		#region GetAnimeStaffAsync

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<AnimeStaffPosition>>> GetAnimeStaffAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndpointConsts.Anime, id.ToString(), JikanEndpointConsts.Staff };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<AnimeStaffPosition>>>(endpointParts);
		}

		#endregion GetAnimeStaffAsync

		#region GetAnimeEpisodesAsync

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<AnimeEpisode>>> GetAnimeEpisodesAsync(long id, int page)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			Guard.IsGreaterThanZero(page, nameof(page));
			string[] endpointParts = new string[] { JikanEndpointConsts.Anime, id.ToString(), JikanEndpointConsts.Episodes + $"?page={page}" };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<AnimeEpisode>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<AnimeEpisode>>> GetAnimeEpisodesAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndpointConsts.Anime, id.ToString(), JikanEndpointConsts.Episodes };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<AnimeEpisode>>>(endpointParts);
		}

		#endregion GetAnimeEpisodesAsync

		#region GetAnimeEpisodeAsync

		/// <inheritdoc />
		public async Task<BaseJikanResponse<AnimeEpisode>> GetAnimeEpisodeAsync(long animeId, int episodeId)
		{
			Guard.IsGreaterThanZero(animeId, nameof(animeId));
			Guard.IsGreaterThanZero(episodeId, nameof(episodeId));
			string[] endpointParts = new string[] { JikanEndpointConsts.Anime, animeId.ToString(), JikanEndpointConsts.Episodes, episodeId.ToString() };
			return await ExecuteGetRequestAsync<BaseJikanResponse<AnimeEpisode>>(endpointParts);
		}

		#endregion GetAnimeEpisodeAsync

		#region GetAnimeNewsAsync

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<News>>> GetAnimeNewsAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndpointConsts.Anime, id.ToString(), JikanEndpointConsts.News };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<News>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<News>>> GetAnimeNewsAsync(long id, int page)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			Guard.IsGreaterThanZero(page, nameof(page));
			string[] endpointParts = new string[] { JikanEndpointConsts.Anime, id.ToString(), JikanEndpointConsts.News + $"?page={page}" };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<News>>>(endpointParts);
		}

		#endregion GetAnimeNewsAsync

		#region GetAnimeForumTopicsAsync

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<ForumTopic>>> GetAnimeForumTopicsAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndpointConsts.Anime, id.ToString(), JikanEndpointConsts.Forum };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<ForumTopic>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<ForumTopic>>> GetAnimeForumTopicsAsync(long id, ForumTopicType type = ForumTopicType.All)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			Guard.IsValidEnum(type, nameof(type));

			var queryParams = $"?topic={type.GetDescription()}";
			string[] endpointParts = new string[] { JikanEndpointConsts.Anime, id.ToString(), JikanEndpointConsts.Forum + queryParams };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<ForumTopic>>>(endpointParts);
		}

		#endregion GetAnimeForumTopicsAsync

		#region GetAnimeVideosAsync

		/// <inheritdoc />
		public async Task<BaseJikanResponse<AnimeVideos>> GetAnimeVideosAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndpointConsts.Anime, id.ToString(), JikanEndpointConsts.Videos };
			return await ExecuteGetRequestAsync<BaseJikanResponse<AnimeVideos>>(endpointParts);
		}

		#endregion GetAnimeVideosAsync

		#region GetAnimePicturesAsync

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<ImagesSet>>> GetAnimePicturesAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndpointConsts.Anime, id.ToString(), JikanEndpointConsts.Pictures };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<ImagesSet>>>(endpointParts);
		}

		#endregion GetAnimePicturesAsync

		#region GetAnimeStatisticsAsync

		/// <inheritdoc />
		public async Task<BaseJikanResponse<AnimeStatistics>> GetAnimeStatisticsAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndpointConsts.Anime, id.ToString(), JikanEndpointConsts.Statistics };
			return await ExecuteGetRequestAsync<BaseJikanResponse<AnimeStatistics>>(endpointParts);
		}

		#endregion GetAnimeStatisticsAsync

		#region GetAnimeMoreInfoAsync

		/// <inheritdoc />
		public async Task<BaseJikanResponse<MoreInfo>> GetAnimeMoreInfoAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndpointConsts.Anime, id.ToString(), JikanEndpointConsts.MoreInfo };
			return await ExecuteGetRequestAsync<BaseJikanResponse<MoreInfo>>(endpointParts);
		}

		#endregion GetAnimeMoreInfoAsync

		#region GetAnimeRecommendationsAsync

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<Recommendation>>> GetAnimeRecommendationsAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndpointConsts.Anime, id.ToString(), JikanEndpointConsts.Recommendations };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<Recommendation>>>(endpointParts);
		}

		#endregion GetAnimeRecommendationsAsync

		#region GetAnimeUserUpdatesAsync

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<AnimeUserUpdate>>> GetAnimeUserUpdatesAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndpointConsts.Anime, id.ToString(), JikanEndpointConsts.UserUpdates };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<AnimeUserUpdate>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<AnimeUserUpdate>>> GetAnimeUserUpdatesAsync(long id, int page)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			Guard.IsGreaterThanZero(page, nameof(page));

			var queryParams = $"?page={page}";
			string[] endpointParts = new string[] { JikanEndpointConsts.Anime, id.ToString(), JikanEndpointConsts.UserUpdates + queryParams };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<AnimeUserUpdate>>>(endpointParts);
		}

		#endregion GetAnimeUserUpdatesAsync

		#region GetAnimeReviewsAsync

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<AnimeReview>>> GetAnimeReviewsAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndpointConsts.Anime, id.ToString(), JikanEndpointConsts.Reviews };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<AnimeReview>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<AnimeReview>>> GetAnimeReviewsAsync(long id, int page)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			Guard.IsGreaterThanZero(page, nameof(page));

			var queryParams = $"?page={page}";
			string[] endpointParts = new string[] { JikanEndpointConsts.Anime, id.ToString(), JikanEndpointConsts.Reviews + queryParams };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<AnimeReview>>>(endpointParts);
		}

		#endregion GetAnimeReviewsAsync

		#region GetAnimeRelationsAsync

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<RelatedEntry>>> GetAnimeRelationsAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndpointConsts.Anime, id.ToString(), JikanEndpointConsts.Relations };
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
			string[] endpointParts = new string[] { JikanEndpointConsts.Anime, id.ToString(), JikanEndpointConsts.Themes };
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
			string[] endpointParts = new string[] { JikanEndpointConsts.Characters, id.ToString() };
			return await ExecuteGetRequestAsync<BaseJikanResponse<Character>>(endpointParts);
		}

		#endregion GetCharacterAsync

		#region GetCharacterAnimeAsync

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<CharacterAnimeographyEntry>>> GetCharacterAnimeAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndpointConsts.Characters, id.ToString(), JikanEndpointConsts.Anime };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<CharacterAnimeographyEntry>>>(endpointParts);
		}

		#endregion GetCharacterAnimeAsync

		#region GetCharacterMangaAsync

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<CharacterMangaographyEntry>>> GetCharacterMangaAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndpointConsts.Characters, id.ToString(), JikanEndpointConsts.Manga };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<CharacterMangaographyEntry>>>(endpointParts);
		}

		#endregion GetCharacterMangaAsync

		#region GetCharacterVoiceActorsAsync

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<VoiceActorEntry>>> GetCharacterVoiceActorsAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndpointConsts.Characters, id.ToString(), JikanEndpointConsts.Voices };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<VoiceActorEntry>>>(endpointParts);
		}

		#endregion GetCharacterVoiceActorsAsync

		#region GetCharacterPicturesAsync

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<ImagesSet>>> GetCharacterPicturesAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndpointConsts.Characters, id.ToString(), JikanEndpointConsts.Pictures };
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
			string[] endpointParts = new string[] { JikanEndpointConsts.Manga, id.ToString() };
			return await ExecuteGetRequestAsync<BaseJikanResponse<Manga>>(endpointParts);
		}

		#endregion GetMangaAsync

		#region GetMangaCharactersAsync

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<MangaCharacter>>> GetMangaCharactersAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndpointConsts.Manga, id.ToString(), JikanEndpointConsts.Characters };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<MangaCharacter>>>(endpointParts);
		}

		#endregion GetMangaCharactersAsync

		#region GetMangaNewsAsync

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<News>>> GetMangaNewsAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndpointConsts.Manga, id.ToString(), JikanEndpointConsts.News };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<News>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<News>>> GetMangaNewsAsync(long id, int page)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			Guard.IsGreaterThanZero(page, nameof(page));

			var queryParams = $"?page={page}";
			string[] endpointParts = new string[] { JikanEndpointConsts.Manga, id.ToString(), JikanEndpointConsts.News + queryParams };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<News>>>(endpointParts);
		}

		#endregion GetMangaNewsAsync

		#region GetMangaForumTopicsAsync

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<ForumTopic>>> GetMangaForumTopicsAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndpointConsts.Manga, id.ToString(), JikanEndpointConsts.Forum };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<ForumTopic>>>(endpointParts);
		}

		#endregion GetMangaForumTopicsAsync

		#region GetMangaPicturesAsync

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<ImagesSet>>> GetMangaPicturesAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndpointConsts.Manga, id.ToString(), JikanEndpointConsts.Pictures };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<ImagesSet>>>(endpointParts);
		}

		#endregion GetMangaPicturesAsync

		#region GetMangaStatisticsAsync

		/// <inheritdoc />
		public async Task<BaseJikanResponse<MangaStatistics>> GetMangaStatisticsAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndpointConsts.Manga, id.ToString(), JikanEndpointConsts.Statistics };
			return await ExecuteGetRequestAsync<BaseJikanResponse<MangaStatistics>>(endpointParts);
		}

		#endregion GetMangaStatisticsAsync

		#region GetMangaMoreInfoAsync

		/// <inheritdoc />
		public async Task<BaseJikanResponse<MoreInfo>> GetMangaMoreInfoAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndpointConsts.Manga, id.ToString(), JikanEndpointConsts.MoreInfo };
			return await ExecuteGetRequestAsync<BaseJikanResponse<MoreInfo>>(endpointParts);
		}

		#endregion GetMangaMoreInfoAsync

		#region GetMangaUserUpdatesAsync

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<MangaUserUpdate>>> GetMangaUserUpdatesAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndpointConsts.Manga, id.ToString(), JikanEndpointConsts.UserUpdates };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<MangaUserUpdate>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<MangaUserUpdate>>> GetMangaUserUpdatesAsync(long id, int page)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			Guard.IsGreaterThanZero(page, nameof(page));

			var queryParams = $"?page={page}";
			string[] endpointParts = new string[] { JikanEndpointConsts.Manga, id.ToString(), JikanEndpointConsts.UserUpdates + queryParams };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<MangaUserUpdate>>>(endpointParts);
		}

		#endregion GetMangaUserUpdatesAsync

		#region GetMangaRecommendationsAsync

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<Recommendation>>> GetMangaRecommendationsAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndpointConsts.Manga, id.ToString(), JikanEndpointConsts.Recommendations };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<Recommendation>>>(endpointParts);
		}

		#endregion GetMangaRecommendationsAsync

		#region GetMangaReviewsAsync

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<MangaReview>>> GetMangaReviewsAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndpointConsts.Manga, id.ToString(), JikanEndpointConsts.Reviews };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<MangaReview>>>(endpointParts);
		}

		#endregion GetMangaReviewsAsync

		#region GetMangaRelationsAsync

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<RelatedEntry>>> GetMangaRelationsAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndpointConsts.Manga, id.ToString(), JikanEndpointConsts.Relations };
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
			string[] endpointParts = new string[] { JikanEndpointConsts.People, id.ToString() };
			return await ExecuteGetRequestAsync<BaseJikanResponse<Person>>(endpointParts);
		}

		#endregion GetPersonAsync

		#region GetPersonAnimeAsync

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<PersonAnimeographyEntry>>> GetPersonAnimeAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndpointConsts.People, id.ToString(), JikanEndpointConsts.Anime };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<PersonAnimeographyEntry>>>(endpointParts);
		}

		#endregion GetPersonAnimeAsync

		#region GetPersonMangaAsync

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<PersonMangaographyEntry>>> GetPersonMangaAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndpointConsts.People, id.ToString(), JikanEndpointConsts.Manga };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<PersonMangaographyEntry>>>(endpointParts);
		}

		#endregion GetPersonMangaAsync

		#region GetPersonVoiceActingRolesAsync

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<VoiceActingRole>>> GetPersonVoiceActingRolesAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndpointConsts.People, id.ToString(), JikanEndpointConsts.Voices };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<VoiceActingRole>>>(endpointParts);
		}

		#endregion GetPersonVoiceActingRolesAsync

		#region GetPersonPicturesAsync

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<ImagesSet>>> GetPersonPicturesAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndpointConsts.People, id.ToString(), JikanEndpointConsts.Pictures };
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
			string[] endpointParts = new string[] { JikanEndpointConsts.Seasons, year.ToString(), season.GetDescription() };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Anime>>>(endpointParts);
		}

		#endregion GetSeasonAsync

		#region GetSeasonArchiveAsync

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<SeasonArchive>>> GetSeasonArchiveAsync()
		{
			string[] endpointParts = new string[] { JikanEndpointConsts.Seasons };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<SeasonArchive>>>(endpointParts);
		}

		#endregion GetSeasonArchiveAsync

		#region GetUpcomingSeasonAsync

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Anime>>> GetUpcomingSeasonAsync()
		{
			string[] endpointParts = new string[] { JikanEndpointConsts.Seasons, JikanEndpointConsts.Upcoming };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Anime>>>(endpointParts);
		}

		#endregion GetUpcomingSeasonAsync

		#endregion Season methods

		#region Schedule methods

		#region GetScheduleAsync

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Anime>>> GetScheduleAsync()
		{
			string[] endpointParts = new string[] { JikanEndpointConsts.Schedules };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Anime>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Anime>>> GetScheduleAsync(int page)
		{
			Guard.IsGreaterThanZero(page, nameof(page));
			var queryParams = $"?page={page}";
			string[] endpointParts = new string[] { JikanEndpointConsts.Schedules + queryParams };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Anime>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Anime>>> GetScheduleAsync(ScheduledDay scheduledDay)
		{
			Guard.IsValidEnum(scheduledDay, nameof(scheduledDay));
			var queryParams = $"?topic={scheduledDay.GetDescription()}";
			string[] endpointParts = new string[] { JikanEndpointConsts.Schedules + queryParams };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Anime>>>(endpointParts);
		}

		#endregion GetScheduleAsync

		#endregion Schedule methods

		#region Top methods

		#region GetTopAnimeAsync

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Anime>>> GetTopAnimeAsync()
		{
			string[] endpointParts = new string[] { JikanEndpointConsts.TopList, JikanEndpointConsts.Anime };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Anime>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Anime>>> GetTopAnimeAsync(int page)
		{
			Guard.IsGreaterThanZero(page, nameof(page));
			var queryParams = $"?page={page}";
			string[] endpointParts = new string[] { JikanEndpointConsts.TopList, JikanEndpointConsts.Anime + queryParams };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Anime>>>(endpointParts);
		}

		#endregion GetTopAnimeAsync

		#region GetTopMangaAsync

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Manga>>> GetTopMangaAsync()
		{
			string[] endpointParts = new string[] { JikanEndpointConsts.TopList, JikanEndpointConsts.Manga };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Manga>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Manga>>> GetTopMangaAsync(int page)
		{
			Guard.IsGreaterThanZero(page, nameof(page));
			var queryParams = $"?page={page}";
			string[] endpointParts = new string[] { JikanEndpointConsts.TopList, JikanEndpointConsts.Manga + queryParams };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Manga>>>(endpointParts);
		}

		#endregion GetTopMangaAsync

		#region GetTopPeopleAsync

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Person>>> GetTopPeopleAsync()
		{
			string[] endpointParts = new string[] { JikanEndpointConsts.TopList, JikanEndpointConsts.People };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Person>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Person>>> GetTopPeopleAsync(int page)
		{
			Guard.IsGreaterThanZero(page, nameof(page));
			var queryParams = $"?page={page}";
			string[] endpointParts = new string[] { JikanEndpointConsts.TopList, JikanEndpointConsts.People + queryParams };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Person>>>(endpointParts);
		}

		#endregion GetTopPeopleAsync

		#region GetTopCharactersAsync

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Character>>> GetTopCharactersAsync()
		{
			string[] endpointParts = new string[] { JikanEndpointConsts.TopList, JikanEndpointConsts.Characters };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Character>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Character>>> GetTopCharactersAsync(int page)
		{
			Guard.IsGreaterThanZero(page, nameof(page));
			var queryParams = $"?page={page}";
			string[] endpointParts = new string[] { JikanEndpointConsts.TopList, JikanEndpointConsts.Characters + queryParams };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Character>>>(endpointParts);
		}

		#endregion GetTopCharactersAsync

		#region GetTopReviewsAsync

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Review>>> GetTopReviewsAsync()
		{
			string[] endpointParts = new string[] { JikanEndpointConsts.TopList, JikanEndpointConsts.Reviews };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Review>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Review>>> GetTopReviewsAsync(int page)
		{
			Guard.IsGreaterThanZero(page, nameof(page));
			var queryParams = $"?page={page}";
			string[] endpointParts = new string[] { JikanEndpointConsts.TopList, JikanEndpointConsts.Reviews + queryParams };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Review>>>(endpointParts);
		}

		#endregion GetTopReviewsAsync

		#endregion Top methods

		#region Genre methods

		#region GetAnimeGenresAsync

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<Genre>>> GetAnimeGenresAsync()
		{
			string[] endpointParts = new string[] { JikanEndpointConsts.Genres, JikanEndpointConsts.Anime };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<Genre>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<Genre>>> GetAnimeGenresAsync(GenresFilter filter)
		{
			Guard.IsValidEnum(filter, nameof(filter));
			var queryParams = $"?filter={filter.GetDescription()}";
			string[] endpointParts = new string[] { JikanEndpointConsts.Genres, JikanEndpointConsts.Anime + queryParams };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<Genre>>>(endpointParts);
		}

		#endregion GetAnimeGenresAsync

		#region GetMangaGenresAsync

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<Genre>>> GetMangaGenresAsync()
		{
			string[] endpointParts = new string[] { JikanEndpointConsts.Genres, JikanEndpointConsts.Manga };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<Genre>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<Genre>>> GetMangaGenresAsync(GenresFilter filter)
		{
			Guard.IsValidEnum(filter, nameof(filter));
			var queryParams = $"?filter={filter.GetDescription()}";
			string[] endpointParts = new string[] { JikanEndpointConsts.Genres, JikanEndpointConsts.Manga + queryParams };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<Genre>>>(endpointParts);
		}

		#endregion GetMangaGenresAsync

		#endregion Genre methods

		#region Producer methods

		#region GetProducersAsync

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Producer>>> GetProducersAsync()
		{
			string[] endpointParts = new string[] { JikanEndpointConsts.Producers };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Producer>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Producer>>> GetProducersAsync(int page)
		{
			Guard.IsGreaterThanZero(page, nameof(page));
			var queryParams = $"?page={page}";
			string[] endpointParts = new string[] { JikanEndpointConsts.Producers + queryParams };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Producer>>>(endpointParts);
		}

		#endregion GetProducersAsync

		#endregion Producer methods

		#region Magazine methods

		#region GetMagazinesAsync

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Magazine>>> GetMagazinesAsync()
		{
			string[] endpointParts = new string[] { JikanEndpointConsts.Magazines };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Magazine>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Magazine>>> GetMagazinesAsync(int page)
		{
			Guard.IsGreaterThanZero(page, nameof(page));
			var queryParams = $"?page={page}";
			string[] endpointParts = new string[] { JikanEndpointConsts.Magazines + queryParams };
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
			string[] endpointParts = new string[] { JikanEndpointConsts.Clubs, id.ToString() };
			return await ExecuteGetRequestAsync<BaseJikanResponse<Club>>(endpointParts);
		}

		#endregion GetClubAsync

		#region GetClubMembersAsync

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<ClubMember>>> GetClubMembersAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndpointConsts.Clubs, id.ToString(), JikanEndpointConsts.Members };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<ClubMember>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<ClubMember>>> GetClubMembersAsync(long id, int page)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			Guard.IsGreaterThanZero(page, nameof(page));
			var queryParams = $"?page={page}";
			string[] endpointParts = new string[] { JikanEndpointConsts.Clubs, id.ToString(), JikanEndpointConsts.Members + queryParams };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<ClubMember>>>(endpointParts);
		}

		#endregion GetClubMembersAsync

		#region GetClubStaffAsync

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<ClubStaff>>> GetClubStaffAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndpointConsts.Clubs, id.ToString(), JikanEndpointConsts.Staff };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<ClubStaff>>>(endpointParts);
		}

		#endregion GetClubStaffAsync

		#region GetClubRelationsAsync

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ClubRelations>> GetClubRelationsAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			string[] endpointParts = new string[] { JikanEndpointConsts.Clubs, id.ToString(), JikanEndpointConsts.Relations };
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
			string[] endpointParts = new string[] { JikanEndpointConsts.Users, username };
			return await ExecuteGetRequestAsync<BaseJikanResponse<UserProfile>>(endpointParts);
		}

		#endregion GetUserProfileAsync

		#region GetUserStatisticsAsync

		/// <inheritdoc />
		public async Task<BaseJikanResponse<UserStatistics>> GetUserStatisticsAsync(string username)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			string[] endpointParts = new string[] { JikanEndpointConsts.Users, username, JikanEndpointConsts.Statistics };
			return await ExecuteGetRequestAsync<BaseJikanResponse<UserStatistics>>(endpointParts);
		}

		#endregion GetUserStatisticsAsync

		#region GetUserFavoritesAsync

		/// <inheritdoc />
		public async Task<BaseJikanResponse<UserFavorites>> GetUserFavoritesAsync(string username)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			string[] endpointParts = new string[] { JikanEndpointConsts.Users, username, JikanEndpointConsts.Favorites };
			return await ExecuteGetRequestAsync<BaseJikanResponse<UserFavorites>>(endpointParts);
		}

		#endregion GetUserFavoritesAsync

		#region GetUserAboutAsync

		/// <inheritdoc />
		public async Task<BaseJikanResponse<UserAbout>> GetUserAboutAsync(string username)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			string[] endpointParts = new string[] { JikanEndpointConsts.Users, username, JikanEndpointConsts.About };
			return await ExecuteGetRequestAsync<BaseJikanResponse<UserAbout>>(endpointParts);
		}

		#endregion GetUserAboutAsync

		#region GetUserHistoryAsync

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<HistoryEntry>>> GetUserHistoryAsync(string username)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			string[] endpointParts = new string[] { JikanEndpointConsts.Users, username, JikanEndpointConsts.History };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<HistoryEntry>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<HistoryEntry>>> GetUserHistoryAsync(string username, UserHistoryExtension historyExtension)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			Guard.IsValidEnum(historyExtension, nameof(historyExtension));
			string[] endpointParts = new string[] { JikanEndpointConsts.Users, username, JikanEndpointConsts.History, historyExtension.GetDescription() };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<HistoryEntry>>>(endpointParts);
		}

		#endregion GetUserHistoryAsync

		#region GetUserAnimeListAsync

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<AnimeListEntry>>> GetUserAnimeListAsync(string username)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			string[] endpointParts = new string[] { JikanEndpointConsts.Users, username, JikanEndpointConsts.AnimeList };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<AnimeListEntry>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<AnimeListEntry>>> GetUserAnimeListAsync(string username, int page)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			Guard.IsGreaterThanZero(page, nameof(page));
			var queryParams = $"?page={page}";
			string[] endpointParts = new string[] { JikanEndpointConsts.Users, username, JikanEndpointConsts.AnimeList + queryParams };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<AnimeListEntry>>>(endpointParts);
		}

		#endregion GetUserAnimeListAsync

		#region GetUserMangaListAsync

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<MangaListEntry>>> GetUserMangaListAsync(string username)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			string[] endpointParts = new string[] { JikanEndpointConsts.Users, username, JikanEndpointConsts.MangaList };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<MangaListEntry>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<MangaListEntry>>> GetUserMangaListAsync(string username, int page)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			Guard.IsGreaterThanZero(page, nameof(page));
			var queryParams = $"?page={page}";
			string[] endpointParts = new string[] { JikanEndpointConsts.Users, username, JikanEndpointConsts.MangaList + queryParams };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<MangaListEntry>>>(endpointParts);
		}

		#endregion GetUserMangaListAsync

		#region GetUserFriendsAsync

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Friend>>> GetUserFriendsAsync(string username)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			string[] endpointParts = new string[] { JikanEndpointConsts.Users, username, JikanEndpointConsts.Friends };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Friend>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Friend>>> GetUserFriendsAsync(string username, int page)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			Guard.IsGreaterThanZero(page, nameof(page));
			var queryParams = $"?page={page}";
			string[] endpointParts = new string[] { JikanEndpointConsts.Users, username, JikanEndpointConsts.Friends + queryParams };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Friend>>>(endpointParts);
		}

		#endregion GetUserFriendsAsync

		#region GetUserReviewsAsync

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Review>>> GetUserReviewsAsync(string username)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			string[] endpointParts = new string[] { JikanEndpointConsts.Users, username, JikanEndpointConsts.Reviews };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Review>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Review>>> GetUserReviewsAsync(string username, int page)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			Guard.IsGreaterThanZero(page, nameof(page));
			var queryParams = $"?page={page}";
			string[] endpointParts = new string[] { JikanEndpointConsts.Users, username, JikanEndpointConsts.Reviews + queryParams };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Review>>>(endpointParts);
		}

		#endregion GetUserReviewsAsync

		#region GetUserRecommendationsAsync

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<UserRecommendation>>> GetUserRecommendationsAsync(string username)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			string[] endpointParts = new string[] { JikanEndpointConsts.Users, username, JikanEndpointConsts.Recommendations };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<UserRecommendation>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<UserRecommendation>>> GetUserRecommendationsAsync(string username, int page)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			Guard.IsGreaterThanZero(page, nameof(page));
			var queryParams = $"?page={page}";
			string[] endpointParts = new string[] { JikanEndpointConsts.Users, username, JikanEndpointConsts.Recommendations + queryParams };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<UserRecommendation>>>(endpointParts);
		}

		#endregion GetUserRecommendationsAsync

		#region GetUserClubsAsync

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<MalUrl>>> GetUserClubsAsync(string username)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			string[] endpointParts = new string[] { JikanEndpointConsts.Users, username, JikanEndpointConsts.Clubs };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<MalUrl>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<MalUrl>>> GetUserClubsAsync(string username, int page)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			Guard.IsGreaterThanZero(page, nameof(page));
			var queryParams = $"?page={page}";
			string[] endpointParts = new string[] { JikanEndpointConsts.Users, username, JikanEndpointConsts.Clubs + queryParams };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<MalUrl>>>(endpointParts);
		}

		#endregion GetUserClubsAsync

		#endregion User methods

		#region GetRandom methods

		/// <inheritdoc/>
		public async Task<BaseJikanResponse<Anime>> GetRandomAnimeAsync()
		{
			string[] endpointParts = new string[] { JikanEndpointConsts.Random, JikanEndpointConsts.Anime };
			return await ExecuteGetRequestAsync<BaseJikanResponse<Anime>>(endpointParts);
		}

		/// <inheritdoc/>
		public async Task<BaseJikanResponse<Manga>> GetRandomMangaAsync()
		{
			string[] endpointParts = new string[] { JikanEndpointConsts.Random, JikanEndpointConsts.Manga };
			return await ExecuteGetRequestAsync<BaseJikanResponse<Manga>>(endpointParts);
		}

		/// <inheritdoc/>
		public async Task<BaseJikanResponse<Character>> GetRandomCharacterAsync()
		{
			string[] endpointParts = new string[] { JikanEndpointConsts.Random, JikanEndpointConsts.Characters };
			return await ExecuteGetRequestAsync<BaseJikanResponse<Character>>(endpointParts);
		}

		/// <inheritdoc/>
		public async Task<BaseJikanResponse<Person>> GetRandomPersonAsync()
		{
			string[] endpointParts = new string[] { JikanEndpointConsts.Random, JikanEndpointConsts.People };
			return await ExecuteGetRequestAsync<BaseJikanResponse<Person>>(endpointParts);
		}

		/// <inheritdoc/>
		public async Task<BaseJikanResponse<UserProfile>> GetRandomUserAsync()
		{
			string[] endpointParts = new string[] { JikanEndpointConsts.Random, JikanEndpointConsts.Users };
			return await ExecuteGetRequestAsync<BaseJikanResponse<UserProfile>>(endpointParts);
		}

		#endregion GetRandom methods

		#region Search methods

		#region SearchAnime

		/// <inheritdoc />
		public async Task<AnimeSearchResult> SearchAnime(string query)
		{
			Guard.IsLongerThan2Characters(query, nameof(query));
			query = string.Concat(JikanEndpointConsts.Anime, "?q=", query.Replace(' ', '+'));
			string[] endpointParts = new string[] { JikanEndpointConsts.Search, query };
			return await ExecuteGetRequestAsync<AnimeSearchResult>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<AnimeSearchResult> SearchAnime(string query, int page)
		{
			Guard.IsLongerThan2Characters(query, nameof(query));
			Guard.IsGreaterThanZero(page, nameof(page));
			query = string.Concat(JikanEndpointConsts.Anime, "/", page.ToString(), "?q=", query.Replace(' ', '+'));
			string[] endpointParts = new string[] { JikanEndpointConsts.Search, query };
			return await ExecuteGetRequestAsync<AnimeSearchResult>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<AnimeSearchResult> SearchAnime(string query, AnimeSearchConfig searchConfig)
		{
			Guard.IsLongerThan2Characters(query, nameof(query));
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			query = string.Concat(JikanEndpointConsts.Anime, "?q=", query.Replace(' ', '+'), "&", searchConfig.ConfigToString());
			string[] endpointParts = new string[] { JikanEndpointConsts.Search, query };
			return await ExecuteGetRequestAsync<AnimeSearchResult>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<AnimeSearchResult> SearchAnime(AnimeSearchConfig searchConfig)
		{
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			var query = string.Concat(JikanEndpointConsts.Anime, "?", searchConfig.ConfigToString());
			string[] endpointParts = new string[] { JikanEndpointConsts.Search, query };
			return await ExecuteGetRequestAsync<AnimeSearchResult>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<AnimeSearchResult> SearchAnime(AnimeSearchConfig searchConfig, int page)
		{
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			Guard.IsGreaterThanZero(page, nameof(page));
			var query = string.Concat(JikanEndpointConsts.Anime, "/", page.ToString(), "?", searchConfig.ConfigToString());
			string[] endpointParts = new string[] { JikanEndpointConsts.Search, query };
			return await ExecuteGetRequestAsync<AnimeSearchResult>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<AnimeSearchResult> SearchAnime(string query, int page, AnimeSearchConfig searchConfig)
		{
			Guard.IsLongerThan2Characters(query, nameof(query));
			Guard.IsGreaterThanZero(page, nameof(page));
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			query = string.Concat(JikanEndpointConsts.Anime, "/", page.ToString(), "?q=", query.Replace(' ', '+'), "&", searchConfig.ConfigToString());
			string[] endpointParts = new string[] { JikanEndpointConsts.Search, query };
			return await ExecuteGetRequestAsync<AnimeSearchResult>(endpointParts);
		}

		#endregion SearchAnime

		#region SearchManga

		/// <inheritdoc />
		public async Task<MangaSearchResult> SearchManga(string query)
		{
			Guard.IsLongerThan2Characters(query, nameof(query));
			query = string.Concat(JikanEndpointConsts.Manga, "?q=", query.Replace(' ', '+'));
			string[] endpointParts = new string[] { JikanEndpointConsts.Search, query };
			return await ExecuteGetRequestAsync<MangaSearchResult>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<MangaSearchResult> SearchManga(string query, int page)
		{
			Guard.IsLongerThan2Characters(query, nameof(query));
			Guard.IsGreaterThanZero(page, nameof(page));
			query = string.Concat(JikanEndpointConsts.Manga, "/", page.ToString(), "?q=", query.Replace(' ', '+'));
			string[] endpointParts = new string[] { JikanEndpointConsts.Search, query };
			return await ExecuteGetRequestAsync<MangaSearchResult>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<MangaSearchResult> SearchManga(string query, MangaSearchConfig searchConfig)
		{
			Guard.IsLongerThan2Characters(query, nameof(query));
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			query = string.Concat(JikanEndpointConsts.Manga, "?q=", query.Replace(' ', '+'), "&", searchConfig.ConfigToString());
			string[] endpointParts = new string[] { JikanEndpointConsts.Search, query };
			return await ExecuteGetRequestAsync<MangaSearchResult>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<MangaSearchResult> SearchManga(MangaSearchConfig searchConfig)
		{
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			var query = string.Concat(JikanEndpointConsts.Manga, "?", searchConfig.ConfigToString());
			string[] endpointParts = new string[] { JikanEndpointConsts.Search, query };
			return await ExecuteGetRequestAsync<MangaSearchResult>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<MangaSearchResult> SearchManga(MangaSearchConfig searchConfig, int page)
		{
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			Guard.IsGreaterThanZero(page, nameof(page));
			var query = string.Concat(JikanEndpointConsts.Manga, "/", page.ToString(), "?", searchConfig.ConfigToString());
			string[] endpointParts = new string[] { JikanEndpointConsts.Search, query };
			return await ExecuteGetRequestAsync<MangaSearchResult>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<MangaSearchResult> SearchManga(string query, int page, MangaSearchConfig searchConfig)
		{
			Guard.IsLongerThan2Characters(query, nameof(query));
			Guard.IsGreaterThanZero(page, nameof(page));
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			query = string.Concat(JikanEndpointConsts.Manga, "/", page.ToString(), "?q=", query.Replace(' ', '+'), "&", searchConfig.ConfigToString());
			string[] endpointParts = new string[] { JikanEndpointConsts.Search, query };
			return await ExecuteGetRequestAsync<MangaSearchResult>(endpointParts);
		}

		#endregion SearchManga

		#region SearchPerson

		/// <inheritdoc />
		public async Task<PersonSearchResult> SearchPerson(string query)
		{
			Guard.IsLongerThan2Characters(query, nameof(query));
			query = string.Concat(JikanEndpointConsts.Person, "?q=", query.Replace(' ', '+'));
			string[] endpointParts = new string[] { JikanEndpointConsts.Search, query };
			return await ExecuteGetRequestAsync<PersonSearchResult>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<PersonSearchResult> SearchPerson(string query, int page)
		{
			Guard.IsLongerThan2Characters(query, nameof(query));
			Guard.IsGreaterThanZero(page, nameof(page));
			query = string.Concat(JikanEndpointConsts.Person, "/", page.ToString(), "?q=", query.Replace(' ', '+'));
			string[] endpointParts = new string[] { JikanEndpointConsts.Search, query };
			return await ExecuteGetRequestAsync<PersonSearchResult>(endpointParts);
		}

		#endregion SearchPerson

		#region SearchCharacter

		/// <inheritdoc />
		public async Task<CharacterSearchResult> SearchCharacter(string query)
		{
			Guard.IsLongerThan2Characters(query, nameof(query));
			query = string.Concat(JikanEndpointConsts.Character, "?q=", query.Replace(' ', '+'));
			string[] endpointParts = new string[] { JikanEndpointConsts.Search, query };
			return await ExecuteGetRequestAsync<CharacterSearchResult>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<CharacterSearchResult> SearchCharacter(string query, int page)
		{
			Guard.IsLongerThan2Characters(query, nameof(query));
			Guard.IsGreaterThanZero(page, nameof(page));
			query = string.Concat(JikanEndpointConsts.Character, "/", page.ToString(), "?q=", query.Replace(' ', '+'));
			string[] endpointParts = new string[] { JikanEndpointConsts.Search, query };
			return await ExecuteGetRequestAsync<CharacterSearchResult>(endpointParts);
		}

		#endregion SearchCharacter

		#endregion Search methods

		#endregion Public Methods
	}
}