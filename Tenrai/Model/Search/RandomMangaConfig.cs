using System.Collections.Generic;
using Tenrai.Helpers;
using Tenrai.Interfaces;

namespace Tenrai;

/// <summary>
/// Filters for a random manga.
/// </summary>
public class RandomMangaConfig : ISearchConfig, ISfwConfig
{
	/// <summary>
	/// Manga types to pick from.
	/// </summary>
	public ICollection<MangaType> Types { get; set; } = new List<MangaType>();

	/// <summary>
	/// Publishing status.
	/// </summary>
	public PublishingStatus? Status { get; set; }

	/// <summary>
	/// Genres it must have, all of them.
	/// </summary>
	public ICollection<MangaGenreSearch> Genres { get; set; } = new List<MangaGenreSearch>();

	/// <summary>
	/// Genres to exclude.
	/// </summary>
	public ICollection<MangaGenreSearch> ExcludedGenres { get; set; } = new List<MangaGenreSearch>();

	/// <summary>
	/// Author person ids.
	/// </summary>
	public ICollection<long> AuthorIds { get; set; } = new List<long>();

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
			.AddList("genres", Genres)
			.AddList("genres_exclude", ExcludedGenres)
			.AddList("authors", AuthorIds)
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
