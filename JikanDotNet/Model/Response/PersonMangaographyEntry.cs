using System.Text.Json.Serialization;

namespace JikanDotNet
{
	/// <summary>
	/// Model for mangaography entry of a person
	/// </summary>
	public class PersonMangaographyEntry
	{
		/// <summary>
		/// Person's mangaography entry.
		/// </summary>
		[JsonPropertyName("manga")]
		public MalImageSubItem Manga { get; set; }

		/// <summary>
		/// Position of the person in the manga production
		/// </summary>
		[JsonPropertyName("position")]
		public string Position { get; set; }
	}
}