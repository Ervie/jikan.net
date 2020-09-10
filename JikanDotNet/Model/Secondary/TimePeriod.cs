using System.Text.Json.Serialization;
using System;

namespace JikanDotNet
{
	/// <summary>
	/// Model class for time periods, e. g. dates of start and end manga publishing.
	/// </summary>
	public class TimePeriod
	{
		/// <summary>
		/// Start date.
		/// </summary>
		[JsonPropertyName("from")]
		public DateTime? From { get; set; }

		/// <summary>
		/// End date.
		/// </summary>
		[JsonPropertyName("to")]
		public DateTime? To { get; set; }
	}
}