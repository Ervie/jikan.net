using Tenrai.Helpers;
using Tenrai.Interfaces;

namespace Tenrai;

/// <summary>
/// Sorting and paging for the top episodes list.
/// </summary>
public class TopEpisodesConfig : ISearchConfig, ISfwConfig
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
	/// Select order property. Always descending.
	/// </summary>
	public TopEpisodesOrderBy OrderBy { get; set; }

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
		return new QueryBuilder()
			.Page(Page)
			.Limit(Limit)
			.Add("order_by", OrderBy)
			.Sfw(Sfw, SfwStrict)
			.ToString();
	}
}
