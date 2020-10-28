using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace JikanDotNet
{
	/// <summary>
	/// Anime episodes list model class.
	/// </summary>
	public class AnimeEpisodes: BaseJikanRequest
	{
		/// <summary>
		/// Last page of episodes' list.
		/// </summary>
		[JsonPropertyName("last_page")]
		public int LastPage { get; set; }

		/// <summary>
		/// Anime's episode collection with basic information.
		/// </summary>
		[JsonPropertyName("episodes")]
		public ICollection<AnimeEpisode> EpisodeCollection { get; set; }
	}
}