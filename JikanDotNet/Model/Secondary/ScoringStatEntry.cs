using System.Text.Json.Serialization;

namespace JikanDotNet
{
	/// <summary>
	/// Model class representing number of votes and percentage share for single score.
	/// </summary>
	public class ScoringStatEntry
	{
		/// <summary>
		/// Percentage share of overall votes poll.
		/// </summary>
		[JsonPropertyName("percentage")]
		public float? Percentage { get; set; }

		/// <summary>
		/// Number of votes casted for score.
		/// </summary>
		[JsonPropertyName("votes")]
		public int? Votes { get; set; }

		/// <summary>
		/// Overriden ToString method.
		/// </summary>
		/// <returns>Number of votes if not null, base method elsewhere.</returns>
		public override string ToString()
		{
			return Votes.ToString() ?? base.ToString();
		}
	}
}