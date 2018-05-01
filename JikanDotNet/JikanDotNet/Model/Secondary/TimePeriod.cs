using Newtonsoft.Json;
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
		[JsonProperty(PropertyName = "from")]
		public DateTime From { get; set; }

		/// <summary>
		/// End date.
		/// </summary>
		[JsonProperty(PropertyName = "to")]
		public DateTime To { get; set; }
	}
}