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
		[JsonPropertyName("topic_id")]
		public long? TopicId { get; set; }

		/// <summary>
		/// Topic's URL.
		/// </summary>
		[JsonPropertyName("url")]
		public string Url { get; set; }

		/// <summary>
		/// Topic's title.
		/// </summary>
		[JsonPropertyName("Title")]
		public string Title { get; set; }

		/// <summary>
		/// Date of topic start.
		/// </summary>
		[JsonPropertyName("date_posted")]
		public DateTime? DatePosted { get; set; }

		/// <summary>
		/// Topic's author username.
		/// </summary>
		[JsonPropertyName("author_name")]
		public string AuthorName { get; set; }

		/// <summary>
		/// URL to profile of topic author.
		/// </summary>
		[JsonPropertyName("author_url")]
		public string AuthorURL { get; set; }

		/// <summary>
		/// Number of replies.
		/// </summary>
		[JsonPropertyName("replies")]
		public int? Replies { get; set; }

		/// <summary>
		/// Basic information about last post in the topic.
		/// </summary>
		[JsonPropertyName("last_post")]
		public ForumPostSnippet LastPost { get; set; }
	}
}