using Newtonsoft.Json;
using System.Collections.Generic;

namespace JikanDotNet
{
	/// <summary>
	/// User's friends model class.
	/// </summary>
	public class UserFriends
	{
		/// <summary>
		/// Collection of user's friends basic information
		/// </summary>
		[JsonProperty(PropertyName = "friends")]
		public ICollection<Friend> Friends { get; set; }
	}
}