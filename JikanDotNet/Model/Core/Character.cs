using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace JikanDotNet
{
	/// <summary>
	/// Character model class.
	/// </summary>
	public class Character: BaseJikanRequest
	{
		/// <summary>
		/// ID associated with MyAnimeList.
		/// </summary>
		[JsonPropertyName("mal_id")]
		public long MalId { get; set; }

		/// <summary>
		/// Character's canonical link.
		/// </summary>
		[JsonPropertyName("url")]
		public string LinkCanonical { get; set; }

		/// <summary>
		/// Character's name.
		/// </summary>
		[JsonPropertyName("name")]
		public string Name { get; set; }

		/// <summary>
		/// Character's name in kanji.
		/// </summary>
		[JsonPropertyName("name_kanji")]
		public string NameKanji { get; set; }

		/// <summary>
		/// Character's nicknames.
		/// </summary>
		[JsonPropertyName("nicknames")]
		public ICollection<string> Nicknames { get; set; }

		/// <summary>
		/// About character
		/// </summary>
		[JsonPropertyName("about")]
		public string About { get; set; }

		/// <summary>
		/// Character favourite count on MyAnimeList.
		/// </summary>
		[JsonPropertyName("member_favorites")]
		public int? MemberFavorites { get; set; }

		/// <summary>
		/// Character's image URL
		/// </summary>
		[JsonPropertyName("image_url")]
		public string ImageURL { get; set; }

		/// <summary>
		/// Character's animeography.
		/// </summary>
		[JsonPropertyName("animeography")]
		public ICollection<MALImageSubItem> Animeography { get; set; }

		/// <summary>
		/// Character's mangaography.
		/// </summary>
		[JsonPropertyName("mangaography")]
		public ICollection<MALImageSubItem> Mangaography { get; set; }

		/// <summary>
		/// Character's voice actors.
		/// </summary>
		[JsonPropertyName("voice_actors")]
		public ICollection<VoiceActorEntry> VoiceActors { get; set; }
	}
}