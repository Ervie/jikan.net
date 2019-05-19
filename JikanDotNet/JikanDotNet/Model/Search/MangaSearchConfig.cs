using JikanDotNet.Extensions;
using JikanDotNet.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JikanDotNet
{
	/// <summary>
	/// Model class of search configuration for advanced manga search.
	/// </summary>
	public class MangaSearchConfig : ISearchConfig
	{
		/// <summary>
		/// Manga type of searched result;
		/// </summary>
		public MangaType Type { get; set; }

		/// <summary>
		/// Minimum score results (1-10).
		/// </summary>
		public int? Score { get; set; }

		/// <summary>
		/// Age rating.
		/// </summary>
		public AgeRating Rating { get; set; }

		/// <summary>
		/// Current status.
		/// </summary>
		public AiringStatus Status { get; set; }

		/// <summary>
		/// Filter start date of results.
		/// </summary>
		public DateTime? StartDate { get; set; }

		/// <summary>
		/// Filter end date of results.
		/// </summary>
		public DateTime? EndDate { get; set; }

		/// <summary>
		/// Select order property.
		/// </summary>
		public MangaSearchSortable OrderBy { get; set; }

		/// <summary>
		/// Define sort direction for <see cref="OrderBy">OrderBy</see> property.
		/// </summary>
		public SortDirection SortDirection { get; set; }

		/// <summary>
		/// Genres to seach/exclude.
		/// </summary>
		public ICollection<GenreSearch> Genres { get; set; } = new List<GenreSearch>();

		/// <summary>
		/// Filter by magazine id.
		/// </summary>
		public long MagazineId { get; set; }

		/// <summary>
		/// If true, search manga of genres included in <see cref="Genres">Genres</see>. If false, exlude genres included from <see cref="Genres">Genres</see> from search result. />
		/// </summary>
		public bool GenreIncluded { get; set; } = false;

		/// <summary>
		/// Create query from current parameters for search request.
		/// </summary>
		/// <returns>Query from current parameters for search request</returns>
		public string ConfigToString()
		{
			StringBuilder builder = new StringBuilder();
			
			if (Type != MangaType.EveryType)
			{
				builder.Append($"&type={Type.GetDescription()}");
			}

			if (Score.HasValue)
			{
				builder.Append($"&score={Score}");
			}

			if (Rating != AgeRating.EveryRating)
			{
				builder.Append($"&rated={Rating.GetDescription()}");
			}

			if (Status != AiringStatus.EveryStatus)
			{
				builder.Append($"&status={Status.GetDescription()}");
			}

			if (StartDate.HasValue)
			{
				builder.Append($"&start_date={StartDate.Value.ToString("yyyy-MM-dd")}");
			}

			if (EndDate.HasValue)
			{
				builder.Append($"&end_date={EndDate.Value.ToString("yyyy-MM-dd")}");
			}

			if (Genres.Count > 0)
			{
				var genresId = Genres.Select(x => x.GetDescription()).ToArray();

				builder.Append($"&genre={string.Join(",", genresId)}");
			}

			if (GenreIncluded)
			{
				builder.Append($"genre_exclude=0$");
			}

			if (OrderBy != MangaSearchSortable.NoSorting)
			{
				builder.Append($"&order_by={OrderBy.GetDescription()}");

				if (SortDirection != SortDirection.Default)
				{
					builder.Append($"&sort={SortDirection.GetDescription()}");
				}
			}

			if (MagazineId > 0)
			{
				builder.Append($"&magazine={MagazineId}");
			}

			return builder.ToString().TrimEnd('&');
		}
	}
}