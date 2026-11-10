using System.ComponentModel;

namespace Tenrai;

/// <summary>
/// Properties by which character search results can be ordered.
/// </summary>
public enum CharacterSearchOrderBy
{
	/// <summary>
	/// Does not order results.
	/// </summary>
	[Description("")]
	NoSorting,

	/// <summary>
	/// Orders results by MAL id.
	/// </summary>
	[Description("mal_id")]
	MalId,

	/// <summary>
	/// Orders results by name.
	/// </summary>
	[Description("name")]
	Name,

	/// <summary>
	/// Orders results by favorites.
	/// </summary>
	[Description("favorites")]
	Favorites,

	/// <summary>
	/// Orders results by kanji name.
	/// </summary>
	[Description("name_kanji")]
	NameKanji,

	/// <summary>
	/// Orders results by number of anime and manga appearances.
	/// </summary>
	[Description("appearances")]
	Appearances,

	/// <summary>
	/// Orders results by number of anime appearances.
	/// </summary>
	[Description("anime_appearances")]
	AnimeAppearances,

	/// <summary>
	/// Orders results by number of manga appearances.
	/// </summary>
	[Description("manga_appearances")]
	MangaAppearances,

	/// <summary>
	/// Orders results by number of main roles.
	/// </summary>
	[Description("main_roles")]
	MainRoles,

	/// <summary>
	/// Orders results by number of supporting roles.
	/// </summary>
	[Description("supporting_roles")]
	SupportingRoles,

	/// <summary>
	/// Orders results by number of voice cast languages.
	/// </summary>
	[Description("voice_languages")]
	VoiceLanguages,

	/// <summary>
	/// Orders results by number of voice actors.
	/// </summary>
	[Description("voice_actors")]
	VoiceActors,
}