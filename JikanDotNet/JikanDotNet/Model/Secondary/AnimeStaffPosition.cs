using Newtonsoft.Json;

namespace JikanDotNet
{
	/// <summary>
	/// Model class for anime staff position.
	/// </summary>
	public class AnimeStaffPosition
	{
		/// <summary>
		/// Anime associated with staff position.
		/// </summary>
		[JsonProperty(PropertyName = "anime")]
		public MALImageSubItem Anime { get; set; }

		/// <summary>
		/// Role associated with staff position.
		/// </summary>
		[JsonProperty(PropertyName = "role")]
		public string Role { get; set; }
	}
}