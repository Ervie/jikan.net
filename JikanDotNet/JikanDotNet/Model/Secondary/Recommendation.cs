using Newtonsoft.Json;

namespace JikanDotNet
{
	/// <summary>
	/// Model class for single recommendation.
	/// </summary>
	public class Recommendation
	{
		/// <summary>
		/// Mal Id of recommended anime/manga.
		/// </summary>
		[JsonProperty(PropertyName = "mal_id")]
		public long MalId { get; set; }

		/// <summary>
		/// Title of recommended anime/manga.
		/// </summary>
		[JsonProperty(PropertyName = "title")]
		public string Title { get; set; }

		/// <summary>
		/// Url to page of recommended anime/manga.
		/// </summary>
		[JsonProperty(PropertyName = "url")]
		public string Url { get; set; }

		/// <summary>
		/// Recommended anime/manga image URL.
		/// </summary>
		[JsonProperty(PropertyName = "image_url")]
		public string ImageURL { get; set; }

		/// <summary>
		/// Url to recommendation.
		/// </summary>
		[JsonProperty(PropertyName = "recommendation_url")]
		public string RecommendationUrl { get; set; }

		/// <summary>
		/// Number of recommendations.
		/// </summary>
		[JsonProperty(PropertyName = "recommendation_count")]
		public int? RecommendationCount { get; set; }
	}
}