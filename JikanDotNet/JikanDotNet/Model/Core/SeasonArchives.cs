using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace JikanDotNet
{
	/// <summary>
	/// Seasons archive collection model class
	/// </summary>
	public class SeasonArchives
	{
		/// <summary>
		/// Season entry for season.
		/// </summary>
		[JsonProperty(PropertyName = "archive")]
		public ICollection<SeasonArchive> Archives { get; set; }
	}
}