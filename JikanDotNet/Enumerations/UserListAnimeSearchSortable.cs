using System.ComponentModel;

namespace JikanDotNet
{
	/// <summary>
	/// Properties by which user list anime search results can be ordered.
	/// </summary>
	public enum UserListAnimeSearchSortable
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
		/// Orders results by finish date.
		/// </summary>
		[Description("finish_date")]
		FinishDate,

		/// <summary>
		/// Orders results by start date.
		/// </summary>
		[Description("start_date")]
		StartDate,

		/// <summary>
		/// Orders results by score.
		/// </summary>
		[Description("score")]
		Score,

		/// <summary>
		/// Orders results by last update.
		/// </summary>
		[Description("last_updated")]
		LastUpdated,

		/// <summary>
		/// Orders results by type.
		/// </summary>
		[Description("type")]
		Type,

		/// <summary>
		/// Orders results by rating.
		/// </summary>
		[Description("rated")]
		Rating,

		/// <summary>
		/// Orders results by rewatch.
		/// </summary>
		[Description("rewatch")]
		Rewatch,

		/// <summary>
		/// Orders results by priority.
		/// </summary>
		[Description("priority")]
		Priority,

		/// <summary>
		/// Orders results by progress.
		/// </summary>
		[Description("progress")]
		Progress,

		/// <summary>
		/// Orders results by storage.
		/// </summary>
		[Description("storage")]
		Storage,

		/// <summary>
		/// Orders results by air start.
		/// </summary>
		[Description("air_start")]
		AirStart,

		/// <summary>
		/// Orders results by air end.
		/// </summary>
		[Description("air_end")]
		AirEnd,

		/// <summary>
		/// Orders results by status.
		/// </summary>
		[Description("status")]
		Status
	}
}