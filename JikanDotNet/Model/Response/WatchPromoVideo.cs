using System.Text.Json.Serialization;

namespace JikanDotNet
{
    /// <summary>
    /// Model of entry in recent/popular episodes list.
    /// </summary>
    public class WatchPromoVideo: PromoVideo
    {
        /// <summary>
        /// Related anime entry
        /// </summary>
        [JsonPropertyName("entry")]
        public MalImageSubItem Entry { get; set; }
    }
}