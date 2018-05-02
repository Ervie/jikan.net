using JikanDotNet.Consts;
using JikanDotNet.Exceptions;
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

		/// <summary>
		/// Should exception be thrown in case of failed request.
		/// </summary>
		private readonly bool surpressException;

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
		/// <param name="surpressException">Should exception be thrown in case of failed request. If true, failed request return null.</param>
		public Jikan(bool useHttps, bool surpressException = true)
		{
			this.useHttps = useHttps;
			this.surpressException = surpressException;
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

		/// <summary>
		/// Vasic method for handling requests and responses from endpoint.
		/// </summary>
		/// <typeparam name="T">Class type received from GET requests.</typeparam>
		/// <param name="malId">Id of related item on MyAnimeList.</param>
		/// <param name="endPoint">Endpoint target.</param>
		/// <returns>Requested object if successfull, null otherwise.</returns>
		private async Task<T> ExecuteGetRequest<T> (long malId, string endPoint) where T: class
		{
			T returnedObject = null;
			string requestUrl = BuildRequestUrl(endPoint, malId);
			using (HttpResponseMessage response = await httpClient.GetAsync(requestUrl))
			{
				if (response.IsSuccessStatusCode)
				{
					string json = await response.Content.ReadAsStringAsync();
					returnedObject = JsonConvert.DeserializeObject<T>(json);
				}
				else if (!surpressException)
				{
					throw new JikanRequestException(string.Format(Resources.Errors.FailedRequest, response.Content), response.StatusCode);
				}
			}
			return returnedObject;
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
			return await ExecuteGetRequest<Anime>(id, JikanEndPointCategories.Anime);
		}

		/// <summary>
		/// Return character with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of character.</param>
		/// <returns>Character with given MAL id.</returns>
		public async Task<Character> GetCharacter(long id)
		{
			return await ExecuteGetRequest<Character>(id, JikanEndPointCategories.Character);
		}

		/// <summary>
		/// Return manga with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of manga.</param>
		public async Task<Manga> GetManga(long id)
		{
			return await ExecuteGetRequest<Manga>(id, JikanEndPointCategories.Manga);
		}

		/// <summary>
		/// Return person with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of person.</param>
		/// <returns>Person with given MAL id.</returns>
		public async Task<Person> GetPerson(long id)
		{
			return await ExecuteGetRequest<Person>(id, JikanEndPointCategories.Person);
		}

		#endregion Public Methods
	}
}