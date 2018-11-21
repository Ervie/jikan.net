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
		private async Task<T> ExecuteGetRequest<T>(string[] args) where T : class
		{
			T returnedObject = null;
			string requestUrl = String.Join("/", args);
			try
			{
				using (HttpResponseMessage response = await httpClient.GetAsync(requestUrl))
				{
					if (response.IsSuccessStatusCode)
					{
						string json = await response.Content.ReadAsStringAsync();

						// prevent deserializing "related" into empty array
						// May change if endpoint implementation change
						json = json.Replace("\"related\":[]", "\"related\":null");
						returnedObject = JsonConvert.DeserializeObject<T>(json);
					}
					else if (!surpressException)
					{
						throw new JikanRequestException(string.Format(Resources.Errors.FailedRequest, response.Content), response.StatusCode);
					}
				}
			}
			catch (JsonSerializationException ex)
			{
				if (!surpressException)
				{
					throw new JikanRequestException(Resources.Errors.SerializationFailed + Environment.NewLine + "Inner exception message: " + ex.Message);
				}
			}
			return returnedObject;
		}

		#endregion Private Methods

		#region Public Methods

		#region GetAnime

		/// <summary>
		/// Return anime with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <returns>Anime with given MAL id.</returns>
		public async Task<Anime> GetAnime(long id)
		{
			string[] endpointParts = new string[] { Endpoint, JikanEndPointCategories.Anime, id.ToString() };
			return await ExecuteGetRequest<Anime>(endpointParts);
		}

		/// <summary>
		/// Return anime with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <param name="extension">Extension for extra data.</param>
		/// <returns>Anime with given MAL id.</returns>
		public async Task<Anime> GetAnime(long id, AnimeExtension extension)
		{
			string[] endpointParts = new string[] { Endpoint, JikanEndPointCategories.Anime, id.ToString(), extension.GetDescription() };
			return await ExecuteGetRequest<Anime>(endpointParts);
		}

		#endregion GetAnime

		#region GetAnimeEpisodes

		/// <summary>
		/// Return list of episodes for anime with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <param name="page">Indexx of page folding 50 records of top ranging (e.g. 1 will return first 50 records, 2 will return record from 51 to 100 etc.)</param>
		/// <returns>List of episodes with details.</returns>
		public async Task<AnimeEpisodes> GetAnimeEpisodes(long id, int page)
		{
			string[] endpointParts = new string[] { Endpoint, JikanEndPointCategories.Anime, id.ToString(), AnimeExtension.Episodes.GetDescription(), page.ToString() };
			return await ExecuteGetRequest<AnimeEpisodes>(endpointParts);
		}

		/// <summary>
		/// Return list of episodes for anime with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <returns>List of episodes with details.</returns>
		public async Task<AnimeEpisodes> GetAnimeEpisodes(long id)
		{
			string[] endpointParts = new string[] { Endpoint, JikanEndPointCategories.Anime, id.ToString(), AnimeExtension.Episodes.GetDescription() };
			return await ExecuteGetRequest<AnimeEpisodes>(endpointParts);
		}

		#endregion

		#region  GetAnimeCharactersStaff

		/// <summary>
		/// Return collections of characters and staff of anime with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <returns>Collections of characters and staff of anime with given MAL id.</returns>
		public async Task<AnimeCharactersStaff> GetAnimeCharactersStaff(long id)
		{
			string[] endpointParts = new string[] { Endpoint, JikanEndPointCategories.Anime, id.ToString(), AnimeExtension.CharactersStaff.GetDescription() };
			return await ExecuteGetRequest<AnimeCharactersStaff>(endpointParts);
		}

		#endregion

		#region  GetAnimePictures

		/// <summary>
		/// Return collections of links to pictures related to anime with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <returns>Collections of links to pictures related to anime with given MAL id.</returns>
		public async Task<AnimePictures> GetAnimePictures(long id)
		{
			string[] endpointParts = new string[] { Endpoint, JikanEndPointCategories.Anime, id.ToString(), AnimeExtension.Pictures.GetDescription() };
			return await ExecuteGetRequest<AnimePictures>(endpointParts);
		}

		#endregion

		#region  GetAnimeNews

		/// <summary>
		/// Return collections of news related to anime with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <returns>Collections of news related to anime with given MAL id.</returns>
		public async Task<AnimeNews> GetAnimeNews(long id)
		{
			string[] endpointParts = new string[] { Endpoint, JikanEndPointCategories.Anime, id.ToString(), AnimeExtension.News.GetDescription() };
			return await ExecuteGetRequest<AnimeNews>(endpointParts);
		}

		#endregion

		#region GetAnimeVideos

		/// <summary>
		/// Return collections of videos related to anime with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <returns>Collections of videos related to anime with given MAL id.</returns>
		public async Task<AnimeVideos> GetAnimeVideos(long id)
		{
			string[] endpointParts = new string[] { Endpoint, JikanEndPointCategories.Anime, id.ToString(), AnimeExtension.Videos.GetDescription() };
			return await ExecuteGetRequest<AnimeVideos>(endpointParts);
		}

		#endregion

		#region GetAnimeStatistics

		/// <summary>
		/// Return statistics related to anime with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <returns>Statistics related to anime with given MAL id.</returns>
		public async Task<AnimeStats> GetAnimeStatistics(long id)
		{
			string[] endpointParts = new string[] { Endpoint, JikanEndPointCategories.Anime, id.ToString(), AnimeExtension.Stats.GetDescription() };
			return await ExecuteGetRequest<AnimeStats>(endpointParts);
		}

		#endregion

		#region GetAnimeForumTopics

		/// <summary>
		/// Return collections of forum topics related to anime with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <returns>Collections of forum topics related to anime with given MAL id.</returns>
		public async Task<ForumTopics> GetAnimeForumTopics(long id)
		{
			string[] endpointParts = new string[] { Endpoint, JikanEndPointCategories.Anime, id.ToString(), AnimeExtension.Forum.GetDescription() };
			return await ExecuteGetRequest<ForumTopics>(endpointParts);
		}

		#endregion

		#region GetAnimeMoreInfo

		/// <summary>
		/// Return additional information related to anime with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <returns>Additional information related to anime with given MAL id.</returns>
		public async Task<MoreInfo> GetAnimeMoreInfo(long id)
		{
			string[] endpointParts = new string[] { Endpoint, JikanEndPointCategories.Anime, id.ToString(), AnimeExtension.MoreInfo.GetDescription() };
			return await ExecuteGetRequest<MoreInfo>(endpointParts);
		}

		#endregion

		#region GetManga

		/// <summary>
		/// Return manga with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of manga.</param>
		/// <returns>Manga with given MAL id.</returns>
		public async Task<Manga> GetManga(long id)
		{
			string[] endpointParts = new string[] { Endpoint, JikanEndPointCategories.Manga, id.ToString() };
			return await ExecuteGetRequest<Manga>(endpointParts);
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

		#endregion GetManga

		#region  GetMangaPictures

		/// <summary>
		/// Return collections of links to pictures related to manga with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of manga.</param>
		/// <returns>Collections of links to pictures related to manga with given MAL id.</returns>
		public async Task<MangaPictures> GetMangaPictures(long id)
		{
			string[] endpointParts = new string[] { Endpoint, JikanEndPointCategories.Manga, id.ToString(), MangaExtension.Pictures.GetDescription() };
			return await ExecuteGetRequest<MangaPictures>(endpointParts);
		}

		#endregion

		#region GetMangaCharacters

		/// <summary>
		/// Return collections of characters appearing in manga with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of manga.</param>
		/// <returns>Collections of characters appearing in manga with given MAL id.</returns>
		public async Task<MangaCharacters> GetMangaCharacters(long id)
		{
			string[] endpointParts = new string[] { Endpoint, JikanEndPointCategories.Manga, id.ToString(), MangaExtension.Characters.GetDescription() };
			return await ExecuteGetRequest<MangaCharacters>(endpointParts);
		}

		#endregion

		#region  GetMangaNews

		/// <summary>
		/// Return collections of news related to manga with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of manga.</param>
		/// <returns>Collections of news related to manga with given MAL id.</returns>
		public async Task<MangaNews> GetMangaNews(long id)
		{
			string[] endpointParts = new string[] { Endpoint, JikanEndPointCategories.Manga, id.ToString(), MangaExtension.News.GetDescription() };
			return await ExecuteGetRequest<MangaNews>(endpointParts);
		}

		#endregion

		#region GetMangaStatistics

		/// <summary>
		/// Return statistics related to manga with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of manga.</param>
		/// <returns>Statistics related to manga with given MAL id.</returns>
		public async Task<MangaStats> GetMangaStatistics(long id)
		{
			string[] endpointParts = new string[] { Endpoint, JikanEndPointCategories.Manga, id.ToString(), MangaExtension.Stats.GetDescription() };
			return await ExecuteGetRequest<MangaStats>(endpointParts);
		}

		#endregion

		#region GetMangaForumTopics

		/// <summary>
		/// Return collections of forum topics related to manga with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of manga.</param>
		/// <returns>Collections of forum topics related to manga with given MAL id.</returns>
		public async Task<ForumTopics> GetMangaForumTopics(long id)
		{
			string[] endpointParts = new string[] { Endpoint, JikanEndPointCategories.Manga, id.ToString(), MangaExtension.Forum.GetDescription() };
			return await ExecuteGetRequest<ForumTopics>(endpointParts);
		}

		#endregion

		#region GetMangaMoreInfo

		/// <summary>
		/// Return additional information related to manga with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of manga.</param>
		/// <returns>Additional information related to manga with given MAL id.</returns>
		public async Task<MoreInfo> GetMangaMoreInfo(long id)
		{
			string[] endpointParts = new string[] { Endpoint, JikanEndPointCategories.Manga, id.ToString(), AnimeExtension.MoreInfo.GetDescription() };
			return await ExecuteGetRequest<MoreInfo>(endpointParts);
		}

		#endregion

		#region GetCharacter

		/// <summary>
		/// Return character with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of character.</param>
		/// <returns>Character with given MAL id.</returns>
		public async Task<Character> GetCharacter(long id)
		{
			string[] endpointParts = new string[] { Endpoint, JikanEndPointCategories.Character, id.ToString() };
			return await ExecuteGetRequest<Character>(endpointParts);
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

		#endregion GetCharacter

		#region GetCharacterPictures

		/// <summary>
		/// Return collections of links to pictures related to character with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of character.</param>
		/// <returns>Collections of links to pictures related to character with given MAL id.</returns>
		public async Task<CharacterPictures> GetCharacterPictures(long id)
		{
			string[] endpointParts = new string[] { Endpoint, JikanEndPointCategories.Character, id.ToString(), CharacterExtension.Pictures.GetDescription() };
			return await ExecuteGetRequest<CharacterPictures>(endpointParts);
		}

		#endregion

		#region GetPerson

		/// <summary>
		/// Return person with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of person.</param>
		/// <returns>Person with given MAL id.</returns>
		public async Task<Person> GetPerson(long id)
		{
			string[] endpointParts = new string[] { Endpoint, JikanEndPointCategories.Person, id.ToString() };
			return await ExecuteGetRequest<Person>(endpointParts);
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

		#endregion GetPerson

		#region GetPersonPictures

		/// <summary>
		/// Return collections of links to pictures related to person with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of person.</param>
		/// <returns>Collections of links to pictures related to person with given MAL id.</returns>
		public async Task<PersonPictures> GetPersonPictures(long id)
		{
			string[] endpointParts = new string[] { Endpoint, JikanEndPointCategories.Person, id.ToString(), CharacterExtension.Pictures.GetDescription() };
			return await ExecuteGetRequest<PersonPictures>(endpointParts);
		}

		#endregion

		#region GetSeason

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

		#endregion GetSeason

		#region GetSchedule

		/// <summary>
		/// Return current season schedule.
		/// </summary>
		/// <returns>Current season schedule.</returns>
		public async Task<Schedule> GetSchedule()
		{
			string[] endpointParts = new string[] { Endpoint, JikanEndPointCategories.Schedule };
			return await ExecuteGetRequest<Schedule>(endpointParts);
		}

		#endregion GetSchedule

		#region GetAnimeTop

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
		/// <returns>List of top anime.</returns>
		public async Task<AnimeTop> GetAnimeTop(int page)
		{
			string[] endpointParts = new string[] { Endpoint, JikanEndPointCategories.TopList, JikanEndPointCategories.Anime, page.ToString() };
			return await ExecuteGetRequest<AnimeTop>(endpointParts);
		}

		/// <summary>
		/// Return list of top anime.
		/// </summary>
		/// <param name="extension">Extension for specific type of ranking.</param>
		/// <returns>List of top anime.</returns>
		public async Task<AnimeTop> GetAnimeTop(TopAnimeExtension extension)
		{
			string[] endpointParts = new string[] { Endpoint, JikanEndPointCategories.TopList, JikanEndPointCategories.Anime, "1", extension.GetDescription() };
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

		#endregion GetAnimeTop

		#region GetMangaTop

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
		/// <returns>List of top manga.</returns>
		public async Task<MangaTop> GetMangaTop(int page)
		{
			string[] endpointParts = new string[] { Endpoint, JikanEndPointCategories.TopList, JikanEndPointCategories.Manga, page.ToString() };
			return await ExecuteGetRequest<MangaTop>(endpointParts);
		}

		/// <summary>
		/// Return list of top manga.
		/// </summary>
		/// <param name="extension">Extension for specific type of ranking.</param>
		/// <returns>List of top manga.</returns>
		public async Task<MangaTop> GetMangaTop(TopMangaExtension extension)
		{
			string[] endpointParts = new string[] { Endpoint, JikanEndPointCategories.TopList, JikanEndPointCategories.Manga, "1", extension.GetDescription() };
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

		#endregion GetMangaTop

		#region GetTopPeople

		/// <summary>
		/// Return list of most popular people.
		/// </summary>
		/// <returns>List of most popular people.</returns>
		public async Task<PeopleTop> GetPeopleTop()
		{
			string[] endpointParts = new string[] { Endpoint, JikanEndPointCategories.TopList, JikanEndPointCategories.Person };
			return await ExecuteGetRequest<PeopleTop>(endpointParts);
		}

		/// <summary>
		/// Return list of most popular people.
		/// </summary>
		/// <param name="page">Indexx of page folding 50 records of top ranging (e.g. 1 will return first 50 records, 2 will return record from 51 to 100 etc.)</param>
		/// <returns>List of most popular people.</returns>
		public async Task<PeopleTop> GetPeopleTop(int page)
		{
			string[] endpointParts = new string[] { Endpoint, JikanEndPointCategories.TopList, JikanEndPointCategories.Person, page.ToString() };
			return await ExecuteGetRequest<PeopleTop>(endpointParts);
		}

		#endregion GetTopPeople

		#region SearchAnime

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
		/// <param name="searchConfig">Additional configuration for advanced search.</param>
		/// <returns>List of result related to search query.</returns>
		public async Task<AnimeSearchResult> SearchAnime(string query, AnimeSearchConfig searchConfig)
		{
			query = query.Replace(' ', '_');
			string[] endpointParts = new string[] { Endpoint, JikanEndPointCategories.Search, JikanEndPointCategories.Anime, query, searchConfig.ConfigToString() };
			return await ExecuteGetRequest<AnimeSearchResult>(endpointParts);
		}

		/// <summary>
		/// Return list of results related to search.
		/// </summary>
		/// <param name="query">Search query.</param>
		/// <param name="page">Indexx of page folding 50 records of top ranging (e.g. 1 will return first 50 records, 2 will return record from 51 to 100 etc.)</param>
		/// <param name="searchConfig">Additional configuration for advanced search.</param>
		/// <returns>List of result related to search query.</returns>
		public async Task<AnimeSearchResult> SearchAnime(string query, int page, AnimeSearchConfig searchConfig)
		{
			query = query.Replace(' ', '_');
			string[] endpointParts = new string[] { Endpoint, JikanEndPointCategories.Search, JikanEndPointCategories.Anime, query, page.ToString(), searchConfig.ConfigToString() };
			return await ExecuteGetRequest<AnimeSearchResult>(endpointParts);
		}

		#endregion SearchAnime

		#region SearchManga

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
		/// <param name="searchConfig">Additional configuration for advanced search.</param>
		/// <returns>List of result related to search query.</returns>
		public async Task<MangaSearchResult> SearchManga(string query, MangaSearchConfig searchConfig)
		{
			query = query.Replace(' ', '_');
			string[] endpointParts = new string[] { Endpoint, JikanEndPointCategories.Search, JikanEndPointCategories.Manga, query, searchConfig.ConfigToString() };
			return await ExecuteGetRequest<MangaSearchResult>(endpointParts);
		}

		/// <summary>
		/// Return list of results related to search.
		/// </summary>
		/// <param name="query">Search query.</param>
		/// <param name="page">Indexx of page folding 50 records of top ranging (e.g. 1 will return first 50 records, 2 will return record from 51 to 100 etc.)</param>
		/// <param name="searchConfig">Additional configuration for advanced search.</param>
		/// <returns>List of result related to search query.</returns>
		public async Task<MangaSearchResult> SearchManga(string query, int page, MangaSearchConfig searchConfig)
		{
			query = query.Replace(' ', '_');
			string[] endpointParts = new string[] { Endpoint, JikanEndPointCategories.Search, JikanEndPointCategories.Manga, query, page.ToString(), searchConfig.ConfigToString() };
			return await ExecuteGetRequest<MangaSearchResult>(endpointParts);
		}

		#endregion SearchManga

		#region SearchPerson

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

		#endregion SearchPerson

		#region SearchCharacter

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
		#endregion SearchCharacter

		#endregion Public Methods
	}
}