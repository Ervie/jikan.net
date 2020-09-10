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
		[JsonPropertyName("author_name")]
		public string AuthorName { get; set; }

		/// <summary>
		/// URL to profile of post author.
		/// </summary>
		[JsonPropertyName("author_url")]
		public string AuthorURL { get; set; }

		/// <summary>
		/// Relative date of time when post was publicated.
		/// </summary>
		[JsonPropertyName("date_posted")]
		public DateTime? DateRelative { get; set; }
	}
}