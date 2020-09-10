using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace JikanDotNet
{
	/// <summary>
	/// Model class for anime review collection.
	/// </summary>
	public class AnimeReviews: BaseJikanRequest
	{
		/// <summary>
		/// Collection of anime reviews.
		/// </summary>
		[JsonPropertyName("reviews")]
		public ICollection<AnimeReview> Reviews { get; set; }
	}
}