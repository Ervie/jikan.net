using System.Threading.Tasks;

namespace JikanDotNet
{
	/// <summary>
	/// Interface for Jikan.net client
	/// </summary>
	public interface IJikan
	{
		#region Anime requests

		#region GetAnime

		/// <summary>
		/// Returns anime with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <returns>Anime with given MAL id.</returns>
		Task<Anime> GetAnime(long id);

		#endregion

		#region GetAnimeEpisodes

		/// <summary>
		/// Returns list of episodes for anime with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <returns>List of episodes with details.</returns>
		Task<AnimeEpisodes> GetAnimeEpisodes(long id);

		/// <summary>
		/// Returns list of episodes for anime with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <param name="page">Index of page folding 50 records of top ranging (e.g. 1 will return first 50 records, 2 will return record from 51 to 100 etc.)</param>
		/// <returns>List of episodes with details.</returns>
		Task<AnimeEpisodes> GetAnimeEpisodes(long id, int page);

		#endregion

		#region GetAnimeCharactersStaff

		/// <summary>
		/// Returns collections of characters and staff of anime with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <returns>Collections of characters and staff of anime with given MAL id.</returns>
		Task<AnimeCharactersStaff> GetAnimeCharactersStaff(long id);

		#endregion

		#region GetAnimePictures

		/// <summary>
		/// Returns collections of links to pictures related to anime with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <returns>Collections of links to pictures related to anime with given MAL id.</returns>
		Task<AnimePictures> GetAnimePictures(long id);

		#endregion

		#region GetAnimeNews

		/// <summary>
		/// Returns collections of news related to anime with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <returns>Collections of news related to anime with given MAL id.</returns>
		Task<AnimeNews> GetAnimeNews(long id);

		#endregion

		#region GetAnimeVideos

		/// <summary>
		/// Returns collections of videos related to anime with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <returns>Collections of videos related to anime with given MAL id.</returns>
		Task<AnimeVideos> GetAnimeVideos(long id);

		#endregion

		#region GetAnimeStatistics

		/// <summary>
		/// Returns statistics related to anime with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <returns>Statistics related to anime with given MAL id.</returns>
		Task<AnimeStats> GetAnimeStatistics(long id);

		#endregion

		#region GetAnimeForumTopics

		/// <summary>
		/// Returns collections of forum topics related to anime with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <returns>Collections of forum topics related to anime with given MAL id.</returns>
		Task<ForumTopics> GetAnimeForumTopics (long id);

		#endregion

		#region GetAnimeMoreInfo

		/// <summary>
		/// Returns additional information related to anime with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <returns>Additional information related to anime with given MAL id.</returns>
		Task<MoreInfo> GetAnimeMoreInfo(long id);

		#endregion

		#region GetAnimeRecommendations

		/// <summary>
		/// Returns collection of anime recommendation.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <returns>Collection of anime recomendation.</returns>
		Task<Recommendations> GetAnimeRecommendations(long id);

		#endregion

		#region GetAnimeReviews

		/// <summary>
		/// Returns collection of anime reviews.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <returns>Collection of anime reviews.</returns>
		Task<AnimeReviews> GetAnimeReviews(long id);

		/// <summary>
		/// Returns collection of anime reviews.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <param name="page">Index of page folding 20 records of top ranging (e.g. 1 will return first 20 records, 2 will return record from 21 to 40 etc.)</param>
		/// <returns>Collection of anime reviews.</returns>
		Task<AnimeReviews> GetAnimeReviews(long id, int page);

		#endregion

		#region GetAnimeUserUpdates

		/// <summary>
		/// Returns collection of anime user updates.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <returns>Collection of anime user updates.</returns>
		Task<AnimeUserUpdates> GetAnimeUserUpdates(long id);

		/// <summary>
		/// Returns collection of anime user updates.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <param name="page">Index of page folding 75 records of top ranging (e.g. 1 will return first 75 records, 2 will return record from 76 to 150 etc.)</param>
		/// <returns>Collection of anime user updates.</returns>
		Task<AnimeUserUpdates> GetAnimeUserUpdates(long id, int page);

