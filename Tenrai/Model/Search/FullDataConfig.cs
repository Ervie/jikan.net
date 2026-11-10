using System.Collections.Generic;
using Tenrai.Helpers;
using Tenrai.Interfaces;

namespace Tenrai;

/// <summary>
/// Options for <c>GetAnimeFullDataAsync</c> and <c>GetMangaFullDataAsync</c>: more ids in one request, the archive to search, and sfw filtering.
/// </summary>
public class FullDataConfig : ISearchConfig, ISfwConfig
{
	/// <summary>
	/// Extra MAL ids returned in <see cref="FullTenraiResponse{TResponse}.Included"/>. At most 24, as the API allows 25 including the path id.
	/// </summary>
	public ICollection<long> Ids { get; set; } = new List<long>();

	/// <summary>
	/// Search the denied or deleted archive instead of the regular catalogue. Applies to every id in the request.
	/// </summary>
	public FullDataMode? Mode { get; set; }

	/// <summary>
	/// Filter adult entries out of the relations. Null uses the client setting (<see cref="Config.TenraiClientConfiguration.Sfw"/>).
	/// </summary>
	public bool? Sfw { get; set; }

	/// <summary>
	/// Also filter ecchi entries out of the relations, on top of <see cref="Sfw"/>. Null uses the client setting.
	/// </summary>
	public bool? SfwStrict { get; set; }

	/// <summary>
	/// Create query from current parameters for the request.
	/// </summary>
	/// <returns>Query from current parameters for the request</returns>
	public string ConfigToString()
	{
		return new QueryBuilder()
			.AddList("ids", Ids)
			.Add("mode", Mode)
			.Sfw(Sfw, SfwStrict)
			.ToString();
	}
}
