using Newtonsoft.Json;

namespace JikanDotNet
{
	/// <summary>
	/// Model class for scores from anime review.
	/// </summary>
	public class AnimeReviewScores
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
		/// Score for animation.
		/// </summary>
		[JsonProperty(PropertyName = "animation")]
		public int? Animation { get; set; }

		/// <summary>
		/// Score for sound.
		/// </summary>
		[JsonProperty(PropertyName = "sound")]
		public int? Sound { get; set; }

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
