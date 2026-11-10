using Tenrai.Helpers;
using Tenrai.Interfaces;

namespace Tenrai;

/// <summary>
/// Filters for the reviews of one anime or manga.
/// </summary>
public class ReviewsSearchConfig : ISearchConfig
{
	/// <summary>
	/// Index of the page to retrieve.
	/// </summary>
	public int? Page { get; set; }

	/// <summary>
	/// Only reviews written by this MyAnimeList user (full username).
	/// </summary>
	public string Username { get; set; }

	/// <summary>
	/// Order in which reviews are returned. Defaults to <see cref="ReviewSortOrder.MostHelpful"/> on the API when omitted.
	/// </summary>
	public ReviewSortOrder? Sort { get; set; }

	/// <summary>
	/// How preliminary reviews are handled (include, exclude, or return only preliminary reviews).
	/// </summary>
	public InclusionFilter? Preliminary { get; set; }

	/// <summary>
	/// How spoiler reviews are handled (include, exclude, or return only spoiler reviews).
	/// </summary>
	public InclusionFilter? Spoilers { get; set; }

	/// <summary>
	/// Filter reviews by sentiment (recommended, mixed feelings, or not recommended).
	/// </summary>
	public ReviewSentiment? Sentiment { get; set; }

	/// <summary>
	/// Create query from current parameters for the request.
	/// </summary>
	/// <returns>Query from current parameters for the request.</returns>
	public string ConfigToString()
	{
		return new QueryBuilder()
			.Page(Page)
			.Add("username", Username)
			.Add("sort", Sort)
			.Add("preliminary", Preliminary)
			.Add("spoilers", Spoilers)
			.Add("sentiment", Sentiment)
			.ToString();
	}
}
