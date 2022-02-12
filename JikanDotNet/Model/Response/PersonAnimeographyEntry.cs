using System.Text.Json.Serialization;

namespace JikanDotNet
{
	/// <summary>
	/// Model for animeography entry of a person
	/// </summary>
	public class PersonAnimeographyEntry
	{
		/// <summary>
		/// Person's animeography entry.
		/// </summary>
		[JsonPropertyName("anime")]
		public MalImageSubItem Anime { get; set; }

		/// <summary>
		/// Position of the person in the anime production
		/// </summary>
		[JsonPropertyName("position")]
		public string Position { get; set; }
	}
}