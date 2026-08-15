using System;
using System.Text.Json.Serialization;

namespace Tenrai
{
	/// <summary>
	/// A scheduled or active maintenance window.
	/// </summary>
	public class StatusMaintenance
	{
		/// <summary>
		/// Identifier of the maintenance window.
		/// </summary>
		[JsonPropertyName("id")]
		public string Id { get; set; }

		/// <summary>
		/// Identifier of the affected service, or the literal "all" when every service is affected.
		/// </summary>
		[JsonPropertyName("service_id")]
		public string ServiceId { get; set; }

		/// <summary>
		/// Title of the maintenance window.
		/// </summary>
		[JsonPropertyName("title")]
		public string Title { get; set; }

		/// <summary>
		/// Date the maintenance window starts.
		/// </summary>
		[JsonPropertyName("starts_at")]
		public DateTime? StartsAt { get; set; }

		/// <summary>
		/// Date the maintenance window ends.
		/// </summary>
		[JsonPropertyName("ends_at")]
		public DateTime? EndsAt { get; set; }

		/// <summary>
		/// Status the affected service reports while the window is running.
		/// </summary>
		[JsonPropertyName("status")]
		public string Status { get; set; }

		/// <summary>
		/// Whether the window is "scheduled" or "active".
		/// </summary>
		[JsonPropertyName("state")]
		public string State { get; set; }

		/// <summary>
		/// Date the maintenance window was created.
		/// </summary>
		[JsonPropertyName("created_at")]
		public DateTime? CreatedAt { get; set; }

		/// <summary>
		/// Date the maintenance window was last updated.
		/// </summary>
		[JsonPropertyName("updated_at")]
		public DateTime? UpdatedAt { get; set; }
	}
}
