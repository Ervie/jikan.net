using Tenrai.Helpers;
using Tenrai.Interfaces;

namespace Tenrai;

/// <summary>
/// Filters for the top reviews.
/// </summary>
public class TopReviewsConfig : ISearchConfig, ISfwConfig
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
	/// Only reviews of anime, or only reviews of manga.
	/// </summary>
	public ReviewTargetType? Type { get; set; }

	/// <summary>
	/// Only reviews written by this MyAnimeList user (full username).
	/// </summary>
	public string Username { get; set; }

	/// <summary>
	/// Whether preliminary reviews are included. The API includes them when omitted.
	/// </summary>
	public bool? Preliminary { get; set; }

	/// <summary>
	/// Whether reviews with spoilers are included. The API includes them when omitted.
	/// </summary>
	public bool? Spoilers { get; set; }

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
			.Add("type", Type)
			.Add("username", Username)
			.Add("preliminary", Preliminary)
			.Add("spoilers", Spoilers)
			.Sfw(Sfw, SfwStrict)
			.ToString();
	}
}
