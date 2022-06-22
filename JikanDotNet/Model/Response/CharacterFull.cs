using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace JikanDotNet;

/// <summary>
/// Character with full data model class.
/// </summary>
public class CharacterFull: Character
{
    /// <summary>
    /// Animeography of character.
    /// </summary>
    [JsonPropertyName("anime")]
    public ICollection<CharacterAnimeographyEntry> Animeography { get; set; }
    
    /// <summary>
    /// Mangaography of character.
    /// </summary>
    [JsonPropertyName("manga")]
    public ICollection<CharacterMangaographyEntry> Mangaography { get; set; }
    
    /// <summary>
    /// Voice actors of character.
    /// </summary>
    [JsonPropertyName("voices")]
    public ICollection<VoiceActorEntry> VoiceActors { get; set; }
}