using System.Collections.Generic;
using System.Threading.Tasks;

namespace JikanDotNet
{
	/// <summary>
	/// Interface for Jikan.net client
	/// </summary>
	public interface IJikan
	{
		#region Anime requests

		#region GetAnimeAsync

		/// <summary>
		/// Returns anime with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <returns>Anime with given MAL id.</returns>
		Task<BaseJikanResponse<Anime>> GetAnimeAsync(long id);

		#endregion GetAnimeAsync

		#region GetAnimeCharactersAsync

		/// <summary>
		/// Returns collections of characters of anime with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <returns>Collection of characters of anime with given MAL id.</returns>
		Task<BaseJikanResponse<ICollection<AnimeCharacter>>> GetAnimeCharactersAsync(long id);

		#endregion GetAnimeCharactersAsync

		#region GetAnimeStaffAsync

		/// <summary>
		/// Returns collections of staff of anime with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <returns>Collection of staff of anime with given MAL id.</returns>
		Task<BaseJikanResponse<ICollection<AnimeStaffPosition>>> GetAnimeStaffAsync(long id);

		#endregion GetAnimeStaffAsync

		#region GetAnimeEpisodesAsync

		/// <summary>
		/// Returns list of episodes for anime with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <returns>List of episodes with details.</returns>
		Task<PaginatedJikanResponse<ICollection<AnimeEpisode>>> GetAnimeEpisodesAsync(long id);

		/// <summary>
		/// Returns list of episodes for anime with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <param name="page">Index of page folding 100 records of top ranging (e.g. 1 will return first 100 records, 2 will return record from 101 to 200 etc.)</param>
		/// <returns>List of episodes with details.</returns>
		Task<PaginatedJikanResponse<ICollection<AnimeEpisode>>> GetAnimeEpisodesAsync(long id, int page);

		#endregion GetAnimeEpisodesAsync

		#region GetAnimeEpisodeAsync

		/// <summary>
		/// Returns details about specific episode.
		/// </summary>
		/// <param name="animeId">MAL id of anime.</param>
		/// <param name="episodeId">Id of episode.</param>
		/// <returns>Details about specific episode.</returns>
		Task<BaseJikanResponse<AnimeEpisode>> GetAnimeEpisodeAsync(long animeId, int episodeId);

		#endregion GetAnimeEpisodeAsync

		#region GetAnimeNewsAsync

		/// <summary>
		/// Returns collections of news related to anime with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <returns>Collections of news related to anime with given MAL id.</returns>
		Task<PaginatedJikanResponse<ICollection<News>>> GetAnimeNewsAsync(long id);

		/// <summary>
		/// Returns collections of news related to anime with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <param name="page">Index of page folding 100 records of top ranging (e.g. 1 will return first 100 records, 2 will return record from 101 to 200 etc.)</param>
		/// <returns>Collections of news related to anime with given MAL id.</returns>
		Task<PaginatedJikanResponse<ICollection<News>>> GetAnimeNewsAsync(long id, int page);

		#endregion GetAnimeNewsAsync

		#region GetAnimeForumTopicsAsync

		/// <summary>
		/// Returns collections of forum topics related to anime with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <returns>Collections of forum topics related to anime with given MAL id.</returns>
		Task<BaseJikanResponse<ICollection<ForumTopic>>> GetAnimeForumTopicsAsync(long id);

		/// <summary>
		/// Returns collections of forum topics related to anime with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <param name="type">ForumTopicType filter</param>
		/// <returns>Collections of forum topics related to anime with given MAL id.</returns>
		Task<BaseJikanResponse<ICollection<ForumTopic>>> GetAnimeForumTopicsAsync(long id, ForumTopicType type);

		#endregion GetAnimeForumTopicsAsync

		#region GetAnimeVideosAsync

		/// <summary>
		/// Returns collections of videos related to anime with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <returns>Collections of videos related to anime with given MAL id.</returns>
		Task<BaseJikanResponse<AnimeVideos>> GetAnimeVideosAsync(long id);

		#endregion GetAnimeVideosAsync

		#region GetAnimePicturesAsync

		/// <summary>
		/// Returns collections of links to pictures related to anime with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <returns>Collections of links to pictures related to anime with given MAL id.</returns>
		Task<BaseJikanResponse<ICollection<ImagesSet>>> GetAnimePicturesAsync(long id);

		#endregion GetAnimePictures

		#region GetAnimeStatisticsAsync

