using System.Text.Json.Serialization;

namespace JikanDotNet
{
	/// <summary>
	/// Model representing user statistics
	/// </summary>
	public class UserStatistics
	{
		/// <summary>
		/// User's anime statistics.
		/// </summary>
		[JsonPropertyName("anime")]
		public UserAnimeStatistics AnimeStatistics { get; set; }

		/// <summary>
		/// User's manga statistics.
		/// </summary>
		[JsonPropertyName("manga")]
		public UserMangaStatistics MangaStatistics { get; set; }
	}
}