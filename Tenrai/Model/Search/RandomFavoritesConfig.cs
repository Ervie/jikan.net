using Tenrai.Helpers;
using Tenrai.Interfaces;

namespace Tenrai;

/// <summary>
/// Favorites range for a random character or person.
/// </summary>
public class RandomFavoritesConfig : ISearchConfig
{
	/// <summary>
	/// Lowest number of favorites.
	/// </summary>
	public int? MinimumFavorites { get; set; }

	/// <summary>
	/// Highest number of favorites.
	/// </summary>
	public int? MaximumFavorites { get; set; }

	/// <summary>
	/// Create query from current parameters for the request.
	/// </summary>
	/// <returns>Query from current parameters for the request</returns>
	public string ConfigToString()
	{
		return new QueryBuilder()
			.Add("min_favorites", MinimumFavorites)
			.Add("max_favorites", MaximumFavorites)
			.ToString();
	}
}
