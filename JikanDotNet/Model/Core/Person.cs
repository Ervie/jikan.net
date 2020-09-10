using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;

namespace JikanDotNet
{
	/// <summary>
	/// Person model class.
	/// </summary>
	public class Person: BaseJikanRequest,  IMalEntity
	{
		/// <summary>
		/// ID associated with MyAnimeList.
		/// </summary>
		[JsonPropertyName("mal_id")]
		public long MalId { get; set; }

		/// <summary>
		/// Person's canonical link.
		/// </summary>
		[JsonPropertyName("url")]
		public string LinkCanonical { get; set; }

		/// <summary>
		/// Person's name.
		/// </summary>
		[JsonPropertyName("name")]
		public string Name { get; set; }

		/// <summary>
		/// Person's given name..
		/// </summary>
		[JsonPropertyName("given_name")]
		public string GivenName { get; set; }

		/// <summary>
		/// Person's family name.
		/// </summary>
		[JsonPropertyName("family_name")]
		public string FamilyName { get; set; }

		/// <summary>
		/// Person's alternate names.
		/// </summary>
		[JsonPropertyName("alternate_name")]
		public ICollection<string> AlternativeName { get; set; }

		/// <summary>
		/// Person's birthday.
		/// </summary>
		[JsonPropertyName("birthday")]
		public DateTime? Birthday { get; set; }

		/// <summary>
		/// Person's website URL.
		/// </summary>
		[JsonPropertyName("website_url")]
		public string WebsiteUrl { get; set; }

		/// <summary>
		/// Person's favourite count on MyAnimeList.
		/// </summary>
		[JsonPropertyName("member_favorites")]
		public int? MemberFavorites { get; set; }

		/// <summary>
		/// More information about person.
		/// </summary>
		[JsonPropertyName("about")]
		public string More { get; set; }

		/// <summary>
		/// Person's image URL
		/// </summary>
		[JsonPropertyName("image_url")]
		public string ImageURL { get; set; }

		/// <summary>
		/// Person's voice acting roles.
		/// </summary>
		[JsonPropertyName("voice_acting_roles")]
		public ICollection<VoiceActingRole> VoiceActingRoles { get; set; }

		/// <summary>
		/// Person's anime staff positions.
		/// </summary>
		[JsonPropertyName("anime_staff_positions")]
		public ICollection<AnimeStaffPosition> AnimeStaffPositions { get; set; }

		/// <summary>
		/// Person's published manga.
		/// </summary>
		[JsonPropertyName("published_manga")]
		public ICollection<PublishedManga> PublishedManga { get; set; }
	}
}