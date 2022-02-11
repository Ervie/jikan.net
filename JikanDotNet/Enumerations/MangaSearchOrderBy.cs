using System.ComponentModel;

namespace JikanDotNet
{
	/// <summary>
	/// Properties by which manga search results can be ordered.
	/// </summary>
	public enum MangaSearchOrderBy
	{
		/// <summary>
		/// Does not order results.
		/// </summary>
		[Description("")]
		NoSorting,

		/// <summary>
		/// Orders results by title.
		/// </summary>
		[Description("title")]
		Title,

		/// <summary>
		/// Orders results by start date.
		/// </summary>
		[Description("start_date")]
		StartDate,

		/// <summary>
		/// Orders results by end date.
		/// </summary>
		[Description("end_date")]
		EndDate,

		/// <summary>
		/// Orders results by score.
		/// </summary>
		[Description("score")]
		Score,
		
		/// <summary>
		/// Orders results by count of scores.
		/// </summary>
		[Description("scored_by")]
		ScoredBy,

		/// <summary>
		/// Orders results by anime type.
		/// </summary>
		[Description("type")]
		Type,

		/// <summary>
		/// Orders results by members.
		/// </summary>
		[Description("members")]
		Members,

		/// <summary>
		/// Orders results by id.
		/// </summary>
		[Description("mal_id")]
		MalId,

		/// <summary>
		/// Orders results by number of chapters.
		/// </summary>
		[Description("chapters")]
		Chapters,

		/// <summary>
		/// Orders results by number of volumes.
		/// </summary>
		[Description("volumes")]
		Volumes,

		/// <summary>
		/// Orders results by rank.
		/// </summary>
		[Description("rank")]
		Rank,
		
		/// <summary>
		/// Orders results by popularity.
		/// </summary>
		[Description("popularity")]
		Popularity,

		/// <summary>
		/// Orders results by favorites.
		/// </summary>
		[Description("favorites")]
		Favorites
	}
}