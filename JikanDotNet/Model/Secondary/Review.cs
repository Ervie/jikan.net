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
		public string Url { get; set; }

		/// <summary>
		/// Review's type.
		/// </summary>
		[JsonPropertyName("type")]
		public string Type { get; set; }

		/// <summary>
		/// Date of review creation.
		/// </summary>
		[JsonPropertyName("date")]
		public DateTime? Date { get; set; }

		/// <summary>
		/// Review's content.
		/// </summary>
		[JsonPropertyName("review")]
		public string Content { get; set; }

		/// <summary>
		/// Count of votes when the review was marked as helpful.
		/// </summary>
		[JsonPropertyName("votes")]
		public int? Votes { get; set; }

		/// <summary>
		/// Reviewwing user
		/// </summary>
		[JsonPropertyName("user")]
		public UserMetadata User { get; set; }
	}
}