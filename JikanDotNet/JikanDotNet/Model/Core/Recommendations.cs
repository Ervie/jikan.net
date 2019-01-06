using Newtonsoft.Json;
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
		[JsonProperty(PropertyName = "recommendations")]
		public ICollection<Recommendation> RecommendationCollection { get; set; }
	}
}