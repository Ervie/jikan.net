using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace JikanDotNet;

/// <summary>
/// Person with full data model class.
/// </summary>
public class PersonFull: Person
{
    /// <summary>
    /// Animeography of person.
    /// </summary>
    [JsonPropertyName("anime")]
    public ICollection<PersonAnimeographyEntry> Animeography { get; set; }
    
    /// <summary>
    /// Mangaography of person.
    /// </summary>
    [JsonPropertyName("manga")]
    public ICollection<PersonMangaographyEntry> Mangaography { get; set; }
    
    /// <summary>
    /// Voice actors of person.
    /// </summary>
    [JsonPropertyName("voices")]
    public ICollection<VoiceActingRole> VoiceActingRoles { get; set; }
}