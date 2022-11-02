using System.Text.Json.Serialization;

namespace JikanDotNet
{
	/// <summary>
	/// Pagination model
	/// </summary>
	public class Pagination
	{
		/// <summary>
		/// Last visible page
		/// </summary>
		[JsonPropertyName("last_visible_page")]
		public int LastVisiblePage { get; set; }

		/// <summary>
		/// Has next page
		/// </summary>
		[JsonPropertyName("has_next_page")]
		public bool HasNextPage { get; set; }
		
		/// <summary>
		/// Current page
		/// </summary>
		[JsonPropertyName("current_page")]
		public int? CurrentPage { get; set; }
    
		/// <summary>
		/// Summary about current query
		/// </summary>
		[JsonPropertyName("items")]
		public PaginationSummary Items { get; set; }
	}
}