		#endregion

		#endregion Anime requests

		#region Manga requests

		#region GetManga

		/// <summary>
		/// Returns manga with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of manga.</param>
		/// <returns>Manga with given MAL id.</returns>
		Task<Manga> GetManga(long id);

		#endregion

		#region GetMangaPictures

		/// <summary>
		/// Returns collections of links to pictures related to manga with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of manga.</param>
		/// <returns>Collections of links to pictures related to manga with given MAL id.</returns>
		Task<MangaPictures> GetMangaPictures(long id);

		#endregion

		#region GetMangaCharacters

		/// <summary>
		/// Returns collections of characters appearing in manga with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of manga.</param>
		/// <returns>Collections of characters appearing in manga with given MAL id.</returns>
		Task<MangaCharacters> GetMangaCharacters(long id);

		#endregion

		#region GetMangaNews

		/// <summary>
		/// Returns collections of news related to manga with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of manga.</param>
		/// <returns>Collections of news related to manga with given MAL id.</returns>
		Task<MangaNews> GetMangaNews(long id);

		#endregion

		#region GetMangaStatistics

		/// <summary>
		/// Returns statistics related to manga with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of manga.</param>
		/// <returns>Statistics related to manga with given MAL id.</returns>
		Task<MangaStats> GetMangaStatistics(long id);

		#endregion

		#region GetMangaForumTopics

		/// <summary>
		/// Returns collections of forum topics related to manga with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of manga.</param>
		/// <returns>Collections of forum topics related to manga with given MAL id.</returns>
		Task<ForumTopics> GetMangaForumTopics(long id);

		#endregion

		#region GetMangaMoreInfo

		/// <summary>
		/// Returns additional information related to manga with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of manga.</param>
		/// <returns>Collections of forum topics related to manga with given MAL id.</returns>
		Task<MoreInfo> GetMangaMoreInfo(long id);

		#endregion

		#region GetMangaRecommendations

		/// <summary>
		/// Returns collection of manga recommendation.
		/// </summary>
		/// <param name="id">MAL id of manga.</param>
		/// <returns>Collection of manga recomendation.</returns>
		Task<Recommendations> GetMangaRecommendations(long id);

		#endregion

		#region GetMangaReviews

		/// <summary>
		/// Returns collection of manga reviews.
		/// </summary>
		/// <param name="id">MAL id of manga.</param>
		/// <returns>Collection of manga reviews.</returns>
		Task<MangaReviews> GetMangaReviews(long id);

		/// <summary>
		/// Returns collection of manga reviews.
		/// </summary>
		/// <param name="id">MAL id of manga.</param>
		/// <param name="page">Index of page folding 20 records of top ranging (e.g. 1 will return first 20 records, 2 will return record from 21 to 40 etc.)</param>
		/// <returns>Collection of manga reviews.</returns>
		Task<MangaReviews> GetMangaReviews(long id, int page);

		#endregion

		#region GetMangaUserUpdates

		/// <summary>
		/// Returns collection of manga user updates.
		/// </summary>
		/// <param name="id">MAL id of manga.</param>
		/// <returns>Collection of manga user updates.</returns>
		Task<MangaUserUpdates> GetMangaUserUpdates(long id);

		/// <summary>
		/// Returns collection of manga user updates.
		/// </summary>
		/// <param name="id">MAL id of manga.</param>
		/// <param name="page">Index of page folding 75 records of top ranging (e.g. 1 will return first 75 records, 2 will return record from 76 to 150 etc.)</param>
		/// <returns>Collection of manga user updates.</returns>
		Task<MangaUserUpdates> GetMangaUserUpdates(long id, int page);

		#endregion

		#endregion Manga requests

		#region Character requests

		#region GetCharacter

		/// <summary>
		/// Returns character with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of character.</param>
		/// <returns>Character with given MAL id.</returns>
		Task<Character> GetCharacter(long id);

		#endregion

		#region GetCharacterPictures

		/// <summary>
		/// Returns collections of links to pictures related to character with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of character.</param>
		/// <returns>Collections of links to pictures related to character with given MAL id.</returns>
		Task<CharacterPictures> GetCharacterPictures(long id);

