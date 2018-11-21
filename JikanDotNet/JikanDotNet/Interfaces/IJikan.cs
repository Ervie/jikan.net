using System.Threading.Tasks;

namespace JikanDotNet
{
	/// <summary>
	/// Interface for Jikan.net client
	/// </summary>
	public interface IJikan
	{
		/// <summary>
		/// Return anime with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <returns>Anime with given MAL id.</returns>
		Task<Anime> GetAnime(long id);

		/// <summary>
		/// Return anime with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <param name="extension">Extension for extra data.</param>
		/// <returns>Anime with given MAL id.</returns>
		Task<Anime> GetAnime(long id, AnimeExtension extension);

		/// <summary>
		/// Return list of episodes for anime with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <returns>List of episodes with details.</returns>
		Task<AnimeEpisodes> GetAnimeEpisodes(long id);

		/// <summary>
		/// Return list of episodes for anime with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <param name="page">Indexx of page folding 50 records of top ranging (e.g. 1 will return first 50 records, 2 will return record from 51 to 100 etc.)</param>
		/// <returns>List of episodes with details.</returns>
		Task<AnimeEpisodes> GetAnimeEpisodes(long id, int page);

		/// <summary>
		/// Return collections of characters and staff of anime with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <returns>Collections of characters and staff of anime with given MAL id.</returns>
		Task<AnimeCharactersStaff> GetAnimeCharactersStaff(long id);

		/// <summary>
		/// Return collections of links to pictures related to anime with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <returns>Collections of links to pictures related to anime with given MAL id.</returns>
		Task<AnimePictures> GetAnimePictures(long id);

		/// <summary>
		/// Return collections of news related to anime with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <returns>Collections of news related to anime with given MAL id.</returns>
		Task<AnimeNews> GetAnimeNews(long id);

		/// <summary>
		/// Return collections of videos related to anime with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <returns>Collections of videos related to anime with given MAL id.</returns>
		Task<AnimeVideos> GetAnimeVideos(long id);

		/// <summary>
		/// Return statistics related to anime with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <returns>Statistics related to anime with given MAL id.</returns>
		Task<AnimeStats> GetAnimeStatistics(long id);

		/// <summary>
		/// Return collections of forum topics related to anime with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <returns>Collections of forum topics related to anime with given MAL id.</returns>
		Task<ForumTopics> GetAnimeForumTopics (long id);

		/// <summary>
		/// Return additional information related to anime with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <returns>Additional information related to anime with given MAL id.</returns>
		Task<MoreInfo> GetAnimeMoreInfo(long id);

		/// <summary>
		/// Return manga with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of manga.</param>
		/// <returns>Manga with given MAL id.</returns>
		Task<Manga> GetManga(long id);

		/// <summary>
		/// Return manga with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of manga.</param>
		/// <param name="extension">Extension for extra data.</param>
		/// <returns>Manga with given MAL id.</returns>
		Task<Manga> GetManga(long id, MangaExtension extension);

		/// <summary>
		/// Return collections of links to pictures related to manga with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of manga.</param>
		/// <returns>Collections of links to pictures related to manga with given MAL id.</returns>
		Task<MangaPictures> GetMangaPictures(long id);

		/// <summary>
		/// Return collections of characters appearing in manga with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of manga.</param>
		/// <returns>Collections of characters appearing in manga with given MAL id.</returns>
		Task<MangaCharacters> GetMangaCharacters(long id);

		/// <summary>
		/// Return collections of news related to manga with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of manga.</param>
		/// <returns>Collections of news related to manga with given MAL id.</returns>
		Task<MangaNews> GetMangaNews(long id);

		/// <summary>
		/// Return statistics related to manga with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of manga.</param>
		/// <returns>Statistics related to manga with given MAL id.</returns>
		Task<MangaStats> GetMangaStatistics(long id);

		/// <summary>
		/// Return collections of forum topics related to manga with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of manga.</param>
		/// <returns>Collections of forum topics related to manga with given MAL id.</returns>
		Task<ForumTopics> GetMangaForumTopics(long id);

		/// <summary>
		/// Return additional information related to manga with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of manga.</param>
		/// <returns>Collections of forum topics related to manga with given MAL id.</returns>
		Task<MoreInfo> GetMangaMoreInfo(long id);

