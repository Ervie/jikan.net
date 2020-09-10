 ![build status](https://travis-ci.org/Ervie/jikan.net.svg?branch=master) ![build status](https://img.shields.io/nuget/v/JikanDotNet.svg) [![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://opensource.org/licenses/MIT) [![GitHub issues open](https://img.shields.io/github/issues/Ervie/jikan.net.svg?maxAge=2592000)]() 

# jikan.net

Jikan.net is a .NET wrapper for [Jikan](https://jikan.moe) RESTful API for parsing data from [MyAnimeList](https://myanimelist.com). Main objective of the wrapper is to simplify utilization of Jikan API, as strongly typed languages are not-so-easy to use with elastic json (sure we can go use dynamics in .NET, but let's think about performance).

### Main attributes

* Written in to work with .Net Standard 2.0, compatible with .Net Framework (4.6.1 or newer) and .Net Core (2.0 or newer).
* Fully asynchromous request fetching (can be forced to synchromous if needed).
* Can handle both SSL encrypted and non-SSL encrypted requests.
* Light on dependencies 
    * No dependencies if you are using .Net Core 3.x.
    * Single dependancy for .Net Framework and .Net Core 2.x (System.Text.Json).
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
        - Advanced filters
        - Pagination support
    - Manga list
        - Filter by status (reading, completed, etc.)
        - Advanced filters
        - Pagination support
- Clubs
    - Profile
    - Member list
        - Pagination support
- Meta
    - API status
# Installation

### Package manager

```
PM> Install-Package JikanDotNet
```

### .NET CLI

```
>dotnet add package JikanDotNet
```

Then restore dependencies:
```
>dotnet restore
```

# Changelog

## Future - Version 2.0.0

- Compataible with Jikan REST API v4.0

## 10.09.2020 - Version 1.5.0 (newest)

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
        

**[Read More](https://github.com/Ervie/jikan.net/blob/master/Changelog.md)**

# Documentation &  Usage example

See [project wiki](https://github.com/Ervie/jikan.net/wiki#usage-example).
