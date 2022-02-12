using System.Text.Json.Serialization;
using System;

namespace JikanDotNet
{
	/// <summary>
	/// Model class for MyAnimeList forum topic.
	/// </summary>
	public class ForumTopic
	{
		/// <summary>
		/// Topic's MyAnimeList Id.
		/// </summary>
		[JsonPropertyName("mal_id")]
		public long MalId { get; set; }

		/// <summary>
		/// Topic's URL.
		/// </summary>
		[JsonPropertyName("url")]
		public string Url { get; set; }

		/// <summary>
		/// Topic's title.
		/// </summary>
		[JsonPropertyName("title")]
		public string Title { get; set; }

		/// <summary>
		/// Date of topic start.
		/// </summary>
		[JsonPropertyName("date")]
		public DateTime? Date { get; set; }

		/// <summary>
		/// Topic's author username.
		/// </summary>
		[JsonPropertyName("author_username")]
		public string AuthorUsername { get; set; }

		/// <summary>
		/// URL to profile of topic author.
		/// </summary>
		[JsonPropertyName("author_url")]
		public string AuthorUrl { get; set; }

		/// <summary>
		/// Comment count.
		/// </summary>
		[JsonPropertyName("comments")]
		public int? Comments { get; set; }

		/// <summary>
		/// Basic information about last comment in the topic.
		/// </summary>
		[JsonPropertyName("last_comment")]
		public ForumPostSnippet LastPost { get; set; }
	}
}