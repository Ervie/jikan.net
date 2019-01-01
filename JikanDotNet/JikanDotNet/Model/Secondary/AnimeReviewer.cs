using Newtonsoft.Json;

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
		[JsonProperty(PropertyName = "episodes_seen")]
		public int? EpisodesSeen { get; set; }

		/// <summary>
		/// Scores from the review.
		/// </summary>
		[JsonProperty(PropertyName = "scores")]
		public AnimeReviewScores Scores { get; set; }
	}
}