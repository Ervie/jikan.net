using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace JikanDotNet
{
	/// <summary>
	/// Model class for single recommendation from user profile.
	/// </summary>
	public class UserRecommendation
	{
		/// <summary>
		/// Combined mal ids of both recommended entries.
		/// </summary>
		[JsonPropertyName("mal_id")]
		public string MalIds { get; set; }

		/// <summary>
		/// Both recommended entries/
		/// </summary>
		[JsonPropertyName("entry")]
		public ICollection<MalImageSubItem> Entries { get; set; }

		/// <summary>
		/// Recommendation content
		/// </summary>
		[JsonPropertyName("content")]
		public string Content { get; set; }

		/// <summary>
		/// Date of creation
		/// </summary>
		[JsonPropertyName("date")]
		public DateTime Date { get; set; }

		/// <summary>
		/// Reviewing user.
		/// </summary>
		[JsonPropertyName("user")]
		public UserMetadata User { get; set; }
	}
}