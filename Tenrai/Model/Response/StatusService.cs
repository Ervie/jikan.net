using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Tenrai
{
	/// <summary>
	/// Current status and 90 day history of a single monitored service.
	/// </summary>
	public class StatusService
	{
		/// <summary>
		/// Identifier of the service (e. g. "tenrai").
		/// </summary>
		[JsonPropertyName("id")]
		public string Id { get; set; }

		/// <summary>
		/// Display name of the service.
		/// </summary>
		[JsonPropertyName("name")]
		public string Name { get; set; }

		/// <summary>
		/// Current status. One of "operational", "degraded", "down", "unknown" or "maintenance".
		/// </summary>
		[JsonPropertyName("status")]
		public string Status { get; set; }

		/// <summary>
		/// Link to the service's homepage.
		/// </summary>
		[JsonPropertyName("homepage_url")]
		public string HomepageUrl { get; set; }

		/// <summary>
		/// Link to the service's logo.
		/// </summary>
		[JsonPropertyName("logo_url")]
		public string LogoUrl { get; set; }

		/// <summary>
		/// Date this service was last probed.
		/// </summary>
		[JsonPropertyName("last_check_at")]
		public DateTime? LastCheckAt { get; set; }

		/// <summary>
		/// Total minutes the service was down over the last 90 days.
		/// </summary>
		[JsonPropertyName("outage_minutes_90d")]
		public int OutageMinutes90d { get; set; }

		/// <summary>
		/// Total minutes the service was degraded over the last 90 days.
		/// </summary>
		[JsonPropertyName("degraded_minutes_90d")]
		public int DegradedMinutes90d { get; set; }

		/// <summary>
		/// Minutes down per day over the last 90 days, keyed by date in "yyyy-MM-dd" format. Days without an outage are omitted.
		/// </summary>
		[JsonPropertyName("daily_outage_minutes_90d")]
		public IDictionary<string, int> DailyOutageMinutes90d { get; set; }

		/// <summary>
		/// Minutes degraded per day over the last 90 days, keyed by date in "yyyy-MM-dd" format. Days without degradation are omitted.
		/// </summary>
		[JsonPropertyName("daily_degraded_minutes_90d")]
		public IDictionary<string, int> DailyDegradedMinutes90d { get; set; }

		/// <summary>
		/// Maintenance window currently affecting this service. Null when there is none.
		/// </summary>
		[JsonPropertyName("active_maintenance")]
		public StatusMaintenance ActiveMaintenance { get; set; }
	}
}
