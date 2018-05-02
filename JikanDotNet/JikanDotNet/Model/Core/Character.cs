using Newtonsoft.Json;
using System.Collections.Generic;

namespace JikanDotNet
{
	/// <summary>
	/// Character model class.
	/// </summary>
	public class Character
	{
		/// <summary>
		/// ID associated with MyAnimeList.
		/// </summary>
		[JsonProperty(PropertyName = "mal_id")]
		public long MalId { get; set; }

		/// <summary>
		/// Character's canonical link.
		/// </summary>
		[JsonProperty(PropertyName = "link_canonical")]
		public string LinkCanonical { get; set; }

		/// <summary>
		/// Character's name.
		/// </summary>
		[JsonProperty(PropertyName = "name")]
		public string Name { get; set; }

		/// <summary>
		/// Character's name in kanji.
		/// </summary>
		[JsonProperty(PropertyName = "name_kanji")]
		public string NameKanji { get; set; }

		/// <summary>
		/// Character's nicknames.
		/// </summary>
		[JsonProperty(PropertyName = "nicknames")]
		public string Nicknames { get; set; }

		/// <summary>
		/// About character
		/// </summary>
		[JsonProperty(PropertyName = "About")]
		public string About { get; set; }

		/// <summary>
		/// Character favourite count on MyAnimeList.
		/// </summary>
		[JsonProperty(PropertyName = "member_favorites")]
		public int? MemberFavorites { get; set; }

		/// <summary>
		/// Character's image URL
		/// </summary>
		[JsonProperty(PropertyName = "image_url")]
		public string ImageURL { get; set; }

		/// <summary>
		/// Character's animeography.
		/// </summary>
		[JsonProperty(PropertyName = "animeography")]
		public ICollection<MALImageSubItem> Animeography { get; set; }

		/// <summary>
		/// Character's mangaography.
		/// </summary>
		[JsonProperty(PropertyName = "mangaography")]
		public ICollection<MALImageSubItem> Mangaography { get; set; }

		/// <summary>
		/// Character's voice actors.
		/// </summary>
		[JsonProperty(PropertyName = "voice_actor")]
		public ICollection<VoiceActorEntry> VoiceActors { get; set; }
	}
}