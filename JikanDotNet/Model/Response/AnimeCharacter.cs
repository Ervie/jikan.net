using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace JikanDotNet
{
	/// <summary>
	/// Model class for anime character entry.
	/// </summary>
	public class AnimeCharacter
	{
		/// <summary>
		/// Character details
		/// </summary>
		[JsonPropertyName("character")]
		public CharacterEntry Character { get; set; }

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
	}
}