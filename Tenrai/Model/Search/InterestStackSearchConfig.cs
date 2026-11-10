using Tenrai.Helpers;
using Tenrai.Interfaces;

namespace Tenrai;

/// <summary>
/// Model class of search configuration for interest stack search.
/// </summary>
public class InterestStackSearchConfig : ISearchConfig, ISfwConfig
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
	/// Search query.
	/// </summary>
	public string Query { get; set; }

	/// <summary>
	/// Type of entries the stack holds.
	/// </summary>
	public InterestStackType? Type { get; set; }

	/// <summary>
	/// How spoiler stacks are handled (include, exclude, or return only spoiler stacks).
	/// The API leaves them out unless this or <see cref="Challenges"/> or <see cref="Official"/> is set.
	/// </summary>
	public InclusionFilter? Spoilers { get; set; }

	/// <summary>
	/// How challenge stacks are handled (include, exclude, or return only challenge stacks).
	/// The API leaves them out unless this or <see cref="Spoilers"/> or <see cref="Official"/> is set.
	/// </summary>
	public InclusionFilter? Challenges { get; set; }

	/// <summary>
	/// How official stacks are handled (include, exclude, or return only official stacks).
	/// The API leaves them out unless this or <see cref="Spoilers"/> or <see cref="Challenges"/> is set.
	/// </summary>
	public InclusionFilter? Official { get; set; }

	/// <summary>
	/// Select order property.
	/// </summary>
	public InterestStackSearchOrderBy OrderBy { get; set; }

	/// <summary>
	/// Define sort direction for <see cref="OrderBy">OrderBy</see> property.
	/// </summary>
	public SortDirection SortDirection { get; set; }

	/// <summary>
	/// Should only search for sfw stacks. Filter out adult entries. Null uses the client setting (<see cref="Config.TenraiClientConfiguration.Sfw"/>).
	/// </summary>
	public bool? Sfw { get; set; }

	/// <summary>
	/// Also filter out ecchi entries, on top of <see cref="Sfw"/>. Null uses the client setting.
	/// </summary>
	public bool? SfwStrict { get; set; }

	/// <summary>
	/// Create query from current parameters for search request.
	/// </summary>
	/// <returns>Query from current parameters for search request</returns>
	public string ConfigToString()
	{
		var query = new QueryBuilder()
			.Page(Page)
			.Limit(Limit)
			.Add("q", Query)
			.Add("stack_type", Type)
			.Add("spoilers", Spoilers)
			.Add("challenges", Challenges)
			.Add("official", Official);

		if (OrderBy != InterestStackSearchOrderBy.NoSorting)
		{
			query.Add("order_by", OrderBy).Add("sort", SortDirection);
		}

		return query.Sfw(Sfw, SfwStrict).ToString();
	}
}
