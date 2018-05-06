using Newtonsoft.Json;
using System;
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
		[JsonProperty(PropertyName = "season")]
		public ICollection<SeasonEntry> SeasonEntries { get; set; }
	}
}