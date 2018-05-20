using Newtonsoft.Json;
using System.Collections.Generic;

namespace JikanDotNet
{
	/// <summary>
	/// Person model class.
	/// </summary>
	public class Person
	{
		#region Basic request props

		/// <summary>
		/// ID associated with MyAnimeList.
		/// </summary>
		[JsonProperty(PropertyName = "mal_id")]
		public long MalId { get; set; }

		/// <summary>
		/// Person's canonical link.
		/// </summary>
		[JsonProperty(PropertyName = "link_canonical")]
		public string LinkCanonical { get; set; }

		/// <summary>
		/// Person's name.
		/// </summary>
		[JsonProperty(PropertyName = "name")]
		public string Name { get; set; }

		/// <summary>
		/// Person's given name..
		/// </summary>
		[JsonProperty(PropertyName = "given_name")]
		public string GivenName { get; set; }

		/// <summary>
		/// Person's family name.
		/// </summary>
		[JsonProperty(PropertyName = "family_name")]
		public string FamilyName { get; set; }

		/// <summary>
		/// Person's alternate names.
		/// </summary>
		[JsonProperty(PropertyName = "alternate_name")]
		public ICollection<string> AlternativeName { get; set; }

		/// <summary>
		/// Person's birthday.
		/// </summary>
		[JsonProperty(PropertyName = "birthday")]
		public string Birthday { get; set; }

		/// <summary>
		/// Person's website URL.
		/// </summary>
		[JsonProperty(PropertyName = "website_url")]
		public string WebsiteUrl { get; set; }

		/// <summary>
		/// Person's favourite count on MyAnimeList.
		/// </summary>
		[JsonProperty(PropertyName = "member_favorites")]
		public int? MemberFavorites { get; set; }

		/// <summary>
		/// More information about person.
		/// </summary>
		[JsonProperty(PropertyName = "more")]
		public string More { get; set; }

		/// <summary>
		/// Person's image URL
		/// </summary>
		[JsonProperty(PropertyName = "image_url")]
		public string ImageURL { get; set; }

		/// <summary>
		/// Person's voice acting roles.
		/// </summary>
		[JsonProperty(PropertyName = "voice_acting_role")]
		public ICollection<VoiceActingRole> VoiceActingRoles { get; set; }

		/// <summary>
		/// Person's anime staff positions.
		/// </summary>
		[JsonProperty(PropertyName = "anime_staff_position")]
		public ICollection<AnimeStaffPosition> AnimeStaffPositions { get; set; }

		/// <summary>
		/// Person's published manga.
		/// </summary>
		[JsonProperty(PropertyName = "published_manga")]
		public ICollection<PublishedManga> PublishedManga { get; set; }

		#endregion Basic request props

		#region Pictures request props

		/// <summary>
		/// Person's extra image URLs.
		/// </summary>
		[JsonProperty(PropertyName = "image")]
		public ICollection<string> Images { get; set; }

		#endregion Pictures request props
	}
}