using System.Text.Json.Serialization;

namespace JikanDotNet
{
	/// <summary>
	/// Model class representing distribution of score on manga/anime.
	/// </summary>
	public class ScoringStats
	{
		/// <summary>
		/// Statistics for "1" score.
		/// </summary>
		[JsonPropertyName("1")]
		public ScoringStatEntry _1 { get; set; }

		/// <summary>
		/// Statistics for "2" score.
		/// </summary>
		[JsonPropertyName("2")]
		public ScoringStatEntry _2 { get; set; }

		/// <summary>
		/// Statistics for "3" score.
		/// </summary>
		[JsonPropertyName("3")]
		public ScoringStatEntry _3 { get; set; }

		/// <summary>
		/// Statistics for "4" score.
		/// </summary>
		[JsonPropertyName("4")]
		public ScoringStatEntry _4 { get; set; }

		/// <summary>
		/// Statistics for "5" score.
		/// </summary>
		[JsonPropertyName("5")]
		public ScoringStatEntry _5 { get; set; }

		/// <summary>
		/// Statistics for "6" score.
		/// </summary>
		[JsonPropertyName("6")]
		public ScoringStatEntry _6 { get; set; }

		/// <summary>
		/// Statistics for "7" score.
		/// </summary>
		[JsonPropertyName("7")]
		public ScoringStatEntry _7 { get; set; }

		/// <summary>
		/// Statistics for "8" score.
		/// </summary>
		[JsonPropertyName("8")]
		public ScoringStatEntry _8 { get; set; }

		/// <summary>
		/// Statistics for "9" score.
		/// </summary>
		[JsonPropertyName("9")]
		public ScoringStatEntry _9 { get; set; }

		/// <summary>
		/// Statistics for "10" score.
		/// </summary>
		[JsonPropertyName("10")]
		public ScoringStatEntry _10 { get; set; }
	}
}