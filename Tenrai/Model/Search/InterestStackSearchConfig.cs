using Tenrai.Helpers;
using Tenrai.Interfaces;

namespace Tenrai
{
	/// <summary>
	/// Model class of search configuration for interest stack search.
	/// </summary>
	public class InterestStackSearchConfig : ISearchConfig
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
		public InterestStackType Type { get; set; } = InterestStackType.EveryType;

		/// <summary>
		/// Select order property.
		/// </summary>
		public InterestStackSearchOrderBy OrderBy { get; set; }

		/// <summary>
		/// Define sort direction for <see cref="OrderBy">OrderBy</see> property.
		/// </summary>
		public SortDirection SortDirection { get; set; }

		/// <summary>
		/// Should only search for sfw stacks. Filter out adult entries.
		/// </summary>
		public bool Sfw { get; set; } = true;

		/// <summary>
		/// Also filter out ecchi entries, on top of <see cref="Sfw"/>.
		/// </summary>
		public bool SfwStrict { get; set; }

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
				.Add("stack_type", Type);

			if (OrderBy != InterestStackSearchOrderBy.NoSorting)
			{
				query.Add("order_by", OrderBy).Add("sort", SortDirection);
			}

			return query.Sfw(Sfw, SfwStrict).ToString();
		}
	}
}
