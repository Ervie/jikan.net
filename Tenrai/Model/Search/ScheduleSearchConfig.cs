using System.Collections.Generic;
using Tenrai.Helpers;
using Tenrai.Interfaces;

namespace Tenrai;

/// <summary>
/// Filters for the broadcast schedule.
/// </summary>
public class ScheduleSearchConfig : ISearchConfig, ISfwConfig
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
	/// Broadcast day. Every day when null.
	/// </summary>
	public ScheduledDay? Day { get; set; }

	/// <summary>
	/// Anime types to include.
	/// </summary>
	public ICollection<AnimeType> Types { get; set; } = new List<AnimeType>();

	/// <summary>
	/// Age ratings to include.
	/// </summary>
	public ICollection<AnimeAgeRating> Ratings { get; set; } = new List<AnimeAgeRating>();

	/// <summary>
	/// Genres to include. Entries must have all of them.
	/// </summary>
	public ICollection<AnimeGenreSearch> Genres { get; set; } = new List<AnimeGenreSearch>();

	/// <summary>
	/// Genres to exclude.
	/// </summary>
	public ICollection<AnimeGenreSearch> ExcludedGenres { get; set; } = new List<AnimeGenreSearch>();

	/// <summary>
	/// Filter out adult entries. Null uses the client setting (<see cref="Config.TenraiClientConfiguration.Sfw"/>).
	/// </summary>
	public bool? Sfw { get; set; }

	/// <summary>
	/// Also filter out ecchi entries, on top of <see cref="Sfw"/>. Null uses the client setting.
	/// </summary>
	public bool? SfwStrict { get; set; }

	/// <summary>
	/// Leave out kids titles.
	/// </summary>
	public bool ExcludeKids { get; set; }

	/// <summary>
	/// Unapproved entries: <see cref="InclusionFilter.Include"/> adds them, <see cref="InclusionFilter.Only"/> returns only them.
	/// </summary>
	public InclusionFilter? Unapproved { get; set; }

	/// <summary>
	/// Create query from current parameters for the request.
	/// </summary>
	/// <returns>Query from current parameters for the request</returns>
	public string ConfigToString()
	{
		return new QueryBuilder()
			.Page(Page)
			.Limit(Limit)
			.Add("filter", Day)
			.AddList("type", Types)
			.AddList("rating", Ratings)
			.AddList("genres", Genres)
			.AddList("genres_exclude", ExcludedGenres)
			.Sfw(Sfw, SfwStrict)
			.Flag("kids", ExcludeKids)
			.Add("unapproved", Unapproved)
			.ToString();
	}
}
