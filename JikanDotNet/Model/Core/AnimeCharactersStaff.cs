using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace JikanDotNet
{
	/// <summary>
	/// Anime's characters and staff model class.
	/// </summary>
	public class AnimeCharactersStaff: BaseJikanRequest
	{
		/// <summary>
		/// Anime's characters collection with basic information.
		/// </summary>
		[JsonPropertyName("characters")]
		public ICollection<CharacterEntry> Characters { get; set; }

		/// <summary>
		/// Anime's staff collection with basic information.
		/// </summary>
		[JsonPropertyName("staff")]
		public ICollection<StaffPositionEntry> Staff { get; set; }
	}
}