using Tenrai.Helpers;
using Tenrai.Interfaces;
using System;
using System.Collections.Generic;

namespace Tenrai
{
	/// <summary>
	/// Model class of search configuration for advanced manga search.
	/// </summary>
	public class MangaSearchConfig : ISearchConfig
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
		/// Manga type of searched result.
		/// </summary>
		public MangaType Type { get; set; } = MangaType.EveryType;

		/// <summary>
		/// Minimum score results (1-10).
		/// </summary>
		public double? MinimumScore { get; set; }
		
		/// <summary>
		/// Maximum score results (1-10).
		/// </summary>
		public double? MaximumScore { get; set; }

		/// <summary>
		/// Current status.
		/// </summary>
		public PublishingStatus Status { get; set; }

		/// <summary>
		/// Select order property.
		/// </summary>
		public MangaSearchOrderBy OrderBy { get; set; }

		/// <summary>
		/// Define sort direction for <see cref="OrderBy">OrderBy</see> property.
		/// </summary>
		public SortDirection SortDirection { get; set; }

		/// <summary>
		/// Genres to include.
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
		/// Should only search for sfw title. Filter out adult entries.
		/// </summary>
		public bool Sfw { get; set; } = true;

		/// <summary>
		/// Also filter out ecchi entries, on top of <see cref="Sfw"/>.
		/// </summary>
		public bool SfwStrict { get; set; }
		
		/// <summary>
		/// Filter by starting date.
		/// </summary>
		public DateTime? StartDate { get; set; }
		
		/// <summary>
		/// Filter by ending date.
		/// </summary>
		public DateTime? EndDate { get; set; }
		
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
				.Add("type", Type)
				.Add("min_score", MinimumScore)
				.Add("max_score", MaximumScore)
				.Add("status", Status)
				.AddList("genres", Genres)
				.AddList("genres_exclude", ExcludedGenres);

			if (OrderBy != MangaSearchOrderBy.NoSorting)
			{
				query.Add("order_by", OrderBy).Add("sort", SortDirection);
			}

			return query
				.AddList("magazines", MagazineIds)
				.Add("start_date", StartDate)
				.Add("end_date", EndDate)
				.Sfw(Sfw, SfwStrict)
				.ToString();
		}
	}
}