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
}
