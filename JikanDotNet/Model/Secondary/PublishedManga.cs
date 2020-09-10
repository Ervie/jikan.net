using System.Text.Json.Serialization;

namespace JikanDotNet
{
	/// <summary>
	/// Published manga  model class for person's class.
	/// </summary>
	public class PublishedManga
	{
		/// <summary>
		/// Published manga.
		/// </summary>
		[JsonPropertyName("manga")]
		public MALImageSubItem Manga { get; set; }

		/// <summary>
		/// Position associated with published manga.
		/// </summary>
		[JsonPropertyName("position")]
		public string Position { get; set; }
	}
}