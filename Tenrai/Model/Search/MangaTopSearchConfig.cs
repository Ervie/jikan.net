using System.Collections.Generic;
using Tenrai.Helpers;
using Tenrai.Interfaces;

namespace Tenrai;

/// <summary>
/// Filters for the top manga ranking.
/// </summary>
public class MangaTopSearchConfig : ISearchConfig, ISfwConfig
{
	/// <summary>
	/// Index of page folding 50 records of top ranging (e.g. 1 will return first 50 records, 2 will return record from 51 to 100 etc.)
	/// </summary>
	public int? Page { get; set; }

	/// <summary>
	/// Size of the page (100 is the max).
	/// </summary>
	public int? Limit { get; set; }

	/// <summary>
	/// Top items filter types
	/// </summary>
	public TopMangaFilter? Filter { get; set; }

	/// <summary>
	/// Manga types to include.
	/// </summary>
	public ICollection<MangaType> Types { get; set; } = new List<MangaType>();

	/// <summary>
	/// Genres to include. Entries must have all of them.
	/// </summary>
	public ICollection<MangaGenreSearch> Genres { get; set; } = new List<MangaGenreSearch>();

	/// <summary>
	/// Genres to exclude.
	/// </summary>
	public ICollection<MangaGenreSearch> ExcludedGenres { get; set; } = new List<MangaGenreSearch>();

	/// <summary>
	/// Ids to leave out.
	/// </summary>
	public ICollection<long> ExcludedIds { get; set; } = new List<long>();

	/// <summary>
	/// Lowest number of members.
	/// </summary>
	public int? MinimumMembers { get; set; }

	/// <summary>
	/// Highest number of members.
	/// </summary>
	public int? MaximumMembers { get; set; }

	/// <summary>
	/// Lowest number of chapters.
	/// </summary>
	public int? MinimumChapters { get; set; }

	/// <summary>
	/// Highest number of chapters.
	/// </summary>
	public int? MaximumChapters { get; set; }

	/// <summary>
	/// Lowest number of volumes.
	/// </summary>
	public int? MinimumVolumes { get; set; }

	/// <summary>
	/// Highest number of volumes.
	/// </summary>
	public int? MaximumVolumes { get; set; }

	/// <summary>
	/// Should only search for sfw title. Filter out adult entries. Null uses the client setting (<see cref="Config.TenraiClientConfiguration.Sfw"/>).
	/// </summary>
	public bool? Sfw { get; set; }

	/// <summary>
	/// Also filter out ecchi entries, on top of <see cref="Sfw"/>. Null uses the client setting.
	/// </summary>
	public bool? SfwStrict { get; set; }

	/// <summary>
	/// <see cref="SortDirection.Descending"/> reverses the ranking. Top first when null.
	/// </summary>
	public SortDirection? SortDirection { get; set; }

	/// <summary>
	/// Create query from current parameters for search request.
	/// </summary>
	/// <returns>Query from current parameters for search request</returns>
	public string ConfigToString()
	{
		return new QueryBuilder()
			.Page(Page)
			.Limit(Limit)
			.Add("filter", Filter)
			.AddList("type", Types)
			.AddList("genres", Genres)
			.AddList("genres_exclude", ExcludedGenres)
			.AddList("exclude_ids", ExcludedIds)
			.Add("min_members", MinimumMembers)
			.Add("max_members", MaximumMembers)
			.Add("min_chapters", MinimumChapters)
			.Add("max_chapters", MaximumChapters)
			.Add("min_volumes", MinimumVolumes)
			.Add("max_volumes", MaximumVolumes)
			.Sfw(Sfw, SfwStrict)
			.Add("sort", SortDirection)
			.ToString();
	}
}
