using JikanDotNet.Extensions;
using JikanDotNet.Helpers;
using JikanDotNet.Interfaces;
using System.Text;

namespace JikanDotNet
{
	/// <summary>
	/// Model class of search configuration for advanced manga user list search.
	/// </summary>
	public class UserListMangaSearchConfig : ISearchConfig
	{
		/// <summary>
		/// Query to filter by.
		/// </summary>
		public string Query { get; set; }

		/// <summary>
		/// Order items with respect to a property
		/// </summary>
		public UserListMangaSearchSortable OrderBy { get; set; }

		/// <summary>
		/// Order items with respect to a second property
		/// </summary>
		public UserListMangaSearchSortable OrderBy2 { get; set; }

		/// <summary>
		/// Define sort direction for <see cref="OrderBy">OrderBy</see> property.
		/// </summary>
		public SortDirection SortDirection { get; set; }

		/// <summary>
		/// Index of page (page size = 300).
		/// </summary>
		public int Page { get; set; }

		/// <summary>
		/// Filter manga by this Magazine ID.
		/// </summary>
		public long MagazineId { get; set; }

		/// <summary>
		/// Filter manga with a status.
		/// </summary>
		public UserListMangaPublishingStatus PublishingStatus { get; set; }

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

			if (OrderBy != UserListMangaSearchSortable.NoSorting)
			{
				Guard.IsValidEnum(OrderBy, nameof(OrderBy));
				Guard.IsValidEnum(SortDirection, nameof(SortDirection));
				builder.Append($"&order_by={OrderBy.GetDescription()}");
				builder.Append($"&sort={SortDirection.GetDescription()}");

				if (OrderBy2 != UserListMangaSearchSortable.NoSorting)
				{
					Guard.IsValidEnum(OrderBy2, nameof(OrderBy2));
					builder.Append($"&order_by2={OrderBy2.GetDescription()}");
				}
			}

			if (MagazineId > 0)
			{
				builder.Append($"&magazine={MagazineId}");
			}

			if (PublishingStatus != UserListMangaPublishingStatus.NoFilter)
			{
				Guard.IsValidEnum(PublishingStatus, nameof(PublishingStatus));
				builder.Append($"&publishing_status={PublishingStatus.GetDescription()}");
			}

			// change first ampersand into question mark
			return string.Concat("?", builder.ToString().TrimEnd('&').TrimStart('&'));
		}
	}
}