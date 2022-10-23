using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace JikanDotNet;

/// <summary>
/// Collection of user updates model class.
/// </summary>
public class UserUpdates
{
    /// <summary>
    /// Anime updates.
    /// </summary>
    [JsonPropertyName("anime")]
    public ICollection<AnimeUserUpdate> AnimeUpdates { get; set; }

    /// <summary>
    /// Manga updates.
    /// </summary>
    [JsonPropertyName("manga")]
    public ICollection<MangaUserUpdate> MangaUpdates { get; set; }
}