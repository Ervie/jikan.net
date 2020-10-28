using System.Text.Json.Serialization;

namespace JikanDotNet
{
	/// <summary>
	/// Anime Crunchyroll video model class.
	/// </summary>
	public class AnimeCrunchyrollVideo
	{
		/// <summary>
		/// ID associated with Crunchyroll.
		/// </summary>
		[JsonPropertyName("id")]
		public long Id { get; set; }

		/// <summary>
		/// URL to the video.
		/// </summary>
		[JsonPropertyName("url")]
		public string Url { get; set; }
	}
}