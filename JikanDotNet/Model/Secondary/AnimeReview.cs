using System.Text.Json.Serialization;

namespace JikanDotNet
{
	/// <summary>
	/// Model class for anime review.
	/// </summary>
	public class AnimeReview : Review
	{
		/// <summary>
		/// Scores from the review.
		/// </summary>
		[JsonPropertyName("scores")]
		public AnimeReviewScores Scores { get; set; }

		/// <summary>
		/// Number of episodes watched by the reviewer.
		/// </summary>
		[JsonPropertyName("episodes_watched")]
		public int? EpisodesWatched { get; set; }
	}
}
