using Tenrai.Helpers;
using Tenrai.Interfaces;

namespace Tenrai;

/// <summary>
/// Filters for the anime or manga genre list.
/// </summary>
public class GenresConfig : ISearchConfig, ISfwConfig
{
	/// <summary>
	/// Only genres of this kind (genres, explicit genres, themes or demographics).
	/// </summary>
	public GenresFilter? Filter { get; set; }

	/// <summary>
	/// Leave out adult genres. Null uses the client setting (<see cref="Config.TenraiClientConfiguration.Sfw"/>).
	/// </summary>
	public bool? Sfw { get; set; }

	/// <summary>
	/// Also leave out ecchi, on top of <see cref="Sfw"/>. Null uses the client setting.
	/// </summary>
	public bool? SfwStrict { get; set; }

	/// <summary>
	/// Create query from current parameters for the request.
	/// </summary>
	/// <returns>Query from current parameters for the request</returns>
	public string ConfigToString()
	{
		return new QueryBuilder()
			.Add("filter", Filter)
			.Sfw(Sfw, SfwStrict)
			.ToString();
	}
}
