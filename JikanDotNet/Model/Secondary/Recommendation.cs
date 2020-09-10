using System.Text.Json.Serialization;

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
		[JsonPropertyName("mal_id")]
		public long MalId { get; set; }

		/// <summary>
		/// Title of recommended anime/manga.
		/// </summary>
		[JsonPropertyName("title")]
		public string Title { get; set; }

		/// <summary>
		/// Url to page of recommended anime/manga.
		/// </summary>
		[JsonPropertyName("url")]
		public string Url { get; set; }

		/// <summary>
		/// Recommended anime/manga image URL.
		/// </summary>
		[JsonPropertyName("image_url")]
		public string ImageURL { get; set; }

		/// <summary>
		/// Url to recommendation.
		/// </summary>
		[JsonPropertyName("recommendation_url")]
		public string RecommendationUrl { get; set; }

		/// <summary>
		/// Number of recommendations.
		/// </summary>
		[JsonPropertyName("recommendation_count")]
		public int? RecommendationCount { get; set; }
	}
}