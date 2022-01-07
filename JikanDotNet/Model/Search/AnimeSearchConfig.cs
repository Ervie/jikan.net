using JikanDotNet.Extensions;
using JikanDotNet.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JikanDotNet.Helpers;

namespace JikanDotNet
{
	/// <summary>
	/// Model class of search configuration for advanced anime search.
	/// </summary>
	public class AnimeSearchConfig : ISearchConfig
	{
		/// <summary>
		/// Anime type of searched result;
		/// </summary>
		public AnimeType Type { get; set; } = AnimeType.EveryType;

		/// <summary>
		/// Minimum score results (1-10).
		/// </summary>
		public int? Score { get; set; }

		/// <summary>
		/// Age rating.
		/// </summary>
		public AnimeAgeRating Rating { get; set; } = AnimeAgeRating.EveryRating;

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
		public AnimeSearchSortable OrderBy { get; set; }

		/// <summary>
		/// Define sort direction for <see cref="OrderBy">OrderBy</see> property.
		/// </summary>
		public SortDirection SortDirection{ get; set; }

		/// <summary>
		/// Genres to search/exclude.
		/// </summary>
		public ICollection<AnimeGenreSearch> Genres { get; set; } = new List<AnimeGenreSearch>();

		/// <summary>
		/// Filter by producer id.
		/// </summary>
		public long ProducerId { get; set; }

		/// <summary>
		/// If true, search anime of genres included in <see cref="Genres">Genres</see>. If false, exclude genres included from <see cref="Genres">Genres</see> from search result. />
		/// </summary>
		public bool GenreIncluded { get; set; } = true;

		/// <summary>
		/// Create query from current parameters for search request.
		/// </summary>
		/// <returns>Query from current parameters for search request</returns>
		public string ConfigToString()
		{
			StringBuilder builder = new();


			if (Type != AnimeType.EveryType)
			{
				Guard.IsValidEnum(Type, nameof(Type));
				builder.Append($"&type={Type.GetDescription()}");
			}

			if (Score.HasValue)
			{
				builder.Append($"&score={Score}");
			}

			if (Rating != AnimeAgeRating.EveryRating)
			{
				Guard.IsValidEnum(Rating, nameof(Rating));
				builder.Append($"&rated={Rating.GetDescription()}");
			}

			if (Status != AiringStatus.EveryStatus)
			{
				Guard.IsValidEnum(Status, nameof(Status));
				builder.Append($"&status={Status.GetDescription()}");
			}

			if (StartDate.HasValue)
			{
				builder.Append($"&start_date={StartDate.Value:yyyy-MM-dd}");
			}

			if (EndDate.HasValue)
			{
				builder.Append($"&end_date={EndDate.Value:yyyy-MM-dd}");
			}

			if (Genres.Count > 0 )
			{
				var genresId = Genres.Select(genreSearch =>
				{
					Guard.IsValidEnum(genreSearch, nameof(genreSearch));
					return genreSearch.GetDescription();
				}).ToArray();

				builder.Append($"&genre={string.Join(",", genresId)}");
			}

			if (!GenreIncluded)
			{
				builder.Append($"&genre_exclude=0");
			}

			if (OrderBy != AnimeSearchSortable.NoSorting)
			{
				Guard.IsValidEnum(OrderBy, nameof(OrderBy));
				Guard.IsValidEnum(SortDirection, nameof(SortDirection));
				builder.Append($"&order_by={OrderBy.GetDescription()}");
				builder.Append($"&sort={SortDirection.GetDescription()}");
			}

			if (ProducerId > 0)
			{
				builder.Append($"&producer={ProducerId}");
			}

			return builder.ToString().Trim('&');
		}
	}
}