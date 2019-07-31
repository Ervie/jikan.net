using Newtonsoft.Json;

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
		[JsonProperty(PropertyName = "reviewer")]
		public MangaReviewer Reviewer { get; set; }
	}
}
