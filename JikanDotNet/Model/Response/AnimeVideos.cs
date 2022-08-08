using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace JikanDotNet
{
	/// <summary>
	/// Anime related videos list model class.
	/// </summary>
	public class AnimeVideos
	{
		/// <summary>
		/// Anime's related promo videos URLs.
		/// </summary>
		[JsonPropertyName("promo")]
		public ICollection<PromoVideo> PromoVideos { get; set; }

		/// <summary>
		/// Anime's related episode videos URLs.
		/// </summary>
		[JsonPropertyName("episodes")]
		public ICollection<EpisodeVideo> EpisodeVideos { get; set; }
		
		/// <summary>
		/// Anime's related music videos URLs.
		/// </summary>
		[JsonPropertyName("music_videos")]
		public ICollection<MusicVideo> MusicVideos { get; set; }
	}
}