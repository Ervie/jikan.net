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

# Installation

### Package manager

```
PM> Install-Package JikanDotNet -Version 0.3.0
```

### .NET CLI

```
>dotnet add package JikanDotNet --version 0.3.0
```

Then restore dependencies:
```
>dotnet restore
```

# Changelog

## Upcoming - Version 1.0.0 

- Requests for searching on MyAnimeList
- More overload methods.
- Up to date with Jikan API.

## 08.05.2018 - Version 0.3.0 (latest)

- Request for season preview.
- Request for anime schedule of current season.
- Request for Top Manga & Anime.

## 05.05.2018 - Version 0.2.0

- Extended request for Anime/Manga/Character/Person via extensions.

## 02.05.2018 - Version 0.1.0 

- First usable version.
- Added basic requests for Anime/Manga/Person/Character.

# Documentation &  Usage example

See [project wiki](https://github.com/Ervie/jikan.net/wiki#usage-example).