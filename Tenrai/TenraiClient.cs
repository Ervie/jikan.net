using Tenrai.Config;
using Tenrai.Consts;
using Tenrai.Exceptions;
using Tenrai.Extensions;
using Tenrai.Helpers;
using Tenrai.Limiter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Tenrai
{
	/// <summary>
	/// Implementation of Tenrai wrapper for .Net platform.
	/// </summary>
	public class TenraiClient : ITenrai
	{
		#region Fields

		/// <summary>
		/// Http client class to call REST request and receive REST response.
		/// </summary>
		private readonly HttpClient _httpClient;

		/// <summary>
		/// Client configuration.
		/// </summary>
		private readonly TenraiClientConfiguration _tenraiConfiguration;

		/// <summary>
		/// API call limiter
		/// </summary>
		private readonly ITaskLimiter _limiter;

		#endregion Fields

		#region Constructors

		/// <summary>
		/// Constructor.
		/// </summary>
		public TenraiClient() : this(new TenraiClientConfiguration()) { }
		
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="tenraiClientConfiguration">Options.</param>
		/// <param name="httpClient">Http client.</param>
		public TenraiClient(TenraiClientConfiguration tenraiClientConfiguration, HttpClient httpClient = null)
		{
			_tenraiConfiguration = tenraiClientConfiguration;
			_limiter = new CompositeTaskLimiter(ResolveLimiterConfigurations(tenraiClientConfiguration).Distinct());
			_httpClient = httpClient ?? DefaultHttpClientProvider.GetDefaultHttpClient(serverKey: tenraiClientConfiguration.ServerKey);
		}

		/// <summary>
		/// Resolves the limiter configuration to use, upgrading to the Server Key tier when a server key
		/// is supplied and the caller has not overridden the default limiter configuration.
		/// </summary>
		private static IEnumerable<TaskLimiterConfiguration> ResolveLimiterConfigurations(TenraiClientConfiguration configuration)
		{
			if (!string.IsNullOrWhiteSpace(configuration.ServerKey)
				&& ReferenceEquals(configuration.LimiterConfigurations, TaskLimiterConfiguration.Default))
			{
				return TaskLimiterConfiguration.ServerKey;
			}

			return configuration.LimiterConfigurations ?? TaskLimiterConfiguration.None;
		}

        #endregion Constructors

        #region Private Methods

        /// <summary>
        /// Basic method for handling requests and responses from endpoint.
        /// </summary>
        /// <typeparam name="T">Class type received from GET requests.</typeparam>
        /// <param name="routeSections">Arguments building endpoint.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <param name="bypassLimiter">Skip the rate limiter. Only for calls that do not hit the Tenrai API itself.</param>
        /// <returns>Requested object if successful, null otherwise.</returns>
        private async Task<T> ExecuteGetRequestAsync<T>(ICollection<string> routeSections, CancellationToken cancellationToken = default, bool bypassLimiter = false) where T : class
		{
			T returnedObject = null;
			var requestUrl = string.Join("/", routeSections);
			try
			{
				using var response = bypassLimiter
					? await _httpClient.GetAsync(requestUrl, cancellationToken)
					: await _limiter.LimitAsync(() => _httpClient.GetAsync(requestUrl, cancellationToken));
				if (response.IsSuccessStatusCode)
				{
					var json = await response.Content.ReadAsStringAsync();

					returnedObject = JsonSerializer.Deserialize<T>(json);
				}
				else if (!_tenraiConfiguration.SuppressException)
				{
					var json = await response.Content.ReadAsStringAsync();
					var errorData = JsonSerializer.Deserialize<TenraiApiError>(json);
					throw new TenraiRequestException(string.Format(ErrorMessagesConst.FailedRequest, response.StatusCode, response.Content), errorData);
				}
			}
			catch (JsonException ex)
			{
				if (!_tenraiConfiguration.SuppressException)
				{
					throw new TenraiRequestException(ErrorMessagesConst.SerializationFailed + Environment.NewLine + "Inner exception message: " + ex.Message, ex);
				}
			}
			return returnedObject;
		}

        #endregion Private Methods

		#region Public Methods

		#region Anime methods

		/// <inheritdoc />
		public async Task<BaseTenraiResponse<Anime>> GetAnimeAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { TenraiEndpointConsts.Anime, id.ToString() };
			return await ExecuteGetRequestAsync<BaseTenraiResponse<Anime>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<BaseTenraiResponse<ICollection<AnimeCharacter>>> GetAnimeCharactersAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { TenraiEndpointConsts.Anime, id.ToString(), TenraiEndpointConsts.Characters };
			return await ExecuteGetRequestAsync<BaseTenraiResponse<ICollection<AnimeCharacter>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<BaseTenraiResponse<ICollection<AnimeStaffPosition>>> GetAnimeStaffAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { TenraiEndpointConsts.Anime, id.ToString(), TenraiEndpointConsts.Staff };
			return await ExecuteGetRequestAsync<BaseTenraiResponse<ICollection<AnimeStaffPosition>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<PaginatedTenraiResponse<ICollection<AnimeEpisode>>> GetAnimeEpisodesAsync(long id, int page, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			Guard.IsGreaterThanZero(page, nameof(page));
			var endpointParts = new[] { TenraiEndpointConsts.Anime, id.ToString(), TenraiEndpointConsts.Episodes + $"?page={page}" };
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<AnimeEpisode>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<PaginatedTenraiResponse<ICollection<AnimeEpisode>>> GetAnimeEpisodesAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { TenraiEndpointConsts.Anime, id.ToString(), TenraiEndpointConsts.Episodes };
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<AnimeEpisode>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<BaseTenraiResponse<AnimeEpisode>> GetAnimeEpisodeAsync(long animeId, int episodeId, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(animeId, nameof(animeId));
			Guard.IsGreaterThanZero(episodeId, nameof(episodeId));
			var endpointParts = new[] { TenraiEndpointConsts.Anime, animeId.ToString(), TenraiEndpointConsts.Episodes, episodeId.ToString() };
			return await ExecuteGetRequestAsync<BaseTenraiResponse<AnimeEpisode>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<PaginatedTenraiResponse<ICollection<News>>> GetAnimeNewsAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { TenraiEndpointConsts.Anime, id.ToString(), TenraiEndpointConsts.News };
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<News>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<PaginatedTenraiResponse<ICollection<News>>> GetAnimeNewsAsync(long id, int page, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			Guard.IsGreaterThanZero(page, nameof(page));
			var endpointParts = new[] { TenraiEndpointConsts.Anime, id.ToString(), TenraiEndpointConsts.News + $"?page={page}" };
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<News>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		[Obsolete(ErrorMessagesConst.UnsupportedForum)]
		public Task<BaseTenraiResponse<ICollection<ForumTopic>>> GetAnimeForumTopicsAsync(long id, CancellationToken cancellationToken = default)
			=> throw new NotSupportedException(ErrorMessagesConst.UnsupportedForum);

		/// <inheritdoc />
		[Obsolete(ErrorMessagesConst.UnsupportedForum)]
		public Task<BaseTenraiResponse<ICollection<ForumTopic>>> GetAnimeForumTopicsAsync(long id, ForumTopicType type, CancellationToken cancellationToken = default)
			=> throw new NotSupportedException(ErrorMessagesConst.UnsupportedForum);

		/// <inheritdoc />
		public async Task<BaseTenraiResponse<AnimeVideos>> GetAnimeVideosAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { TenraiEndpointConsts.Anime, id.ToString(), TenraiEndpointConsts.Videos };
			return await ExecuteGetRequestAsync<BaseTenraiResponse<AnimeVideos>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<PaginatedTenraiResponse<ICollection<EpisodeVideo>>> GetAnimeVideosEpisodesAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { TenraiEndpointConsts.Anime, id.ToString(), TenraiEndpointConsts.Videos, TenraiEndpointConsts.Episodes };
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<EpisodeVideo>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<PaginatedTenraiResponse<ICollection<EpisodeVideo>>> GetAnimeVideosEpisodesAsync(long id, int page, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			Guard.IsGreaterThanZero(page, nameof(page));
			var queryParams = $"?page={page}";
			var endpointParts = new[] { TenraiEndpointConsts.Anime, id.ToString(), TenraiEndpointConsts.Videos, TenraiEndpointConsts.Episodes + queryParams };
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<EpisodeVideo>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<BaseTenraiResponse<ICollection<ImagesSet>>> GetAnimePicturesAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { TenraiEndpointConsts.Anime, id.ToString(), TenraiEndpointConsts.Pictures };
			return await ExecuteGetRequestAsync<BaseTenraiResponse<ICollection<ImagesSet>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<BaseTenraiResponse<AnimeStatistics>> GetAnimeStatisticsAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { TenraiEndpointConsts.Anime, id.ToString(), TenraiEndpointConsts.Statistics };
			return await ExecuteGetRequestAsync<BaseTenraiResponse<AnimeStatistics>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<BaseTenraiResponse<MoreInfo>> GetAnimeMoreInfoAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { TenraiEndpointConsts.Anime, id.ToString(), TenraiEndpointConsts.MoreInfo };
			return await ExecuteGetRequestAsync<BaseTenraiResponse<MoreInfo>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<BaseTenraiResponse<ICollection<Recommendation>>> GetAnimeRecommendationsAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { TenraiEndpointConsts.Anime, id.ToString(), TenraiEndpointConsts.Recommendations };
			return await ExecuteGetRequestAsync<BaseTenraiResponse<ICollection<Recommendation>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		[Obsolete(ErrorMessagesConst.UnsupportedUserUpdates)]
		public Task<PaginatedTenraiResponse<ICollection<AnimeUserUpdate>>> GetAnimeUserUpdatesAsync(long id, CancellationToken cancellationToken = default)
			=> throw new NotSupportedException(ErrorMessagesConst.UnsupportedUserUpdates);

		/// <inheritdoc />
		[Obsolete(ErrorMessagesConst.UnsupportedUserUpdates)]
		public Task<PaginatedTenraiResponse<ICollection<AnimeUserUpdate>>> GetAnimeUserUpdatesAsync(long id, int page, CancellationToken cancellationToken = default)
			=> throw new NotSupportedException(ErrorMessagesConst.UnsupportedUserUpdates);

		/// <inheritdoc />
		public async Task<PaginatedTenraiResponse<ICollection<Review>>> GetAnimeReviewsAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { TenraiEndpointConsts.Anime, id.ToString(), TenraiEndpointConsts.Reviews };
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<Review>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<PaginatedTenraiResponse<ICollection<Review>>> GetAnimeReviewsAsync(long id, ReviewsSearchConfig searchConfig, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			var endpointParts = new[] { TenraiEndpointConsts.Anime, id.ToString(), TenraiEndpointConsts.Reviews + searchConfig.ConfigToString() };
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<Review>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<PaginatedTenraiResponse<ICollection<RelatedEntry>>> GetAnimeRelationsAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { TenraiEndpointConsts.Anime, id.ToString(), TenraiEndpointConsts.Relations };
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<RelatedEntry>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<BaseTenraiResponse<AnimeThemes>> GetAnimeThemesAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { TenraiEndpointConsts.Anime, id.ToString(), TenraiEndpointConsts.Themes };
			return await ExecuteGetRequestAsync<BaseTenraiResponse<AnimeThemes>>(endpointParts, cancellationToken);
		}
		
		/// <inheritdoc />
		public async Task<BaseTenraiResponse<ICollection<ExternalLink>>> GetAnimeExternalLinksAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { TenraiEndpointConsts.Anime, id.ToString(), TenraiEndpointConsts.External };
			return await ExecuteGetRequestAsync<BaseTenraiResponse<ICollection<ExternalLink>>>(endpointParts, cancellationToken);
		}
		
		/// <inheritdoc />
		public async Task<BaseTenraiResponse<ICollection<ExternalLink>>> GetAnimeStreamingLinksAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { TenraiEndpointConsts.Anime, id.ToString(), TenraiEndpointConsts.Streaming };
			return await ExecuteGetRequestAsync<BaseTenraiResponse<ICollection<ExternalLink>>>(endpointParts, cancellationToken);
		}
		
		/// <inheritdoc />
		public async Task<BaseTenraiResponse<AnimeFull>> GetAnimeFullDataAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { TenraiEndpointConsts.Anime, id.ToString(), TenraiEndpointConsts.Full };
			return await ExecuteGetRequestAsync<BaseTenraiResponse<AnimeFull>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<BaseTenraiResponse<ICollection<long>>> GetAnimeIdsAsync(CancellationToken cancellationToken = default)
		{
			var endpointParts = new[] { TenraiEndpointConsts.Anime, TenraiEndpointConsts.Ids };
			return await ExecuteGetRequestAsync<BaseTenraiResponse<ICollection<long>>>(endpointParts, cancellationToken);
		}

		#endregion Anime methods

		#region Character methods

		/// <inheritdoc />
		public async Task<BaseTenraiResponse<Character>> GetCharacterAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { TenraiEndpointConsts.Characters, id.ToString() };
			return await ExecuteGetRequestAsync<BaseTenraiResponse<Character>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<BaseTenraiResponse<ICollection<CharacterAnimeographyEntry>>> GetCharacterAnimeAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { TenraiEndpointConsts.Characters, id.ToString(), TenraiEndpointConsts.Anime };
			return await ExecuteGetRequestAsync<BaseTenraiResponse<ICollection<CharacterAnimeographyEntry>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<BaseTenraiResponse<ICollection<CharacterMangaographyEntry>>> GetCharacterMangaAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { TenraiEndpointConsts.Characters, id.ToString(), TenraiEndpointConsts.Manga };
			return await ExecuteGetRequestAsync<BaseTenraiResponse<ICollection<CharacterMangaographyEntry>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<BaseTenraiResponse<ICollection<VoiceActorEntry>>> GetCharacterVoiceActorsAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { TenraiEndpointConsts.Characters, id.ToString(), TenraiEndpointConsts.Voices };
			return await ExecuteGetRequestAsync<BaseTenraiResponse<ICollection<VoiceActorEntry>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<BaseTenraiResponse<ICollection<ImagesSet>>> GetCharacterPicturesAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { TenraiEndpointConsts.Characters, id.ToString(), TenraiEndpointConsts.Pictures };
			return await ExecuteGetRequestAsync<BaseTenraiResponse<ICollection<ImagesSet>>>(endpointParts, cancellationToken);
		}
		
		/// <inheritdoc />
		public async Task<BaseTenraiResponse<CharacterFull>> GetCharacterFullDataAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { TenraiEndpointConsts.Characters, id.ToString(), TenraiEndpointConsts.Full };
			return await ExecuteGetRequestAsync<BaseTenraiResponse<CharacterFull>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<BaseTenraiResponse<ICollection<long>>> GetCharacterIdsAsync(CancellationToken cancellationToken = default)
		{
			var endpointParts = new[] { TenraiEndpointConsts.Characters, TenraiEndpointConsts.Ids };
			return await ExecuteGetRequestAsync<BaseTenraiResponse<ICollection<long>>>(endpointParts, cancellationToken);
		}

		#endregion Character methods

		#region Manga methods

		/// <inheritdoc />
		public async Task<BaseTenraiResponse<Manga>> GetMangaAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { TenraiEndpointConsts.Manga, id.ToString() };
			return await ExecuteGetRequestAsync<BaseTenraiResponse<Manga>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<BaseTenraiResponse<ICollection<MangaCharacter>>> GetMangaCharactersAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { TenraiEndpointConsts.Manga, id.ToString(), TenraiEndpointConsts.Characters };
			return await ExecuteGetRequestAsync<BaseTenraiResponse<ICollection<MangaCharacter>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<PaginatedTenraiResponse<ICollection<News>>> GetMangaNewsAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { TenraiEndpointConsts.Manga, id.ToString(), TenraiEndpointConsts.News };
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<News>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<PaginatedTenraiResponse<ICollection<News>>> GetMangaNewsAsync(long id, int page, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			Guard.IsGreaterThanZero(page, nameof(page));

			var queryParams = $"?page={page}";
			var endpointParts = new[] { TenraiEndpointConsts.Manga, id.ToString(), TenraiEndpointConsts.News + queryParams };
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<News>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		[Obsolete(ErrorMessagesConst.UnsupportedForum)]
		public Task<BaseTenraiResponse<ICollection<ForumTopic>>> GetMangaForumTopicsAsync(long id, CancellationToken cancellationToken = default)
			=> throw new NotSupportedException(ErrorMessagesConst.UnsupportedForum);

		/// <inheritdoc />
		[Obsolete(ErrorMessagesConst.UnsupportedForum)]
		public Task<BaseTenraiResponse<ICollection<ForumTopic>>> GetMangaForumTopicsAsync(long id, ForumTopicType type, CancellationToken cancellationToken = default)
			=> throw new NotSupportedException(ErrorMessagesConst.UnsupportedForum);

		/// <inheritdoc />
		public async Task<BaseTenraiResponse<ICollection<ImagesSet>>> GetMangaPicturesAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { TenraiEndpointConsts.Manga, id.ToString(), TenraiEndpointConsts.Pictures };
			return await ExecuteGetRequestAsync<BaseTenraiResponse<ICollection<ImagesSet>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<BaseTenraiResponse<MangaStatistics>> GetMangaStatisticsAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { TenraiEndpointConsts.Manga, id.ToString(), TenraiEndpointConsts.Statistics };
			return await ExecuteGetRequestAsync<BaseTenraiResponse<MangaStatistics>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<BaseTenraiResponse<MoreInfo>> GetMangaMoreInfoAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { TenraiEndpointConsts.Manga, id.ToString(), TenraiEndpointConsts.MoreInfo };
			return await ExecuteGetRequestAsync<BaseTenraiResponse<MoreInfo>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		[Obsolete(ErrorMessagesConst.UnsupportedUserUpdates)]
		public Task<PaginatedTenraiResponse<ICollection<MangaUserUpdate>>> GetMangaUserUpdatesAsync(long id, CancellationToken cancellationToken = default)
			=> throw new NotSupportedException(ErrorMessagesConst.UnsupportedUserUpdates);

		/// <inheritdoc />
		[Obsolete(ErrorMessagesConst.UnsupportedUserUpdates)]
		public Task<PaginatedTenraiResponse<ICollection<MangaUserUpdate>>> GetMangaUserUpdatesAsync(long id, int page, CancellationToken cancellationToken = default)
			=> throw new NotSupportedException(ErrorMessagesConst.UnsupportedUserUpdates);

		/// <inheritdoc />
		public async Task<BaseTenraiResponse<ICollection<Recommendation>>> GetMangaRecommendationsAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { TenraiEndpointConsts.Manga, id.ToString(), TenraiEndpointConsts.Recommendations };
			return await ExecuteGetRequestAsync<BaseTenraiResponse<ICollection<Recommendation>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<PaginatedTenraiResponse<ICollection<Review>>> GetMangaReviewsAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new List<string>() {TenraiEndpointConsts.Manga, id.ToString(), TenraiEndpointConsts.Reviews};
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<Review>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<PaginatedTenraiResponse<ICollection<Review>>> GetMangaReviewsAsync(long id, ReviewsSearchConfig searchConfig, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			var endpointParts = new List<string>() {TenraiEndpointConsts.Manga, id.ToString(), TenraiEndpointConsts.Reviews + searchConfig.ConfigToString()};
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<Review>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<PaginatedTenraiResponse<ICollection<RelatedEntry>>> GetMangaRelationsAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { TenraiEndpointConsts.Manga, id.ToString(), TenraiEndpointConsts.Relations };
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<RelatedEntry>>>(endpointParts, cancellationToken);
		}
		
		/// <inheritdoc />
		public async Task<BaseTenraiResponse<ICollection<ExternalLink>>> GetMangaExternalLinksAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { TenraiEndpointConsts.Manga, id.ToString(), TenraiEndpointConsts.External };
			return await ExecuteGetRequestAsync<BaseTenraiResponse<ICollection<ExternalLink>>>(endpointParts, cancellationToken);
		}
		
		/// <inheritdoc />
		public async Task<BaseTenraiResponse<MangaFull>> GetMangaFullDataAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { TenraiEndpointConsts.Manga, id.ToString(), TenraiEndpointConsts.Full };
			return await ExecuteGetRequestAsync<BaseTenraiResponse<MangaFull>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<BaseTenraiResponse<ICollection<long>>> GetMangaIdsAsync(CancellationToken cancellationToken = default)
		{
			var endpointParts = new[] { TenraiEndpointConsts.Manga, TenraiEndpointConsts.Ids };
			return await ExecuteGetRequestAsync<BaseTenraiResponse<ICollection<long>>>(endpointParts, cancellationToken);
		}

		#endregion Manga methods

		#region Person methods

		/// <inheritdoc />
		public async Task<BaseTenraiResponse<Person>> GetPersonAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { TenraiEndpointConsts.People, id.ToString() };
			return await ExecuteGetRequestAsync<BaseTenraiResponse<Person>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<BaseTenraiResponse<ICollection<PersonAnimeographyEntry>>> GetPersonAnimeAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { TenraiEndpointConsts.People, id.ToString(), TenraiEndpointConsts.Anime };
			return await ExecuteGetRequestAsync<BaseTenraiResponse<ICollection<PersonAnimeographyEntry>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<BaseTenraiResponse<ICollection<PersonMangaographyEntry>>> GetPersonMangaAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { TenraiEndpointConsts.People, id.ToString(), TenraiEndpointConsts.Manga };
			return await ExecuteGetRequestAsync<BaseTenraiResponse<ICollection<PersonMangaographyEntry>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<BaseTenraiResponse<ICollection<VoiceActingRole>>> GetPersonVoiceActingRolesAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { TenraiEndpointConsts.People, id.ToString(), TenraiEndpointConsts.Voices };
			return await ExecuteGetRequestAsync<BaseTenraiResponse<ICollection<VoiceActingRole>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<BaseTenraiResponse<ICollection<ImagesSet>>> GetPersonPicturesAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { TenraiEndpointConsts.People, id.ToString(), TenraiEndpointConsts.Pictures };
			return await ExecuteGetRequestAsync<BaseTenraiResponse<ICollection<ImagesSet>>>(endpointParts, cancellationToken);
		}
		
		/// <inheritdoc />
		public async Task<BaseTenraiResponse<PersonFull>> GetPersonFullDataAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { TenraiEndpointConsts.People, id.ToString(), TenraiEndpointConsts.Full };
			return await ExecuteGetRequestAsync<BaseTenraiResponse<PersonFull>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<BaseTenraiResponse<ICollection<long>>> GetPersonIdsAsync(CancellationToken cancellationToken = default)
		{
			var endpointParts = new[] { TenraiEndpointConsts.People, TenraiEndpointConsts.Ids };
			return await ExecuteGetRequestAsync<BaseTenraiResponse<ICollection<long>>>(endpointParts, cancellationToken);
		}

		#endregion Person methods

		#region Season methods

		/// <inheritdoc />
		public async Task<PaginatedTenraiResponse<ICollection<Anime>>> GetSeasonAsync(int year, Season season, CancellationToken cancellationToken = default)
		{
			Guard.IsValid(x => x is >= 1000 and < 10000, year, nameof(year));
			Guard.IsValidEnum(season, nameof(season));
			var endpointParts = new[] { TenraiEndpointConsts.Seasons, year.ToString(), season.GetDescription() };
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<Anime>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<PaginatedTenraiResponse<ICollection<Anime>>> GetSeasonAsync(int year, Season season, int page, CancellationToken cancellationToken = default)
		{
			Guard.IsValid(x => x is >= 1000 and < 10000, year, nameof(year));
			Guard.IsValidEnum(season, nameof(season));
			Guard.IsGreaterThanZero(page, nameof(page));
			var queryParams = $"?page={page}";
			var endpointParts = new[] { TenraiEndpointConsts.Seasons, year.ToString(), season.GetDescription() + queryParams};
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<Anime>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<PaginatedTenraiResponse<ICollection<SeasonArchive>>> GetSeasonArchiveAsync(CancellationToken cancellationToken = default)
		{
			var endpointParts = new[] { TenraiEndpointConsts.Seasons };
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<SeasonArchive>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<PaginatedTenraiResponse<ICollection<Anime>>> GetCurrentSeasonAsync(CancellationToken cancellationToken = default)
		{
			var endpointParts = new[] { TenraiEndpointConsts.Seasons, TenraiEndpointConsts.Now };
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<Anime>>>(endpointParts, cancellationToken);
		}
		
		/// <inheritdoc />
		public async Task<PaginatedTenraiResponse<ICollection<Anime>>> GetCurrentSeasonAsync(int page, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(page, nameof(page));
			var queryParams = $"?page={page}";
			var endpointParts = new[] { TenraiEndpointConsts.Seasons, TenraiEndpointConsts.Now + queryParams};
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<Anime>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<PaginatedTenraiResponse<ICollection<Anime>>> GetUpcomingSeasonAsync(CancellationToken cancellationToken = default)
		{
			var endpointParts = new[] { TenraiEndpointConsts.Seasons, TenraiEndpointConsts.Upcoming };
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<Anime>>>(endpointParts, cancellationToken);
		}
		
		/// <inheritdoc />
		public async Task<PaginatedTenraiResponse<ICollection<Anime>>> GetUpcomingSeasonAsync(int page, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(page, nameof(page));
			var queryParams = $"?page={page}";
			var endpointParts = new[] { TenraiEndpointConsts.Seasons, TenraiEndpointConsts.Upcoming + queryParams};
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<Anime>>>(endpointParts, cancellationToken);
		}

		#endregion Season methods

		#region Schedule methods

		/// <inheritdoc />
		public async Task<PaginatedTenraiResponse<ICollection<Anime>>> GetScheduleAsync(CancellationToken cancellationToken = default)
		{
			var endpointParts = new[] { TenraiEndpointConsts.Schedules };
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<Anime>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<PaginatedTenraiResponse<ICollection<Anime>>> GetScheduleAsync(int page, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(page, nameof(page));
			var queryParams = $"?page={page}";
			var endpointParts = new[] { TenraiEndpointConsts.Schedules + queryParams };
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<Anime>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<PaginatedTenraiResponse<ICollection<Anime>>> GetScheduleAsync(ScheduledDay scheduledDay, CancellationToken cancellationToken = default)
		{
			Guard.IsValidEnum(scheduledDay, nameof(scheduledDay));
			var queryParams = $"?filter={scheduledDay.GetDescription()}";
			var endpointParts = new[] { TenraiEndpointConsts.Schedules + queryParams };
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<Anime>>>(endpointParts, cancellationToken);
		}

		#endregion Schedule methods

		#region Top methods

		/// <inheritdoc />
		public async Task<PaginatedTenraiResponse<ICollection<Anime>>> GetTopAnimeAsync(CancellationToken cancellationToken = default)
		{
			var endpointParts = new[] { TenraiEndpointConsts.TopList, TenraiEndpointConsts.Anime };
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<Anime>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<PaginatedTenraiResponse<ICollection<Anime>>> GetTopAnimeAsync(int page, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(page, nameof(page));
			var queryParams = $"?page={page}";
			var endpointParts = new[] { TenraiEndpointConsts.TopList, TenraiEndpointConsts.Anime + queryParams };
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<Anime>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<PaginatedTenraiResponse<ICollection<Anime>>> GetTopAnimeAsync(TopAnimeFilter filter, CancellationToken cancellationToken = default)
		{
			Guard.IsValidEnum(filter, nameof(filter));
			var queryParams = $"?filter={filter.GetDescription()}";
			var endpointParts = new[] { TenraiEndpointConsts.TopList, TenraiEndpointConsts.Anime + queryParams };
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<Anime>>>(endpointParts, cancellationToken);
		}
		
        /// <inheritdoc />
        public async Task<PaginatedTenraiResponse<ICollection<Anime>>> GetTopAnimeAsync(TopAnimeFilter filter, int page, CancellationToken cancellationToken = default)
        {
	        Guard.IsValidEnum(filter, nameof(filter));
            Guard.IsGreaterThanZero(page, nameof(page));
            var queryParams = $"?page={page}&filter={filter.GetDescription()}";
            var endpointParts = new[] { TenraiEndpointConsts.TopList, TenraiEndpointConsts.Anime + queryParams };
            return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<Anime>>>(endpointParts, cancellationToken);
        }
		
        /// <inheritdoc />
        public async Task<PaginatedTenraiResponse<ICollection<Anime>>> GetTopAnimeAsync(AnimeTopSearchConfig searchConfig, CancellationToken cancellationToken = default)
        {
	        Guard.IsNotNull(searchConfig, nameof(searchConfig));
	        var endpointParts = new[] { TenraiEndpointConsts.TopList, TenraiEndpointConsts.Anime + searchConfig.ConfigToString()};
	        return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<Anime>>>(endpointParts, cancellationToken);
        }
        
        /// <inheritdoc />
        public async Task<PaginatedTenraiResponse<ICollection<Manga>>> GetTopMangaAsync(CancellationToken cancellationToken = default)
		{
			var endpointParts = new[] { TenraiEndpointConsts.TopList, TenraiEndpointConsts.Manga };
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<Manga>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<PaginatedTenraiResponse<ICollection<Manga>>> GetTopMangaAsync(int page, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(page, nameof(page));
			var queryParams = $"?page={page}";
			var endpointParts = new[] { TenraiEndpointConsts.TopList, TenraiEndpointConsts.Manga + queryParams };
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<Manga>>>(endpointParts, cancellationToken);
		}
		
		/// <inheritdoc />
		public async Task<PaginatedTenraiResponse<ICollection<Manga>>> GetTopMangaAsync(MangaTopSearchConfig searchConfig, CancellationToken cancellationToken = default)
		{
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			var endpointParts = new[] { TenraiEndpointConsts.TopList, TenraiEndpointConsts.Manga + searchConfig.ConfigToString()};
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<Manga>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<PaginatedTenraiResponse<ICollection<Person>>> GetTopPeopleAsync(CancellationToken cancellationToken = default)
		{
			var endpointParts = new[] { TenraiEndpointConsts.TopList, TenraiEndpointConsts.People };
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<Person>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<PaginatedTenraiResponse<ICollection<Person>>> GetTopPeopleAsync(int page, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(page, nameof(page));
			var queryParams = $"?page={page}";
			var endpointParts = new[] { TenraiEndpointConsts.TopList, TenraiEndpointConsts.People + queryParams };
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<Person>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<PaginatedTenraiResponse<ICollection<Character>>> GetTopCharactersAsync(CancellationToken cancellationToken = default)
		{
			var endpointParts = new[] { TenraiEndpointConsts.TopList, TenraiEndpointConsts.Characters };
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<Character>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<PaginatedTenraiResponse<ICollection<Character>>> GetTopCharactersAsync(int page, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(page, nameof(page));
			var queryParams = $"?page={page}";
			var endpointParts = new[] { TenraiEndpointConsts.TopList, TenraiEndpointConsts.Characters + queryParams };
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<Character>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<PaginatedTenraiResponse<ICollection<Review>>> GetTopReviewsAsync(CancellationToken cancellationToken = default)
		{
			var endpointParts = new[] { TenraiEndpointConsts.TopList, TenraiEndpointConsts.Reviews };
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<Review>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<PaginatedTenraiResponse<ICollection<Review>>> GetTopReviewsAsync(ReviewsSearchConfig searchConfig, CancellationToken cancellationToken = default)
		{
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			var endpointParts = new[] { TenraiEndpointConsts.TopList, TenraiEndpointConsts.Reviews + searchConfig.ConfigToString() };
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<Review>>>(endpointParts, cancellationToken);
		}

		#endregion Top methods

		#region Genre methods

		/// <inheritdoc />
		public async Task<BaseTenraiResponse<ICollection<Genre>>> GetAnimeGenresAsync(CancellationToken cancellationToken = default)
		{
			var endpointParts = new[] { TenraiEndpointConsts.Genres, TenraiEndpointConsts.Anime };
			return await ExecuteGetRequestAsync<BaseTenraiResponse<ICollection<Genre>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<BaseTenraiResponse<ICollection<Genre>>> GetAnimeGenresAsync(GenresFilter filter, CancellationToken cancellationToken = default)
		{
			Guard.IsValidEnum(filter, nameof(filter));
			var queryParams = $"?filter={filter.GetDescription()}";
			var endpointParts = new[] { TenraiEndpointConsts.Genres, TenraiEndpointConsts.Anime + queryParams };
			return await ExecuteGetRequestAsync<BaseTenraiResponse<ICollection<Genre>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<BaseTenraiResponse<ICollection<Genre>>> GetMangaGenresAsync(CancellationToken cancellationToken = default)
		{
			var endpointParts = new[] { TenraiEndpointConsts.Genres, TenraiEndpointConsts.Manga };
			return await ExecuteGetRequestAsync<BaseTenraiResponse<ICollection<Genre>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<BaseTenraiResponse<ICollection<Genre>>> GetMangaGenresAsync(GenresFilter filter, CancellationToken cancellationToken = default)
		{
			Guard.IsValidEnum(filter, nameof(filter));
			var queryParams = $"?filter={filter.GetDescription()}";
			var endpointParts = new[] { TenraiEndpointConsts.Genres, TenraiEndpointConsts.Manga + queryParams };
			return await ExecuteGetRequestAsync<BaseTenraiResponse<ICollection<Genre>>>(endpointParts, cancellationToken);
		}

		#endregion Genre methods

		#region Producer methods

		/// <inheritdoc />
		public async Task<PaginatedTenraiResponse<ICollection<Producer>>> GetProducersAsync(CancellationToken cancellationToken = default)
		{
			var endpointParts = new[] { TenraiEndpointConsts.Producers };
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<Producer>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<PaginatedTenraiResponse<ICollection<Producer>>> GetProducersAsync(int page, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(page, nameof(page));
			var queryParams = $"?page={page}";
			var endpointParts = new[] { TenraiEndpointConsts.Producers + queryParams };
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<Producer>>>(endpointParts, cancellationToken);
		}
		
		/// <inheritdoc />
		public async Task<BaseTenraiResponse<Producer>> GetProducerAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { TenraiEndpointConsts.Producers, id.ToString() };
			return await ExecuteGetRequestAsync<BaseTenraiResponse<Producer>>(endpointParts, cancellationToken);
		}
		
		/// <inheritdoc />
		public async Task<BaseTenraiResponse<ICollection<ExternalLink>>> GetProducerExternalLinksAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { TenraiEndpointConsts.Producers, id.ToString(), TenraiEndpointConsts.External };
			return await ExecuteGetRequestAsync<BaseTenraiResponse<ICollection<ExternalLink>>>(endpointParts, cancellationToken);
		}
		
		/// <inheritdoc />
		public async Task<BaseTenraiResponse<ProducerFull>> GetProducerFullDataAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { TenraiEndpointConsts.Producers, id.ToString(), TenraiEndpointConsts.Full };
			return await ExecuteGetRequestAsync<BaseTenraiResponse<ProducerFull>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<BaseTenraiResponse<ICollection<long>>> GetProducerIdsAsync(CancellationToken cancellationToken = default)
		{
			var endpointParts = new[] { TenraiEndpointConsts.Producers, TenraiEndpointConsts.Ids };
			return await ExecuteGetRequestAsync<BaseTenraiResponse<ICollection<long>>>(endpointParts, cancellationToken);
		}

		#endregion Producer methods

		#region Magazine methods

		/// <inheritdoc />
		public async Task<PaginatedTenraiResponse<ICollection<Magazine>>> GetMagazinesAsync(CancellationToken cancellationToken = default)
		{
			var endpointParts = new[] { TenraiEndpointConsts.Magazines };
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<Magazine>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<PaginatedTenraiResponse<ICollection<Magazine>>> GetMagazinesAsync(int page, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(page, nameof(page));
			var queryParams = $"?page={page}";
			var endpointParts = new[] { TenraiEndpointConsts.Magazines + queryParams };
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<Magazine>>>(endpointParts, cancellationToken);
		}

		#endregion Magazine methods

		#region Club methods

		/// <inheritdoc />
		[Obsolete(ErrorMessagesConst.UnsupportedClub)]
		public Task<BaseTenraiResponse<Club>> GetClubAsync(long id, CancellationToken cancellationToken = default)
			=> throw new NotSupportedException(ErrorMessagesConst.UnsupportedClub);

		/// <inheritdoc />
		[Obsolete(ErrorMessagesConst.UnsupportedClub)]
		public Task<PaginatedTenraiResponse<ICollection<ClubMember>>> GetClubMembersAsync(long id, CancellationToken cancellationToken = default)
			=> throw new NotSupportedException(ErrorMessagesConst.UnsupportedClub);

		/// <inheritdoc />
		[Obsolete(ErrorMessagesConst.UnsupportedClub)]
		public Task<PaginatedTenraiResponse<ICollection<ClubMember>>> GetClubMembersAsync(long id, int page, CancellationToken cancellationToken = default)
			=> throw new NotSupportedException(ErrorMessagesConst.UnsupportedClub);

		/// <inheritdoc />
		[Obsolete(ErrorMessagesConst.UnsupportedClub)]
		public Task<BaseTenraiResponse<ICollection<ClubStaff>>> GetClubStaffAsync(long id, CancellationToken cancellationToken = default)
			=> throw new NotSupportedException(ErrorMessagesConst.UnsupportedClub);

		/// <inheritdoc />
		[Obsolete(ErrorMessagesConst.UnsupportedClub)]
		public Task<BaseTenraiResponse<ClubRelations>> GetClubRelationsAsync(long id, CancellationToken cancellationToken = default)
			=> throw new NotSupportedException(ErrorMessagesConst.UnsupportedClub);

		#endregion Club methods

		#region User methods

		/// <inheritdoc />
		[Obsolete(ErrorMessagesConst.UnsupportedUserData)]
		public Task<BaseTenraiResponse<UserProfile>> GetUserProfileAsync(string username, CancellationToken cancellationToken = default)
			=> throw new NotSupportedException(ErrorMessagesConst.UnsupportedUserData);

		/// <inheritdoc />
		[Obsolete(ErrorMessagesConst.UnsupportedUserData)]
		public Task<BaseTenraiResponse<UserProfile>> GetUserByIdAsync(long id, CancellationToken cancellationToken = default)
			=> throw new NotSupportedException(ErrorMessagesConst.UnsupportedUserData);

		/// <inheritdoc />
		[Obsolete(ErrorMessagesConst.UnsupportedUserData)]
		public Task<BaseTenraiResponse<UserStatistics>> GetUserStatisticsAsync(string username, CancellationToken cancellationToken = default)
			=> throw new NotSupportedException(ErrorMessagesConst.UnsupportedUserData);

		/// <inheritdoc />
		[Obsolete(ErrorMessagesConst.UnsupportedUserData)]
		public Task<BaseTenraiResponse<UserFavorites>> GetUserFavoritesAsync(string username, CancellationToken cancellationToken = default)
			=> throw new NotSupportedException(ErrorMessagesConst.UnsupportedUserData);

		/// <inheritdoc />
		[Obsolete(ErrorMessagesConst.UnsupportedUserData)]
		public Task<BaseTenraiResponse<UserUpdates>> GetUserUpdatesAsync(string username, CancellationToken cancellationToken = default)
			=> throw new NotSupportedException(ErrorMessagesConst.UnsupportedUserData);

		/// <inheritdoc />
		[Obsolete(ErrorMessagesConst.UnsupportedUserData)]
		public Task<BaseTenraiResponse<UserAbout>> GetUserAboutAsync(string username, CancellationToken cancellationToken = default)
			=> throw new NotSupportedException(ErrorMessagesConst.UnsupportedUserData);

		/// <inheritdoc />
		[Obsolete(ErrorMessagesConst.UnsupportedUserData)]
		public Task<BaseTenraiResponse<ICollection<HistoryEntry>>> GetUserHistoryAsync(string username, CancellationToken cancellationToken = default)
			=> throw new NotSupportedException(ErrorMessagesConst.UnsupportedUserData);

		/// <inheritdoc />
		[Obsolete(ErrorMessagesConst.UnsupportedUserData)]
		public Task<BaseTenraiResponse<ICollection<HistoryEntry>>> GetUserHistoryAsync(string username, UserHistoryExtension historyExtension, CancellationToken cancellationToken = default)
			=> throw new NotSupportedException(ErrorMessagesConst.UnsupportedUserData);

		/// <inheritdoc />
		[Obsolete(ErrorMessagesConst.UnsupportedUserData)]
		public Task<PaginatedTenraiResponse<ICollection<Friend>>> GetUserFriendsAsync(string username, CancellationToken cancellationToken = default)
			=> throw new NotSupportedException(ErrorMessagesConst.UnsupportedUserData);

		/// <inheritdoc />
		[Obsolete(ErrorMessagesConst.UnsupportedUserData)]
		public Task<PaginatedTenraiResponse<ICollection<Friend>>> GetUserFriendsAsync(string username, int page, CancellationToken cancellationToken = default)
			=> throw new NotSupportedException(ErrorMessagesConst.UnsupportedUserData);

		/// <inheritdoc />
		[Obsolete(ErrorMessagesConst.UnsupportedUserData)]
		public Task<PaginatedTenraiResponse<ICollection<Review>>> GetUserReviewsAsync(string username, CancellationToken cancellationToken = default)
			=> throw new NotSupportedException(ErrorMessagesConst.UnsupportedUserData);

		/// <inheritdoc />
		[Obsolete(ErrorMessagesConst.UnsupportedUserData)]
		public Task<PaginatedTenraiResponse<ICollection<Review>>> GetUserReviewsAsync(string username, int page, CancellationToken cancellationToken = default)
			=> throw new NotSupportedException(ErrorMessagesConst.UnsupportedUserData);

		/// <inheritdoc />
		[Obsolete(ErrorMessagesConst.UnsupportedUserData)]
		public Task<PaginatedTenraiResponse<ICollection<UserRecommendation>>> GetUserRecommendationsAsync(string username, CancellationToken cancellationToken = default)
			=> throw new NotSupportedException(ErrorMessagesConst.UnsupportedUserData);

		/// <inheritdoc />
		[Obsolete(ErrorMessagesConst.UnsupportedUserData)]
		public Task<PaginatedTenraiResponse<ICollection<UserRecommendation>>> GetUserRecommendationsAsync(string username, int page, CancellationToken cancellationToken = default)
			=> throw new NotSupportedException(ErrorMessagesConst.UnsupportedUserData);

		/// <inheritdoc />
		[Obsolete(ErrorMessagesConst.UnsupportedUserData)]
		public Task<PaginatedTenraiResponse<ICollection<MalUrl>>> GetUserClubsAsync(string username, CancellationToken cancellationToken = default)
			=> throw new NotSupportedException(ErrorMessagesConst.UnsupportedUserData);

		/// <inheritdoc />
		[Obsolete(ErrorMessagesConst.UnsupportedUserData)]
		public Task<PaginatedTenraiResponse<ICollection<MalUrl>>> GetUserClubsAsync(string username, int page, CancellationToken cancellationToken = default)
			=> throw new NotSupportedException(ErrorMessagesConst.UnsupportedUserData);

		/// <inheritdoc />
		[Obsolete(ErrorMessagesConst.UnsupportedUserData)]
		public Task<BaseTenraiResponse<ICollection<ExternalLink>>> GetUserExternalLinksAsync(string username, CancellationToken cancellationToken = default)
			=> throw new NotSupportedException(ErrorMessagesConst.UnsupportedUserData);

		/// <inheritdoc />
		[Obsolete(ErrorMessagesConst.UnsupportedUserData)]
		public Task<BaseTenraiResponse<UserFull>> GetUserFullDataAsync(string username, CancellationToken cancellationToken = default)
			=> throw new NotSupportedException(ErrorMessagesConst.UnsupportedUserData);

		#endregion User methods

		#region GetRandom methods

		/// <inheritdoc/>
		public async Task<BaseTenraiResponse<Anime>> GetRandomAnimeAsync(CancellationToken cancellationToken = default)
		{
			var endpointParts = new[] { TenraiEndpointConsts.Random, TenraiEndpointConsts.Anime };
			return await ExecuteGetRequestAsync<BaseTenraiResponse<Anime>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc/>
		public async Task<BaseTenraiResponse<Manga>> GetRandomMangaAsync(CancellationToken cancellationToken = default)
		{
			var endpointParts = new[] { TenraiEndpointConsts.Random, TenraiEndpointConsts.Manga };
			return await ExecuteGetRequestAsync<BaseTenraiResponse<Manga>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc/>
		public async Task<BaseTenraiResponse<Character>> GetRandomCharacterAsync(CancellationToken cancellationToken = default)
		{
			var endpointParts = new[] { TenraiEndpointConsts.Random, TenraiEndpointConsts.Characters };
			return await ExecuteGetRequestAsync<BaseTenraiResponse<Character>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc/>
		public async Task<BaseTenraiResponse<Person>> GetRandomPersonAsync(CancellationToken cancellationToken = default)
		{
			var endpointParts = new[] { TenraiEndpointConsts.Random, TenraiEndpointConsts.People };
			return await ExecuteGetRequestAsync<BaseTenraiResponse<Person>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc/>
		[Obsolete(ErrorMessagesConst.UnsupportedUserData)]
		public Task<BaseTenraiResponse<UserProfile>> GetRandomUserAsync(CancellationToken cancellationToken = default)
			=> throw new NotSupportedException(ErrorMessagesConst.UnsupportedUserData);

		#endregion GetRandom methods
		
		#region Watch methods

		/// <inheritdoc />
		[Obsolete(ErrorMessagesConst.UnsupportedWatch)]
		public Task<PaginatedTenraiResponse<ICollection<WatchEpisode>>> GetWatchRecentEpisodesAsync(CancellationToken cancellationToken = default)
			=> throw new NotSupportedException(ErrorMessagesConst.UnsupportedWatch);

		/// <inheritdoc />
		[Obsolete(ErrorMessagesConst.UnsupportedWatch)]
		public Task<PaginatedTenraiResponse<ICollection<WatchEpisode>>> GetWatchPopularEpisodesAsync(CancellationToken cancellationToken = default)
			=> throw new NotSupportedException(ErrorMessagesConst.UnsupportedWatch);

		/// <inheritdoc />
		[Obsolete(ErrorMessagesConst.UnsupportedWatch)]
		public Task<PaginatedTenraiResponse<ICollection<WatchPromoVideo>>> GetWatchRecentPromosAsync(CancellationToken cancellationToken = default)
			=> throw new NotSupportedException(ErrorMessagesConst.UnsupportedWatch);

		/// <inheritdoc />
		[Obsolete(ErrorMessagesConst.UnsupportedWatch)]
		public Task<PaginatedTenraiResponse<ICollection<WatchPromoVideo>>> GetWatchRecentPromosAsync(int page, CancellationToken cancellationToken = default)
			=> throw new NotSupportedException(ErrorMessagesConst.UnsupportedWatch);

		/// <inheritdoc />
		[Obsolete(ErrorMessagesConst.UnsupportedWatch)]
		public Task<PaginatedTenraiResponse<ICollection<WatchPromoVideo>>> GetWatchPopularPromosAsync(CancellationToken cancellationToken = default)
			=> throw new NotSupportedException(ErrorMessagesConst.UnsupportedWatch);

		#endregion

		#region Reviews methods

		/// <inheritdoc />
		public async Task<PaginatedTenraiResponse<ICollection<Review>>> GetRecentAnimeReviewsAsync(CancellationToken cancellationToken = default)
		{
			var endpointParts = new[] { TenraiEndpointConsts.Reviews, TenraiEndpointConsts.Anime };
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<Review>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<PaginatedTenraiResponse<ICollection<Review>>> GetRecentAnimeReviewsAsync(ReviewsSearchConfig searchConfig, CancellationToken cancellationToken = default)
		{
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			var endpointParts = new[] { TenraiEndpointConsts.Reviews, TenraiEndpointConsts.Anime + searchConfig.ConfigToString() };
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<Review>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<PaginatedTenraiResponse<ICollection<Review>>> GetRecentMangaReviewsAsync(CancellationToken cancellationToken = default)
		{
			var endpointParts = new[] { TenraiEndpointConsts.Reviews, TenraiEndpointConsts.Manga };
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<Review>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<PaginatedTenraiResponse<ICollection<Review>>> GetRecentMangaReviewsAsync(ReviewsSearchConfig searchConfig, CancellationToken cancellationToken = default)
		{
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			var endpointParts = new[] { TenraiEndpointConsts.Reviews, TenraiEndpointConsts.Manga + searchConfig.ConfigToString() };
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<Review>>>(endpointParts, cancellationToken);
		}
		
		#endregion
		
		#region Recommendations methods
		
		/// <inheritdoc />
		public async Task<PaginatedTenraiResponse<ICollection<UserRecommendation>>> GetRecentAnimeRecommendationsAsync(CancellationToken cancellationToken = default)
		{
			var endpointParts = new[] { TenraiEndpointConsts.Recommendations, TenraiEndpointConsts.Anime };
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<UserRecommendation>>>(endpointParts, cancellationToken);
		}
		
		/// <inheritdoc />
		public async Task<PaginatedTenraiResponse<ICollection<UserRecommendation>>> GetRecentAnimeRecommendationsAsync(int page, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(page, nameof(page));
			var queryParams = $"?page={page}";
			var endpointParts = new[] { TenraiEndpointConsts.Recommendations, TenraiEndpointConsts.Anime + queryParams };
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<UserRecommendation>>>(endpointParts, cancellationToken);
		}
		
		/// <inheritdoc />
		public async Task<PaginatedTenraiResponse<ICollection<UserRecommendation>>> GetRecentMangaRecommendationsAsync(CancellationToken cancellationToken = default)
		{
			var endpointParts = new[] { TenraiEndpointConsts.Recommendations, TenraiEndpointConsts.Manga };
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<UserRecommendation>>>(endpointParts, cancellationToken);
		}
		
		/// <inheritdoc />
		public async Task<PaginatedTenraiResponse<ICollection<UserRecommendation>>> GetRecentMangaRecommendationsAsync(int page, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(page, nameof(page));
			var queryParams = $"?page={page}";
			var endpointParts = new[] { TenraiEndpointConsts.Recommendations, TenraiEndpointConsts.Manga + queryParams };
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<UserRecommendation>>>(endpointParts, cancellationToken);
		}
		
		#endregion

		#region Search methods
		
		
		
		/// <inheritdoc />
		public Task<PaginatedTenraiResponse<ICollection<Anime>>> SearchAnimeAsync(string query, CancellationToken cancellationToken = default)
			=> SearchAnimeAsync(new AnimeSearchConfig {Query = query}, cancellationToken);
		
		/// <inheritdoc />
		public async Task<PaginatedTenraiResponse<ICollection<Anime>>> SearchAnimeAsync(AnimeSearchConfig searchConfig, CancellationToken cancellationToken = default)
		{
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			var endpointParts = new[] { TenraiEndpointConsts.Anime + searchConfig.ConfigToString()};
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<Anime>>>(endpointParts, cancellationToken);
		}
		
		/// <inheritdoc />
		public Task<PaginatedTenraiResponse<ICollection<Manga>>> SearchMangaAsync(string query, CancellationToken cancellationToken = default)
			=> SearchMangaAsync(new MangaSearchConfig {Query = query}, cancellationToken);
		
		/// <inheritdoc />
		public async Task<PaginatedTenraiResponse<ICollection<Manga>>> SearchMangaAsync(MangaSearchConfig searchConfig, CancellationToken cancellationToken = default)
		{
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			var endpointParts = new[] { TenraiEndpointConsts.Manga + searchConfig.ConfigToString()};
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<Manga>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public Task<PaginatedTenraiResponse<ICollection<Person>>> SearchPersonAsync(string query, CancellationToken cancellationToken = default)
			=> SearchPersonAsync(new PersonSearchConfig {Query = query}, cancellationToken);
		
		/// <inheritdoc />
		public async Task<PaginatedTenraiResponse<ICollection<Person>>> SearchPersonAsync(PersonSearchConfig searchConfig, CancellationToken cancellationToken = default)
		{	
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			var endpointParts = new[] { TenraiEndpointConsts.People + searchConfig.ConfigToString()};
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<Person>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public Task<PaginatedTenraiResponse<ICollection<Character>>> SearchCharacterAsync(string query, CancellationToken cancellationToken = default)
			=> SearchCharacterAsync(new CharacterSearchConfig {Query = query}, cancellationToken);
		
		/// <inheritdoc />
		public async Task<PaginatedTenraiResponse<ICollection<Character>>> SearchCharacterAsync(CharacterSearchConfig searchConfig, CancellationToken cancellationToken = default)
		{
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			var endpointParts = new[] { TenraiEndpointConsts.Characters + searchConfig.ConfigToString()};
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<Character>>>(endpointParts, cancellationToken);
		}
	
		/// <inheritdoc />
		[Obsolete(ErrorMessagesConst.UnsupportedUserData)]
		public Task<PaginatedTenraiResponse<ICollection<UserMetadata>>> SearchUserAsync(string query, CancellationToken cancellationToken = default)
			=> throw new NotSupportedException(ErrorMessagesConst.UnsupportedUserData);

		/// <inheritdoc />
		[Obsolete(ErrorMessagesConst.UnsupportedUserData)]
		public Task<PaginatedTenraiResponse<ICollection<UserMetadata>>> SearchUserAsync(UserSearchConfig searchConfig, CancellationToken cancellationToken = default)
			=> throw new NotSupportedException(ErrorMessagesConst.UnsupportedUserData);

		/// <inheritdoc />
		[Obsolete(ErrorMessagesConst.UnsupportedClub)]
		public Task<PaginatedTenraiResponse<ICollection<Club>>> SearchClubAsync(string query, CancellationToken cancellationToken = default)
			=> throw new NotSupportedException(ErrorMessagesConst.UnsupportedClub);

		/// <inheritdoc />
		[Obsolete(ErrorMessagesConst.UnsupportedClub)]
		public Task<PaginatedTenraiResponse<ICollection<Club>>> SearchClubAsync(ClubSearchConfig searchConfig, CancellationToken cancellationToken = default)
			=> throw new NotSupportedException(ErrorMessagesConst.UnsupportedClub);

		#endregion Search methods

		#region Interest stack methods

		/// <inheritdoc />
		public async Task<PaginatedTenraiResponse<ICollection<InterestStack>>> GetInterestStacksAsync(CancellationToken cancellationToken = default)
		{
			var endpointParts = new[] { TenraiEndpointConsts.Stacks };
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<InterestStack>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<PaginatedTenraiResponse<ICollection<InterestStack>>> GetInterestStacksAsync(int page, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(page, nameof(page));
			var endpointParts = new[] { TenraiEndpointConsts.Stacks + $"?page={page}" };
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<InterestStack>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<PaginatedTenraiResponse<ICollection<InterestStack>>> SearchInterestStacksAsync(InterestStackSearchConfig searchConfig, CancellationToken cancellationToken = default)
		{
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			var endpointParts = new[] { TenraiEndpointConsts.Stacks + searchConfig.ConfigToString() };
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<InterestStack>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<BaseTenraiResponse<InterestStackDetails>> GetInterestStackAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { TenraiEndpointConsts.Stacks, id.ToString() };
			return await ExecuteGetRequestAsync<BaseTenraiResponse<InterestStackDetails>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<PaginatedTenraiResponse<ICollection<InterestStack>>> GetAnimeInterestStacksAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { TenraiEndpointConsts.Anime, id.ToString(), TenraiEndpointConsts.Stacks };
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<InterestStack>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<PaginatedTenraiResponse<ICollection<InterestStack>>> GetAnimeInterestStacksAsync(long id, int page, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			Guard.IsGreaterThanZero(page, nameof(page));
			var endpointParts = new[] { TenraiEndpointConsts.Anime, id.ToString(), TenraiEndpointConsts.Stacks + $"?page={page}" };
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<InterestStack>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<PaginatedTenraiResponse<ICollection<InterestStack>>> GetMangaInterestStacksAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { TenraiEndpointConsts.Manga, id.ToString(), TenraiEndpointConsts.Stacks };
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<InterestStack>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<PaginatedTenraiResponse<ICollection<InterestStack>>> GetMangaInterestStacksAsync(long id, int page, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			Guard.IsGreaterThanZero(page, nameof(page));
			var endpointParts = new[] { TenraiEndpointConsts.Manga, id.ToString(), TenraiEndpointConsts.Stacks + $"?page={page}" };
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<InterestStack>>>(endpointParts, cancellationToken);
		}

		#endregion Interest stack methods

		#region Status methods

		/// <inheritdoc />
		public async Task<TenraiStatus> GetStatusAsync(CancellationToken cancellationToken = default)
		{
			// Absolute URL as a single route section, so string.Join is a no-op and BaseAddress is bypassed
			var endpointParts = new[] { TenraiEndpointConsts.StatusEndpoint };
			return await ExecuteGetRequestAsync<TenraiStatus>(endpointParts, cancellationToken, bypassLimiter: true);
		}

		#endregion Status methods

		#endregion Public Methods
	}
}