		/// <summary>
		/// Returns statistics related to anime with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <returns>Statistics related to anime with given MAL id.</returns>
		Task<BaseJikanResponse<AnimeStatistics>> GetAnimeStatisticsAsync(long id);

		#endregion GetAnimeStatisticsAsync

		#region GetAnimeMoreInfoAsync

		/// <summary>
		/// Returns additional information related to anime with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <returns>Additional information related to anime with given MAL id.</returns>
		Task<BaseJikanResponse<MoreInfo>> GetAnimeMoreInfoAsync(long id);

		#endregion GetAnimeMoreInfoAsync

		#region GetAnimeRecommendationsAsync

		/// <summary>
		/// Returns collection of anime recommendation.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <returns>Collection of anime recomendation.</returns>
		Task<BaseJikanResponse<ICollection<Recommendation>>> GetAnimeRecommendationsAsync(long id);

		#endregion GetAnimeRecommendationsAsync

		#region GetAnimeUserUpdatesAsync

		/// <summary>
		/// Returns collection of anime user updates.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <returns>Collection of anime user updates.</returns>
		Task<PaginatedJikanResponse<ICollection<AnimeUserUpdate>>> GetAnimeUserUpdatesAsync(long id);

		/// <summary>
		/// Returns collection of anime user updates.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <param name="page">Index of page folding 75 records of top ranging (e.g. 1 will return first 75 records, 2 will return record from 76 to 150 etc.)</param>
		/// <returns>Collection of anime user updates.</returns>
		Task<PaginatedJikanResponse<ICollection<AnimeUserUpdate>>> GetAnimeUserUpdatesAsync(long id, int page);

		#endregion GetAnimeUserUpdatesAsync

		#region GetAnimeReviewsAsync

		/// <summary>
		/// Returns collection of anime reviews.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <returns>Collection of anime reviews.</returns>
		Task<PaginatedJikanResponse<ICollection<AnimeReview>>> GetAnimeReviewsAsync(long id);

		/// <summary>
		/// Returns collection of anime reviews.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <param name="page">Index of page folding 20 records of top ranging (e.g. 1 will return first 20 records, 2 will return record from 21 to 40 etc.)</param>
		/// <returns>Collection of anime reviews.</returns>
		Task<PaginatedJikanResponse<ICollection<AnimeReview>>> GetAnimeReviewsAsync(long id, int page);

		#endregion GetAnimeReviewsAsync

		#region GetAnimeRelationsAsync

		/// <summary>
		/// Returns collection of anime related entries.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <returns>Collection of anime related entries.</returns>
		Task<PaginatedJikanResponse<ICollection<RelatedEntry>>> GetAnimeRelationsAsync(long id);

		#endregion GetAnimeRelationsAsync

		#region GetAnimeThemesAsync

		/// <summary>
		/// Returns collection of anime openings and endings.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <returns>Collection of anime openings and endings.</returns>
		Task<BaseJikanResponse<AnimeThemes>> GetAnimeThemesAsync(long id);

		#endregion GetAnimeThemesAsync

		#endregion Anime requests

		#region Manga requests

		#region GetMangaAsync

		/// <summary>
		/// Returns manga with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of manga.</param>
		/// <returns>Manga with given MAL id.</returns>
		Task<BaseJikanResponse<Manga>> GetMangaAsync(long id);

		#endregion GetMangaAsync

		#region GetMangaCharactersAsync

		/// <summary>
		/// Returns collections of characters appearing in manga with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of manga.</param>
		/// <returns>Collections of characters appearing in manga with given MAL id.</returns>
		Task<BaseJikanResponse<ICollection<MangaCharacter>>> GetMangaCharactersAsync(long id);

		#endregion GetMangaCharactersAsync

		#region GetMangaNewsAsync

		/// <summary>
		/// Returns collections of news related to manga with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of manga.</param>
		/// <returns>Collections of news related to manga with given MAL id.</returns>
		Task<PaginatedJikanResponse<ICollection<News>>> GetMangaNewsAsync(long id);

		/// <summary>
		/// Returns collections of news related to manga with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of manga.</param>
		/// <param name="page">Index of page folding 100 records of top ranging (e.g. 1 will return first 100 records, 2 will return record from 101 to 200 etc.)</param>
		/// <returns>Collections of news related to manga with given MAL id.</returns>
		Task<PaginatedJikanResponse<ICollection<News>>> GetMangaNewsAsync(long id, int page);

		#endregion GetMangaNewsAsync

		#region GetMangaForumTopicsAsync

