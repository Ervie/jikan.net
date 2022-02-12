using System.Text;
using JikanDotNet.Consts;
using JikanDotNet.Extensions;
using JikanDotNet.Helpers;
using JikanDotNet.Interfaces;

namespace JikanDotNet;

public class UserSearchConfig: ISearchConfig
{
    /// <summary>
	/// Index of page folding 50 records of top ranging (e.g. 1 will return first 50 records, 2 will return record from 51 to 100 etc.)
	/// </summary>
	public int? Page { get; set; }
    
    /// <summary>
	/// Search query.
	/// </summary>
	public string Query { get; set; }
	
	/// <summary>
	/// Gender of the user.
	/// </summary>
	public UserGender Gender { get; set; }
	
	/// <summary>
	/// Location of the searched users.
	/// </summary>
	public string Location { get; set; }
	
	/// <summary>
	/// Max age of the searched users.
	/// </summary>
	public int? MaxAge { get; set; }
	
	/// <summary>
	/// Min age of the searched users.
	/// </summary>
	public int? MinAge { get; set; }
	
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
        
        if (!string.IsNullOrWhiteSpace(Query))
        {
	        builder.Append($"q={Query}&");
        }
        
        if (Gender != UserGender.Any)
        {
	        Guard.IsValidEnum(Gender, nameof(Gender));
	        builder.Append($"gender={Gender.GetDescription()}&");
        }
        
        if (!string.IsNullOrWhiteSpace(Location))
        {
	        builder.Append($"location={Location}&");
        }
        
        if (MinAge.HasValue)
        {
	        builder.Append($"minAge={MinAge.Value}&");
        }
        
        if (MaxAge.HasValue)
        {
	        builder.Append($"maxAge={MaxAge.Value}&");
        }

    	return builder.ToString().Trim('&');
    }
}