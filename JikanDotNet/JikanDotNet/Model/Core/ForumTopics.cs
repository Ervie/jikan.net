using Newtonsoft.Json;
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
		[JsonProperty(PropertyName = "topics")]
		public ICollection<ForumTopic> Topics { get; set; }
	}
}