		/// <summary>
		/// Returns collections of forum topics related to manga with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of manga.</param>
		/// <returns>Collections of forum topics related to manga with given MAL id.</returns>
		Task<BaseJikanResponse<ICollection<ForumTopic>>> GetMangaForumTopicsAsync(long id);

		#endregion GetMangaForumTopicsAsync

		#region GetMangaPicturesAsync

		/// <summary>
		/// Returns collections of links to pictures related to manga with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of manga.</param>
		/// <returns>Collections of links to pictures related to manga with given MAL id.</returns>
		Task<BaseJikanResponse<ICollection<ImagesSet>>> GetMangaPicturesAsync(long id);

		#endregion GetMangaPicturesAsync

		#region GetMangaStatisticsAsync

		/// <summary>
		/// Returns statistics related to manga with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of manga.</param>
		/// <returns>Statistics related to manga with given MAL id.</returns>
		Task<BaseJikanResponse<MangaStatistics>> GetMangaStatisticsAsync(long id);

		#endregion GetMangaStatisticsAsync

		#region GetMangaMoreInfoAsync

		/// <summary>
		/// Returns additional information related to manga with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of manga.</param>
		/// <returns>Collections of forum topics related to manga with given MAL id.</returns>
		Task<BaseJikanResponse<MoreInfo>> GetMangaMoreInfoAsync(long id);

		#endregion GetMangaMoreInfoAsync

		#region GetMangaUserUpdatesAsync

		/// <summary>
		/// Returns collection of manga user updates.
		/// </summary>
		/// <param name="id">MAL id of manga.</param>
		/// <returns>Collection of manga user updates.</returns>
		Task<PaginatedJikanResponse<ICollection<MangaUserUpdate>>> GetMangaUserUpdatesAsync(long id);

		/// <summary>
		/// Returns collection of manga user updates.
		/// </summary>
		/// <param name="id">MAL id of manga.</param>
		/// <param name="page">Index of page folding 75 records of top ranging (e.g. 1 will return first 75 records, 2 will return record from 76 to 150 etc.)</param>
		/// <returns>Collection of manga user updates.</returns>
		Task<PaginatedJikanResponse<ICollection<MangaUserUpdate>>> GetMangaUserUpdatesAsync(long id, int page);

		#endregion GetMangaUserUpdatesAsync

		#region GetMangaRecommendationsAsync

		/// <summary>
		/// Returns collection of manga recommendation.
		/// </summary>
		/// <param name="id">MAL id of manga.</param>
		/// <returns>Collection of manga recomendation.</returns>
		Task<BaseJikanResponse<ICollection<Recommendation>>> GetMangaRecommendationsAsync(long id);

		#endregion GetMangaRecommendationsAsync

		#region GetMangaReviewsAsync

		/// <summary>
		/// Returns collection of manga reviews.
		/// </summary>
		/// <param name="id">MAL id of manga.</param>
		/// <returns>Collection of manga reviews.</returns>
		Task<PaginatedJikanResponse<ICollection<MangaReview>>> GetMangaReviewsAsync(long id);

		#endregion GetMangaReviewsAsync

		#region GetMangaRelationsAsync

		/// <summary>
		/// Returns collection of manga related entries.
		/// </summary>
		/// <param name="id">MAL id of manga.</param>
		/// <returns>Collection of manga related entries.</returns>
		Task<PaginatedJikanResponse<ICollection<RelatedEntry>>> GetMangaRelationsAsync(long id);

		#endregion GetMangaRelationsAsync

		#endregion Manga requests

		#region Character requests

		#region GetCharacterAsync

		/// <summary>
		/// Returns character with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of character.</param>
		/// <returns>Character with given MAL id.</returns>
		Task<BaseJikanResponse<Character>> GetCharacterAsync(long id);

		#endregion GetCharacterAsync

		#region GetCharacterAnimeAsync

		/// <summary>
		/// Returns return animeography of character with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of character.</param>
		/// <returns>Collection of anime where character has appeared.</returns>
		Task<BaseJikanResponse<ICollection<CharacterAnimeographyEntry>>> GetCharacterAnimeAsync(long id);

		#endregion GetCharacterAnimeAsync

		#region GetCharacterMangaAsync

		/// <summary>
		/// Returns return mangaography of character with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of character.</param>
		/// <returns>Collection of manga where character has appeared.</returns>
		Task<BaseJikanResponse<ICollection<CharacterMangaographyEntry>>> GetCharacterMangaAsync(long id);

		#endregion GetCharacterMangaAsync

		#region GetCharacterVoiceActorsAsync

