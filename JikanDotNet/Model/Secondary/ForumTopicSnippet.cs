using System.Text.Json.Serialization;

namespace JikanDotNet.Model
{
	/// <summary>
	/// Model for basic information about forum topic.
	/// </summary>
	internal class ForumTopicSnippet
	{
		/// <summary>
		/// ID associated with MyAnimeList.
		/// </summary>
		[JsonPropertyName("mal_id")]
		public long MalId { get; set; }

		/// <summary>
		/// Url to the topic.
		/// </summary>
		[JsonPropertyName("url")]
		public long Url { get; set; }
	}
}