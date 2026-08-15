using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Tenrai
{
	/// <summary>
	/// Public status snapshot of the Tenrai services.
	/// </summary>
	public class TenraiStatus
	{
		/// <summary>
		/// Version of the status API contract.
		/// </summary>
		[JsonPropertyName("api_version")]
		public int ApiVersion { get; set; }

		/// <summary>
		/// Details of the status page this snapshot belongs to.
		/// </summary>
		[JsonPropertyName("page")]
		public StatusPage Page { get; set; }

		/// <summary>
		/// Health of the monitoring process itself.
		/// </summary>
		[JsonPropertyName("checker")]
		public StatusChecker Checker { get; set; }

		/// <summary>
		/// Date the snapshot was generated.
		/// </summary>
		[JsonPropertyName("generated_at")]
		public DateTime? GeneratedAt { get; set; }

		/// <summary>
		/// Monitored services and their current status.
		/// </summary>
		[JsonPropertyName("services")]
		public ICollection<StatusService> Services { get; set; }

		/// <summary>
		/// Maintenance windows that are scheduled or currently running.
		/// </summary>
		[JsonPropertyName("scheduled_maintenances")]
		public ICollection<StatusMaintenance> ScheduledMaintenances { get; set; }
	}
}
