using System;
using Tenrai.Interfaces;
using System.Collections.Generic;
using Tenrai.Helpers;

namespace Tenrai;

/// <summary>
/// Model class of search configuration for advanced anime search.
/// List filters match any of their values unless noted; the API rejects lists over 25 values (1000 for <see cref="Ids"/> and <see cref="ExcludedIds"/>).
/// </summary>
public class AnimeSearchConfig : ISearchConfig, ISfwConfig
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
	/// Anime types to include.
	/// </summary>
	public ICollection<AnimeType> Types { get; set; } = new List<AnimeType>();

	/// <summary>
	/// Anime types to leave out.
	/// </summary>
	public ICollection<AnimeType> ExcludedTypes { get; set; } = new List<AnimeType>();

	/// <summary>
	/// Airing statuses to include.
	/// </summary>
	public ICollection<AiringStatus> Statuses { get; set; } = new List<AiringStatus>();

	/// <summary>
	/// Airing statuses to leave out.
	/// </summary>
	public ICollection<AiringStatus> ExcludedStatuses { get; set; } = new List<AiringStatus>();

	/// <summary>
	/// Age ratings to include.
	/// </summary>
	public ICollection<AnimeAgeRating> Ratings { get; set; } = new List<AnimeAgeRating>();

	/// <summary>
	/// Age ratings to leave out. Entries without a rating are kept.
	/// </summary>
	public ICollection<AnimeAgeRating> ExcludedRatings { get; set; } = new List<AnimeAgeRating>();

	/// <summary>
	/// Source materials to include.
	/// </summary>
	public ICollection<AnimeSource> Sources { get; set; } = new List<AnimeSource>();

	/// <summary>
	/// Source materials to leave out.
	/// </summary>
	public ICollection<AnimeSource> ExcludedSources { get; set; } = new List<AnimeSource>();

	/// <summary>
	/// Whether a main trailer is listed. Playback availability is not checked.
	/// </summary>
	public bool? HasTrailer { get; set; }

	/// <summary>
	/// Streaming providers, e.g. "Crunchyroll" or "Netflix". Capitalization and spacing are ignored.
	/// </summary>
	public ICollection<string> StreamingServices { get; set; } = new List<string>();

	/// <summary>
	/// Recorded MyAnimeList relations: <see cref="InclusionFilter.Include"/> keeps every entry, <see cref="InclusionFilter.Exclude"/> only entries without relations, <see cref="InclusionFilter.Only"/> only entries with them.
	/// </summary>
	public InclusionFilter? HasRelations { get; set; }

	/// <summary>
	/// Genres to include. Entries must have all of them.
	/// </summary>
	public ICollection<AnimeGenreSearch> Genres { get; set; } = new List<AnimeGenreSearch>();

	/// <summary>
	/// Genres to exclude.
	/// </summary>
	public ICollection<AnimeGenreSearch> ExcludedGenres { get; set; } = new List<AnimeGenreSearch>();

	/// <summary>
	/// Filter by producer, studio or licensor id.
	/// </summary>
	public ICollection<long> ProducerIds { get; set; } = new List<long>();

	/// <summary>
	/// Producer, studio or licensor ids to leave out.
	/// </summary>
	public ICollection<long> ExcludedProducerIds { get; set; } = new List<long>();

	/// <summary>
	/// Credited staff person ids.
	/// </summary>
	public ICollection<long> StaffIds { get; set; } = new List<long>();

	/// <summary>
	/// Character ids.
	/// </summary>
	public ICollection<long> CharacterIds { get; set; } = new List<long>();

	/// <summary>
	/// Only count <see cref="CharacterIds"/> in this role. The API rejects it without <see cref="CharacterIds"/>.
	/// </summary>
	public CharacterRole? CharacterRole { get; set; }

	/// <summary>
	/// Voice actor person ids.
	/// </summary>
	public ICollection<long> VoiceActorIds { get; set; } = new List<long>();

	/// <summary>
	/// Voice cast languages, e.g. English for an English dub.
	/// </summary>
	public ICollection<VoiceLanguage> VoiceLanguages { get; set; } = new List<VoiceLanguage>();

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
	/// Lowest number of episodes.
	/// </summary>
	public int? MinimumEpisodes { get; set; }

	/// <summary>
	/// Highest number of episodes.
	/// </summary>
	public int? MaximumEpisodes { get; set; }

	/// <summary>
	/// Calendar years of the start date. The API rejects it combined with <see cref="MinimumYear"/> or <see cref="MaximumYear"/>.
	/// </summary>
	public ICollection<int> Years { get; set; } = new List<int>();

	/// <summary>
	/// MyAnimeList debut seasons.
	/// </summary>
	public ICollection<Season> Seasons { get; set; } = new List<Season>();

	/// <summary>
	/// MyAnimeList debut season years. Can differ from the calendar start year.
	/// </summary>
	public ICollection<int> SeasonYears { get; set; } = new List<int>();

	/// <summary>
	/// Days the anime airs in Japan (JST).
	/// </summary>
	public ICollection<BroadcastDay> BroadcastDays { get; set; } = new List<BroadcastDay>();

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
	/// Only anime added to MyAnimeList on or after this date.
	/// </summary>
	public DateTime? AddedAfter { get; set; }

	/// <summary>
	/// Only anime added to MyAnimeList on or before this date.
	/// </summary>
	public DateTime? AddedBefore { get; set; }

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
	public AnimeSearchOrderBy OrderBy { get; set; }

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
			.AddList("rating", Ratings)
			.AddList("rating_exclude", ExcludedRatings)
			.AddList("source", Sources)
			.AddList("source_exclude", ExcludedSources)
			.Add("has_trailer", HasTrailer)
			.AddList("streaming_service", StreamingServices)
			.Add("has_relations", HasRelations)
			.AddList("genres", Genres)
			.AddList("genres_exclude", ExcludedGenres)
			.AddList("producers", ProducerIds)
			.AddList("producers_exclude", ExcludedProducerIds)
			.AddList("staff", StaffIds)
			.AddList("characters", CharacterIds)
			.Add("character_role", CharacterRole)
			.AddList("voice_actors", VoiceActorIds)
			.AddList("voice_language", VoiceLanguages)
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
			.Add("min_episodes", MinimumEpisodes)
			.Add("max_episodes", MaximumEpisodes)
			.AddList("year", Years)
			.AddList("season", Seasons)
			.AddList("season_year", SeasonYears)
			.AddList("broadcast_day", BroadcastDays)
			.Add("min_year", MinimumYear)
			.Add("max_year", MaximumYear)
			.Add("start_date", StartDate)
			.Add("end_date", EndDate)
			.Add("added_after", AddedAfter)
			.Add("added_before", AddedBefore)
			.Sfw(Sfw, SfwStrict)
			.Add("unapproved", Unapproved);

		if (OrderBy != AnimeSearchOrderBy.NoSorting)
		{
			query.Add("order_by", OrderBy).Add("sort", SortDirection).Add("nulls", Nulls);
		}

		return query.ToString();
	}
}
