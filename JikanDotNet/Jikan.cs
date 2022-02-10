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
		/// Basic method for handling requests and responses from endpoint.
		/// </summary>
		/// <typeparam name="T">Class type received from GET requests.</typeparam>
		/// <param name="routeSections">Arguments building endpoint.</param>
		/// <returns>Requested object if successful, null otherwise.</returns>
		private async Task<T> ExecuteGetRequestAsync<T>(ICollection<string> routeSections) where T : class
		{
			T returnedObject = null;
			var requestUrl = string.Join("/", routeSections);
			try
			{
				using var response = await _httpClient.GetAsync(requestUrl);
				if (response.IsSuccessStatusCode)
				{
					var json = await response.Content.ReadAsStringAsync();

					returnedObject = JsonSerializer.Deserialize<T>(json);
				}
				else if (!_jikanConfiguration.SuppressException)
				{
					var json = await response.Content.ReadAsStringAsync();
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

		/// <inheritdoc />
		public async Task<BaseJikanResponse<Anime>> GetAnimeAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.Anime, id.ToString() };
			return await ExecuteGetRequestAsync<BaseJikanResponse<Anime>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<AnimeCharacter>>> GetAnimeCharactersAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.Anime, id.ToString(), JikanEndpointConsts.Characters };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<AnimeCharacter>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<AnimeStaffPosition>>> GetAnimeStaffAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.Anime, id.ToString(), JikanEndpointConsts.Staff };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<AnimeStaffPosition>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<AnimeEpisode>>> GetAnimeEpisodesAsync(long id, int page)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			Guard.IsGreaterThanZero(page, nameof(page));
			var endpointParts = new[] { JikanEndpointConsts.Anime, id.ToString(), JikanEndpointConsts.Episodes + $"?page={page}" };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<AnimeEpisode>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<AnimeEpisode>>> GetAnimeEpisodesAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.Anime, id.ToString(), JikanEndpointConsts.Episodes };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<AnimeEpisode>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<AnimeEpisode>> GetAnimeEpisodeAsync(long animeId, int episodeId)
		{
			Guard.IsGreaterThanZero(animeId, nameof(animeId));
			Guard.IsGreaterThanZero(episodeId, nameof(episodeId));
			var endpointParts = new[] { JikanEndpointConsts.Anime, animeId.ToString(), JikanEndpointConsts.Episodes, episodeId.ToString() };
			return await ExecuteGetRequestAsync<BaseJikanResponse<AnimeEpisode>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<News>>> GetAnimeNewsAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.Anime, id.ToString(), JikanEndpointConsts.News };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<News>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<News>>> GetAnimeNewsAsync(long id, int page)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			Guard.IsGreaterThanZero(page, nameof(page));
			var endpointParts = new[] { JikanEndpointConsts.Anime, id.ToString(), JikanEndpointConsts.News + $"?page={page}" };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<News>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<ForumTopic>>> GetAnimeForumTopicsAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.Anime, id.ToString(), JikanEndpointConsts.Forum };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<ForumTopic>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<ForumTopic>>> GetAnimeForumTopicsAsync(long id, ForumTopicType type = ForumTopicType.All)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			Guard.IsValidEnum(type, nameof(type));

			var queryParams = $"?topic={type.GetDescription()}";
			var endpointParts = new[] { JikanEndpointConsts.Anime, id.ToString(), JikanEndpointConsts.Forum + queryParams };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<ForumTopic>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<AnimeVideos>> GetAnimeVideosAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.Anime, id.ToString(), JikanEndpointConsts.Videos };
			return await ExecuteGetRequestAsync<BaseJikanResponse<AnimeVideos>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<ImagesSet>>> GetAnimePicturesAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.Anime, id.ToString(), JikanEndpointConsts.Pictures };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<ImagesSet>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<AnimeStatistics>> GetAnimeStatisticsAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.Anime, id.ToString(), JikanEndpointConsts.Statistics };
			return await ExecuteGetRequestAsync<BaseJikanResponse<AnimeStatistics>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<MoreInfo>> GetAnimeMoreInfoAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.Anime, id.ToString(), JikanEndpointConsts.MoreInfo };
			return await ExecuteGetRequestAsync<BaseJikanResponse<MoreInfo>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<Recommendation>>> GetAnimeRecommendationsAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.Anime, id.ToString(), JikanEndpointConsts.Recommendations };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<Recommendation>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<AnimeUserUpdate>>> GetAnimeUserUpdatesAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.Anime, id.ToString(), JikanEndpointConsts.UserUpdates };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<AnimeUserUpdate>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<AnimeUserUpdate>>> GetAnimeUserUpdatesAsync(long id, int page)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			Guard.IsGreaterThanZero(page, nameof(page));

			var queryParams = $"?page={page}";
			var endpointParts = new[] { JikanEndpointConsts.Anime, id.ToString(), JikanEndpointConsts.UserUpdates + queryParams };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<AnimeUserUpdate>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Review>>> GetAnimeReviewsAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.Anime, id.ToString(), JikanEndpointConsts.Reviews };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Review>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Review>>> GetAnimeReviewsAsync(long id, int page)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			Guard.IsGreaterThanZero(page, nameof(page));

			var queryParams = $"?page={page}";
			var endpointParts = new[] { JikanEndpointConsts.Anime, id.ToString(), JikanEndpointConsts.Reviews + queryParams };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Review>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<RelatedEntry>>> GetAnimeRelationsAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.Anime, id.ToString(), JikanEndpointConsts.Relations };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<RelatedEntry>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<AnimeThemes>> GetAnimeThemesAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.Anime, id.ToString(), JikanEndpointConsts.Themes };
			return await ExecuteGetRequestAsync<BaseJikanResponse<AnimeThemes>>(endpointParts);
		}

		#endregion Anime methods

		#region Character methods

		/// <inheritdoc />
		public async Task<BaseJikanResponse<Character>> GetCharacterAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.Characters, id.ToString() };
			return await ExecuteGetRequestAsync<BaseJikanResponse<Character>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<CharacterAnimeographyEntry>>> GetCharacterAnimeAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.Characters, id.ToString(), JikanEndpointConsts.Anime };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<CharacterAnimeographyEntry>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<CharacterMangaographyEntry>>> GetCharacterMangaAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.Characters, id.ToString(), JikanEndpointConsts.Manga };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<CharacterMangaographyEntry>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<VoiceActorEntry>>> GetCharacterVoiceActorsAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.Characters, id.ToString(), JikanEndpointConsts.Voices };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<VoiceActorEntry>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<ImagesSet>>> GetCharacterPicturesAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.Characters, id.ToString(), JikanEndpointConsts.Pictures };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<ImagesSet>>>(endpointParts);
		}

		#endregion Character methods

		#region Manga methods

		/// <inheritdoc />
		public async Task<BaseJikanResponse<Manga>> GetMangaAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.Manga, id.ToString() };
			return await ExecuteGetRequestAsync<BaseJikanResponse<Manga>>(endpointParts);
		}
		
		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Manga>>> GetMangaAsync()
		{
			var endpointParts = new[] { JikanEndpointConsts.Manga };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Manga>>>(endpointParts);
		}
		
		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Manga>>> GetMangaAsync(int page, int pageSize)
		{
			Guard.IsGreaterThanZero(page, nameof(page));
			Guard.IsGreaterThanZero(pageSize, nameof(pageSize));
			Guard.IsLesserOrEqualThan(pageSize,ParameterConsts.MaximumPageSize, nameof(pageSize));
			
			var queryParams = $"?page={page}&limit={pageSize}";
			var endpointParts = new[] { JikanEndpointConsts.Manga + queryParams};
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Manga>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<MangaCharacter>>> GetMangaCharactersAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.Manga, id.ToString(), JikanEndpointConsts.Characters };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<MangaCharacter>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<News>>> GetMangaNewsAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.Manga, id.ToString(), JikanEndpointConsts.News };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<News>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<News>>> GetMangaNewsAsync(long id, int page)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			Guard.IsGreaterThanZero(page, nameof(page));

			var queryParams = $"?page={page}";
			var endpointParts = new[] { JikanEndpointConsts.Manga, id.ToString(), JikanEndpointConsts.News + queryParams };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<News>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<ForumTopic>>> GetMangaForumTopicsAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.Manga, id.ToString(), JikanEndpointConsts.Forum };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<ForumTopic>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<ImagesSet>>> GetMangaPicturesAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.Manga, id.ToString(), JikanEndpointConsts.Pictures };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<ImagesSet>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<MangaStatistics>> GetMangaStatisticsAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.Manga, id.ToString(), JikanEndpointConsts.Statistics };
			return await ExecuteGetRequestAsync<BaseJikanResponse<MangaStatistics>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<MoreInfo>> GetMangaMoreInfoAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.Manga, id.ToString(), JikanEndpointConsts.MoreInfo };
			return await ExecuteGetRequestAsync<BaseJikanResponse<MoreInfo>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<MangaUserUpdate>>> GetMangaUserUpdatesAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.Manga, id.ToString(), JikanEndpointConsts.UserUpdates };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<MangaUserUpdate>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<MangaUserUpdate>>> GetMangaUserUpdatesAsync(long id, int page)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			Guard.IsGreaterThanZero(page, nameof(page));

			var queryParams = $"?page={page}";
			var endpointParts = new[] { JikanEndpointConsts.Manga, id.ToString(), JikanEndpointConsts.UserUpdates + queryParams };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<MangaUserUpdate>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<Recommendation>>> GetMangaRecommendationsAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.Manga, id.ToString(), JikanEndpointConsts.Recommendations };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<Recommendation>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Review>>> GetMangaReviewsAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.Manga, id.ToString(), JikanEndpointConsts.Reviews };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Review>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<RelatedEntry>>> GetMangaRelationsAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.Manga, id.ToString(), JikanEndpointConsts.Relations };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<RelatedEntry>>>(endpointParts);
		}

		#endregion Manga methods

		#region Person methods

		/// <inheritdoc />
		public async Task<BaseJikanResponse<Person>> GetPersonAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.People, id.ToString() };
			return await ExecuteGetRequestAsync<BaseJikanResponse<Person>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<PersonAnimeographyEntry>>> GetPersonAnimeAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.People, id.ToString(), JikanEndpointConsts.Anime };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<PersonAnimeographyEntry>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<PersonMangaographyEntry>>> GetPersonMangaAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.People, id.ToString(), JikanEndpointConsts.Manga };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<PersonMangaographyEntry>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<VoiceActingRole>>> GetPersonVoiceActingRolesAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.People, id.ToString(), JikanEndpointConsts.Voices };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<VoiceActingRole>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<ImagesSet>>> GetPersonPicturesAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.People, id.ToString(), JikanEndpointConsts.Pictures };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<ImagesSet>>>(endpointParts);
		}

		#endregion Person methods

		#region Season methods

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Anime>>> GetSeasonAsync(int year, Season season)
		{
			Guard.IsValid(x => x >= 1000 && x < 10000, year, nameof(year));
			Guard.IsValidEnum(season, nameof(season));
			var endpointParts = new[] { JikanEndpointConsts.Seasons, year.ToString(), season.GetDescription() };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Anime>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<SeasonArchive>>> GetSeasonArchiveAsync()
		{
			var endpointParts = new[] { JikanEndpointConsts.Seasons };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<SeasonArchive>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Anime>>> GetUpcomingSeasonAsync()
		{
			var endpointParts = new[] { JikanEndpointConsts.Seasons, JikanEndpointConsts.Upcoming };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Anime>>>(endpointParts);
		}

		#endregion Season methods

		#region Schedule methods

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Anime>>> GetScheduleAsync()
		{
			var endpointParts = new[] { JikanEndpointConsts.Schedules };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Anime>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Anime>>> GetScheduleAsync(int page)
		{
			Guard.IsGreaterThanZero(page, nameof(page));
			var queryParams = $"?page={page}";
			var endpointParts = new[] { JikanEndpointConsts.Schedules + queryParams };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Anime>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Anime>>> GetScheduleAsync(ScheduledDay scheduledDay)
		{
			Guard.IsValidEnum(scheduledDay, nameof(scheduledDay));
			var queryParams = $"?topic={scheduledDay.GetDescription()}";
			var endpointParts = new[] { JikanEndpointConsts.Schedules + queryParams };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Anime>>>(endpointParts);
		}

		#endregion Schedule methods

		#region Top methods

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Anime>>> GetTopAnimeAsync()
		{
			var endpointParts = new[] { JikanEndpointConsts.TopList, JikanEndpointConsts.Anime };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Anime>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Anime>>> GetTopAnimeAsync(int page)
		{
			Guard.IsGreaterThanZero(page, nameof(page));
			var queryParams = $"?page={page}";
			var endpointParts = new[] { JikanEndpointConsts.TopList, JikanEndpointConsts.Anime + queryParams };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Anime>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Manga>>> GetTopMangaAsync()
		{
			var endpointParts = new[] { JikanEndpointConsts.TopList, JikanEndpointConsts.Manga };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Manga>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Manga>>> GetTopMangaAsync(int page)
		{
			Guard.IsGreaterThanZero(page, nameof(page));
			var queryParams = $"?page={page}";
			var endpointParts = new[] { JikanEndpointConsts.TopList, JikanEndpointConsts.Manga + queryParams };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Manga>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Person>>> GetTopPeopleAsync()
		{
			var endpointParts = new[] { JikanEndpointConsts.TopList, JikanEndpointConsts.People };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Person>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Person>>> GetTopPeopleAsync(int page)
		{
			Guard.IsGreaterThanZero(page, nameof(page));
			var queryParams = $"?page={page}";
			var endpointParts = new[] { JikanEndpointConsts.TopList, JikanEndpointConsts.People + queryParams };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Person>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Character>>> GetTopCharactersAsync()
		{
			var endpointParts = new[] { JikanEndpointConsts.TopList, JikanEndpointConsts.Characters };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Character>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Character>>> GetTopCharactersAsync(int page)
		{
			Guard.IsGreaterThanZero(page, nameof(page));
			var queryParams = $"?page={page}";
			var endpointParts = new[] { JikanEndpointConsts.TopList, JikanEndpointConsts.Characters + queryParams };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Character>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Review>>> GetTopReviewsAsync()
		{
			var endpointParts = new[] { JikanEndpointConsts.TopList, JikanEndpointConsts.Reviews };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Review>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Review>>> GetTopReviewsAsync(int page)
		{
			Guard.IsGreaterThanZero(page, nameof(page));
			var queryParams = $"?page={page}";
			var endpointParts = new[] { JikanEndpointConsts.TopList, JikanEndpointConsts.Reviews + queryParams };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Review>>>(endpointParts);
		}

		#endregion Top methods

		#region Genre methods

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<Genre>>> GetAnimeGenresAsync()
		{
			var endpointParts = new[] { JikanEndpointConsts.Genres, JikanEndpointConsts.Anime };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<Genre>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<Genre>>> GetAnimeGenresAsync(GenresFilter filter)
		{
			Guard.IsValidEnum(filter, nameof(filter));
			var queryParams = $"?filter={filter.GetDescription()}";
			var endpointParts = new[] { JikanEndpointConsts.Genres, JikanEndpointConsts.Anime + queryParams };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<Genre>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<Genre>>> GetMangaGenresAsync()
		{
			var endpointParts = new[] { JikanEndpointConsts.Genres, JikanEndpointConsts.Manga };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<Genre>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<Genre>>> GetMangaGenresAsync(GenresFilter filter)
		{
			Guard.IsValidEnum(filter, nameof(filter));
			var queryParams = $"?filter={filter.GetDescription()}";
			var endpointParts = new[] { JikanEndpointConsts.Genres, JikanEndpointConsts.Manga + queryParams };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<Genre>>>(endpointParts);
		}

		#endregion Genre methods

		#region Producer methods

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Producer>>> GetProducersAsync()
		{
			var endpointParts = new[] { JikanEndpointConsts.Producers };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Producer>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Producer>>> GetProducersAsync(int page)
		{
			Guard.IsGreaterThanZero(page, nameof(page));
			var queryParams = $"?page={page}";
			var endpointParts = new[] { JikanEndpointConsts.Producers + queryParams };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Producer>>>(endpointParts);
		}

		#endregion Producer methods

		#region Magazine methods

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Magazine>>> GetMagazinesAsync()
		{
			var endpointParts = new[] { JikanEndpointConsts.Magazines };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Magazine>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Magazine>>> GetMagazinesAsync(int page)
		{
			Guard.IsGreaterThanZero(page, nameof(page));
			var queryParams = $"?page={page}";
			var endpointParts = new[] { JikanEndpointConsts.Magazines + queryParams };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Magazine>>>(endpointParts);
		}

		#endregion Magazine methods

		#region Club methods

		/// <inheritdoc />
		public async Task<BaseJikanResponse<Club>> GetClubAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.Clubs, id.ToString() };
			return await ExecuteGetRequestAsync<BaseJikanResponse<Club>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<ClubMember>>> GetClubMembersAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.Clubs, id.ToString(), JikanEndpointConsts.Members };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<ClubMember>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<ClubMember>>> GetClubMembersAsync(long id, int page)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			Guard.IsGreaterThanZero(page, nameof(page));
			var queryParams = $"?page={page}";
			var endpointParts = new[] { JikanEndpointConsts.Clubs, id.ToString(), JikanEndpointConsts.Members + queryParams };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<ClubMember>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<ClubStaff>>> GetClubStaffAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.Clubs, id.ToString(), JikanEndpointConsts.Staff };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<ClubStaff>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ClubRelations>> GetClubRelationsAsync(long id)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.Clubs, id.ToString(), JikanEndpointConsts.Relations };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ClubRelations>>(endpointParts);
		}

		#endregion Club methods

		#region User methods

		/// <inheritdoc />
		public async Task<BaseJikanResponse<UserProfile>> GetUserProfileAsync(string username)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			var endpointParts = new[] { JikanEndpointConsts.Users, username };
			return await ExecuteGetRequestAsync<BaseJikanResponse<UserProfile>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<UserStatistics>> GetUserStatisticsAsync(string username)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			var endpointParts = new[] { JikanEndpointConsts.Users, username, JikanEndpointConsts.Statistics };
			return await ExecuteGetRequestAsync<BaseJikanResponse<UserStatistics>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<UserFavorites>> GetUserFavoritesAsync(string username)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			var endpointParts = new[] { JikanEndpointConsts.Users, username, JikanEndpointConsts.Favorites };
			return await ExecuteGetRequestAsync<BaseJikanResponse<UserFavorites>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<UserAbout>> GetUserAboutAsync(string username)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			var endpointParts = new[] { JikanEndpointConsts.Users, username, JikanEndpointConsts.About };
			return await ExecuteGetRequestAsync<BaseJikanResponse<UserAbout>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<HistoryEntry>>> GetUserHistoryAsync(string username)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			var endpointParts = new[] { JikanEndpointConsts.Users, username, JikanEndpointConsts.History };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<HistoryEntry>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<HistoryEntry>>> GetUserHistoryAsync(string username, UserHistoryExtension historyExtension)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			Guard.IsValidEnum(historyExtension, nameof(historyExtension));
			var endpointParts = new[] { JikanEndpointConsts.Users, username, JikanEndpointConsts.History, historyExtension.GetDescription() };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<HistoryEntry>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<AnimeListEntry>>> GetUserAnimeListAsync(string username)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			var endpointParts = new[] { JikanEndpointConsts.Users, username, JikanEndpointConsts.AnimeList };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<AnimeListEntry>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<AnimeListEntry>>> GetUserAnimeListAsync(string username, int page)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			Guard.IsGreaterThanZero(page, nameof(page));
			var queryParams = $"?page={page}";
			var endpointParts = new[] { JikanEndpointConsts.Users, username, JikanEndpointConsts.AnimeList + queryParams };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<AnimeListEntry>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<MangaListEntry>>> GetUserMangaListAsync(string username)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			var endpointParts = new[] { JikanEndpointConsts.Users, username, JikanEndpointConsts.MangaList };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<MangaListEntry>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<MangaListEntry>>> GetUserMangaListAsync(string username, int page)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			Guard.IsGreaterThanZero(page, nameof(page));
			var queryParams = $"?page={page}";
			var endpointParts = new[] { JikanEndpointConsts.Users, username, JikanEndpointConsts.MangaList + queryParams };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<MangaListEntry>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Friend>>> GetUserFriendsAsync(string username)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			var endpointParts = new[] { JikanEndpointConsts.Users, username, JikanEndpointConsts.Friends };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Friend>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Friend>>> GetUserFriendsAsync(string username, int page)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			Guard.IsGreaterThanZero(page, nameof(page));
			var queryParams = $"?page={page}";
			var endpointParts = new[] { JikanEndpointConsts.Users, username, JikanEndpointConsts.Friends + queryParams };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Friend>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Review>>> GetUserReviewsAsync(string username)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			var endpointParts = new[] { JikanEndpointConsts.Users, username, JikanEndpointConsts.Reviews };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Review>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Review>>> GetUserReviewsAsync(string username, int page)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			Guard.IsGreaterThanZero(page, nameof(page));
			var queryParams = $"?page={page}";
			var endpointParts = new[] { JikanEndpointConsts.Users, username, JikanEndpointConsts.Reviews + queryParams };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Review>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<UserRecommendation>>> GetUserRecommendationsAsync(string username)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			var endpointParts = new[] { JikanEndpointConsts.Users, username, JikanEndpointConsts.Recommendations };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<UserRecommendation>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<UserRecommendation>>> GetUserRecommendationsAsync(string username, int page)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			Guard.IsGreaterThanZero(page, nameof(page));
			var queryParams = $"?page={page}";
			var endpointParts = new[] { JikanEndpointConsts.Users, username, JikanEndpointConsts.Recommendations + queryParams };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<UserRecommendation>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<MalUrl>>> GetUserClubsAsync(string username)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			var endpointParts = new[] { JikanEndpointConsts.Users, username, JikanEndpointConsts.Clubs };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<MalUrl>>>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<MalUrl>>> GetUserClubsAsync(string username, int page)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			Guard.IsGreaterThanZero(page, nameof(page));
			var queryParams = $"?page={page}";
			var endpointParts = new[] { JikanEndpointConsts.Users, username, JikanEndpointConsts.Clubs + queryParams };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<MalUrl>>>(endpointParts);
		}

		#endregion User methods

		#region GetRandom methods

		/// <inheritdoc/>
		public async Task<BaseJikanResponse<Anime>> GetRandomAnimeAsync()
		{
			var endpointParts = new[] { JikanEndpointConsts.Random, JikanEndpointConsts.Anime };
			return await ExecuteGetRequestAsync<BaseJikanResponse<Anime>>(endpointParts);
		}

		/// <inheritdoc/>
		public async Task<BaseJikanResponse<Manga>> GetRandomMangaAsync()
		{
			var endpointParts = new[] { JikanEndpointConsts.Random, JikanEndpointConsts.Manga };
			return await ExecuteGetRequestAsync<BaseJikanResponse<Manga>>(endpointParts);
		}

		/// <inheritdoc/>
		public async Task<BaseJikanResponse<Character>> GetRandomCharacterAsync()
		{
			var endpointParts = new[] { JikanEndpointConsts.Random, JikanEndpointConsts.Characters };
			return await ExecuteGetRequestAsync<BaseJikanResponse<Character>>(endpointParts);
		}

		/// <inheritdoc/>
		public async Task<BaseJikanResponse<Person>> GetRandomPersonAsync()
		{
			var endpointParts = new[] { JikanEndpointConsts.Random, JikanEndpointConsts.People };
			return await ExecuteGetRequestAsync<BaseJikanResponse<Person>>(endpointParts);
		}

		/// <inheritdoc/>
		public async Task<BaseJikanResponse<UserProfile>> GetRandomUserAsync()
		{
			var endpointParts = new[] { JikanEndpointConsts.Random, JikanEndpointConsts.Users };
			return await ExecuteGetRequestAsync<BaseJikanResponse<UserProfile>>(endpointParts);
		}

		#endregion GetRandom methods
		
		#region Watch methods
		
		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<WatchEpisode>>> GetWatchRecentEpisodesAsync()
		{
			var endpointParts = new[] { JikanEndpointConsts.Watch, JikanEndpointConsts.Episodes };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<WatchEpisode>>>(endpointParts);
		}
		
		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<WatchEpisode>>> GetWatchPopularEpisodesAsync()
		{
			var endpointParts = new[] { JikanEndpointConsts.Watch, JikanEndpointConsts.Episodes, JikanEndpointConsts.Popular };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<WatchEpisode>>>(endpointParts);
		}
		
		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<WatchPromoVideo>>> GetWatchRecentPromosAsync()
		{
			var endpointParts = new[] { JikanEndpointConsts.Watch, JikanEndpointConsts.Promos };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<WatchPromoVideo>>>(endpointParts);
		}
		
		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<WatchPromoVideo>>> GetWatchPopularPromosAsync()
		{
			var endpointParts = new[] { JikanEndpointConsts.Watch, JikanEndpointConsts.Promos, JikanEndpointConsts.Popular };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<WatchPromoVideo>>>(endpointParts);
		}
		
		#endregion

		#region Reviews methods

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Review>>> GetRecentAnimeReviewsAsync()
		{
			var endpointParts = new[] { JikanEndpointConsts.Reviews, JikanEndpointConsts.Anime };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Review>>>(endpointParts);
		}
		
		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Review>>> GetRecentAnimeReviewsAsync(int page)
		{
			Guard.IsGreaterThanZero(page, nameof(page));
			var queryParams = $"?page={page}";
			var endpointParts = new[] { JikanEndpointConsts.Reviews, JikanEndpointConsts.Anime + queryParams };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Review>>>(endpointParts);
		}
		
		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Review>>> GetRecentMangaReviewsAsync()
		{
			var endpointParts = new[] { JikanEndpointConsts.Reviews, JikanEndpointConsts.Manga };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Review>>>(endpointParts);
		}
		
		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Review>>> GetRecentMangaReviewsAsync(int page)
		{
			Guard.IsGreaterThanZero(page, nameof(page));
			var queryParams = $"?page={page}";
			var endpointParts = new[] { JikanEndpointConsts.Reviews, JikanEndpointConsts.Manga + queryParams };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Review>>>(endpointParts);
		}
		
		#endregion
		
		#region Recommendations methods
		
		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<UserRecommendation>>> GetRecentAnimeRecommendationsAsync()
		{
			var endpointParts = new[] { JikanEndpointConsts.Recommendations, JikanEndpointConsts.Anime };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<UserRecommendation>>>(endpointParts);
		}
		
		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<UserRecommendation>>> GetRecentAnimeRecommendationsAsync(int page)
		{
			Guard.IsGreaterThanZero(page, nameof(page));
			var queryParams = $"?page={page}";
			var endpointParts = new[] { JikanEndpointConsts.Recommendations, JikanEndpointConsts.Anime + queryParams };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<UserRecommendation>>>(endpointParts);
		}
		
		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<UserRecommendation>>> GetRecentMangaRecommendationsAsync()
		{
			var endpointParts = new[] { JikanEndpointConsts.Recommendations, JikanEndpointConsts.Manga };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<UserRecommendation>>>(endpointParts);
		}
		
		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<UserRecommendation>>> GetRecentMangaRecommendationsAsync(int page)
		{
			Guard.IsGreaterThanZero(page, nameof(page));
			var queryParams = $"?page={page}";
			var endpointParts = new[] { JikanEndpointConsts.Recommendations, JikanEndpointConsts.Manga + queryParams };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<UserRecommendation>>>(endpointParts);
		}
		
		#endregion

		#region Search methods
		
		
		
		/// <inheritdoc />
		public Task<PaginatedJikanResponse<ICollection<Anime>>> SearchAnimeAsync(string query)
			=> SearchAnimeAsync(new AnimeSearchConfig {Query = query});
		

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Anime>>> SearchAnimeAsync(AnimeSearchConfig searchConfig)
		{
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			var endpointParts = new[] { JikanEndpointConsts.Anime + searchConfig.ConfigToString()};
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Anime>>>(endpointParts);
		}

		#region SearchManga

		/// <inheritdoc />
		public async Task<MangaSearchResult> SearchManga(string query)
		{
			Guard.IsLongerThan2Characters(query, nameof(query));
			query = string.Concat(JikanEndpointConsts.Manga, "?q=", query.Replace(' ', '+'));
			var endpointParts = new[] { JikanEndpointConsts.Search, query };
			return await ExecuteGetRequestAsync<MangaSearchResult>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<MangaSearchResult> SearchManga(string query, int page)
		{
			Guard.IsLongerThan2Characters(query, nameof(query));
			Guard.IsGreaterThanZero(page, nameof(page));
			query = string.Concat(JikanEndpointConsts.Manga, "/", page.ToString(), "?q=", query.Replace(' ', '+'));
			var endpointParts = new[] { JikanEndpointConsts.Search, query };
			return await ExecuteGetRequestAsync<MangaSearchResult>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<MangaSearchResult> SearchManga(string query, MangaSearchConfig searchConfig)
		{
			Guard.IsLongerThan2Characters(query, nameof(query));
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			query = string.Concat(JikanEndpointConsts.Manga, "?q=", query.Replace(' ', '+'), "&", searchConfig.ConfigToString());
			var endpointParts = new[] { JikanEndpointConsts.Search, query };
			return await ExecuteGetRequestAsync<MangaSearchResult>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<MangaSearchResult> SearchManga(MangaSearchConfig searchConfig)
		{
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			var query = string.Concat(JikanEndpointConsts.Manga, "?", searchConfig.ConfigToString());
			var endpointParts = new[] { JikanEndpointConsts.Search, query };
			return await ExecuteGetRequestAsync<MangaSearchResult>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<MangaSearchResult> SearchManga(MangaSearchConfig searchConfig, int page)
		{
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			Guard.IsGreaterThanZero(page, nameof(page));
			var query = string.Concat(JikanEndpointConsts.Manga, "/", page.ToString(), "?", searchConfig.ConfigToString());
			var endpointParts = new[] { JikanEndpointConsts.Search, query };
			return await ExecuteGetRequestAsync<MangaSearchResult>(endpointParts);
		}

		/// <inheritdoc />
		public async Task<MangaSearchResult> SearchManga(string query, int page, MangaSearchConfig searchConfig)
		{
			Guard.IsLongerThan2Characters(query, nameof(query));
			Guard.IsGreaterThanZero(page, nameof(page));
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			query = string.Concat(JikanEndpointConsts.Manga, "/", page.ToString(), "?q=", query.Replace(' ', '+'), "&", searchConfig.ConfigToString());
			var endpointParts = new[] { JikanEndpointConsts.Search, query };
			return await ExecuteGetRequestAsync<MangaSearchResult>(endpointParts);
		}

		#endregion SearchManga

		/// <inheritdoc />
		public Task<PaginatedJikanResponse<ICollection<Person>>> SearchPersonAsync(string query)
			=> SearchPersonAsync(new PersonSearchConfig {Query = query});
		
		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Person>>> SearchPersonAsync(PersonSearchConfig searchConfig)
		{	
			var endpointParts = new[] { JikanEndpointConsts.People + searchConfig.ConfigToString()};
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Person>>>(endpointParts);
		}

		/// <inheritdoc />
		public Task<PaginatedJikanResponse<ICollection<Character>>> SearchCharacterAsync(string query)
			=> SearchCharacterAsync(new CharacterSearchConfig {Query = query});
		
		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Character>>> SearchCharacterAsync(CharacterSearchConfig searchConfig)
		{
			var endpointParts = new[] { JikanEndpointConsts.Characters + searchConfig.ConfigToString()};
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Character>>>(endpointParts);
		}
	
		/// <inheritdoc />
		public  Task<PaginatedJikanResponse<ICollection<UserMetadata>>> SearchUserAsync(string query) => SearchUserAsync(new UserSearchConfig {Query = query});

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<UserMetadata>>> SearchUserAsync(UserSearchConfig searchConfig)
		{
			var endpointParts = new[] {JikanEndpointConsts.Users + searchConfig.ConfigToString()};
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<UserMetadata>>>(endpointParts);
		}

		#endregion Search methods

		#endregion Public Methods
	}
}