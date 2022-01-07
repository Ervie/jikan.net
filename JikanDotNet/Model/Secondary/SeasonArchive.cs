using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace JikanDotNet
{
	/// <summary>
	/// Seasons archive model class
	/// </summary>
	public class SeasonArchive
	{
		/// <summary>
		/// Available year to query.
		/// </summary>
		[JsonPropertyName("year")]
		public int Year { get; set; }

		/// <summary>
		/// Collection of available seasons in year to query.
		/// </summary>
		[JsonPropertyName("seasons")]
		public ICollection<AnimeSeason> Season { get; set; }
	}
}