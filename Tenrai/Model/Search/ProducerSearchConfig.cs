using System.Collections.Generic;
using Tenrai.Helpers;
using Tenrai.Interfaces;

namespace Tenrai;

/// <summary>
/// Filters for the producer listing (studios, producers, licensors).
/// </summary>
public class ProducerSearchConfig : ISearchConfig, ISfwConfig
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
	/// Search query.
	/// </summary>
	public string Query { get; set; }

	/// <summary>
	/// Return entries starting with the given letter.
	/// </summary>
	public char? Letter { get; set; }

	/// <summary>
	/// Companies credited on anime of any of these types.
	/// </summary>
	public ICollection<AnimeType> AnimeTypes { get; set; } = new List<AnimeType>();

	/// <summary>
	/// Companies credited on anime from any of these sources.
	/// </summary>
	public ICollection<AnimeSource> Sources { get; set; } = new List<AnimeSource>();

	/// <summary>
	/// Whether the company has an about text.
	/// </summary>
	public bool? HasAbout { get; set; }

	/// <summary>
	/// Whether the company has an image.
	/// </summary>
	public bool? HasImage { get; set; }

	/// <summary>
	/// Whether the company has a founding date.
	/// </summary>
	public bool? HasEstablished { get; set; }

	/// <summary>
	/// True for companies MyAnimeList lists as dissolved. Some closed companies have no dissolved date there.
	/// </summary>
	public bool? Dissolved { get; set; }

	/// <summary>
	/// Companies credited on anime with all of these genres.
	/// </summary>
	public ICollection<AnimeGenreSearch> Genres { get; set; } = new List<AnimeGenreSearch>();

	/// <summary>
	/// Companies credited on anime with any of these genres are left out.
	/// </summary>
	public ICollection<AnimeGenreSearch> ExcludedGenres { get; set; } = new List<AnimeGenreSearch>();

	/// <summary>
	/// Companies that worked on an anime with any of these companies.
	/// </summary>
	public ICollection<long> WorkedWithIds { get; set; } = new List<long>();

	/// <summary>
	/// Only studios, producers or licensors.
	/// </summary>
	public ICollection<ProducerRole> Roles { get; set; } = new List<ProducerRole>();

	/// <summary>
	/// True for companies credited on at least one anime airing now, false for companies credited on none.
	/// </summary>
	public bool? Airing { get; set; }

	/// <summary>
	/// True for companies credited on at least one anime that has not aired yet, false for companies credited on none.
	/// </summary>
	public bool? Upcoming { get; set; }

	/// <summary>
	/// Lowest number of favorites.
	/// </summary>
	public int? MinimumFavorites { get; set; }

	/// <summary>
	/// Highest number of favorites.
	/// </summary>
	public int? MaximumFavorites { get; set; }

	/// <summary>
	/// Lowest number of credited anime.
	/// </summary>
	public int? MinimumAnime { get; set; }

	/// <summary>
	/// Highest number of credited anime.
	/// </summary>
	public int? MaximumAnime { get; set; }

	/// <summary>
	/// Companies credited on anime from any of these debut seasons.
	/// </summary>
	public ICollection<Season> Seasons { get; set; } = new List<Season>();

	/// <summary>
	/// Companies credited on anime from any of these debut season years.
	/// </summary>
	public ICollection<int> SeasonYears { get; set; } = new List<int>();

	/// <summary>
	/// Companies credited on an anime that started in this year or later. With <see cref="ActiveBefore"/>, the same anime must meet both.
	/// </summary>
	public int? ActiveAfter { get; set; }

	/// <summary>
	/// Companies credited on an anime that started in this year or earlier.
	/// </summary>
	public int? ActiveBefore { get; set; }

	/// <summary>
	/// Companies whose earliest credited anime started in this year or later.
	/// </summary>
	public int? DebutAfter { get; set; }

	/// <summary>
	/// Companies whose latest credited anime started in this year or earlier. Anime not started yet count with their planned year.
	/// </summary>
	public int? LastCreditBefore { get; set; }

	/// <summary>
	/// Companies founded in this year or later.
	/// </summary>
	public int? EstablishedAfter { get; set; }

	/// <summary>
	/// Companies founded in this year or earlier.
	/// </summary>
	public int? EstablishedBefore { get; set; }

	/// <summary>
	/// Filter out adult entries. Null uses the client setting (<see cref="Config.TenraiClientConfiguration.Sfw"/>).
	/// </summary>
	public bool? Sfw { get; set; }

	/// <summary>
	/// Also filter out ecchi entries, on top of <see cref="Sfw"/>. Null uses the client setting.
	/// </summary>
	public bool? SfwStrict { get; set; }

	/// <summary>
	/// Select order property.
	/// </summary>
	public ProducerSearchOrderBy OrderBy { get; set; }

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
		if (Letter.HasValue)
		{
			Guard.IsLetter(Letter.Value, nameof(Letter));
		}

		var query = new QueryBuilder()
			.Page(Page)
			.Limit(Limit)
			.Add("q", Query)
			.Add("letter", Letter?.ToString())
			.AddList("anime_type", AnimeTypes)
			.AddList("source", Sources)
			.Add("has_about", HasAbout)
			.Add("has_image", HasImage)
			.Add("has_established", HasEstablished)
			.Add("dissolved", Dissolved)
			.AddList("genres", Genres)
			.AddList("genres_exclude", ExcludedGenres)
			.AddList("worked_with", WorkedWithIds)
			.AddList("role", Roles)
			.Add("airing", Airing)
			.Add("upcoming", Upcoming)
			.Add("min_favorites", MinimumFavorites)
			.Add("max_favorites", MaximumFavorites)
			.Add("min_anime", MinimumAnime)
			.Add("max_anime", MaximumAnime)
			.AddList("season", Seasons)
			.AddList("season_year", SeasonYears)
			.Add("active_after", ActiveAfter)
			.Add("active_before", ActiveBefore)
			.Add("debut_after", DebutAfter)
			.Add("last_credit_before", LastCreditBefore)
			.Add("established_after", EstablishedAfter)
			.Add("established_before", EstablishedBefore)
			.Sfw(Sfw, SfwStrict);

		if (OrderBy != ProducerSearchOrderBy.NoSorting)
		{
			query.Add("order_by", OrderBy).Add("sort", SortDirection);
		}

		return query.ToString();
	}
}
