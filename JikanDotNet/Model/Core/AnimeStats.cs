using Newtonsoft.Json;

namespace JikanDotNet
{
	/// <summary>
	/// Anime related statistics model class.
	/// </summary>
	public class AnimeStats: BaseJikanRequest
	{
		/// <summary>
		/// Number of users who labeled anime status as "watching"
		/// </summary>
		[JsonProperty(PropertyName = "watching")]
		public int? Watching { get; set; }

		/// <summary>
		/// Number of users who labeled anime status as "completed"
		/// </summary>
		[JsonProperty(PropertyName = "completed")]
		public int? Completed { get; set; }

		/// <summary>
		/// Number of users who labeled anime status as "on hold"
		/// </summary>
		[JsonProperty(PropertyName = "on_hold")]
		public int? OnHold { get; set; }

		/// <summary>
		/// Number of users who labeled anime status as "dropped"
		/// </summary>
		[JsonProperty(PropertyName = "dropped")]
		public int? Dropped { get; set; }

		/// <summary>
		/// Number of users who labeled anime status as "plan to watch"
		/// </summary>
		[JsonProperty(PropertyName = "plan_to_watch")]
		public int? PlanToWatch { get; set; }

		/// <summary>
		/// Number of users who added anime to their lists.
		/// </summary>
		[JsonProperty(PropertyName = "scores")]
		public ScoringStats ScoreStats { get; set; }
	}
}