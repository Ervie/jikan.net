using System.Collections.Generic;
using Tenrai.Interfaces;
using Tenrai.Helpers;

namespace Tenrai;

/// <summary>
/// Filters for the top anime ranking.
/// </summary>
public class AnimeTopSearchConfig : ISearchConfig, ISfwConfig
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
	public TopAnimeFilter? Filter { get; set; }

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
	/// Lowest number of episodes.
	/// </summary>
	public int? MinimumEpisodes { get; set; }

	/// <summary>
	/// Highest number of episodes.
	/// </summary>
	public int? MaximumEpisodes { get; set; }

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
			.AddList("rating", Ratings)
			.AddList("genres", Genres)
			.AddList("genres_exclude", ExcludedGenres)
			.AddList("exclude_ids", ExcludedIds)
			.Add("min_members", MinimumMembers)
			.Add("max_members", MaximumMembers)
			.Add("min_episodes", MinimumEpisodes)
			.Add("max_episodes", MaximumEpisodes)
			.Sfw(Sfw, SfwStrict)
			.Add("sort", SortDirection)
			.ToString();
	}
}
