using System.Text.Json.Serialization;

namespace JikanDotNet
{
	/// <summary>
	/// Model class for single recommendation.
	/// </summary>
	public class Recommendation
	{
		/// <summary>
		/// Url to recommendation.
		/// </summary>
		[JsonPropertyName("url")]
		public string Url { get; set; }

		/// <summary>
		/// Number of users who have recommended this entry.
		/// </summary>
		[JsonPropertyName("votes")]
		public int Votes { get; set; }

		/// <summary>
		/// Details about recommendation.
		/// </summary>
		[JsonPropertyName("entry")]
		public RecommendationEntry Entry { get; set; }
	}
}