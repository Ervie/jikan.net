using Tenrai.Interfaces;
using Tenrai.Helpers;

namespace Tenrai
{
	/// <summary>
	/// Model class of search configuration for advanced anime search.
	/// </summary>
	public class AnimeTopSearchConfig : ISearchConfig
	{
		/// <summary>
		/// Index of page folding 50 records of top ranging (e.g. 1 will return first 50 records, 2 will return record from 51 to 100 etc.)
		/// </summary>
		public int? Page { get; set; }
		
		/// <summary>
		/// Anime type of searched result.
		/// </summary>
		public AnimeType Type { get; set; } = AnimeType.EveryType;
		
		/// <summary>
		/// Top items filter types
		/// </summary>
		public TopAnimeFilter Filter { get; set; } = TopAnimeFilter.NoFilter;

		/// <summary>
		/// Age rating.
		/// </summary>
		public AnimeAgeRating Rating { get; set; } = AnimeAgeRating.EveryRating;

		/// <summary>
		/// Should only search for sfw title. Filter out adult entries.
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
			return new QueryBuilder()
				.Page(Page)
				.Add("type", Type)
				.Add("rating", Rating)
				.Add("filter", Filter)
				.Sfw(Sfw, SfwStrict)
				.ToString();
		}
	}
}