using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Tenrai;

public partial interface ITenrai
{
	/// <summary>
	/// Returns the first page of the full news catalogue, newest first.
	/// </summary>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Page of news.</returns>
	Task<PaginatedTenraiResponse<ICollection<News>>> GetNewsAsync(CancellationToken cancellationToken = default);

	/// <summary>
	/// Searches the full news catalogue.
	/// </summary>
	/// <param name="searchConfig">Search text, tag, author, publish dates, sort order, paging and sfw filtering.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Page of matching news.</returns>
	Task<PaginatedTenraiResponse<ICollection<News>>> SearchNewsAsync(NewsSearchConfig searchConfig, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns a news item with its full text and related anime and manga.
	/// </summary>
	/// <param name="id">MAL id of the news item.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>News item with content and relations.</returns>
	Task<BaseTenraiResponse<NewsDetails>> GetNewsItemAsync(long id, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns every news tag with how many news items carry it.
	/// </summary>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>News tags.</returns>
	Task<BaseTenraiResponse<ICollection<ContentTag>>> GetNewsTagsAsync(CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns the first page of the full article catalogue, newest first.
	/// </summary>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Page of articles.</returns>
	Task<PaginatedTenraiResponse<ICollection<Article>>> GetArticlesAsync(CancellationToken cancellationToken = default);

	/// <summary>
	/// Searches the full article catalogue.
	/// </summary>
	/// <param name="searchConfig">Search text, tag, author, publish dates, sort order, paging and sfw filtering.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Page of matching articles.</returns>
	Task<PaginatedTenraiResponse<ICollection<Article>>> SearchArticlesAsync(ArticleSearchConfig searchConfig, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns an article with its full text and related anime, manga, people and characters.
	/// </summary>
	/// <param name="id">MAL id of the article.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Article with content and relations.</returns>
	Task<BaseTenraiResponse<ArticleDetails>> GetArticleAsync(long id, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns every article tag with how many articles carry it.
	/// </summary>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Article tags.</returns>
	Task<BaseTenraiResponse<ICollection<ContentTag>>> GetArticleTagsAsync(CancellationToken cancellationToken = default);
}
