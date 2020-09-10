using System.Text.Json.Serialization;
using System;

namespace JikanDotNet
{
	/// <summary>
	/// User's profile model class.
	/// </summary>
	public class UserProfile: BaseJikanRequest
	{
		/// <summary>
		/// Username.
		/// </summary>
		[JsonPropertyName("username")]
		public string Username { get; set; }

		/// <summary>
		/// User's image URL
		/// </summary>
		[JsonPropertyName("image_url")]
		public string ImageURL { get; set; }

		/// <summary>
		/// User's URL
		/// </summary>
		[JsonPropertyName("url")]
		public string URL { get; set; }

		/// <summary>
		/// User's id.
		/// </summary>
		[JsonPropertyName("user_id")]
		public long UserId { get; set; }

		/// <summary>
		/// User's gender.
		/// </summary>
		[JsonPropertyName("gender")]
		public string Gender { get; set; }

		/// <summary>
		/// User's location.
		/// </summary>
		[JsonPropertyName("location")]
		public string Location { get; set; }

		/// <summary>
		/// Timestamp of user's last activity.
		/// </summary>
		[JsonPropertyName("last_online")]
		public DateTime? LastOnline { get; set; }

		/// <summary>
		/// User's birthday.
		/// </summary>
		[JsonPropertyName("birthday")]
		public DateTime? Birthday { get; set; }

		/// <summary>
		/// Timestamp of user's account creation
		/// </summary>
		[JsonPropertyName("joined")]
		public DateTime? Joined { get; set; }

		/// <summary>
		/// User's favorite section.
		/// </summary>
		[JsonPropertyName("favorites")]
		public UserFavoritesSection Favorites { get; set; }

		/// <summary>
		/// User's anime statistics.
		/// </summary>
		[JsonPropertyName("anime_stats")]
		public UserAnimeStatistics AnimeStatistics { get; set; }

		/// <summary>
		/// User's manga statistics.
		/// </summary>
		[JsonPropertyName("manga_stats")]
		public UserMangaStatistics MangaStatistics { get; set; }

		/// <summary>
		/// User's about section as a text.
		/// </summary>
		[JsonPropertyName("about")]
		public string About { get; set; }
	}
}
