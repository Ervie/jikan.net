using JikanDotNet.Consts;
using JikanDotNet.Helpers;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace JikanDotNet
{
	/// <summary>
	/// Implementation of Jikan wrapper for .Net platform.
	/// </summary>
	public class Jikan : IJikan
	{
		#region Field

		/// <summary>
		/// Http client class to call REST request and receive REST response.
		/// </summary>
		private readonly HttpClient httpClient;

		/// <summary>
		/// Should library use HTTPS protocol instead of HTTP.
		/// </summary>
		private readonly bool useHttps;

		#endregion Field

		#region Properties

		/// <summary>
		/// End to which request will be send to.
		/// </summary>
		public string Endpoint
		{
			get
			{
				return this.useHttps ? HttpProvider.httpsEndpoint : HttpProvider.httpEndpoint;
			}
		}

		#endregion Properties

		#region Constructors

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="useHttps">Should client send SSL encrypted requests.</param>
		public Jikan(bool useHttps)
		{
			this.useHttps = useHttps;
			httpClient = HttpProvider.GetHttpClient(useHttps);
		}

		#endregion Constructors

		#region Private Methods

		/// <summary>
		/// Build while http request string from single components.
		/// </summary>
		/// <param name="jikanEndPoint">Endpoint category for Jikan API.</param>
		/// <param name="malId">MAL id of searched element.</param>
		/// <returns>Request URL.</returns>
		private string BuildRequestUrl(string jikanEndPoint, long malId)
		{
			return $"{Endpoint}/{jikanEndPoint}/{malId}";
		}

		#endregion Private Methods

		#region Public Methods

		/// <summary>
		/// Return anime with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <returns>Anime with given MAL id.</returns>
		public async Task<Anime> GetAnime(long id)
		{
			Anime anime = null;
			string requestUrl = BuildRequestUrl(JikanEndPointCategories.Anime, id);
			HttpResponseMessage response = await httpClient.GetAsync(requestUrl);
			if (response.IsSuccessStatusCode)
			{
				string json = await response.Content.ReadAsStringAsync();
				anime = JsonConvert.DeserializeObject<Anime>(json);
			}
			return anime;
		}

		public async Task<Character> GetCharacter(long id)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Return manga with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of manga.</param>
		public async Task<Manga> GetManga(long id)
		{
			Manga manga = null;
			string requestUrl = BuildRequestUrl(JikanEndPointCategories.Manga, id);
			HttpResponseMessage response = await httpClient.GetAsync(requestUrl);
			if (response.IsSuccessStatusCode)
			{
				string json = await response.Content.ReadAsStringAsync();
				manga = JsonConvert.DeserializeObject<Manga>(json);
			}
			return manga;
		}

		public async Task<Person> GetPerson(long id)
		{
			throw new NotImplementedException();
		}

		#endregion Public Methods
	}
}