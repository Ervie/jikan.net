using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace JikanDotNet
{
	/// <summary>
	/// Model class for schedule of current season.
	/// </summary>
	public class Schedule: BaseJikanRequest
	{
		/// <summary>
		/// All current season entries scheduled for Monday.
		/// </summary>
		[JsonPropertyName("monday")]
		public ICollection<AnimeSubEntry> Monday { get; set; }

		/// <summary>
		/// All current season entries scheduled for Tuesday.
		/// </summary>
		[JsonPropertyName("tuesday")]
		public ICollection<AnimeSubEntry> Tuesday { get; set; }

		/// <summary>
		/// All current season entries scheduled for Wednesday.
		/// </summary>
		[JsonPropertyName("wednesday")]
		public ICollection<AnimeSubEntry> Wednesday { get; set; }

		/// <summary>
		/// All current season entries scheduled for Thursday.
		/// </summary>
		[JsonPropertyName("thursday")]
		public ICollection<AnimeSubEntry> Thursday { get; set; }

		/// <summary>
		/// All current season entries scheduled for Friday.
		/// </summary>
		[JsonPropertyName("friday")]
		public ICollection<AnimeSubEntry> Friday { get; set; }

		/// <summary>
		/// All current season entries scheduled for Saturday.
		/// </summary>
		[JsonPropertyName("saturday")]
		public ICollection<AnimeSubEntry> Saturday { get; set; }

		/// <summary>
		/// All current season entries scheduled for Sunday.
		/// </summary>Sunday
		[JsonPropertyName("sunday")]
		public ICollection<AnimeSubEntry> Sunday { get; set; }

		/// <summary>
		/// All current season entries scheduled for other days (irregular airing).
		/// </summary>Sunday
		[JsonPropertyName("other")]
		public ICollection<AnimeSubEntry> Other { get; set; }

		/// <summary>
		/// All current season entries scheduled for unknown airing.
		/// </summary>Sunday
		[JsonPropertyName("unknown")]
		public ICollection<AnimeSubEntry> Unknown { get; set; }
	}
}