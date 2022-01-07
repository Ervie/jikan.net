using System.Text.Json.Serialization;

namespace JikanDotNet
{
	/// <summary>
	/// Base wrapping class for response with paginated data
	/// </summary>
	public class PaginatedJikanResponse<TResponse> : BaseJikanResponse<TResponse>
	{
		/// <summary>
		/// Last visible page
		/// </summary>
		[JsonPropertyName("last_visible_page")]
		public int LastVisiblePage { get; set; }

		/// <summary>
		/// Has next page
		/// </summary>
		[JsonPropertyName("has_nextPage")]
		public bool HasNextPage { get; set; }

		/// <summary>
		/// Parametereless constructor, required for serialization
		/// </summary>
		public PaginatedJikanResponse() { }
	}
}