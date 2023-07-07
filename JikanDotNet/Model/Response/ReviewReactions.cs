using System.Text.Json.Serialization;

/// <summary>
/// Reactions from users on the Review.
/// </summary>
public class ReviewReactions
{
    /// <summary>
    /// Sum of all reactions.
    /// </summary>
    [JsonPropertyName("overall")]
    public int TotalReactions { get; set; }

    /// <summary>
    /// Count of Nice Reactions.
    /// </summary>
    [JsonPropertyName("nice")]
    public int Nice { get; set; }

    /// <summary>
    /// Count of Love It Reactions.
    /// </summary>
    [JsonPropertyName("love_it")]
    public int LoveIt { get; set; }
    
    /// <summary>
    /// Count of Funny Reactions.
    /// </summary>
    [JsonPropertyName("funny")]
    public int Funny { get; set; }

    /// <summary>
    /// Count of Confusing Reactions.
    /// </summary>
    [JsonPropertyName("confusing")]
    public int Confusing { get; set; }
		
    /// <summary>
    /// Count of Informative Reactions.
    /// </summary>
    [JsonPropertyName("informative")]
    public int Informative { get; set; }

    /// <summary>
    /// Count of Well Written Reactions.
    /// </summary>
    [JsonPropertyName("well_written")]
    public int WellWritten { get; set; }
    
    /// <summary>
    /// Count of Creative Reactions.
    /// </summary>
    [JsonPropertyName("creative")]
    public int Creative { get; set; }
}