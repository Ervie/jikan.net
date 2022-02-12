using System.ComponentModel;

namespace JikanDotNet
{
	/// <summary>
	/// Enumeration for club categories (search config).
	/// </summary>
	public enum ClubCategory
	{
		/// <summary>
		/// Allow all categories to be displayed in results.
		/// </summary>
		[Description("")]
		EveryCategory,

		/// <summary>
		/// Anime category
		/// </summary>
		[Description("anim")]
		Anime,

		/// <summary>
		/// Manga category
		/// </summary>
		[Description("manga")]
		Manga,

		/// <summary>
		/// Actors and artists category
		/// </summary>
		[Description("actors_and_artists")]
		ActorsAndArtists,

		/// <summary>
		/// Characters category
		/// </summary>
		[Description("characters")]
		Characters,

		/// <summary>
		/// Cities and neighbourhoods category
		/// </summary>
		[Description("cities_and_neighbourhoods")]
		CitiesAndNeighbourhoods,

		/// <summary>
		/// Companies category
		/// </summary>
		[Description("companies")]
		Companies,

		/// <summary>
		/// Conventions category
		/// </summary>
		[Description("conventions")]
		Conventions,

		/// <summary>
		/// Games category
		/// </summary>
		[Description("games")]
		Games,

		/// <summary>
		/// Japan category
		/// </summary>
		[Description("japan")]
		Japan,

		/// <summary>
		/// Music category
		/// </summary>
		[Description("music")]
		Music,

		/// <summary>
		/// Schools category
		/// </summary>
		[Description("shools")]
		Schools,

		/// <summary>
		/// Other category
		/// </summary>
		[Description("other")]
		Other,
	}
}