using System.Text.Json.Serialization;

namespace JikanDotNet
{
	/// <summary>
	/// Model class representing voice actor entry on Character's page.
	/// </summary>
	public class VoiceActorEntry
	{
		/// <summary>
		/// ID associated with MyAnimeList.
		/// </summary>
		[JsonPropertyName("mal_id")]
		public long MalId { get; set; }

		/// <summary>
		/// Voice actor's name.
		/// </summary>
		[JsonPropertyName("name")]
		public string Name { get; set; }

		/// <summary>
		/// Url to voice actor main page.
		/// </summary>
		[JsonPropertyName("url")]
		public string Url { get; set; }

		/// <summary>
		/// Voice actor's image URL
		/// </summary>
		[JsonPropertyName("image_url")]
		public Picture ImageURL { get; set; }

		/// <summary>
		/// Voice actor's language.
		/// </summary>
		[JsonPropertyName("language")]
		public string Language { get; set; }

		/// <summary>
		/// Overriden ToString method.
		/// </summary>
		/// <returns>Name if not null, base method elsewhere.</returns>
		public override string ToString()
		{
			return Name ?? base.ToString();
		}
	}
}