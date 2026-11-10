using System.Collections.Generic;
using Tenrai.Helpers;
using Tenrai.Interfaces;

namespace Tenrai;

/// <summary>
/// Model class of search configuration for advanced character search.
/// </summary>
public class CharacterSearchConfig: ISearchConfig, ISfwConfig
{
	/// <summary>
	/// Index of page folding 50 records of top ranging (e.g. 1 will return first 50 records, 2 will return record from 51 to 100 etc.)
	/// </summary>
	public int? Page { get; set; }

	/// <summary>
	/// Size of the page (100 is the max).
	/// </summary>
	public int? Limit { get; set; }

	/// <summary>
	/// Search query.
	/// </summary>
	public string Query { get; set; }

	/// <summary>
	/// Return entries starting with the given letter.
	/// </summary>
	public char? Letter { get; set; }

	/// <summary>
	/// Given name to match.
	/// </summary>
	public string GivenName { get; set; }

	/// <summary>
	/// Family name to match.
	/// </summary>
	public string FamilyName { get; set; }

	/// <summary>
	/// Whether the character has an about text.
	/// </summary>
	public bool? HasAbout { get; set; }

	/// <summary>
	/// Whether the character has an image.
	/// </summary>
	public bool? HasImage { get; set; }

	/// <summary>
	/// Whether the character has nicknames.
	/// </summary>
	public bool? HasNicknames { get; set; }

	/// <summary>
	/// Whether the character has a kanji name.
	/// </summary>
	public bool? HasKanji { get; set; }

	/// <summary>
	/// Whether the character has a family name.
	/// </summary>
	public bool? HasFamilyName { get; set; }

	/// <summary>
	/// Characters in any of these anime.
	/// </summary>
	public ICollection<long> AnimeIds { get; set; } = new List<long>();

	/// <summary>
	/// Characters in any of these manga.
	/// </summary>
	public ICollection<long> MangaIds { get; set; } = new List<long>();

	/// <summary>
	/// Role the character has: in <see cref="AnimeIds"/>/<see cref="MangaIds"/> when set, otherwise in any anime or manga.
	/// </summary>
	public CharacterRole? Role { get; set; }

	/// <summary>
	/// Characters only ever listed in this role, never in the other.
	/// </summary>
	public CharacterRole? RoleOnly { get; set; }

	/// <summary>
	/// Kinds of entries the character appears in.
	/// </summary>
	public AppearsIn? AppearsIn { get; set; }

	/// <summary>
	/// True for characters in at least one anime airing now, false for characters in none.
	/// </summary>
	public bool? Airing { get; set; }

	/// <summary>
	/// Voice actor person ids.
	/// </summary>
	public ICollection<long> VoiceActorIds { get; set; } = new List<long>();

	/// <summary>
	/// Characters voiced in any of these languages.
	/// </summary>
	public ICollection<VoiceLanguage> VoiceLanguages { get; set; } = new List<VoiceLanguage>();

	/// <summary>
	/// Characters voiced in any of these languages are left out.
	/// </summary>
	public ICollection<VoiceLanguage> ExcludedVoiceLanguages { get; set; } = new List<VoiceLanguage>();

	/// <summary>
	/// Whether the character has a credited voice actor.
	/// </summary>
	public bool? HasVoiceActor { get; set; }

	/// <summary>
	/// Lowest number of favorites.
	/// </summary>
	public int? MinimumFavorites { get; set; }

	/// <summary>
	/// Highest number of favorites.
	/// </summary>
	public int? MaximumFavorites { get; set; }

	/// <summary>
	/// Lowest number of anime and manga appearances.
	/// </summary>
	public int? MinimumAppearances { get; set; }

	/// <summary>
	/// Highest number of anime and manga appearances.
	/// </summary>
	public int? MaximumAppearances { get; set; }

	/// <summary>
	/// Lowest number of anime appearances.
	/// </summary>
	public int? MinimumAnimeAppearances { get; set; }

	/// <summary>
	/// Highest number of anime appearances.
	/// </summary>
	public int? MaximumAnimeAppearances { get; set; }

	/// <summary>
	/// Lowest number of manga appearances.
	/// </summary>
	public int? MinimumMangaAppearances { get; set; }

	/// <summary>
	/// Highest number of manga appearances.
	/// </summary>
	public int? MaximumMangaAppearances { get; set; }

