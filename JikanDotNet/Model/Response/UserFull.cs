using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace JikanDotNet;

/// <summary>
/// User with full data model class.
/// </summary>
public class UserFull: UserProfile
{
    /// <summary>
    /// User's anime and manga statistics
    /// </summary>
    [JsonPropertyName("statistics")]
    public UserStatistics Statistics { get; set; }
    
    /// <summary>
    /// User's favorites
    /// </summary>
    [JsonPropertyName("favorites")]
    public UserFavorites Favorites { get; set; }
    
    /// <summary>
    /// User's anime and manga updates
    /// </summary>
    [JsonPropertyName("updates")]
    public UserUpdates Updates { get; set; }
    
    /// <summary>
    /// User's about
    /// </summary>
    [JsonPropertyName("about")]
    public string About { get; set; }
    
    /// <summary>
    /// User's external links
    /// </summary>
    [JsonPropertyName("external")]
    public ICollection<ExternalLink> External { get; set; }
}