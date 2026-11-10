using Tenrai.Consts;
using Tenrai.Helpers;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Tenrai;

public partial class TenraiClient
{
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
}
