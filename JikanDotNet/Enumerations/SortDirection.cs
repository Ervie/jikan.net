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
		[Description("asc")]
		Ascending,

		/// <summary>
		/// Use descending.
		/// </summary>
		[Description("desc")]
		Descending,
	}
}