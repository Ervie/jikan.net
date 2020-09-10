using System.Text.Json.Serialization;
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
		[JsonPropertyName("days_watched")]
		public decimal? DaysWatched { get; set; }

		/// <summary>
		/// User's mean score for anime.
		/// </summary>
		[JsonPropertyName("mean_score")]
		public decimal? MeanScore { get; set; }

		/// <summary>
		/// User's amount of anime currently watching.
		/// </summary>
		[JsonPropertyName("watching")]
		public int? Watching { get; set; }

		/// <summary>
		/// User's amount of completed anime.
		/// </summary>
		[JsonPropertyName("completed")]
		public int? Completed { get; set; }

		/// <summary>
		/// User's amount of anime on hold.
		/// </summary>
		[JsonPropertyName("on_hold")]
		public int? OnHold { get; set; }

		/// <summary>
		/// User's amount of dropped anime.
		/// </summary>
		[JsonPropertyName("dropped")]
		public int? Dropped { get; set; }

		/// <summary>
		/// User's amount of plan to watch anime.
		/// </summary>
		[JsonPropertyName("plan_to_watch")]
		public int? PlanToWatch { get; set; }

		/// <summary>
		/// User's total amount of anime.
		/// </summary>
		[JsonPropertyName("total_entries")]
		public int? TotalEntries { get; set; }

		/// <summary>
		/// Total times user rewatched anime.
		/// </summary>
		[JsonPropertyName("rewatched")]
		public int? Rewatched { get; set; }

		/// <summary>
		/// User's total amount of watched episodes.
		/// </summary>
		[JsonPropertyName("episodes_watched")]
		public int? EpisodesWatched { get; set; }
	}
}
