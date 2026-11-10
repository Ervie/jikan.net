using Tenrai.Helpers;
using Tenrai.Interfaces;

namespace Tenrai;

/// <summary>
/// Filters for the recent anime or manga reviews.
/// </summary>
public class RecentReviewsConfig : ISearchConfig, ISfwConfig
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
	/// Only reviews written by this MyAnimeList user (full username).
	/// </summary>
	public string Username { get; set; }

	/// <summary>
	/// Order in which reviews are returned. Defaults to <see cref="ReviewSortOrder.Newest"/> on the API when omitted.
	/// </summary>
	public ReviewSortOrder? Sort { get; set; }

	/// <summary>
	/// How preliminary reviews are handled (include, exclude, or return only preliminary reviews). Left out by the API unless set.
	/// </summary>
	public InclusionFilter? Preliminary { get; set; }

	/// <summary>
	/// How spoiler reviews are handled (include, exclude, or return only spoiler reviews). Left out by the API unless set.
	/// </summary>
	public InclusionFilter? Spoilers { get; set; }

	/// <summary>
	/// Filter reviews by sentiment (recommended, mixed feelings, or not recommended).
	/// </summary>
	public ReviewSentiment? Sentiment { get; set; }

	/// <summary>
	/// Filter out reviews of adult entries. Null uses the client setting (<see cref="Config.TenraiClientConfiguration.Sfw"/>).
	/// </summary>
	public bool? Sfw { get; set; }

	/// <summary>
	/// Also filter out reviews of ecchi entries, on top of <see cref="Sfw"/>. Null uses the client setting.
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
			.Add("username", Username)
			.Add("sort", Sort)
			.Add("preliminary", Preliminary)
			.Add("spoilers", Spoilers)
			.Add("sentiment", Sentiment)
			.Sfw(Sfw, SfwStrict)
			.ToString();
	}
}
