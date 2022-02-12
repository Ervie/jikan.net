using System.Text.Json.Serialization;

namespace JikanDotNet
{
	/// <summary>
	/// Broadcast Details
	/// </summary>
	public  class AnimeBroadcast
	{
        /// <summary>Day of the week</summary>
        [JsonPropertyName("day")]
        public string Day { get; set; }

        /// <summary>Time in 24 hour format</summary>
        [JsonPropertyName("time")]
        public string Time { get; set; }

        /// <summary>Timezone (Tz Database format https://en.wikipedia.org/wiki/List_of_tz_database_time_zones)</summary>
        [JsonPropertyName("timezone")]
        public string Timezone { get; set; }

        /// <summary>Raw parsed broadcast string</summary>
        [JsonPropertyName("string")]
        public string String { get; set; }
    }
}