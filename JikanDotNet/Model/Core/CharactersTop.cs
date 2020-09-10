using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace JikanDotNet
{
	/// <summary>
	/// Model class for most popular characters.
	/// </summary>
	public class CharactersTop: BaseJikanRequest
	{
		/// <summary>
		/// Collection of characters entries on top list.
		/// </summary>
		[JsonPropertyName("top")]
		public ICollection<CharacterTopEntry> Top { get; set; }
	}
}