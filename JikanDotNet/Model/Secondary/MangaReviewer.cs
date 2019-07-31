using Newtonsoft.Json;

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
		[JsonProperty(PropertyName = "chapters_read")]
		public int? ChaptersRead { get; set; }

		/// <summary>
		/// Scores from the review.
		/// </summary>
		[JsonProperty(PropertyName = "scores")]
		public MangaReviewScores Scores { get; set; }
	}
}