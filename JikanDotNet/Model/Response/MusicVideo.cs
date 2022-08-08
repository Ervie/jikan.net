using System.Text.Json.Serialization;

namespace JikanDotNet;

/// <summary>
/// Model class for music video.
/// </summary>
public class MusicVideo
{
    /// <summary>
    /// Title of the version of music video.
    /// </summary>
    [JsonPropertyName("title")]
    public string Title { get; set; }
    
    /// <summary>
    /// Video of the music video.
    /// </summary>
    [JsonPropertyName("video")]
    public AnimeTrailer Video { get; set; }
    
    /// <summary>
    /// Metadata of the music video.
    /// </summary>
    [JsonPropertyName("meta")]
    public MusicVideoMetadata Metadata { get; set; }
}