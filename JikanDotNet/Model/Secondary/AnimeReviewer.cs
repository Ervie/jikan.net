using System.Text.Json.Serialization;

namespace JikanDotNet
{
	/// <summary>
	/// Model class for anime reviewer.
	/// </summary>
	public class AnimeReviewer : Reviewer
	{
		/// <summary>
		/// Number of episodes seen by reviewer.
		/// </summary>
		[JsonPropertyName("episodes_seen")]
		public int? EpisodesSeen { get; set; }

		/// <summary>
		/// Scores from the review.
		/// </summary>
		[JsonPropertyName("scores")]
		public AnimeReviewScores Scores { get; set; }
	}
}