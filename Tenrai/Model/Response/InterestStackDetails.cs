using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Tenrai
{
	/// <summary>
	/// Interest stack with its entries model class.
	/// </summary>
	public class InterestStackDetails : InterestStack
	{
		/// <summary>
		/// Entries of the stack, in the order the author arranged them.
		/// </summary>
		[JsonPropertyName("entries")]
		public ICollection<InterestStackEntry> Entries { get; set; }
	}
}
