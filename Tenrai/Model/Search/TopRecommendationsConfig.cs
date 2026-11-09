using Tenrai.Helpers;
using Tenrai.Interfaces;

namespace Tenrai
{
	/// <summary>
	/// Paging for the top recommendations lists.
	/// </summary>
	public class TopRecommendationsConfig : ISearchConfig
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
		/// Filter out adult entries.
		/// </summary>
		public bool Sfw { get; set; } = true;

		/// <summary>
		/// Also filter out ecchi entries, on top of <see cref="Sfw"/>.
		/// </summary>
		public bool SfwStrict { get; set; }

		/// <summary>
		/// Create query from current parameters for the request.
		/// </summary>
		/// <returns>Query from current parameters for the request</returns>
		public string ConfigToString() => BuildQuery(null);

		internal string ConfigToString(string type) => BuildQuery(type);

		private string BuildQuery(string type)
		{
			return new QueryBuilder()
				.Add("type", type)
				.Page(Page)
				.Limit(Limit)
				.Sfw(Sfw, SfwStrict)
				.ToString();
		}
	}
}
