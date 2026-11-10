using System;
using System.Collections.Generic;
using Tenrai.Helpers;
using Tenrai.Interfaces;

namespace Tenrai;

/// <summary>
/// Filters for the list of manga adapted into anime.
/// </summary>
public class AdaptedMangaSearchConfig : ISearchConfig, ISfwConfig
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
	/// Which adaptations count. Not sent when null (the API treats that as <see cref="AdaptedMangaFilter.All"/>).
	/// </summary>
	public AdaptedMangaFilter? Filter { get; set; }

	/// <summary>
	/// Manga types to include. <see cref="MangaType.Novel"/> and <see cref="MangaType.LightNovel"/> are not accepted here and the API rejects them.
	/// </summary>
	public ICollection<MangaType> Types { get; set; } = new List<MangaType>();

	/// <summary>
	/// Publishing status of the manga.
	/// </summary>
	public PublishingStatus? Status { get; set; }

	/// <summary>
	/// Genres the manga must have.
	/// </summary>
	public ICollection<MangaGenreSearch> Genres { get; set; } = new List<MangaGenreSearch>();

	/// <summary>
	/// Exact score. The API rejects it combined with <see cref="MinimumScore"/> or <see cref="MaximumScore"/>.
	/// </summary>
	public double? Score { get; set; }

	/// <summary>
	/// Lowest score.
	/// </summary>
	public double? MinimumScore { get; set; }

	/// <summary>
	/// Highest score.
	/// </summary>
	public double? MaximumScore { get; set; }

	/// <summary>
	/// Only manga that started publishing on or after this date.
	/// </summary>
	public DateTime? StartDate { get; set; }

	/// <summary>
	/// Only manga that ended publishing on or before this date.
	/// </summary>
	public DateTime? EndDate { get; set; }

	/// <summary>
	/// Select order property.
	/// </summary>
	public AdaptedMangaOrderBy OrderBy { get; set; }

	/// <summary>
	/// Define sort direction for <see cref="OrderBy">OrderBy</see> property.
	/// </summary>
	public SortDirection SortDirection { get; set; }

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
		var query = new QueryBuilder()
			.Page(Page)
			.Limit(Limit)
			.Add("filter", Filter)
			.AddList("type", Types)
			.Add("status", Status)
			.AddList("genres", Genres)
			.Add("score", Score)
			.Add("min_score", MinimumScore)
			.Add("max_score", MaximumScore)
			.Add("start_date", StartDate)
			.Add("end_date", EndDate);

		if (OrderBy != AdaptedMangaOrderBy.NoSorting)
		{
			query.Add("order_by", OrderBy).Add("sort", SortDirection);
		}

		return query.Sfw(Sfw, SfwStrict).ToString();
	}
}
