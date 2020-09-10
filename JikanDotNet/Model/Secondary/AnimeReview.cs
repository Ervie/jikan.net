using System.Text.Json.Serialization;

namespace JikanDotNet
{
	/// <summary>
	/// Model class for anime review.
	/// </summary>
	public class AnimeReview : Review
	{
		/// <summary>
		/// Reviewer.
		/// </summary>
		[JsonPropertyName("reviewer")]
		public AnimeReviewer Reviewer { get; set; }
	}
}
