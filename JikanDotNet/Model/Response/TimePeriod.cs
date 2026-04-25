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
		/// Start date. Uses <see cref="DateTimeOffset"/> to preserve the UTC offset returned by the API
		/// (ISO-8601 timestamps with explicit "+00:00" offset) and avoid local-time conversions.
		/// </summary>
		[JsonPropertyName("from")]
		public DateTimeOffset? From { get; set; }

		/// <summary>
		/// End date. Uses <see cref="DateTimeOffset"/> to preserve the UTC offset returned by the API
		/// (ISO-8601 timestamps with explicit "+00:00" offset) and avoid local-time conversions.
		/// </summary>
		[JsonPropertyName("to")]
		public DateTimeOffset? To { get; set; }
	}
}