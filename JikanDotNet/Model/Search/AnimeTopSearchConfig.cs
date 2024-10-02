using JikanDotNet.Extensions;
using JikanDotNet.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JikanDotNet.Consts;
using JikanDotNet.Helpers;

namespace JikanDotNet
{
	/// <summary>
	/// Model class of search configuration for advanced anime search.
	/// </summary>
	public class AnimeTopSearchConfig : ISearchConfig
	{
		/// <summary>
		/// Index of page folding 50 records of top ranging (e.g. 1 will return first 50 records, 2 will return record from 51 to 100 etc.)
		/// </summary>
		public int? Page { get; set; }
		
		/// <summary>
		/// Anime type of searched result.
		/// </summary>
		public AnimeType Type { get; set; } = AnimeType.EveryType;
		
		/// <summary>
		/// Top items filter types
		/// </summary>
		public TopAnimeFilter Filter { get; set; } = TopAnimeFilter.NoFilter;

		/// <summary>
		/// Age rating.
		/// </summary>
		public AnimeAgeRating Rating { get; set; } = AnimeAgeRating.EveryRating;

		/// <summary>
		/// Should only search for sfw title. Filter out adult entries.
		/// </summary>
		public bool Sfw { get; set; } = true;

		/// <summary>
		/// Create query from current parameters for search request.
		/// </summary>
		/// <returns>Query from current parameters for search request</returns>
		public string ConfigToString()
		{
			var builder = new StringBuilder().Append('?');

			if (Page.HasValue)
			{
				Guard.IsGreaterThanZero(Page.Value, nameof(Page.Value));
				builder.Append($"page={Page.Value}&");
			}
			
			if (Type != AnimeType.EveryType)
			{
				Guard.IsValidEnum(Type, nameof(Type));
				builder.Append($"type={Type.GetDescription()}&");
			}

			if (Rating != AnimeAgeRating.EveryRating)
			{
				Guard.IsValidEnum(Rating, nameof(Rating));
				builder.Append($"rating={Rating.GetDescription()}&");
			}

			if (Filter != TopAnimeFilter.NoFilter)
			{
				Guard.IsValidEnum(Filter, nameof(Filter));
				builder.Append($"filter={Filter.GetDescription()}&");
			}

			if (Sfw)
			{
				builder.Append("sfw");
			}

			return builder.ToString().Trim('&');
		}
	}
}