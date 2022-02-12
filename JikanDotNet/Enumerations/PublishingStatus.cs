using System.ComponentModel;

namespace JikanDotNet
{
	/// <summary>
	/// Current status of manga (search config).
	/// </summary>
	public enum PublishingStatus
	{
		/// <summary>
		/// Allow all statuses to be displayed in results.
		/// </summary>
		[Description("")]
		EveryStatus,
		
		/// <summary>
		/// Publishing status.
		/// </summary>
		[Description("publishing")]
		Publishing,

		/// <summary>
		/// Complete status.
		/// </summary>
		[Description("complete")]
		Complete,

		/// <summary>
		/// Hiatus status.
		/// </summary>
		[Description("hiatus")]
		Hiatus,

		/// <summary>
		/// Discontinued status.
		/// </summary>
		[Description("discontinued")]
		Discontinued,

		/// <summary>
		/// Upcoming status.
		/// </summary>
		[Description("upcoming")]
		Upcoming
	}
}