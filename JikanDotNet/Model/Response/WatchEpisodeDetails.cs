using System.Text.Json.Serialization;

namespace JikanDotNet
{
    /// <summary>
    /// Model of entry details in recent/popular episodes list.
    /// </summary>
    public class WatchEpisodeDetails
    {
        /// <summary>
        /// ID associated with MyAnimeList.
        /// </summary>
        [JsonPropertyName("mal_id")]
        public long MalId { get; set; }

        /// <summary>
        /// Is episode premium.
        /// </summary>
        [JsonPropertyName("premium")]
        public bool? Premium { get; set; }

        /// <summary>
        /// Episode's title.
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; }

        /// <summary>
        /// Url to sub item main page.
        /// </summary>
        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}