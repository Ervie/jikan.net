using System.ComponentModel;

namespace JikanDotNet
{
	/// <summary>
	/// Status value for user list anime filtering.
	/// </summary>
	public enum UserListAnimeAiringStatus
	{
		/// <summary>
		/// Allow all statuses to be displayed in results.
		/// </summary>
		[Description("")]
		NoFilter,

		/// <summary>
		/// Airing status.
		/// </summary>
		[Description("airing")]
		Airing,

		/// <summary>
		/// Finished status.
		/// </summary>
		[Description("finished")]
		Finished,

		/// <summary>
		/// To be aired status.
		/// </summary>
		[Description("to_be_aired")]
		ToBeAired
	}
}