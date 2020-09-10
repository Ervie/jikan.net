using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace JikanDotNet
{
	/// <summary>
	/// User's anime list model class.
	/// </summary>
	public class UserAnimeList: BaseJikanRequest
	{
		/// <summary>
		/// Collection of user's anime on their anime list.
		/// </summary>
		[JsonPropertyName("anime")]
		public ICollection<AnimeListEntry> Anime { get; set; }
	}
}