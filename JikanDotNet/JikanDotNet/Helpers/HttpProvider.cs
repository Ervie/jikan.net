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
		/// Endpoint for not SSL encrypted requests.
		/// </summary>
		public const string httpEndpoint = "http://api.jikan.moe";

		/// <summary>
		/// Endpoint for SSL encrypted requests.
		/// </summary>
		public const string httpsEndpoint = "https://api.jikan.moe";

		/// <summary>
		/// Static HttpClient.
		/// </summary>
		private static HttpClient Client { get; set; }

		/// <summary>
		/// Constructor.
		/// </summary>
		static HttpProvider()
		{
			Client = new HttpClient();
		}

		/// <summary>
		/// Get static HttpClient.
		/// </summary>
		/// <returns>Static HttpClient.</returns>
		public static HttpClient GetHttpClient(bool useHttps)
		{
			if (Client == null)
			{
				string endpoint = useHttps ? httpsEndpoint : httpEndpoint;
				Client = new HttpClient
				{
					BaseAddress = new Uri(endpoint)
				};
				Client.DefaultRequestHeaders.Accept.Clear();
				Client.DefaultRequestHeaders.Accept.Add(
					new MediaTypeWithQualityHeaderValue("application/json"));
			}

			return Client;
		}
	}
}
