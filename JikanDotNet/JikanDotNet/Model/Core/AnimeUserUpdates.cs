using Newtonsoft.Json;
using System.Collections.Generic;

namespace JikanDotNet
{
	/// <summary>
	/// Model class for anime user updates collection.
	/// </summary>
	public class AnimeUserUpdates
	{
		/// <summary>
		/// Collection of anime user updates.
		/// </summary>
		[JsonProperty(PropertyName = "users")]
		public ICollection<AnimeUserUpdate> Updates { get; set; }
	}
}