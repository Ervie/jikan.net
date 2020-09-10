using System.Text.Json.Serialization;
using System;

namespace JikanDotNet
{
	/// <summary>
	/// Entry on user's manga list model class.
	/// </summary>
	public class MangaListEntry
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
		/// Manga's image URL
		/// </summary>
		[JsonPropertyName("image_url")]
		public string ImageURL { get; set; }

		/// <summary>
		/// Manga's URL
		/// </summary>
		[JsonPropertyName("url")]
		public string URL { get; set; }

		/// <summary>
		/// Manga type (e. g. "Manga", "Light novel").
		/// </summary>
		[JsonPropertyName("type")]
		public string Type { get; set; }

		/// <summary>
		/// Manga's chapters count read by the user.
		/// </summary>
		[JsonPropertyName("read_chapters")]
		public int? ReadChapters { get; set; }

		/// <summary>
		/// Manga's volumes count read by the user.
		/// </summary>
		[JsonPropertyName("read_volumes")]
		public int? ReadVolumes { get; set; }

		/// <summary>
		/// Manga's chapters total count. 0 if not finished.
		/// </summary>
		[JsonPropertyName("total_chapters")]
		public int? TotalChapters { get; set; }

		/// <summary>
		/// Manga's volumes total count. 0 if not finished.
		/// </summary>
		[JsonPropertyName("total_volumes")]
		public int? TotalVolumes { get; set; }

		/// <summary>
		/// User's score for the manga. 0 if not assigned yet.
		/// </summary>
		[JsonPropertyName("score")]
		public int Score { get; set; }

		/// <summary>
		/// Does user reread manga.
		/// </summary>
		[JsonPropertyName("is_rereading")]
		public bool? IsRereading { get; set; }
		
		/// <summary>
		/// Start date of manga publishing.
		/// </summary>
		[JsonPropertyName("start_date")]
		public DateTime? StartDate { get; set; }

		/// <summary>
		/// End date of manga publishing.
		/// </summary>
		[JsonPropertyName("end_date")]
		public DateTime? EndDate { get; set; }

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
		/// Priority of manga on user's list.
		/// </summary>
		[JsonPropertyName("priority")]
		public string Priority { get; set; }

		/// <summary>
		/// Current publishing status of manga.
		/// </summary>
		[JsonPropertyName("publishing_status")]
		public AiringStatus PublishingStatus { get; set; }

		/// <summary>
		/// Current user's reading status of manga.
		/// </summary>
		[JsonPropertyName("reading_status")]
		public UserMangaListExtension ReadingStatus { get; set; }
	}
}