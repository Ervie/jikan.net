﻿using System.Text.Json.Serialization;
using System;

namespace JikanDotNet
{
	/// <summary>
	/// Single base user update model class.
	/// </summary>
	public class UserUpdate
	{
		/// <summary>
		/// Data about the user who made the update. Null if checked for specific user.
		/// </summary>
		[JsonPropertyName("user")]
		public UserMetadata User { get; set; }
		
		/// <summary>
		/// Data about the manga/anime the update. Null if checked for specific manga/anime.
		/// </summary>
		[JsonPropertyName("entry")]
		public MalImageSubItem Entry { get; set; }

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