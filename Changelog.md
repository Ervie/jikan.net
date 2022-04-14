## 14.04.2022 - Version 2.1.0

- Feature: additional paging information returned from entries queried from jikan database.

Example:
```
{
  "pagination": {
    "last_visible_page": 4, // already exists
    "has_next_page": true, // already exists
    
    // ...

    "current_page": 4, // NEW: as the name suggests, the current page we're on
    "items": { // NEW
      "count": 7, // results on the current page
      "total": 80, // total results
      "per_page": 25 // total results per page (can be changed with `limit` query)
    }
  },
  // data
```
This additional information is available in the following endpoints:
* Schedules
* Seasons
* Search Anime/Manga/People/Characters
* Top Anime/Manga/People/Characters

## 28.03.2022 - Version 2.0.1

- Bug fixes
    - <b>[Character]</b> Add missing `NameKanji` property
    - <b>[AnimeSearchConfig/MangaSearchConfig]</b> Adjust `Genre`, `MagazineIds` and `ProducerIds` according to recent changes

## 14.02.2022 - Version 2.0.0

- Compatible with Jikan REST API v4.0
    - Important - current documentation is for versions 1.x of library and is largely obsolete (although most of the endpoints overlaps). In the future, it will be either updated or moved to external documentation link. For now, please use the [following guide](https://github.com/Ervie/jikan.net/wiki/Migrating-from-1.x-to-2.0).

## 16.09.2021 - Version 1.6.0

- Rework of Genres enumeration
    - `GenreSearch` enum is now change to two separate enums: `AnimeGenreSearch` and `MangaGenreSearch`.
    - <b>[GetAnimeGenre]</b> Now accepts `AnimeGenreSearch` as a parameter.
    - <b>[GetMangaGenre]</b> Now accepts `MangaGenreSearch` as a parameter.
    - <b>[AnimeSearch]</b> `AnimeSearchConfig` now has property `Genres` of `AnimeGenreSearch` type (was `GenreSearch` before).
    - <b>[MangaSearch]</b> `MangaSearchConfig` now has property `Genres` of `MangaGenreSearch` type (was `GenreSearch` before).

## 23.06.2021 - Version 1.5.6

- Features
    - <b>[Validation]</b> Validation of input parameters of Enum types parameters most method are added for fail fast approach.
- Fixes
    - <b>[SearchManga]</b> Remove redundant page part during url building.

## 01.04.2021 - Version 1.5.5

- Fixes
    - <b>[Animelist|Mangalist]</b> Fix building url for scenario with filter.

## 14.12.2020 - Version 1.5.4

- Fixes
    - <b>[Seasons]</b> Extends available range for acceptable input year to <1000, 9999>.

## 27.11.2020 - Version 1.5.3

- Jikan.net now can be used with own instance of `HttpClient` targetting Jikan REST API. Read more [here](https://github.com/Ervie/jikan.net/wiki/Using-own-instance-of-Jikan).

## 26.10.2020 - Version 1.5.2
- Features
    - <b>[Validation]</b> Validation of input parameters for most method are added for fail fast approach. General rules
        - Methods using mal Id (e.g. `GetAnime()`) accept only number larger than 0.
        - Search query for searcg methods (e.g. `SearchAnime()`) must not be null or whitespace and at least 3 characters long.
        - Methods with page number as a parameter (e.g. `SearchAnime()`) only accepts page when it's larger than 0.
        - Search configs for search methods (e.g. `SearchAnime()`) must not `null`.
        
## 22.09.2020 - Version 1.5.1
- Fixes 
    - <b>[Character]</b> Fix incorrect mapping for `About` property, which could led to null result.

## 10.09.2020 - Version 1.5.0

- Targetting multiple frameworks in order to decrease number of dependancies
    - System.Text.Json also is smaller library (30 KB vs Newtonsoft.Json's ~640 KB).
- Change from Newtonsoft.Json to System.Text.Json (bundled in basic library as for .NET Core 3.0)
    - No dependencies for .Net Core 3.0 and 3.1.
    - Single nuget dependancy (System.Text.Json) for .NET Standard 2.0 and compatible frameworks (.Net Core 2.0/2.1, .NET Framework 4.6.1 and newer).
- Fixes 
    - <b>[Manga/Anime]</b> `RelatedAnime`/`RelatedManga` had some incorrect mappings, which could led to null collections.
    - Minor code cleanups.
- Changes
    - <b>[Anime]</b> `Episodes` are now `int?` instead of `string`.
    - <b>[Manga]</b> `Chapters` are now `int?` instead of `string`.
    - <b>[Manga]</b> `Volumes` are now `int?` instead of `string`.
    - <b>[AnimeSubEntry]</b> `Episodes` are now `int?` instead of `string`.
    - <b>[MangasubEntry]</b> `Volumes` are now `int?` instead of `string`.

## 08.07.2020 - Version 1.4.2

- Fixes 
    - <b>[MangaSearch/AnimeSearch]</b> Fix incorrect url building for different `GenreInclusion` values in `AnimeSearchConfig` and `MangaSearchConfig`.
        
## 13.06.2020 - Version 1.4.1

- Fixes 
    - <b>[MangaSearch]</b> `Chapters` property of `MangaSearchEntry` class changes to nullable (`int?`) due to occasional null returned.
    - <b>Search</b> Improved encoding queries with spaces (changed from underscore to plus sign).

## 31.05.2020 - Version 1.4.0

- Feature
    - suppressException is now turned off by default. This should give usert better insight on any exception occuring during call to Jikan API providing failed call code and/or exception message. User of wrapper can change it by passing `true` as a second parameter in the constructor - failed request will return `null`s as before.
- Fixed feature
    - <b>[AnimeSearch]</b> Add `SearchAnime(string query, int page AnimeSearchConfig searchConfig)` method removed in version 1.3.3 after MAL fix it on their end.
    - <b>[MangaSearch]</b> Add `SearchManga(string query, int page MangaSearchConfig searchConfig)` method method removed in version 1.3.3 after MAL fix it on their end.

## 10.04.2020 - Version 1.3.3

- Fixes
    - <b>[Search]</b> Fix pagination.
- Fixes Features
    - <b>[AnimeSearch]</b> Remove `SearchAnime(string query, int page AnimeSearchConfig searchConfig)` method - MAL does not support pagination for search phrase with advanced filter.
    - <b>[AnimeSearch]</b> Add `SearchAnime(AnimeSearchConfig searchConfig)` and `SearchAnime(AnimeSearchConfig searchConfig, int page)` - it does support pagination for advanced filters without search phrase.
    - <b>[MangaSearch]</b> Remove `SearchManga(string query, int page MangaSearchConfig searchConfig)` method - MAL does not support pagination for search phrase with advanced filter.
    - <b>[MangaSearch]</b> Add `SearchManga(MangaSearchConfig searchConfig)` and `SearchManga(MangaSearchConfig searchConfig, int page)` - it does support pagination for advanced filters without search phrase.

## 20.11.2019 - Version 1.3.2

- Integration with Jikan API v3.4.
- Features
    - <b>[User]</b> `UserId` added to `UserProfile`

## 29.09.2019 - Version 1.3.1

- Fixes
    - <b>[General]</b> Removed null check during creation http client in order to avoid multiple `Jikan` objects holding same url.
    - <b>[Search]</b> Fixed incorrect parameters listing in search queries, which leaded to returning null.
    
## 31.07.2019 - Version 1.3.0

- Integration with Jikan API v3.3.
- Features
    - <b>[Search]</b> Improved searching for manga and anime
        - Order by data (Title, score, etc.)
        - Filter Producer (anime) or Magazine (manga)
        - Improved multiple genre query.
    - <b>[UserList]</b> Advanced User Lists (Anime/Manga) queries
        - Usable by passing `UserListAnimeSearchConfig` to `GetUserAnimeList` and `UserListMangaSearchConfig`to `GetUserMangaList` methods
            - Order by data: `OrderBy`, `OrderBy2` (Title, score, etc.)
            - Sort by ascending/descending - `SortBy`
            - Search user list: `Query` property
            - New Anime filters: `Producer`, `Season`, `Year`, `AiringStatus`
            - New Manga filters: `Magazine`, `PublishingStatus`
            - Paging support: `Page` property
- Fixes
    - <b>[AnimeEpisoded]</b> changed `Aired` property from `TimeSpan` (a pair of `DateTime`) to single `DateTime`

## 07.04.2019 - Version 1.2.5

- Jikan.net now can be used with own instance of Jikan REST API. Read more [here](https://github.com/Ervie/jikan.net/wiki/Using-own-instance-of-Jikan).
- New fields
    - RelatedAnime
        - `RelatedAnime` now has `AlternativeVersions`, `ParentStories` and `FullStories` fields.
    - RelatedManga
        - `RelatedManga` now has `AlternativeVersions`, `ParentStories` and `FullStories` fields.

## 17.03.2019 - Version 1.2.4

- Fixes
    - <b>[Recommendation]</b> Added missing `Title` field.
    - <b>[AiringStatus]</b> Fixed PlanToWatch/PlanToRead values (in 1.2.4).
- New fields
    - AnimeList
        - `AnimeListEntry` now has `AiringStatus` and `PublishingStatus` fields.
    - MangaList
        - `MangaListEntry` now has `ReadingStatus` and `WatchingStatus` fields.

## 06.01.2019 - Version 1.2.2

- `Jikan` class has parameterless contructor now, which makes requests over HTTPS by default.
- New class `BaseJikanRequest` with cache related properties is now inherited by all main classes returned from wrapper methods (search request only in version 1.2.2).

## 01.01.2019 - Version 1.2.0 

- Integration with Jikan API v3.2.
- New endpoints
    - Anime
        - Reviews
        - Recommendations
        - User Updates
    - Manga
        - Reviews
        - Recommendations
        - User Updates
    - Season schedule with undefined date, marked as "Later" on MAL.
- Fixes
    - <b>[Anime]</b> Removed obsolete `EpisodeNumber` from `AnimeEpisode` class.
    - <b>[Anime]/[Manga]</b> `Related` field is deserialized properly when empty (fix in REST API).
- Other
    - All data from user related endpoints are now cached for 5 minutes only.
    - MAL entities with their own MAL Id now share `IMalEntity` interface.

## 23.11.2018 - Version 1.1.0 

- Integration with Jikan API v3
- New endpoints
    - Genre
        - Anime genres
        - Manga genres
    - Producer
    - Magazine
    - User
        - Profile
        - Friends
        - History
            - Filter by Anime/Manga.
        - Anime list
            - Filter by status (watching, completed, etc.)
            - Paging support
        - Manga list
            - Filter by status (reading, completed, etc.)
            - Paging support
    - Meta
        - API status
    - Top
        - People Top.
        - Characters Top.
    - Season Archive
- Extensions are no longer supported due to changes in REST API. Each type of extension now has separate method. Example:
    - Previously:
        - GetAnime(id) -> returns basic information about anime.
        - GetAnime(id, AnimeExtension.CharactersStaff) -> return basic information and characters/staff.
    - Currently:
        - GetAnime(id) -> returns basic information about anime.
        - GetAnimeCharactersStaff(id) -> return characters/staff of anime.
- <b>[Search]</b> Status enum renamed to AiringStatus
- <b>[Anime]</b>
    - Removed `AiredString`
    - `Pictures` is now collection of `Picture` type.
    - `StaffPositionEntry.Role` is now a collection.
    - `ForumPostSnippet.DateRelatice` is now DateTime.
- <b>[Manga]</b>
    - Removed `PublishedString`
    - `TitleSynonyms` are now a collection.
    - `Pictures` is now collection of `Picture` type.
    - `Authors`, `Genres` and `Serializations` are now `MALSubItem` collections.
- <b>[Character]</b>
    - `Nicknames` are now a collection.
    - `Images` got renamed to `Pictures` and now are collection of `Picture` type.
- <b>[Person]</b>
    - `Birthday` is now DateTime.
    - `Images` got renamed to `Pictures` and now are collection of `Picture` type.
- <b>[AnimeSearch]</b>
    - Add `Airing`, `StartDate`, `EndDate` and `Rated` data.
- <b>[MangaSearch]</b>
    - Add `Publishing`, `StartDate`, `EndDate` and `Chapters` data.
- <b>[CharactersSearch]</b>
    - `Nicknames` are now a collection.
- <b>[PersonSearch]</b>
    - `Nicknames` got renamed to `AlternativeNames` and are now a collection.
- <b>[Schedule]</b>
    - Filtering by day of the week is enabled now.

## 27.05.2018 - Version 1.0.3

- <b>[Season]</b> Add `SeasonName` and `SeasonYear` data.

## 20.05.2018 - Version 1.0.2 

- <b>[Character|Person]</b> Added add role info in animeography and mangaography (Character) and voice acting roles (Person).
- <b>[Season]</b> Added "type" and "continued" properties for season entry.

## 15.05.2018 - Version 1.0.1

- Fixed issue [#1](https://github.com/Ervie/jikan.net/issues/1) - error during parsing anime/manga with no related entries.

## 14.05.2018 - Version 1.0.0

- Requests for searching on MyAnimeList
- More overload methods.
- Initial version, up to date with Jikan API.

## 08.05.2018 - Version 0.3.0 

- Request for season preview.
- Request for anime schedule of current season.
- Request for Top Manga & Anime.

## 05.05.2018 - Version 0.2.0

- Extended request for Anime/Manga/Character/Person via extensions.

## 02.05.2018 - Version 0.1.0 

- First usable version.
- Added basic requests for Anime/Manga/Person/Character.