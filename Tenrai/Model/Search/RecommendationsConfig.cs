using Tenrai.Helpers;
using Tenrai.Interfaces;

namespace Tenrai;

/// <summary>
/// Filters for the recommendations of one anime or manga.
/// </summary>
public class RecommendationsConfig : ISearchConfig, ISfwConfig
{
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
			.Add("username", Username)
			.Sfw(Sfw, SfwStrict)
			.ToString();
	}
}
