using Tenrai.Config;
using Tenrai.Consts;
using Tenrai.Exceptions;
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

namespace Tenrai;

/// <summary>
/// Implementation of Tenrai wrapper for .Net platform.
/// </summary>
public partial class TenraiClient : ITenrai
{
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
			if (!_tenraiConfiguration.SuppressException) throw new TenraiRequestException(ErrorMessagesConst.SerializationFailed + Environment.NewLine + "Inner exception message: " + ex.Message, ex);
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
}
