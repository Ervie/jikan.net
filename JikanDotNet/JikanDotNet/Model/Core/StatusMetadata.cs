using Newtonsoft.Json;

namespace JikanDotNet
{
	/// <summary>
	/// Status metadata model class.
	/// </summary>
	public class StatusMetadata
	{
		/// <summary>
		/// Cached requests.
		/// </summary>
		[JsonProperty(PropertyName = "cached_requests")]
		public int CachedRequests { get; set; }

		/// <summary>
		/// Amount of requests today.
		/// </summary>
		[JsonProperty(PropertyName = "requests_today")]
		public int RequestsToday { get; set; }

		/// <summary>
		/// Amount of requests this week.
		/// </summary>
		[JsonProperty(PropertyName = "requests_this_week")]
		public int RequestsThisWeek { get; set; }

		/// <summary>
		/// Amount of requests this month.
		/// </summary>
		[JsonProperty(PropertyName = "requests_this_month")]
		public int RequestsThisMonth { get; set; }

		/// <summary>
		/// Number of currently connected clients.
		/// </summary>
		[JsonProperty(PropertyName = "connected_clients")]
		public string ConnectedClients { get; set; }

		/// <summary>
		/// Total number of connections received.
		/// </summary>
		[JsonProperty(PropertyName = "total_connections_received")]
		public string TotalConnectionsReceived { get; set; }
	}
}