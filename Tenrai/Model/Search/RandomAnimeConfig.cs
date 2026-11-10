using System.Collections.Generic;
using Tenrai.Helpers;
using Tenrai.Interfaces;

namespace Tenrai;

/// <summary>
/// Filters for a random anime.
/// </summary>
public class RandomAnimeConfig : ISearchConfig, ISfwConfig
{
	/// <summary>
	/// Anime types to pick from.
	/// </summary>
	public ICollection<AnimeType> Types { get; set; } = new List<AnimeType>();

	/// <summary>
	/// Airing status.
	/// </summary>
	public AiringStatus? Status { get; set; }

	/// <summary>
	/// Age ratings to pick from.
	/// </summary>
	public ICollection<AnimeAgeRating> Ratings { get; set; } = new List<AnimeAgeRating>();

	/// <summary>
	/// Source materials to pick from.
	/// </summary>
	public ICollection<AnimeSource> Sources { get; set; } = new List<AnimeSource>();

	/// <summary>
	/// Whether a main trailer is listed.
	/// </summary>
	public bool? HasTrailer { get; set; }

	/// <summary>
	/// Genres it must have, all of them.
	/// </summary>
	public ICollection<AnimeGenreSearch> Genres { get; set; } = new List<AnimeGenreSearch>();

	/// <summary>
	/// Genres to exclude.
	/// </summary>
	public ICollection<AnimeGenreSearch> ExcludedGenres { get; set; } = new List<AnimeGenreSearch>();

	/// <summary>
	/// Producer, studio or licensor ids.
	/// </summary>
	public ICollection<long> ProducerIds { get; set; } = new List<long>();

	/// <summary>
	/// Ids never to return, e.g. ones already shown.
	/// </summary>
	public ICollection<long> ExcludedIds { get; set; } = new List<long>();

	/// <summary>
	/// Lowest score.
	/// </summary>
	public double? MinimumScore { get; set; }

	/// <summary>
	/// Highest score.
	/// </summary>
	public double? MaximumScore { get; set; }

	/// <summary>
	/// Calendar start year. The API rejects it combined with <see cref="MinimumYear"/> or <see cref="MaximumYear"/>.
	/// </summary>
	public int? Year { get; set; }

	/// <summary>
	/// Earliest calendar start year.
	/// </summary>
	public int? MinimumYear { get; set; }

	/// <summary>
	/// Latest calendar start year.
	/// </summary>
	public int? MaximumYear { get; set; }

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
			.AddList("type", Types)
			.Add("status", Status)
			.AddList("rating", Ratings)
			.AddList("source", Sources)
			.Add("has_trailer", HasTrailer)
			.AddList("genres", Genres)
			.AddList("genres_exclude", ExcludedGenres)
			.AddList("producers", ProducerIds)
			.AddList("exclude_ids", ExcludedIds)
			.Add("min_score", MinimumScore)
			.Add("max_score", MaximumScore)
			.Add("year", Year)
			.Add("min_year", MinimumYear)
			.Add("max_year", MaximumYear)
			.Sfw(Sfw, SfwStrict)
			.ToString();
	}
}
