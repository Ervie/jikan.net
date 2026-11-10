using Tenrai.Helpers;
using Tenrai.Interfaces;
using System;
using System.Collections.Generic;

namespace Tenrai;

/// <summary>
/// Model class of search configuration for advanced manga search.
/// List filters match any of their values unless noted; the API rejects lists over 25 values (1000 for <see cref="Ids"/> and <see cref="ExcludedIds"/>).
/// </summary>
public class MangaSearchConfig : ISearchConfig, ISfwConfig
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
	/// Search query.
	/// </summary>
	public string Query { get; set; }

	/// <summary>
	/// Return entries starting with the given letter.
	/// </summary>
	public char? Letter { get; set; }

	/// <summary>
	/// Manga types to include.
	/// </summary>
	public ICollection<MangaType> Types { get; set; } = new List<MangaType>();

	/// <summary>
	/// Manga types to leave out.
	/// </summary>
	public ICollection<MangaType> ExcludedTypes { get; set; } = new List<MangaType>();

	/// <summary>
	/// Publishing statuses to include.
	/// </summary>
	public ICollection<PublishingStatus> Statuses { get; set; } = new List<PublishingStatus>();

	/// <summary>
	/// Publishing statuses to leave out.
	/// </summary>
	public ICollection<PublishingStatus> ExcludedStatuses { get; set; } = new List<PublishingStatus>();

	/// <summary>
	/// Genres to include. Entries must have all of them.
	/// </summary>
	public ICollection<MangaGenreSearch> Genres { get; set; } = new List<MangaGenreSearch>();

	/// <summary>
	/// Genres to exclude.
	/// </summary>
	public ICollection<MangaGenreSearch> ExcludedGenres { get; set; } = new List<MangaGenreSearch>();

	/// <summary>
	/// Filter by magazine id.
	/// </summary>
	public ICollection<long> MagazineIds { get; set; } = new List<long>();

	/// <summary>
	/// Magazine or serialization platform ids to leave out.
	/// </summary>
	public ICollection<long> ExcludedMagazineIds { get; set; } = new List<long>();

	/// <summary>
	/// Credited author or artist person ids.
	/// </summary>
	public ICollection<long> AuthorIds { get; set; } = new List<long>();

	/// <summary>
	/// Only count <see cref="AuthorIds"/> credited with this role.
	/// </summary>
	public AuthorRole? AuthorRole { get; set; }

	/// <summary>
	/// True for manga written and drawn by one person, false for manga whose story and art are credited to different people.
	/// </summary>
	public bool? SoloAuthor { get; set; }

	/// <summary>
	/// Author person ids to leave out, in any role.
	/// </summary>
	public ICollection<long> ExcludedAuthorIds { get; set; } = new List<long>();

	/// <summary>
	/// Character ids.
	/// </summary>
	public ICollection<long> CharacterIds { get; set; } = new List<long>();

	/// <summary>
	/// Only count <see cref="CharacterIds"/> in this role. The API rejects it without <see cref="CharacterIds"/>.
	/// </summary>
	public CharacterRole? CharacterRole { get; set; }

	/// <summary>
	/// Lowest MyAnimeList id.
	/// </summary>
	public long? MinimumId { get; set; }

	/// <summary>
	/// Highest MyAnimeList id.
	/// </summary>
	public long? MaximumId { get; set; }

	/// <summary>
	/// Only search within these ids, e.g. a user's list. Other filters and sorting still apply.
	/// </summary>
	public ICollection<long> Ids { get; set; } = new List<long>();

	/// <summary>
	/// Ids to leave out, e.g. the ones already on a user's list.
	/// </summary>
	public ICollection<long> ExcludedIds { get; set; } = new List<long>();

	/// <summary>
	/// Exact score. The API rejects it combined with <see cref="MinimumScore"/> or <see cref="MaximumScore"/>.
	/// </summary>
	public double? Score { get; set; }

	/// <summary>
	/// Minimum score results (1-10).
	/// </summary>
	public double? MinimumScore { get; set; }

	/// <summary>
	/// Maximum score results (1-10).
	/// </summary>
	public double? MaximumScore { get; set; }

	/// <summary>
	/// Lowest number of users who scored it.
	/// </summary>
	public int? MinimumScoredBy { get; set; }

	/// <summary>
	/// Highest number of users who scored it.
	/// </summary>
	public int? MaximumScoredBy { get; set; }

	/// <summary>
	/// Lowest number of reviews.
	/// </summary>
	public int? MinimumReviews { get; set; }

	/// <summary>
	/// Highest number of reviews.
	/// </summary>
	public int? MaximumReviews { get; set; }

	/// <summary>
	/// Lowest number of members.
	/// </summary>
	public int? MinimumMembers { get; set; }

	/// <summary>
	/// Highest number of members.
	/// </summary>
	public int? MaximumMembers { get; set; }

	/// <summary>
	/// Lowest number of favorites.
	/// </summary>
	public int? MinimumFavorites { get; set; }

	/// <summary>
	/// Highest number of favorites.
	/// </summary>
	public int? MaximumFavorites { get; set; }

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
	/// Calendar years of the start date. The API rejects it combined with <see cref="MinimumYear"/> or <see cref="MaximumYear"/>.
	/// </summary>
	public ICollection<int> Years { get; set; } = new List<int>();

	/// <summary>
	/// Earliest calendar start year.
	/// </summary>
	public int? MinimumYear { get; set; }

	/// <summary>
	/// Latest calendar start year.
	/// </summary>
	public int? MaximumYear { get; set; }

	/// <summary>
	/// Filter by starting date.
	/// </summary>
	public DateTime? StartDate { get; set; }

	/// <summary>
	/// Filter by ending date.
	/// </summary>
	public DateTime? EndDate { get; set; }

	/// <summary>
	/// Should only search for sfw title. Filter out adult entries. Null uses the client setting (<see cref="Config.TenraiClientConfiguration.Sfw"/>).
	/// </summary>
	public bool? Sfw { get; set; }

	/// <summary>
	/// Also filter out ecchi entries, on top of <see cref="Sfw"/>. Null uses the client setting.
	/// </summary>
	public bool? SfwStrict { get; set; }

	/// <summary>
	/// Unapproved entries: <see cref="InclusionFilter.Include"/> adds them, <see cref="InclusionFilter.Only"/> returns only them.
	/// </summary>
	public InclusionFilter? Unapproved { get; set; }

	/// <summary>
	/// Select order property.
	/// </summary>
	public MangaSearchOrderBy OrderBy { get; set; }

	/// <summary>
	/// Define sort direction for <see cref="OrderBy">OrderBy</see> property.
	/// </summary>
	public SortDirection SortDirection { get; set; }

	/// <summary>
	/// Where entries without a value for <see cref="OrderBy"/> go.
	/// </summary>
	public NullsOrder? Nulls { get; set; }

	/// <summary>
	/// Create query from current parameters for search request.
	/// </summary>
	/// <returns>Query from current parameters for search request</returns>
	public string ConfigToString()
	{
		if (Letter.HasValue)
		{
			Guard.IsLetter(Letter.Value, nameof(Letter));
		}

		var query = new QueryBuilder()
			.Page(Page)
			.Limit(Limit)
			.Add("q", Query)
			.Add("letter", Letter?.ToString())
			.AddList("type", Types)
			.AddList("type_exclude", ExcludedTypes)
			.AddList("status", Statuses)
			.AddList("status_exclude", ExcludedStatuses)
			.AddList("genres", Genres)
			.AddList("genres_exclude", ExcludedGenres)
			.AddList("magazines", MagazineIds)
			.AddList("magazines_exclude", ExcludedMagazineIds)
			.AddList("authors", AuthorIds)
			.Add("author_role", AuthorRole)
			.Add("solo_author", SoloAuthor)
			.AddList("authors_exclude", ExcludedAuthorIds)
			.AddList("characters", CharacterIds)
			.Add("character_role", CharacterRole)
			.Add("min_id", MinimumId)
			.Add("max_id", MaximumId)
			.AddList("ids", Ids)
			.AddList("exclude_ids", ExcludedIds)
			.Add("score", Score)
			.Add("min_score", MinimumScore)
			.Add("max_score", MaximumScore)
			.Add("min_scored_by", MinimumScoredBy)
			.Add("max_scored_by", MaximumScoredBy)
			.Add("min_reviews", MinimumReviews)
			.Add("max_reviews", MaximumReviews)
			.Add("min_members", MinimumMembers)
			.Add("max_members", MaximumMembers)
			.Add("min_favorites", MinimumFavorites)
			.Add("max_favorites", MaximumFavorites)
			.Add("min_chapters", MinimumChapters)
			.Add("max_chapters", MaximumChapters)
			.Add("min_volumes", MinimumVolumes)
			.Add("max_volumes", MaximumVolumes)
			.AddList("year", Years)
			.Add("min_year", MinimumYear)
			.Add("max_year", MaximumYear)
			.Add("start_date", StartDate)
			.Add("end_date", EndDate)
			.Sfw(Sfw, SfwStrict)
			.Add("unapproved", Unapproved);

		if (OrderBy != MangaSearchOrderBy.NoSorting)
		{
			query.Add("order_by", OrderBy).Add("sort", SortDirection).Add("nulls", Nulls);
		}

		return query.ToString();
	}
}
