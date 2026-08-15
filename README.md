[![NuGet](https://img.shields.io/nuget/v/Tenrai.Net.svg)](https://www.nuget.org/packages/Tenrai.Net) [![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://opensource.org/licenses/MIT)

# Tenrai.Net

Tenrai.Net is a .NET wrapper for the [Tenrai](https://tenrai.org) RESTful API for parsing data from [MyAnimeList](https://myanimelist.com). The main objective of the wrapper is to simplify utilization of the Tenrai API, as strongly typed languages are not-so-easy to use with elastic json (sure, we can use dynamics in .NET, but let's think about performance).

Tenrai is a drop-in successor to the [Jikan](https://jikan.moe) v4 API, matching its schema closely so existing applications can migrate with mostly a base URL and package change.

## Credits

Tenrai.Net is based on, and a huge thank you goes to, [@Ervie](https://github.com/Ervie) who wrote the original [**Jikan.NET**](https://github.com/Ervie/jikan.net) library, as well as the [Jikan](https://jikan.moe) project itself. This library would not exist without their work.

### Differences from Jikan.NET

- **Not all endpoints are supported by Tenrai.** The methods for users, clubs, watch, forum topics, and anime/manga user updates are retained for source compatibility but are marked `[Obsolete]` and throw `NotSupportedException`. For user-specific data, use the [official MyAnimeList API](https://myanimelist.net/apiconfig/references/api/v2).
- **Richer reviews.** Reviews can be sorted (`newest`/`oldest`/`most_helpful`), filtered by preliminary/spoiler state as a tri-state (`Include`/`Exclude`/`Only`), and filtered by sentiment (`recommended`/`mixed_feelings`/`not_recommended`). See [review filtering](docs/API.md#reviews).
- **Server Key support.** Supply a Tenrai Server Key to raise rate limits (300/min, 5/sec, unlimited daily). See [Rate Limiting](docs/RateLimiting.md).
- **Bulk ID endpoints.** Fetch every MAL ID for anime, manga, characters, people, or producers (requires a Server Key).
- **More accurate data.** Tenrai fixes several Jikan issues server-side: correct totals, no duplicate entries in list endpoints, precise pagination (`last_visible_page`), and working spoiler sorting.

### Main attributes

* Written to work with .NET Standard 2.0, compatible with .NET Framework (4.6.1 or newer) and .NET (6.0 or newer).
* Fully asynchronous request fetching (can be forced to synchronous if needed).
* Light on dependencies
    * No dependencies if you are using .NET 6.0+
    * Single dependency for .NET Framework (System.Text.Json).
* Usable with Dependency Injection.

# List of features

- Anime
    - Basic / Full information, Characters, Staff, Episodes, News, Videos/PV/Episodes, Pictures, Statistics, More Info, Reviews, Recommendations, Related entries, Themes, External links, Streaming links
- Manga
    - Basic / Full information, Characters, News, Pictures, Statistics, More Info, Reviews, Recommendations, Related entries, External links
- People
    - Basic / Full information, Related anime, Related manga, Voice acting roles, Pictures
- Characters
    - Basic / Full information, Related anime, Related manga, Voice actors, Pictures
- Reviews (anime, manga, recent, top) with sort, sentiment, and preliminary/spoiler filtering
- Search: Anime, Manga, People, Characters
- Seasonal Anime: Current, Upcoming, Archival
- Anime scheduling (for current season)
- Top: Anime, Manga, People, Characters, Reviews
- Genres: Anime genres, Manga genres
- Producer: Basic information, External links, Full data
- Magazines
- Random: Anime, Manga, Character, Person
- Interest Stacks: Browse, search, full stack with entries in source order, stacks containing a given anime or manga
- Bulk MAL IDs: Anime, Manga, Characters, People, Producers *(requires a Server Key)*
- Service status snapshot

### Not supported by Tenrai

The following Jikan.NET endpoint families are retained but throw `NotSupportedException`: **Users**, **Clubs**, **Watch**, **Forum Topics**, and **anime/manga User Updates** (plus random user and user/club search).

# Installation

### Package manager

```
PM> Install-Package Tenrai.Net
```

### .NET CLI

```
> dotnet add package Tenrai.Net
```

Then restore dependencies:
```
> dotnet restore
```

# Changelog

## 15.08.2026 - Version 3.1.0

- Interest Stacks: browse, search, full stack with entries in source order, and the stacks containing a given anime or manga
- `GetStatusAsync` returns the public service status snapshot (targets the status host, so it ignores a custom base address and the client rate limiter)

## 07.07.2026 - Version 3.0.0

- Rebrand from `JikanDotNet` to `Tenrai.Net` targeting the Tenrai API (`https://api.tenrai.org/v1`) — major, breaking release
- `Jikan` → `TenraiClient`, `IJikan` → `ITenrai`, namespace `JikanDotNet` → `Tenrai`, `*JikanResponse` → `*TenraiResponse`
- Reviews gain sort, sentiment, and tri-state preliminary/spoiler filtering via `ReviewsSearchConfig`; `Review` adds `Tags` and `IsPreliminary`
- Server Key support (`X-Server-Key`) with a higher rate-limit tier; new bulk ID endpoints
- Unsupported endpoint families (users, clubs, watch, forum topics, user updates) marked `[Obsolete]` and throw `NotSupportedException`

## 25.04.2026 - Version 2.10.4 *(Jikan.NET)*

- Fix `TimePeriod.From`/`TimePeriod.To` losing UTC offset during deserialization (affects `Anime.Aired` and `Manga.Published`). Types changed from `DateTime?` to `DateTimeOffset?` - **breaking change**

**[Read the full changelog](Changelog.md)**

# Documentation & Usage example

See the [documentation](docs/README.md) for quick start, API reference, and guides.
