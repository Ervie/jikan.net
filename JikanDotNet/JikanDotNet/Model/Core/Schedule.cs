using Newtonsoft.Json;
using System.Collections.Generic;

namespace JikanDotNet
{
	/// <summary>
	/// Model class for schedule of current season.
	/// </summary>
	public class Schedule
	{
		/// <summary>
		/// All current season entries scheduled for Monday.
		/// </summary>
		[JsonProperty(PropertyName = "monday")]
		public ICollection<AnimeSubEntry> Monday;

		/// <summary>
		/// All current season entries scheduled for Tuesday.
		/// </summary>
		[JsonProperty(PropertyName = "tuesday")]
		public ICollection<AnimeSubEntry> Tuesday;

		/// <summary>
		/// All current season entries scheduled for Wednesday.
		/// </summary>
		[JsonProperty(PropertyName = "wednesday")]
		public ICollection<AnimeSubEntry> Wednesday;

		/// <summary>
		/// All current season entries scheduled for Thursday.
		/// </summary>
		[JsonProperty(PropertyName = "thursday")]
		public ICollection<AnimeSubEntry> Thursday;

		/// <summary>
		/// All current season entries scheduled for Friday.
		/// </summary>
		[JsonProperty(PropertyName = "friday")]
		public ICollection<AnimeSubEntry> Friday;

		/// <summary>
		/// All current season entries scheduled for Saturday.
		/// </summary>
		[JsonProperty(PropertyName = "saturday")]
		public ICollection<AnimeSubEntry> Saturday;

		/// <summary>
		/// All current season entries scheduled for Sunday.
		/// </summary>Sunday
		[JsonProperty(PropertyName = "sunday")]
		public ICollection<AnimeSubEntry> Sunday;

		/// <summary>
		/// All current season entries scheduled for other days (irregular airing).
		/// </summary>Sunday
		[JsonProperty(PropertyName = "other")]
		public ICollection<AnimeSubEntry> Other;

		/// <summary>
		/// All current season entries scheduled for unknown airing.
		/// </summary>Sunday
		[JsonProperty(PropertyName = "unknown")]
		public ICollection<AnimeSubEntry> Unknown;
	}
}