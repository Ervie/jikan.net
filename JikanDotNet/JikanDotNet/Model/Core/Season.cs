using Newtonsoft.Json;
using System.Collections.Generic;

namespace JikanDotNet
{
	/// <summary>
	/// Season model class
	/// </summary>
	public class Season
	{
		/// <summary>
		/// Season entry for season.
		/// </summary>
		[JsonProperty(PropertyName = "anime")]
		public ICollection<AnimeSubEntry> SeasonEntries { get; set; }

		/// <summary>
		/// Year of the season.
		/// </summary>
		[JsonProperty(PropertyName = "season_year")]
		public int SeasonYear { get; set; }

		/// <summary>
		/// Name of the season.
		/// </summary>
		[JsonProperty(PropertyName = "season_name")]
		public string SeasonName { get; set; }
	}
}