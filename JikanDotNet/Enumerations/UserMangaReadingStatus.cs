namespace JikanDotNet;

/// <summary>
/// Status of manga on user list.
/// </summary>
public enum UserMangaReadingStatus
{
    /// <summary>
    /// Reading status.
    /// </summary>
    Reading = 1,

    /// <summary>
    /// Completed status.
    /// </summary>
    Completed = 2,

    /// <summary>
    /// On hold status.
    /// </summary>
    OnHold = 3,
    
    /// <summary>
    /// Dropped status.
    /// </summary>
    Dropped = 4,
    
    /// <summary>
    /// Plan to read status.
    /// </summary>
    PlanToRead = 6
}