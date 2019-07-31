using Newtonsoft.Json;

namespace JikanDotNet
{
	/// <summary>
	/// Model class for scores from manga review.
	/// </summary>
	public class MangaReviewScores
	{
		/// <summary>
		/// Overall score.
		/// </summary>
		[JsonProperty(PropertyName = "overall")]
		public int? Overall { get; set; }

		/// <summary>
		/// Score for story.
		/// </summary>
		[JsonProperty(PropertyName = "story")]
		public int? Story { get; set; }

		/// <summary>
		/// Score for art.
		/// </summary>
		[JsonProperty(PropertyName = "art")]
		public int? Art { get; set; }
		
		/// <summary>
		/// Score for characters.
		/// </summary>
		[JsonProperty(PropertyName = "character")]
		public int? Character { get; set; }

		/// <summary>
		/// Score for enjoyment.
		/// </summary>
		[JsonProperty(PropertyName = "enjoyment")]
		public int? Enjoyment { get; set; }
	}
}
