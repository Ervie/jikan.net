using Newtonsoft.Json;

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
		[JsonProperty(PropertyName = "request_hash")]
		public string RequestHash { get; set; }

		/// <summary>
		/// Is request cached.
		/// </summary>
		[JsonProperty(PropertyName = "request_cached")]
		public bool RequestCached { get; set; }

		/// <summary>
		/// Time till cache expiration (in seconds).
		/// </summary>
		[JsonProperty(PropertyName = "request_cache_expiry")]
		public int RequestCacheExpiry { get; set; }
	}
}