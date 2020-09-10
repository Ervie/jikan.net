using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace JikanDotNet
{
	/// <summary>
	/// Model class for manga review collection.
	/// </summary>
	public class MangaReviews: BaseJikanRequest
	{
		/// <summary>
		/// Collection of manga reviews.
		/// </summary>
		[JsonPropertyName("reviews")]
		public ICollection<MangaReview> Reviews { get; set; }
	}
}