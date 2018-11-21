using Newtonsoft.Json;
using System;
using System.Collections.Generic;

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
		[JsonProperty(PropertyName = "year")]
		public int Year { get; set; }

		/// <summary>
		/// Collection of available seasons in year to query.
		/// </summary>
		[JsonProperty(PropertyName = "seasons")]
		public ICollection<Seasons> Season { get; set; }
	}
}