		/// <summary>
		/// Returns return voice actors of character with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of character.</param>
		/// <returns>Collection of voice acotrs voicing character.</returns>
		Task<BaseJikanResponse<ICollection<VoiceActorEntry>>> GetCharacterVoiceActorsAsync(long id);

		#endregion GetCharacterVoiceActorsAsync

		#region GetCharacterPicturesAsync

		/// <summary>
		/// Returns collections of links to pictures related to character with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of character.</param>
		/// <returns>Collections of links to pictures related to character with given MAL id.</returns>
		Task<BaseJikanResponse<ICollection<ImagesSet>>> GetCharacterPicturesAsync(long id);

		#endregion GetCharacterPicturesAsync

		#endregion Character requests

		#region Person requests

		#region GetPersonAsync

		/// <summary>
		/// Returns person with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of person.</param>
		/// <returns>Person with given MAL id.</returns>
		Task<BaseJikanResponse<Person>> GetPersonAsync(long id);

		#endregion GetPersonAsync

		#region GetPersonAnimeAsync

		/// <summary>
		/// Returns return animeography of person with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of character.</param>
		/// <returns>Collection of anime the person collaborated on.</returns>
		Task<BaseJikanResponse<ICollection<PersonAnimeographyEntry>>> GetPersonAnimeAsync(long id);

		#endregion GetPersonAnimeAsync

		#region GetPersonMangaAsync

		/// <summary>
		/// Returns return mangaography of person with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of character.</param>
		/// <returns>Collection of manga the person worked on.</returns>
		Task<BaseJikanResponse<ICollection<PersonMangaographyEntry>>> GetPersonMangaAsync(long id);

		#endregion GetPersonMangaAsync

		#region GetPersonPictures

		/// <summary>
		/// Returns collections of links to pictures related to person with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of person.</param>
		/// <returns>Collections of links to pictures related to person with given MAL id.</returns>
		Task<PersonPictures> GetPersonPictures(long id);

		#endregion GetPersonPictures

		#endregion Person requests

		#region Season requests

		#region GetSeason

		/// <summary>
		/// Returns current season preview.
		/// </summary>
		/// <returns>Current season preview.</returns>
		Task<AnimeSeason> GetSeason();

		/// <summary>
		/// Returns season preview.
		/// </summary>
		/// <param name="year">Year of selected season.</param>
		/// <param name="season">Selected season.</param>
		/// <returns>Season preview.</returns>
		Task<AnimeSeason> GetSeason(int year, Season season);

		#endregion GetSeason

		#region GetSeasonArchive

		/// <summary>
		/// Returns list of availaible season to query with <see cref="GetSeason(int, Season)"/>
		/// </summary>
		/// <returns></returns>
		Task<SeasonArchives> GetSeasonArchive();

		#endregion GetSeasonArchive

		#region GetSeasonLater

		/// <summary>
		/// Return season preview for anime with undefined airing season (marked as "Later" on MAL).
		/// </summary>
		/// <returns>Season preview for anime with undefined airing date.</returns>
		Task<AnimeSeason> GetSeasonLater();

		#endregion GetSeasonLater

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

		#endregion GetSchedule

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

		#endregion GetAnimeTop

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

		#endregion GetMangaTop

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

		#endregion GetPeopleTop

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

		#endregion GetCharactersTop

		#endregion Top requests

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
		/// <param name="animeGenre">Searched genre.</param>
		/// <returns>Information about anime genre</returns>
		Task<AnimeGenre> GetAnimeGenre(AnimeGenreSearch animeGenre);

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
		/// <param name="animeGenre">Searched genre.</param>
		/// <param name="page">Index of page folding 100 records of top ranging (e.g. 1 will return first 100 records, 2 will return record from 101 to 200 etc.)</param>
		/// <returns>Information about anime genre</returns>
		Task<AnimeGenre> GetAnimeGenre(AnimeGenreSearch animeGenre, int page);

		#endregion GetAnimeGenre

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
		/// <param name="mangaGenre">Searched genre.</param>
		/// <returns>Information about manga genre</returns>
		Task<MangaGenre> GetMangaGenre(MangaGenreSearch mangaGenre);

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
		/// <param name="mangaGenre">Searched genre.</param>
		/// <param name="page">Index of page folding 100 records of top ranging (e.g. 1 will return first 100 records, 2 will return record from 101 to 200 etc.)</param>
		/// <returns>Information about manga genre</returns>
		Task<MangaGenre> GetMangaGenre(MangaGenreSearch mangaGenre, int page);

