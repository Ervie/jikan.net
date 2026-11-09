using Tenrai.Helpers;
using Tenrai.Interfaces;

namespace Tenrai
{
	/// <summary>
	/// Model class of query configuration for review requests.
	/// </summary>
	public class ReviewsSearchConfig : ISearchConfig
	{
		/// <summary>
		/// Index of the page to retrieve.
		/// </summary>
		public int? Page { get; set; }

		/// <summary>
		/// Number of entries to return per page.
		/// Only honored by the recent (<c>/reviews/{type}</c>) and top (<c>/top/reviews</c>) review endpoints.
		/// </summary>
		public int? Limit { get; set; }

		/// <summary>
		/// Order in which reviews are returned. Defaults to <see cref="ReviewSortOrder.MostHelpful"/> on the API when omitted.
		/// </summary>
		public ReviewSortOrder? Sort { get; set; }

		/// <summary>
		/// How preliminary reviews are handled (include, exclude, or return only preliminary reviews).
		/// </summary>
		public ReviewFilter? Preliminary { get; set; }

		/// <summary>
		/// How spoiler reviews are handled (include, exclude, or return only spoiler reviews).
		/// </summary>
		public ReviewFilter? Spoilers { get; set; }

		/// <summary>
		/// Filter reviews by sentiment (recommended, mixed feelings, or not recommended).
		/// </summary>
		public ReviewSentiment? Sentiment { get; set; }

		/// <summary>
		/// Filter by target entry type. Only applies to the top reviews (<c>/top/reviews</c>) endpoint.
		/// </summary>
		public ReviewTargetType? Type { get; set; }

		/// <summary>
		/// Create query from current parameters for the request.
		/// </summary>
		/// <returns>Query from current parameters for the request.</returns>
		public string ConfigToString()
		{
			return new QueryBuilder()
				.Page(Page)
				.Limit(Limit)
				.Add("sort", Sort)
				.Add("preliminary", Preliminary)
				.Add("spoilers", Spoilers)
				.Add("sentiment", Sentiment)
				.Add("type", Type)
				.ToString();
		}
	}
}
