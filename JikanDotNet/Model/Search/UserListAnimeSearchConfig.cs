using JikanDotNet.Extensions;
using JikanDotNet.Interfaces;
using System.Text;
using JikanDotNet.Helpers;

namespace JikanDotNet
{
	/// <summary>
	/// Model class of search configuration for advanced anime user list search.
	/// </summary>
	public class UserListAnimeSearchConfig : ISearchConfig
	{
		/// <summary>
		/// Query to filter by.
		/// </summary>
		public string Query { get; set; }

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
		/// Index of page (page size = 300).
		/// </summary>
		public int Page { get; set; }

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

			if (!string.IsNullOrWhiteSpace(Query))
			{
				builder.Append($"q={Query}");
			}

			if (Page > 0)
			{
				builder.Append($"&page={Page}");
			}

			if (OrderBy != UserListAnimeSearchSortable.NoSorting)
			{
				Guard.IsValidEnum(OrderBy, nameof(OrderBy));
				Guard.IsValidEnum(SortDirection, nameof(SortDirection));
				builder.Append($"&order_by={OrderBy.GetDescription()}");
				builder.Append($"&sort={SortDirection.GetDescription()}");

				if (OrderBy2 != UserListAnimeSearchSortable.NoSorting)
				{
					Guard.IsValidEnum(OrderBy2, nameof(OrderBy2));
					builder.Append($"&order_by2={OrderBy2.GetDescription()}");
				}
			}

			if (ProducerId > 0)
			{
				builder.Append($"&producer={ProducerId}");
			}

			if (Year > 0)
			{
				Guard.IsValidEnum(Season, nameof(Season));
				builder.Append($"&year={Year}");
				builder.Append($"&season={Season.GetDescription()}");
			}

			if (AiringStatus != UserListAnimeAiringStatus.NoFilter)
			{
				Guard.IsValidEnum(AiringStatus, nameof(AiringStatus));
				builder.Append($"&airing_status={AiringStatus.GetDescription()}");
			}

			// change first ampersand into question mark
			return string.Concat("?", builder.ToString().TrimEnd('&').TrimStart('&'));
		}
	}
}