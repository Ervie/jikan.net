using Tenrai.Helpers;
using Tenrai.Interfaces;

namespace Tenrai;

/// <summary>
/// Filters for the recent anime or manga recommendations.
/// </summary>
public class RecentRecommendationsConfig : ISearchConfig, ISfwConfig
{
	/// <summary>
	/// 1-based page index (e.g. 1 returns the first page, 2 the second).
	/// </summary>
	public int? Page { get; set; }

	/// <summary>
	/// Size of the page (100 is the max, and the API default).
	/// </summary>
	public int? Limit { get; set; }

	/// <summary>
	/// Only recommendations written by this MyAnimeList user (full username).
	/// </summary>
	public string Username { get; set; }

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
			.Add("username", Username)
			.Sfw(Sfw, SfwStrict)
			.ToString();
	}
}
