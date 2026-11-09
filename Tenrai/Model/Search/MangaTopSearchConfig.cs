using Tenrai.Helpers;
using Tenrai.Interfaces;

namespace Tenrai
{
	/// <summary>
	/// Model class of search configuration for advanced manga search.
	/// </summary>
	public class MangaTopSearchConfig : ISearchConfig
	{
		/// <summary>
		/// Index of page folding 50 records of top ranging (e.g. 1 will return first 50 records, 2 will return record from 51 to 100 etc.)
		/// </summary>
		public int? Page { get; set; }
	
		/// <summary>
		/// Manga type of searched result.
		/// </summary>
		public MangaType Type { get; set; } = MangaType.EveryType;
		
		/// <summary>
		/// Top items filter types
		/// </summary>
		public TopMangaFilter Filter { get; set; } = TopMangaFilter.NoFilter;

		
		/// <summary>
		/// Create query from current parameters for search request.
		/// </summary>
		/// <returns>Query from current parameters for search request</returns>
		public string ConfigToString()
		{
			return new QueryBuilder()
				.Page(Page)
				.Add("type", Type)
				.Add("filter", Filter)
				.ToString();
		}
	}
}