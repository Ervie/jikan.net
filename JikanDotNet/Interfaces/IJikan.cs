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
		
		/// <summary>
		/// Returns anime with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <returns>Anime with given MAL id.</returns>
		Task<BaseJikanResponse<Anime>> GetAnimeAsync(long id);
		
		/// <summary>
		/// Returns collections of characters of anime with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <returns>Collection of characters of anime with given MAL id.</returns>
		Task<BaseJikanResponse<ICollection<AnimeCharacter>>> GetAnimeCharactersAsync(long id);
		
		/// <summary>
		/// Returns collections of staff of anime with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <returns>Collection of staff of anime with given MAL id.</returns>
		Task<BaseJikanResponse<ICollection<AnimeStaffPosition>>> GetAnimeStaffAsync(long id);
		
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
		
		/// <summary>
		/// Returns details about specific episode.
		/// </summary>
		/// <param name="animeId">MAL id of anime.</param>
		/// <param name="episodeId">Id of episode.</param>
		/// <returns>Details about specific episode.</returns>
		Task<BaseJikanResponse<AnimeEpisode>> GetAnimeEpisodeAsync(long animeId, int episodeId);
		
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
		
		/// <summary>
		/// Returns collections of videos related to anime with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <returns>Collections of videos related to anime with given MAL id.</returns>
		Task<BaseJikanResponse<AnimeVideos>> GetAnimeVideosAsync(long id);
		/// <summary>
		/// Returns collections of links to pictures related to anime with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <returns>Collections of links to pictures related to anime with given MAL id.</returns>
		Task<BaseJikanResponse<ICollection<ImagesSet>>> GetAnimePicturesAsync(long id);

		/// <summary>
		/// Returns statistics related to anime with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <returns>Statistics related to anime with given MAL id.</returns>
		Task<BaseJikanResponse<AnimeStatistics>> GetAnimeStatisticsAsync(long id);

		/// <summary>
		/// Returns additional information related to anime with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <returns>Additional information related to anime with given MAL id.</returns>
		Task<BaseJikanResponse<MoreInfo>> GetAnimeMoreInfoAsync(long id);

		/// <summary>
		/// Returns collection of anime recommendation.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <returns>Collection of anime recommendation.</returns>
		Task<BaseJikanResponse<ICollection<Recommendation>>> GetAnimeRecommendationsAsync(long id);

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
		/// <summary>
		/// Returns collection of anime reviews.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <returns>Collection of anime reviews.</returns>
		Task<PaginatedJikanResponse<ICollection<Review>>> GetAnimeReviewsAsync(long id);

		/// <summary>
		/// Returns collection of anime reviews.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <param name="page">Index of page folding 20 records of top ranging (e.g. 1 will return first 20 records, 2 will return record from 21 to 40 etc.)</param>
		/// <returns>Collection of anime reviews.</returns>
		Task<PaginatedJikanResponse<ICollection<Review>>> GetAnimeReviewsAsync(long id, int page);

		/// <summary>
		/// Returns collection of anime related entries.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <returns>Collection of anime related entries.</returns>
		Task<PaginatedJikanResponse<ICollection<RelatedEntry>>> GetAnimeRelationsAsync(long id);

		/// <summary>
		/// Returns collection of anime openings and endings.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <returns>Collection of anime openings and endings.</returns>
		Task<BaseJikanResponse<AnimeThemes>> GetAnimeThemesAsync(long id);
		
		/// <summary>
		/// Returns collection of external services links related to anime.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <returns>Collection of external services links related to anime.</returns>
		Task<BaseJikanResponse<ICollection<ExternalLink>>> GetAnimeExternalLinksAsync(long id);
		
		#endregion Anime requests

		#region Manga requests

		/// <summary>
		/// Returns manga with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of manga.</param>
		/// <returns>Manga with given MAL id.</returns>
		Task<BaseJikanResponse<Manga>> GetMangaAsync(long id);

		/// <summary>
		/// Returns collections of characters appearing in manga with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of manga.</param>
		/// <returns>Collections of characters appearing in manga with given MAL id.</returns>
		Task<BaseJikanResponse<ICollection<MangaCharacter>>> GetMangaCharactersAsync(long id);

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

		/// <summary>
		/// Returns collections of forum topics related to manga with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of manga.</param>
		/// <returns>Collections of forum topics related to manga with given MAL id.</returns>
		Task<BaseJikanResponse<ICollection<ForumTopic>>> GetMangaForumTopicsAsync(long id);

		/// <summary>
		/// Returns collections of links to pictures related to manga with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of manga.</param>
		/// <returns>Collections of links to pictures related to manga with given MAL id.</returns>
		Task<BaseJikanResponse<ICollection<ImagesSet>>> GetMangaPicturesAsync(long id);

		/// <summary>
		/// Returns statistics related to manga with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of manga.</param>
		/// <returns>Statistics related to manga with given MAL id.</returns>
		Task<BaseJikanResponse<MangaStatistics>> GetMangaStatisticsAsync(long id);

		/// <summary>
		/// Returns additional information related to manga with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of manga.</param>
		/// <returns>Collections of forum topics related to manga with given MAL id.</returns>
		Task<BaseJikanResponse<MoreInfo>> GetMangaMoreInfoAsync(long id);

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

		/// <summary>
		/// Returns collection of manga recommendation.
		/// </summary>
		/// <param name="id">MAL id of manga.</param>
		/// <returns>Collection of manga recomendation.</returns>
		Task<BaseJikanResponse<ICollection<Recommendation>>> GetMangaRecommendationsAsync(long id);

		/// <summary>
		/// Returns collection of manga reviews.
		/// </summary>
		/// <param name="id">MAL id of manga.</param>
		/// <returns>Collection of manga reviews.</returns>
		Task<PaginatedJikanResponse<ICollection<Review>>> GetMangaReviewsAsync(long id);

		/// <summary>
		/// Returns collection of manga related entries.
		/// </summary>
		/// <param name="id">MAL id of manga.</param>
		/// <returns>Collection of manga related entries.</returns>
		Task<PaginatedJikanResponse<ICollection<RelatedEntry>>> GetMangaRelationsAsync(long id);
		
		/// <summary>
		/// Returns collection of external services links related to manga.
		/// </summary>
		/// <param name="id">MAL id of manga.</param>
		/// <returns>Collection of external services links related to anime.</returns>
		Task<BaseJikanResponse<ICollection<ExternalLink>>> GetMangaExternalLinksAsync(long id);

		#endregion Manga requests

		#region Character requests

		/// <summary>
		/// Returns character with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of character.</param>
		/// <returns>Character with given MAL id.</returns>
		Task<BaseJikanResponse<Character>> GetCharacterAsync(long id);

		/// <summary>
		/// Returns return animeography of character with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of character.</param>
		/// <returns>Collection of anime where character has appeared.</returns>
		Task<BaseJikanResponse<ICollection<CharacterAnimeographyEntry>>> GetCharacterAnimeAsync(long id);

		/// <summary>
		/// Returns return mangaography of character with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of character.</param>
		/// <returns>Collection of manga where character has appeared.</returns>
		Task<BaseJikanResponse<ICollection<CharacterMangaographyEntry>>> GetCharacterMangaAsync(long id);

		/// <summary>
		/// Returns return voice actors of character with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of character.</param>
		/// <returns>Collection of voice actors voicing character.</returns>
		Task<BaseJikanResponse<ICollection<VoiceActorEntry>>> GetCharacterVoiceActorsAsync(long id);

		/// <summary>
		/// Returns collections of links to pictures related to character with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of character.</param>
		/// <returns>Collections of links to pictures related to character with given MAL id.</returns>
		Task<BaseJikanResponse<ICollection<ImagesSet>>> GetCharacterPicturesAsync(long id);

		#endregion Character requests

		#region Person requests

		/// <summary>
		/// Returns person with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of person.</param>
		/// <returns>Person with given MAL id.</returns>
		Task<BaseJikanResponse<Person>> GetPersonAsync(long id);
		
		/// <summary>
		/// Returns animeography of person with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of person.</param>
		/// <returns>Collection of anime the person collaborated on.</returns>
		Task<BaseJikanResponse<ICollection<PersonAnimeographyEntry>>> GetPersonAnimeAsync(long id);

		/// <summary>
		/// Returns mangaography of person with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of person.</param>
		/// <returns>Collection of manga the person worked on.</returns>
		Task<BaseJikanResponse<ICollection<PersonMangaographyEntry>>> GetPersonMangaAsync(long id);

		/// <summary>
		/// Returns voice acting roles of person with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of person.</param>
		/// <returns>Collection of voice acting roles of the person.</returns>
		Task<BaseJikanResponse<ICollection<VoiceActingRole>>> GetPersonVoiceActingRolesAsync(long id);

		/// <summary>
		/// Returns collections of links to pictures related to person with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of person.</param>
		/// <returns>Collections of links to pictures related to person with given MAL id.</returns>
		Task<BaseJikanResponse<ICollection<ImagesSet>>> GetPersonPicturesAsync(long id);

		#endregion Person requests

		#region Season requests

		/// <summary>
		/// Returns season preview.
		/// </summary>
		/// <param name="year">Year of selected season.</param>
		/// <param name="season">Selected season.</param>
		/// <returns>Season preview.</returns>
		Task<PaginatedJikanResponse<ICollection<Anime>>> GetSeasonAsync(int year, Season season);

		/// <summary>
		/// Returns list of available season to query with <see cref="GetSeasonAsync(int, Season)"/>
		/// </summary>
		/// <returns></returns>
		Task<PaginatedJikanResponse<ICollection<SeasonArchive>>> GetSeasonArchiveAsync();

		/// <summary>
		/// Return season preview for anime with undefined airing season (marked as "Later" on MAL).
		/// </summary>
		/// <returns>Season preview for anime with undefined airing date.</returns>
		Task<PaginatedJikanResponse<ICollection<Anime>>> GetUpcomingSeasonAsync();

		#endregion Season requests

		#region Schedule requests

		/// <summary>
		/// Returns current season schedule.
		/// </summary>
		/// <returns>Current season schedule.</returns>
		Task<PaginatedJikanResponse<ICollection<Anime>>> GetScheduleAsync();

		/// <summary>
		/// Returns current season schedule.
		/// </summary>
		/// <param name="page">Index of page folding 25 records of top ranging (e.g. 1 will return first 25 records, 2 will return record from 26 to 50 etc.)</param>
		/// <returns>Current season schedule.</returns>
		Task<PaginatedJikanResponse<ICollection<Anime>>> GetScheduleAsync(int page);

		/// <summary>
		/// Returns current season schedule.
		/// </summary>
		/// <param name="scheduledDay">Scheduled day to filter by.</param>
		/// <returns>Current season schedule.</returns>
		Task<PaginatedJikanResponse<ICollection<Anime>>> GetScheduleAsync(ScheduledDay scheduledDay);

		#endregion Schedule requests

		#region Top requests

		/// <summary>
		/// Returns list of top anime.
		/// </summary>
		/// <returns>List of top anime.</returns>
		Task<PaginatedJikanResponse<ICollection<Anime>>> GetTopAnimeAsync();

		/// <summary>
		/// Returns list of top anime.
		/// </summary>
		/// <param name="page">Index of page folding 25 records of top ranging (e.g. 1 will return first 25 records, 2 will return record from 26 to 50 etc.)</param>
		/// <returns>List of top anime.</returns>
		Task<PaginatedJikanResponse<ICollection<Anime>>> GetTopAnimeAsync(int page);

		/// <summary>
		/// Returns list of top manga.
		/// </summary>
		/// <returns>List of top manga.</returns>
		Task<PaginatedJikanResponse<ICollection<Manga>>> GetTopMangaAsync();

		/// <summary>
		/// Returns list of top manga.
		/// </summary>
		/// <param name="page">Index of page folding 25 records of top ranging (e.g. 1 will return first 25 records, 2 will return record from 26 to 50 etc.)</param>
		/// <returns>List of top manga.</returns>
		Task<PaginatedJikanResponse<ICollection<Manga>>> GetTopMangaAsync(int page);

		/// <summary>
		/// Returns list of most popular people.
		/// </summary>
		/// <returns>List of most popular people.</returns>
		Task<PaginatedJikanResponse<ICollection<Person>>> GetTopPeopleAsync();

		/// <summary>
		/// Returns list of most popular people.
		/// </summary>
		/// <param name="page">Index of page folding 25 records of top ranging (e.g. 1 will return first 25 records, 2 will return record from 26 to 50 etc.)</param>
		/// <returns>List of most popular people.</returns>
		Task<PaginatedJikanResponse<ICollection<Person>>> GetTopPeopleAsync(int page);

		/// <summary>
		/// Returns list of most popular characters.
		/// </summary>
		/// <returns>List of most popular characters.</returns>
		Task<PaginatedJikanResponse<ICollection<Character>>> GetTopCharactersAsync();

		/// <summary>
		/// Returns list of most popular characters.
		/// </summary>
		/// <param name="page">Index of page folding 25 records of top ranging (e.g. 1 will return first 25 records, 2 will return record from 26 to 50 etc.)</param>
		/// <returns>List of most popular characters.</returns>
		Task<PaginatedJikanResponse<ICollection<Character>>> GetTopCharactersAsync(int page);

		/// <summary>
		/// Returns list of most popular characters.
		/// </summary>
		/// <returns>List of most popular characters.</returns>
		Task<PaginatedJikanResponse<ICollection<Review>>> GetTopReviewsAsync();

		/// <summary>
		/// Returns list of most popular characters.
		/// </summary>
		/// <param name="page">Index of page folding 25 records of top ranging (e.g. 1 will return first 25 records, 2 will return record from 26 to 50 etc.)</param>
		/// <returns>List of most popular characters.</returns>
		Task<PaginatedJikanResponse<ICollection<Review>>> GetTopReviewsAsync(int page);

		#endregion Top requests

		#region Genre requests

		/// <summary>
		/// Returns list of anime genres.
		/// </summary>
		/// <returns>List of anime genres</returns>
		Task<BaseJikanResponse<ICollection<Genre>>> GetAnimeGenresAsync();

		/// <summary>
		/// Returns list of anime genres.
		/// </summary>
		/// <param name="filter">Filter for genre types.</param>
		/// <returns>List of anime genres</returns>
		Task<BaseJikanResponse<ICollection<Genre>>> GetAnimeGenresAsync(GenresFilter filter);

		/// <summary>
		/// Returns list of manga genres.
		/// </summary>
		/// <returns>List of manga genres</returns>
		Task<BaseJikanResponse<ICollection<Genre>>> GetMangaGenresAsync();

		/// <summary>
		/// Returns list of manga genres.
		/// </summary>
		/// <param name="filter">Filter for genre types.</param>
		/// <returns>List of manga genres</returns>
		Task<BaseJikanResponse<ICollection<Genre>>> GetMangaGenresAsync(GenresFilter filter);

		#endregion Genre requests

		#region Producer requests

		/// <summary>
		/// Returns information about producers.
		/// </summary>
		/// <returns>Basic Information about producers.</returns>
		Task<PaginatedJikanResponse<ICollection<Producer>>> GetProducersAsync();

		/// <summary>
		/// Returns information about producers.
		/// </summary>
		/// <param name="page">Index of page folding 100 records of top ranging (e.g. 1 will return first 100 records, 2 will return record from 101 to 200 etc.)</param>
		/// <returns>Basic Information about producers.</returns>
		Task<PaginatedJikanResponse<ICollection<Producer>>> GetProducersAsync(int page);

		#endregion Producer requests

		#region Magazine requests

		/// <summary>
		/// Returns information about magazines.
		/// </summary>
		/// <returns>Basic Information about magazines.</returns>
		Task<PaginatedJikanResponse<ICollection<Magazine>>> GetMagazinesAsync();

		/// <summary>
		/// Returns information about magazines.
		/// </summary>
		/// <param name="page">Index of page folding 100 records of top ranging (e.g. 1 will return first 100 records, 2 will return record from 101 to 200 etc.)</param>
		/// <returns>Basic Information about magazines.</returns>
		Task<PaginatedJikanResponse<ICollection<Magazine>>> GetMagazinesAsync(int page);

		#endregion Magazine requests

		#region Club requests

		/// <summary>
		/// Return club's profile information.
		/// </summary>
		/// <param name="id">MAL id of the club.</param>
		/// <returns>Club's profile information.</returns>
		Task<BaseJikanResponse<Club>> GetClubAsync(long id);

		/// <summary>
		/// Return club's member list.
		/// </summary>
		/// <param name="id">MAL id of the club.</param>
		/// <returns>Club's member list.</returns>
		Task<PaginatedJikanResponse<ICollection<ClubMember>>> GetClubMembersAsync(long id);

		/// <summary>
		/// Return club's member list.
		/// </summary>
		/// <param name="id">MAL id of the club.</param>
		/// <param name="page">Index of page folding 36 records of top ranging (e.g. 1 will return first 36 records, 2 will return record from 37 to 72 etc.)</param>
		/// <returns>Club's member list.</returns>
		Task<PaginatedJikanResponse<ICollection<ClubMember>>> GetClubMembersAsync(long id, int page);

		/// <summary>
		/// Return club's staff list.
		/// </summary>
		/// <param name="id">MAL id of the club.</param>
		/// <returns>Club's staff list.</returns>
		Task<BaseJikanResponse<ICollection<ClubStaff>>> GetClubStaffAsync(long id);

		/// <summary>
		/// Return club's related entities.
		/// </summary>
		/// <param name="id">MAL id of the club.</param>
		/// <returns>Club's related entities collections..</returns>
		Task<BaseJikanResponse<ClubRelations>> GetClubRelationsAsync(long id);

		#endregion Club requests

		#region User requests
		
		/// <summary>
		/// Returns information about user's profile with given username.
		/// </summary>
		/// <param name="username">Username.</param>
		/// <returns>Information about user's profile with given username.</returns>
		Task<BaseJikanResponse<UserProfile>> GetUserProfileAsync(string username);

		/// <summary>
		/// Returns information about user's anime and manga statistics
		/// </summary>
		/// <param name="username">Username.</param>
		/// <returns>Information about user's anime and manga statistics.</returns>
		Task<BaseJikanResponse<UserStatistics>> GetUserStatisticsAsync(string username);

		/// <summary>
		/// Returns information about user's favorite section.
		/// </summary>
		/// <param name="username">Username.</param>
		/// <returns>Information about user's favorite section..</returns>
		Task<BaseJikanResponse<UserFavorites>> GetUserFavoritesAsync(string username);

		/// <summary>
		/// Returns information about user's description on the profile.
		/// </summary>
		/// <param name="username">Username.</param>
		/// <returns>Information about user's description on the profile.</returns>
		Task<BaseJikanResponse<UserAbout>> GetUserAboutAsync(string username);

		/// <summary>
		/// Returns information about user's history with given username.
		/// </summary>
		/// <param name="username">Username.</param>
		/// <returns>Information about user's profile with given username.</returns>
		Task<BaseJikanResponse<ICollection<HistoryEntry>>> GetUserHistoryAsync(string username);

		/// <summary>
		/// Returns information about user's history with given username.
		/// </summary>
		/// <param name="username">Username.</param>
		/// <param name="filter">Option to filter history.</param>
		/// <returns>Information about user's profile with given username.</returns>
		Task<BaseJikanResponse<ICollection<HistoryEntry>>> GetUserHistoryAsync(string username, UserHistoryExtension filter);

		/// <summary>
		/// Returns information about user's friends with given username.
		/// </summary>
		/// <param name="username">Username.</param>
		/// <returns>Information about user's friends with given username.</returns>
		Task<PaginatedJikanResponse<ICollection<Friend>>> GetUserFriendsAsync(string username);

		/// <summary>
		/// Returns information about user's friends with given username.
		/// </summary>
		/// <param name="username">Username.</param>
		/// <param name="page">Index of the page.</param>
		/// <returns>Information about user's friends with given username.</returns>
		Task<PaginatedJikanResponse<ICollection<Friend>>> GetUserFriendsAsync(string username, int page);

		/// <summary>
		/// Returns entries on user's anime list.
		/// </summary>
		/// <param name="username">Username.</param>
		/// <returns>Entries on user's anime list.</returns>
		Task<BaseJikanResponse<ICollection<AnimeListEntry>>> GetUserAnimeListAsync(string username);

		/// <summary>
		/// Returns entries on user's anime list.
		/// </summary>
		/// <param name="username">Username.</param>
		/// <param name="page">Index of page folding 300 records of top ranging (e.g. 1 will return first 300 records, 2 will return record from 301 to 600 etc.)</param>
		/// <returns>Entries on user's anime list.</returns>
		Task<BaseJikanResponse<ICollection<AnimeListEntry>>> GetUserAnimeListAsync(string username, int page);

		/// <summary>
		/// Returns entries on user's manga list.
		/// </summary>
		/// <param name="username">Username.</param>
		/// <returns>Entries on user's manga list.</returns>
		Task<BaseJikanResponse<ICollection<MangaListEntry>>> GetUserMangaListAsync(string username);

		/// <summary>
		/// Returns entries on user's manga list.
		/// </summary>
		/// <param name="username">Username.</param>
		/// <param name="page">Index of page folding 300 records of top ranging (e.g. 1 will return first 300 records, 2 will return record from 301 to 600 etc.)</param>
		/// <returns>Entries on user's manga list.</returns>
		Task<BaseJikanResponse<ICollection<MangaListEntry>>> GetUserMangaListAsync(string username, int page);
		
		/// <summary>
		/// Returns user's reviews.
		/// </summary>
		/// <param name="username">Username.</param>
		/// <returns>User's reviews.</returns>
		Task<PaginatedJikanResponse<ICollection<Review>>> GetUserReviewsAsync(string username);

		/// <summary>
		/// Returns user's reviews.
		/// </summary>
		/// <param name="username">Username.</param>
		/// <param name="page">Index of page folding 10 records of top ranging (e.g. 1 will return first 10 records, 2 will return record from 11 to 21 etc.)</param>
		/// <returns>User's reviews.</returns>
		Task<PaginatedJikanResponse<ICollection<Review>>> GetUserReviewsAsync(string username, int page);

		/// <summary>
		/// Returns user's recommendations.
		/// </summary>
		/// <param name="username">Username.</param>
		/// <returns>User's recommendations.</returns>
		Task<PaginatedJikanResponse<ICollection<UserRecommendation>>> GetUserRecommendationsAsync(string username);

		/// <summary>
		/// Returns user's recommendations.
		/// </summary>
		/// <param name="username">Username.</param>
		/// <param name="page">Index of page folding 10 records of top ranging (e.g. 1 will return first 10 records, 2 will return record from 11 to 21 etc.)</param>
		/// <returns>User's recommendations.</returns>
		Task<PaginatedJikanResponse<ICollection<UserRecommendation>>> GetUserRecommendationsAsync(string username, int page);

		/// <summary>
		/// Returns user's clubs.
		/// </summary>
		/// <param name="username">Username.</param>
		/// <returns>User's clubs.</returns>
		Task<PaginatedJikanResponse<ICollection<MalUrl>>> GetUserClubsAsync(string username);

		/// <summary>
		/// Returns user's clubs.
		/// </summary>
		/// <param name="username">Username.</param>
		/// <param name="page">Index of page folding 10 records of top ranging (e.g. 1 will return first 10 records, 2 will return record from 11 to 21 etc.)</param>
		/// <returns>User's clubs.</returns>
		Task<PaginatedJikanResponse<ICollection<MalUrl>>> GetUserClubsAsync(string username, int page);

		#endregion User requests

		#region GetRandom requests

		/// <summary>
		/// Gets random anime.
		/// </summary>
		/// <returns>Random anime</returns>
		Task<BaseJikanResponse<Anime>> GetRandomAnimeAsync();

		/// <summary>
		/// Gets random manga.
		/// </summary>
		/// <returns>Random manga</returns>
		Task<BaseJikanResponse<Manga>> GetRandomMangaAsync();

		/// <summary>
		/// Gets random character.
		/// </summary>
		/// <returns>Random character</returns>
		Task<BaseJikanResponse<Character>> GetRandomCharacterAsync();

		/// <summary>
		/// Gets random person.
		/// </summary>
		/// <returns>Random person</returns>
		Task<BaseJikanResponse<Person>> GetRandomPersonAsync();

		/// <summary>
		/// Gets random user.
		/// </summary>
		/// <returns>Random character</returns>
		Task<BaseJikanResponse<UserProfile>> GetRandomUserAsync();

		#endregion

		#region Recommendations requests

		/// <summary>
		/// Gets collection of recently added anime recommendations.
		/// </summary>
		/// <returns>Collection of recently added recommendations.r</returns>
		Task<PaginatedJikanResponse<ICollection<UserRecommendation>>> GetRecentAnimeRecommendationsAsync();
		
		/// <summary>
		/// Gets collection of recently added anime recommendations.
		/// </summary>
		/// <param name="page">Index of page folding 100 records of top ranging (e.g. 1 will return first 100 records, 2 will return record from 101 to 200 etc.)</param>
		/// <returns>Collection of recently added recommendations.r</returns>
		Task<PaginatedJikanResponse<ICollection<UserRecommendation>>> GetRecentAnimeRecommendationsAsync(int page);

		/// <summary>
		/// Gets collection of recently added manga recommendations.
		/// </summary>
		/// <returns>Collection of recently added recommendations.r</returns>
		Task<PaginatedJikanResponse<ICollection<UserRecommendation>>> GetRecentMangaRecommendationsAsync();
		
		/// <summary>
		/// Gets collection of recently added manga recommendations.
		/// </summary>
		/// <param name="page">Index of page folding 100 records of top ranging (e.g. 1 will return first 100 records, 2 will return record from 101 to 200 etc.)</param>
		/// <returns>Collection of recently added recommendations.r</returns>
		Task<PaginatedJikanResponse<ICollection<UserRecommendation>>> GetRecentMangaRecommendationsAsync(int page);
		
		#endregion

		#region Reviews requests

		/// <summary>
		/// Gets collection of recently added anime reviews.
		/// </summary>
		/// <returns>Collection of recently added anime reviews</returns>
		Task<PaginatedJikanResponse<ICollection<Review>>> GetRecentAnimeReviewsAsync();
		
		/// <summary>
		/// Gets collection of recently added reviews.
		/// </summary>
		/// <param name="page">Index of page folding 50 records of top ranging (e.g. 1 will return first 50 records, 2 will return record from 51 to 100 etc.)</param>
		/// <returns>Collection of recently added reviews</returns>
		Task<PaginatedJikanResponse<ICollection<Review>>> GetRecentAnimeReviewsAsync(int page);

		/// <summary>
		/// Gets collection of recently added manga reviews.
		/// </summary>
		/// <returns>Collection of recently added manga reviews</returns>
		Task<PaginatedJikanResponse<ICollection<Review>>> GetRecentMangaReviewsAsync();
		
		/// <summary>
		/// Gets collection of recently added manga reviews.
		/// </summary>
		/// <param name="page">Index of page folding 50 records of top ranging (e.g. 1 will return first 50 records, 2 will return record from 51 to 100 etc.)</param>
		/// <returns>Collection of recently added manga reviews.</returns>
		Task<PaginatedJikanResponse<ICollection<Review>>> GetRecentMangaReviewsAsync(int page);

		#endregion

		#region Watch requests

		/// <summary>
		/// Return collection of recently released episodes details.
		/// </summary>
		/// <returns>Collection of recently released episodes details..</returns>
		Task<PaginatedJikanResponse<ICollection<WatchEpisode>>> GetWatchRecentEpisodesAsync();
		
		/// <summary>
		/// Return collection of popular episodes details.
		/// </summary>
		/// <returns>Collection of popular episodes details.</returns>
		Task<PaginatedJikanResponse<ICollection<WatchEpisode>>> GetWatchPopularEpisodesAsync();

		/// <summary>
		/// Return collection of recently released promos details.
		/// </summary>
		/// <returns>Collection of recently released promos details.</returns>
		Task<PaginatedJikanResponse<ICollection<WatchPromoVideo>>> GetWatchRecentPromosAsync();
		
		/// <summary>
		/// Return collection of popular promos details.
		/// </summary>
		/// <returns>Collection of popular promos details.</returns>
		Task<PaginatedJikanResponse<ICollection<WatchPromoVideo>>> GetWatchPopularPromosAsync();

		#endregion Watch requests

		#region Search requests

		/// <summary>
		/// Returns list of results related to search.
		/// </summary>
		/// <param name="query">Search query.</param>
		/// <returns>List of result related to search query.</returns>
		Task<PaginatedJikanResponse<ICollection<Anime>>> SearchAnimeAsync(string query);
		
		/// <summary>
		/// Returns list of results related to search.
		/// </summary>
		/// <param name="searchConfig">Additional configuration for advanced search.</param>
		/// <returns>List of result related to search query.</returns>
		Task<PaginatedJikanResponse<ICollection<Anime>>> SearchAnimeAsync(AnimeSearchConfig searchConfig);
		
		/// <summary>
		/// Returns list of results related to search.
		/// </summary>
		/// <param name="query">Search query.</param>
		/// <returns>List of result related to search query.</returns>
		Task<PaginatedJikanResponse<ICollection<Manga>>> SearchMangaAsync(string query);
		
		/// <summary>
		/// Returns list of results related to search.
		/// </summary>
		/// <param name="searchConfig">Additional configuration for advanced search.</param>
		/// <returns>List of result related to search query.</returns>
		Task<PaginatedJikanResponse<ICollection<Manga>>> SearchMangaAsync(MangaSearchConfig searchConfig);
		
		/// <summary>
		/// Returns list of results related to search.
		/// </summary>
		/// <param name="query">Search query.</param>
		/// <returns>List of result related to search query.</returns>
		Task<PaginatedJikanResponse<ICollection<Person>>> SearchPersonAsync(string query);

		/// <summary>
		/// Returns list of results related to search.
		/// </summary>
		/// <param name="searchConfig">Additional configuration for advanced search.</param>
		/// <returns>List of result related to search query.</returns>
		Task<PaginatedJikanResponse<ICollection<Person>>> SearchPersonAsync(PersonSearchConfig searchConfig);

		/// <summary>
		/// Returns list of results related to search.
		/// </summary>
		/// <param name="query">Search query.</param>
		/// <returns>List of result related to search query.</returns>
		Task<PaginatedJikanResponse<ICollection<Character>>> SearchCharacterAsync(string query);

		/// <summary>
		/// Returns list of results related to search.
		/// </summary>
		/// <param name="searchConfig">Additional configuration for advanced search.</param>
		/// <returns>List of result related to search query.</returns>
		Task<PaginatedJikanResponse<ICollection<Character>>> SearchCharacterAsync(CharacterSearchConfig searchConfig);
		
		/// <summary>
		/// Returns list of results related to search.
		/// </summary>
		/// <param name="query">Search query.</param>
		/// <returns>List of result related to search query.</returns>
		Task<PaginatedJikanResponse<ICollection<UserMetadata>>> SearchUserAsync(string query);

		/// <summary>
		/// Returns list of results related to search.
		/// </summary>
		/// <param name="searchConfig">Additional configuration for advanced search.</param>
		/// <returns>List of result related to search query.</returns>
		Task<PaginatedJikanResponse<ICollection<UserMetadata>>> SearchUserAsync(UserSearchConfig searchConfig);

		#endregion Search requests
	}
}