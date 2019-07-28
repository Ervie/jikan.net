using JikanDotNet.Extensions;
using JikanDotNet.Interfaces;
using System;
using System.Text;

namespace JikanDotNet
{
	/// <summary>
	/// Model class of search configuration for advanced anime user list search.
	/// </summary>
	public class UserListAnimeSearchConfig : ISearchConfig
	{
		/// <summary>
		/// Order items with respect to a property
		/// </summary>
		public UserListAnimeSearchSortable OrderBy { get; set; }

		/// <summary>
		/// Order items with respect to a second property
		/// </summary>
		public UserListAnimeSearchSortable OrderBy2 { get; set; }

		/// <summary>
		/// Define sort direction for <see cref="OrderBy">OrderBy</see> property.
		/// </summary>
		public SortDirection SortDirection { get; set; }

		/// <summary>
		/// Filter Anime by this Producer ID.
		/// </summary>
		public long ProducerId { get; set; }

		/// <summary>
		/// Filter anime from a year.
		/// </summary>
		public int Year { get; set; }

		/// <summary>
		/// Filter anime from a season (require year).
		/// </summary>
		public Seasons Season { get; set; }

		/// <summary>
		/// Filter Anime with a status.
		/// </summary>
		public UserListAnimeAiringStatus AiringStatus { get; set; }

		/// <summary>
		/// Create query from current parameters for search request.
		/// </summary>
		/// <returns>Query from current parameters for search request</returns>
		public string ConfigToString()
		{
			StringBuilder builder = new StringBuilder();

			if (OrderBy != UserListAnimeSearchSortable.NoSorting)
			{
				builder.Append($"&order_by={OrderBy.GetDescription()}");
				builder.Append($"&sort={SortDirection.GetDescription()}");

				if (OrderBy2 != UserListAnimeSearchSortable.NoSorting)
				{
					builder.Append($"&order_by2={OrderBy2.GetDescription()}");
				}

			}

			if (ProducerId > 0)
			{
				builder.Append($"&producer={ProducerId}");
			}

			if (Year > 0)
			{
				builder.Append($"&year={Year}");
				builder.Append($"&season={Season.GetDescription()}");
			}

			if (AiringStatus != UserListAnimeAiringStatus.NoFilter)
			{
				builder.Append($"&airing_status={AiringStatus.GetDescription()}");
			}

			return builder.ToString().TrimEnd('&');
		}
	}
}
