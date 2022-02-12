using System.ComponentModel;

namespace JikanDotNet;

/// <summary>
/// Properties by which anime search results can be ordered.
/// </summary>
public enum ClubSearchOrderBy
{
    /// <summary>
    /// Does not order results.
    /// </summary>
    [Description("")]
    NoSorting,

    /// <summary>
    /// Orders results by title.
    /// </summary>
    [Description("title")]
    Title,
		
    /// <summary>
    /// Orders results by MalId.
    /// </summary>
    [Description("mal_id")]
    Id,

    /// <summary>
    /// Orders results by created date.
    /// </summary>
    [Description("created")]
    Created,
		
    /// <summary>
    /// Orders results by count of members.
    /// </summary>
    [Description("members_count")]
    MembersCount,
		
    /// <summary>
    /// Orders results by count of pictures.
    /// </summary>
    [Description("pictures_count")]
    PicturesCount
}