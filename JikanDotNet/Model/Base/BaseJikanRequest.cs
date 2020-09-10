using System.Text.Json.Serialization;

namespace JikanDotNet
{
	/// <summary>
	/// Base model class for each Jikan request model, containing information about cache.
	/// </summary>
	public class BaseJikanRequest
	{
		/// <summary>
		/// Hash of the request.
		/// </summary>
		[JsonPropertyName("request_hash")]
		public string RequestHash { get; set; }

		/// <summary>
		/// Is request cached.
		/// </summary>
		[JsonPropertyName("request_cached")]
		public bool RequestCached { get; set; }

		/// <summary>
		/// Time till cache expiration (in seconds).
		/// </summary>
		[JsonPropertyName("request_cache_expiry")]
		public int RequestCacheExpiry { get; set; }
	}
}