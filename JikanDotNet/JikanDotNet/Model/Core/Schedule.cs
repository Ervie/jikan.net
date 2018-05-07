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
		public ICollection<SeasonEntry> Monday;

		/// <summary>
		/// All current season entries scheduled for Tuesday.
		/// </summary>
		[JsonProperty(PropertyName = "tuesday")]
		public ICollection<SeasonEntry> Tuesday;

		/// <summary>
		/// All current season entries scheduled for Wednesday.
		/// </summary>
		[JsonProperty(PropertyName = "wednesday")]
		public ICollection<SeasonEntry> Wednesday;

		/// <summary>
		/// All current season entries scheduled for Thursday.
		/// </summary>
		[JsonProperty(PropertyName = "thursday")]
		public ICollection<SeasonEntry> Thursday;

		/// <summary>
		/// All current season entries scheduled for Friday.
		/// </summary>
		[JsonProperty(PropertyName = "friday")]
		public ICollection<SeasonEntry> Friday;

		/// <summary>
		/// All current season entries scheduled for Saturday.
		/// </summary>
		[JsonProperty(PropertyName = "saturday")]
		public ICollection<SeasonEntry> Saturday;

		/// <summary>
		/// All current season entries scheduled for Sunday.
		/// </summary>Sunday
		[JsonProperty(PropertyName = "sunday")]
		public ICollection<SeasonEntry> Sunday;
	}
}