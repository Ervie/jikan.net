using System;
using Tenrai.Helpers;
using Tenrai.Interfaces;

namespace Tenrai;

/// <summary>
/// Date range, order and paging for the day-by-day statistics history of an anime or manga.
/// </summary>
public class StatisticsHistoryConfig : ISearchConfig
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
	/// First day to include.
	/// </summary>
	public DateTime? StartDate { get; set; }

	/// <summary>
	/// Last day to include.
	/// </summary>
	public DateTime? EndDate { get; set; }

	/// <summary>
	/// Order of the days. Not sent when null; the API returns newest first.
	/// </summary>
	public SortDirection? SortDirection { get; set; }

	/// <summary>
	/// Create query from current parameters for the request.
	/// </summary>
	/// <returns>Query from current parameters for the request</returns>
	public string ConfigToString()
	{
		return new QueryBuilder()
			.Page(Page)
			.Limit(Limit)
			.Add("start_date", StartDate)
			.Add("end_date", EndDate)
			.Add("sort", SortDirection)
			.ToString();
	}
}
