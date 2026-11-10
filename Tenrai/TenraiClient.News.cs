using Tenrai.Consts;
using Tenrai.Helpers;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Tenrai;

public partial class TenraiClient
{
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
}
