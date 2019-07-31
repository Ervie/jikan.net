using Newtonsoft.Json;
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
		[JsonProperty(PropertyName = "url")]
		public string Url { get; set; }

		/// <summary>
		/// News' image URL.
		/// </summary>
		[JsonProperty(PropertyName = "image_url")]
		public string ImageURL { get; set; }

		/// <summary>
		/// News' forum URL.
		/// </summary>
		[JsonProperty(PropertyName = "forum_url")]
		public string ForumUrl { get; set; }

		/// <summary>
		/// Amount of comments under news.
		/// </summary>
		[JsonProperty(PropertyName = "comments")]
		public int? Comments { get; set; }

		/// <summary>
		/// Title of the news.
		/// </summary>
		[JsonProperty(PropertyName = "title")]
		public string Title { get; set; }

		/// <summary>
		/// News' publication date.
		/// </summary>
		[JsonProperty(PropertyName = "date")]
		public DateTime? Date { get; set; }

		/// <summary>
		/// News' author username.
		/// </summary>
		[JsonProperty(PropertyName = "author_name")]
		public string Author { get; set; }

		/// <summary>
		/// News' author url.
		/// </summary>
		[JsonProperty(PropertyName = "author_url")]
		public string AuthorUrl { get; set; }
	}
}