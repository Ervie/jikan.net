using System.Text.Json.Serialization;

namespace JikanDotNet
{
	/// <summary>
	/// Model class representing number of votes and percentage share for single score.
	/// </summary>
	public class ScoringStatistics
	{
		/// <summary>
		/// Score value (1-10).
		/// </summary>
		[JsonPropertyName("score")]
		public int Score { get; set; }

		/// <summary>
		/// Percentage share of overall votes poll.
		/// </summary>
		[JsonPropertyName("percentage")]
		public double? Percentage { get; set; }

		/// <summary>
		/// Number of votes casted for score.
		/// </summary>
		[JsonPropertyName("votes")]
		public int? Votes { get; set; }

		/// <summary>
		/// Overriden ToString method.
		/// </summary>
		/// <returns>Score value if not null, base method elsewhere.</returns>
		public override string ToString()
		{
			return Score.ToString() ?? base.ToString();
		}
	}
}