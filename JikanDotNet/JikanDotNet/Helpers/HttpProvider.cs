using System.Net.Http;

namespace JikanDotNet.Helpers
{
	/// <summary>
	/// Provider class for static HttpClient.
	/// </summary>
	public static class HttpProvider
	{
		/// <summary>
		/// Static HttpClient.
		/// </summary>
		private static HttpClient client { get; set; }

		/// <summary>
		/// Constructor.
		/// </summary>
		static HttpProvider()
		{
			client = new HttpClient();
		}

		/// <summary>
		/// Get static HttpClient.
		/// </summary>
		/// <returns>Static HttpClient.</returns>
		public static HttpClient GetHttpClient()
		{
			return client ?? new HttpClient();
		}
	}
}