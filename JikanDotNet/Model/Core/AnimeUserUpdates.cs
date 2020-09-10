using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace JikanDotNet
{
	/// <summary>
	/// Model class for anime user updates collection.
	/// </summary>
	public class AnimeUserUpdates: BaseJikanRequest
	{
		/// <summary>
		/// Collection of anime user updates.
		/// </summary>
		[JsonPropertyName("users")]
		public ICollection<AnimeUserUpdate> Updates { get; set; }
	}
}