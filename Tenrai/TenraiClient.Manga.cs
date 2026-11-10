using Tenrai.Consts;
using Tenrai.Helpers;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Tenrai;

public partial class TenraiClient
{
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
}
