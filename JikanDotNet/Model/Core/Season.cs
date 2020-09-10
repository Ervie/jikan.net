using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace JikanDotNet
{
	/// <summary>
	/// Season model class
	/// </summary>
	public class Season: BaseJikanRequest
	{
		/// <summary>
		/// Season entry for season.
		/// </summary>
		[JsonPropertyName("anime")]
		public ICollection<AnimeSubEntry> SeasonEntries { get; set; }

		/// <summary>
		/// Year of the season.
		/// </summary>
		[JsonPropertyName("season_year")]
		public int? SeasonYear { get; set; }

		/// <summary>
		/// Name of the season.
		/// </summary>
		[JsonPropertyName("season_name")]
		public string SeasonName { get; set; }
	}
}