![build status](https://travis-ci.org/Ervie/jikan.net.svg?branch=master) ![build status](https://img.shields.io/nuget/v/JikanDotNet.svg)

# jikan.net

Jikan.net is a .NET wrapper for [Jikan](https://jikan.moe) RESTful API for parsing data from [MyAnimeList](https://myanimelist.com). Main objective of the wrapper is to simplify utilization of Jikan API, as strongly typed languages are not-so-easy to use with elastic json (sure we can go use dynamics in .NET, but let's think about performance).

### Main attributes

* Written in .Net Standard, compatible with .Net Framework and .Net Core.
* Fully asynchromous request fetching (can be forced to synchromous if needed).
* Can handle both SSL encrypted and non-SSL encrypted requests.
* Light on dependencies (require only Newtonsoft.Json for parsing).
* Usable with Dependency Injection.

# List of features

- Basic requests
    - [X] Anime
    - [X] Manga
    - [X] Character
    - [X] People
- Anime extended requests
    - [X] Characters & Staff
    - [X] Episode
    - [X] News
    - [X] Videos/PV/Episodes
    - [X] Pictures
    - [X] Stats
    - [X] Forum Topics
    - [X] More Info
    - [ ] Recommendation
    - [ ] Reviews
- Manga extended requests
    - [X] Characters
    - [X] News
    - [X] Stats
    - [X] Pictures
    - [X] Forum Topics
    - [X] More Info
    - [ ] Recommendation
    - [ ] Reviews
- Character extended requests
    - [X] Pictures
- People Parsing
    - [X] Pictures
- Search (Anime/Manga/Character/Person)
    - [X] Basic query
    - [X] Filters (Advanced Search)
    - [X] Pagination Support
    - [X] No.# of pages
- [X] Seasonal Anime (Season + Year)
- [X] Anime Scheduling (for current season)
- Top
    - [X] Anime
    - [X] Manga
    - [X] Sub Types & Pagination Support
- [ ] Genre
- [ ] Producer
- [ ] Magazine
- [ ] User
- [ ] Meta

# Installation

### Package manager

```
PM> Install-Package JikanDotNet -Version 1.0.3
```

### .NET CLI

```
>dotnet add package JikanDotNet --version 1.0.3
```

Then restore dependencies:
```
>dotnet restore
```

# Changelog

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


## 27.05.2018 - Version 1.0.3 (latest)

- <b>[Season]</b> Add `SeasonName` and `SeasonYear` data.

**[Read More](https://github.com/Ervie/jikan.net/blob/master/Changelog.md)**

# Documentation &  Usage example

See [project wiki](https://github.com/Ervie/jikan.net/wiki#usage-example).