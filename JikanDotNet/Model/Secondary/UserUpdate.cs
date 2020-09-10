using System.Text.Json.Serialization;
using System;

namespace JikanDotNet
{
	/// <summary>
	/// Single base user update model class.
	/// </summary>
	public class UserUpdate
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
		/// User's score.
		/// </summary>
		[JsonPropertyName("score")]
		public int? Score { get; set; }

		/// <summary>
		/// Date ofd the update.
		/// </summary>
		[JsonPropertyName("date")]
		public DateTime? Date { get; set; }

		/// <summary>
		/// Status (reading, watching, completed, etc.)
		/// </summary>
		[JsonPropertyName("status")]
		public string Status { get; set; }
	}
}