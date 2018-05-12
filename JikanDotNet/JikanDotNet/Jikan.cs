using JikanDotNet.Consts;
using JikanDotNet.Exceptions;
using JikanDotNet.Extensions;
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
		/// Vasic method for handling requests and responses from endpoint.
		/// </summary>
		/// <typeparam name="T">Class type received from GET requests.</typeparam>
		/// <param name="args">Arguments building endpoint.</param>
		/// <returns>Requested object if successfull, null otherwise.</returns>
		private async Task<T> ExecuteGetRequest<T> (string[] args) where T: class
		{
			T returnedObject = null;
			string requestUrl = String.Join("/", args);
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
		/// <param name="extension">Extension for extra data.</param>
		/// <returns>Anime with given MAL id.</returns>
		public async Task<Anime> GetAnime(long id, AnimeExtension extension = AnimeExtension.None)
		{
			string[] endpointParts = new string[] { Endpoint, JikanEndPointCategories.Anime, id.ToString(), extension.GetDescription() };
			return await ExecuteGetRequest<Anime>(endpointParts);
		}

		/// <summary>
		/// Return character with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of character.</param>
		/// <param name="extension">Extension for extra data.</param>
		/// <returns>Character with given MAL id.</returns>
		public async Task<Character> GetCharacter(long id, CharacterExtension extension = CharacterExtension.None)
		{
			string[] endpointParts = new string[] { Endpoint, JikanEndPointCategories.Character, id.ToString(), extension.GetDescription() };
			return await ExecuteGetRequest<Character>(endpointParts);
		}

		/// <summary>
		/// Return manga with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of manga.</param>
		/// <param name="extension">Extension for extra data.</param>
		public async Task<Manga> GetManga(long id, MangaExtension extension = MangaExtension.None)
		{
			string[] endpointParts = new string[] { Endpoint, JikanEndPointCategories.Manga, id.ToString(), extension.GetDescription() };
			return await ExecuteGetRequest<Manga>(endpointParts);
		}

		/// <summary>
		/// Return person with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of person.</param>
		/// <param name="extension">Extension for extra data.</param>
		/// <returns>Person with given MAL id.</returns>
		public async Task<Person> GetPerson(long id, PersonExtension extension = PersonExtension.None)
		{
			string[] endpointParts = new string[] { Endpoint, JikanEndPointCategories.Person, id.ToString(), extension.GetDescription() };
			return await ExecuteGetRequest<Person>(endpointParts);
		}

		/// <summary>
		/// Return current season preview.
		/// </summary>
		/// <returns>Current season preview.</returns>
		public async Task<Season> GetSeason()
		{
			string[] endpointParts = new string[] { Endpoint, JikanEndPointCategories.Season };
			return await ExecuteGetRequest<Season>(endpointParts);
		}

		/// <summary>
		/// Return season preview.
		/// </summary>
		/// <param name="year">Year of selected season.</param>
		/// <param name="season">Selected season.</param>
		/// <returns>Season preview.</returns>
		public async Task<Season> GetSeason(int year, Seasons season)
		{
			string[] endpointParts = new string[] { Endpoint, JikanEndPointCategories.Season, year.ToString(), season.GetDescription() };
			return await ExecuteGetRequest<Season>(endpointParts);
		}

		/// <summary>
		/// Return current season schedule.
		/// </summary>
		/// <returns>Current season schedule.</returns>
		public async Task<Schedule> GetSchedule()
		{
			string[] endpointParts = new string[] { Endpoint, JikanEndPointCategories.Schedule };
			return await ExecuteGetRequest<Schedule>(endpointParts);
		}

		/// <summary>
		/// Return list of top anime.
		/// </summary>
		/// <returns>List of top anime.</returns>
		public async Task<AnimeTop> GetAnimeTop()
		{
			string[] endpointParts = new string[] { Endpoint, JikanEndPointCategories.TopList, JikanEndPointCategories.Anime };
			return await ExecuteGetRequest<AnimeTop>(endpointParts);
		}

		/// <summary>
		/// Return list of top anime.
		/// </summary>
		/// <param name="page">Indexx of page folding 50 records of top ranging (e.g. 1 will return first 50 records, 2 will return record from 51 to 100 etc.)</param>
		/// <param name="extension">Extension for specific type of ranking.</param>
		/// <returns>List of top anime.</returns>
		public async Task<AnimeTop> GetAnimeTop(int page, TopAnimeExtension extension = TopAnimeExtension.None)
		{
			string[] endpointParts = new string[] { Endpoint, JikanEndPointCategories.TopList, JikanEndPointCategories.Anime, page.ToString(), extension.GetDescription() };
			return await ExecuteGetRequest<AnimeTop>(endpointParts);
		}

		/// <summary>
		/// Return list of top manga.
		/// </summary>
		/// <returns>List of top manga.</returns>
		public async Task<MangaTop> GetMangaTop()
		{
			string[] endpointParts = new string[] { Endpoint, JikanEndPointCategories.TopList, JikanEndPointCategories.Manga };
			return await ExecuteGetRequest<MangaTop>(endpointParts);
		}

		/// <summary>
		/// Return list of top manga.
		/// </summary>
		/// <param name="page">Indexx of page folding 50 records of top ranging (e.g. 1 will return first 50 records, 2 will return record from 51 to 100 etc.)</param>
		/// <param name="extension">Extension for specific type of ranking.</param>
		/// <returns>List of top manga.</returns>
		public async Task<MangaTop> GetMangaTop(int page, TopMangaExtension extension = TopMangaExtension.None)
		{
			string[] endpointParts = new string[] { Endpoint, JikanEndPointCategories.TopList, JikanEndPointCategories.Manga, page.ToString(), extension.GetDescription() };
			return await ExecuteGetRequest<MangaTop>(endpointParts);
		}

		/// <summary>
		/// Return list of results related to search.
		/// </summary>
		/// <param name="query">Search query.</param>
		/// <returns>List of result related to search query.</returns>
		public async Task<AnimeSearchResult> SearchAnime(string query)
		{
			query = query.Replace(' ', '_');
			string[] endpointParts = new string[] { Endpoint, JikanEndPointCategories.Search, JikanEndPointCategories.Anime, query };
			return await ExecuteGetRequest<AnimeSearchResult>(endpointParts);
		}

		/// <summary>
		/// Return list of results related to search.
		/// </summary>
		/// <param name="query">Search query.</param>
		/// <param name="page">Indexx of page folding 50 records of top ranging (e.g. 1 will return first 50 records, 2 will return record from 51 to 100 etc.)</param>
		/// <returns>List of result related to search query.</returns>
		public async Task<AnimeSearchResult> SearchAnime(string query, int page)
		{
			query = query.Replace(' ', '_');
			string[] endpointParts = new string[] { Endpoint, JikanEndPointCategories.Search, JikanEndPointCategories.Anime, query, page.ToString() };
			return await ExecuteGetRequest<AnimeSearchResult>(endpointParts);
		}

		/// <summary>
		/// Return list of results related to search.
		/// </summary>
		/// <param name="query">Search query.</param>
		/// <returns>List of result related to search query.</returns>
		public async Task<MangaSearchResult> SearchManga(string query)
		{
			query = query.Replace(' ', '_');
			string[] endpointParts = new string[] { Endpoint, JikanEndPointCategories.Search, JikanEndPointCategories.Manga, query };
			return await ExecuteGetRequest<MangaSearchResult>(endpointParts);
		}

		/// <summary>
		/// Return list of results related to search.
		/// </summary>
		/// <param name="query">Search query.</param>
		/// <param name="page">Indexx of page folding 50 records of top ranging (e.g. 1 will return first 50 records, 2 will return record from 51 to 100 etc.)</param>
		/// <returns>List of result related to search query.</returns>
		public async Task<MangaSearchResult> SearchManga(string query, int page)
		{
			query = query.Replace(' ', '_');
			string[] endpointParts = new string[] { Endpoint, JikanEndPointCategories.Search, JikanEndPointCategories.Manga, query, page.ToString() };
			return await ExecuteGetRequest<MangaSearchResult>(endpointParts);
		}

		/// <summary>
		/// Return list of results related to search.
		/// </summary>
		/// <param name="query">Search query.</param>
		/// <returns>List of result related to search query.</returns>
		public async Task<PersonSearchResult> SearchPerson(string query)
		{
			query = query.Replace(' ', '_');
			string[] endpointParts = new string[] { Endpoint, JikanEndPointCategories.Search, JikanEndPointCategories.Person, query };
			return await ExecuteGetRequest<PersonSearchResult>(endpointParts);
		}

		/// <summary>
		/// Return list of results related to search.
		/// </summary>
		/// <param name="query">Search query.</param>
		/// <param name="page">Indexx of page folding 50 records of top ranging (e.g. 1 will return first 50 records, 2 will return record from 51 to 100 etc.)</param>
		/// <returns>List of result related to search query.</returns>
		public async Task<PersonSearchResult> SearchPerson(string query, int page)
		{
			query = query.Replace(' ', '_');
			string[] endpointParts = new string[] { Endpoint, JikanEndPointCategories.Search, JikanEndPointCategories.Person, query, page.ToString() };
			return await ExecuteGetRequest<PersonSearchResult>(endpointParts);
		}

		/// <summary>
		/// Return list of results related to search.
		/// </summary>
		/// <param name="query">Search query.</param>
		/// <returns>List of result related to search query.</returns>
		public async Task<CharacterSearchResult> SearchCharacter(string query)
		{
			query = query.Replace(' ', '_');
			string[] endpointParts = new string[] { Endpoint, JikanEndPointCategories.Search, JikanEndPointCategories.Character, query };
			return await ExecuteGetRequest<CharacterSearchResult>(endpointParts);
		}

		/// <summary>
		/// Return list of results related to search.
		/// </summary>
		/// <param name="query">Search query.</param>
		/// <param name="page">Indexx of page folding 50 records of top ranging (e.g. 1 will return first 50 records, 2 will return record from 51 to 100 etc.)</param>
		/// <returns>List of result related to search query.</returns>
		public async Task<CharacterSearchResult> SearchCharacter(string query, int page)
		{
			query = query.Replace(' ', '_');
			string[] endpointParts = new string[] { Endpoint, JikanEndPointCategories.Search, JikanEndPointCategories.Character, query, page.ToString() };
			return await ExecuteGetRequest<CharacterSearchResult>(endpointParts);
		}

		#endregion Public Methods
	}
}