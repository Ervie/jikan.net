using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace JikanDotNet
{
	/// <summary>
	/// Model representing anime openings and endings
	/// </summary>
	public class AnimeThemes
	{
		/// <summary>
		/// Anime openings.
		/// </summary>
		[JsonPropertyName("openings")]
		public ICollection<string> Openings { get; set; }

		/// <summary>
		/// Anime endings.
		/// </summary>
		[JsonPropertyName("endings")]
		public ICollection<string> Endings { get; set; }
	}
}