	/// <summary>
	/// Lowest number of main roles.
	/// </summary>
	public int? MinimumMainRoles { get; set; }

	/// <summary>
	/// Highest number of main roles.
	/// </summary>
	public int? MaximumMainRoles { get; set; }

	/// <summary>
	/// Lowest number of supporting roles.
	/// </summary>
	public int? MinimumSupportingRoles { get; set; }

	/// <summary>
	/// Highest number of supporting roles.
	/// </summary>
	public int? MaximumSupportingRoles { get; set; }

	/// <summary>
	/// Lowest number of voice cast languages.
	/// </summary>
	public int? MinimumVoiceLanguages { get; set; }

	/// <summary>
	/// Highest number of voice cast languages.
	/// </summary>
	public int? MaximumVoiceLanguages { get; set; }

	/// <summary>
	/// Lowest number of voice actors.
	/// </summary>
	public int? MinimumVoiceActors { get; set; }

	/// <summary>
	/// Highest number of voice actors.
	/// </summary>
	public int? MaximumVoiceActors { get; set; }

	/// <summary>
	/// Filter out adult entries. Null uses the client setting (<see cref="Config.TenraiClientConfiguration.Sfw"/>).
	/// </summary>
	public bool? Sfw { get; set; }

	/// <summary>
	/// Also filter out ecchi entries, on top of <see cref="Sfw"/>. Null uses the client setting.
	/// </summary>
	public bool? SfwStrict { get; set; }

	/// <summary>
	/// Select order by property.
	/// </summary>
	public CharacterSearchOrderBy OrderBy { get; set; }

	/// <summary>
	/// Define sort direction for <see cref="OrderBy">OrderBy</see> property.
	/// </summary>
	public SortDirection SortDirection { get; set; }

	/// <summary>
	/// Create query from current parameters for search request.
	/// </summary>
	/// <returns>Query from current parameters for search request</returns>
	public string ConfigToString()
	{
		if (Letter.HasValue)
		{
			Guard.IsLetter(Letter.Value, nameof(Letter));
		}

		var query = new QueryBuilder()
			.Page(Page)
			.Limit(Limit)
			.Add("q", Query)
			.Add("letter", Letter?.ToString())
			.Add("given_name", GivenName)
			.Add("family_name", FamilyName)
			.Add("has_about", HasAbout)
			.Add("has_image", HasImage)
			.Add("has_nicknames", HasNicknames)
			.Add("has_kanji", HasKanji)
			.Add("has_family_name", HasFamilyName)
			.AddList("anime", AnimeIds)
			.AddList("manga", MangaIds)
			.Add("role", Role)
			.Add("role_only", RoleOnly)
			.Add("appears_in", AppearsIn)
			.Add("airing", Airing)
			.AddList("voice_actors", VoiceActorIds)
			.AddList("voice_language", VoiceLanguages)
			.AddList("voice_language_exclude", ExcludedVoiceLanguages)
			.Add("has_voice_actor", HasVoiceActor)
			.Add("min_favorites", MinimumFavorites)
			.Add("max_favorites", MaximumFavorites)
			.Add("min_appearances", MinimumAppearances)
			.Add("max_appearances", MaximumAppearances)
			.Add("min_anime_appearances", MinimumAnimeAppearances)
			.Add("max_anime_appearances", MaximumAnimeAppearances)
			.Add("min_manga_appearances", MinimumMangaAppearances)
			.Add("max_manga_appearances", MaximumMangaAppearances)
			.Add("min_main_roles", MinimumMainRoles)
			.Add("max_main_roles", MaximumMainRoles)
			.Add("min_supporting_roles", MinimumSupportingRoles)
			.Add("max_supporting_roles", MaximumSupportingRoles)
			.Add("min_voice_languages", MinimumVoiceLanguages)
			.Add("max_voice_languages", MaximumVoiceLanguages)
			.Add("min_voice_actors", MinimumVoiceActors)
			.Add("max_voice_actors", MaximumVoiceActors)
			.Sfw(Sfw, SfwStrict);

		if (OrderBy != CharacterSearchOrderBy.NoSorting)
		{
			query.Add("order_by", OrderBy).Add("sort", SortDirection);
		}

		return query.ToString();
	}
}
