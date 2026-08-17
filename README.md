 [![Discord Server](https://img.shields.io/discord/460491088004907029.svg?style=flat&logo=discord)](https://discord.gg/4tvCr36) ![build status](https://img.shields.io/nuget/v/JikanDotNet.svg) [![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://opensource.org/licenses/MIT) [![GitHub issues open](https://img.shields.io/github/issues/Ervie/jikan.net.svg?maxAge=2592000)]() 

## Important notice: Jikan public API shutdown and library dormancy

The [Jikan REST API](https://jikan.moe) that this library wraps will be discontinued. JikanDotNet depends on that upstream service, so **this repository is entering dormancy**. No new features will be added to the wrapper.

### Announcement Info

- **Effective immediately (June 14, 2026):** No new features will be added; the public API is in maintenance mode only
- **September 1, 2026:** public API will enter brownout mode
- **October 1, 2026:** public API will be fully discontinued

Please begin planning to migrate toward a private/self-hosted Jikan instance or away from the Jikan public API. Upstream is working on making the Jikan REST API easier to self-host.

The direct successor to Jikan is [Tenrai.net](https://github.com/Kareadita/tenrai.net), a .NET wrapper for the Tenrai REST API.

Existing NuGet packages remain available. After October 1, 2026, the default public endpoint (`https://api.jikan.moe/v4/`) will no longer work. You can still point this client at a self-hosted instance by setting `HttpClient.BaseAddress` (see [Using Own Instance of Jikan API](docs/GettingStarted.md#using-own-instance-of-jikan-api)).

If you are starting a new project, prefer [Tenrai.net](https://github.com/Kareadita/tenrai.net), or consider the [official MyAnimeList API](https://myanimelist.net/apiconfig).

# jikan.net

Jikan.net is a .NET wrapper for [Jikan](https://jikan.moe) RESTful API for parsing data from [MyAnimeList](https://myanimelist.com). Main objective of the wrapper is to simplify utilization of Jikan API, as strongly typed languages are not-so-easy to use with elastic json (sure we can go use dynamics in .NET, but let's think about performance).

### Main attributes

* Written in to work with .NET Standard 2.0, compatible with .NET Framework (4.6.1 or newer) and .NET (6.0 or newer).
* Fully asynchromous request fetching (can be forced to synchromous if needed).
* Light on dependencies 
    * No dependencies if you are using .NET 6.0+
    * Single dependancy for .NET Framework (System.Text.Json).
* Usable with Dependency Injection.

# List of features

- Anime
    - Basic information
    - Characters 
    - Staff
    - Episode
    - News
    - Videos/PV/Episodes
    - Pictures
    - Statistics
    - Forum Topics
    - More Info
    - Reviews
    - Recommendations
    - User Updates
    - Related entries
    - Themes
    - External links
    - Full information
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
    - Related entries
    - External links
    - Full information
- People
    - Basic information
    - Related anime
    - Related manga
    - Voice acting roles
    - Pictures
    - Full information
- Characters
    - Basic information
    - Related anime
    - Related manga
    - Voice actors
    - Pictures
    - Full information
- Search 
    - Anime
    - Manga
    - People
    - Characters
    - Users
    - Clubs
- Seasonal Anime
    - Current
    - Upcoming
    - Archival
- Anime Scheduling (for current season)
- Top
    - Anime
    - Manga
    - People
    - Characters
    - Reviews
- Genre
    - Anime genres
    - Manga genres
- Producer
    - Basic information
    - External links
    - Full data
- Magazine
- User
    - Profile
    - Friends
    - History
    - Statistics
    - Favorites
    - About
    - Reviews
    - Recommendations
    - Clubs
    - Full data
- Clubs
    - Profile
    - Member list
    - Staff
    - Relations
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

## 17.08.2026 - Project status

- Jikan public API shutdown announced; this wrapper is entering dormancy
- Direct successor: [Tenrai.net](https://github.com/Kareadita/tenrai.net)
- Effective immediately (June 14, 2026): No new features will be added; the public API is in maintenance mode only
- September 1, 2026: public API will enter brownout mode
- October 1, 2026: public API will be fully discontinued

## 25.04.2026 - Version 2.10.4

- Fix `TimePeriod.From`/`TimePeriod.To` losing UTC offset during deserialization (affects `Anime.Aired` and `Manga.Published`). Types changed from `DateTime?` to `DateTimeOffset?` - **breaking change**

## 22.04.2026 - Version 2.10.3

- Add `StartDate`/`EndDate` (`DateTime?`) to `AnimeSearchConfig` and `MangaSearchConfig`
- Fix excluded genres parameter in search config for `SearchAnimeAsync` and `SearchMangaAsync`

## 08.03.2026 - Version 2.10.2

- Add missing API method overloads (manga forum topic filter, video episodes pagination, watch promos pagination)
- Fix `SearchClubAsync` using incorrect endpoint
- Fix `GetAnimeVideosEpisodesAsync` return type to `PaginatedJikanResponse`

**[Read More](https://github.com/Ervie/jikan.net/blob/master/Changelog.md)**

# Documentation & Usage example

See [documentation](docs/README.md) for quick start, API reference, and guides.
