using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace JikanDotNet
{
	/// <summary>
	/// Manga related statistics model class.
	/// </summary>
	public class MangaStatistics
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
		/// Total count of users who added anime to their lists.
		/// </summary>
		[JsonPropertyName("total")]
		public int? Total { get; set; }

		/// <summary>
		/// Number of users who added anime to their lists.
		/// </summary>
		[JsonPropertyName("scores")]
		public ICollection<ScoringStatistics> ScoreStats { get; set; }
	}
}