using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;

namespace JikanDotNet
{
	/// <summary>
	/// Person model class.
	/// </summary>
	public class Person: BaseJikanRequest
	{
		/// <summary>
		/// ID associated with MyAnimeList.
		/// </summary>
		[JsonPropertyName("mal_id")]
		public long MalId { get; set; }

		/// <summary>
		/// Person's url.
		/// </summary>
		[JsonPropertyName("url")]
		public string Url { get; set; }

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
		[JsonPropertyName("alternate_names")]
		public ICollection<string> AlternativeNames { get; set; }

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
		[JsonPropertyName("favorites")]
		public int? MemberFavorites { get; set; }

		/// <summary>
		/// More information about person.
		/// </summary>
		[JsonPropertyName("about")]
		public string About { get; set; }

		/// <summary>
		/// Person's image set
		/// </summary>
		[JsonPropertyName("images")]
		public ImagesSet Images { get; set; }

		/// <summary>
		/// Person's voice acting roles.
		/// </summary>
		[JsonPropertyName("voice_acting_roles")]
		public ICollection<VoiceActingRole> VoiceActingRoles { get; set; }
	}
}