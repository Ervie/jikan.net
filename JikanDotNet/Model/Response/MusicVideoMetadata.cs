using System.Text.Json.Serialization;

namespace JikanDotNet;

/// <summary>
/// Model class for music video metadata.
/// </summary>
public class MusicVideoMetadata
{
    /// <summary>
    /// Title of the music.
    /// </summary>
    [JsonPropertyName("title")]
    public string Title { get; set; }
    
    /// <summary>
    /// Author of the music.
    /// </summary>
    [JsonPropertyName("author")]
    public string Author { get; set; }
}