using System.Text.Json.Serialization;
using System;

namespace JikanDotNet
{
	/// <summary>
	/// Model for basic information about forum post.
	/// </summary>
	public class ForumPostSnippet
	{
		/// <summary>
		/// Url to post.
		/// </summary>
		[JsonPropertyName("url")]
		public string Url { get; set; }

		/// <summary>
		/// Post's author username.
		/// </summary>
		[JsonPropertyName("author_username")]
		public string AuthorUsername { get; set; }

		/// <summary>
		/// URL to profile of post author.
		/// </summary>
		[JsonPropertyName("author_url")]
		public string AuthorUrl { get; set; }

		/// <summary>
		/// Date when the post was publicated.
		/// </summary>
		[JsonPropertyName("date")]
		public DateTime? Date { get; set; }
	}
}