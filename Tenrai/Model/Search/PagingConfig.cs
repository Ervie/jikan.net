using Tenrai.Helpers;
using Tenrai.Interfaces;

namespace Tenrai;

/// <summary>
/// Page and page size, for the top people and top characters lists.
/// </summary>
public class PagingConfig : ISearchConfig
{
	/// <summary>
	/// 1-based page index (e.g. 1 returns the first page, 2 the second).
	/// </summary>
	public int? Page { get; set; }

	/// <summary>
	/// Size of the page (100 is the max).
	/// </summary>
	public int? Limit { get; set; }

	/// <summary>
	/// Create query from current parameters for the request.
	/// </summary>
	/// <returns>Query from current parameters for the request</returns>
	public string ConfigToString()
	{
		return new QueryBuilder()
			.Page(Page)
			.Limit(Limit)
			.ToString();
	}
}
