using System.Text.Json.Serialization;
using System;

namespace JikanDotNet
{
	/// <summary>
	/// News model class.
	/// </summary>
	public class News
	{
		/// <summary>
		/// News' URL.
		/// </summary>
		[JsonPropertyName("url")]
		public string Url { get; set; }

		/// <summary>
		/// News' image URL.
		/// </summary>
		[JsonPropertyName("image_url")]
		public string ImageURL { get; set; }

		/// <summary>
		/// News' forum URL.
		/// </summary>
		[JsonPropertyName("forum_url")]
		public string ForumUrl { get; set; }

		/// <summary>
		/// Amount of comments under news.
		/// </summary>
		[JsonPropertyName("comments")]
		public int? Comments { get; set; }

		/// <summary>
		/// Title of the news.
		/// </summary>
		[JsonPropertyName("title")]
		public string Title { get; set; }

		/// <summary>
		/// News' publication date.
		/// </summary>
		[JsonPropertyName("date")]
		public DateTime? Date { get; set; }

		/// <summary>
		/// News' author username.
		/// </summary>
		[JsonPropertyName("author_name")]
		public string Author { get; set; }

		/// <summary>
		/// News' author url.
		/// </summary>
		[JsonPropertyName("author_url")]
		public string AuthorUrl { get; set; }
	}
}