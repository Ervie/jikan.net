using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;

namespace JikanDotNet
{
	/// <summary>
	/// User's manga statistics model class.
	/// </summary>
	public class UserMangaStatistics
	{
		/// <summary>
		/// Number of days user has been reading manga.
		/// </summary>
		[JsonPropertyName("days_read")]
		public decimal? DaysRead { get; set; }

		/// <summary>
		/// User's mean score for manga.
		/// </summary>
		[JsonPropertyName("mean_score")]
		public decimal? MeanScore { get; set; }

		/// <summary>
		/// User's amount of manga currently reading.
		/// </summary>
		[JsonPropertyName("reading")]
		public int? Reading { get; set; }

		/// <summary>
		/// User's amount of completed manga.
		/// </summary>
		[JsonPropertyName("completed")]
		public int? Completed { get; set; }

		/// <summary>
		/// User's amount of manga on hold.
		/// </summary>
		[JsonPropertyName("on_hold")]
		public int? OnHold { get; set; }

		/// <summary>
		/// User's amount of dropped manga.
		/// </summary>
		[JsonPropertyName("dropped")]
		public int? Dropped { get; set; }

		/// <summary>
		/// User's amount of plan to read manga.
		/// </summary>
		[JsonPropertyName("plan_to_read")]
		public int? PlanToRead { get; set; }

		/// <summary>
		/// User's total amount of manga.
		/// </summary>
		[JsonPropertyName("total_entries")]
		public int? TotalEntries { get; set; }

		/// <summary>
		/// Total times user reread manga.
		/// </summary>
		[JsonPropertyName("reread")]
		public int? Reread { get; set; }

		/// <summary>
		/// User's total amount of read chapters.
		/// </summary>
		[JsonPropertyName("chapters_read")]
		public int? ChaptersRead { get; set; }

		/// <summary>
		/// User's total amount of read volumes.
		/// </summary>
		[JsonPropertyName("volumes_read")]
		public int? VolumesRead { get; set; }
	}
}
