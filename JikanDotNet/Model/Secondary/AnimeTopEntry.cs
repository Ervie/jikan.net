using System.Text.Json.Serialization;

namespace JikanDotNet
{
	/// <summary>
	/// Model class for entry on top anime list.
	/// </summary>
	public class AnimeTopEntry
	{
		/// <summary>
		/// ID associated with MyAnimeList.
		/// </summary>
		[JsonPropertyName("mal_id")]
		public long MalId { get; set; }

		/// <summary>
		/// Rank of the anime on selected list.
		/// </summary>
		[JsonPropertyName("rank")]
		public int Rank { get; set; }

		/// <summary>
		/// URL to anime page.
		/// </summary>
		[JsonPropertyName("url")]
		public string Url { get; set; }

		/// <summary>
		/// Anime's image URL
		/// </summary>
		[JsonPropertyName("image_url")]
		public string ImageURL { get; set; }

		/// <summary>
		/// Title of the anime.
		/// </summary>
		[JsonPropertyName("title")]
		public string Title { get; set; }

		/// <summary>
		/// Anime type (e. g. "TV", "Movie").
		/// </summary>
		[JsonPropertyName("type")]
		public string Type { get; set; }

		/// <summary>
		/// Anime's score on MyAnimeList up to 2 decimal places.
		/// </summary>
		[JsonPropertyName("score")]
		public float? Score { get; set; }

		/// <summary>
		/// Anime members count on MyAnimeList.
		/// </summary>
		[JsonPropertyName("members")]
		public int? Members { get; set; }

		/// <summary>
		/// Date of airing start.
		/// </summary>
		[JsonPropertyName("start_date")]
		public string AiringStart { get; set; }

		/// <summary>
		/// Date of airing end.
		/// </summary>
		[JsonPropertyName("end_date")]
		public string AiringEnd { get; set; }

		/// <summary>
		/// Anime's episode count.
		/// </summary>
		[JsonPropertyName("episodes")]
		public int? Episodes { get; set; }
	}
}