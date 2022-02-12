using System.Text.Json.Serialization;

namespace JikanDotNet
{
	/// <summary>
	/// Model class for promo video.
	/// </summary>
	public class PromoVideo
	{
		/// <summary>
		/// Title of the promotional video.
		/// </summary>
		[JsonPropertyName("title")]
		public string Title { get; set; }

		/// <summary>
		/// Trailer to promo video
		/// </summary>
		[JsonPropertyName("trailer")]
		public AnimeTrailer Trailer { get; set; }
	}
}