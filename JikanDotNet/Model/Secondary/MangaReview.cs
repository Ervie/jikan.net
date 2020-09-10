using System.Text.Json.Serialization;

namespace JikanDotNet
{
	/// <summary>
	/// Model class for manga review.
	/// </summary>
	public class MangaReview : Review
	{
		/// <summary>
		/// Reviewer.
		/// </summary>
		[JsonPropertyName("reviewer")]
		public MangaReviewer Reviewer { get; set; }
	}
}
