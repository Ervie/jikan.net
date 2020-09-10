using System.Text.Json.Serialization;

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
		[JsonPropertyName("overall")]
		public int? Overall { get; set; }

		/// <summary>
		/// Score for story.
		/// </summary>
		[JsonPropertyName("story")]
		public int? Story { get; set; }

		/// <summary>
		/// Score for art.
		/// </summary>
		[JsonPropertyName("art")]
		public int? Art { get; set; }
		
		/// <summary>
		/// Score for characters.
		/// </summary>
		[JsonPropertyName("character")]
		public int? Character { get; set; }

		/// <summary>
		/// Score for enjoyment.
		/// </summary>
		[JsonPropertyName("enjoyment")]
		public int? Enjoyment { get; set; }
	}
}
