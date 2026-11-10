using System;
using Tenrai.Helpers;
using Tenrai.Interfaces;

namespace Tenrai;

/// <summary>
/// Filters for articles about one anime, manga, character or person. <see cref="ArticleSearchConfig"/> adds search text and tag for the full article catalogue.
/// </summary>
public class ArticleFilterConfig : ISearchConfig, ISfwConfig
{
	/// <summary>
	/// 1-based page index.
	/// </summary>
	public int? Page { get; set; }

	/// <summary>
	/// Size of the page (100 is the max).
	/// </summary>
	public int? Limit { get; set; }

	/// <summary>
	/// Only articles written by this MyAnimeList user (full username).
	/// </summary>
	public string AuthorUsername { get; set; }

	/// <summary>
	/// Only articles published on or after this date.
	/// </summary>
	public DateTime? PublishedAfter { get; set; }

	/// <summary>
	/// Only articles published on or before this date.
	/// </summary>
	public DateTime? PublishedBefore { get; set; }

	/// <summary>
	/// Select order property.
	/// </summary>
	public ArticleOrderBy OrderBy { get; set; }

	/// <summary>
	/// Define sort direction for <see cref="OrderBy">OrderBy</see> property.
	/// </summary>
	public SortDirection SortDirection { get; set; }

	/// <summary>
	/// Filter out articles about adult entries. Null uses the client setting (<see cref="Config.TenraiClientConfiguration.Sfw"/>).
	/// </summary>
	public bool? Sfw { get; set; }

	/// <summary>
	/// Also filter out articles about ecchi entries, on top of <see cref="Sfw"/>. Null uses the client setting.
	/// </summary>
	public bool? SfwStrict { get; set; }

	/// <summary>
	/// Create query from current parameters for the request.
	/// </summary>
	/// <returns>Query from current parameters for the request</returns>
	public string ConfigToString()
	{
		var query = new QueryBuilder().Page(Page).Limit(Limit);
		AddSearchTerms(query);
		query
			.Add("author_username", AuthorUsername)
			.Add("published_after", PublishedAfter)
			.Add("published_before", PublishedBefore);

		if (OrderBy != ArticleOrderBy.NoSorting)
		{
			query.Add("order_by", OrderBy).Add("sort", SortDirection);
		}

		return query.Sfw(Sfw, SfwStrict).ToString();
	}

	private protected virtual void AddSearchTerms(QueryBuilder query)
	{
	}
}
