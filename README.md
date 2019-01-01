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
    - Reviews
    - Recommendations
    - User Updates
- Manga
    - Basic information
    - Characters 
    - News
    - Pictures
    - Stats
    - Forum Topics
    - More Info
    - Reviews
    - Recommendations
    - User Updates
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
- Seasonal Anime 
    - Season + Year
    - Undefined airing date
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
        - Pagination support
    - Manga list
        - Filter by status (reading, completed, etc.)
        - Pagination support
- Clubs
    - Profile
    - Member list
        - Paging support
- Meta
    - API status
# Installation

### Package manager

```
PM> Install-Package JikanDotNet -Version 1.2.0
```

### .NET CLI

```
>dotnet add package JikanDotNet --version 1.2.0
```

Then restore dependencies:
```
>dotnet restore
```

# Changelog

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

**[Read More](https://github.com/Ervie/jikan.net/blob/master/Changelog.md)**

# Documentation &  Usage example

See [project wiki](https://github.com/Ervie/jikan.net/wiki#usage-example).