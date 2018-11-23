using System.ComponentModel;

namespace JikanDotNet
{
	/// <summary>
	/// Current status of anime or manga (search config).
	/// </summary>
	public enum AiringStatus
	{
		/// <summary>
		/// Allow all statuses to be displayed in results.
		/// </summary>
		[Description("")]
		EveryStatus,

		/// <summary>
		/// Airing (anime) or publishing (manga) status.
		/// </summary>
		[Description("airing")]
		Airing,

		/// <summary>
		/// Completed status.
		/// </summary>
		[Description("completed")]
		Completed,

		/// <summary>
		/// Upcoming status.
		/// </summary>
		[Description("upcoming")]
		Upcoming
	}
}