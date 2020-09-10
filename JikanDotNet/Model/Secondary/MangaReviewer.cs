using System.Text.Json.Serialization;

namespace JikanDotNet
{
	/// <summary>
	/// Model class for manga reviewer.
	/// </summary>
	public class MangaReviewer : Reviewer
	{
		/// <summary>
		/// Number of chapters read by reviewer.
		/// </summary>
		[JsonPropertyName("chapters_read")]
		public int? ChaptersRead { get; set; }

		/// <summary>
		/// Scores from the review.
		/// </summary>
		[JsonPropertyName("scores")]
		public MangaReviewScores Scores { get; set; }
	}
}