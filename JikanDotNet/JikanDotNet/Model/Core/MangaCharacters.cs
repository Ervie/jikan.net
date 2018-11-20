using Newtonsoft.Json;
using System.Collections.Generic;

namespace JikanDotNet
{
	/// <summary>
	/// Manga characters list model class.
	/// </summary>
	public class MangaCharacters
	{
		/// <summary>
		/// Collection of manga's characters.
		/// </summary>
		[JsonProperty(PropertyName = "characters")]
		public ICollection<CharacterEntry> Characters { get; set; }
	}
}