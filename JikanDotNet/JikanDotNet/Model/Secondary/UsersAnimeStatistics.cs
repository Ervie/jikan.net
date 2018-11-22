using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace JikanDotNet
{
	/// <summary>
	/// User's anime statistics model class.
	/// </summary>
	public class UserAnimeStatistics
	{
		/// <summary>
		/// Number of days user has been watching anime.
		/// </summary>
		[JsonProperty(PropertyName = "days_watched")]
		public decimal? DaysWatched { get; set; }

		/// <summary>
		/// User's mean score for anime.
		/// </summary>
		[JsonProperty(PropertyName = "mean_score")]
		public decimal? MeanScore { get; set; }

		/// <summary>
		/// User's amount of anime currently watching.
		/// </summary>
		[JsonProperty(PropertyName = "watching")]
		public int? Watching { get; set; }

		/// <summary>
		/// User's amount of completed anime.
		/// </summary>
		[JsonProperty(PropertyName = "completed")]
		public int? Completed { get; set; }

		/// <summary>
		/// User's amount of anime on hold.
		/// </summary>
		[JsonProperty(PropertyName = "on_hold")]
		public int? OnHold { get; set; }

		/// <summary>
		/// User's amount of dropped anime.
		/// </summary>
		[JsonProperty(PropertyName = "dropped")]
		public int? Dropped { get; set; }

		/// <summary>
		/// User's amount of plan to watch anime.
		/// </summary>
		[JsonProperty(PropertyName = "plan_to_watch")]
		public int? PlanToWatch { get; set; }

		/// <summary>
		/// User's total amount of anime.
		/// </summary>
		[JsonProperty(PropertyName = "total_entries")]
		public int? TotalEntries { get; set; }

		/// <summary>
		/// Total times user rewatched anime.
		/// </summary>
		[JsonProperty(PropertyName = "rewatched")]
		public int? Rewatched { get; set; }

		/// <summary>
		/// User's total amount of watched episodes.
		/// </summary>
		[JsonProperty(PropertyName = "episodes_watched")]
		public int? EpisodesWatched { get; set; }
	}
}
