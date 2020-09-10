using System.Text.Json.Serialization;
using System;

namespace JikanDotNet
{
	/// <summary>
	/// Model class for entry on user's history (single update).
	/// </summary>
	public class Friend
	{
		/// <summary>
		/// Friend's username.
		/// </summary>
		[JsonPropertyName("username")]
		public string Username { get; set; }

		/// <summary>
		/// Url to friend page.
		/// </summary>
		[JsonPropertyName("url")]
		public string Url { get; set; }

		/// <summary>
		/// Friend's image URL.
		/// </summary>
		[JsonPropertyName("image_url")]
		public string ImageURL { get; set; }

		/// <summary>
		/// Timestamp of friend's last online activity.
		/// </summary>
		[JsonPropertyName("last_online")]
		public DateTime? LastOnline { get; set; }

		/// <summary>
		/// Timestamp of friend addidition.
		/// </summary>
		[JsonPropertyName("friends_since")]
		public DateTime? FriendsSince { get; set; }

	}
}