		/// <summary>
		/// Return character with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of character.</param>
		/// <returns>Character with given MAL id.</returns>
		Task<Character> GetCharacter(long id);

		/// <summary>
		/// Return character with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of character.</param>
		/// <param name="extension">Extension for extra data.</param>
		/// <returns>Character with given MAL id.</returns>
		Task<Character> GetCharacter(long id, CharacterExtension extension);

		/// <summary>
		/// Return collections of links to pictures related to character with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of character.</param>
		/// <returns>Collections of links to pictures related to character with given MAL id.</returns>
		Task<CharacterPictures> GetCharacterPictures(long id);

		/// <summary>
		/// Return person with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of person.</param>
		/// <returns>Person with given MAL id.</returns>
		Task<Person> GetPerson(long id);

		/// <summary>
		/// Return person with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of person.</param>
		/// <param name="extension">Extension for extra data.</param>
		/// <returns>Person with given MAL id.</returns>
		Task<Person> GetPerson(long id, PersonExtension extension);

		/// <summary>
		/// Return collections of links to pictures related to person with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of person.</param>
		/// <returns>Collections of links to pictures related to person with given MAL id.</returns>
		Task<PersonPictures> GetPersonPictures(long id);

		/// <summary>
		/// Return current season preview.
		/// </summary>
		/// <returns>Current season preview.</returns>
		Task<Season> GetSeason();

		/// <summary>
		/// Return season preview.
		/// </summary>
		/// <param name="year">Year of selected season.</param>
		/// <param name="season">Selected season.</param>
		/// <returns>Season preview.</returns>
		Task<Season> GetSeason(int year, Seasons season);

		/// <summary>
		/// Return list of availaible season to query with <see cref="GetSeason(int, Seasons)"/>
		/// </summary>
		/// <returns></returns>
		Task<SeasonArchives> GetSeasonArchive();

		/// <summary>
		/// Return current season schedule.
		/// </summary>
		/// <returns>Current season schedule.</returns>
		Task<Schedule> GetSchedule();

		/// <summary>
		/// Return current season schedule.
		/// </summary>
		/// <param name="scheduledDay">Scheduled day to filter by.</param>
		/// <returns>Current season schedule.</returns>
		Task<Schedule> GetSchedule(ScheduledDay scheduledDay);

		/// <summary>
		/// Return list of top anime.
		/// </summary>
		/// <returns>List of top anime.</returns>
		Task<AnimeTop> GetAnimeTop();

		/// <summary>
		/// Return list of top anime.
		/// </summary>
		/// <param name="page">Index of page folding 50 records of top ranging (e.g. 1 will return first 50 records, 2 will return record from 51 to 100 etc.)</param>
		/// <returns>List of top anime.</returns>
		Task<AnimeTop> GetAnimeTop(int page);

		/// <summary>
		/// Return list of top anime.
		/// </summary>
		/// <param name="extension">Extension for specific type of ranking.</param>
		/// <returns>List of top anime.</returns>
		Task<AnimeTop> GetAnimeTop(TopAnimeExtension extension);

		/// <summary>
		/// Return list of top anime.
		/// </summary>
		/// <param name="page">Index of page folding 50 records of top ranging (e.g. 1 will return first 50 records, 2 will return record from 51 to 100 etc.)</param>
		/// <param name="extension">Extension for specific type of ranking.</param>
		/// <returns>List of top anime.</returns>
		Task<AnimeTop> GetAnimeTop(int page, TopAnimeExtension extension);

		/// <summary>
		/// Return list of top manga.
		/// </summary>
		/// <returns>List of top manga.</returns>
		Task<MangaTop> GetMangaTop();

		/// <summary>
		/// Return list of top manga.
		/// </summary>
		/// <param name="page">Index of page folding 50 records of top ranging (e.g. 1 will return first 50 records, 2 will return record from 51 to 100 etc.)</param>
		/// <returns>List of top manga.</returns>
		Task<MangaTop> GetMangaTop(int page);

		/// <summary>
		/// Return list of top manga.
		/// </summary>
		/// <param name="extension">Extension for specific type of ranking.</param>
		/// <returns>List of top manga.</returns>
		Task<MangaTop> GetMangaTop(TopMangaExtension extension);

