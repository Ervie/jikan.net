using System.ComponentModel;

namespace JikanDotNet
{
	/// <summary>
	/// Defines sort direction in search requests
	/// </summary>
	public enum SortDirection
	{
		/// <summary>
		/// Sort ascending.
		/// </summary>
		[Description("ascending")]
		Ascending,

		/// <summary>
		/// Use descending.
		/// </summary>
		[Description("descending")]
		Descending,
	}
}