using System;
using System.Text.Json.Serialization;

namespace Tenrai
{
	/// <summary>
	/// Health of the process that probes the monitored services.
	/// </summary>
	public class StatusChecker
	{
		/// <summary>
		/// Is the checker itself running normally. When false, the per service statuses may be out of date.
		/// </summary>
		[JsonPropertyName("healthy")]
		public bool Healthy { get; set; }

		/// <summary>
		/// Date of the last completed check.
		/// </summary>
		[JsonPropertyName("last_check_at")]
		public DateTime? LastCheckAt { get; set; }

		/// <summary>
		/// Number of seconds after <see cref="LastCheckAt">LastCheckAt</see> at which the snapshot is considered stale.
		/// </summary>
		[JsonPropertyName("stale_after_seconds")]
		public int StaleAfterSeconds { get; set; }
	}
}
