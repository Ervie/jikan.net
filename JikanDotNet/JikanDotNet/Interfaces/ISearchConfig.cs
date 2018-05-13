namespace JikanDotNet.Interfaces
{
	/// <summary>
	/// Interface of search config for advanced searching.
	/// </summary>
	internal interface ISearchConfig
	{
		/// <summary>
		/// Create query from current parameters for search request.
		/// </summary>
		/// <returns>Query from current parameters for search request</returns>
		string ConfigToString();
	}
}