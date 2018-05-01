using JikanDotNet.Helpers;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace JikanDotNet
{
	/// <summary>
	/// Implementation of Jikan wrapper for .Net platform.
	/// </summary>
	public class Jikan : IJikan
	{
		#region Field

		/// <summary>
		/// Endpoint for not SSL encrypted requests.
		/// </summary>
		private const string httpEndpoint = "http://api.jikan.moe";

		/// <summary>
		/// Endpoint for SSL encrypted requests.
		/// </summary>
		private const string httpsEndpoint = "https://api.jikan.moe";

		/// <summary>
		/// Http client class to call REST request and receive REST response.
		/// </summary>
		private readonly HttpClient httpClient;

		/// <summary>
		/// Should library use HTTPS protocol instead of HTTP.
		/// </summary>
		private readonly bool useHttps;

		#endregion

		#region Properties

		/// <summary>
		/// Endpoint to which requests will be send;
		/// </summary>
		public string Endpoint
		{
			get
			{
				return useHttps ? httpsEndpoint : httpEndpoint;
			}
		}

		#endregion


		public Jikan(bool useHttps)
		{
			this.useHttps = useHttps;
			httpClient = HttpProvider.GetHttpClient();
			httpClient.BaseAddress = new Uri(Endpoint);
			httpClient.DefaultRequestHeaders.Accept.Clear();
			httpClient.DefaultRequestHeaders.Accept.Add(
				new MediaTypeWithQualityHeaderValue("application/json"));
		}

		public void GetAnime(long id)
		{
			throw new NotImplementedException();
		}

		public void GetCharacter(long id)
		{
			throw new NotImplementedException();
		}

		public void GetManga(long id)
		{
			throw new NotImplementedException();
		}

		public void GetPerson(long id)
		{
			throw new NotImplementedException();
		}
	}
}
