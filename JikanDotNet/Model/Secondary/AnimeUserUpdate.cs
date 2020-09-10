using System.Text.Json.Serialization;

namespace JikanDotNet
{
	/// <summary>
	/// Single anime user update model class.
	/// </summary>
	public class AnimeUserUpdate : UserUpdate
	{
		/// <summary>
		/// Amount of episodes seen by the user.
		/// </summary>
		[JsonPropertyName("episodes_seen")]
		public int? EpisodesSeen { get; set; }

		/// <summary>
		/// Total amount of the episodes.
		/// </summary>
		[JsonPropertyName("episodes_total")]
		public int? EpisodesTotal { get; set; }
	}
}