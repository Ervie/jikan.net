using JikanDotNet.Interfaces;

namespace JikanDotNet
{
	/// <summary>
	/// Model class of search configuration for advanced manga search.
	/// </summary>
	public class MangaSearchConfig : ISearchConfig
	{
		/// <summary>
		/// Create query from current parameters for search request.
		/// </summary>
		/// <returns>Query from current parameters for search request</returns>
		public string GetQuery()
		{
			throw new System.NotImplementedException();
		}
	}
}