		#endregion

		#endregion Character requests

		#region Person requests

		#region GetPerson

		/// <summary>
		/// Returns person with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of person.</param>
		/// <returns>Person with given MAL id.</returns>
		Task<Person> GetPerson(long id);

		#endregion

		#region GetPersonPictures

		/// <summary>
		/// Returns collections of links to pictures related to person with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of person.</param>
		/// <returns>Collections of links to pictures related to person with given MAL id.</returns>
		Task<PersonPictures> GetPersonPictures(long id);

		#endregion

		#endregion Person requests

		#region Season requests

		#region GetSeason

		/// <summary>
		/// Returns current season preview.
		/// </summary>
		/// <returns>Current season preview.</returns>
		Task<Season> GetSeason();

		/// <summary>
		/// Returns season preview.
		/// </summary>
		/// <param name="year">Year of selected season.</param>
		/// <param name="season">Selected season.</param>
		/// <returns>Season preview.</returns>
		Task<Season> GetSeason(int year, Seasons season);

		#endregion

		#region GetSeasonArchive

		/// <summary>
		/// Returns list of availaible season to query with <see cref="GetSeason(int, Seasons)"/>
		/// </summary>
		/// <returns></returns>
		Task<SeasonArchives> GetSeasonArchive();

		#endregion

		#region GetSeasonLater

		/// <summary>
		/// Return season preview for anime with undefined airing season (marked as "Later" on MAL).
		/// </summary>
		/// <returns>Season preview for anime with undefined airing date.</returns>
		Task<Season> GetSeasonLater();

		#endregion

		#endregion Season requests

		#region Schedule requests

		#region GetSchedule

		/// <summary>
		/// Returns current season schedule.
		/// </summary>
		/// <returns>Current season schedule.</returns>
		Task<Schedule> GetSchedule();

		/// <summary>
		/// Returns current season schedule.
		/// </summary>
		/// <param name="scheduledDay">Scheduled day to filter by.</param>
		/// <returns>Current season schedule.</returns>
		Task<Schedule> GetSchedule(ScheduledDay scheduledDay);

		#endregion

		#endregion Schedule requests

		#region Top requests

		#region GetAnimeTop

		/// <summary>
		/// Returns list of top anime.
		/// </summary>
		/// <returns>List of top anime.</returns>
		Task<AnimeTop> GetAnimeTop();

		/// <summary>
		/// Returns list of top anime.
		/// </summary>
		/// <param name="page">Index of page folding 50 records of top ranging (e.g. 1 will return first 50 records, 2 will return record from 51 to 100 etc.)</param>
		/// <returns>List of top anime.</returns>
		Task<AnimeTop> GetAnimeTop(int page);

		/// <summary>
		/// Returns list of top anime.
		/// </summary>
		/// <param name="extension">Extension for specific type of ranking.</param>
		/// <returns>List of top anime.</returns>
		Task<AnimeTop> GetAnimeTop(TopAnimeExtension extension);

		/// <summary>
		/// Returns list of top anime.
		/// </summary>
		/// <param name="page">Index of page folding 50 records of top ranging (e.g. 1 will return first 50 records, 2 will return record from 51 to 100 etc.)</param>
		/// <param name="extension">Extension for specific type of ranking.</param>
		/// <returns>List of top anime.</returns>
		Task<AnimeTop> GetAnimeTop(int page, TopAnimeExtension extension);

		#endregion 

		#region GetMangaTop

		/// <summary>
		/// Returns list of top manga.
		/// </summary>
		/// <returns>List of top manga.</returns>
		Task<MangaTop> GetMangaTop();

		/// <summary>
		/// Returns list of top manga.
		/// </summary>
		/// <param name="page">Index of page folding 50 records of top ranging (e.g. 1 will return first 50 records, 2 will return record from 51 to 100 etc.)</param>
		/// <returns>List of top manga.</returns>
		Task<MangaTop> GetMangaTop(int page);

