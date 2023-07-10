using JikanDotNet.Config;
using JikanDotNet.Consts;
using JikanDotNet.Exceptions;
using JikanDotNet.Extensions;
using JikanDotNet.Helpers;
using JikanDotNet.Limiter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
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

		/// <summary>
		/// API call limiter
		/// </summary>
		private readonly ITaskLimiter _limiter;

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
		/// <param name="httpClient">Http client.</param>
		public Jikan(JikanClientConfiguration jikanClientConfiguration, HttpClient httpClient = null)
		{
			_jikanConfiguration = jikanClientConfiguration;
			_limiter = new CompositeTaskLimiter(jikanClientConfiguration.LimiterConfigurations?.Distinct() ?? TaskLimiterConfiguration.None);
			_httpClient = httpClient ?? DefaultHttpClientProvider.GetDefaultHttpClient();
		}

        #endregion Constructors

        #region Private Methods

        /// <summary>
        /// Basic method for handling requests and responses from endpoint.
        /// </summary>
        /// <typeparam name="T">Class type received from GET requests.</typeparam>
        /// <param name="routeSections">Arguments building endpoint.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Requested object if successful, null otherwise.</returns>
        private async Task<T> ExecuteGetRequestAsync<T>(ICollection<string> routeSections, CancellationToken cancellationToken = default) where T : class
		{
			T returnedObject = null;
			var requestUrl = string.Join("/", routeSections);
			try
			{
				using var response = await _limiter.LimitAsync(() => _httpClient.GetAsync(requestUrl, cancellationToken));
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
		public async Task<BaseJikanResponse<Anime>> GetAnimeAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.Anime, id.ToString() };
			return await ExecuteGetRequestAsync<BaseJikanResponse<Anime>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<AnimeCharacter>>> GetAnimeCharactersAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.Anime, id.ToString(), JikanEndpointConsts.Characters };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<AnimeCharacter>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<AnimeStaffPosition>>> GetAnimeStaffAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.Anime, id.ToString(), JikanEndpointConsts.Staff };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<AnimeStaffPosition>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<AnimeEpisode>>> GetAnimeEpisodesAsync(long id, int page, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			Guard.IsGreaterThanZero(page, nameof(page));
			var endpointParts = new[] { JikanEndpointConsts.Anime, id.ToString(), JikanEndpointConsts.Episodes + $"?page={page}" };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<AnimeEpisode>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<AnimeEpisode>>> GetAnimeEpisodesAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.Anime, id.ToString(), JikanEndpointConsts.Episodes };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<AnimeEpisode>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<AnimeEpisode>> GetAnimeEpisodeAsync(long animeId, int episodeId, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(animeId, nameof(animeId));
			Guard.IsGreaterThanZero(episodeId, nameof(episodeId));
			var endpointParts = new[] { JikanEndpointConsts.Anime, animeId.ToString(), JikanEndpointConsts.Episodes, episodeId.ToString() };
			return await ExecuteGetRequestAsync<BaseJikanResponse<AnimeEpisode>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<News>>> GetAnimeNewsAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.Anime, id.ToString(), JikanEndpointConsts.News };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<News>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<News>>> GetAnimeNewsAsync(long id, int page, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			Guard.IsGreaterThanZero(page, nameof(page));
			var endpointParts = new[] { JikanEndpointConsts.Anime, id.ToString(), JikanEndpointConsts.News + $"?page={page}" };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<News>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<ForumTopic>>> GetAnimeForumTopicsAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.Anime, id.ToString(), JikanEndpointConsts.Forum };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<ForumTopic>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<ForumTopic>>> GetAnimeForumTopicsAsync(long id, ForumTopicType type, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			Guard.IsValidEnum(type, nameof(type));

			var queryParams = $"?filter={type.GetDescription()}";
			var endpointParts = new[] { JikanEndpointConsts.Anime, id.ToString(), JikanEndpointConsts.Forum + queryParams };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<ForumTopic>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<AnimeVideos>> GetAnimeVideosAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.Anime, id.ToString(), JikanEndpointConsts.Videos };
			return await ExecuteGetRequestAsync<BaseJikanResponse<AnimeVideos>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<ImagesSet>>> GetAnimePicturesAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.Anime, id.ToString(), JikanEndpointConsts.Pictures };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<ImagesSet>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<AnimeStatistics>> GetAnimeStatisticsAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.Anime, id.ToString(), JikanEndpointConsts.Statistics };
			return await ExecuteGetRequestAsync<BaseJikanResponse<AnimeStatistics>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<MoreInfo>> GetAnimeMoreInfoAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.Anime, id.ToString(), JikanEndpointConsts.MoreInfo };
			return await ExecuteGetRequestAsync<BaseJikanResponse<MoreInfo>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<Recommendation>>> GetAnimeRecommendationsAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.Anime, id.ToString(), JikanEndpointConsts.Recommendations };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<Recommendation>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<AnimeUserUpdate>>> GetAnimeUserUpdatesAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.Anime, id.ToString(), JikanEndpointConsts.UserUpdates };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<AnimeUserUpdate>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<AnimeUserUpdate>>> GetAnimeUserUpdatesAsync(long id, int page, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			Guard.IsGreaterThanZero(page, nameof(page));

			var queryParams = $"?page={page}";
			var endpointParts = new[] { JikanEndpointConsts.Anime, id.ToString(), JikanEndpointConsts.UserUpdates + queryParams };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<AnimeUserUpdate>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Review>>> GetAnimeReviewsAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.Anime, id.ToString(), JikanEndpointConsts.Reviews };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Review>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Review>>> GetAnimeReviewsAsync(long id, int page, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			Guard.IsGreaterThanZero(page, nameof(page));

			var queryParams = $"?page={page}";
			var endpointParts = new[] { JikanEndpointConsts.Anime, id.ToString(), JikanEndpointConsts.Reviews + queryParams };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Review>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<RelatedEntry>>> GetAnimeRelationsAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.Anime, id.ToString(), JikanEndpointConsts.Relations };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<RelatedEntry>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<AnimeThemes>> GetAnimeThemesAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.Anime, id.ToString(), JikanEndpointConsts.Themes };
			return await ExecuteGetRequestAsync<BaseJikanResponse<AnimeThemes>>(endpointParts, cancellationToken);
		}
		
		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<ExternalLink>>> GetAnimeExternalLinksAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.Anime, id.ToString(), JikanEndpointConsts.External };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<ExternalLink>>>(endpointParts, cancellationToken);
		}
		
		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<ExternalLink>>> GetAnimeStreamingLinksAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.Anime, id.ToString(), JikanEndpointConsts.Streaming };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<ExternalLink>>>(endpointParts, cancellationToken);
		}
		
		/// <inheritdoc />
		public async Task<BaseJikanResponse<AnimeFull>> GetAnimeFullDataAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.Anime, id.ToString(), JikanEndpointConsts.Full };
			return await ExecuteGetRequestAsync<BaseJikanResponse<AnimeFull>>(endpointParts, cancellationToken);
		}

		#endregion Anime methods

		#region Character methods

		/// <inheritdoc />
		public async Task<BaseJikanResponse<Character>> GetCharacterAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.Characters, id.ToString() };
			return await ExecuteGetRequestAsync<BaseJikanResponse<Character>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<CharacterAnimeographyEntry>>> GetCharacterAnimeAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.Characters, id.ToString(), JikanEndpointConsts.Anime };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<CharacterAnimeographyEntry>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<CharacterMangaographyEntry>>> GetCharacterMangaAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.Characters, id.ToString(), JikanEndpointConsts.Manga };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<CharacterMangaographyEntry>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<VoiceActorEntry>>> GetCharacterVoiceActorsAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.Characters, id.ToString(), JikanEndpointConsts.Voices };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<VoiceActorEntry>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<ImagesSet>>> GetCharacterPicturesAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.Characters, id.ToString(), JikanEndpointConsts.Pictures };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<ImagesSet>>>(endpointParts, cancellationToken);
		}
		
		/// <inheritdoc />
		public async Task<BaseJikanResponse<CharacterFull>> GetCharacterFullDataAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.Characters, id.ToString(), JikanEndpointConsts.Full };
			return await ExecuteGetRequestAsync<BaseJikanResponse<CharacterFull>>(endpointParts, cancellationToken);
		}

		#endregion Character methods

		#region Manga methods

		/// <inheritdoc />
		public async Task<BaseJikanResponse<Manga>> GetMangaAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.Manga, id.ToString() };
			return await ExecuteGetRequestAsync<BaseJikanResponse<Manga>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<MangaCharacter>>> GetMangaCharactersAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.Manga, id.ToString(), JikanEndpointConsts.Characters };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<MangaCharacter>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<News>>> GetMangaNewsAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.Manga, id.ToString(), JikanEndpointConsts.News };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<News>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<News>>> GetMangaNewsAsync(long id, int page, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			Guard.IsGreaterThanZero(page, nameof(page));

			var queryParams = $"?page={page}";
			var endpointParts = new[] { JikanEndpointConsts.Manga, id.ToString(), JikanEndpointConsts.News + queryParams };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<News>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<ForumTopic>>> GetMangaForumTopicsAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.Manga, id.ToString(), JikanEndpointConsts.Forum };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<ForumTopic>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<ImagesSet>>> GetMangaPicturesAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.Manga, id.ToString(), JikanEndpointConsts.Pictures };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<ImagesSet>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<MangaStatistics>> GetMangaStatisticsAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.Manga, id.ToString(), JikanEndpointConsts.Statistics };
			return await ExecuteGetRequestAsync<BaseJikanResponse<MangaStatistics>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<MoreInfo>> GetMangaMoreInfoAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.Manga, id.ToString(), JikanEndpointConsts.MoreInfo };
			return await ExecuteGetRequestAsync<BaseJikanResponse<MoreInfo>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<MangaUserUpdate>>> GetMangaUserUpdatesAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.Manga, id.ToString(), JikanEndpointConsts.UserUpdates };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<MangaUserUpdate>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<MangaUserUpdate>>> GetMangaUserUpdatesAsync(long id, int page, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			Guard.IsGreaterThanZero(page, nameof(page));

			var queryParams = $"?page={page}";
			var endpointParts = new[] { JikanEndpointConsts.Manga, id.ToString(), JikanEndpointConsts.UserUpdates + queryParams };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<MangaUserUpdate>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<Recommendation>>> GetMangaRecommendationsAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.Manga, id.ToString(), JikanEndpointConsts.Recommendations };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<Recommendation>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Review>>> GetMangaReviewsAsync(long id, bool includePreliminary = false, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var queryParams = $"?preliminary={includePreliminary}";
			var endpointParts = new[] { JikanEndpointConsts.Manga, id.ToString(), JikanEndpointConsts.Reviews, queryParams };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Review>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<RelatedEntry>>> GetMangaRelationsAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.Manga, id.ToString(), JikanEndpointConsts.Relations };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<RelatedEntry>>>(endpointParts, cancellationToken);
		}
		
		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<ExternalLink>>> GetMangaExternalLinksAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.Manga, id.ToString(), JikanEndpointConsts.External };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<ExternalLink>>>(endpointParts, cancellationToken);
		}
		
		/// <inheritdoc />
		public async Task<BaseJikanResponse<MangaFull>> GetMangaFullDataAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.Manga, id.ToString(), JikanEndpointConsts.Full };
			return await ExecuteGetRequestAsync<BaseJikanResponse<MangaFull>>(endpointParts, cancellationToken);
		}

		#endregion Manga methods

		#region Person methods

		/// <inheritdoc />
		public async Task<BaseJikanResponse<Person>> GetPersonAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.People, id.ToString() };
			return await ExecuteGetRequestAsync<BaseJikanResponse<Person>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<PersonAnimeographyEntry>>> GetPersonAnimeAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.People, id.ToString(), JikanEndpointConsts.Anime };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<PersonAnimeographyEntry>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<PersonMangaographyEntry>>> GetPersonMangaAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.People, id.ToString(), JikanEndpointConsts.Manga };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<PersonMangaographyEntry>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<VoiceActingRole>>> GetPersonVoiceActingRolesAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.People, id.ToString(), JikanEndpointConsts.Voices };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<VoiceActingRole>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<ImagesSet>>> GetPersonPicturesAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.People, id.ToString(), JikanEndpointConsts.Pictures };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<ImagesSet>>>(endpointParts, cancellationToken);
		}
		
		/// <inheritdoc />
		public async Task<BaseJikanResponse<PersonFull>> GetPersonFullDataAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.People, id.ToString(), JikanEndpointConsts.Full };
			return await ExecuteGetRequestAsync<BaseJikanResponse<PersonFull>>(endpointParts, cancellationToken);
		}

		#endregion Person methods

		#region Season methods

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Anime>>> GetSeasonAsync(int year, Season season, CancellationToken cancellationToken = default)
		{
			Guard.IsValid(x => x is >= 1000 and < 10000, year, nameof(year));
			Guard.IsValidEnum(season, nameof(season));
			var endpointParts = new[] { JikanEndpointConsts.Seasons, year.ToString(), season.GetDescription() };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Anime>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Anime>>> GetSeasonAsync(int year, Season season, int page, CancellationToken cancellationToken = default)
		{
			Guard.IsValid(x => x is >= 1000 and < 10000, year, nameof(year));
			Guard.IsValidEnum(season, nameof(season));
			Guard.IsGreaterThanZero(page, nameof(page));
			var queryParams = $"?page={page}";
			var endpointParts = new[] { JikanEndpointConsts.Seasons, year.ToString(), season.GetDescription() + queryParams};
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Anime>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<SeasonArchive>>> GetSeasonArchiveAsync(CancellationToken cancellationToken = default)
		{
			var endpointParts = new[] { JikanEndpointConsts.Seasons };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<SeasonArchive>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Anime>>> GetCurrentSeasonAsync(CancellationToken cancellationToken = default)
		{
			var endpointParts = new[] { JikanEndpointConsts.Seasons, JikanEndpointConsts.Now };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Anime>>>(endpointParts, cancellationToken);
		}
		
		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Anime>>> GetCurrentSeasonAsync(int page, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(page, nameof(page));
			var queryParams = $"?page={page}";
			var endpointParts = new[] { JikanEndpointConsts.Seasons, JikanEndpointConsts.Now + queryParams};
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Anime>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Anime>>> GetUpcomingSeasonAsync(CancellationToken cancellationToken = default)
		{
			var endpointParts = new[] { JikanEndpointConsts.Seasons, JikanEndpointConsts.Upcoming };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Anime>>>(endpointParts, cancellationToken);
		}
		
		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Anime>>> GetUpcomingSeasonAsync(int page, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(page, nameof(page));
			var queryParams = $"?page={page}";
			var endpointParts = new[] { JikanEndpointConsts.Seasons, JikanEndpointConsts.Upcoming + queryParams};
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Anime>>>(endpointParts, cancellationToken);
		}

		#endregion Season methods

		#region Schedule methods

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Anime>>> GetScheduleAsync(CancellationToken cancellationToken = default)
		{
			var endpointParts = new[] { JikanEndpointConsts.Schedules };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Anime>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Anime>>> GetScheduleAsync(int page, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(page, nameof(page));
			var queryParams = $"?page={page}";
			var endpointParts = new[] { JikanEndpointConsts.Schedules + queryParams };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Anime>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Anime>>> GetScheduleAsync(ScheduledDay scheduledDay, CancellationToken cancellationToken = default)
		{
			Guard.IsValidEnum(scheduledDay, nameof(scheduledDay));
			var queryParams = $"?filter={scheduledDay.GetDescription()}";
			var endpointParts = new[] { JikanEndpointConsts.Schedules + queryParams };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Anime>>>(endpointParts, cancellationToken);
		}

		#endregion Schedule methods

		#region Top methods

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Anime>>> GetTopAnimeAsync(CancellationToken cancellationToken = default)
		{
			var endpointParts = new[] { JikanEndpointConsts.TopList, JikanEndpointConsts.Anime };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Anime>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Anime>>> GetTopAnimeAsync(int page, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(page, nameof(page));
			var queryParams = $"?page={page}";
			var endpointParts = new[] { JikanEndpointConsts.TopList, JikanEndpointConsts.Anime + queryParams };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Anime>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Anime>>> GetTopAnimeAsync(TopAnimeFilter filter, CancellationToken cancellationToken = default)
		{
			Guard.IsValidEnum(filter, nameof(filter));
			var queryParams = $"?filter={filter.GetDescription()}";
			var endpointParts = new[] { JikanEndpointConsts.TopList, JikanEndpointConsts.Anime + queryParams };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Anime>>>(endpointParts, cancellationToken);
		}
		
        /// <inheritdoc />
        public async Task<PaginatedJikanResponse<ICollection<Anime>>> GetTopAnimeAsync(TopAnimeFilter filter, int page, CancellationToken cancellationToken = default)
        {
	        Guard.IsValidEnum(filter, nameof(filter));
            Guard.IsGreaterThanZero(page, nameof(page));
            var queryParams = $"?page={page}&filter={filter.GetDescription()}";
            var endpointParts = new[] { JikanEndpointConsts.TopList, JikanEndpointConsts.Anime + queryParams };
            return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Anime>>>(endpointParts, cancellationToken);
        }
        
        /// <inheritdoc />
        public async Task<PaginatedJikanResponse<ICollection<Manga>>> GetTopMangaAsync(CancellationToken cancellationToken = default)
		{
			var endpointParts = new[] { JikanEndpointConsts.TopList, JikanEndpointConsts.Manga };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Manga>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Manga>>> GetTopMangaAsync(int page, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(page, nameof(page));
			var queryParams = $"?page={page}";
			var endpointParts = new[] { JikanEndpointConsts.TopList, JikanEndpointConsts.Manga + queryParams };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Manga>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Person>>> GetTopPeopleAsync(CancellationToken cancellationToken = default)
		{
			var endpointParts = new[] { JikanEndpointConsts.TopList, JikanEndpointConsts.People };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Person>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Person>>> GetTopPeopleAsync(int page, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(page, nameof(page));
			var queryParams = $"?page={page}";
			var endpointParts = new[] { JikanEndpointConsts.TopList, JikanEndpointConsts.People + queryParams };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Person>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Character>>> GetTopCharactersAsync(CancellationToken cancellationToken = default)
		{
			var endpointParts = new[] { JikanEndpointConsts.TopList, JikanEndpointConsts.Characters };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Character>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Character>>> GetTopCharactersAsync(int page, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(page, nameof(page));
			var queryParams = $"?page={page}";
			var endpointParts = new[] { JikanEndpointConsts.TopList, JikanEndpointConsts.Characters + queryParams };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Character>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Review>>> GetTopReviewsAsync(CancellationToken cancellationToken = default)
		{
			var endpointParts = new[] { JikanEndpointConsts.TopList, JikanEndpointConsts.Reviews };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Review>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Review>>> GetTopReviewsAsync(int page, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(page, nameof(page));
			var queryParams = $"?page={page}";
			var endpointParts = new[] { JikanEndpointConsts.TopList, JikanEndpointConsts.Reviews + queryParams };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Review>>>(endpointParts, cancellationToken);
		}

		#endregion Top methods

		#region Genre methods

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<Genre>>> GetAnimeGenresAsync(CancellationToken cancellationToken = default)
		{
			var endpointParts = new[] { JikanEndpointConsts.Genres, JikanEndpointConsts.Anime };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<Genre>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<Genre>>> GetAnimeGenresAsync(GenresFilter filter, CancellationToken cancellationToken = default)
		{
			Guard.IsValidEnum(filter, nameof(filter));
			var queryParams = $"?filter={filter.GetDescription()}";
			var endpointParts = new[] { JikanEndpointConsts.Genres, JikanEndpointConsts.Anime + queryParams };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<Genre>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<Genre>>> GetMangaGenresAsync(CancellationToken cancellationToken = default)
		{
			var endpointParts = new[] { JikanEndpointConsts.Genres, JikanEndpointConsts.Manga };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<Genre>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<Genre>>> GetMangaGenresAsync(GenresFilter filter, CancellationToken cancellationToken = default)
		{
			Guard.IsValidEnum(filter, nameof(filter));
			var queryParams = $"?filter={filter.GetDescription()}";
			var endpointParts = new[] { JikanEndpointConsts.Genres, JikanEndpointConsts.Manga + queryParams };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<Genre>>>(endpointParts, cancellationToken);
		}

		#endregion Genre methods

		#region Producer methods

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Producer>>> GetProducersAsync(CancellationToken cancellationToken = default)
		{
			var endpointParts = new[] { JikanEndpointConsts.Producers };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Producer>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Producer>>> GetProducersAsync(int page, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(page, nameof(page));
			var queryParams = $"?page={page}";
			var endpointParts = new[] { JikanEndpointConsts.Producers + queryParams };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Producer>>>(endpointParts, cancellationToken);
		}
		
		/// <inheritdoc />
		public async Task<BaseJikanResponse<Producer>> GetProducerAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.Producers, id.ToString() };
			return await ExecuteGetRequestAsync<BaseJikanResponse<Producer>>(endpointParts, cancellationToken);
		}
		
		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<ExternalLink>>> GetProducerExternalLinksAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.Producers, id.ToString(), JikanEndpointConsts.External };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<ExternalLink>>>(endpointParts, cancellationToken);
		}
		
		/// <inheritdoc />
		public async Task<BaseJikanResponse<ProducerFull>> GetProducerFullDataAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.Producers, id.ToString(), JikanEndpointConsts.Full };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ProducerFull>>(endpointParts, cancellationToken);
		}

		#endregion Producer methods

		#region Magazine methods

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Magazine>>> GetMagazinesAsync(CancellationToken cancellationToken = default)
		{
			var endpointParts = new[] { JikanEndpointConsts.Magazines };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Magazine>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Magazine>>> GetMagazinesAsync(int page, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(page, nameof(page));
			var queryParams = $"?page={page}";
			var endpointParts = new[] { JikanEndpointConsts.Magazines + queryParams };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Magazine>>>(endpointParts, cancellationToken);
		}

		#endregion Magazine methods

		#region Club methods

		/// <inheritdoc />
		public async Task<BaseJikanResponse<Club>> GetClubAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.Clubs, id.ToString() };
			return await ExecuteGetRequestAsync<BaseJikanResponse<Club>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<ClubMember>>> GetClubMembersAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.Clubs, id.ToString(), JikanEndpointConsts.Members };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<ClubMember>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<ClubMember>>> GetClubMembersAsync(long id, int page, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			Guard.IsGreaterThanZero(page, nameof(page));
			var queryParams = $"?page={page}";
			var endpointParts = new[] { JikanEndpointConsts.Clubs, id.ToString(), JikanEndpointConsts.Members + queryParams };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<ClubMember>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<ClubStaff>>> GetClubStaffAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.Clubs, id.ToString(), JikanEndpointConsts.Staff };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<ClubStaff>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ClubRelations>> GetClubRelationsAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { JikanEndpointConsts.Clubs, id.ToString(), JikanEndpointConsts.Relations };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ClubRelations>>(endpointParts, cancellationToken);
		}

		#endregion Club methods

		#region User methods

		/// <inheritdoc />
		public async Task<BaseJikanResponse<UserProfile>> GetUserProfileAsync(string username, CancellationToken cancellationToken = default)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			var endpointParts = new[] { JikanEndpointConsts.Users, username };
			return await ExecuteGetRequestAsync<BaseJikanResponse<UserProfile>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<UserStatistics>> GetUserStatisticsAsync(string username, CancellationToken cancellationToken = default)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			var endpointParts = new[] { JikanEndpointConsts.Users, username, JikanEndpointConsts.Statistics };
			return await ExecuteGetRequestAsync<BaseJikanResponse<UserStatistics>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<UserFavorites>> GetUserFavoritesAsync(string username, CancellationToken cancellationToken = default)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			var endpointParts = new[] { JikanEndpointConsts.Users, username, JikanEndpointConsts.Favorites };
			return await ExecuteGetRequestAsync<BaseJikanResponse<UserFavorites>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<UserUpdates>> GetUserUpdatesAsync(string username, CancellationToken cancellationToken = default)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			var endpointParts = new[] { JikanEndpointConsts.Users, username, JikanEndpointConsts.UserUpdates };
			return await ExecuteGetRequestAsync<BaseJikanResponse<UserUpdates>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<UserAbout>> GetUserAboutAsync(string username, CancellationToken cancellationToken = default)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			var endpointParts = new[] { JikanEndpointConsts.Users, username, JikanEndpointConsts.About };
			return await ExecuteGetRequestAsync<BaseJikanResponse<UserAbout>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<HistoryEntry>>> GetUserHistoryAsync(string username, CancellationToken cancellationToken = default)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			var endpointParts = new[] { JikanEndpointConsts.Users, username, JikanEndpointConsts.History };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<HistoryEntry>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<HistoryEntry>>> GetUserHistoryAsync(string username, UserHistoryExtension historyExtension, CancellationToken cancellationToken = default)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			Guard.IsValidEnum(historyExtension, nameof(historyExtension));
			var queryParams = $"?filter={historyExtension.GetDescription()}";
			var endpointParts = new[] { JikanEndpointConsts.Users, username, JikanEndpointConsts.History + queryParams };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<HistoryEntry>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<AnimeListEntry>>> GetUserAnimeListAsync(string username, CancellationToken cancellationToken = default)
		{
			Guard.IsDefaultEndpoint(_httpClient.BaseAddress.ToString(), nameof(GetUserAnimeListAsync));
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			var endpointParts = new[] { JikanEndpointConsts.Users, username, JikanEndpointConsts.AnimeList };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<AnimeListEntry>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<AnimeListEntry>>> GetUserAnimeListAsync(string username, int page, CancellationToken cancellationToken = default)
		{
			Guard.IsDefaultEndpoint(_httpClient.BaseAddress.ToString(), nameof(GetUserAnimeListAsync));
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			Guard.IsGreaterThanZero(page, nameof(page));
			var queryParams = $"?page={page}";
			var endpointParts = new[] { JikanEndpointConsts.Users, username, JikanEndpointConsts.AnimeList + queryParams };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<AnimeListEntry>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<MangaListEntry>>> GetUserMangaListAsync(string username, CancellationToken cancellationToken = default)
		{
			Guard.IsDefaultEndpoint(_httpClient.BaseAddress.ToString(), nameof(GetUserMangaListAsync));
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			var endpointParts = new[] { JikanEndpointConsts.Users, username, JikanEndpointConsts.MangaList };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<MangaListEntry>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<MangaListEntry>>> GetUserMangaListAsync(string username, int page, CancellationToken cancellationToken = default)
		{
			Guard.IsDefaultEndpoint(_httpClient.BaseAddress.ToString(), nameof(GetUserMangaListAsync));
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			Guard.IsGreaterThanZero(page, nameof(page));
			var queryParams = $"?page={page}";
			var endpointParts = new[] { JikanEndpointConsts.Users, username, JikanEndpointConsts.MangaList + queryParams };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<MangaListEntry>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Friend>>> GetUserFriendsAsync(string username, CancellationToken cancellationToken = default)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			var endpointParts = new[] { JikanEndpointConsts.Users, username, JikanEndpointConsts.Friends };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Friend>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Friend>>> GetUserFriendsAsync(string username, int page, CancellationToken cancellationToken = default)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			Guard.IsGreaterThanZero(page, nameof(page));
			var queryParams = $"?page={page}";
			var endpointParts = new[] { JikanEndpointConsts.Users, username, JikanEndpointConsts.Friends + queryParams };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Friend>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Review>>> GetUserReviewsAsync(string username, CancellationToken cancellationToken = default)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			var endpointParts = new[] { JikanEndpointConsts.Users, username, JikanEndpointConsts.Reviews };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Review>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Review>>> GetUserReviewsAsync(string username, int page, CancellationToken cancellationToken = default)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			Guard.IsGreaterThanZero(page, nameof(page));
			var queryParams = $"?page={page}";
			var endpointParts = new[] { JikanEndpointConsts.Users, username, JikanEndpointConsts.Reviews + queryParams };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Review>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<UserRecommendation>>> GetUserRecommendationsAsync(string username, CancellationToken cancellationToken = default)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			var endpointParts = new[] { JikanEndpointConsts.Users, username, JikanEndpointConsts.Recommendations };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<UserRecommendation>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<UserRecommendation>>> GetUserRecommendationsAsync(string username, int page, CancellationToken cancellationToken = default)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			Guard.IsGreaterThanZero(page, nameof(page));
			var queryParams = $"?page={page}";
			var endpointParts = new[] { JikanEndpointConsts.Users, username, JikanEndpointConsts.Recommendations + queryParams };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<UserRecommendation>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<MalUrl>>> GetUserClubsAsync(string username, CancellationToken cancellationToken = default)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			var endpointParts = new[] { JikanEndpointConsts.Users, username, JikanEndpointConsts.Clubs };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<MalUrl>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<MalUrl>>> GetUserClubsAsync(string username, int page, CancellationToken cancellationToken = default)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			Guard.IsGreaterThanZero(page, nameof(page));
			var queryParams = $"?page={page}";
			var endpointParts = new[] { JikanEndpointConsts.Users, username, JikanEndpointConsts.Clubs + queryParams };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<MalUrl>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<ICollection<ExternalLink>>> GetUserExternalLinksAsync(string username, CancellationToken cancellationToken = default)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			var endpointParts = new[] { JikanEndpointConsts.Users, username, JikanEndpointConsts.External };
			return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<ExternalLink>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<BaseJikanResponse<UserFull>> GetUserFullDataAsync(string username, CancellationToken cancellationToken = default)
		{
			Guard.IsNotNullOrWhiteSpace(username, nameof(username));
			var endpointParts = new[] { JikanEndpointConsts.Users, username, JikanEndpointConsts.Full };
			return await ExecuteGetRequestAsync<BaseJikanResponse<UserFull>>(endpointParts, cancellationToken);
		}

		#endregion User methods

		#region GetRandom methods

		/// <inheritdoc/>
		public async Task<BaseJikanResponse<Anime>> GetRandomAnimeAsync(CancellationToken cancellationToken = default)
		{
			var endpointParts = new[] { JikanEndpointConsts.Random, JikanEndpointConsts.Anime };
			return await ExecuteGetRequestAsync<BaseJikanResponse<Anime>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc/>
		public async Task<BaseJikanResponse<Manga>> GetRandomMangaAsync(CancellationToken cancellationToken = default)
		{
			var endpointParts = new[] { JikanEndpointConsts.Random, JikanEndpointConsts.Manga };
			return await ExecuteGetRequestAsync<BaseJikanResponse<Manga>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc/>
		public async Task<BaseJikanResponse<Character>> GetRandomCharacterAsync(CancellationToken cancellationToken = default)
		{
			var endpointParts = new[] { JikanEndpointConsts.Random, JikanEndpointConsts.Characters };
			return await ExecuteGetRequestAsync<BaseJikanResponse<Character>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc/>
		public async Task<BaseJikanResponse<Person>> GetRandomPersonAsync(CancellationToken cancellationToken = default)
		{
			var endpointParts = new[] { JikanEndpointConsts.Random, JikanEndpointConsts.People };
			return await ExecuteGetRequestAsync<BaseJikanResponse<Person>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc/>
		public async Task<BaseJikanResponse<UserProfile>> GetRandomUserAsync(CancellationToken cancellationToken = default)
		{
			var endpointParts = new[] { JikanEndpointConsts.Random, JikanEndpointConsts.Users };
			return await ExecuteGetRequestAsync<BaseJikanResponse<UserProfile>>(endpointParts, cancellationToken);
		}

		#endregion GetRandom methods
		
		#region Watch methods
		
		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<WatchEpisode>>> GetWatchRecentEpisodesAsync(CancellationToken cancellationToken = default)
		{
			var endpointParts = new[] { JikanEndpointConsts.Watch, JikanEndpointConsts.Episodes };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<WatchEpisode>>>(endpointParts, cancellationToken);
		}
		
		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<WatchEpisode>>> GetWatchPopularEpisodesAsync(CancellationToken cancellationToken = default)
		{
			var endpointParts = new[] { JikanEndpointConsts.Watch, JikanEndpointConsts.Episodes, JikanEndpointConsts.Popular };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<WatchEpisode>>>(endpointParts, cancellationToken);
		}
		
		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<WatchPromoVideo>>> GetWatchRecentPromosAsync(CancellationToken cancellationToken = default)
		{
			var endpointParts = new[] { JikanEndpointConsts.Watch, JikanEndpointConsts.Promos };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<WatchPromoVideo>>>(endpointParts, cancellationToken);
		}
		
		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<WatchPromoVideo>>> GetWatchPopularPromosAsync(CancellationToken cancellationToken = default)
		{
			var endpointParts = new[] { JikanEndpointConsts.Watch, JikanEndpointConsts.Promos, JikanEndpointConsts.Popular };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<WatchPromoVideo>>>(endpointParts, cancellationToken);
		}
		
		#endregion

		#region Reviews methods

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Review>>> GetRecentAnimeReviewsAsync(CancellationToken cancellationToken = default)
		{
			var endpointParts = new[] { JikanEndpointConsts.Reviews, JikanEndpointConsts.Anime };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Review>>>(endpointParts, cancellationToken);
		}
		
		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Review>>> GetRecentAnimeReviewsAsync(int page, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(page, nameof(page));
			var queryParams = $"?page={page}";
			var endpointParts = new[] { JikanEndpointConsts.Reviews, JikanEndpointConsts.Anime + queryParams };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Review>>>(endpointParts, cancellationToken);
		}
		
		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Review>>> GetRecentMangaReviewsAsync(CancellationToken cancellationToken = default)
		{
			var endpointParts = new[] { JikanEndpointConsts.Reviews, JikanEndpointConsts.Manga };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Review>>>(endpointParts, cancellationToken);
		}
		
		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Review>>> GetRecentMangaReviewsAsync(int page, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(page, nameof(page));
			var queryParams = $"?page={page}";
			var endpointParts = new[] { JikanEndpointConsts.Reviews, JikanEndpointConsts.Manga + queryParams };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Review>>>(endpointParts, cancellationToken);
		}
		
		#endregion
		
		#region Recommendations methods
		
		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<UserRecommendation>>> GetRecentAnimeRecommendationsAsync(CancellationToken cancellationToken = default)
		{
			var endpointParts = new[] { JikanEndpointConsts.Recommendations, JikanEndpointConsts.Anime };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<UserRecommendation>>>(endpointParts, cancellationToken);
		}
		
		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<UserRecommendation>>> GetRecentAnimeRecommendationsAsync(int page, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(page, nameof(page));
			var queryParams = $"?page={page}";
			var endpointParts = new[] { JikanEndpointConsts.Recommendations, JikanEndpointConsts.Anime + queryParams };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<UserRecommendation>>>(endpointParts, cancellationToken);
		}
		
		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<UserRecommendation>>> GetRecentMangaRecommendationsAsync(CancellationToken cancellationToken = default)
		{
			var endpointParts = new[] { JikanEndpointConsts.Recommendations, JikanEndpointConsts.Manga };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<UserRecommendation>>>(endpointParts, cancellationToken);
		}
		
		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<UserRecommendation>>> GetRecentMangaRecommendationsAsync(int page, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(page, nameof(page));
			var queryParams = $"?page={page}";
			var endpointParts = new[] { JikanEndpointConsts.Recommendations, JikanEndpointConsts.Manga + queryParams };
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<UserRecommendation>>>(endpointParts, cancellationToken);
		}
		
		#endregion

		#region Search methods
		
		
		
		/// <inheritdoc />
		public Task<PaginatedJikanResponse<ICollection<Anime>>> SearchAnimeAsync(string query, CancellationToken cancellationToken = default)
			=> SearchAnimeAsync(new AnimeSearchConfig {Query = query}, cancellationToken);
		
		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Anime>>> SearchAnimeAsync(AnimeSearchConfig searchConfig, CancellationToken cancellationToken = default)
		{
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			var endpointParts = new[] { JikanEndpointConsts.Anime + searchConfig.ConfigToString()};
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Anime>>>(endpointParts, cancellationToken);
		}
		
		/// <inheritdoc />
		public Task<PaginatedJikanResponse<ICollection<Manga>>> SearchMangaAsync(string query, CancellationToken cancellationToken = default)
			=> SearchMangaAsync(new MangaSearchConfig {Query = query}, cancellationToken);
		
		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Manga>>> SearchMangaAsync(MangaSearchConfig searchConfig, CancellationToken cancellationToken = default)
		{
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			var endpointParts = new[] { JikanEndpointConsts.Manga + searchConfig.ConfigToString()};
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Manga>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public Task<PaginatedJikanResponse<ICollection<Person>>> SearchPersonAsync(string query, CancellationToken cancellationToken = default)
			=> SearchPersonAsync(new PersonSearchConfig {Query = query}, cancellationToken);
		
		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Person>>> SearchPersonAsync(PersonSearchConfig searchConfig, CancellationToken cancellationToken = default)
		{	
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			var endpointParts = new[] { JikanEndpointConsts.People + searchConfig.ConfigToString()};
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Person>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public Task<PaginatedJikanResponse<ICollection<Character>>> SearchCharacterAsync(string query, CancellationToken cancellationToken = default)
			=> SearchCharacterAsync(new CharacterSearchConfig {Query = query}, cancellationToken);
		
		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Character>>> SearchCharacterAsync(CharacterSearchConfig searchConfig, CancellationToken cancellationToken = default)
		{
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			var endpointParts = new[] { JikanEndpointConsts.Characters + searchConfig.ConfigToString()};
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Character>>>(endpointParts, cancellationToken);
		}
	
		/// <inheritdoc />
		public  Task<PaginatedJikanResponse<ICollection<UserMetadata>>> SearchUserAsync(string query, CancellationToken cancellationToken = default) => SearchUserAsync(new UserSearchConfig {Query = query}, cancellationToken);

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<UserMetadata>>> SearchUserAsync(UserSearchConfig searchConfig, CancellationToken cancellationToken = default)
		{
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			var endpointParts = new[] {JikanEndpointConsts.Users + searchConfig.ConfigToString()};
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<UserMetadata>>>(endpointParts, cancellationToken);
		}
		
		/// <inheritdoc />
		public  Task<PaginatedJikanResponse<ICollection<Club>>> SearchClubAsync(string query, CancellationToken cancellationToken = default) => SearchClubAsync(new ClubSearchConfig {Query = query}, cancellationToken);

		/// <inheritdoc />
		public async Task<PaginatedJikanResponse<ICollection<Club>>> SearchClubAsync(ClubSearchConfig searchConfig, CancellationToken cancellationToken = default)
		{
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			var endpointParts = new[] {JikanEndpointConsts.Users + searchConfig.ConfigToString()};
			return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<Club>>>(endpointParts, cancellationToken);
		}

		#endregion Search methods

		#endregion Public Methods
	}
}
