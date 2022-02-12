using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace JikanDotNet
{
	/// <summary>
	/// Entry on user's manga list model class.
	/// </summary>
	public class MangaListEntry
	{
		/// <summary>
		/// Current user's reading status of manga.
		/// </summary>
		[JsonPropertyName("reading_status")]
		public string ReadingStatus { get; set; }

		/// <summary>
		/// User's score for the manga. 0 if not assigned yet.
		/// </summary>
		[JsonPropertyName("score")]
		public int Score { get; set; }

		/// <summary>
		/// Manga's chapters count read by the user.
		/// </summary>
		[JsonPropertyName("chapters_read")]
		public int? ChaptersRead { get; set; }

		/// <summary>
		/// Manga's volumes count read by the user.
		/// </summary>
		[JsonPropertyName("volumes_read")]
		public int? VolumesRead { get; set; }

		/// <summary>
		/// Tags added by user.
		/// </summary>
		[JsonPropertyName("tags")]
		public string Tags { get; set; }

		/// <summary>
		/// Does user reread manga.
		/// </summary>
		[JsonPropertyName("is_rereading")]
		public bool? IsRereading { get; set; }

		/// <summary>
		/// Start date of user reading.
		/// </summary>
		[JsonPropertyName("read_start_date")]
		public DateTime? ReadStartDate { get; set; }

		/// <summary>
		/// End date of user reading.
		/// </summary>
		[JsonPropertyName("read_end_date")]
		public DateTime? ReadEndDate { get; set; }

		/// <summary>
		/// Time user has been reading manga.
		/// </summary>
		[JsonPropertyName("days")]
		public int? Days { get; set; }

		/// <summary>
		/// Retail of manga on user's list.
		/// </summary>
		[JsonPropertyName("retail")]
		public int? Retail { get; set; }

		/// <summary>
		/// Priority of manga on user's list.
		/// </summary>
		[JsonPropertyName("priority")]
		public string Priority { get; set; }

		/// <summary>
		/// Manga details.
		/// </summary>
		[JsonPropertyName("manga")]
		public MangaListEntryDetails Manga { get; set; }
	}

	/// <summary>
	/// Anime details on the user list.
	/// </summary>
	public class MangaListEntryDetails
	{
		/// <summary>
		/// ID associated with MyAnimeList.
		/// </summary>
		[JsonPropertyName("mal_id")]
		public long MalId { get; set; }

		/// <summary>
		/// Title of the manga.
		/// </summary>
		[JsonPropertyName("title")]
		public string Title { get; set; }

		/// <summary>
		/// Manga's URL
		/// </summary>
		[JsonPropertyName("url")]
		public string Url { get; set; }

		/// <summary>
		/// Manga's image set
		/// </summary>
		[JsonPropertyName("images")]
		public ImagesSet Images { get; set; }

		/// <summary>
		/// Manga type (e. g. "Manga", "Light novel").
		/// </summary>
		[JsonPropertyName("type")]
		public string Type { get; set; }

		/// <summary>
		/// Manga's chapters total count. 0 if not finished.
		/// </summary>
		[JsonPropertyName("chapters")]
		public int? Chapters { get; set; }

		/// <summary>
		/// Manga's volumes total count. 0 if not finished.
		/// </summary>
		[JsonPropertyName("volumes")]
		public int? Volumes { get; set; }

		/// <summary>
		/// Anime's airing status (e. g. "Publishing").
		/// </summary>
		[JsonPropertyName("status")]
		public string Status { get; set; }

		/// <summary>
		/// Is manga currently being published.
		/// </summary>
		[JsonPropertyName("publishing")]
		public bool Publishing { get; set; }

		/// <summary>
		/// Assiociative keys "from" and "to" .
		/// </summary>
		[JsonPropertyName("published")]
		public TimePeriod Published { get; set; }

		/// <summary>
		/// Manga's magazines numerically indexed with array values.
		/// </summary>
		[JsonPropertyName("magazines")]
		public ICollection<MalUrl> Magazines { get; set; }

		/// <summary>
		/// Manga's genres numerically indexed with array values.
		/// </summary>
		[JsonPropertyName("genres")]
		public ICollection<MalUrl> Genres { get; set; }

		/// <summary>
		/// Manga's demographics
		/// </summary>
		[JsonPropertyName("demographics")]
		public ICollection<MalUrl> Demographics { get; set; }
	}
}