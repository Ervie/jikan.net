using System;
using System.Collections.Generic;
using Tenrai.Helpers;
using Tenrai.Interfaces;

namespace Tenrai;

/// <summary>
/// Filters for the bulk manga ids.
/// </summary>
public class MangaIdsConfig : ISearchConfig, ISfwConfig
{
	/// <summary>
	/// Only manga of any of these types. Empty for every type.
	/// </summary>
	public ICollection<MangaType> Types { get; set; } = new List<MangaType>();

	/// <summary>
	/// Only manga with this publishing status.
	/// </summary>
	public PublishingStatus? Status { get; set; }

	/// <summary>
	/// Only manga updated on or after this date.
	/// </summary>
	public DateTime? UpdatedAfter { get; set; }

	/// <summary>
	/// Leave out the ids of adult entries. Null uses the client setting (<see cref="Config.TenraiClientConfiguration.Sfw"/>).
	/// </summary>
	public bool? Sfw { get; set; }

	/// <summary>
	/// Also leave out the ids of ecchi entries, on top of <see cref="Sfw"/>. Null uses the client setting.
	/// </summary>
	public bool? SfwStrict { get; set; }

	/// <summary>
	/// Create query from current parameters for the request.
	/// </summary>
	/// <returns>Query from current parameters for the request</returns>
	public string ConfigToString()
	{
		return new QueryBuilder()
			.AddList("type", Types)
			.Add("status", Status)
			.Add("updated_at", UpdatedAfter)
			.Sfw(Sfw, SfwStrict)
			.ToString();
	}
}
