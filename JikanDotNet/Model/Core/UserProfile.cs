using System.Text.Json.Serialization;
using System;

namespace JikanDotNet
{
	/// <summary>
	/// User's profile model class.
	/// </summary>
	public class UserProfile
	{
		/// <summary>
		/// ID associated with MyAnimeList.
		/// </summary>
		[JsonPropertyName("mal_id")]
		public long? MalId { get; set; }

		/// <summary>
		/// Username.
		/// </summary>
		[JsonPropertyName("username")]
		public string Username { get; set; }

		/// <summary>
		/// User's URL
		/// </summary>
		[JsonPropertyName("url")]
		public string Url { get; set; }

		/// <summary>
		/// User's image set
		/// </summary>
		[JsonPropertyName("images")]
		public ImagesSet Images { get; set; }

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
	}
}
