using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace JikanDotNet
{
	/// <summary>
	/// Model class for anime/manga staff position.
	/// </summary>
	public class CharacterEntry
	{
		/// <summary>
		/// ID associated with MyAnimeList.
		/// </summary>
		[JsonPropertyName("mal_id")]
		public long MalId { get; set; }

		/// <summary>
		/// Character's name.
		/// </summary>
		[JsonPropertyName("name")]
		public string Name { get; set; }

		/// <summary>
		/// Url to character's main page.
		/// </summary>
		[JsonPropertyName("url")]
		public string Url { get; set; }

		/// <summary>
		/// Character's set of images
		/// </summary>
		[JsonPropertyName("image")]
		public Image ImageURL { get; set; }

		/// <summary>
		/// Character's role (e. g. "main", "supporting")
		/// </summary>
		[JsonPropertyName("role")]
		public string Role { get; set; }

		/// <summary>
		/// Character's list of voice actor in this entry (anime only).
		/// </summary>
		[JsonPropertyName("voice_actors")]
		public ICollection<VoiceActorEntry> VoiceActors { get; set; }

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