using JikanDotNet.Extensions;
using JikanDotNet.Interfaces;
using System;
using System.Collections.Generic;
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
		public Status Status { get; set; }

		/// <summary>
		/// Filter start date of results.
		/// </summary>
		public DateTime? StartDate { get; set; }

		/// <summary>
		/// Filter end date of results.
		/// </summary>
		public DateTime? EndDate { get; set; }

		/// <summary>
		/// Genres to seach/exclude.
		/// </summary>
		public ICollection<GenreSearch> Genres { get; set; } = new List<GenreSearch>();

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

			builder.Append("?");

			if (Type != MangaType.EveryType)
			{
				builder.Append($"type={Type.GetDescription()}&");
			}

			if (Score.HasValue)
			{
				builder.Append($"score={Score}$");
			}

			if (Rating != AgeRating.EveryRating)
			{
				builder.Append($"rated={Rating.GetDescription()}&");
			}

			if (Status != Status.EveryStatus)
			{
				builder.Append($"status={Status.GetDescription()}&");
			}

			if (StartDate.HasValue)
			{
				builder.Append($"start_date={StartDate.Value.ToString("yyyy-MM-dd")}$");
			}

			if (EndDate.HasValue)
			{
				builder.Append($"end_date={EndDate.Value.ToString("yyyy-MM-dd")}$");
			}

			if (Genres.Count > 0)
			{
				foreach (var genre in Genres)
				{
					builder.Append($"genre[]={genre.GetDescription()}&");
				}
			}

			if (GenreIncluded)
			{
				builder.Append($"genre_exclude=0$");
			}

			return builder.ToString().TrimEnd('&');
		}
	}
}