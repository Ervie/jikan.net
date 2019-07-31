using System.ComponentModel;

namespace JikanDotNet
{
	/// <summary>
	/// Status value for user list manga filtering.
	/// </summary>
	public enum UserListMangaPublishingStatus
	{
		/// <summary>
		/// Allow all statuses to be displayed in results.
		/// </summary>
		[Description("")]
		NoFilter,

		/// <summary>
		/// Publishing status.
		/// </summary>
		[Description("publishing")]
		Publishing,

		/// <summary>
		/// Finished status.
		/// </summary>
		[Description("finished")]
		Finished,

		/// <summary>
		/// To be published status.
		/// </summary>
		[Description("to_be_published")]
		ToBePublished
	}
}
