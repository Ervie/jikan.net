using System.Text.Json.Serialization;
using System;

/// <summary>
/// Model class for review.
/// </summary>
public class ReviewScores
{
    /// <summary>
    /// Overall score.
    /// </summary>
    [JsonPropertyName("overall")]
    public int? Overall { get; set; }

    /// <summary>
    /// Score for story.
    /// </summary>
    [JsonPropertyName("story")]
    public int? Story { get; set; }

    /// <summary>
    /// Score for art (manga review only).
    /// </summary>
    [JsonPropertyName("art")]
    public int? Art { get; set; }
    
    /// <summary>
    /// Score for animation (anime review only).
    /// </summary>
    [JsonPropertyName("animation")]
    public int? Animation { get; set; }

    /// <summary>
    /// Score for sound (anime review only).
    /// </summary>
    [JsonPropertyName("sound")]
    public int? Sound { get; set; }
		
    /// <summary>
    /// Score for characters.
    /// </summary>
    [JsonPropertyName("character")]
    public int? Character { get; set; }

    /// <summary>
    /// Score for enjoyment.
    /// </summary>
    [JsonPropertyName("enjoyment")]
    public int? Enjoyment { get; set; }
}