## 23.11.2018 - Version 1.1.0 (latest)

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