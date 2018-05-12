using JikanDotNet.Interfaces;
using System;

namespace JikanDotNet
{
	/// <summary>
	/// Model class of search configuration for advanced anime search.
	/// </summary>
	public class AnimeSearchConfig : ISearchConfig
	{
		/// <summary>
		/// Create query from current parameters for search request.
		/// </summary>
		/// <returns>Query from current parameters for search request</returns>
		public string GetQuery()
		{
			throw new NotImplementedException();
		}
	}
}