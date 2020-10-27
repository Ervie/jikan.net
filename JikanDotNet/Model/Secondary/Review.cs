using System.Text.Json.Serialization;
using System;

namespace JikanDotNet
{
	/// <summary>
	/// Model class for base review.
	/// </summary>
	public class Review
	{
		/// <summary>
		/// ID associated with MyAnimeList.
		/// </summary>
		[JsonPropertyName("mal_id")]
		public long MalId { get; set; }

		/// <summary>
		/// Review's URL.
		/// </summary>
		[JsonPropertyName("url")]
		public string URL { get; set; }

		/// <summary>
		/// Date of review creation.
		/// </summary>
		[JsonPropertyName("date")]
		public DateTime? Date { get; set; }

		/// <summary>
		/// Review's content.
		/// </summary>
		[JsonPropertyName("content")]
		public string Content { get; set; }

		/// <summary>
		/// Number of times the review was marked as helpful.
		/// </summary>
		[JsonPropertyName("helpful_count")]
		public int? HelpfulCount { get; set; }
	}
}