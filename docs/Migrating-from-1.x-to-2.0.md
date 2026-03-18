# Migrating from 1.x to 2.0

Jikan v4 introduced breaking changes to the API structure. Updating from Jikan.net 1.x to 2.0 will require code changes. Below is a guide for a smooth migration.

## Initialization

| Was (v3) | Is (v4) |
|----------|---------|
| `Jikan()` | `Jikan()` |
| `Jikan("endpoint")` | `Jikan(new JikanClientConfiguration { Endpoint = "endpoint" })` |
| `Jikan(true, true)` | `Jikan(new JikanClientConfiguration { SuppressException = true })` |
| `Jikan("endpoint", true)` | `Jikan(new JikanClientConfiguration { Endpoint = "endpoint", SuppressException = true })` |

## Retrieving Data

All response models now contain a `Data` property where the actual response data resides. Paginated endpoints also include pagination metadata.

```csharp
var jikan = new Jikan();
var bebopAnime = await jikan.GetAnimeAsync(1);
Console.WriteLine(bebopAnime.Data.Title);  // "Cowboy Bebop"
```

## Method Names

All methods now use the `Async` suffix. Some endpoints were split into multiple methods:

| Was (v3) | Is (v4) |
|----------|---------|
| `GetAnime` | `GetAnimeAsync`, `GetAnimeRelationsAsync` (for related entries) |
| `GetAnimeEpisodes` | `GetAnimeEpisodesAsync` (list), `GetAnimeEpisodeAsync` (single episode) |
| `GetAnimeCharactersStaff` | `GetAnimeCharactersAsync`, `GetAnimeStaffAsync` |
| `GetAnimePictures` | `GetAnimePicturesAsync` |
| `GetAnimeNews` | `GetAnimeNewsAsync` |
| `GetAnimeVideos` | `GetAnimeVideosAsync` |
| `GetAnimeStatistics` | `GetAnimeStatisticsAsync` |
| `GetAnimeForumTopics` | `GetAnimeForumTopicsAsync` |
| `GetAnimeMoreInfo` | `GetAnimeMoreInfoAsync` |
| `GetAnimeRecommendations` | `GetAnimeRecommendationsAsync` |
| `GetAnimeReviews` | `GetAnimeReviewsAsync` |
| `GetAnimeUserUpdates` | `GetAnimeUserUpdatesAsync` |
| `GetManga` | `GetMangaAsync`, `GetMangaRelationsAsync` |
| `GetMangaPictures` | `GetMangaPicturesAsync` |
| `GetMangaCharacters` | `GetMangaCharactersAsync` |
| `GetMangaNews` | `GetMangaNewsAsync` |
| `GetMangaStatistics` | `GetMangaStatisticsAsync` |
| `GetMangaForumTopics` | `GetMangaForumTopicsAsync` |
| `GetMangaMoreInfo` | `GetMangaMoreInfoAsync` |
| `GetMangaRecommendations` | `GetMangaRecommendationsAsync` |
| `GetMangaReviews` | `GetMangaReviewsAsync` |
| `GetMangaUserUpdates` | `GetMangaUserUpdatesAsync` |
| `GetCharacter` | `GetCharacterAsync`, `GetCharacterAnimeAsync`, `GetCharacterMangaAsync`, `GetCharacterVoiceActorsAsync` |
| `GetCharacterPictures` | `GetCharacterPicturesAsync` |
| `GetPerson` | `GetPersonAsync`, `GetPersonAnimeAsync`, `GetPersonMangaAsync`, `GetPersonVoiceActingRolesAsync` |
| `GetPersonPictures` | `GetPersonPicturesAsync` |
| `GetSeason` | `GetSeasonAsync` |
| `GetSeasonArchive` | `GetSeasonArchiveAsync` |
| `GetSeasonLater` | `GetUpcomingSeasonAsync` |
| `GetSchedule` | `GetScheduleAsync` |
| `GetAnimeTop` | `GetTopAnimeAsync` |
| `GetMangaTop` | `GetTopMangaAsync` |
| `GetPeopleTop` | `GetTopPeopleAsync` |
| `GetCharactersTop` | `GetTopCharactersAsync` |
| `GetAnimeGenre` | `GetAnimeGenresAsync` |
| `GetMangaGenre` | `GetMangaGenresAsync` |
| `GetProducer` | `GetProducersAsync`, `GetProducerAsync` |
| `GetMagazine` | `GetMagazinesAsync` |
| `GetUserProfile` | `GetUserProfileAsync`, `GetUserStatisticsAsync`, `GetUserFavoritesAsync`, `GetUserAboutAsync` |
| `GetUserHistory` | `GetUserHistoryAsync` |
| `GetUserFriends` | `GetUserFriendsAsync` |
| `GetClub` | `GetClubAsync`, `GetClubStaffAsync`, `GetClubRelationsAsync` |
| `GetClubMembers` | `GetClubMembersAsync` |
| `SearchAnime` | `SearchAnimeAsync` |
| `SearchManga` | `SearchMangaAsync` |
| `SearchPerson` | `SearchPersonAsync` |
| `SearchCharacter` | `SearchCharacterAsync` |

## Removed Methods

| Method | Comment |
|--------|---------|
| `GetStatusMetadata` | Discontinued in Jikan API |
| `GetUserAnimeList` / `GetUserAnimeListAsync` | Removed in 2.9.0 (no longer supported by Jikan API) |
| `GetUserMangaList` / `GetUserMangaListAsync` | Removed in 2.9.0 (no longer supported by Jikan API) |

## New Methods (2.x)

| Method | Comment |
|--------|---------|
| `GetAnimeThemesAsync` | Anime openings and endings |
| `GetAnimeExternalLinksAsync` | External services links |
| `GetMangaExternalLinksAsync` | External services links |
| `GetTopReviewsAsync` | Most popular reviews |
| `GetUserReviewsAsync` | User's reviews |
| `GetUserRecommendationsAsync` | User's recommendations |
| `GetUserClubsAsync` | User's clubs |
| `GetRandomAnimeAsync` | Random anime |
| `GetRandomMangaAsync` | Random manga |
| `GetRandomCharacterAsync` | Random character |
| `GetRandomPersonAsync` | Random person |
| `GetRandomUserAsync` | Random user |
| `GetRecentAnimeRecommendationsAsync` | Recently added anime recommendations |
| `GetRecentMangaRecommendationsAsync` | Recently added manga recommendations |
| `GetRecentAnimeReviewsAsync` | Recently added anime reviews |
| `GetRecentMangaReviewsAsync` | Recently added manga reviews |
| `GetWatchRecentEpisodesAsync` | Recently released episodes |
| `GetWatchPopularEpisodesAsync` | Popular episodes |
| `GetWatchRecentPromosAsync` | Recently released promos |
| `GetWatchPopularPromosAsync` | Popular promos |
| `SearchUserAsync` | Search users |
| `SearchClubAsync` | Search clubs |
| `GetUserByIdAsync` | User profile by numeric id |
| `GetAnimeStreamingLinksAsync` | Streaming services links |
| `GetAnimeFullDataAsync`, `GetMangaFullDataAsync`, `GetCharacterFullDataAsync`, `GetPersonFullDataAsync`, `GetUserFullDataAsync` | Full data endpoints |
