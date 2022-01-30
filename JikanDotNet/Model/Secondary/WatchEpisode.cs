using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace JikanDotNet
{
    /// <summary>
    /// Model of entry in recent/popular episodes list.
    /// </summary>
    public class WatchEpisode
    {
        /// <summary>
        /// Is the episode region locked.
        /// </summary>
        [JsonPropertyName("region_locked")]
        public bool? RegionLocked { get; set; }
        
        /// <summary>
        /// Relate anime entry
        /// </summary>
        [JsonPropertyName("entry")]
        public MalImageSubItem Entry { get; set; }
        
        /// <summary>
        /// List of available episodes
        /// </summary>
        [JsonPropertyName("episodes")]
        public ICollection<WatchEpisodeDetails> Episodes { get; set; }
    }
}