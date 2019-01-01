using Newtonsoft.Json;
using System;

namespace JikanDotNet
{
	/// <summary>
	/// User's profile model class.
	/// </summary>
	public class UserProfile
	{
		/// <summary>
		/// Username.
		/// </summary>
		[JsonProperty(PropertyName = "username")]
		public string Username { get; set; }

		/// <summary>
		/// User's image URL
		/// </summary>
		[JsonProperty(PropertyName = "image_url")]
		public string ImageURL { get; set; }

		/// <summary>
		/// User's URL
		/// </summary>
		[JsonProperty(PropertyName = "url")]
		public string URL { get; set; }

		/// <summary>
		/// User's gender.
		/// </summary>
		[JsonProperty(PropertyName = "gender")]
		public string Gender { get; set; }

		/// <summary>
		/// User's location.
		/// </summary>
		[JsonProperty(PropertyName = "location")]
		public string Location { get; set; }

		/// <summary>
		/// Timestamp of user's last activity.
		/// </summary>
		[JsonProperty(PropertyName = "last_online")]
		public DateTime? LastOnline { get; set; }

		/// <summary>
		/// User's birthday.
		/// </summary>
		[JsonProperty(PropertyName = "birthday")]
		public DateTime? Birthday { get; set; }

		/// <summary>
		/// Timestamp of user's account creation
		/// </summary>
		[JsonProperty(PropertyName = "joined")]
		public DateTime? Joined { get; set; }

		/// <summary>
		/// User's favorite section.
		/// </summary>
		[JsonProperty(PropertyName = "favorites")]
		public UserFavoritesSection Favorites { get; set; }

		/// <summary>
		/// User's anime statistics.
		/// </summary>
		[JsonProperty(PropertyName = "anime_stats")]
		public UserAnimeStatistics AnimeStatistics { get; set; }

		/// <summary>
		/// User's manga statistics.
		/// </summary>
		[JsonProperty(PropertyName = "manga_stats")]
		public UserMangaStatistics MangaStatistics { get; set; }

		/// <summary>
		/// User's about section as a text.
		/// </summary>
		[JsonProperty(PropertyName = "about")]
		public string About { get; set; }
	}
}
