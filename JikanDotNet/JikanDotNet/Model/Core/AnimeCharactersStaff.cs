using Newtonsoft.Json;
using System.Collections.Generic;

namespace JikanDotNet
{
	/// <summary>
	/// Anime's characters and staff model class.
	/// </summary>
	public class AnimeCharactersStaff
	{
		/// <summary>
		/// Anime's characters collection with basic information.
		/// </summary>
		[JsonProperty(PropertyName = "characters")]
		public ICollection<CharacterEntry> Characters { get; set; }

		/// <summary>
		/// Anime's staff collection with basic information.
		/// </summary>
		[JsonProperty(PropertyName = "staff")]
		public ICollection<StaffPositionEntry> Staff { get; set; }
	}
}