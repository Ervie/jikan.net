using System.ComponentModel;

namespace Tenrai
{
	/// <summary>
	/// Enumeration for interest stack ordering (search config).
	/// </summary>
	public enum InterestStackSearchOrderBy
	{
		/// <summary>
		/// Does not sort results.
		/// </summary>
		[Description("")]
		NoSorting,

		/// <summary>
		/// Sort by the date the stack was created.
		/// </summary>
		[Description("created_at")]
		CreatedAt,

		/// <summary>
		/// Sort by how many users have restacked the stack.
		/// </summary>
		[Description("restack_count")]
		RestackCount
	}
}
