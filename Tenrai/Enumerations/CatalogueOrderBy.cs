using System.ComponentModel;

namespace Tenrai;

/// <summary>
/// Sort field for the unapproved and denied anime and manga lists.
/// </summary>
public enum CatalogueOrderBy
{
	/// <summary>
	/// Leave the order to the API.
	/// </summary>
	[Description("")]
	NoSorting,

	/// <summary>
	/// MyAnimeList id.
	/// </summary>
	[Description("mal_id")]
	MalId,

	/// <summary>
	/// Title.
	/// </summary>
	[Description("title")]
	Title
}
