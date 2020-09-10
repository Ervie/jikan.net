using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace JikanDotNet
{
	/// <summary>
	/// Model class for manga user updates collection.
	/// </summary>
	public class MangaUserUpdates: BaseJikanRequest
	{
		/// <summary>
		/// Collection of manga user updates.
		/// </summary>
		[JsonPropertyName("users")]
		public ICollection<MangaUserUpdate> Updates { get; set; }
	}
}