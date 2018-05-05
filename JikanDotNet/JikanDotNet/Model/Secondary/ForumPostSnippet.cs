using Newtonsoft.Json;

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
		[JsonProperty(PropertyName = "url")]
		public string Url { get; set; }

		/// <summary>
		/// Post's author username.
		/// </summary>
		[JsonProperty(PropertyName = "author_name")]
		public string AuthorName { get; set; }

		/// <summary>
		/// URL to profile of post author.
		/// </summary>
		[JsonProperty(PropertyName = "author_url")]
		public string AuthorURL { get; set; }

		/// <summary>
		/// Relative date of time when post was publicated.
		/// </summary>
		[JsonProperty(PropertyName = "date_relative")]
		public string DateRelative { get; set; }
	}
}