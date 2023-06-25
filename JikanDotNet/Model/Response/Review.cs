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
		/// Reviewing user.
		/// </summary>
		[JsonPropertyName("user")]
		public UserMetadata User { get; set; }
		
		/// <summary>
		/// Review reactions.
		/// </summary>
		[JsonPropertyName("reactions")]
		public ReviewReactions Reactions { get; set; }
		
		/// <summary>
		/// Number of episodes watched by the reviewer (if review is about anime).
		/// </summary>
		[JsonPropertyName("episodes_watched")]
		public int? EpisodesWatched { get; set; }
		
		/// <summary>
		/// Number of chapters read by reviewer (if review is about manga).
		/// </summary>
		[JsonPropertyName("chapters_read")]
		public int? ChaptersRead { get; set; }
		
		/// <summary>
		/// The score of the review.
		/// </summary>
		[JsonPropertyName("score")]
		public int Score { get; set; }
		
		/// <summary>
		/// If the review contains spoilers.
		/// </summary>
		[JsonPropertyName("is_spoiler")]
		public bool IsSpoiler { get; set; }
	}
}