using Tenrai.Config;
using Tenrai.Consts;
using Tenrai.Exceptions;
using Tenrai.Extensions;
using Tenrai.Helpers;
using Tenrai.Interfaces;
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

		private async Task<PaginatedTenraiResponse<ICollection<News>>> GetEntityNewsAsync(string entity, long id, NewsFilterConfig filterConfig, CancellationToken cancellationToken)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			Guard.IsNotNull(filterConfig, nameof(filterConfig));
			var endpointParts = new[] { entity, id.ToString(), TenraiEndpointConsts.News + QueryFor(filterConfig) };
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<News>>>(endpointParts, cancellationToken);
		}

		private async Task<PaginatedTenraiResponse<ICollection<Article>>> GetEntityArticlesAsync(string entity, long id, ArticleFilterConfig filterConfig, CancellationToken cancellationToken)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			Guard.IsNotNull(filterConfig, nameof(filterConfig));
			var endpointParts = new[] { entity, id.ToString(), TenraiEndpointConsts.Articles + QueryFor(filterConfig) };
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<Article>>>(endpointParts, cancellationToken);
		}

		private async Task<PaginatedTenraiResponse<ICollection<T>>> GetCatalogueListAsync<T>(string entity, string list, ISearchConfig searchConfig, CancellationToken cancellationToken)
		{
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			var endpointParts = new[] { entity, list + QueryFor(searchConfig) };
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<T>>>(endpointParts, cancellationToken);
		}

		private async Task<PaginatedTenraiResponse<ICollection<StatisticsHistoryEntry<T>>>> GetStatisticsHistoryAsync<T>(string entity, long id, StatisticsHistoryConfig config, CancellationToken cancellationToken) where T : class
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			Guard.IsNotNull(config, nameof(config));
			var endpointParts = new[] { entity, id.ToString(), TenraiEndpointConsts.Statistics, TenraiEndpointConsts.History + QueryFor(config) };
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<StatisticsHistoryEntry<T>>>>(endpointParts, cancellationToken);
		}

		private async Task<PaginatedTenraiResponse<ICollection<TopRecommendation>>> GetTopRecommendationsAsync(string type, TopRecommendationsConfig searchConfig, CancellationToken cancellationToken)
		{
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			var endpointParts = new[] { TenraiEndpointConsts.TopList, TenraiEndpointConsts.Recommendations + WithSfwDefaults(searchConfig.ConfigToString(type), searchConfig) };
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<TopRecommendation>>>(endpointParts, cancellationToken);
		}

		private async Task<PaginatedTenraiResponse<ICollection<Anime>>> GetSeasonListAsync(string list, SeasonSearchConfig searchConfig, CancellationToken cancellationToken)
		{
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			var endpointParts = new[] { TenraiEndpointConsts.Seasons, list + QueryFor(searchConfig) };
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<Anime>>>(endpointParts, cancellationToken);
		}

		private string QueryFor(ISearchConfig config)
		{
			var query = config.ConfigToString();
			return config is ISfwConfig sfwConfig ? WithSfwDefaults(query, sfwConfig) : query;
		}

		private string IdEntriesQueryFor(ISearchConfig config)
		{
			var query = QueryFor(config);
			return query + (string.IsNullOrEmpty(query) ? "?" : "&") + "include=title,type";
		}

		private string WithSfwDefaults(string query, ISfwConfig config)
		{
			var defaults = new List<string>();
			if (config?.Sfw == null && _tenraiConfiguration.Sfw)
			{
				defaults.Add("sfw=true");
			}

			if (config?.SfwStrict == null && _tenraiConfiguration.SfwStrict)
			{
				defaults.Add("sfw-strict=true");
			}

			if (defaults.Count == 0) return query;

			return query + (string.IsNullOrEmpty(query) ? "?" : "&") + string.Join("&", defaults);
		}

		private static void GuardFullDataRequest(long id, FullDataConfig config)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			Guard.IsNotNull(config, nameof(config));

			var extraIds = config.Ids ?? new List<long>();
			foreach (var extraId in extraIds)
			{
				Guard.IsGreaterThanZero(extraId, nameof(config.Ids));
			}

			var uniqueIds = extraIds.Where(extraId => extraId != id).Distinct().Count() + 1;
			Guard.IsLesserOrEqualThan(uniqueIds, ParameterConsts.MaximumFullDataIds, nameof(config.Ids));
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
		public Task<PaginatedTenraiResponse<ICollection<News>>> GetAnimeNewsAsync(long id, NewsFilterConfig filterConfig, CancellationToken cancellationToken = default)
			=> GetEntityNewsAsync(TenraiEndpointConsts.Anime, id, filterConfig, cancellationToken);

		/// <inheritdoc />
		public Task<PaginatedTenraiResponse<ICollection<Article>>> GetAnimeArticlesAsync(long id, CancellationToken cancellationToken = default)
			=> GetEntityArticlesAsync(TenraiEndpointConsts.Anime, id, new ArticleFilterConfig(), cancellationToken);

		/// <inheritdoc />
		public Task<PaginatedTenraiResponse<ICollection<Article>>> GetAnimeArticlesAsync(long id, ArticleFilterConfig filterConfig, CancellationToken cancellationToken = default)
			=> GetEntityArticlesAsync(TenraiEndpointConsts.Anime, id, filterConfig, cancellationToken);

		/// <inheritdoc />
		public Task<PaginatedTenraiResponse<ICollection<ForumTopic>>> GetAnimeForumTopicsAsync(long id, CancellationToken cancellationToken = default)
			=> GetAnimeForumTopicsAsync(id, new AnimeForumSearchConfig(), cancellationToken);

		/// <inheritdoc />
		public async Task<PaginatedTenraiResponse<ICollection<ForumTopic>>> GetAnimeForumTopicsAsync(long id, AnimeForumSearchConfig searchConfig, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			var endpointParts = new[] { TenraiEndpointConsts.Anime, id.ToString(), TenraiEndpointConsts.Forum + QueryFor(searchConfig) };
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<ForumTopic>>>(endpointParts, cancellationToken);
		}

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
		public Task<BaseTenraiResponse<ICollection<Recommendation>>> GetAnimeRecommendationsAsync(long id, CancellationToken cancellationToken = default)
			=> GetAnimeRecommendationsAsync(id, new RecommendationsConfig(), cancellationToken);

		/// <inheritdoc />
		public async Task<BaseTenraiResponse<ICollection<Recommendation>>> GetAnimeRecommendationsAsync(long id, RecommendationsConfig searchConfig, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			var endpointParts = new[] { TenraiEndpointConsts.Anime, id.ToString(), TenraiEndpointConsts.Recommendations + QueryFor(searchConfig) };
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
			var endpointParts = new[] { TenraiEndpointConsts.Anime, id.ToString(), TenraiEndpointConsts.Reviews + QueryFor(searchConfig) };
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<Review>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public Task<PaginatedTenraiResponse<ICollection<RelatedEntry>>> GetAnimeRelationsAsync(long id, CancellationToken cancellationToken = default)
			=> GetAnimeRelationsAsync(id, new SfwConfig(), cancellationToken);

		/// <inheritdoc />
		public async Task<PaginatedTenraiResponse<ICollection<RelatedEntry>>> GetAnimeRelationsAsync(long id, SfwConfig searchConfig, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			var endpointParts = new[] { TenraiEndpointConsts.Anime, id.ToString(), TenraiEndpointConsts.Relations + QueryFor(searchConfig) };
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
		public async Task<FullTenraiResponse<AnimeFull>> GetAnimeFullDataAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { TenraiEndpointConsts.Anime, id.ToString(), TenraiEndpointConsts.Full + WithSfwDefaults(string.Empty, null) };
			return await ExecuteGetRequestAsync<FullTenraiResponse<AnimeFull>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<FullTenraiResponse<AnimeFull>> GetAnimeFullDataAsync(long id, FullDataConfig config, CancellationToken cancellationToken = default)
		{
			GuardFullDataRequest(id, config);
			var endpointParts = new[] { TenraiEndpointConsts.Anime, id.ToString(), TenraiEndpointConsts.Full + QueryFor(config) };
			return await ExecuteGetRequestAsync<FullTenraiResponse<AnimeFull>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public Task<IdsTenraiResponse> GetAnimeIdsAsync(CancellationToken cancellationToken = default)
			=> GetAnimeIdsAsync(new AnimeIdsConfig(), cancellationToken);

		/// <inheritdoc />
		public async Task<IdsTenraiResponse> GetAnimeIdsAsync(AnimeIdsConfig searchConfig, CancellationToken cancellationToken = default)
		{
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			var endpointParts = new[] { TenraiEndpointConsts.Anime, TenraiEndpointConsts.Ids + QueryFor(searchConfig) };
			return await ExecuteGetRequestAsync<IdsTenraiResponse>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public Task<IdEntriesTenraiResponse> GetAnimeIdEntriesAsync(CancellationToken cancellationToken = default)
			=> GetAnimeIdEntriesAsync(new AnimeIdsConfig(), cancellationToken);

		/// <inheritdoc />
		public async Task<IdEntriesTenraiResponse> GetAnimeIdEntriesAsync(AnimeIdsConfig searchConfig, CancellationToken cancellationToken = default)
		{
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			var endpointParts = new[] { TenraiEndpointConsts.Anime, TenraiEndpointConsts.Ids + IdEntriesQueryFor(searchConfig) };
			return await ExecuteGetRequestAsync<IdEntriesTenraiResponse>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public Task<PaginatedTenraiResponse<ICollection<Anime>>> GetUnapprovedAnimeAsync(CancellationToken cancellationToken = default)
			=> GetUnapprovedAnimeAsync(new CatalogueSearchConfig(), cancellationToken);

		/// <inheritdoc />
		public Task<PaginatedTenraiResponse<ICollection<Anime>>> GetUnapprovedAnimeAsync(CatalogueSearchConfig searchConfig, CancellationToken cancellationToken = default)
			=> GetCatalogueListAsync<Anime>(TenraiEndpointConsts.Anime, TenraiEndpointConsts.Unapproved, searchConfig, cancellationToken);

		/// <inheritdoc />
		public Task<PaginatedTenraiResponse<ICollection<Anime>>> GetDeniedAnimeAsync(CancellationToken cancellationToken = default)
			=> GetDeniedAnimeAsync(new CatalogueSearchConfig(), cancellationToken);

		/// <inheritdoc />
		public Task<PaginatedTenraiResponse<ICollection<Anime>>> GetDeniedAnimeAsync(CatalogueSearchConfig searchConfig, CancellationToken cancellationToken = default)
			=> GetCatalogueListAsync<Anime>(TenraiEndpointConsts.Anime, TenraiEndpointConsts.Denied, searchConfig, cancellationToken);

		/// <inheritdoc />
		public Task<PaginatedTenraiResponse<ICollection<Anime>>> GetDeletedAnimeAsync(CancellationToken cancellationToken = default)
			=> GetDeletedAnimeAsync(new DeletedSearchConfig(), cancellationToken);

		/// <inheritdoc />
		public Task<PaginatedTenraiResponse<ICollection<Anime>>> GetDeletedAnimeAsync(DeletedSearchConfig searchConfig, CancellationToken cancellationToken = default)
			=> GetCatalogueListAsync<Anime>(TenraiEndpointConsts.Anime, TenraiEndpointConsts.Deleted, searchConfig, cancellationToken);

		/// <inheritdoc />
		public Task<PaginatedTenraiResponse<ICollection<StatisticsHistoryEntry<AnimeStatistics>>>> GetAnimeStatisticsHistoryAsync(long id, CancellationToken cancellationToken = default)
			=> GetAnimeStatisticsHistoryAsync(id, new StatisticsHistoryConfig(), cancellationToken);

		/// <inheritdoc />
		public Task<PaginatedTenraiResponse<ICollection<StatisticsHistoryEntry<AnimeStatistics>>>> GetAnimeStatisticsHistoryAsync(long id, StatisticsHistoryConfig config, CancellationToken cancellationToken = default)
			=> GetStatisticsHistoryAsync<AnimeStatistics>(TenraiEndpointConsts.Anime, id, config, cancellationToken);

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
		public Task<BaseTenraiResponse<ICollection<CharacterAnimeographyEntry>>> GetCharacterAnimeAsync(long id, CancellationToken cancellationToken = default)
			=> GetCharacterAnimeAsync(id, new SfwConfig(), cancellationToken);

		/// <inheritdoc />
		public async Task<BaseTenraiResponse<ICollection<CharacterAnimeographyEntry>>> GetCharacterAnimeAsync(long id, SfwConfig searchConfig, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			var endpointParts = new[] { TenraiEndpointConsts.Characters, id.ToString(), TenraiEndpointConsts.Anime + QueryFor(searchConfig) };
			return await ExecuteGetRequestAsync<BaseTenraiResponse<ICollection<CharacterAnimeographyEntry>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public Task<BaseTenraiResponse<ICollection<CharacterMangaographyEntry>>> GetCharacterMangaAsync(long id, CancellationToken cancellationToken = default)
			=> GetCharacterMangaAsync(id, new SfwConfig(), cancellationToken);

		/// <inheritdoc />
		public async Task<BaseTenraiResponse<ICollection<CharacterMangaographyEntry>>> GetCharacterMangaAsync(long id, SfwConfig searchConfig, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			var endpointParts = new[] { TenraiEndpointConsts.Characters, id.ToString(), TenraiEndpointConsts.Manga + QueryFor(searchConfig) };
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
		public Task<PaginatedTenraiResponse<ICollection<Article>>> GetCharacterArticlesAsync(long id, CancellationToken cancellationToken = default)
			=> GetEntityArticlesAsync(TenraiEndpointConsts.Characters, id, new ArticleFilterConfig(), cancellationToken);

		/// <inheritdoc />
		public Task<PaginatedTenraiResponse<ICollection<Article>>> GetCharacterArticlesAsync(long id, ArticleFilterConfig filterConfig, CancellationToken cancellationToken = default)
			=> GetEntityArticlesAsync(TenraiEndpointConsts.Characters, id, filterConfig, cancellationToken);
		
		/// <inheritdoc />
		public Task<BaseTenraiResponse<CharacterFull>> GetCharacterFullDataAsync(long id, CancellationToken cancellationToken = default)
			=> GetCharacterFullDataAsync(id, new SfwConfig(), cancellationToken);

		/// <inheritdoc />
		public async Task<BaseTenraiResponse<CharacterFull>> GetCharacterFullDataAsync(long id, SfwConfig searchConfig, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			var endpointParts = new[] { TenraiEndpointConsts.Characters, id.ToString(), TenraiEndpointConsts.Full + QueryFor(searchConfig) };
			return await ExecuteGetRequestAsync<BaseTenraiResponse<CharacterFull>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<IdsTenraiResponse> GetCharacterIdsAsync(CancellationToken cancellationToken = default)
		{
			var endpointParts = new[] { TenraiEndpointConsts.Characters, TenraiEndpointConsts.Ids };
			return await ExecuteGetRequestAsync<IdsTenraiResponse>(endpointParts, cancellationToken);
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
		public Task<PaginatedTenraiResponse<ICollection<News>>> GetMangaNewsAsync(long id, NewsFilterConfig filterConfig, CancellationToken cancellationToken = default)
			=> GetEntityNewsAsync(TenraiEndpointConsts.Manga, id, filterConfig, cancellationToken);

		/// <inheritdoc />
		public Task<PaginatedTenraiResponse<ICollection<Article>>> GetMangaArticlesAsync(long id, CancellationToken cancellationToken = default)
			=> GetEntityArticlesAsync(TenraiEndpointConsts.Manga, id, new ArticleFilterConfig(), cancellationToken);

		/// <inheritdoc />
		public Task<PaginatedTenraiResponse<ICollection<Article>>> GetMangaArticlesAsync(long id, ArticleFilterConfig filterConfig, CancellationToken cancellationToken = default)
			=> GetEntityArticlesAsync(TenraiEndpointConsts.Manga, id, filterConfig, cancellationToken);

		/// <inheritdoc />
		public Task<PaginatedTenraiResponse<ICollection<ForumTopic>>> GetMangaForumTopicsAsync(long id, CancellationToken cancellationToken = default)
			=> GetMangaForumTopicsAsync(id, new MangaForumSearchConfig(), cancellationToken);

		/// <inheritdoc />
		public async Task<PaginatedTenraiResponse<ICollection<ForumTopic>>> GetMangaForumTopicsAsync(long id, MangaForumSearchConfig searchConfig, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			var endpointParts = new[] { TenraiEndpointConsts.Manga, id.ToString(), TenraiEndpointConsts.Forum + QueryFor(searchConfig) };
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<ForumTopic>>>(endpointParts, cancellationToken);
		}

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
		public Task<BaseTenraiResponse<ICollection<Recommendation>>> GetMangaRecommendationsAsync(long id, CancellationToken cancellationToken = default)
			=> GetMangaRecommendationsAsync(id, new RecommendationsConfig(), cancellationToken);

		/// <inheritdoc />
		public async Task<BaseTenraiResponse<ICollection<Recommendation>>> GetMangaRecommendationsAsync(long id, RecommendationsConfig searchConfig, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			var endpointParts = new[] { TenraiEndpointConsts.Manga, id.ToString(), TenraiEndpointConsts.Recommendations + QueryFor(searchConfig) };
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
			var endpointParts = new List<string>() {TenraiEndpointConsts.Manga, id.ToString(), TenraiEndpointConsts.Reviews + QueryFor(searchConfig)};
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<Review>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public Task<PaginatedTenraiResponse<ICollection<RelatedEntry>>> GetMangaRelationsAsync(long id, CancellationToken cancellationToken = default)
			=> GetMangaRelationsAsync(id, new SfwConfig(), cancellationToken);

		/// <inheritdoc />
		public async Task<PaginatedTenraiResponse<ICollection<RelatedEntry>>> GetMangaRelationsAsync(long id, SfwConfig searchConfig, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			var endpointParts = new[] { TenraiEndpointConsts.Manga, id.ToString(), TenraiEndpointConsts.Relations + QueryFor(searchConfig) };
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
		public async Task<FullTenraiResponse<MangaFull>> GetMangaFullDataAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { TenraiEndpointConsts.Manga, id.ToString(), TenraiEndpointConsts.Full + WithSfwDefaults(string.Empty, null) };
			return await ExecuteGetRequestAsync<FullTenraiResponse<MangaFull>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<FullTenraiResponse<MangaFull>> GetMangaFullDataAsync(long id, FullDataConfig config, CancellationToken cancellationToken = default)
		{
			GuardFullDataRequest(id, config);
			var endpointParts = new[] { TenraiEndpointConsts.Manga, id.ToString(), TenraiEndpointConsts.Full + QueryFor(config) };
			return await ExecuteGetRequestAsync<FullTenraiResponse<MangaFull>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public Task<IdsTenraiResponse> GetMangaIdsAsync(CancellationToken cancellationToken = default)
			=> GetMangaIdsAsync(new MangaIdsConfig(), cancellationToken);

		/// <inheritdoc />
		public async Task<IdsTenraiResponse> GetMangaIdsAsync(MangaIdsConfig searchConfig, CancellationToken cancellationToken = default)
		{
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			var endpointParts = new[] { TenraiEndpointConsts.Manga, TenraiEndpointConsts.Ids + QueryFor(searchConfig) };
			return await ExecuteGetRequestAsync<IdsTenraiResponse>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public Task<IdEntriesTenraiResponse> GetMangaIdEntriesAsync(CancellationToken cancellationToken = default)
			=> GetMangaIdEntriesAsync(new MangaIdsConfig(), cancellationToken);

		/// <inheritdoc />
		public async Task<IdEntriesTenraiResponse> GetMangaIdEntriesAsync(MangaIdsConfig searchConfig, CancellationToken cancellationToken = default)
		{
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			var endpointParts = new[] { TenraiEndpointConsts.Manga, TenraiEndpointConsts.Ids + IdEntriesQueryFor(searchConfig) };
			return await ExecuteGetRequestAsync<IdEntriesTenraiResponse>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public Task<PaginatedTenraiResponse<ICollection<Manga>>> GetUnapprovedMangaAsync(CancellationToken cancellationToken = default)
			=> GetUnapprovedMangaAsync(new CatalogueSearchConfig(), cancellationToken);

		/// <inheritdoc />
		public Task<PaginatedTenraiResponse<ICollection<Manga>>> GetUnapprovedMangaAsync(CatalogueSearchConfig searchConfig, CancellationToken cancellationToken = default)
			=> GetCatalogueListAsync<Manga>(TenraiEndpointConsts.Manga, TenraiEndpointConsts.Unapproved, searchConfig, cancellationToken);

		/// <inheritdoc />
		public Task<PaginatedTenraiResponse<ICollection<Manga>>> GetDeniedMangaAsync(CancellationToken cancellationToken = default)
			=> GetDeniedMangaAsync(new CatalogueSearchConfig(), cancellationToken);

		/// <inheritdoc />
		public Task<PaginatedTenraiResponse<ICollection<Manga>>> GetDeniedMangaAsync(CatalogueSearchConfig searchConfig, CancellationToken cancellationToken = default)
			=> GetCatalogueListAsync<Manga>(TenraiEndpointConsts.Manga, TenraiEndpointConsts.Denied, searchConfig, cancellationToken);

		/// <inheritdoc />
		public Task<PaginatedTenraiResponse<ICollection<Manga>>> GetDeletedMangaAsync(CancellationToken cancellationToken = default)
			=> GetDeletedMangaAsync(new DeletedSearchConfig(), cancellationToken);

		/// <inheritdoc />
		public Task<PaginatedTenraiResponse<ICollection<Manga>>> GetDeletedMangaAsync(DeletedSearchConfig searchConfig, CancellationToken cancellationToken = default)
			=> GetCatalogueListAsync<Manga>(TenraiEndpointConsts.Manga, TenraiEndpointConsts.Deleted, searchConfig, cancellationToken);

		/// <inheritdoc />
		public Task<PaginatedTenraiResponse<ICollection<Manga>>> GetAdaptedMangaAsync(CancellationToken cancellationToken = default)
			=> GetAdaptedMangaAsync(new AdaptedMangaSearchConfig(), cancellationToken);

		/// <inheritdoc />
		public Task<PaginatedTenraiResponse<ICollection<Manga>>> GetAdaptedMangaAsync(AdaptedMangaSearchConfig searchConfig, CancellationToken cancellationToken = default)
			=> GetCatalogueListAsync<Manga>(TenraiEndpointConsts.Manga, TenraiEndpointConsts.Adapted, searchConfig, cancellationToken);

		/// <inheritdoc />
		public Task<PaginatedTenraiResponse<ICollection<StatisticsHistoryEntry<MangaStatistics>>>> GetMangaStatisticsHistoryAsync(long id, CancellationToken cancellationToken = default)
			=> GetMangaStatisticsHistoryAsync(id, new StatisticsHistoryConfig(), cancellationToken);

		/// <inheritdoc />
		public Task<PaginatedTenraiResponse<ICollection<StatisticsHistoryEntry<MangaStatistics>>>> GetMangaStatisticsHistoryAsync(long id, StatisticsHistoryConfig config, CancellationToken cancellationToken = default)
			=> GetStatisticsHistoryAsync<MangaStatistics>(TenraiEndpointConsts.Manga, id, config, cancellationToken);

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
		public Task<BaseTenraiResponse<ICollection<PersonAnimeographyEntry>>> GetPersonAnimeAsync(long id, CancellationToken cancellationToken = default)
			=> GetPersonAnimeAsync(id, new SfwConfig(), cancellationToken);

		/// <inheritdoc />
		public async Task<BaseTenraiResponse<ICollection<PersonAnimeographyEntry>>> GetPersonAnimeAsync(long id, SfwConfig searchConfig, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			var endpointParts = new[] { TenraiEndpointConsts.People, id.ToString(), TenraiEndpointConsts.Anime + QueryFor(searchConfig) };
			return await ExecuteGetRequestAsync<BaseTenraiResponse<ICollection<PersonAnimeographyEntry>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public Task<BaseTenraiResponse<ICollection<PersonMangaographyEntry>>> GetPersonMangaAsync(long id, CancellationToken cancellationToken = default)
			=> GetPersonMangaAsync(id, new SfwConfig(), cancellationToken);

		/// <inheritdoc />
		public async Task<BaseTenraiResponse<ICollection<PersonMangaographyEntry>>> GetPersonMangaAsync(long id, SfwConfig searchConfig, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			var endpointParts = new[] { TenraiEndpointConsts.People, id.ToString(), TenraiEndpointConsts.Manga + QueryFor(searchConfig) };
			return await ExecuteGetRequestAsync<BaseTenraiResponse<ICollection<PersonMangaographyEntry>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public Task<BaseTenraiResponse<ICollection<VoiceActingRole>>> GetPersonVoiceActingRolesAsync(long id, CancellationToken cancellationToken = default)
			=> GetPersonVoiceActingRolesAsync(id, new SfwConfig(), cancellationToken);

		/// <inheritdoc />
		public async Task<BaseTenraiResponse<ICollection<VoiceActingRole>>> GetPersonVoiceActingRolesAsync(long id, SfwConfig searchConfig, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			var endpointParts = new[] { TenraiEndpointConsts.People, id.ToString(), TenraiEndpointConsts.Voices + QueryFor(searchConfig) };
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
		public Task<PaginatedTenraiResponse<ICollection<Article>>> GetPersonArticlesAsync(long id, CancellationToken cancellationToken = default)
			=> GetEntityArticlesAsync(TenraiEndpointConsts.People, id, new ArticleFilterConfig(), cancellationToken);

		/// <inheritdoc />
		public Task<PaginatedTenraiResponse<ICollection<Article>>> GetPersonArticlesAsync(long id, ArticleFilterConfig filterConfig, CancellationToken cancellationToken = default)
			=> GetEntityArticlesAsync(TenraiEndpointConsts.People, id, filterConfig, cancellationToken);
		
		/// <inheritdoc />
		public Task<BaseTenraiResponse<PersonFull>> GetPersonFullDataAsync(long id, CancellationToken cancellationToken = default)
			=> GetPersonFullDataAsync(id, new SfwConfig(), cancellationToken);

		/// <inheritdoc />
		public async Task<BaseTenraiResponse<PersonFull>> GetPersonFullDataAsync(long id, SfwConfig searchConfig, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			var endpointParts = new[] { TenraiEndpointConsts.People, id.ToString(), TenraiEndpointConsts.Full + QueryFor(searchConfig) };
			return await ExecuteGetRequestAsync<BaseTenraiResponse<PersonFull>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<IdsTenraiResponse> GetPersonIdsAsync(CancellationToken cancellationToken = default)
		{
			var endpointParts = new[] { TenraiEndpointConsts.People, TenraiEndpointConsts.Ids };
			return await ExecuteGetRequestAsync<IdsTenraiResponse>(endpointParts, cancellationToken);
		}

		#endregion Person methods

		#region Season methods

		/// <inheritdoc />
		public Task<PaginatedTenraiResponse<ICollection<Anime>>> GetSeasonAsync(int year, Season season, CancellationToken cancellationToken = default)
			=> GetSeasonAsync(year, season, new SeasonSearchConfig(), cancellationToken);

		/// <inheritdoc />
		public Task<PaginatedTenraiResponse<ICollection<Anime>>> GetSeasonAsync(int year, Season season, int page, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(page, nameof(page));
			return GetSeasonAsync(year, season, new SeasonSearchConfig { Page = page }, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<PaginatedTenraiResponse<ICollection<Anime>>> GetSeasonAsync(int year, Season season, SeasonSearchConfig searchConfig, CancellationToken cancellationToken = default)
		{
			Guard.IsValid(x => x is >= 1000 and < 10000, year, nameof(year));
			Guard.IsValidEnum(season, nameof(season));
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			var endpointParts = new[] { TenraiEndpointConsts.Seasons, year.ToString(), season.GetDescription() + QueryFor(searchConfig) };
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<Anime>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public Task<PaginatedTenraiResponse<ICollection<Anime>>> GetCurrentSeasonAsync(SeasonSearchConfig searchConfig, CancellationToken cancellationToken = default)
			=> GetSeasonListAsync(TenraiEndpointConsts.Now, searchConfig, cancellationToken);

		/// <inheritdoc />
		public Task<PaginatedTenraiResponse<ICollection<Anime>>> GetUpcomingSeasonAsync(SeasonSearchConfig searchConfig, CancellationToken cancellationToken = default)
			=> GetSeasonListAsync(TenraiEndpointConsts.Upcoming, searchConfig, cancellationToken);

		/// <inheritdoc />
		public Task<PaginatedTenraiResponse<ICollection<Anime>>> GetPreviousSeasonAsync(SeasonSearchConfig searchConfig, CancellationToken cancellationToken = default)
			=> GetSeasonListAsync(TenraiEndpointConsts.Previous, searchConfig, cancellationToken);

		/// <inheritdoc />
		public async Task<PaginatedTenraiResponse<ICollection<SeasonArchive>>> GetSeasonArchiveAsync(CancellationToken cancellationToken = default)
		{
			var endpointParts = new[] { TenraiEndpointConsts.Seasons };
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<SeasonArchive>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public Task<PaginatedTenraiResponse<ICollection<Anime>>> GetCurrentSeasonAsync(CancellationToken cancellationToken = default)
			=> GetCurrentSeasonAsync(new SeasonSearchConfig(), cancellationToken);

		/// <inheritdoc />
		public Task<PaginatedTenraiResponse<ICollection<Anime>>> GetCurrentSeasonAsync(int page, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(page, nameof(page));
			return GetCurrentSeasonAsync(new SeasonSearchConfig { Page = page }, cancellationToken);
		}

		/// <inheritdoc />
		public Task<PaginatedTenraiResponse<ICollection<Anime>>> GetUpcomingSeasonAsync(CancellationToken cancellationToken = default)
			=> GetUpcomingSeasonAsync(new SeasonSearchConfig(), cancellationToken);

		/// <inheritdoc />
		public Task<PaginatedTenraiResponse<ICollection<Anime>>> GetUpcomingSeasonAsync(int page, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(page, nameof(page));
			return GetUpcomingSeasonAsync(new SeasonSearchConfig { Page = page }, cancellationToken);
		}

		/// <inheritdoc />
		public Task<PaginatedTenraiResponse<ICollection<Anime>>> GetPreviousSeasonAsync(CancellationToken cancellationToken = default)
			=> GetPreviousSeasonAsync(new SeasonSearchConfig(), cancellationToken);

		/// <inheritdoc />
		public Task<PaginatedTenraiResponse<ICollection<Anime>>> GetPreviousSeasonAsync(int page, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(page, nameof(page));
			return GetPreviousSeasonAsync(new SeasonSearchConfig { Page = page }, cancellationToken);
		}

		#endregion Season methods

		#region Schedule methods

		/// <inheritdoc />
		public Task<PaginatedTenraiResponse<ICollection<Anime>>> GetScheduleAsync(CancellationToken cancellationToken = default)
			=> GetScheduleAsync(new ScheduleSearchConfig(), cancellationToken);

		/// <inheritdoc />
		public Task<PaginatedTenraiResponse<ICollection<Anime>>> GetScheduleAsync(int page, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(page, nameof(page));
			return GetScheduleAsync(new ScheduleSearchConfig { Page = page }, cancellationToken);
		}

		/// <inheritdoc />
		public Task<PaginatedTenraiResponse<ICollection<Anime>>> GetScheduleAsync(ScheduledDay scheduledDay, CancellationToken cancellationToken = default)
		{
			Guard.IsValidEnum(scheduledDay, nameof(scheduledDay));
			return GetScheduleAsync(new ScheduleSearchConfig { Day = scheduledDay }, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<PaginatedTenraiResponse<ICollection<Anime>>> GetScheduleAsync(ScheduleSearchConfig searchConfig, CancellationToken cancellationToken = default)
		{
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			var endpointParts = new[] { TenraiEndpointConsts.Schedules + QueryFor(searchConfig) };
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<Anime>>>(endpointParts, cancellationToken);
		}

		#endregion Schedule methods

		#region Top methods

		/// <inheritdoc />
		public Task<PaginatedTenraiResponse<ICollection<Anime>>> GetTopAnimeAsync(CancellationToken cancellationToken = default)
			=> GetTopAnimeAsync(new AnimeTopSearchConfig(), cancellationToken);

		/// <inheritdoc />
		public Task<PaginatedTenraiResponse<ICollection<Anime>>> GetTopAnimeAsync(int page, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(page, nameof(page));
			return GetTopAnimeAsync(new AnimeTopSearchConfig { Page = page }, cancellationToken);
		}

		/// <inheritdoc />
		public Task<PaginatedTenraiResponse<ICollection<Anime>>> GetTopAnimeAsync(TopAnimeFilter filter, CancellationToken cancellationToken = default)
		{
			Guard.IsValidEnum(filter, nameof(filter));
			return GetTopAnimeAsync(new AnimeTopSearchConfig { Filter = filter }, cancellationToken);
		}
		
        /// <inheritdoc />
        public Task<PaginatedTenraiResponse<ICollection<Anime>>> GetTopAnimeAsync(TopAnimeFilter filter, int page, CancellationToken cancellationToken = default)
        {
	        Guard.IsValidEnum(filter, nameof(filter));
            Guard.IsGreaterThanZero(page, nameof(page));
            return GetTopAnimeAsync(new AnimeTopSearchConfig { Filter = filter, Page = page }, cancellationToken);
        }
		
        /// <inheritdoc />
        public async Task<PaginatedTenraiResponse<ICollection<Anime>>> GetTopAnimeAsync(AnimeTopSearchConfig searchConfig, CancellationToken cancellationToken = default)
        {
	        Guard.IsNotNull(searchConfig, nameof(searchConfig));
	        var endpointParts = new[] { TenraiEndpointConsts.TopList, TenraiEndpointConsts.Anime + QueryFor(searchConfig)};
	        return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<Anime>>>(endpointParts, cancellationToken);
        }
        
        /// <inheritdoc />
        public Task<PaginatedTenraiResponse<ICollection<Manga>>> GetTopMangaAsync(CancellationToken cancellationToken = default)
			=> GetTopMangaAsync(new MangaTopSearchConfig(), cancellationToken);

		/// <inheritdoc />
		public Task<PaginatedTenraiResponse<ICollection<Manga>>> GetTopMangaAsync(int page, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(page, nameof(page));
			return GetTopMangaAsync(new MangaTopSearchConfig { Page = page }, cancellationToken);
		}
		
		/// <inheritdoc />
		public async Task<PaginatedTenraiResponse<ICollection<Manga>>> GetTopMangaAsync(MangaTopSearchConfig searchConfig, CancellationToken cancellationToken = default)
		{
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			var endpointParts = new[] { TenraiEndpointConsts.TopList, TenraiEndpointConsts.Manga + QueryFor(searchConfig)};
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<Manga>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public Task<PaginatedTenraiResponse<ICollection<Person>>> GetTopPeopleAsync(CancellationToken cancellationToken = default)
			=> GetTopPeopleAsync(new PagingConfig(), cancellationToken);

		/// <inheritdoc />
		public async Task<PaginatedTenraiResponse<ICollection<Person>>> GetTopPeopleAsync(PagingConfig searchConfig, CancellationToken cancellationToken = default)
		{
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			var endpointParts = new[] { TenraiEndpointConsts.TopList, TenraiEndpointConsts.People + QueryFor(searchConfig) };
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<Person>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public Task<PaginatedTenraiResponse<ICollection<Person>>> GetTopPeopleAsync(int page, CancellationToken cancellationToken = default)
			=> GetTopPeopleAsync(new PagingConfig { Page = page }, cancellationToken);

		/// <inheritdoc />
		public Task<PaginatedTenraiResponse<ICollection<Character>>> GetTopCharactersAsync(CancellationToken cancellationToken = default)
			=> GetTopCharactersAsync(new PagingConfig(), cancellationToken);

		/// <inheritdoc />
		public async Task<PaginatedTenraiResponse<ICollection<Character>>> GetTopCharactersAsync(PagingConfig searchConfig, CancellationToken cancellationToken = default)
		{
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			var endpointParts = new[] { TenraiEndpointConsts.TopList, TenraiEndpointConsts.Characters + QueryFor(searchConfig) };
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<Character>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public Task<PaginatedTenraiResponse<ICollection<Character>>> GetTopCharactersAsync(int page, CancellationToken cancellationToken = default)
			=> GetTopCharactersAsync(new PagingConfig { Page = page }, cancellationToken);

		/// <inheritdoc />
		public Task<PaginatedTenraiResponse<ICollection<Review>>> GetTopReviewsAsync(CancellationToken cancellationToken = default)
			=> GetTopReviewsAsync(new TopReviewsConfig(), cancellationToken);

		/// <inheritdoc />
		public async Task<PaginatedTenraiResponse<ICollection<Review>>> GetTopReviewsAsync(TopReviewsConfig searchConfig, CancellationToken cancellationToken = default)
		{
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			var endpointParts = new[] { TenraiEndpointConsts.TopList, TenraiEndpointConsts.Reviews + QueryFor(searchConfig) };
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<Review>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public Task<PaginatedTenraiResponse<ICollection<TopRecommendation>>> GetTopAnimeRecommendationsAsync(CancellationToken cancellationToken = default)
			=> GetTopAnimeRecommendationsAsync(new TopRecommendationsConfig(), cancellationToken);

		/// <inheritdoc />
		public Task<PaginatedTenraiResponse<ICollection<TopRecommendation>>> GetTopAnimeRecommendationsAsync(TopRecommendationsConfig searchConfig, CancellationToken cancellationToken = default)
			=> GetTopRecommendationsAsync(TenraiEndpointConsts.Anime, searchConfig, cancellationToken);

		/// <inheritdoc />
		public Task<PaginatedTenraiResponse<ICollection<TopRecommendation>>> GetTopMangaRecommendationsAsync(CancellationToken cancellationToken = default)
			=> GetTopMangaRecommendationsAsync(new TopRecommendationsConfig(), cancellationToken);

		/// <inheritdoc />
		public Task<PaginatedTenraiResponse<ICollection<TopRecommendation>>> GetTopMangaRecommendationsAsync(TopRecommendationsConfig searchConfig, CancellationToken cancellationToken = default)
			=> GetTopRecommendationsAsync(TenraiEndpointConsts.Manga, searchConfig, cancellationToken);

		/// <inheritdoc />
		public Task<PaginatedTenraiResponse<ICollection<TopEpisode>>> GetTopEpisodesAsync(CancellationToken cancellationToken = default)
			=> GetTopEpisodesAsync(new TopEpisodesConfig(), cancellationToken);

		/// <inheritdoc />
		public async Task<PaginatedTenraiResponse<ICollection<TopEpisode>>> GetTopEpisodesAsync(TopEpisodesConfig searchConfig, CancellationToken cancellationToken = default)
		{
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			var endpointParts = new[] { TenraiEndpointConsts.TopList, TenraiEndpointConsts.Episodes + QueryFor(searchConfig) };
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<TopEpisode>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public Task<PaginatedTenraiResponse<ICollection<TopVideo>>> GetTopVideosAsync(CancellationToken cancellationToken = default)
			=> GetTopVideosAsync(new TopVideosConfig(), cancellationToken);

		/// <inheritdoc />
		public async Task<PaginatedTenraiResponse<ICollection<TopVideo>>> GetTopVideosAsync(TopVideosConfig searchConfig, CancellationToken cancellationToken = default)
		{
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			var endpointParts = new[] { TenraiEndpointConsts.TopList, TenraiEndpointConsts.Videos + QueryFor(searchConfig) };
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<TopVideo>>>(endpointParts, cancellationToken);
		}

		#endregion Top methods

		#region Genre methods

		/// <inheritdoc />
		public Task<BaseTenraiResponse<ICollection<Genre>>> GetAnimeGenresAsync(CancellationToken cancellationToken = default)
			=> GetAnimeGenresAsync(new GenresConfig(), cancellationToken);

		/// <inheritdoc />
		public async Task<BaseTenraiResponse<ICollection<Genre>>> GetAnimeGenresAsync(GenresConfig searchConfig, CancellationToken cancellationToken = default)
		{
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			var endpointParts = new[] { TenraiEndpointConsts.Genres, TenraiEndpointConsts.Anime + QueryFor(searchConfig) };
			return await ExecuteGetRequestAsync<BaseTenraiResponse<ICollection<Genre>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public Task<BaseTenraiResponse<ICollection<Genre>>> GetAnimeGenresAsync(GenresFilter filter, CancellationToken cancellationToken = default)
			=> GetAnimeGenresAsync(new GenresConfig { Filter = filter }, cancellationToken);

		/// <inheritdoc />
		public Task<BaseTenraiResponse<ICollection<Genre>>> GetMangaGenresAsync(CancellationToken cancellationToken = default)
			=> GetMangaGenresAsync(new GenresConfig(), cancellationToken);

		/// <inheritdoc />
		public async Task<BaseTenraiResponse<ICollection<Genre>>> GetMangaGenresAsync(GenresConfig searchConfig, CancellationToken cancellationToken = default)
		{
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			var endpointParts = new[] { TenraiEndpointConsts.Genres, TenraiEndpointConsts.Manga + QueryFor(searchConfig) };
			return await ExecuteGetRequestAsync<BaseTenraiResponse<ICollection<Genre>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public Task<BaseTenraiResponse<ICollection<Genre>>> GetMangaGenresAsync(GenresFilter filter, CancellationToken cancellationToken = default)
			=> GetMangaGenresAsync(new GenresConfig { Filter = filter }, cancellationToken);

		#endregion Genre methods

		#region Producer methods

		/// <inheritdoc />
		public Task<PaginatedTenraiResponse<ICollection<Producer>>> GetProducersAsync(CancellationToken cancellationToken = default)
			=> SearchProducersAsync(new ProducerSearchConfig(), cancellationToken);

		/// <inheritdoc />
		public Task<PaginatedTenraiResponse<ICollection<Producer>>> GetProducersAsync(int page, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(page, nameof(page));
			return SearchProducersAsync(new ProducerSearchConfig { Page = page }, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<PaginatedTenraiResponse<ICollection<Producer>>> SearchProducersAsync(ProducerSearchConfig searchConfig, CancellationToken cancellationToken = default)
		{
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			var endpointParts = new[] { TenraiEndpointConsts.Producers + QueryFor(searchConfig) };
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
		public async Task<IdsTenraiResponse> GetProducerIdsAsync(CancellationToken cancellationToken = default)
		{
			var endpointParts = new[] { TenraiEndpointConsts.Producers, TenraiEndpointConsts.Ids };
			return await ExecuteGetRequestAsync<IdsTenraiResponse>(endpointParts, cancellationToken);
		}

		#endregion Producer methods

		#region Magazine methods

		/// <inheritdoc />
		public Task<PaginatedTenraiResponse<ICollection<Magazine>>> GetMagazinesAsync(CancellationToken cancellationToken = default)
			=> SearchMagazinesAsync(new MagazineSearchConfig(), cancellationToken);

		/// <inheritdoc />
		public Task<PaginatedTenraiResponse<ICollection<Magazine>>> GetMagazinesAsync(int page, CancellationToken cancellationToken = default)
			=> SearchMagazinesAsync(new MagazineSearchConfig { Page = page }, cancellationToken);

		/// <inheritdoc />
		public async Task<PaginatedTenraiResponse<ICollection<Magazine>>> SearchMagazinesAsync(MagazineSearchConfig searchConfig, CancellationToken cancellationToken = default)
		{
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			var endpointParts = new[] { TenraiEndpointConsts.Magazines + QueryFor(searchConfig) };
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
		public Task<BaseTenraiResponse<Anime>> GetRandomAnimeAsync(CancellationToken cancellationToken = default)
			=> GetRandomAnimeAsync(new RandomAnimeConfig(), cancellationToken);

		/// <inheritdoc/>
		public Task<BaseTenraiResponse<Manga>> GetRandomMangaAsync(CancellationToken cancellationToken = default)
			=> GetRandomMangaAsync(new RandomMangaConfig(), cancellationToken);

		/// <inheritdoc/>
		public async Task<BaseTenraiResponse<Anime>> GetRandomAnimeAsync(RandomAnimeConfig config, CancellationToken cancellationToken = default)
		{
			Guard.IsNotNull(config, nameof(config));
			var endpointParts = new[] { TenraiEndpointConsts.Random, TenraiEndpointConsts.Anime + QueryFor(config) };
			return await ExecuteGetRequestAsync<BaseTenraiResponse<Anime>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc/>
		public async Task<BaseTenraiResponse<Manga>> GetRandomMangaAsync(RandomMangaConfig config, CancellationToken cancellationToken = default)
		{
			Guard.IsNotNull(config, nameof(config));
			var endpointParts = new[] { TenraiEndpointConsts.Random, TenraiEndpointConsts.Manga + QueryFor(config) };
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
		public async Task<BaseTenraiResponse<Character>> GetRandomCharacterAsync(RandomFavoritesConfig config, CancellationToken cancellationToken = default)
		{
			Guard.IsNotNull(config, nameof(config));
			var endpointParts = new[] { TenraiEndpointConsts.Random, TenraiEndpointConsts.Characters + QueryFor(config) };
			return await ExecuteGetRequestAsync<BaseTenraiResponse<Character>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc/>
		public async Task<BaseTenraiResponse<Person>> GetRandomPersonAsync(RandomFavoritesConfig config, CancellationToken cancellationToken = default)
		{
			Guard.IsNotNull(config, nameof(config));
			var endpointParts = new[] { TenraiEndpointConsts.Random, TenraiEndpointConsts.People + QueryFor(config) };
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
		public Task<PaginatedTenraiResponse<ICollection<Review>>> GetRecentAnimeReviewsAsync(CancellationToken cancellationToken = default)
			=> GetRecentAnimeReviewsAsync(new RecentReviewsConfig(), cancellationToken);

		/// <inheritdoc />
		public async Task<PaginatedTenraiResponse<ICollection<Review>>> GetRecentAnimeReviewsAsync(RecentReviewsConfig searchConfig, CancellationToken cancellationToken = default)
		{
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			var endpointParts = new[] { TenraiEndpointConsts.Reviews, TenraiEndpointConsts.Anime + QueryFor(searchConfig) };
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<Review>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public Task<PaginatedTenraiResponse<ICollection<Review>>> GetRecentMangaReviewsAsync(CancellationToken cancellationToken = default)
			=> GetRecentMangaReviewsAsync(new RecentReviewsConfig(), cancellationToken);

		/// <inheritdoc />
		public async Task<PaginatedTenraiResponse<ICollection<Review>>> GetRecentMangaReviewsAsync(RecentReviewsConfig searchConfig, CancellationToken cancellationToken = default)
		{
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			var endpointParts = new[] { TenraiEndpointConsts.Reviews, TenraiEndpointConsts.Manga + QueryFor(searchConfig) };
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<Review>>>(endpointParts, cancellationToken);
		}
		
		#endregion
		
		#region Recommendations methods
		
		/// <inheritdoc />
		public Task<PaginatedTenraiResponse<ICollection<UserRecommendation>>> GetRecentAnimeRecommendationsAsync(CancellationToken cancellationToken = default)
			=> GetRecentAnimeRecommendationsAsync(new RecentRecommendationsConfig(), cancellationToken);

		/// <inheritdoc />
		public async Task<PaginatedTenraiResponse<ICollection<UserRecommendation>>> GetRecentAnimeRecommendationsAsync(RecentRecommendationsConfig searchConfig, CancellationToken cancellationToken = default)
		{
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			var endpointParts = new[] { TenraiEndpointConsts.Recommendations, TenraiEndpointConsts.Anime + QueryFor(searchConfig) };
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<UserRecommendation>>>(endpointParts, cancellationToken);
		}
		
		/// <inheritdoc />
		public Task<PaginatedTenraiResponse<ICollection<UserRecommendation>>> GetRecentAnimeRecommendationsAsync(int page, CancellationToken cancellationToken = default)
			=> GetRecentAnimeRecommendationsAsync(new RecentRecommendationsConfig { Page = page }, cancellationToken);
		
		/// <inheritdoc />
		public Task<PaginatedTenraiResponse<ICollection<UserRecommendation>>> GetRecentMangaRecommendationsAsync(CancellationToken cancellationToken = default)
			=> GetRecentMangaRecommendationsAsync(new RecentRecommendationsConfig(), cancellationToken);

		/// <inheritdoc />
		public async Task<PaginatedTenraiResponse<ICollection<UserRecommendation>>> GetRecentMangaRecommendationsAsync(RecentRecommendationsConfig searchConfig, CancellationToken cancellationToken = default)
		{
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			var endpointParts = new[] { TenraiEndpointConsts.Recommendations, TenraiEndpointConsts.Manga + QueryFor(searchConfig) };
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<UserRecommendation>>>(endpointParts, cancellationToken);
		}
		
		/// <inheritdoc />
		public Task<PaginatedTenraiResponse<ICollection<UserRecommendation>>> GetRecentMangaRecommendationsAsync(int page, CancellationToken cancellationToken = default)
			=> GetRecentMangaRecommendationsAsync(new RecentRecommendationsConfig { Page = page }, cancellationToken);
		
		#endregion

		#region Search methods
		
		
		
		/// <inheritdoc />
		public Task<PaginatedTenraiResponse<ICollection<Anime>>> SearchAnimeAsync(string query, CancellationToken cancellationToken = default)
			=> SearchAnimeAsync(new AnimeSearchConfig {Query = query}, cancellationToken);
		
		/// <inheritdoc />
		public async Task<PaginatedTenraiResponse<ICollection<Anime>>> SearchAnimeAsync(AnimeSearchConfig searchConfig, CancellationToken cancellationToken = default)
		{
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			var endpointParts = new[] { TenraiEndpointConsts.Anime + QueryFor(searchConfig)};
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<Anime>>>(endpointParts, cancellationToken);
		}
		
		/// <inheritdoc />
		public Task<PaginatedTenraiResponse<ICollection<Manga>>> SearchMangaAsync(string query, CancellationToken cancellationToken = default)
			=> SearchMangaAsync(new MangaSearchConfig {Query = query}, cancellationToken);
		
		/// <inheritdoc />
		public async Task<PaginatedTenraiResponse<ICollection<Manga>>> SearchMangaAsync(MangaSearchConfig searchConfig, CancellationToken cancellationToken = default)
		{
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			var endpointParts = new[] { TenraiEndpointConsts.Manga + QueryFor(searchConfig)};
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<Manga>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public Task<PaginatedTenraiResponse<ICollection<Person>>> SearchPersonAsync(string query, CancellationToken cancellationToken = default)
			=> SearchPersonAsync(new PersonSearchConfig {Query = query}, cancellationToken);
		
		/// <inheritdoc />
		public async Task<PaginatedTenraiResponse<ICollection<Person>>> SearchPersonAsync(PersonSearchConfig searchConfig, CancellationToken cancellationToken = default)
		{	
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			var endpointParts = new[] { TenraiEndpointConsts.People + QueryFor(searchConfig)};
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<Person>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public Task<PaginatedTenraiResponse<ICollection<Character>>> SearchCharacterAsync(string query, CancellationToken cancellationToken = default)
			=> SearchCharacterAsync(new CharacterSearchConfig {Query = query}, cancellationToken);
		
		/// <inheritdoc />
		public async Task<PaginatedTenraiResponse<ICollection<Character>>> SearchCharacterAsync(CharacterSearchConfig searchConfig, CancellationToken cancellationToken = default)
		{
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			var endpointParts = new[] { TenraiEndpointConsts.Characters + QueryFor(searchConfig)};
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
		public Task<PaginatedTenraiResponse<ICollection<InterestStack>>> GetInterestStacksAsync(CancellationToken cancellationToken = default)
			=> SearchInterestStacksAsync(new InterestStackSearchConfig(), cancellationToken);

		/// <inheritdoc />
		public Task<PaginatedTenraiResponse<ICollection<InterestStack>>> GetInterestStacksAsync(int page, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(page, nameof(page));
			return SearchInterestStacksAsync(new InterestStackSearchConfig { Page = page }, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<PaginatedTenraiResponse<ICollection<InterestStack>>> SearchInterestStacksAsync(InterestStackSearchConfig searchConfig, CancellationToken cancellationToken = default)
		{
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			var endpointParts = new[] { TenraiEndpointConsts.Stacks + QueryFor(searchConfig) };
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<InterestStack>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public Task<BaseTenraiResponse<InterestStackDetails>> GetInterestStackAsync(long id, CancellationToken cancellationToken = default)
			=> GetInterestStackAsync(id, new SfwConfig(), cancellationToken);

		/// <inheritdoc />
		public async Task<BaseTenraiResponse<InterestStackDetails>> GetInterestStackAsync(long id, SfwConfig searchConfig, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			var endpointParts = new[] { TenraiEndpointConsts.Stacks, id.ToString() + QueryFor(searchConfig) };
			return await ExecuteGetRequestAsync<BaseTenraiResponse<InterestStackDetails>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public Task<PaginatedTenraiResponse<ICollection<InterestStack>>> GetAnimeInterestStacksAsync(long id, CancellationToken cancellationToken = default)
			=> GetAnimeInterestStacksAsync(id, new InterestStackListConfig(), cancellationToken);

		/// <inheritdoc />
		public async Task<PaginatedTenraiResponse<ICollection<InterestStack>>> GetAnimeInterestStacksAsync(long id, InterestStackListConfig searchConfig, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			var endpointParts = new[] { TenraiEndpointConsts.Anime, id.ToString(), TenraiEndpointConsts.Stacks + QueryFor(searchConfig) };
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<InterestStack>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public Task<PaginatedTenraiResponse<ICollection<InterestStack>>> GetAnimeInterestStacksAsync(long id, int page, CancellationToken cancellationToken = default)
			=> GetAnimeInterestStacksAsync(id, new InterestStackListConfig { Page = page }, cancellationToken);

		/// <inheritdoc />
		public Task<PaginatedTenraiResponse<ICollection<InterestStack>>> GetMangaInterestStacksAsync(long id, CancellationToken cancellationToken = default)
			=> GetMangaInterestStacksAsync(id, new InterestStackListConfig(), cancellationToken);

		/// <inheritdoc />
		public async Task<PaginatedTenraiResponse<ICollection<InterestStack>>> GetMangaInterestStacksAsync(long id, InterestStackListConfig searchConfig, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			var endpointParts = new[] { TenraiEndpointConsts.Manga, id.ToString(), TenraiEndpointConsts.Stacks + QueryFor(searchConfig) };
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<InterestStack>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public Task<PaginatedTenraiResponse<ICollection<InterestStack>>> GetMangaInterestStacksAsync(long id, int page, CancellationToken cancellationToken = default)
			=> GetMangaInterestStacksAsync(id, new InterestStackListConfig { Page = page }, cancellationToken);

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

		#region News and article methods

		/// <inheritdoc />
		public Task<PaginatedTenraiResponse<ICollection<News>>> GetNewsAsync(CancellationToken cancellationToken = default)
			=> SearchNewsAsync(new NewsSearchConfig(), cancellationToken);

		/// <inheritdoc />
		public async Task<PaginatedTenraiResponse<ICollection<News>>> SearchNewsAsync(NewsSearchConfig searchConfig, CancellationToken cancellationToken = default)
		{
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			var endpointParts = new[] { TenraiEndpointConsts.News + QueryFor(searchConfig) };
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<News>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<BaseTenraiResponse<NewsDetails>> GetNewsItemAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { TenraiEndpointConsts.News, id.ToString() };
			return await ExecuteGetRequestAsync<BaseTenraiResponse<NewsDetails>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<BaseTenraiResponse<ICollection<ContentTag>>> GetNewsTagsAsync(CancellationToken cancellationToken = default)
		{
			var endpointParts = new[] { TenraiEndpointConsts.News, TenraiEndpointConsts.Tags };
			return await ExecuteGetRequestAsync<BaseTenraiResponse<ICollection<ContentTag>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public Task<PaginatedTenraiResponse<ICollection<Article>>> GetArticlesAsync(CancellationToken cancellationToken = default)
			=> SearchArticlesAsync(new ArticleSearchConfig(), cancellationToken);

		/// <inheritdoc />
		public async Task<PaginatedTenraiResponse<ICollection<Article>>> SearchArticlesAsync(ArticleSearchConfig searchConfig, CancellationToken cancellationToken = default)
		{
			Guard.IsNotNull(searchConfig, nameof(searchConfig));
			var endpointParts = new[] { TenraiEndpointConsts.Articles + QueryFor(searchConfig) };
			return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<Article>>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<BaseTenraiResponse<ArticleDetails>> GetArticleAsync(long id, CancellationToken cancellationToken = default)
		{
			Guard.IsGreaterThanZero(id, nameof(id));
			var endpointParts = new[] { TenraiEndpointConsts.Articles, id.ToString() };
			return await ExecuteGetRequestAsync<BaseTenraiResponse<ArticleDetails>>(endpointParts, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<BaseTenraiResponse<ICollection<ContentTag>>> GetArticleTagsAsync(CancellationToken cancellationToken = default)
		{
			var endpointParts = new[] { TenraiEndpointConsts.Articles, TenraiEndpointConsts.Tags };
			return await ExecuteGetRequestAsync<BaseTenraiResponse<ICollection<ContentTag>>>(endpointParts, cancellationToken);
		}

		#endregion News and article methods

		#endregion Public Methods
	}
}