		/// <summary>
		/// Return list of top manga.
		/// </summary>
		/// <param name="page">Index of page folding 50 records of top ranging (e.g. 1 will return first 50 records, 2 will return record from 51 to 100 etc.)</param>
		/// <param name="extension">Extension for specific type of ranking.</param>
		/// <returns>List of top manga.</returns>
		Task<MangaTop> GetMangaTop(int page, TopMangaExtension extension);

		/// <summary>
		/// Return list of most popular people.
		/// </summary>
		/// <returns>List of most popular people.</returns>
		Task<PeopleTop> GetPeopleTop();

		/// <summary>
		/// Return list of most popular people.
		/// </summary>
		/// <param name="page">Index of page folding 50 records of top ranging (e.g. 1 will return first 50 records, 2 will return record from 51 to 100 etc.)</param>
		/// <returns>List of most popular people.</returns>
		Task<PeopleTop> GetPeopleTop(int page);

		/// <summary>
		/// Return list of most popular characters.
		/// </summary>
		/// <returns>List of most popular characters.</returns>
		Task<CharactersTop> GetCharactersTop();

		/// <summary>
		/// Return list of most popular characters.
		/// </summary>
		/// <param name="page">Index of page folding 50 records of top ranging (e.g. 1 will return first 50 records, 2 will return record from 51 to 100 etc.)</param>
		/// <returns>List of most popular characters.</returns>
		Task<CharactersTop> GetCharactersTop(int page);

		/// <summary>
		/// Return information about anime genre.
		/// </summary>
		/// <param name="genreId">Id of the searched genre.</param>
		/// <returns>Information about anime genre</returns>
		Task<AnimeGenre> GetAnimeGenre(long genreId);

		/// <summary>
		/// Return information about anime genre.
		/// </summary>
		/// <param name="genre">Searched genre.</param>
		/// <returns>Information about anime genre</returns>
		Task<AnimeGenre> GetAnimeGenre(GenreSearch genre);

		/// <summary>
		/// Return information about anime genre.
		/// </summary>
		/// <param name="genreId">Id of the searched genre.</param>
		/// <param name="page">Index of page folding 100 records of top ranging (e.g. 1 will return first 100 records, 2 will return record from 101 to 200 etc.)</param>
		/// <returns>Information about anime genre</returns>
		Task<AnimeGenre> GetAnimeGenre(long genreId, int page);

		/// <summary>
		/// Return information about anime genre.
		/// </summary>
		/// <param name="genre">Searched genre.</param>
		/// <param name="page">Index of page folding 100 records of top ranging (e.g. 1 will return first 100 records, 2 will return record from 101 to 200 etc.)</param>
		/// <returns>Information about anime genre</returns>
		Task<AnimeGenre> GetAnimeGenre(GenreSearch genre, int page);

		/// <summary>
		/// Return information about manga genre.
		/// </summary>
		/// <param name="genreId">Id of the searched genre.</param>
		/// <returns>Information about manga genre</returns>
		Task<MangaGenre> GetMangaGenre(long genreId);

		/// <summary>
		/// Return information about manga genre.
		/// </summary>
		/// <param name="genre">Searched genre.</param>
		/// <returns>Information about manga genre</returns>
		Task<MangaGenre> GetMangaGenre(GenreSearch genre);

		/// <summary>
		/// Return information about manga genre.
		/// </summary>
		/// <param name="genreId">Id of the searched genre.</param>
		/// <param name="page">Index of page folding 100 records of top ranging (e.g. 1 will return first 100 records, 2 will return record from 101 to 200 etc.)</param>
		/// <returns>Information about manga genre</returns>
		Task<MangaGenre> GetMangaGenre(long genreId, int page);

		/// <summary>
		/// Return information about manga genre.
		/// </summary>
		/// <param name="genre">Searched genre.</param>
		/// <param name="page">Index of page folding 100 records of top ranging (e.g. 1 will return first 100 records, 2 will return record from 101 to 200 etc.)</param>
		/// <returns>Information about manga genre</returns>
		Task<MangaGenre> GetMangaGenre(GenreSearch genre, int page);