		/// <summary>
		/// Returns list of top manga.
		/// </summary>
		/// <param name="extension">Extension for specific type of ranking.</param>
		/// <returns>List of top manga.</returns>
		Task<MangaTop> GetMangaTop(TopMangaExtension extension);

		/// <summary>
		/// Returns list of top manga.
		/// </summary>
		/// <param name="page">Index of page folding 50 records of top ranging (e.g. 1 will return first 50 records, 2 will return record from 51 to 100 etc.)</param>
		/// <param name="extension">Extension for specific type of ranking.</param>
		/// <returns>List of top manga.</returns>
		Task<MangaTop> GetMangaTop(int page, TopMangaExtension extension);

		#endregion

		#region GetPeopleTop

		/// <summary>
		/// Returns list of most popular people.
		/// </summary>
		/// <returns>List of most popular people.</returns>
		Task<PeopleTop> GetPeopleTop();

		/// <summary>
		/// Returns list of most popular people.
		/// </summary>
		/// <param name="page">Index of page folding 50 records of top ranging (e.g. 1 will return first 50 records, 2 will return record from 51 to 100 etc.)</param>
		/// <returns>List of most popular people.</returns>
		Task<PeopleTop> GetPeopleTop(int page);

		#endregion

		#region GetCharactersTop

		/// <summary>
		/// Returns list of most popular characters.
		/// </summary>
		/// <returns>List of most popular characters.</returns>
		Task<CharactersTop> GetCharactersTop();

		/// <summary>
		/// Returns list of most popular characters.
		/// </summary>
		/// <param name="page">Index of page folding 50 records of top ranging (e.g. 1 will return first 50 records, 2 will return record from 51 to 100 etc.)</param>
		/// <returns>List of most popular characters.</returns>
		Task<CharactersTop> GetCharactersTop(int page);

		#endregion

		#endregion Top Requests

		#region Genre requests

		#region GetAnimeGenre

		/// <summary>
		/// Returns information about anime genre.
		/// </summary>
		/// <param name="genreId">Id of the searched genre.</param>
		/// <returns>Information about anime genre</returns>
		Task<AnimeGenre> GetAnimeGenre(long genreId);

		/// <summary>
		/// Returns information about anime genre.
		/// </summary>
		/// <param name="genre">Searched genre.</param>
		/// <returns>Information about anime genre</returns>
		Task<AnimeGenre> GetAnimeGenre(GenreSearch genre);

		/// <summary>
		/// Returns information about anime genre.
		/// </summary>
		/// <param name="genreId">Id of the searched genre.</param>
		/// <param name="page">Index of page folding 100 records of top ranging (e.g. 1 will return first 100 records, 2 will return record from 101 to 200 etc.)</param>
		/// <returns>Information about anime genre</returns>
		Task<AnimeGenre> GetAnimeGenre(long genreId, int page);

		/// <summary>
		/// Returns information about anime genre.
		/// </summary>
		/// <param name="genre">Searched genre.</param>
		/// <param name="page">Index of page folding 100 records of top ranging (e.g. 1 will return first 100 records, 2 will return record from 101 to 200 etc.)</param>
		/// <returns>Information about anime genre</returns>
		Task<AnimeGenre> GetAnimeGenre(GenreSearch genre, int page);

		#endregion

		#region GetMangaGenre

		/// <summary>
		/// Returns information about manga genre.
		/// </summary>
		/// <param name="genreId">Id of the searched genre.</param>
		/// <returns>Information about manga genre</returns>
		Task<MangaGenre> GetMangaGenre(long genreId);

		/// <summary>
		/// Returns information about manga genre.
		/// </summary>
		/// <param name="genre">Searched genre.</param>
		/// <returns>Information about manga genre</returns>
		Task<MangaGenre> GetMangaGenre(GenreSearch genre);

		/// <summary>
		/// Returns information about manga genre.
		/// </summary>
		/// <param name="genreId">Id of the searched genre.</param>
		/// <param name="page">Index of page folding 100 records of top ranging (e.g. 1 will return first 100 records, 2 will return record from 101 to 200 etc.)</param>
		/// <returns>Information about manga genre</returns>
		Task<MangaGenre> GetMangaGenre(long genreId, int page);

