using Tenrai.Helpers;
using Tenrai.Interfaces;

namespace Tenrai;

/// <summary>
/// Model class of search configuration for advanced person search.
/// </summary>
public class PersonSearchConfig: ISearchConfig
{
	/// <summary>
	/// Index of page folding 50 records of top ranging (e.g. 1 will return first 50 records, 2 will return record from 51 to 100 etc.)
	/// </summary>
	public int? Page { get; set; }

	/// <summary>
	/// Size of the page (100 is the max).
	/// </summary>
	public int? Limit { get; set; }

	/// <summary>
	/// Search query.
	/// </summary>
	public string Query { get; set; }

	/// <summary>
	/// Return entries starting with the given letter.
	/// </summary>
	public char? Letter { get; set; }

	/// <summary>
	/// Lowest number of favorites.
	/// </summary>
	public int? MinimumFavorites { get; set; }

	/// <summary>
	/// Highest number of favorites.
	/// </summary>
	public int? MaximumFavorites { get; set; }

	/// <summary>
	/// Select order by property.
	/// </summary>
	public PersonSearchOrderBy OrderBy { get; set; }

	/// <summary>
	/// Define sort direction for <see cref="OrderBy">OrderBy</see> property.
	/// </summary>
	public SortDirection SortDirection { get; set; }

	/// <summary>
	/// Create query from current parameters for search request.
	/// </summary>
	/// <returns>Query from current parameters for search request</returns>
	public string ConfigToString()
	{
		if (Letter.HasValue)
		{
			Guard.IsLetter(Letter.Value, nameof(Letter));
		}

		var query = new QueryBuilder()
			.Page(Page)
			.Limit(Limit)
			.Add("q", Query)
			.Add("letter", Letter?.ToString())
			.Add("min_favorites", MinimumFavorites)
			.Add("max_favorites", MaximumFavorites);

		if (OrderBy != PersonSearchOrderBy.NoSorting)
		{
			query.Add("order_by", OrderBy).Add("sort", SortDirection);
		}

		return query.ToString();
	}
}