using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace JikanDotNet
{
	/// <summary>
	/// Model class for collection of MyAnimeList forum topic.
	/// </summary>
	public class ForumTopics: BaseJikanRequest
	{
		/// <summary>
		/// Forum topics related to anime.
		/// </summary>
		[JsonPropertyName("topics")]
		public ICollection<ForumTopic> Topics { get; set; }
	}
}