		/// <summary>
		/// Returns information about manga genre.
		/// </summary>
		/// <param name="genre">Searched genre.</param>
		/// <param name="page">Index of page folding 100 records of top ranging (e.g. 1 will return first 100 records, 2 will return record from 101 to 200 etc.)</param>
		/// <returns>Information about manga genre</returns>
		Task<MangaGenre> GetMangaGenre(GenreSearch genre, int page);

		#endregion

		#endregion Genre requests

		#region Producer requests

		#region GetProducer

		/// <summary>
		/// Returns information about producer with given MAL id. 
		/// </summary>
		/// <param name="id">MAL id of the producer.</param>
		/// <returns>Information about producer with given MAL id. </returns>
		Task<Producer> GetProducer(long id);

		/// <summary>
		/// Returns information about producer with given MAL id. 
		/// </summary>
		/// <param name="id">MAL id of the producer.</param>
		/// <param name="page">Index of page folding 100 records of top ranging (e.g. 1 will return first 100 records, 2 will return record from 101 to 200 etc.)</param>
		/// <returns>Information about producer with given MAL id. </returns>
		Task<Producer> GetProducer(long id, int page);

		#endregion

		#endregion Producer requests

		#region Magazine requests

		#region GetMagazine

		/// <summary>
		/// Returns information about magazine with given MAL id. 
		/// </summary>
		/// <param name="id">MAL id of the magazine.</param>
		/// <returns>Information about magazine with given MAL id. </returns>
		Task<Magazine> GetMagazine(long id);

		/// <summary>
		/// Returns information about magazine with given MAL id. 
		/// </summary>
		/// <param name="id">MAL id of the magazine.</param>
		/// <param name="page">Index of page folding 100 records of top ranging (e.g. 1 will return first 100 records, 2 will return record from 101 to 200 etc.)</param>
		/// <returns>Information about magazine with given MAL id. </returns>
		Task<Magazine> GetMagazine(long id, int page);

		#endregion

		#endregion Magazine requests

		#region User requests

		#region GetUserProfile

		/// <summary>
		/// Returns information about user's profile with given username.
		/// </summary>
		/// <param name="username">Username.</param>
		/// <returns>Information about user's profile with given username.</returns>
		Task<UserProfile> GetUserProfile(string username);

		#endregion

		#region GetUserHistory

		/// <summary>
		/// Returns information about user's history with given username.
		/// </summary>
		/// <param name="username">Username.</param>
		/// <returns>Information about user's profile with given username.</returns>
		Task<UserHistory> GetUserHistory(string username);

		/// <summary>
		/// Returns information about user's history with given username.
		/// </summary>
		/// <param name="username">Username.</param>
		/// <param name="filter">Option to filter history.</param>
		/// <returns>Information about user's profile with given username.</returns>
		Task<UserHistory> GetUserHistory(string username, UserHistoryExtension filter);

		#endregion

		#region GetUserFriends

		/// <summary>
		/// Returns information about user's friends with given username.
		/// </summary>
		/// <param name="username">Username.</param>
		/// <returns>Information about user's friends with given username.</returns>
		Task<UserFriends> GetUserFriends(string username);

		/// <summary>
		/// Returns information about user's friends with given username.
		/// </summary>
		/// <param name="username">Username.</param>
		/// <param name="page">Index of the page.</param>
		/// <returns>Information about user's friends with given username.</returns>
		Task<UserFriends> GetUserFriends(string username, int page);

		#endregion

		#region GetUserAnimeList

		/// <summary>
		/// Returns entries on user's anime list.
		/// </summary>
		/// <param name="username">Username.</param>
		/// <returns>Entries on user's anime list.</returns>
		Task<UserAnimeList> GetUserAnimeList(string username);

		/// <summary>
		/// Returns entries on user's anime list.
		/// </summary>
		/// <param name="username">Username.</param>
		/// <param name="page">Index of page folding 300 records of top ranging (e.g. 1 will return first 300 records, 2 will return record from 301 to 600 etc.)</param>
		/// <returns>Entries on user's anime list.</returns>
		Task<UserAnimeList> GetUserAnimeList(string username, int page);
	
