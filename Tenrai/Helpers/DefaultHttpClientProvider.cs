using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Tenrai.Helpers;

/// <summary>
/// Provider class for static HttpClient.
/// </summary>
internal static class DefaultHttpClientProvider
{
	/// <summary>
	/// Endpoint for SSL encrypted requests.
	/// The trailing slash is required so that relative routes resolve under <c>/v1/</c>
	/// instead of replacing the <c>v1</c> segment.
	/// </summary>
	internal const string DefaultEndpoint = "https://api.tenrai.org/v1/";

	/// <summary>
	/// Header used to supply a Tenrai Server Key for higher rate limits.
	/// </summary>
	internal const string ServerKeyHeader = "X-Server-Key";

	/// <summary>
	/// Get static HttpClient. Using default Tenrai REST endpoint.
	/// </summary>
	/// <param name="endpoint">Endpoint of the REST API.</param>
	/// <param name="serverKey">Optional Tenrai Server Key, sent as the <c>X-Server-Key</c> header.</param>
	/// <returns>Static HttpClient.</returns>
	internal static HttpClient GetDefaultHttpClient(string endpoint = null, string serverKey = null)
	{
		var uriEndpoint = !string.IsNullOrWhiteSpace(endpoint) ? endpoint : DefaultEndpoint;

		var client = new HttpClient() {BaseAddress = new Uri(uriEndpoint)};
		client.DefaultRequestHeaders.Accept.Clear();
		client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

		if (!string.IsNullOrWhiteSpace(serverKey))
		{
			client.DefaultRequestHeaders.Add(ServerKeyHeader, serverKey);
		}

		return client;
	}
}
