using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;

namespace JikanDotNet
{
	/// <summary>
	/// Seasons archive collection model class
	/// </summary>
	public class SeasonArchives: BaseJikanRequest
	{
		/// <summary>
		/// Season entry for season.
		/// </summary>
		[JsonPropertyName("archive")]
		public ICollection<SeasonArchive> Archives { get; set; }
	}
}