		/// <summary>
		/// Returns entries on user's anime list.
		/// </summary>
		/// <param name="username">Username.</param>
		/// <param name="filter">Option to filter list.</param>
		/// <returns>Entries on user's anime list.</returns>
		Task<UserAnimeList> GetUserAnimeList(string username, UserAnimeListExtension filter);

		/// <summary>
		/// Returns entries on user's anime list.
		/// </summary>
		/// <param name="username">Username.</param>
		/// <param name="filter">Option to filter list.</param>
		/// <param name="page">Index of page folding 300 records of top ranging (e.g. 1 will return first 300 records, 2 will return record from 301 to 600 etc.)</param>
		/// <returns>Entries on user's anime list.</returns>
		Task<UserAnimeList> GetUserAnimeList(string username, UserAnimeListExtension filter, int page);

		#endregion

		#region GetUserMangaList

		/// <summary>
		/// Returns entries on user's manga list.
		/// </summary>
		/// <param name="username">Username.</param>
		/// <returns>Entries on user's manga list.</returns>
		Task<UserMangaList> GetUserMangaList(string username);

		/// <summary>
		/// Returns entries on user's manga list.
		/// </summary>
		/// <param name="username">Username.</param>
		/// <param name="page">Index of page folding 300 records of top ranging (e.g. 1 will return first 300 records, 2 will return record from 301 to 600 etc.)</param>
		/// <returns>Entries on user's manga list.</returns>
		Task<UserMangaList> GetUserMangaList(string username, int page);

		/// <summary>
		/// Returns entries on user's manga list.
		/// </summary>
		/// <param name="username">Username.</param>
		/// <param name="filter">Option to filter list.</param>
		/// <returns>Entries on user's manga list.</returns>
		Task<UserMangaList> GetUserMangaList(string username, UserMangaListExtension filter);

		/// <summary>
		/// Returns entries on user's manga list.
		/// </summary>
		/// <param name="username">Username.</param>
		/// <param name="filter">Option to filter list.</param>
		/// <param name="page">Index of page folding 300 records of top ranging (e.g. 1 will return first 300 records, 2 will return record from 301 to 600 etc.)</param>
		/// <returns>Entries on user's manga list.</returns>
		Task<UserMangaList> GetUserMangaList(string username, UserMangaListExtension filter, int page);

		#endregion

		#endregion User requests

		#region Club requests

		#region GetClub

		/// <summary>
		/// Return club's profile information.
		/// </summary>
		/// <param name="id">MAL id of the club.</param>
		/// <returns>Club's profile information.</returns>
		Task<Club> GetClub(long id);

		#endregion

		#region GetClubMembers

		/// <summary>
		/// Return club's member list.
		/// </summary>
		/// <param name="id">MAL id of the club.</param>
		/// <returns>Club's member list.</returns>
		Task<ClubMembers> GetClubMembers(long id);

		/// <summary>
		/// Return club's member list.
		/// </summary>
		/// <param name="id">MAL id of the club.</param>
		/// <param name="page">Index of page folding 36 records of top ranging (e.g. 1 will return first 36 records, 2 will return record from 37 to 72 etc.)</param>
		/// <returns>Club's member list.</returns>
		Task<ClubMembers> GetClubMembers(long id, int page);

		#endregion

		#endregion Club requests

		#region Search requests

		#region SearchAnime

		/// <summary>
		/// Returns list of results related to search.
		/// </summary>
		/// <param name="query">Search query.</param>
		/// <returns>List of result related to search query.</returns>
		Task<AnimeSearchResult> SearchAnime(string query);

		/// <summary>
		/// Returns list of results related to search.
		/// </summary>
		/// <param name="query">Search query.</param>
		/// <param name="page">Index of page folding 50 records of top ranging (e.g. 1 will return first 50 records, 2 will return record from 51 to 100 etc.)</param>
		/// <returns>List of result related to search query.</returns>
		Task<AnimeSearchResult> SearchAnime(string query, int page);

