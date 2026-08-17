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

			//This exact value (also order) of Accept-Encoding makes Jikan accept way more requests from my testing.
			//Decryption handling for gzip and deflate was also added to Jikan.cs.
			//Jikan never returns anything other than gzip from my testing.
            client.DefaultRequestHeaders.AcceptEncoding.Clear();
            client.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br, zstd");

            return client;
		}

		/// <summary>
		/// Handler that ensures every request uses HTTP/1.1 since Jikan doesn't support HTTP/2.0, 
		/// and some HttpClients, like the one from Xamarin used by MALClient, automatically sends HTTP/2.0
		/// </summary>
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
