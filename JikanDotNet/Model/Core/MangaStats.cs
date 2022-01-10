using System.Text.Json.Serialization;

namespace JikanDotNet
{
	/// <summary>
	/// Manga related statistics model class.
	/// </summary>
	public class MangaStats: BaseJikanRequest
	{
		/// <summary>
		/// Number of users who labeled manga status as "reading"
		/// </summary>
		[JsonPropertyName("reading")]
		public int? Reading { get; set; }

		/// <summary>
		/// Number of users who labeled manga status as "completed"
		/// </summary>
		[JsonPropertyName("completed")]
		public int? Completed { get; set; }

		/// <summary>
		/// Number of users who labeled manga status as "on hold"
		/// </summary>
		[JsonPropertyName("on_hold")]
		public int? OnHold { get; set; }

		/// <summary>
		/// Number of users who labeled manga status as "dropped"
		/// </summary>
		[JsonPropertyName("dropped")]
		public int? Dropped { get; set; }

		/// <summary>
		/// Number of users who labeled manga status as "plan to read"
		/// </summary>
		[JsonPropertyName("plan_to_read")]
		public int? PlanToRead { get; set; }

		/// <summary>
		/// Number of users who added manga to their lists.
		/// </summary>
		[JsonPropertyName("scores")]
		public ScoringStatistics ScoreStats { get; set; }
	}
}