		#endregion GetMangaGenre

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

		#endregion GetProducer

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

		#endregion GetMagazine

		#endregion Magazine requests

		#region User requests

		#region GetUserProfile

		/// <summary>
		/// Returns information about user's profile with given username.
		/// </summary>
		/// <param name="username">Username.</param>
		/// <returns>Information about user's profile with given username.</returns>
		Task<UserProfile> GetUserProfile(string username);

		#endregion GetUserProfile

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

		#endregion GetUserHistory

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

		#endregion GetUserFriends

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

		/// <summary>
		/// Returns entries on user's anime list.
		/// </summary>
		/// <param name="username">Username.</param>
		/// <param name="searchConfig">Config to modify request input parameters.</param>
		/// <returns>Entries on user's anime list.</returns>
		Task<UserAnimeList> GetUserAnimeList(string username, UserListAnimeSearchConfig searchConfig);

		#endregion GetUserAnimeList

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

		/// <summary>
		/// Returns entries on user's manga list.
		/// </summary>
		/// <param name="username">Username.</param>
		/// <param name="searchConfig">Config to modify request input parameters.</param>
		/// <returns>Entries on user's manga list.</returns>
		Task<UserMangaList> GetUserMangaList(string username, UserListMangaSearchConfig searchConfig);

		#endregion GetUserMangaList

		#endregion User requests

		#region Club requests

		#region GetClub

		/// <summary>
		/// Return club's profile information.
		/// </summary>
		/// <param name="id">MAL id of the club.</param>
		/// <returns>Club's profile information.</returns>
		Task<Club> GetClub(long id);

		#endregion GetClub

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

		#endregion GetClubMembers

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
		/// <param name="searchConfig">Additional configuration for advanced search.</param>
		/// <returns>List of result related to search query.</returns>
		Task<AnimeSearchResult> SearchAnime(AnimeSearchConfig searchConfig);

		/// <summary>
		/// Returns list of results related to search.
		/// </summary>
		/// <param name="page">Index of page folding 50 records of top ranging (e.g. 1 will return first 50 records, 2 will return record from 51 to 100 etc.)</param>
		/// <param name="searchConfig">Additional configuration for advanced search.</param>
		/// <returns>List of result related to search query.</returns>
		Task<AnimeSearchResult> SearchAnime(AnimeSearchConfig searchConfig, int page);

		/// <summary>
		/// Returns list of results related to search.
		/// </summary>
		/// <param name="query">Search query.</param>
		/// <param name="page">Index of page folding 50 records of top ranging (e.g. 1 will return first 50 records, 2 will return record from 51 to 100 etc.)</param>
		/// <param name="searchConfig">Additional configuration for advanced search.</param>
		/// <returns>List of result related to search query.</returns>
		Task<AnimeSearchResult> SearchAnime(string query, int page, AnimeSearchConfig searchConfig);

		#endregion SearchAnime

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
		/// <param name="searchConfig">Additional configuration for advanced search.</param>
		/// <returns>List of result related to search query.</returns>
		Task<MangaSearchResult> SearchManga(MangaSearchConfig searchConfig);

		/// <summary>
		/// Returns list of results related to search.
		/// </summary>
		/// <param name="page">Index of page folding 50 records of top ranging (e.g. 1 will return first 50 records, 2 will return record from 51 to 100 etc.)</param>
		/// <param name="searchConfig">Additional configuration for advanced search.</param>
		/// <returns>List of result related to search query.</returns>
		Task<MangaSearchResult> SearchManga(MangaSearchConfig searchConfig, int page);

		/// <summary>
		/// Returns list of results related to search.
		/// </summary>
		/// <param name="query">Search query.</param>
		/// <param name="page">Index of page folding 50 records of top ranging (e.g. 1 will return first 50 records, 2 will return record from 51 to 100 etc.)</param>
		/// <param name="searchConfig">Additional configuration for advanced search.</param>
		/// <returns>List of result related to search query.</returns>
		Task<MangaSearchResult> SearchManga(string query, int page, MangaSearchConfig searchConfig);

		#endregion SearchManga

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

		#endregion SearchPerson

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

		#endregion SearchCharacter

		#endregion Search requests

		#region Metadata requests

		#region GetStatusMetadata

		/// <summary>
		/// Return Jikan REST API metadata - status.
		/// </summary>
		/// <returns>Jikan REST API metadata - status.</returns>
		Task<StatusMetadata> GetStatusMetadata();

		#endregion GetStatusMetadata

		#endregion Metadata requests
	}
}