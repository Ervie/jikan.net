using Tenrai.Helpers;
using Tenrai.Interfaces;

namespace Tenrai;

/// <summary>
/// Filters for the top videos list.
/// </summary>
public class TopVideosConfig : ISearchConfig, ISfwConfig
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
	/// Airing state of the anime. Not sent when null (the API treats that as <see cref="TopVideosFilter.All"/>).
	/// </summary>
	public TopVideosFilter? Filter { get; set; }

	/// <summary>
	/// Promos or music videos. Both when null.
	/// </summary>
	public TopVideoType? Type { get; set; }

	/// <summary>
	/// Year the video was published. The API rejects it combined with <see cref="MinimumYear"/> or <see cref="MaximumYear"/>.
	/// </summary>
	public int? Year { get; set; }

	/// <summary>
	/// Earliest year the video was published.
	/// </summary>
	public int? MinimumYear { get; set; }

	/// <summary>
	/// Latest year the video was published.
	/// </summary>
	public int? MaximumYear { get; set; }

	/// <summary>
	/// Select order property.
	/// </summary>
	public TopVideosOrderBy OrderBy { get; set; }

	/// <summary>
	/// Define sort direction for <see cref="OrderBy">OrderBy</see> property.
	/// </summary>
	public SortDirection SortDirection { get; set; }

	/// <summary>
	/// Filter out adult entries. Null uses the client setting (<see cref="Config.TenraiClientConfiguration.Sfw"/>).
	/// </summary>
	public bool? Sfw { get; set; }

	/// <summary>
	/// Also filter out ecchi entries, on top of <see cref="Sfw"/>. Null uses the client setting.
	/// </summary>
	public bool? SfwStrict { get; set; }

	/// <summary>
	/// Create query from current parameters for the request.
	/// </summary>
	/// <returns>Query from current parameters for the request</returns>
	public string ConfigToString()
	{
		var query = new QueryBuilder()
			.Page(Page)
			.Limit(Limit)
			.Add("filter", Filter)
			.Add("type", Type)
			.Add("year", Year)
			.Add("min_year", MinimumYear)
			.Add("max_year", MaximumYear);

		if (OrderBy != TopVideosOrderBy.NoSorting)
		{
			query.Add("order_by", OrderBy).Add("sort", SortDirection);
		}

		return query.Sfw(Sfw, SfwStrict).ToString();
	}
}
