using System.Collections.Generic;
using Tenrai.Helpers;
using Tenrai.Interfaces;

namespace Tenrai;

/// <summary>
/// Filters for the season lists (a given season, current, previous, upcoming).
/// </summary>
public class SeasonSearchConfig : ISearchConfig, ISfwConfig
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
	/// Anime types to include.
	/// </summary>
	public ICollection<AnimeType> Types { get; set; } = new List<AnimeType>();

	/// <summary>
	/// Airing status. The upcoming season list ignores it.
	/// </summary>
	public AiringStatus? Status { get; set; }

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
	/// Also return TV series continuing from an earlier season.
	/// </summary>
	public bool IncludeContinuing { get; set; }

	/// <summary>
	/// Unapproved entries: <see cref="InclusionFilter.Include"/> adds them, <see cref="InclusionFilter.Only"/> returns only them.
	/// </summary>
	public InclusionFilter? Unapproved { get; set; }

	/// <summary>
	/// Select order property.
	/// </summary>
	public SeasonOrderBy OrderBy { get; set; }

	/// <summary>
	/// Define sort direction for <see cref="OrderBy">OrderBy</see> property.
	/// </summary>
	public SortDirection SortDirection { get; set; }

	/// <summary>
	/// Create query from current parameters for the request.
	/// </summary>
	/// <returns>Query from current parameters for the request</returns>
	public string ConfigToString()
	{
		var query = new QueryBuilder()
			.Page(Page)
			.Limit(Limit)
			.AddList("filter", Types)
			.Add("status", Status)
			.AddList("rating", Ratings)
			.AddList("genres", Genres)
			.AddList("genres_exclude", ExcludedGenres)
			.AddList("exclude_ids", ExcludedIds)
			.Add("min_members", MinimumMembers)
			.Add("max_members", MaximumMembers)
			.Add("min_episodes", MinimumEpisodes)
			.Add("max_episodes", MaximumEpisodes)
			.Sfw(Sfw, SfwStrict)
			.Flag("kids", ExcludeKids)
			.Flag("continuing", IncludeContinuing)
			.Add("unapproved", Unapproved);

		if (OrderBy != SeasonOrderBy.NoSorting)
		{
			query.Add("order_by", OrderBy).Add("sort", SortDirection);
		}

		return query.ToString();
	}
}
