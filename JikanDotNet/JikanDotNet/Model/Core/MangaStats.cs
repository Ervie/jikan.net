using Newtonsoft.Json;

namespace JikanDotNet
{
	/// <summary>
	/// Manga related statistics model class.
	/// </summary>
	public class MangaStats
	{
		/// <summary>
		/// Number of users who labeled manga status as "reading"
		/// </summary>
		[JsonProperty(PropertyName = "reading")]
		public int? Reading { get; set; }

		/// <summary>
		/// Number of users who labeled manga status as "completed"
		/// </summary>
		[JsonProperty(PropertyName = "completed")]
		public int? Completed { get; set; }

		/// <summary>
		/// Number of users who labeled manga status as "on hold"
		/// </summary>
		[JsonProperty(PropertyName = "on_hold")]
		public int? OnHold { get; set; }

		/// <summary>
		/// Number of users who labeled manga status as "dropped"
		/// </summary>
		[JsonProperty(PropertyName = "dropped")]
		public int? Dropped { get; set; }

		/// <summary>
		/// Number of users who labeled manga status as "plan to read"
		/// </summary>
		[JsonProperty(PropertyName = "plan_to_read")]
		public int? PlanToRead { get; set; }

		/// <summary>
		/// Number of users who added manga to their lists.
		/// </summary>
		[JsonProperty(PropertyName = "scores")]
		public ScoringStats ScoreStats { get; set; }
	}
}