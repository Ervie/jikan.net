using System.Text.Json.Serialization;

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
		[JsonPropertyName("overall")]
		public int? Overall { get; set; }

		/// <summary>
		/// Score for story.
		/// </summary>
		[JsonPropertyName("story")]
		public int? Story { get; set; }

		/// <summary>
		/// Score for animation.
		/// </summary>
		[JsonPropertyName("animation")]
		public int? Animation { get; set; }

		/// <summary>
		/// Score for sound.
		/// </summary>
		[JsonPropertyName("sound")]
		public int? Sound { get; set; }

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
