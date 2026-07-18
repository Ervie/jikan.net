using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace JikanDotNet.Helpers
{
	/// <summary>
	/// Provider class for static HttpClient.
	/// </summary>
	internal static class DefaultHttpClientProvider
	{
		/// <summary>
		/// Endpoint for SSL encrypted requests.
		/// </summary>
		internal const string DefaultEndpoint = "https://api.jikan.moe/v4/";

		/// <summary>
		/// Get static HttpClient. Using default Jikan REST endpoint.
		/// </summary>
		/// <param name="endpoint">Endpoint of the REST API.</param>
		/// <returns>Static HttpClient.</returns>
		internal static HttpClient GetDefaultHttpClient(string endpoint = null)
		{
			var uriEndpoint = !string.IsNullOrWhiteSpace(endpoint) ? endpoint : DefaultEndpoint;
			
			var client = new HttpClient(new ForceHttp11Handler(new HttpClientHandler())) {BaseAddress = new Uri(uriEndpoint)};
			client.DefaultRequestHeaders.Accept.Clear();
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/html"));

            client.DefaultRequestHeaders.AcceptEncoding.Clear();
            client.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br, zstd");

            return client;
		}

        internal class ForceHttp11Handler : DelegatingHandler
        {
            public ForceHttp11Handler(HttpMessageHandler innerHandler) : base(innerHandler) { }

            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                request.Version = new Version(1, 1);
                return base.SendAsync(request, cancellationToken);
            }
        }
    }
}
