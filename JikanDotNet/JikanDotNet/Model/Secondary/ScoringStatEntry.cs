using Newtonsoft.Json;

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
		[JsonProperty(PropertyName = "percentage")]
		public float? Percentage { get; set; }

		/// <summary>
		/// Number of votes casted for score.
		/// </summary>
		[JsonProperty(PropertyName = "votes")]
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