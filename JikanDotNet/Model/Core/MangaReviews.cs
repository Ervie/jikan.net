using Newtonsoft.Json;
using System.Collections.Generic;

namespace JikanDotNet
{
	/// <summary>
	/// Model class for manga review collection.
	/// </summary>
	public class MangaReviews: BaseJikanRequest
	{
		/// <summary>
		/// Collection of manga reviews.
		/// </summary>
		[JsonProperty(PropertyName = "reviews")]
		public ICollection<MangaReview> Reviews { get; set; }
	}
}