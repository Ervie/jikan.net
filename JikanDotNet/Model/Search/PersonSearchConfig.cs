using System.Text;
using JikanDotNet.Consts;
using JikanDotNet.Extensions;
using JikanDotNet.Helpers;
using JikanDotNet.Interfaces;

namespace JikanDotNet;

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
    	
    	if (OrderBy != PersonSearchOrderBy.NoSorting)
    	{
    		Guard.IsValidEnum(OrderBy, nameof(OrderBy));
    		Guard.IsValidEnum(SortDirection, nameof(SortDirection));
    		builder.Append($"order_by={OrderBy.GetDescription()}&");
    		builder.Append($"sort={SortDirection.GetDescription()}");
    	}

    	return builder.ToString().Trim('&');
    }
}