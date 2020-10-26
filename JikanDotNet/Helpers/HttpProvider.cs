using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace JikanDotNet.Helpers
{
	/// <summary>
	/// Provider class for static HttpClient.
	/// </summary>
	public static class HttpProvider
	{
		/// <summary>
		/// Endpoint for SSL encrypted requests.
		/// </summary>
		public const string defaultEndpoint = "https://api.jikan.moe/v4-alpha/";
		
		/// <summary>
		/// Constructor.
		/// </summary>
		static HttpProvider()
		{
		}

		/// <summary>
		/// Get static HttpClient. Using default Jikan REST endpoint.
		/// </summary>
		/// <returns>Static HttpClient.</returns>
		public static HttpClient GetHttpClient()
		{
			HttpClient Client = new HttpClient
			{
				BaseAddress = new Uri(defaultEndpoint)
			};
			Client.DefaultRequestHeaders.Accept.Clear();
			Client.DefaultRequestHeaders.Accept.Add(
				new MediaTypeWithQualityHeaderValue("text/html"));
			
			return Client;
		}

		/// <summary>
		/// Get static HttpClient. Using custom, user defined Jikan REST endpoint.
		/// </summary>
		/// <param name="endpoint">Endpoint of the REST API.</param>
		/// <returns>Static HttpClient.</returns>
		public static HttpClient GetHttpClient(Uri endpoint)
		{
			HttpClient Client = new HttpClient
			{
				BaseAddress = endpoint
			};
			Client.DefaultRequestHeaders.Accept.Clear();
			Client.DefaultRequestHeaders.Accept.Add(
				new MediaTypeWithQualityHeaderValue("text/html"));
			
			return Client;
		}
	}
}
