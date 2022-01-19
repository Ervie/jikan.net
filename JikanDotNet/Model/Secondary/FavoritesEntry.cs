using System.Text.Json.Serialization;

namespace JikanDotNet
{
	/// <summary>
	/// Model with extended data for favorites section in user profile.
	/// </summary>
	public class FavoritesEntry : MalImageSubItem
	{
		/// <summary>
		/// Type of resource
		/// </summary>
		[JsonPropertyName("type")]
		public string Type { get; set; }

		/// <summary>
		/// Year when manga was published or anime started airing
		/// </summary>
		[JsonPropertyName("start_year")]
		public int? StartYear { get; set; }
	}
}