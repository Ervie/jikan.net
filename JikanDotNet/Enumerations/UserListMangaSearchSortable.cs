using System.ComponentModel;

namespace JikanDotNet
{
	/// <summary>
	/// Properties by which user list manga search results can be ordered.
	/// </summary>
	public enum UserListMangaSearchSortable
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
		/// Orders results by priority.
		/// </summary>
		[Description("priority")]
		Priority,

		/// <summary>
		/// Orders results by progress (chapters read).
		/// </summary>
		[Description("chapters_read")]
		ChaptersRead,

		/// <summary>
		/// Orders results by progress (volumes read).
		/// </summary>
		[Description("volumes_read")]
		VolumesRead,

		/// <summary>
		/// Orders results by publish start.
		/// </summary>
		[Description("publish_start")]
		PublishStart,

		/// <summary>
		/// Orders results by publish end.
		/// </summary>
		[Description("publish_end")]
		PublishEnd,

		/// <summary>
		/// Orders results by status.
		/// </summary>
		[Description("status")]
		Status
	}
}