![build status](https://travis-ci.org/Ervie/jikan.net.svg?branch=master) ![build status](https://img.shields.io/nuget/v/JikanDotNet.svg)

# jikan.net

Jikan.net is a .NET wrapper for [Jikan](https://jikan.moe) RESTful API for parsing data from [MyAnimeList](https://myanimelist.com). Main objective of the wrapper is to simplify utilization of Jikan API, as strongly typed languages are not-so-easy to use with elastic json (sure we can go use dynamics in .NET, but let's think about performance).

### Main attributes

* Written in .Net Standard 2.0, compatible with .Net Framework (4.6.1 or newer) and .Net Core (2.0 or newer).
* Fully asynchromous request fetching (can be forced to synchromous if needed).
* Can handle both SSL encrypted and non-SSL encrypted requests.
* Light on dependencies (require only Newtonsoft.Json for parsing).
* Usable with Dependency Injection.

# List of features


- Anime
    - Basic information
    - Characters & Staff
    - Episode
    - News
    - Videos/PV/Episodes
    - Pictures
    - Stats
    - Forum Topics
    - More Info
- Manga
    - Basic information
    - Characters 
    - News
    - Pictures
    - Stats
    - Forum Topics
    - More Info
- People
    - Basic information
    - Pictures
- Characters
    - Basic information
    - Pictures
- Search (Anime/Manga/Character/Person)
    - Basic query
    - Filters (Advanced Search)
    - Pagination Support
    - No.# of pages
- Seasonal Anime (Season + Year)
- Season Archive
- Anime Scheduling (for current season)
    - Filtering by day of the week.
- Top
    - Anime
    - Manga
    - People
    - Characters
    - Sub Types & Pagination Support
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
# Installation

### Package manager

```
PM> Install-Package JikanDotNet -Version 1.1.0
```

### .NET CLI

```
>dotnet add package JikanDotNet --version 1.1.0
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

**[Read More](https://github.com/Ervie/jikan.net/blob/master/Changelog.md)**

# Documentation &  Usage example

See [project wiki](https://github.com/Ervie/jikan.net/wiki#usage-example).