		/// <summary>
		/// Returns list of results related to search.
		/// </summary>
		/// <param name="query">Search query.</param>
		/// <param name="searchConfig">Additional configuration for advanced search.</param>
		/// <returns>List of result related to search query.</returns>
		Task<AnimeSearchResult> SearchAnime(string query, AnimeSearchConfig searchConfig);

		/// <summary>
		/// Returns list of results related to search.
		/// </summary>
		/// <param name="query">Search query.</param>
		/// <param name="page">Index of page folding 50 records of top ranging (e.g. 1 will return first 50 records, 2 will return record from 51 to 100 etc.)</param>
		/// <param name="searchConfig">Additional configuration for advanced search.</param>
		/// <returns>List of result related to search query.</returns>
		Task<AnimeSearchResult> SearchAnime(string query, int page, AnimeSearchConfig searchConfig);

		#endregion

		#region SearchManga

		/// <summary>
		/// Returns list of results related to search.
		/// </summary>
		/// <param name="query">Search query.</param>
		/// <returns>List of result related to search query.</returns>
		Task<MangaSearchResult> SearchManga(string query);

		/// <summary>
		/// Returns list of results related to search.
		/// </summary>
		/// <param name="query">Search query.</param>
		/// <param name="page">Index of page folding 50 records of top ranging (e.g. 1 will return first 50 records, 2 will return record from 51 to 100 etc.)</param>
		/// <returns>List of result related to search query.</returns>
		Task<MangaSearchResult> SearchManga(string query, int page);

		/// <summary>
		/// Returns list of results related to search.
		/// </summary>
		/// <param name="query">Search query.</param>
		/// <param name="searchConfig">Additional configuration for advanced search.</param>
		/// <returns>List of result related to search query.</returns>
		Task<MangaSearchResult> SearchManga(string query, MangaSearchConfig searchConfig);

		/// <summary>
		/// Returns list of results related to search.
		/// </summary>
		/// <param name="query">Search query.</param>
		/// <param name="page">Index of page folding 50 records of top ranging (e.g. 1 will return first 50 records, 2 will return record from 51 to 100 etc.)</param>
		/// <param name="searchConfig">Additional configuration for advanced search.</param>
		/// <returns>List of result related to search query.</returns>
		Task<MangaSearchResult> SearchManga(string query, int page, MangaSearchConfig searchConfig);

		#endregion

		#region SearchPerson

		/// <summary>
		/// Returns list of results related to search.
		/// </summary>
		/// <param name="query">Search query.</param>
		/// <returns>List of result related to search query.</returns>
		Task<PersonSearchResult> SearchPerson(string query);

		/// <summary>
		/// Returns list of results related to search.
		/// </summary>
		/// <param name="query">Search query.</param>
		/// <param name="page">Index of page folding 50 records of top ranging (e.g. 1 will return first 50 records, 2 will return record from 51 to 100 etc.)</param>
		/// <returns>List of result related to search query.</returns>
		Task<PersonSearchResult> SearchPerson(string query, int page);

		#endregion

		#region SearchCharacter

		/// <summary>
		/// Returns list of results related to search.
		/// </summary>
		/// <param name="query">Search query.</param>
		/// <returns>List of result related to search query.</returns>
		Task<CharacterSearchResult> SearchCharacter(string query);

		/// <summary>
		/// Returns list of results related to search.
		/// </summary>
		/// <param name="query">Search query.</param>
		/// <param name="page">Index of page folding 50 records of top ranging (e.g. 1 will return first 50 records, 2 will return record from 51 to 100 etc.)</param>
		/// <returns>List of result related to search query.</returns>
		Task<CharacterSearchResult> SearchCharacter(string query, int page);

		#endregion

		#endregion Search requests

		#region Metadata requests

		#region GetStatusMetadata

		/// <summary>
		/// Return Jikan REST API metadata - status.
		/// </summary>
		/// <returns>Jikan REST API metadata - status.</returns>
		Task<StatusMetadata> GetStatusMetadata();

		#endregion

		#endregion Metadata requests
	}
}