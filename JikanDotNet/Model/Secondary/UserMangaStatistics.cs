using Newtonsoft.Json;
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
		[JsonProperty(PropertyName = "days_read")]
		public decimal? DaysRead { get; set; }

		/// <summary>
		/// User's mean score for manga.
		/// </summary>
		[JsonProperty(PropertyName = "mean_score")]
		public decimal? MeanScore { get; set; }

		/// <summary>
		/// User's amount of manga currently reading.
		/// </summary>
		[JsonProperty(PropertyName = "reading")]
		public int? Reading { get; set; }

		/// <summary>
		/// User's amount of completed manga.
		/// </summary>
		[JsonProperty(PropertyName = "completed")]
		public int? Completed { get; set; }

		/// <summary>
		/// User's amount of manga on hold.
		/// </summary>
		[JsonProperty(PropertyName = "on_hold")]
		public int? OnHold { get; set; }

		/// <summary>
		/// User's amount of dropped manga.
		/// </summary>
		[JsonProperty(PropertyName = "dropped")]
		public int? Dropped { get; set; }

		/// <summary>
		/// User's amount of plan to read manga.
		/// </summary>
		[JsonProperty(PropertyName = "plan_to_read")]
		public int? PlanToRead { get; set; }

		/// <summary>
		/// User's total amount of manga.
		/// </summary>
		[JsonProperty(PropertyName = "total_entries")]
		public int? TotalEntries { get; set; }

		/// <summary>
		/// Total times user reread manga.
		/// </summary>
		[JsonProperty(PropertyName = "reread")]
		public int? Reread { get; set; }

		/// <summary>
		/// User's total amount of read chapters.
		/// </summary>
		[JsonProperty(PropertyName = "chapters_read")]
		public int? ChaptersRead { get; set; }

		/// <summary>
		/// User's total amount of read volumes.
		/// </summary>
		[JsonProperty(PropertyName = "volumes_read")]
		public int? VolumesRead { get; set; }
	}
}
