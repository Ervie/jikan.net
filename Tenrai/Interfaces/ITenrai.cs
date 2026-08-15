using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tenrai.Consts;

namespace Tenrai
{
	/// <summary>
	/// Interface for Tenrai.Net client
	/// </summary>
	public interface ITenrai
	{
        #region Anime requests

        /// <summary>
        /// Returns anime with given MAL id.
        /// </summary>
        /// <param name="id">MAL id of anime.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Anime with given MAL id.</returns>
        Task<BaseTenraiResponse<Anime>> GetAnimeAsync(long id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns collections of characters of anime with given MAL id.
        /// </summary>
        /// <param name="id">MAL id of anime.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Collection of characters of anime with given MAL id.</returns>
        Task<BaseTenraiResponse<ICollection<AnimeCharacter>>> GetAnimeCharactersAsync(long id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns collections of staff of anime with given MAL id.
        /// </summary>
        /// <param name="id">MAL id of anime.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Collection of staff of anime with given MAL id.</returns>
        Task<BaseTenraiResponse<ICollection<AnimeStaffPosition>>> GetAnimeStaffAsync(long id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns list of episodes for anime with given MAL id.
        /// </summary>
        /// <param name="id">MAL id of anime.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>List of episodes with details.</returns>
        Task<PaginatedTenraiResponse<ICollection<AnimeEpisode>>> GetAnimeEpisodesAsync(long id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns list of episodes for anime with given MAL id.
        /// </summary>
        /// <param name="id">MAL id of anime.</param>
        /// <param name="page">Index of page folding 100 records of top ranging (e.g. 1 will return first 100 records, 2 will return record from 101 to 200 etc.)</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>List of episodes with details.</returns>
        Task<PaginatedTenraiResponse<ICollection<AnimeEpisode>>> GetAnimeEpisodesAsync(long id, int page, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns details about specific episode.
        /// </summary>
        /// <param name="animeId">MAL id of anime.</param>
        /// <param name="episodeId">Id of episode.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Details about specific episode.</returns>
        Task<BaseTenraiResponse<AnimeEpisode>> GetAnimeEpisodeAsync(long animeId, int episodeId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns collections of news related to anime with given MAL id.
        /// </summary>
        /// <param name="id">MAL id of anime.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Collections of news related to anime with given MAL id.</returns>
        Task<PaginatedTenraiResponse<ICollection<News>>> GetAnimeNewsAsync(long id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns collections of news related to anime with given MAL id.
        /// </summary>
        /// <param name="id">MAL id of anime.</param>
        /// <param name="page">Index of page folding 100 records of top ranging (e.g. 1 will return first 100 records, 2 will return record from 101 to 200 etc.)</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Collections of news related to anime with given MAL id.</returns>
        Task<PaginatedTenraiResponse<ICollection<News>>> GetAnimeNewsAsync(long id, int page, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns collections of forum topics related to anime with given MAL id.
        /// </summary>
        /// <param name="id">MAL id of anime.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Collections of forum topics related to anime with given MAL id.</returns>
        [Obsolete(ErrorMessagesConst.UnsupportedForum)]
        Task<BaseTenraiResponse<ICollection<ForumTopic>>> GetAnimeForumTopicsAsync(long id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns collections of forum topics related to anime with given MAL id.
        /// </summary>
        /// <param name="id">MAL id of anime.</param>
        /// <param name="type">ForumTopicType filter</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Collections of forum topics related to anime with given MAL id.</returns>
        [Obsolete(ErrorMessagesConst.UnsupportedForum)]
        Task<BaseTenraiResponse<ICollection<ForumTopic>>> GetAnimeForumTopicsAsync(long id, ForumTopicType type, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns collections of videos related to anime with given MAL id.
        /// </summary>
        /// <param name="id">MAL id of anime.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Collections of videos related to anime with given MAL id.</returns>
        Task<BaseTenraiResponse<AnimeVideos>> GetAnimeVideosAsync(long id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns collections of video episodes related to anime with given MAL id.
        /// </summary>
        /// <param name="id">MAL id of anime.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Collections of video episodes related to anime with given MAL id.</returns>
        Task<PaginatedTenraiResponse<ICollection<EpisodeVideo>>> GetAnimeVideosEpisodesAsync(long id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns collections of video episodes related to anime with given MAL id.
        /// </summary>
        /// <param name="id">MAL id of anime.</param>
        /// <param name="page">Index of page folding 100 records of top ranging (e.g. 1 will return first 100 records, 2 will return record from 101 to 200 etc.)</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Collections of video episodes related to anime with given MAL id.</returns>
        Task<PaginatedTenraiResponse<ICollection<EpisodeVideo>>> GetAnimeVideosEpisodesAsync(long id, int page, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns collections of links to pictures related to anime with given MAL id.
        /// </summary>
        /// <param name="id">MAL id of anime.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Collections of links to pictures related to anime with given MAL id.</returns>
        Task<BaseTenraiResponse<ICollection<ImagesSet>>> GetAnimePicturesAsync(long id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns statistics related to anime with given MAL id.
        /// </summary>
        /// <param name="id">MAL id of anime.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Statistics related to anime with given MAL id.</returns>
        Task<BaseTenraiResponse<AnimeStatistics>> GetAnimeStatisticsAsync(long id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns additional information related to anime with given MAL id.
        /// </summary>
        /// <param name="id">MAL id of anime.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Additional information related to anime with given MAL id.</returns>
        Task<BaseTenraiResponse<MoreInfo>> GetAnimeMoreInfoAsync(long id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns collection of anime recommendation.
        /// </summary>
        /// <param name="id">MAL id of anime.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Collection of anime recommendation.</returns>
        Task<BaseTenraiResponse<ICollection<Recommendation>>> GetAnimeRecommendationsAsync(long id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns collection of anime user updates.
        /// </summary>
        /// <param name="id">MAL id of anime.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Collection of anime user updates.</returns>
        [Obsolete(ErrorMessagesConst.UnsupportedUserUpdates)]
        Task<PaginatedTenraiResponse<ICollection<AnimeUserUpdate>>> GetAnimeUserUpdatesAsync(long id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns collection of anime user updates.
        /// </summary>
        /// <param name="id">MAL id of anime.</param>
        /// <param name="page">Index of page folding 75 records of top ranging (e.g. 1 will return first 75 records, 2 will return record from 76 to 150 etc.)</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Collection of anime user updates.</returns>
        [Obsolete(ErrorMessagesConst.UnsupportedUserUpdates)]
        Task<PaginatedTenraiResponse<ICollection<AnimeUserUpdate>>> GetAnimeUserUpdatesAsync(long id, int page, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// Returns collection of anime reviews.
        /// </summary>
        /// <param name="id">MAL id of anime.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Collection of anime reviews.</returns>
        Task<PaginatedTenraiResponse<ICollection<Review>>> GetAnimeReviewsAsync(long id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns collection of anime reviews, filtered and sorted using the supplied configuration.
        /// </summary>
        /// <param name="id">MAL id of anime.</param>
        /// <param name="searchConfig">Query configuration (page, sort order, preliminary/spoiler filters, sentiment).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Collection of anime reviews.</returns>
        Task<PaginatedTenraiResponse<ICollection<Review>>> GetAnimeReviewsAsync(long id, ReviewsSearchConfig searchConfig, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns collection of anime related entries.
        /// </summary>
        /// <param name="id">MAL id of anime.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Collection of anime related entries.</returns>
        Task<PaginatedTenraiResponse<ICollection<RelatedEntry>>> GetAnimeRelationsAsync(long id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns collection of anime openings and endings.
        /// </summary>
        /// <param name="id">MAL id of anime.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Collection of anime openings and endings.</returns>
        Task<BaseTenraiResponse<AnimeThemes>> GetAnimeThemesAsync(long id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns collection of external services links related to anime.
        /// </summary>
        /// <param name="id">MAL id of anime.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Collection of external services links related to anime.</returns>
        Task<BaseTenraiResponse<ICollection<ExternalLink>>> GetAnimeExternalLinksAsync(long id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns collection of external streaming services links related to anime.
        /// </summary>
        /// <param name="id">MAL id of anime.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Collection of external services links related to anime.</returns>
        Task<BaseTenraiResponse<ICollection<ExternalLink>>> GetAnimeStreamingLinksAsync(long id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns anime with additional data.
        /// </summary>
        /// <param name="id">MAL id of anime.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Anime with additional data.</returns>
        Task<BaseTenraiResponse<AnimeFull>> GetAnimeFullDataAsync(long id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns the complete collection of anime MAL IDs available in the catalogue.
        /// </summary>
        /// <remarks>Requires a Tenrai Server Key (set <see cref="Config.TenraiClientConfiguration.ServerKey"/>); otherwise the API responds with 401.</remarks>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Collection of anime MAL IDs.</returns>
        Task<BaseTenraiResponse<ICollection<long>>> GetAnimeIdsAsync(CancellationToken cancellationToken = default);

        #endregion Anime requests

        #region Manga requests

        /// <summary>
        /// Returns manga with given MAL id.
        /// </summary>
        /// <param name="id">MAL id of manga.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Manga with given MAL id.</returns>
        Task<BaseTenraiResponse<Manga>> GetMangaAsync(long id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns collections of characters appearing in manga with given MAL id.
        /// </summary>
        /// <param name="id">MAL id of manga.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Collections of characters appearing in manga with given MAL id.</returns>
        Task<BaseTenraiResponse<ICollection<MangaCharacter>>> GetMangaCharactersAsync(long id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns collections of news related to manga with given MAL id.
        /// </summary>
        /// <param name="id">MAL id of manga.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Collections of news related to manga with given MAL id.</returns>
        Task<PaginatedTenraiResponse<ICollection<News>>> GetMangaNewsAsync(long id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns collections of news related to manga with given MAL id.
        /// </summary>
        /// <param name="id">MAL id of manga.</param>
        /// <param name="page">Index of page folding 100 records of top ranging (e.g. 1 will return first 100 records, 2 will return record from 101 to 200 etc.)</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Collections of news related to manga with given MAL id.</returns>
        Task<PaginatedTenraiResponse<ICollection<News>>> GetMangaNewsAsync(long id, int page, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns collections of forum topics related to manga with given MAL id.
        /// </summary>
        /// <param name="id">MAL id of manga.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Collections of forum topics related to manga with given MAL id.</returns>
        [Obsolete(ErrorMessagesConst.UnsupportedForum)]
        Task<BaseTenraiResponse<ICollection<ForumTopic>>> GetMangaForumTopicsAsync(long id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns collections of forum topics related to manga with given MAL id.
        /// </summary>
        /// <param name="id">MAL id of manga.</param>
        /// <param name="type">ForumTopicType filter</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Collections of forum topics related to manga with given MAL id.</returns>
        [Obsolete(ErrorMessagesConst.UnsupportedForum)]
        Task<BaseTenraiResponse<ICollection<ForumTopic>>> GetMangaForumTopicsAsync(long id, ForumTopicType type, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns collections of links to pictures related to manga with given MAL id.
        /// </summary>
        /// <param name="id">MAL id of manga.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Collections of links to pictures related to manga with given MAL id.</returns>
        Task<BaseTenraiResponse<ICollection<ImagesSet>>> GetMangaPicturesAsync(long id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns statistics related to manga with given MAL id.
        /// </summary>
        /// <param name="id">MAL id of manga.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Statistics related to manga with given MAL id.</returns>
        Task<BaseTenraiResponse<MangaStatistics>> GetMangaStatisticsAsync(long id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns additional information related to manga with given MAL id.
        /// </summary>
        /// <param name="id">MAL id of manga.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Collections of forum topics related to manga with given MAL id.</returns>
        Task<BaseTenraiResponse<MoreInfo>> GetMangaMoreInfoAsync(long id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns collection of manga user updates.
        /// </summary>
        /// <param name="id">MAL id of manga.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Collection of manga user updates.</returns>
        [Obsolete(ErrorMessagesConst.UnsupportedUserUpdates)]
        Task<PaginatedTenraiResponse<ICollection<MangaUserUpdate>>> GetMangaUserUpdatesAsync(long id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns collection of manga user updates.
        /// </summary>
        /// <param name="id">MAL id of manga.</param>
        /// <param name="page">Index of page folding 75 records of top ranging (e.g. 1 will return first 75 records, 2 will return record from 76 to 150 etc.)</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Collection of manga user updates.</returns>
        [Obsolete(ErrorMessagesConst.UnsupportedUserUpdates)]
        Task<PaginatedTenraiResponse<ICollection<MangaUserUpdate>>> GetMangaUserUpdatesAsync(long id, int page, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns collection of manga recommendation.
        /// </summary>
        /// <param name="id">MAL id of manga.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Collection of manga recomendation.</returns>
        Task<BaseTenraiResponse<ICollection<Recommendation>>> GetMangaRecommendationsAsync(long id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns collection of manga reviews.
        /// </summary>
        /// <param name="id">MAL id of manga.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Collection of manga reviews.</returns>
        Task<PaginatedTenraiResponse<ICollection<Review>>> GetMangaReviewsAsync(long id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns collection of manga reviews, filtered and sorted using the supplied configuration.
        /// </summary>
        /// <param name="id">MAL id of manga.</param>
        /// <param name="searchConfig">Query configuration (page, sort order, preliminary/spoiler filters, sentiment).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Collection of manga reviews.</returns>
        Task<PaginatedTenraiResponse<ICollection<Review>>> GetMangaReviewsAsync(long id, ReviewsSearchConfig searchConfig, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns collection of manga related entries.
        /// </summary>
        /// <param name="id">MAL id of manga.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Collection of manga related entries.</returns>
        Task<PaginatedTenraiResponse<ICollection<RelatedEntry>>> GetMangaRelationsAsync(long id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns collection of external services links related to manga.
        /// </summary>
        /// <param name="id">MAL id of manga.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Collection of external services links related to anime.</returns>
        Task<BaseTenraiResponse<ICollection<ExternalLink>>> GetMangaExternalLinksAsync(long id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns manga with additional data.
        /// </summary>
        /// <param name="id">MAL id of manga.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Manga with additional data.</returns>
        Task<BaseTenraiResponse<MangaFull>> GetMangaFullDataAsync(long id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns the complete collection of manga MAL IDs available in the catalogue.
        /// </summary>
        /// <remarks>Requires a Tenrai Server Key (set <see cref="Config.TenraiClientConfiguration.ServerKey"/>); otherwise the API responds with 401.</remarks>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Collection of manga MAL IDs.</returns>
        Task<BaseTenraiResponse<ICollection<long>>> GetMangaIdsAsync(CancellationToken cancellationToken = default);

        #endregion Manga requests

        #region Character requests

        /// <summary>
        /// Returns character with given MAL id.
        /// </summary>
        /// <param name="id">MAL id of character.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Character with given MAL id.</returns>
        Task<BaseTenraiResponse<Character>> GetCharacterAsync(long id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns animeography of character with given MAL id.
        /// </summary>
        /// <param name="id">MAL id of character.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Collection of anime where character has appeared.</returns>
        Task<BaseTenraiResponse<ICollection<CharacterAnimeographyEntry>>> GetCharacterAnimeAsync(long id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns mangaography of character with given MAL id.
        /// </summary>
        /// <param name="id">MAL id of character.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Collection of manga where character has appeared.</returns>
        Task<BaseTenraiResponse<ICollection<CharacterMangaographyEntry>>> GetCharacterMangaAsync(long id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns voice actors of character with given MAL id.
        /// </summary>
        /// <param name="id">MAL id of character.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Collection of voice actors voicing character.</returns>
        Task<BaseTenraiResponse<ICollection<VoiceActorEntry>>> GetCharacterVoiceActorsAsync(long id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns collections of links to pictures related to character with given MAL id.
        /// </summary>
        /// <param name="id">MAL id of character.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Collections of links to pictures related to character with given MAL id.</returns>
        Task<BaseTenraiResponse<ICollection<ImagesSet>>> GetCharacterPicturesAsync(long id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns character with additional data.
        /// </summary>
        /// <param name="id">MAL id of character.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Character with additional data.</returns>
        Task<BaseTenraiResponse<CharacterFull>> GetCharacterFullDataAsync(long id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns the complete collection of character MAL IDs available in the catalogue.
        /// </summary>
        /// <remarks>Requires a Tenrai Server Key (set <see cref="Config.TenraiClientConfiguration.ServerKey"/>); otherwise the API responds with 401.</remarks>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Collection of character MAL IDs.</returns>
        Task<BaseTenraiResponse<ICollection<long>>> GetCharacterIdsAsync(CancellationToken cancellationToken = default);

        #endregion Character requests

        #region Person requests

        /// <summary>
        /// Returns person with given MAL id.
        /// </summary>
        /// <param name="id">MAL id of person.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Person with given MAL id.</returns>
        Task<BaseTenraiResponse<Person>> GetPersonAsync(long id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns animeography of person with given MAL id.
        /// </summary>
        /// <param name="id">MAL id of person.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Collection of anime the person collaborated on.</returns>
        Task<BaseTenraiResponse<ICollection<PersonAnimeographyEntry>>> GetPersonAnimeAsync(long id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns mangaography of person with given MAL id.
        /// </summary>
        /// <param name="id">MAL id of person.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Collection of manga the person worked on.</returns>
        Task<BaseTenraiResponse<ICollection<PersonMangaographyEntry>>> GetPersonMangaAsync(long id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns voice acting roles of person with given MAL id.
        /// </summary>
        /// <param name="id">MAL id of person.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Collection of voice acting roles of the person.</returns>
        Task<BaseTenraiResponse<ICollection<VoiceActingRole>>> GetPersonVoiceActingRolesAsync(long id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns collections of links to pictures related to person with given MAL id.
        /// </summary>
        /// <param name="id">MAL id of person.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Collections of links to pictures related to person with given MAL id.</returns>
        Task<BaseTenraiResponse<ICollection<ImagesSet>>> GetPersonPicturesAsync(long id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns person with additional data.
        /// </summary>
        /// <param name="id">MAL id of person.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Person with additional data.</returns>
        Task<BaseTenraiResponse<PersonFull>> GetPersonFullDataAsync(long id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns the complete collection of person MAL IDs available in the catalogue.
        /// </summary>
        /// <remarks>Requires a Tenrai Server Key (set <see cref="Config.TenraiClientConfiguration.ServerKey"/>); otherwise the API responds with 401.</remarks>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Collection of person MAL IDs.</returns>
        Task<BaseTenraiResponse<ICollection<long>>> GetPersonIdsAsync(CancellationToken cancellationToken = default);

        #endregion Person requests

        #region Season requests

        /// <summary>
        /// Returns season preview.
        /// </summary>
        /// <param name="year">Year of selected season.</param>
        /// <param name="season">Selected season.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Season preview.</returns>
        Task<PaginatedTenraiResponse<ICollection<Anime>>> GetSeasonAsync(int year, Season season, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns season preview.
        /// </summary>
        /// <param name="year">Year of selected season.</param>
        /// <param name="season">Selected season.</param>
        /// <param name="page">Index of page folding 25 records of top ranging (e.g. 1 will return first 25 records, 2 will return record from 26 to 50 etc.)</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Season preview.</returns>
        Task<PaginatedTenraiResponse<ICollection<Anime>>> GetSeasonAsync(int year, Season season, int page, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns list of available season to query with <see cref="GetSeasonAsync(int, Season, CancellationToken)"/>
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns></returns>
        Task<PaginatedTenraiResponse<ICollection<SeasonArchive>>> GetSeasonArchiveAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Return season preview for anime in current airing season.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Season preview for anime in current airing season.</returns>
        Task<PaginatedTenraiResponse<ICollection<Anime>>> GetCurrentSeasonAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Return season preview for anime in current airing season.
        /// </summary>
        /// <param name="page">Index of page folding 25 records of top ranging (e.g. 1 will return first 25 records, 2 will return record from 26 to 50 etc.)</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Season preview for anime in current airing season.</returns>
        Task<PaginatedTenraiResponse<ICollection<Anime>>> GetCurrentSeasonAsync(int page, CancellationToken cancellationToken = default);

        /// <summary>
        /// Return season preview for anime with undefined airing season (marked as "Later" on MAL).
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Season preview for anime with undefined airing date.</returns>
        Task<PaginatedTenraiResponse<ICollection<Anime>>> GetUpcomingSeasonAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Return season preview for anime with undefined airing season (marked as "Later" on MAL).
        /// </summary>
        /// <param name="page">Index of page folding 25 records of top ranging (e.g. 1 will return first 25 records, 2 will return record from 26 to 50 etc.)</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Season preview for anime with undefined airing date.</returns>
        Task<PaginatedTenraiResponse<ICollection<Anime>>> GetUpcomingSeasonAsync(int page, CancellationToken cancellationToken = default);

        #endregion Season requests

        #region Schedule requests

        /// <summary>
        /// Returns current season schedule.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Current season schedule.</returns>
        Task<PaginatedTenraiResponse<ICollection<Anime>>> GetScheduleAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns current season schedule.
        /// </summary>
        /// <param name="page">Index of page folding 25 records of top ranging (e.g. 1 will return first 25 records, 2 will return record from 26 to 50 etc.)</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Current season schedule.</returns>
        Task<PaginatedTenraiResponse<ICollection<Anime>>> GetScheduleAsync(int page, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns current season schedule.
        /// </summary>
        /// <param name="scheduledDay">Scheduled day to filter by.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Current season schedule.</returns>
        Task<PaginatedTenraiResponse<ICollection<Anime>>> GetScheduleAsync(ScheduledDay scheduledDay, CancellationToken cancellationToken = default);

        #endregion Schedule requests

        #region Top requests

        /// <summary>
        /// Returns list of top anime.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>List of top anime.</returns>
        Task<PaginatedTenraiResponse<ICollection<Anime>>> GetTopAnimeAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns list of top anime.
        /// </summary>
        /// <param name="page">Index of page folding 25 records of top ranging (e.g. 1 will return first 25 records, 2 will return record from 26 to 50 etc.)</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>List of top anime.</returns>
        Task<PaginatedTenraiResponse<ICollection<Anime>>> GetTopAnimeAsync(int page, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns list of top anime.
        /// </summary>
        /// <param name="filter">Filter determining result of request (e.g. TopAnimeFilter.Airing will return top airing anime.)</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>List of top anime.</returns>
        Task<PaginatedTenraiResponse<ICollection<Anime>>> GetTopAnimeAsync(TopAnimeFilter filter, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns list of top anime.
        /// </summary>
        /// <param name="filter">Filter determining result of request (e.g. TopAnimeFilter.Airing will return top airing anime.)</param>
        /// <param name="page">Index of page folding 25 records of top ranging (e.g. 1 will return first 25 records, 2 will return record from 26 to 50 etc.)</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>List of top anime.</returns>
        Task<PaginatedTenraiResponse<ICollection<Anime>>> GetTopAnimeAsync(TopAnimeFilter filter, int page, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// Returns list of top anime.
        /// </summary>
        /// <param name="searchConfig">Class containing properties representing query filters</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>List of top anime.</returns>
        Task<PaginatedTenraiResponse<ICollection<Anime>>> GetTopAnimeAsync(AnimeTopSearchConfig searchConfig, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns list of top manga.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>List of top manga.</returns>
        Task<PaginatedTenraiResponse<ICollection<Manga>>> GetTopMangaAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns list of top manga.
        /// </summary>
        /// <param name="page">Index of page folding 25 records of top ranging (e.g. 1 will return first 25 records, 2 will return record from 26 to 50 etc.)</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>List of top manga.</returns>
        Task<PaginatedTenraiResponse<ICollection<Manga>>> GetTopMangaAsync(int page, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns list of top manga.
        /// </summary>
        /// <param name="searchConfig">Class containing properties representing query filters</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>List of top manga.</returns>
        Task<PaginatedTenraiResponse<ICollection<Manga>>> GetTopMangaAsync(MangaTopSearchConfig searchConfig, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns list of most popular people.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>List of most popular people.</returns>
        Task<PaginatedTenraiResponse<ICollection<Person>>> GetTopPeopleAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns list of most popular people.
        /// </summary>
        /// <param name="page">Index of page folding 25 records of top ranging (e.g. 1 will return first 25 records, 2 will return record from 26 to 50 etc.)</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>List of most popular people.</returns>
        Task<PaginatedTenraiResponse<ICollection<Person>>> GetTopPeopleAsync(int page, CancellationToken cancellationToken = default);

		/// <summary>
		/// Returns list of most popular characters.
		/// </summary>
		/// <returns>List of most popular characters.</returns>
		Task<PaginatedTenraiResponse<ICollection<Character>>> GetTopCharactersAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns list of most popular characters.
        /// </summary>
        /// <param name="page">Index of page folding 25 records of top ranging (e.g. 1 will return first 25 records, 2 will return record from 26 to 50 etc.)</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>List of most popular characters.</returns>
        Task<PaginatedTenraiResponse<ICollection<Character>>> GetTopCharactersAsync(int page, CancellationToken cancellationToken = default);

		/// <summary>
		/// Returns list of most popular reviews.
		/// </summary>
		/// <returns>List of most popular reviews.</returns>
		Task<PaginatedTenraiResponse<ICollection<Review>>> GetTopReviewsAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns list of most popular reviews, filtered using the supplied configuration.
        /// </summary>
        /// <param name="searchConfig">Query configuration (page, limit, target type, preliminary/spoiler filters).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>List of most popular reviews.</returns>
        Task<PaginatedTenraiResponse<ICollection<Review>>> GetTopReviewsAsync(ReviewsSearchConfig searchConfig, CancellationToken cancellationToken = default);

        #endregion Top requests

        #region Genre requests

        /// <summary>
        /// Returns list of anime genres.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>List of anime genres</returns>
        Task<BaseTenraiResponse<ICollection<Genre>>> GetAnimeGenresAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns list of anime genres.
        /// </summary>
        /// <param name="filter">Filter for genre types.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>List of anime genres</returns>
        Task<BaseTenraiResponse<ICollection<Genre>>> GetAnimeGenresAsync(GenresFilter filter, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns list of manga genres.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>List of manga genres</returns>
        Task<BaseTenraiResponse<ICollection<Genre>>> GetMangaGenresAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns list of manga genres.
        /// </summary>
        /// <param name="filter">Filter for genre types.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>List of manga genres</returns>
        Task<BaseTenraiResponse<ICollection<Genre>>> GetMangaGenresAsync(GenresFilter filter, CancellationToken cancellationToken = default);

        #endregion Genre requests

        #region Producer requests

        /// <summary>
        /// Returns information about producers.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Basic Information about producers.</returns>
        Task<PaginatedTenraiResponse<ICollection<Producer>>> GetProducersAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns information about producers.
        /// </summary>
        /// <param name="page">Index of page folding 100 records of top ranging (e.g. 1 will return first 100 records, 2 will return record from 101 to 200 etc.)</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Basic Information about producers.</returns>
        Task<PaginatedTenraiResponse<ICollection<Producer>>> GetProducersAsync(int page, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns information about producer.
        /// </summary>
        /// <param name="id">MAL id of the producer.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Basic Information about producer.</returns>
        Task<BaseTenraiResponse<Producer>> GetProducerAsync(long id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns external links related to producer
        /// </summary>
        /// <param name="id">MAL id of the producer.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>External links related to producer</returns>
        Task<BaseTenraiResponse<ICollection<ExternalLink>>> GetProducerExternalLinksAsync(long id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns full information about producer.
        /// </summary>
        /// <param name="id">MAL id of the producer.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Full Information about producer.</returns>
        Task<BaseTenraiResponse<ProducerFull>> GetProducerFullDataAsync(long id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns the complete collection of producer MAL IDs available in the catalogue.
        /// </summary>
        /// <remarks>Requires a Tenrai Server Key (set <see cref="Config.TenraiClientConfiguration.ServerKey"/>); otherwise the API responds with 401.</remarks>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Collection of producer MAL IDs.</returns>
        Task<BaseTenraiResponse<ICollection<long>>> GetProducerIdsAsync(CancellationToken cancellationToken = default);

        #endregion Producer requests

        #region Magazine requests

        /// <summary>
        /// Returns information about magazines.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Basic Information about magazines.</returns>
        Task<PaginatedTenraiResponse<ICollection<Magazine>>> GetMagazinesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns information about magazines.
        /// </summary>
        /// <param name="page">Index of page folding 100 records of top ranging (e.g. 1 will return first 100 records, 2 will return record from 101 to 200 etc.)</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Basic Information about magazines.</returns>
        Task<PaginatedTenraiResponse<ICollection<Magazine>>> GetMagazinesAsync(int page, CancellationToken cancellationToken = default);

        #endregion Magazine requests

        #region Club requests

        /// <summary>
        /// Return club's profile information.
        /// </summary>
        /// <param name="id">MAL id of the club.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Club's profile information.</returns>
        [Obsolete(ErrorMessagesConst.UnsupportedClub)]
        Task<BaseTenraiResponse<Club>> GetClubAsync(long id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Return club's member list.
        /// </summary>
        /// <param name="id">MAL id of the club.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Club's member list.</returns>
        [Obsolete(ErrorMessagesConst.UnsupportedClub)]
        Task<PaginatedTenraiResponse<ICollection<ClubMember>>> GetClubMembersAsync(long id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Return club's member list.
        /// </summary>
        /// <param name="id">MAL id of the club.</param>
        /// <param name="page">Index of page folding 36 records of top ranging (e.g. 1 will return first 36 records, 2 will return record from 37 to 72 etc.)</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Club's member list.</returns>
        [Obsolete(ErrorMessagesConst.UnsupportedClub)]
        Task<PaginatedTenraiResponse<ICollection<ClubMember>>> GetClubMembersAsync(long id, int page, CancellationToken cancellationToken = default);

        /// <summary>
        /// Return club's staff list.
        /// </summary>
        /// <param name="id">MAL id of the club.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Club's staff list.</returns>
        [Obsolete(ErrorMessagesConst.UnsupportedClub)]
        Task<BaseTenraiResponse<ICollection<ClubStaff>>> GetClubStaffAsync(long id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Return club's related entities.
        /// </summary>
        /// <param name="id">MAL id of the club.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Club's related entities collections..</returns>
        [Obsolete(ErrorMessagesConst.UnsupportedClub)]
        Task<BaseTenraiResponse<ClubRelations>> GetClubRelationsAsync(long id, CancellationToken cancellationToken = default);

        #endregion Club requests

        #region User requests

        /// <summary>
        /// Returns information about user's profile with given username.
        /// </summary>
        /// <param name="username">Username.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Information about user's profile with given username.</returns>
        [Obsolete(ErrorMessagesConst.UnsupportedUserData)]
        Task<BaseTenraiResponse<UserProfile>> GetUserProfileAsync(string username, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns user profile by numeric id.
        /// </summary>
        /// <param name="id">Numeric MAL user id.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>User profile by id.</returns>
        [Obsolete(ErrorMessagesConst.UnsupportedUserData)]
        Task<BaseTenraiResponse<UserProfile>> GetUserByIdAsync(long id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns information about user's anime and manga statistics
        /// </summary>
        /// <param name="username">Username.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Information about user's anime and manga statistics.</returns>
        [Obsolete(ErrorMessagesConst.UnsupportedUserData)]
        Task<BaseTenraiResponse<UserStatistics>> GetUserStatisticsAsync(string username, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns information about user's favorite section.
        /// </summary>
        /// <param name="username">Username.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Information about user's favorite section..</returns>
        [Obsolete(ErrorMessagesConst.UnsupportedUserData)]
        Task<BaseTenraiResponse<UserFavorites>> GetUserFavoritesAsync(string username, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns information about user's updates on anime/manga progress.
        /// </summary>
        /// <param name="username">Username.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Information about user's updates on anime/manga progress.</returns>
        [Obsolete(ErrorMessagesConst.UnsupportedUserData)]
        Task<BaseTenraiResponse<UserUpdates>> GetUserUpdatesAsync(string username, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns information about user's description on the profile.
        /// </summary>
        /// <param name="username">Username.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Information about user's description on the profile.</returns>
        [Obsolete(ErrorMessagesConst.UnsupportedUserData)]
        Task<BaseTenraiResponse<UserAbout>> GetUserAboutAsync(string username, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns information about user's history with given username.
        /// </summary>
        /// <param name="username">Username.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Information about user's profile with given username.</returns>
        [Obsolete(ErrorMessagesConst.UnsupportedUserData)]
        Task<BaseTenraiResponse<ICollection<HistoryEntry>>> GetUserHistoryAsync(string username, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns information about user's history with given username.
        /// </summary>
        /// <param name="username">Username.</param>
        /// <param name="filter">Option to filter history.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Information about user's profile with given username.</returns>
        [Obsolete(ErrorMessagesConst.UnsupportedUserData)]
        Task<BaseTenraiResponse<ICollection<HistoryEntry>>> GetUserHistoryAsync(string username, UserHistoryExtension filter, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns information about user's friends with given username.
        /// </summary>
        /// <param name="username">Username.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Information about user's friends with given username.</returns>
        [Obsolete(ErrorMessagesConst.UnsupportedUserData)]
        Task<PaginatedTenraiResponse<ICollection<Friend>>> GetUserFriendsAsync(string username, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns information about user's friends with given username.
        /// </summary>
        /// <param name="username">Username.</param>
        /// <param name="page">Index of the page.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Information about user's friends with given username.</returns>
        [Obsolete(ErrorMessagesConst.UnsupportedUserData)]
        Task<PaginatedTenraiResponse<ICollection<Friend>>> GetUserFriendsAsync(string username, int page, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns user's reviews.
        /// </summary>
        /// <param name="username">Username.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>User's reviews.</returns>
        [Obsolete(ErrorMessagesConst.UnsupportedUserData)]
        Task<PaginatedTenraiResponse<ICollection<Review>>> GetUserReviewsAsync(string username, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns user's reviews.
        /// </summary>
        /// <param name="username">Username.</param>
        /// <param name="page">Index of page folding 10 records of top ranging (e.g. 1 will return first 10 records, 2 will return record from 11 to 21 etc.)</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>User's reviews.</returns>
        [Obsolete(ErrorMessagesConst.UnsupportedUserData)]
        Task<PaginatedTenraiResponse<ICollection<Review>>> GetUserReviewsAsync(string username, int page, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns user's recommendations.
        /// </summary>
        /// <param name="username">Username.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>User's recommendations.</returns>
        [Obsolete(ErrorMessagesConst.UnsupportedUserData)]
        Task<PaginatedTenraiResponse<ICollection<UserRecommendation>>> GetUserRecommendationsAsync(string username, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns user's recommendations.
        /// </summary>
        /// <param name="username">Username.</param>
        /// <param name="page">Index of page folding 10 records of top ranging (e.g. 1 will return first 10 records, 2 will return record from 11 to 21 etc.)</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>User's recommendations.</returns>
        [Obsolete(ErrorMessagesConst.UnsupportedUserData)]
        Task<PaginatedTenraiResponse<ICollection<UserRecommendation>>> GetUserRecommendationsAsync(string username, int page, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns user's clubs.
        /// </summary>
        /// <param name="username">Username.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>User's clubs.</returns>
        [Obsolete(ErrorMessagesConst.UnsupportedUserData)]
        Task<PaginatedTenraiResponse<ICollection<MalUrl>>> GetUserClubsAsync(string username, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns user's clubs.
        /// </summary>
        /// <param name="username">Username.</param>
        /// <param name="page">Index of page folding 10 records of top ranging (e.g. 1 will return first 10 records, 2 will return record from 11 to 21 etc.)</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>User's clubs.</returns>
        [Obsolete(ErrorMessagesConst.UnsupportedUserData)]
        Task<PaginatedTenraiResponse<ICollection<MalUrl>>> GetUserClubsAsync(string username, int page, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns collection of external services links related to user.
        /// </summary>
        /// <param name="username">Username.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Collection of external services links related to anime.</returns>
        [Obsolete(ErrorMessagesConst.UnsupportedUserData)]
        Task<BaseTenraiResponse<ICollection<ExternalLink>>> GetUserExternalLinksAsync(string username, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns user with additional data.
        /// </summary>
        /// <param name="username">Username.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>User profile with additional data.</returns>
        [Obsolete(ErrorMessagesConst.UnsupportedUserData)]
        Task<BaseTenraiResponse<UserFull>> GetUserFullDataAsync(string username, CancellationToken cancellationToken = default);

        #endregion User requests

        #region GetRandom requests

        /// <summary>
        /// Gets random anime.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Random anime</returns>
        Task<BaseTenraiResponse<Anime>> GetRandomAnimeAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets random manga.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Random manga</returns>
        Task<BaseTenraiResponse<Manga>> GetRandomMangaAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets random character.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Random character</returns>
        Task<BaseTenraiResponse<Character>> GetRandomCharacterAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets random person.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Random person</returns>
        Task<BaseTenraiResponse<Person>> GetRandomPersonAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets random user.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Random character</returns>
        [Obsolete(ErrorMessagesConst.UnsupportedUserData)]
        Task<BaseTenraiResponse<UserProfile>> GetRandomUserAsync(CancellationToken cancellationToken = default);

        #endregion

        #region Recommendations requests

        /// <summary>
        /// Gets collection of recently added anime recommendations.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Collection of recently added recommendations.r</returns>
        Task<PaginatedTenraiResponse<ICollection<UserRecommendation>>> GetRecentAnimeRecommendationsAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets collection of recently added anime recommendations.
        /// </summary>
        /// <param name="page">Index of page folding 100 records of top ranging (e.g. 1 will return first 100 records, 2 will return record from 101 to 200 etc.)</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Collection of recently added recommendations.r</returns>
        Task<PaginatedTenraiResponse<ICollection<UserRecommendation>>> GetRecentAnimeRecommendationsAsync(int page, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets collection of recently added manga recommendations.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Collection of recently added recommendations.r</returns>
        Task<PaginatedTenraiResponse<ICollection<UserRecommendation>>> GetRecentMangaRecommendationsAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets collection of recently added manga recommendations.
        /// </summary>
        /// <param name="page">Index of page folding 100 records of top ranging (e.g. 1 will return first 100 records, 2 will return record from 101 to 200 etc.)</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Collection of recently added recommendations.r</returns>
        Task<PaginatedTenraiResponse<ICollection<UserRecommendation>>> GetRecentMangaRecommendationsAsync(int page, CancellationToken cancellationToken = default);

        #endregion

        #region Reviews requests

        /// <summary>
        /// Gets collection of recently added anime reviews.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Collection of recently added anime reviews</returns>
        Task<PaginatedTenraiResponse<ICollection<Review>>> GetRecentAnimeReviewsAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets collection of recently added anime reviews, filtered and sorted using the supplied configuration.
        /// </summary>
        /// <param name="searchConfig">Query configuration (page, limit, sort order, preliminary/spoiler filters, sentiment).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Collection of recently added anime reviews</returns>
        Task<PaginatedTenraiResponse<ICollection<Review>>> GetRecentAnimeReviewsAsync(ReviewsSearchConfig searchConfig, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets collection of recently added manga reviews.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Collection of recently added manga reviews</returns>
        Task<PaginatedTenraiResponse<ICollection<Review>>> GetRecentMangaReviewsAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets collection of recently added manga reviews, filtered and sorted using the supplied configuration.
        /// </summary>
        /// <param name="searchConfig">Query configuration (page, limit, sort order, preliminary/spoiler filters, sentiment).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Collection of recently added manga reviews.</returns>
        Task<PaginatedTenraiResponse<ICollection<Review>>> GetRecentMangaReviewsAsync(ReviewsSearchConfig searchConfig, CancellationToken cancellationToken = default);

        #endregion

        #region Watch requests

        /// <summary>
        /// Return collection of recently released episodes details.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Collection of recently released episodes details..</returns>
        [Obsolete(ErrorMessagesConst.UnsupportedWatch)]
        Task<PaginatedTenraiResponse<ICollection<WatchEpisode>>> GetWatchRecentEpisodesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Return collection of popular episodes details.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Collection of popular episodes details.</returns>
        [Obsolete(ErrorMessagesConst.UnsupportedWatch)]
        Task<PaginatedTenraiResponse<ICollection<WatchEpisode>>> GetWatchPopularEpisodesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Return collection of recently released promos details.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Collection of recently released promos details.</returns>
        [Obsolete(ErrorMessagesConst.UnsupportedWatch)]
        Task<PaginatedTenraiResponse<ICollection<WatchPromoVideo>>> GetWatchRecentPromosAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Return collection of recently released promos details.
        /// </summary>
        /// <param name="page">Index of page folding 100 records of top ranging (e.g. 1 will return first 100 records, 2 will return record from 101 to 200 etc.)</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Collection of recently released promos details.</returns>
        [Obsolete(ErrorMessagesConst.UnsupportedWatch)]
        Task<PaginatedTenraiResponse<ICollection<WatchPromoVideo>>> GetWatchRecentPromosAsync(int page, CancellationToken cancellationToken = default);

        /// <summary>
        /// Return collection of popular promos details.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Collection of popular promos details.</returns>
        [Obsolete(ErrorMessagesConst.UnsupportedWatch)]
        Task<PaginatedTenraiResponse<ICollection<WatchPromoVideo>>> GetWatchPopularPromosAsync(CancellationToken cancellationToken = default);

        #endregion Watch requests

        #region Search requests

        /// <summary>
        /// Returns list of results related to search.
        /// </summary>
        /// <param name="query">Search query.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>List of result related to search query.</returns>
        Task<PaginatedTenraiResponse<ICollection<Anime>>> SearchAnimeAsync(string query, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns list of results related to search.
        /// </summary>
        /// <param name="searchConfig">Additional configuration for advanced search.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>List of result related to search query.</returns>
        Task<PaginatedTenraiResponse<ICollection<Anime>>> SearchAnimeAsync(AnimeSearchConfig searchConfig, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns list of results related to search.
        /// </summary>
        /// <param name="query">Search query.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>List of result related to search query.</returns>
        Task<PaginatedTenraiResponse<ICollection<Manga>>> SearchMangaAsync(string query, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns list of results related to search.
        /// </summary>
        /// <param name="searchConfig">Additional configuration for advanced search.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>List of result related to search query.</returns>
        Task<PaginatedTenraiResponse<ICollection<Manga>>> SearchMangaAsync(MangaSearchConfig searchConfig, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns list of results related to search.
        /// </summary>
        /// <param name="query">Search query.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>List of result related to search query.</returns>
        Task<PaginatedTenraiResponse<ICollection<Person>>> SearchPersonAsync(string query, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns list of results related to search.
        /// </summary>
        /// <param name="searchConfig">Additional configuration for advanced search.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>List of result related to search query.</returns>
        Task<PaginatedTenraiResponse<ICollection<Person>>> SearchPersonAsync(PersonSearchConfig searchConfig, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns list of results related to search.
        /// </summary>
        /// <param name="query">Search query.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>List of result related to search query.</returns>
        Task<PaginatedTenraiResponse<ICollection<Character>>> SearchCharacterAsync(string query, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns list of results related to search.
        /// </summary>
        /// <param name="searchConfig">Additional configuration for advanced search.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>List of result related to search query.</returns>
        Task<PaginatedTenraiResponse<ICollection<Character>>> SearchCharacterAsync(CharacterSearchConfig searchConfig, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns list of results related to search.
        /// </summary>
        /// <param name="query">Search query.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>List of result related to search query.</returns>
        [Obsolete(ErrorMessagesConst.UnsupportedUserData)]
        Task<PaginatedTenraiResponse<ICollection<UserMetadata>>> SearchUserAsync(string query, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns list of results related to search.
        /// </summary>
        /// <param name="searchConfig">Additional configuration for advanced search.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>List of result related to search query.</returns>
        [Obsolete(ErrorMessagesConst.UnsupportedUserData)]
        Task<PaginatedTenraiResponse<ICollection<UserMetadata>>> SearchUserAsync(UserSearchConfig searchConfig, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns list of results related to search.
        /// </summary>
        /// <param name="query">Search query.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>List of result related to search query.</returns>
        [Obsolete(ErrorMessagesConst.UnsupportedClub)]
        Task<PaginatedTenraiResponse<ICollection<Club>>> SearchClubAsync(string query, CancellationToken cancellationToken = default);

		/// <summary>
		/// Returns list of results related to search.
		/// </summary>
		/// <param name="searchConfig">Additional configuration for advanced search.</param>
		/// <param name="cancellationToken">Cancellation token.</param>
		/// <returns>List of result related to search query.</returns>
		[Obsolete(ErrorMessagesConst.UnsupportedClub)]
		Task<PaginatedTenraiResponse<ICollection<Club>>> SearchClubAsync(ClubSearchConfig searchConfig, CancellationToken cancellationToken = default);

		#endregion Search requests

		#region Interest stack requests

		/// <summary>
		/// Returns the first page of public interest stacks.
		/// </summary>
		/// <param name="cancellationToken">Cancellation token.</param>
		/// <returns>First page of public interest stacks.</returns>
		Task<PaginatedTenraiResponse<ICollection<InterestStack>>> GetInterestStacksAsync(CancellationToken cancellationToken = default);

		/// <summary>
		/// Returns a page of public interest stacks.
		/// </summary>
		/// <param name="page">Index of the page.</param>
		/// <param name="cancellationToken">Cancellation token.</param>
		/// <returns>Page of public interest stacks.</returns>
		Task<PaginatedTenraiResponse<ICollection<InterestStack>>> GetInterestStacksAsync(int page, CancellationToken cancellationToken = default);

		/// <summary>
		/// Returns public interest stacks matching the given search configuration.
		/// </summary>
		/// <param name="searchConfig">Additional configuration for advanced search.</param>
		/// <param name="cancellationToken">Cancellation token.</param>
		/// <returns>Public interest stacks matching the search configuration.</returns>
		Task<PaginatedTenraiResponse<ICollection<InterestStack>>> SearchInterestStacksAsync(InterestStackSearchConfig searchConfig, CancellationToken cancellationToken = default);

		/// <summary>
		/// Returns interest stack with given MAL id, including its entries in the order the author arranged them.
		/// </summary>
		/// <param name="id">MAL id of the interest stack.</param>
		/// <param name="cancellationToken">Cancellation token.</param>
		/// <returns>Interest stack with given MAL id.</returns>
		Task<BaseTenraiResponse<InterestStackDetails>> GetInterestStackAsync(long id, CancellationToken cancellationToken = default);

		/// <summary>
		/// Returns first page of public interest stacks containing the anime with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <param name="cancellationToken">Cancellation token.</param>
		/// <returns>First page of public interest stacks containing the anime.</returns>
		Task<PaginatedTenraiResponse<ICollection<InterestStack>>> GetAnimeInterestStacksAsync(long id, CancellationToken cancellationToken = default);

		/// <summary>
		/// Returns a page of public interest stacks containing the anime with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <param name="page">Index of the page.</param>
		/// <param name="cancellationToken">Cancellation token.</param>
		/// <returns>Page of public interest stacks containing the anime.</returns>
		Task<PaginatedTenraiResponse<ICollection<InterestStack>>> GetAnimeInterestStacksAsync(long id, int page, CancellationToken cancellationToken = default);

		/// <summary>
		/// Returns first page of public interest stacks containing the manga with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of manga.</param>
		/// <param name="cancellationToken">Cancellation token.</param>
		/// <returns>First page of public interest stacks containing the manga.</returns>
		Task<PaginatedTenraiResponse<ICollection<InterestStack>>> GetMangaInterestStacksAsync(long id, CancellationToken cancellationToken = default);

		/// <summary>
		/// Returns a page of public interest stacks containing the manga with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of manga.</param>
		/// <param name="page">Index of the page.</param>
		/// <param name="cancellationToken">Cancellation token.</param>
		/// <returns>Page of public interest stacks containing the manga.</returns>
		Task<PaginatedTenraiResponse<ICollection<InterestStack>>> GetMangaInterestStacksAsync(long id, int page, CancellationToken cancellationToken = default);

		#endregion Interest stack requests

		#region Status requests

		/// <summary>
		/// Returns the current public status snapshot of the Tenrai services.
		/// </summary>
		/// <remarks>
		/// This call targets the status host (<c>https://tenrai.org</c>) rather than the API host, so it ignores any
		/// custom base address and is not subject to the client's rate limiter. Poll no more than once per minute;
		/// the server caches the snapshot for 30 seconds. If a Server Key is configured it is sent on this request too,
		/// because the underlying <c>HttpClient</c> is shared.
		/// </remarks>
		/// <param name="cancellationToken">Cancellation token.</param>
		/// <returns>Current public status snapshot.</returns>
		Task<TenraiStatus> GetStatusAsync(CancellationToken cancellationToken = default);

		#endregion Status requests
	}
}
