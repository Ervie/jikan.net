using JikanDotNet.Consts;
using JikanDotNet.Exceptions;
using JikanDotNet.Extensions;
using JikanDotNet.Helpers;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;

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
		private readonly HttpClient _httpClient;

		/// <summary>
		/// Should library use HTTPS protocol instead of HTTP.
		/// </summary>
		private readonly bool _useHttps;

		/// <summary>
		/// Should exception be thrown in case of failed request.
		/// </summary>
		private readonly bool _suppressException;

		#endregion Field
		
		#region Constructors

		/// <summary>
		/// Constructor.
		/// </summary>
		public Jikan()
		{
			_useHttps = true;
			_suppressException = false;
			_httpClient = HttpProvider.GetHttpClient(_useHttps);
		}

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="useHttps">Should client send SSL encrypted requests.</param>
		/// <param name="suppressException">Should exception be thrown in case of failed request. If true, failed request return null.</param>
		public Jikan(bool useHttps, bool suppressException = false)
		{
			_useHttps = useHttps;
			_suppressException = suppressException;
			_httpClient = HttpProvider.GetHttpClient(useHttps);
		}

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="endpointUrl">Endpoint of the REST API.</param>
		/// <param name="suppressException">Should exception be thrown in case of failed request. If true, failed request return null.</param>
		public Jikan(string endpointUrl, bool suppressException = false)
		{
			_suppressException = suppressException;
			_httpClient = HttpProvider.GetHttpClient(new Uri(endpointUrl));
		}

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="endpointUrl">Endpoint of the REST API.</param>
		/// <param name="suppressException">Should exception be thrown in case of failed request. If true, failed request return null.</param>
		public Jikan(Uri endpointUrl, bool suppressException = true)
		{
			_suppressException = suppressException;
			_httpClient = HttpProvider.GetHttpClient(endpointUrl);
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
			var requestUrl = string.Join("/", args);
			try
			{
				using HttpResponseMessage response = await _httpClient.GetAsync(requestUrl);
				if (response.IsSuccessStatusCode)
				{
					string json = await response.Content.ReadAsStringAsync();

					returnedObject = JsonSerializer.Deserialize<T>(json);
				}
				else if (!_suppressException)
				{
					throw new JikanRequestException(string.Format(Resources.Errors.FailedRequest, response.StatusCode, response.Content), response.StatusCode);
				}
			}
			catch (JsonException ex)
			{
				if (!_suppressException)
				{
					throw new JikanRequestException(Resources.Errors.SerializationFailed + Environment.NewLine + "Inner exception message: " + ex.Message, ex);
				}
			}
			return returnedObject;
		}

		#endregion Private Methods

		#region Public Methods

		#region Anime methods

		#region GetAnime

		/// <summary>
		/// Return anime with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <returns>Anime with given MAL id.</returns>
		public async Task<Anime> GetAnime(long id)
		{
			string[] endpointParts = new string[] { JikanEndPointCategories.Anime, id.ToString() };
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
			string[] endpointParts = new string[] { JikanEndPointCategories.Anime, id.ToString(), AnimeExtension.Episodes.GetDescription(), page.ToString() };
			return await ExecuteGetRequest<AnimeEpisodes>(endpointParts);
		}

		/// <summary>
		/// Return list of episodes for anime with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <returns>List of episodes with details.</returns>
		public async Task<AnimeEpisodes> GetAnimeEpisodes(long id)
		{
			string[] endpointParts = new string[] { JikanEndPointCategories.Anime, id.ToString(), AnimeExtension.Episodes.GetDescription() };
			return await ExecuteGetRequest<AnimeEpisodes>(endpointParts);
		}

		#endregion GetAnimeEpisodes

		#region GetAnimeCharactersStaff

		/// <summary>
		/// Return collections of characters and staff of anime with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <returns>Collections of characters and staff of anime with given MAL id.</returns>
		public async Task<AnimeCharactersStaff> GetAnimeCharactersStaff(long id)
		{
			string[] endpointParts = new string[] { JikanEndPointCategories.Anime, id.ToString(), AnimeExtension.CharactersStaff.GetDescription() };
			return await ExecuteGetRequest<AnimeCharactersStaff>(endpointParts);
		}

		#endregion GetAnimeCharactersStaff

		#region GetAnimeGenre

		/// <summary>
		/// Return information about anime genre.
		/// </summary>
		/// <param name="genreId">Id of the searched genre.</param>
		/// <returns>Information about anime genre</returns>
		public async Task<AnimeGenre> GetAnimeGenre(long genreId)
		{
			string[] endpointParts = new string[] { JikanEndPointCategories.Genre, JikanEndPointCategories.Anime, genreId.ToString() };
			return await ExecuteGetRequest<AnimeGenre>(endpointParts);
		}

		/// <summary>
		/// Return information about anime genre.
		/// </summary>
		/// <param name="genre">Searched genre.</param>
		/// <returns>Information about anime genre</returns>
		public async Task<AnimeGenre> GetAnimeGenre(GenreSearch genre)
		{
			string[] endpointParts = new string[] { JikanEndPointCategories.Genre, JikanEndPointCategories.Anime, genre.GetDescription() };
			return await ExecuteGetRequest<AnimeGenre>(endpointParts);
		}

		/// <summary>
		/// Return information about anime genre.
		/// </summary>
		/// <param name="genreId">Id of the searched genre.</param>
		/// <param name="page">Indexx of page folding 100 records of top ranging (e.g. 1 will return first 100 records, 2 will return record from 101 to 200 etc.)</param>
		/// <returns>Information about anime genre</returns>
		public async Task<AnimeGenre> GetAnimeGenre(long genreId, int page)
		{
			string[] endpointParts = new string[] { JikanEndPointCategories.Genre, JikanEndPointCategories.Anime, genreId.ToString(), page.ToString() };
			return await ExecuteGetRequest<AnimeGenre>(endpointParts);
		}

		/// <summary>
		/// Return information about anime genre.
		/// </summary>
		/// <param name="genre">Searched genre.</param>
		/// <param name="page">Index of page folding 100 records of top ranging (e.g. 1 will return first 100 records, 2 will return record from 101 to 200 etc.)</param>
		/// <returns>Information about anime genre</returns>
		public async Task<AnimeGenre> GetAnimeGenre(GenreSearch genre, int page)
		{
			string[] endpointParts = new string[] { JikanEndPointCategories.Genre, JikanEndPointCategories.Anime, genre.GetDescription(), page.ToString() };
			return await ExecuteGetRequest<AnimeGenre>(endpointParts);
		}

		#endregion GetAnimeGenre

		#region GetAnimePictures

		/// <summary>
		/// Return collections of links to pictures related to anime with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <returns>Collections of links to pictures related to anime with given MAL id.</returns>
		public async Task<AnimePictures> GetAnimePictures(long id)
		{
			string[] endpointParts = new string[] { JikanEndPointCategories.Anime, id.ToString(), AnimeExtension.Pictures.GetDescription() };
			return await ExecuteGetRequest<AnimePictures>(endpointParts);
		}

		#endregion GetAnimePictures

		#region GetAnimeNews

		/// <summary>
		/// Return collections of news related to anime with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <returns>Collections of news related to anime with given MAL id.</returns>
		public async Task<AnimeNews> GetAnimeNews(long id)
		{
			string[] endpointParts = new string[] { JikanEndPointCategories.Anime, id.ToString(), AnimeExtension.News.GetDescription() };
			return await ExecuteGetRequest<AnimeNews>(endpointParts);
		}

		#endregion GetAnimeNews

		#region GetAnimeVideos

		/// <summary>
		/// Return collections of videos related to anime with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <returns>Collections of videos related to anime with given MAL id.</returns>
		public async Task<AnimeVideos> GetAnimeVideos(long id)
		{
			string[] endpointParts = new string[] { JikanEndPointCategories.Anime, id.ToString(), AnimeExtension.Videos.GetDescription() };
			return await ExecuteGetRequest<AnimeVideos>(endpointParts);
		}

		#endregion GetAnimeVideos

		#region GetAnimeStatistics

		/// <summary>
		/// Return statistics related to anime with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <returns>Statistics related to anime with given MAL id.</returns>
		public async Task<AnimeStats> GetAnimeStatistics(long id)
		{
			string[] endpointParts = new string[] { JikanEndPointCategories.Anime, id.ToString(), AnimeExtension.Stats.GetDescription() };
			return await ExecuteGetRequest<AnimeStats>(endpointParts);
		}

		#endregion GetAnimeStatistics

		#region GetAnimeForumTopics

		/// <summary>
		/// Return collections of forum topics related to anime with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <returns>Collections of forum topics related to anime with given MAL id.</returns>
		public async Task<ForumTopics> GetAnimeForumTopics(long id)
		{
			string[] endpointParts = new string[] { JikanEndPointCategories.Anime, id.ToString(), AnimeExtension.Forum.GetDescription() };
			return await ExecuteGetRequest<ForumTopics>(endpointParts);
		}

		#endregion GetAnimeForumTopics

		#region GetAnimeMoreInfo

		/// <summary>
		/// Return additional information related to anime with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <returns>Additional information related to anime with given MAL id.</returns>
		public async Task<MoreInfo> GetAnimeMoreInfo(long id)
		{
			string[] endpointParts = new string[] { JikanEndPointCategories.Anime, id.ToString(), AnimeExtension.MoreInfo.GetDescription() };
			return await ExecuteGetRequest<MoreInfo>(endpointParts);
		}

		#endregion GetAnimeMoreInfo

		#region GetAnimeRecommendations

		/// <summary>
		/// Returns collection of anime recommendation.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <returns>Collection of anime recomendation.</returns>
		public async Task<Recommendations> GetAnimeRecommendations(long id)
		{
			string[] endpointParts = new string[] { JikanEndPointCategories.Anime, id.ToString(), AnimeExtension.Recommendations.GetDescription() };
			return await ExecuteGetRequest<Recommendations>(endpointParts);
		}

		#endregion GetAnimeRecommendations

		#region GetAnimeReviews

		/// <summary>
		/// Returns collection of anime reviews.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <returns>Collection of anime reviews.</returns>
		public async Task<AnimeReviews> GetAnimeReviews(long id)
		{
			string[] endpointParts = new string[] { JikanEndPointCategories.Anime, id.ToString(), AnimeExtension.Reviews.GetDescription() };
			return await ExecuteGetRequest<AnimeReviews>(endpointParts);
		}

		/// <summary>
		/// Returns collection of anime reviews.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <param name="page">Index of page folding 20 records (e.g. 1 will return first 20 records, 2 will return record from 21 to 40 etc.)</param>
		/// <returns>Collection of anime reviews.</returns>
		public async Task<AnimeReviews> GetAnimeReviews(long id, int page)
		{
			string[] endpointParts = new string[] { JikanEndPointCategories.Anime, id.ToString(), AnimeExtension.Reviews.GetDescription(), page.ToString() };
			return await ExecuteGetRequest<AnimeReviews>(endpointParts);
		}

		#endregion GetAnimeReviews

		#region GetAnimeUserUpdates

		/// <summary>
		/// Returns collection of anime user updates.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <returns>Collection of anime user updates.</returns>
		public async Task<AnimeUserUpdates> GetAnimeUserUpdates(long id)
		{
			string[] endpointParts = new string[] { JikanEndPointCategories.Anime, id.ToString(), AnimeExtension.UserUpdates.GetDescription() };
			return await ExecuteGetRequest<AnimeUserUpdates>(endpointParts);
		}

		/// <summary>
		/// Returns collection of anime user updates.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <param name="page">Index of page folding 75 records (e.g. 1 will return first 75 records, 2 will return record from 76 to 150 etc.)</param>
		/// <returns>Collection of anime user updates.</returns>
		public async Task<AnimeUserUpdates> GetAnimeUserUpdates(long id, int page)
		{
			string[] endpointParts = new string[] { JikanEndPointCategories.Anime, id.ToString(), AnimeExtension.UserUpdates.GetDescription(), page.ToString() };
			return await ExecuteGetRequest<AnimeUserUpdates>(endpointParts);
		}

		#endregion GetAnimeUserUpdates

		#endregion Anime methods

		#region Character methods

		#region GetCharacter

		/// <summary>
		/// Return character with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of character.</param>
		/// <returns>Character with given MAL id.</returns>
		public async Task<Character> GetCharacter(long id)
		{
			string[] endpointParts = new string[] { JikanEndPointCategories.Character, id.ToString() };
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
			string[] endpointParts = new string[] { JikanEndPointCategories.Character, id.ToString(), CharacterExtension.Pictures.GetDescription() };
			return await ExecuteGetRequest<CharacterPictures>(endpointParts);
		}

		#endregion GetCharacterPictures

		#endregion Character methods

		#region Manga methods

		#region GetManga

		/// <summary>
		/// Return manga with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of manga.</param>
		/// <returns>Manga with given MAL id.</returns>
		public async Task<Manga> GetManga(long id)
		{
			string[] endpointParts = new string[] { JikanEndPointCategories.Manga, id.ToString() };
			return await ExecuteGetRequest<Manga>(endpointParts);
		}

		#endregion GetManga

		#region GetMangaPictures

		/// <summary>
		/// Return collections of links to pictures related to manga with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of manga.</param>
		/// <returns>Collections of links to pictures related to manga with given MAL id.</returns>
		public async Task<MangaPictures> GetMangaPictures(long id)
		{
			string[] endpointParts = new string[] { JikanEndPointCategories.Manga, id.ToString(), MangaExtension.Pictures.GetDescription() };
			return await ExecuteGetRequest<MangaPictures>(endpointParts);
		}

		#endregion GetMangaPictures

		#region GetMangaCharacters

		/// <summary>
		/// Return collections of characters appearing in manga with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of manga.</param>
		/// <returns>Collections of characters appearing in manga with given MAL id.</returns>
		public async Task<MangaCharacters> GetMangaCharacters(long id)
		{
			string[] endpointParts = new string[] { JikanEndPointCategories.Manga, id.ToString(), MangaExtension.Characters.GetDescription() };
			return await ExecuteGetRequest<MangaCharacters>(endpointParts);
		}

		#endregion GetMangaCharacters

		#region GetMangaGenre

		/// <summary>
		/// Return information about manga genre.
		/// </summary>
		/// <param name="genreId">Id of the searched genre.</param>
		/// <returns>Information about manga genre</returns>
		public async Task<MangaGenre> GetMangaGenre(long genreId)
		{
			string[] endpointParts = new string[] { JikanEndPointCategories.Genre, JikanEndPointCategories.Manga, genreId.ToString() };
			return await ExecuteGetRequest<MangaGenre>(endpointParts);
		}

		/// <summary>
		/// Return information about manga genre.
		/// </summary>
		/// <param name="genre">Searched genre.</param>
		/// <returns>Information about manga genre</returns>
		public async Task<MangaGenre> GetMangaGenre(GenreSearch genre)
		{
			string[] endpointParts = new string[] { JikanEndPointCategories.Genre, JikanEndPointCategories.Manga, genre.GetDescription() };
			return await ExecuteGetRequest<MangaGenre>(endpointParts);
		}

		/// <summary>
		/// Return information about manga genre.
		/// </summary>
		/// <param name="genreId">Id of the searched genre.</param>
		/// <param name="page">Index of page folding 100 records of top ranging (e.g. 1 will return first 100 records, 2 will return record from 101 to 200 etc.)</param>
		/// <returns>Information about manga genre</returns>
		public async Task<MangaGenre> GetMangaGenre(long genreId, int page)
		{
			string[] endpointParts = new string[] { JikanEndPointCategories.Genre, JikanEndPointCategories.Manga, genreId.ToString(), page.ToString() };
			return await ExecuteGetRequest<MangaGenre>(endpointParts);
		}

		/// <summary>
		/// Return information about manga genre.
		/// </summary>
		/// <param name="genre">Searched genre.</param>
		/// <param name="page">Index of page folding 100 records of top ranging (e.g. 1 will return first 100 records, 2 will return record from 101 to 200 etc.)</param>
		/// <returns>Information about manga genre</returns>
		public async Task<MangaGenre> GetMangaGenre(GenreSearch genre, int page)
		{
			string[] endpointParts = new string[] { JikanEndPointCategories.Genre, JikanEndPointCategories.Manga, genre.GetDescription(), page.ToString() };
			return await ExecuteGetRequest<MangaGenre>(endpointParts);
		}

		#endregion GetMangaGenre

		#region GetMangaNews

		/// <summary>
		/// Return collections of news related to manga with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of manga.</param>
		/// <returns>Collections of news related to manga with given MAL id.</returns>
		public async Task<MangaNews> GetMangaNews(long id)
		{
			string[] endpointParts = new string[] { JikanEndPointCategories.Manga, id.ToString(), MangaExtension.News.GetDescription() };
			return await ExecuteGetRequest<MangaNews>(endpointParts);
		}

		#endregion GetMangaNews

		#region GetMangaStatistics

		/// <summary>
		/// Return statistics related to manga with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of manga.</param>
		/// <returns>Statistics related to manga with given MAL id.</returns>
		public async Task<MangaStats> GetMangaStatistics(long id)
		{
			string[] endpointParts = new string[] { JikanEndPointCategories.Manga, id.ToString(), MangaExtension.Stats.GetDescription() };
			return await ExecuteGetRequest<MangaStats>(endpointParts);
		}

		#endregion GetMangaStatistics

		#region GetMangaForumTopics

		/// <summary>
		/// Return collections of forum topics related to manga with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of manga.</param>
		/// <returns>Collections of forum topics related to manga with given MAL id.</returns>
		public async Task<ForumTopics> GetMangaForumTopics(long id)
		{
			string[] endpointParts = new string[] { JikanEndPointCategories.Manga, id.ToString(), MangaExtension.Forum.GetDescription() };
			return await ExecuteGetRequest<ForumTopics>(endpointParts);
		}

		#endregion GetMangaForumTopics

		#region GetMangaMoreInfo

		/// <summary>
		/// Return additional information related to manga with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of manga.</param>
		/// <returns>Additional information related to manga with given MAL id.</returns>
		public async Task<MoreInfo> GetMangaMoreInfo(long id)
		{
			string[] endpointParts = new string[] { JikanEndPointCategories.Manga, id.ToString(), AnimeExtension.MoreInfo.GetDescription() };
			return await ExecuteGetRequest<MoreInfo>(endpointParts);
		}

		#endregion GetMangaMoreInfo

		#region GetMangaRecommendations

		/// <summary>
		/// Returns collection of manga recommendation.
		/// </summary>
		/// <param name="id">MAL id of manga.</param>
		/// <returns>Collection of manga recomendation.</returns>
		public async Task<Recommendations> GetMangaRecommendations(long id)
		{
			string[] endpointParts = new string[] { JikanEndPointCategories.Manga, id.ToString(), MangaExtension.Recommendations.GetDescription() };
			return await ExecuteGetRequest<Recommendations>(endpointParts);
		}

		#endregion GetMangaRecommendations

		#region GetMangaReviews

		/// <summary>
		/// Returns collection of manga reviews.
		/// </summary>
		/// <param name="id">MAL id of manga.</param>
		/// <returns>Collection of manga reviews.</returns>
		public async Task<MangaReviews> GetMangaReviews(long id)
		{
			string[] endpointParts = new string[] { JikanEndPointCategories.Manga, id.ToString(), MangaExtension.Reviews.GetDescription() };
			return await ExecuteGetRequest<MangaReviews>(endpointParts);
		}

		/// <summary>
		/// Returns collection of manga reviews.
		/// </summary>
		/// <param name="id">MAL id of manga.</param>
		/// <param name="page">Index of page folding 20 records of top ranging (e.g. 1 will return first 20 records, 2 will return record from 21 to 40 etc.)</param>
		/// <returns>Collection of manga reviews.</returns>
		public async Task<MangaReviews> GetMangaReviews(long id, int page)
		{
			string[] endpointParts = new string[] { JikanEndPointCategories.Manga, id.ToString(), MangaExtension.Reviews.GetDescription(), page.ToString() };
			return await ExecuteGetRequest<MangaReviews>(endpointParts);
		}

		#endregion GetMangaReviews

		#region GetMangaUserUpdates

		/// <summary>
		/// Returns collection of manga user updates.
		/// </summary>
		/// <param name="id">MAL id of manga.</param>
		/// <returns>Collection of manga user updates.</returns>
		public async Task<MangaUserUpdates> GetMangaUserUpdates(long id)
		{
			string[] endpointParts = new string[] { JikanEndPointCategories.Manga, id.ToString(), MangaExtension.UserUpdates.GetDescription() };
			return await ExecuteGetRequest<MangaUserUpdates>(endpointParts);
		}

		/// <summary>
		/// Returns collection of manga user updates.
		/// </summary>
		/// <param name="id">MAL id of manga.</param>
		/// <param name="page">Index of page folding 75 records of top ranging (e.g. 1 will return first 75 records, 2 will return record from 76 to 150 etc.)</param>
		/// <returns>Collection of manga user updates.</returns>
		public async Task<MangaUserUpdates> GetMangaUserUpdates(long id, int page)
		{
			string[] endpointParts = new string[] { JikanEndPointCategories.Manga, id.ToString(), MangaExtension.UserUpdates.GetDescription(), page.ToString() };
			return await ExecuteGetRequest<MangaUserUpdates>(endpointParts);
		}

		#endregion GetMangaUserUpdates

		#endregion Manga methods

		#region Person methods

		#region GetPerson

		/// <summary>
		/// Return person with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of person.</param>
		/// <returns>Person with given MAL id.</returns>
		public async Task<Person> GetPerson(long id)
		{
			string[] endpointParts = new string[] { JikanEndPointCategories.Person, id.ToString() };
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
			string[] endpointParts = new string[] { JikanEndPointCategories.Person, id.ToString(), PersonExtension.Pictures.GetDescription() };
			return await ExecuteGetRequest<PersonPictures>(endpointParts);
		}

		#endregion GetPersonPictures

		#endregion Person methods

		#region Schedule methods

		#region GetSchedule

		/// <summary>
		/// Return current season schedule.
		/// </summary>
		/// <returns>Current season schedule.</returns>
		public async Task<Schedule> GetSchedule()
		{
			string[] endpointParts = new string[] { JikanEndPointCategories.Schedule };
			return await ExecuteGetRequest<Schedule>(endpointParts);
		}

		/// <summary>
		/// Return current season schedule.
		/// </summary>
		/// <param name="scheduledDay">Scheduled day to filter by.</param>
		/// <returns>Current season schedule.</returns>
		public async Task<Schedule> GetSchedule(ScheduledDay scheduledDay)
		{
			string[] endpointParts = new string[] { JikanEndPointCategories.Schedule, scheduledDay.GetDescription() };
			return await ExecuteGetRequest<Schedule>(endpointParts);
		}

		#endregion GetSchedule

		#endregion Schedule methods

		#region Season methods

		#region GetSeason

		/// <summary>
		/// Return current season preview.
		/// </summary>
		/// <returns>Current season preview.</returns>
		public async Task<Season> GetSeason()
		{
			string[] endpointParts = new string[] { JikanEndPointCategories.Season };
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
			string[] endpointParts = new string[] { JikanEndPointCategories.Season, year.ToString(), season.GetDescription() };
			return await ExecuteGetRequest<Season>(endpointParts);
		}

		#endregion GetSeason

		#region GetSeasonArchive

		/// <summary>
		/// Return list of availaible season to query with <see cref="GetSeason(int, Seasons)"/>
		/// </summary>
		/// <returns></returns>
		public async Task<SeasonArchives> GetSeasonArchive()
		{
			string[] endpointParts = new string[] { JikanEndPointCategories.Season, SeasonExtension.Archive.GetDescription() };
			return await ExecuteGetRequest<SeasonArchives>(endpointParts);
		}

		#endregion GetSeasonArchive

		#region GetSeasonLater

		/// <summary>
		/// Return season preview for anime with undefined airing season (marked as "Later" on MAL).
		/// </summary>
		/// <returns>Season preview for anime with undefined airing date.</returns>
		public async Task<Season> GetSeasonLater()
		{
			string[] endpointParts = new string[] { JikanEndPointCategories.Season, SeasonExtension.Later.GetDescription() };
			return await ExecuteGetRequest<Season>(endpointParts);
		}

		#endregion GetSeasonLater

		#endregion Season methods

		#region Top methods

		#region GetAnimeTop

		/// <summary>
		/// Return list of top anime.
		/// </summary>
		/// <returns>List of top anime.</returns>
		public async Task<AnimeTop> GetAnimeTop()
		{
			string[] endpointParts = new string[] { JikanEndPointCategories.TopList, JikanEndPointCategories.Anime };
			return await ExecuteGetRequest<AnimeTop>(endpointParts);
		}

		/// <summary>
		/// Return list of top anime.
		/// </summary>
		/// <param name="page">Indexx of page folding 50 records of top ranging (e.g. 1 will return first 50 records, 2 will return record from 51 to 100 etc.)</param>
		/// <returns>List of top anime.</returns>
		public async Task<AnimeTop> GetAnimeTop(int page)
		{
			string[] endpointParts = new string[] { JikanEndPointCategories.TopList, JikanEndPointCategories.Anime, page.ToString() };
			return await ExecuteGetRequest<AnimeTop>(endpointParts);
		}

		/// <summary>
		/// Return list of top anime.
		/// </summary>
		/// <param name="extension">Extension for specific type of ranking.</param>
		/// <returns>List of top anime.</returns>
		public async Task<AnimeTop> GetAnimeTop(TopAnimeExtension extension)
		{
			string[] endpointParts = new string[] { JikanEndPointCategories.TopList, JikanEndPointCategories.Anime, "1", extension.GetDescription() };
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
			string[] endpointParts = new string[] { JikanEndPointCategories.TopList, JikanEndPointCategories.Anime, page.ToString(), extension.GetDescription() };
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
			string[] endpointParts = new string[] { JikanEndPointCategories.TopList, JikanEndPointCategories.Manga };
			return await ExecuteGetRequest<MangaTop>(endpointParts);
		}

		/// <summary>
		/// Return list of top manga.
		/// </summary>
		/// <param name="page">Indexx of page folding 50 records of top ranging (e.g. 1 will return first 50 records, 2 will return record from 51 to 100 etc.)</param>
		/// <returns>List of top manga.</returns>
		public async Task<MangaTop> GetMangaTop(int page)
		{
			string[] endpointParts = new string[] { JikanEndPointCategories.TopList, JikanEndPointCategories.Manga, page.ToString() };
			return await ExecuteGetRequest<MangaTop>(endpointParts);
		}

		/// <summary>
		/// Return list of top manga.
		/// </summary>
		/// <param name="extension">Extension for specific type of ranking.</param>
		/// <returns>List of top manga.</returns>
		public async Task<MangaTop> GetMangaTop(TopMangaExtension extension)
		{
			string[] endpointParts = new string[] { JikanEndPointCategories.TopList, JikanEndPointCategories.Manga, "1", extension.GetDescription() };
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
			string[] endpointParts = new string[] { JikanEndPointCategories.TopList, JikanEndPointCategories.Manga, page.ToString(), extension.GetDescription() };
			return await ExecuteGetRequest<MangaTop>(endpointParts);
		}

		#endregion GetMangaTop

		#region GetPeopleTop

		/// <summary>
		/// Return list of most popular people.
		/// </summary>
		/// <returns>List of most popular people.</returns>
		public async Task<PeopleTop> GetPeopleTop()
		{
			string[] endpointParts = new string[] { JikanEndPointCategories.TopList, JikanEndPointCategories.People };
			return await ExecuteGetRequest<PeopleTop>(endpointParts);
		}

		/// <summary>
		/// Return list of most popular people.
		/// </summary>
		/// <param name="page">Indexx of page folding 50 records of top ranging (e.g. 1 will return first 50 records, 2 will return record from 51 to 100 etc.)</param>
		/// <returns>List of most popular people.</returns>
		public async Task<PeopleTop> GetPeopleTop(int page)
		{
			string[] endpointParts = new string[] { JikanEndPointCategories.TopList, JikanEndPointCategories.People, page.ToString() };
			return await ExecuteGetRequest<PeopleTop>(endpointParts);
		}

		#endregion GetPeopleTop

		#region GetCharactersTop

		/// <summary>
		/// Return list of most popular characters.
		/// </summary>
		/// <returns>List of most popular characters.</returns>
		public async Task<CharactersTop> GetCharactersTop()
		{
			string[] endpointParts = new string[] { JikanEndPointCategories.TopList, JikanEndPointCategories.Characters };
			return await ExecuteGetRequest<CharactersTop>(endpointParts);
		}

		/// <summary>
		/// Return list of most popular characters.
		/// </summary>
		/// <param name="page">Index of page folding 50 records of top ranging (e.g. 1 will return first 50 records, 2 will return record from 51 to 100 etc.)</param>
		/// <returns>List of most popular characters.</returns>
		public async Task<CharactersTop> GetCharactersTop(int page)
		{
			string[] endpointParts = new string[] { JikanEndPointCategories.TopList, JikanEndPointCategories.Characters, page.ToString() };
			return await ExecuteGetRequest<CharactersTop>(endpointParts);
		}

		#endregion GetCharactersTop

		#endregion Top methods

		#region Producer methods

		#region GetProducer

		/// <summary>
		/// Returns information about producer with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of the producer.</param>
		/// <returns>Information about producer with given MAL id. </returns>
		public async Task<Producer> GetProducer(long id)
		{
			string[] endpointParts = new string[] { JikanEndPointCategories.Producer, id.ToString() };
			return await ExecuteGetRequest<Producer>(endpointParts);
		}

		/// <summary>
		/// Returns information about producer with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of the producer.</param>
		/// <param name="page">Index of page folding 100 records of top ranging (e.g. 1 will return first 100 records, 2 will return record from 101 to 200 etc.)</param>
		/// <returns>Information about producer with given MAL id. </returns>
		public async Task<Producer> GetProducer(long id, int page)
		{
			string[] endpointParts = new string[] { JikanEndPointCategories.Producer, id.ToString(), page.ToString() };
			return await ExecuteGetRequest<Producer>(endpointParts);
		}

		#endregion GetProducer

		#endregion Producer methods

		#region Magazine methods

		#region GetMagazine

		/// <summary>
		/// Returns information about magazine with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of the magazine.</param>
		/// <returns>Information about magazine with given MAL id. </returns>
		public async Task<Magazine> GetMagazine(long id)
		{
			string[] endpointParts = new string[] { JikanEndPointCategories.Magazine, id.ToString() };
			return await ExecuteGetRequest<Magazine>(endpointParts);
		}

		/// <summary>
		/// Returns information about magazine with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of the magazine.</param>
		/// <param name="page">Index of page folding 100 records of top ranging (e.g. 1 will return first 100 records, 2 will return record from 101 to 200 etc.)</param>
		/// <returns>Information about magazine with given MAL id. </returns>
		public async Task<Magazine> GetMagazine(long id, int page)
		{
			string[] endpointParts = new string[] { JikanEndPointCategories.Magazine, id.ToString(), page.ToString() };
			return await ExecuteGetRequest<Magazine>(endpointParts);
		}

		#endregion GetMagazine

		#endregion Magazine methods

		#region User methods

		#region GetUserProfile

		/// <summary>
		/// Returns information about user's profile with given username.
		/// </summary>
		/// <param name="username">Username.</param>
		/// <returns>Information about user's profile with given username.</returns>
		public async Task<UserProfile> GetUserProfile(string username)
		{
			string[] endpointParts = new string[] { JikanEndPointCategories.User, username, UserExtension.Profile.GetDescription() };
			return await ExecuteGetRequest<UserProfile>(endpointParts);
		}

		#endregion GetUserProfile

		#region GetUserHistory

		/// <summary>
		/// Returns information about user's history with given username.
		/// </summary>
		/// <param name="username">Username.</param>
		/// <returns>Information about user's profile with given username.</returns>
		public async Task<UserHistory> GetUserHistory(string username)
		{
			string[] endpointParts = new string[] { JikanEndPointCategories.User, username, UserExtension.History.GetDescription() };
			return await ExecuteGetRequest<UserHistory>(endpointParts);
		}

		/// <summary>
		/// Returns information about user's history with given username.
		/// </summary>
		/// <param name="username">Username.</param>
		/// <param name="historyExtension">Option to filter history.</param>
		/// <returns>Information about user's profile with given username.</returns>
		public async Task<UserHistory> GetUserHistory(string username, UserHistoryExtension historyExtension)
		{
			string[] endpointParts = new string[] { JikanEndPointCategories.User, username, UserExtension.History.GetDescription(), historyExtension.GetDescription() };
			return await ExecuteGetRequest<UserHistory>(endpointParts);
		}

		#endregion GetUserHistory

		#region GetUserAnimeList

		/// <summary>
		/// Returns entries on user's anime list.
		/// </summary>
		/// <param name="username">Username.</param>
		/// <returns>Entries on user's anime list.</returns>
		public async Task<UserAnimeList> GetUserAnimeList(string username)
		{
			string[] endpointParts = new string[] { JikanEndPointCategories.User, username, UserExtension.AnimeList.GetDescription() };
			return await ExecuteGetRequest<UserAnimeList>(endpointParts);
		}

		/// <summary>
		/// Returns entries on user's anime list.
		/// </summary>
		/// <param name="username">Username.</param>
		/// <param name="page">Index of page folding 300 records of top ranging (e.g. 1 will return first 300 records, 2 will return record from 301 to 600 etc.)</param>
		/// <returns>Entries on user's anime list.</returns>
		public async Task<UserAnimeList> GetUserAnimeList(string username, int page)
		{
			string[] endpointParts = new string[] { JikanEndPointCategories.User, username, UserExtension.AnimeList.GetDescription(), UserAnimeListExtension.All.GetDescription(), page.ToString() };
			return await ExecuteGetRequest<UserAnimeList>(endpointParts);
		}

		/// <summary>
		/// Returns entries on user's anime list.
		/// </summary>
		/// <param name="username">Username.</param>
		/// <param name="filter">Option to filter list.</param>
		/// <returns>Entries on user's anime list.</returns>
		public async Task<UserAnimeList> GetUserAnimeList(string username, UserAnimeListExtension filter)
		{
			string[] endpointParts = new string[] { JikanEndPointCategories.User, username, UserExtension.AnimeList.GetDescription(), filter.GetDescription() };
			return await ExecuteGetRequest<UserAnimeList>(endpointParts);
		}

		/// <summary>
		/// Returns entries on user's anime list.
		/// </summary>
		/// <param name="username">Username.</param>
		/// <param name="filter">Option to filter list.</param>
		/// <param name="page">Index of page folding 300 records of top ranging (e.g. 1 will return first 300 records, 2 will return record from 301 to 600 etc.)</param>
		/// <returns>Entries on user's anime list.</returns>
		public async Task<UserAnimeList> GetUserAnimeList(string username, UserAnimeListExtension filter, int page)
		{
			var query = string.Concat(UserExtension.AnimeList.GetDescription(), filter.GetDescription());
			string[] endpointParts = new string[] { JikanEndPointCategories.User, username, query, page.ToString() };
			return await ExecuteGetRequest<UserAnimeList>(endpointParts);
		}

		/// <summary>
		/// Returns entries on user's anime list.
		/// </summary>
		/// <param name="username">Username.</param>
		/// <param name="searchConfig">Config to modify request input parameters.</param>
		/// <returns>Entries on user's anime list.</returns>
		public async Task<UserAnimeList> GetUserAnimeList(string username, UserListAnimeSearchConfig searchConfig)
		{
			var query = string.Concat(UserExtension.AnimeList.GetDescription(), searchConfig.ConfigToString());
			string[] endpointParts = new string[] { JikanEndPointCategories.User, username, query };
			return await ExecuteGetRequest<UserAnimeList>(endpointParts);
		}

		#endregion GetUserAnimeList

		#region GetUserMangaList

		/// <summary>
		/// Returns entries on user's manga list.
		/// </summary>
		/// <param name="username">Username.</param>
		/// <returns>Entries on user's manga list.</returns>
		public async Task<UserMangaList> GetUserMangaList(string username)
		{
			string[] endpointParts = new string[] { JikanEndPointCategories.User, username, UserExtension.MangaList.GetDescription() };
			return await ExecuteGetRequest<UserMangaList>(endpointParts);
		}

		/// <summary>
		/// Returns entries on user's manga list.
		/// </summary>
		/// <param name="username">Username.</param>
		/// <param name="page">Index of page folding 300 records of top ranging (e.g. 1 will return first 300 records, 2 will return record from 301 to 600 etc.)</param>
		/// <returns>Entries on user's manga list.</returns>
		public async Task<UserMangaList> GetUserMangaList(string username, int page)
		{
			string[] endpointParts = new string[] { JikanEndPointCategories.User, username, UserExtension.MangaList.GetDescription(), UserMangaListExtension.All.GetDescription(), page.ToString() };
			return await ExecuteGetRequest<UserMangaList>(endpointParts);
		}

		/// <summary>
		/// Returns entries on user's manga list.
		/// </summary>
		/// <param name="username">Username.</param>
		/// <param name="filter">Option to filter list.</param>
		/// <returns>Entries on user's manga list.</returns>
		public async Task<UserMangaList> GetUserMangaList(string username, UserMangaListExtension filter)
		{
			string[] endpointParts = new string[] { JikanEndPointCategories.User, username, UserExtension.MangaList.GetDescription(), filter.GetDescription() };
			return await ExecuteGetRequest<UserMangaList>(endpointParts);
		}

		/// <summary>
		/// Returns entries on user's manga list.
		/// </summary>
		/// <param name="username">Username.</param>
		/// <param name="filter">Option to filter list.</param>
		/// <param name="page">Index of page folding 300 records of top ranging (e.g. 1 will return first 300 records, 2 will return record from 301 to 600 etc.)</param>
		/// <returns>Entries on user's manga list.</returns>
		public async Task<UserMangaList> GetUserMangaList(string username, UserMangaListExtension filter, int page)
		{
			var query = string.Concat(UserExtension.MangaList.GetDescription(), filter.GetDescription());
			string[] endpointParts = new string[] { JikanEndPointCategories.User, username, query, page.ToString() };
			return await ExecuteGetRequest<UserMangaList>(endpointParts);
		}

		/// <summary>
		/// Returns entries on user's manga list.
		/// </summary>
		/// <param name="username">Username.</param>
		/// <param name="searchConfig">Config to modify request input parameters.</param>
		/// <returns>Entries on user's manga list.</returns>
		public async Task<UserMangaList> GetUserMangaList(string username, UserListMangaSearchConfig searchConfig)
		{
			var query = string.Concat(UserExtension.MangaList.GetDescription(), searchConfig.ConfigToString());
			string[] endpointParts = new string[] { JikanEndPointCategories.User, username, query };
			return await ExecuteGetRequest<UserMangaList>(endpointParts);
		}

		#endregion GetUserMangaList

		#region GetUserFriend

		/// <summary>
		/// Returns information about user's friends with given username.
		/// </summary>
		/// <param name="username">Username.</param>
		/// <returns>Information about user's friends with given username.</returns>
		public async Task<UserFriends> GetUserFriends(string username)
		{
			string[] endpointParts = new string[] { JikanEndPointCategories.User, username, UserExtension.Friends.GetDescription() };
			return await ExecuteGetRequest<UserFriends>(endpointParts);
		}

		/// <summary>
		/// Returns information about user's friends with given username.
		/// </summary>
		/// <param name="username">Username.</param>
		/// <param name="page">Index of the page.</param>
		/// <returns>Information about user's friends with given username.</returns>
		public async Task<UserFriends> GetUserFriends(string username, int page)
		{
			string[] endpointParts = new string[] { JikanEndPointCategories.User, username, UserExtension.Friends.GetDescription(), page.ToString() };
			return await ExecuteGetRequest<UserFriends>(endpointParts);
		}

		#endregion GetUserFriend

		#endregion User methods

		#region Club Methods

		#region GetClub

		/// <summary>
		/// Return club's profile information.
		/// </summary>
		/// <param name="id">MAL id of the club.</param>
		/// <returns>Club's profile information.</returns>
		public async Task<Club> GetClub(long id)
		{
			string[] endpointParts = new string[] { JikanEndPointCategories.Club, id.ToString() };
			return await ExecuteGetRequest<Club>(endpointParts);
		}

		#endregion

		#region GetClubMembers

		/// <summary>
		/// Return club's member list.
		/// </summary>
		/// <param name="id">MAL id of the club.</param>
		/// <returns>Club's member list.</returns>
		public async Task<ClubMembers> GetClubMembers(long id)
		{
			string[] endpointParts = new string[] { JikanEndPointCategories.Club, id.ToString(), ClubExtensions.Members.GetDescription() };
			return await ExecuteGetRequest<ClubMembers>(endpointParts);
		}

		/// <summary>
		/// Return club's member list.
		/// </summary>
		/// <param name="id">MAL id of the club.</param>
		/// <param name="page">Index of page folding 36 records of top ranging (e.g. 1 will return first 36 records, 2 will return record from 37 to 72 etc.)</param>
		/// <returns>Club's member list.</returns>
		public async Task<ClubMembers> GetClubMembers(long id, int page)
		{
			string[] endpointParts = new string[] { JikanEndPointCategories.Club, id.ToString(), ClubExtensions.Members.GetDescription(), page.ToString() };
			return await ExecuteGetRequest<ClubMembers>(endpointParts);
		}

		#endregion

		#endregion

		#region Search methods

		#region SearchAnime

		/// <summary>
		/// Return list of results related to search.
		/// </summary>
		/// <param name="query">Search query.</param>
		/// <returns>List of result related to search query.</returns>
		public async Task<AnimeSearchResult> SearchAnime(string query)
		{
			query = string.Concat(JikanEndPointCategories.Anime, "?q=", query.Replace(' ', '+'));
			string[] endpointParts = new string[] { JikanEndPointCategories.Search, query };
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
			query = string.Concat(JikanEndPointCategories.Anime, "/", page.ToString(), "?q=", query.Replace(' ', '+'));
			string[] endpointParts = new string[] { JikanEndPointCategories.Search, query };
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
			query = string.Concat(JikanEndPointCategories.Anime, "?q=", query.Replace(' ', '+'), "&", searchConfig.ConfigToString());
			string[] endpointParts = new string[] { JikanEndPointCategories.Search, query };
			return await ExecuteGetRequest<AnimeSearchResult>(endpointParts);
		}

		/// <summary>
		/// Returns list of results related to search.
		/// </summary>
		/// <param name="searchConfig">Additional configuration for advanced search.</param>
		/// <returns>List of result related to search query.</returns>
		public async Task<AnimeSearchResult> SearchAnime(AnimeSearchConfig searchConfig)
		{
			var query = string.Concat(JikanEndPointCategories.Anime, "?", searchConfig.ConfigToString());
			string[] endpointParts = new string[] { JikanEndPointCategories.Search, query };
			return await ExecuteGetRequest<AnimeSearchResult>(endpointParts);
		}

		/// <summary>
		/// Returns list of results related to search.
		/// </summary>
		/// <param name="page">Index of page folding 50 records of top ranging (e.g. 1 will return first 50 records, 2 will return record from 51 to 100 etc.)</param>
		/// <param name="searchConfig">Additional configuration for advanced search.</param>
		/// <returns>List of result related to search query.</returns>
		public async Task<AnimeSearchResult> SearchAnime(AnimeSearchConfig searchConfig, int page)
		{
			var query = string.Concat(JikanEndPointCategories.Anime, "/", page.ToString(), "?", searchConfig.ConfigToString());
			string[] endpointParts = new string[] { JikanEndPointCategories.Search, query };
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
			query = string.Concat(JikanEndPointCategories.Anime, "/", page.ToString(), "?q=", query.Replace(' ', '+'), "&", searchConfig.ConfigToString());
			string[] endpointParts = new string[] { JikanEndPointCategories.Search, query };
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
			query = string.Concat(JikanEndPointCategories.Manga, "?q=", query.Replace(' ', '+'));
			string[] endpointParts = new string[] { JikanEndPointCategories.Search, query };
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
			query = string.Concat(JikanEndPointCategories.Manga, "/", page.ToString(), "?q=", query.Replace(' ', '+'));
			string[] endpointParts = new string[] { JikanEndPointCategories.Search, query};
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
			query = string.Concat(JikanEndPointCategories.Manga, "?q=", query.Replace(' ', '+'), "&", searchConfig.ConfigToString());
			string[] endpointParts = new string[] { JikanEndPointCategories.Search, query };
			return await ExecuteGetRequest<MangaSearchResult>(endpointParts);
		}

		/// <summary>
		/// Returns list of results related to search.
		/// </summary>
		/// <param name="searchConfig">Additional configuration for advanced search.</param>
		/// <returns>List of result related to search query.</returns>
		public async Task<MangaSearchResult> SearchManga(MangaSearchConfig searchConfig)
		{
			var query = string.Concat(JikanEndPointCategories.Manga, "?", searchConfig.ConfigToString());
			string[] endpointParts = new string[] { JikanEndPointCategories.Search, query };
			return await ExecuteGetRequest<MangaSearchResult>(endpointParts);
		}

		/// <summary>
		/// Returns list of results related to search.
		/// </summary>
		/// <param name="page">Index of page folding 50 records of top ranging (e.g. 1 will return first 50 records, 2 will return record from 51 to 100 etc.)</param>
		/// <param name="searchConfig">Additional configuration for advanced search.</param>
		/// <returns>List of result related to search query.</returns>
		public async Task<MangaSearchResult> SearchManga(MangaSearchConfig searchConfig, int page)
		{
			var query = string.Concat(JikanEndPointCategories.Manga, "/", page.ToString(), "?", searchConfig.ConfigToString());
			string[] endpointParts = new string[] { JikanEndPointCategories.Search, query };
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
			query = string.Concat(JikanEndPointCategories.Manga, "/", page.ToString(), "?q=", query.Replace(' ', '+'), "&", searchConfig.ConfigToString());
			string[] endpointParts = new string[] { JikanEndPointCategories.Search, query, page.ToString() };
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
			query = string.Concat(JikanEndPointCategories.Person, "?q=", query.Replace(' ', '+'));
			string[] endpointParts = new string[] { JikanEndPointCategories.Search, query };
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
			query = string.Concat(JikanEndPointCategories.Person, "/", page.ToString(), "?q=",  query.Replace(' ', '+'));
			string[] endpointParts = new string[] { JikanEndPointCategories.Search, query};
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
			query = string.Concat(JikanEndPointCategories.Character, "?q=", query.Replace(' ', '+'));
			string[] endpointParts = new string[] { JikanEndPointCategories.Search, query };
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
			query = string.Concat(JikanEndPointCategories.Character, "/", page.ToString(), "?q=", query.Replace(' ', '+'));
			string[] endpointParts = new string[] { JikanEndPointCategories.Search, query};
			return await ExecuteGetRequest<CharacterSearchResult>(endpointParts);
		}

		#endregion SearchCharacter

		#endregion Search methods

		#region Metadata methods

		#region GetStatusMetadata

		/// <summary>
		/// Return Jikan REST API metadata - status.
		/// </summary>
		/// <returns>Jikan REST API metadata - status.</returns>
		public async Task<StatusMetadata> GetStatusMetadata()
		{
			string[] endpointParts = new string[] { JikanEndPointCategories.Meta, "status" };
			return await ExecuteGetRequest<StatusMetadata>(endpointParts);
		}
		#endregion GetStatusMetadata

		#endregion Metadata methods

		#endregion Public Methods
	}
}
