using Newtonsoft.Json;
using System;

namespace JikanDotNet
{
	/// <summary>
	/// Model class for base review.
	/// </summary>
	public class Review : IMalEntity
	{
		/// <summary>
		/// ID associated with MyAnimeList.
		/// </summary>
		[JsonProperty(PropertyName = "mal_id")]
		public long MalId { get; set; }

		/// <summary>
		/// Review's URL.
		/// </summary>
		[JsonProperty(PropertyName = "url")]
		public string URL { get; set; }

		/// <summary>
		/// Date of review creation.
		/// </summary>
		[JsonProperty(PropertyName = "date")]
		public DateTime? Date { get; set; }

		/// <summary>
		/// Review's content.
		/// </summary>
		[JsonProperty(PropertyName = "content")]
		public string Content { get; set; }

		/// <summary>
		/// Number of times the review was marked as helpful.
		/// </summary>
		[JsonProperty(PropertyName = "helpful_count")]
		public int? HelpfulCount { get; set; }
	}
}