using System.ComponentModel;

namespace JikanDotNet;

/// <summary>
/// Properties by which anime search results can be ordered.
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
}