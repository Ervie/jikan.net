using System.Text.Json.Serialization;

namespace JikanDotNet
{
	/// <summary>
	/// Model class representing voice actor entry on Character's page.
	/// </summary>
	public class VoiceActorEntry
	{
		/// <summary>
		/// Voice actor's language.
		/// </summary>
		[JsonPropertyName("language")]
		public string Language { get; set; }

		/// <summary>
		/// Voice actor's details.
		/// </summary>
		[JsonPropertyName("person")]
		public PersonEntry Person { get; set; }
	}
}