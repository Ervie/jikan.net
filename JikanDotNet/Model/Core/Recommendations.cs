using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace JikanDotNet
{
	/// <summary>
	/// Recommendations collection model class.
	/// </summary>
	public class Recommendations: BaseJikanRequest
	{
		/// <summary>
		/// A collection of recommendations for anime.
		/// </summary>
		[JsonPropertyName("recommendations")]
		public ICollection<Recommendation> RecommendationCollection { get; set; }
	}
}