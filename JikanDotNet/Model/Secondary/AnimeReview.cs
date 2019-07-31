using Newtonsoft.Json;

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
		[JsonProperty(PropertyName = "reviewer")]
		public AnimeReviewer Reviewer { get; set; }
	}
}
