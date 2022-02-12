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
	public class MangaSearchConfig : ISearchConfig
	{
		/// <summary>
		/// Index of page folding 50 records of top ranging (e.g. 1 will return first 50 records, 2 will return record from 51 to 100 etc.)
		/// </summary>
		public int? Page { get; set; }
	
		/// <summary>
		/// Size of the page (25 is the max).
		/// </summary>
		public int? PageSize { get; set; }
	
		/// <summary>
		/// Search query.
		/// </summary>
		public string Query { get; set; }
	
		/// <summary>
		/// Return entries starting with the given letter.
		/// </summary>
		public char? Letter { get; set; }
		
		/// <summary>
		/// Manga type of searched result.
		/// </summary>
		public MangaType Type { get; set; } = MangaType.EveryType;

		/// <summary>
		/// Minimum score results (1-10).
		/// </summary>
		public int? MinimumScore { get; set; }
		
		/// <summary>
		/// Maximum score results (1-10).
		/// </summary>
		public int? MaximumScore { get; set; }

		/// <summary>
		/// Current status.
		/// </summary>
		public PublishingStatus Status { get; set; }

		/// <summary>
		/// Select order property.
		/// </summary>
		public MangaSearchOrderBy OrderBy { get; set; }

		/// <summary>
		/// Define sort direction for <see cref="OrderBy">OrderBy</see> property.
		/// </summary>
		public SortDirection SortDirection { get; set; }

		/// <summary>
		/// Genres to include.
		/// </summary>
		public ICollection<MangaGenreSearch> Genres { get; set; } = new List<MangaGenreSearch>();
		
		/// <summary>
		/// Genres to exclude.
		/// </summary>
		public ICollection<MangaGenreSearch> ExcludedGenres { get; set; } = new List<MangaGenreSearch>();

		/// <summary>
		/// Filter by magazine id.
		/// </summary>
		public ICollection<long> MagazineIds { get; set; } = new List<long>();
		
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
        
			if (PageSize.HasValue)
			{
				Guard.IsGreaterThanZero(PageSize.Value, nameof(PageSize.Value));
				Guard.IsLesserOrEqualThan(PageSize.Value,ParameterConsts.MaximumPageSize, nameof(PageSize.Value));
				builder.Append($"limit={PageSize.Value}&");
			}
        
			if (!string.IsNullOrWhiteSpace(Query))
			{
				builder.Append($"q={Query}&");
			}
        
			if (Letter.HasValue)
			{
				Guard.IsLetter(Letter.Value, nameof(Letter.Value));
				builder.Append($"letter={Letter.Value}&");
			}
			
			if (Type != MangaType.EveryType)
			{
				Guard.IsValidEnum(Type, nameof(Type));
				builder.Append($"type={Type.GetDescription()}&");
			}

			if (MinimumScore.HasValue)
			{
				builder.Append($"min_score={MinimumScore}&");
			}
			
			if (MaximumScore.HasValue)
			{
				builder.Append($"max_score={MaximumScore}&");
			}

			if (Status != PublishingStatus.EveryStatus)
			{
				Guard.IsValidEnum(Status, nameof(Status));
				builder.Append($"status={Status.GetDescription()}&");
			}

			if (Genres.Count > 0 )
			{
				var genresIds = Genres.Select(genreSearch =>
				{
					Guard.IsValidEnum(genreSearch, nameof(genreSearch));
					return genreSearch.GetDescription();
				}).ToArray();

				builder.Append($"genres={string.Join(",", genresIds)}&");
			}
			
			
			if (ExcludedGenres.Count > 0 )
			{
				var genresIds = ExcludedGenres.Select(genreSearch =>
				{
					Guard.IsValidEnum(genreSearch, nameof(genreSearch));
					return genreSearch.GetDescription();
				}).ToArray();

				builder.Append($"excluded_genres={string.Join(",", genresIds)}&");
			}

			if (OrderBy != MangaSearchOrderBy.NoSorting)
			{
				Guard.IsValidEnum(OrderBy, nameof(OrderBy));
				Guard.IsValidEnum(SortDirection, nameof(SortDirection));
				builder.Append($"order_by={OrderBy.GetDescription()}&");
				builder.Append($"sort={SortDirection.GetDescription()}&");
			}

			if (MagazineIds.Any())
			{
				builder.Append($"magazine={string.Join(",", MagazineIds)}&");
			}

			if (Sfw)
			{
				builder.Append("sfw");
			}
			
			return builder.ToString().Trim('&');
		}
	}
}