		/// <summary>
		/// Return list of results related to search.
		/// </summary>
		/// <param name="query">Search query.</param>
		/// <returns>List of result related to search query.</returns>
		Task<AnimeSearchResult> SearchAnime(string query);

		/// <summary>
		/// Return list of results related to search.
		/// </summary>
		/// <param name="query">Search query.</param>
		/// <param name="page">Index of page folding 50 records of top ranging (e.g. 1 will return first 50 records, 2 will return record from 51 to 100 etc.)</param>
		/// <returns>List of result related to search query.</returns>
		Task<AnimeSearchResult> SearchAnime(string query, int page);

		/// <summary>
		/// Return list of results related to search.
		/// </summary>
		/// <param name="query">Search query.</param>
		/// <param name="searchConfig">Additional configuration for advanced search.</param>
		/// <returns>List of result related to search query.</returns>
		Task<AnimeSearchResult> SearchAnime(string query, AnimeSearchConfig searchConfig);

		/// <summary>
		/// Return list of results related to search.
		/// </summary>
		/// <param name="query">Search query.</param>
		/// <param name="page">Index of page folding 50 records of top ranging (e.g. 1 will return first 50 records, 2 will return record from 51 to 100 etc.)</param>
		/// <param name="searchConfig">Additional configuration for advanced search.</param>
		/// <returns>List of result related to search query.</returns>
		Task<AnimeSearchResult> SearchAnime(string query, int page, AnimeSearchConfig searchConfig);

		/// <summary>
		/// Return list of results related to search.
		/// </summary>
		/// <param name="query">Search query.</param>
		/// <returns>List of result related to search query.</returns>
		Task<MangaSearchResult> SearchManga(string query);

		/// <summary>
		/// Return list of results related to search.
		/// </summary>
		/// <param name="query">Search query.</param>
		/// <param name="page">Index of page folding 50 records of top ranging (e.g. 1 will return first 50 records, 2 will return record from 51 to 100 etc.)</param>
		/// <returns>List of result related to search query.</returns>
		Task<MangaSearchResult> SearchManga(string query, int page);

		/// <summary>
		/// Return list of results related to search.
		/// </summary>
		/// <param name="query">Search query.</param>
		/// <param name="searchConfig">Additional configuration for advanced search.</param>
		/// <returns>List of result related to search query.</returns>
		Task<MangaSearchResult> SearchManga(string query, MangaSearchConfig searchConfig);

		/// <summary>
		/// Return list of results related to search.
		/// </summary>
		/// <param name="query">Search query.</param>
		/// <param name="page">Index of page folding 50 records of top ranging (e.g. 1 will return first 50 records, 2 will return record from 51 to 100 etc.)</param>
		/// <param name="searchConfig">Additional configuration for advanced search.</param>
		/// <returns>List of result related to search query.</returns>
		Task<MangaSearchResult> SearchManga(string query, int page, MangaSearchConfig searchConfig);

		/// <summary>
		/// Return list of results related to search.
		/// </summary>
		/// <param name="query">Search query.</param>
		/// <returns>List of result related to search query.</returns>
		Task<PersonSearchResult> SearchPerson(string query);

		/// <summary>
		/// Return list of results related to search.
		/// </summary>
		/// <param name="query">Search query.</param>
		/// <param name="page">Index of page folding 50 records of top ranging (e.g. 1 will return first 50 records, 2 will return record from 51 to 100 etc.)</param>
		/// <returns>List of result related to search query.</returns>
		Task<PersonSearchResult> SearchPerson(string query, int page);

		/// <summary>
		/// Return list of results related to search.
		/// </summary>
		/// <param name="query">Search query.</param>
		/// <returns>List of result related to search query.</returns>
		Task<CharacterSearchResult> SearchCharacter(string query);

		/// <summary>
		/// Return list of results related to search.
		/// </summary>
		/// <param name="query">Search query.</param>
		/// <param name="page">Index of page folding 50 records of top ranging (e.g. 1 will return first 50 records, 2 will return record from 51 to 100 etc.)</param>
		/// <returns>List of result related to search query.</returns>
		Task<CharacterSearchResult> SearchCharacter(string query, int page);
	}
}