using System;
using System.Text.Json.Serialization;

namespace JikanDotNet
{
	/// <summary>
	/// Model class for entry on user's history.
	/// </summary>
	public class Friend
	{
		/// <summary>
		/// Friend's user metadata.
		/// </summary>
		[JsonPropertyName("user")]
		public UserMetadata User { get; set; }

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