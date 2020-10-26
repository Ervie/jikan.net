using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace JikanDotNet.Model.Secondary
{
	/// <summary>
	/// Anime trailer  model class.
	/// </summary>
	public class AnimeTrailer
	{
		/// <summary>
		/// ID associated with Youtube.
		/// </summary>
		[JsonPropertyName("youtube_id")]
		public string YoutubeUrl { get; set; }

		/// <summary>
		/// Url to the video.
		/// </summary>
		[JsonPropertyName("url")]
		public string Url { get; set; }

		/// <summary>
		/// Embed url to the video.
		/// </summary>
		[JsonPropertyName("embed_url")]
		public string EmbedUrl { get; set; }

		/// <summary>
		/// Set of images related to the trailer.
		/// </summary>
		[JsonPropertyName("images")]
		public Picture Images { get; set; }
	}
}
