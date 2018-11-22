using Newtonsoft.Json;
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
		[JsonProperty(PropertyName = "username")]
		public string Username { get; set; }

		/// <summary>
		/// Url to friend page.
		/// </summary>
		[JsonProperty(PropertyName = "url")]
		public string Url { get; set; }

		/// <summary>
		/// Friend's image URL.
		/// </summary>
		[JsonProperty(PropertyName = "image_url")]
		public string ImageURL { get; set; }

		/// <summary>
		/// Timestamp of friend's last online activity.
		/// </summary>
		[JsonProperty(PropertyName = "last_online")]
		public DateTime? LastOnline { get; set; }

		/// <summary>
		/// Timestamp of friend addidition.
		/// </summary>
		[JsonProperty(PropertyName = "friends_since")]
		public DateTime? FriendsSince { get; set; }

	}
}