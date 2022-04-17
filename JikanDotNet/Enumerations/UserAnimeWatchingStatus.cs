namespace JikanDotNet;

/// <summary>
/// Status of anime on user list.
/// </summary>
public enum UserAnimeWatchingStatus
{
    /// <summary>
    /// Watching status.
    /// </summary>
    Watching = 1,

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
    /// Plan to watch status.
    /// </summary>
    PlanToWatch = 6
}