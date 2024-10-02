using JikanDotNet.Extensions;
using JikanDotNet.Helpers;
using JikanDotNet.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JikanDotNet.Consts;

namespace JikanDotNet
{
	/// <summary>
	/// Model class of search configuration for advanced manga search.
	/// </summary>
	public class MangaTopSearchConfig : ISearchConfig
	{
		/// <summary>
		/// Index of page folding 50 records of top ranging (e.g. 1 will return first 50 records, 2 will return record from 51 to 100 etc.)
		/// </summary>
		public int? Page { get; set; }
	
		/// <summary>
		/// Manga type of searched result.
		/// </summary>
		public MangaType Type { get; set; } = MangaType.EveryType;
		
		/// <summary>
		/// Top items filter types
		/// </summary>
		public TopMangaFilter Filter { get; set; } = TopMangaFilter.NoFilter;

		
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
			
			if (Type != MangaType.EveryType)
			{
				Guard.IsValidEnum(Type, nameof(Type));
				builder.Append($"type={Type.GetDescription()}&");
			}

			if (Filter != TopMangaFilter.NoFilter)
			{
				Guard.IsValidEnum(Filter, nameof(Filter));
				builder.Append($"filter={Filter.GetDescription()}&");
			}
			
			return builder.ToString().Trim('&');
		}
	}
}