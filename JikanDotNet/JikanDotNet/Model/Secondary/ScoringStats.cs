using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace JikanDotNet
{
	/// <summary>
	/// Model class representing distribution of score on manga/anime.
	/// </summary>
	public class ScoringStats
    {
		/// <summary>
		/// Statistics for "1" score.
		/// </summary>
		[JsonProperty(PropertyName = "1")]
		public ScoringStatEntry _1 { get; set; }

		/// <summary>
		/// Statistics for "2" score.
		/// </summary>
		[JsonProperty(PropertyName = "2")]
		public ScoringStatEntry _2 { get; set; }

		/// <summary>
		/// Statistics for "3" score.
		/// </summary>
		[JsonProperty(PropertyName = "3")]
		public ScoringStatEntry _3 { get; set; }

		/// <summary>
		/// Statistics for "4" score.
		/// </summary>
		[JsonProperty(PropertyName = "4")]
		public ScoringStatEntry _4 { get; set; }

		/// <summary>
		/// Statistics for "5" score.
		/// </summary>
		[JsonProperty(PropertyName = "5")]
		public ScoringStatEntry _5 { get; set; }

		/// <summary>
		/// Statistics for "6" score.
		/// </summary>
		[JsonProperty(PropertyName = "6")]
		public ScoringStatEntry _6 { get; set; }

		/// <summary>
		/// Statistics for "7" score.
		/// </summary>
		[JsonProperty(PropertyName = "7")]
		public ScoringStatEntry _7 { get; set; }

		/// <summary>
		/// Statistics for "8" score.
		/// </summary>
		[JsonProperty(PropertyName = "8")]
		public ScoringStatEntry _8 { get; set; }

		/// <summary>
		/// Statistics for "9" score.
		/// </summary>
		[JsonProperty(PropertyName = "9")]
		public ScoringStatEntry _9 { get; set; }

		/// <summary>
		/// Statistics for "10" score.
		/// </summary>
		[JsonProperty(PropertyName = "10")]
		public ScoringStatEntry _10 { get; set; }
	}
}
